using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Ionic.Zip;
using Microsoft.TeamFoundation.TestManagement.Client;
using TestStepsEditor.Tfs;

namespace TestStepsEditor.Gui
{
	public partial class TestEditUserControl : UserControl
	{
		[DllImport("user32")]
		private static extern bool SendMessage(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);

		private uint LVM_SETTEXTBKCOLOR = 0x1026;

		private readonly TestEditInfo _testInfo;

		private TestStepsDataGridView _testDataGridView;
		private int _displayedRow = -1;

		public TestEditUserControl(TestEditInfo testInfo)
		{
			_testInfo = testInfo;

			InitializeComponent();

            Load += (o, e) => 
                SendMessage(
                    _attachmentListView.Handle,
                    LVM_SETTEXTBKCOLOR,
                    IntPtr.Zero,
                    unchecked((IntPtr)(int)0xFFFFFF));
		}

		public static TestEditUserControl Create(TestEditInfo testInfo, ContextMenuStrip contextMenuStrip, bool showResultsPanel)
		{
			var newControl = new TestEditUserControl(testInfo);

			newControl.TestDataGridView = TestStepsDataGridView.Create(
				testInfo.SimpleSteps, contextMenuStrip);

			newControl._splitContainer.Panel1.Controls.Add(newControl.TestDataGridView);
			newControl._splitContainer.Panel2Collapsed = !showResultsPanel;

			return newControl;
		}

		public TestStepsDataGridView TestDataGridView
		{
			get { return _testDataGridView; }
			private set
			{
				if (_testDataGridView != null)
				{
					_testDataGridView.CurrentCellChanged -= TestDataGridView_CurrentCellChanged;
					_testDataGridView.CellValueChanged -= TestDataGridView_CellValueChanged;
					_testDataGridView.SelectionChanged -= TestDataGridView_SelectionChanged;
				}

				_testDataGridView = value;
				_testDataGridView.CurrentCellChanged += TestDataGridView_CurrentCellChanged;
				_testDataGridView.CellValueChanged += TestDataGridView_CellValueChanged;
				_testDataGridView.SelectionChanged += TestDataGridView_SelectionChanged;
			}
		}

		public void SaveCurrentResultsToZip(string presetPath = null)
		{
			string savePath = presetPath;
			if (String.IsNullOrEmpty(savePath))
			{
				var saveFileDialog = new SaveFileDialog
				{
					AddExtension = true,
					AutoUpgradeEnabled = true,
					DefaultExt = ".zip",
					Filter = "Zip archive (*.zip)|*.zip",
					OverwritePrompt = true,
					Title = "Save current results",
				};

				var saveFileResult = saveFileDialog.ShowDialog(this);
				if (saveFileResult == DialogResult.Cancel)
					return;

				savePath = saveFileDialog.FileName;
			}

			var outcomePath = SaveCurrentOutcomes();

			using (var resultZipFile = new ZipFile())
			{
				resultZipFile.AddFile(outcomePath, "");
				foreach (var step in _testInfo.SimpleSteps)
				{
					foreach (var attachment in step.AttachmentPaths)
					{
						resultZipFile.AddFile(attachment, "");
					}
				}
				resultZipFile.Save(savePath);
			}
		}

		private string SaveCurrentOutcomes()
		{
			var outcomes = _testInfo.SimpleSteps.Select(s => s.Outcome.ToString());
			var outcomeList = String.Join(",", outcomes);
			string outcomePath = Path.Combine(EnsureTestCaseAttachmentFolder(), "outcomes.txt");
			File.WriteAllText(outcomePath, outcomeList);
			return outcomePath;
		}

		public void LoadResultsFromZip()
		{
			var openFileDialog = new OpenFileDialog
			{
				AutoUpgradeEnabled = true,
				DefaultExt = ".zip",
				Filter = "Zip archive (*.zip)|*.zip",
				Title = "Save current results",
			};

			var openFileResult = openFileDialog.ShowDialog(this);
			if (openFileResult == DialogResult.Cancel)
				return;

			// delete contents of current directory
			Directory.Delete(EnsureTestCaseAttachmentFolder(), true);

			// extract zip
			string extractPath = EnsureTestCaseAttachmentFolder();
			using (var resultZipFile = ZipFile.Read(openFileDialog.FileName))
			{
				resultZipFile.ExtractAll(extractPath);
			}

			LoadCurrentResults();
		}

		public void ResetResults()
		{
			var resetConfirm = MessageBox.Show(
				"This action will reset all outcomes to Inconclusive and remove all screenshots. Are you sure you want to clear results?",
				"Clear Results",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);

			if (resetConfirm != DialogResult.Yes)
				return;

			string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			string autoZipFileName = String.Format("{0}_{1}.zip", _testInfo.TestCase.Id, DateTime.Now.ToString("ddMMMyyyy_hhmmss"));
			SaveCurrentResultsToZip(Path.Combine(desktopPath, autoZipFileName));

			ClearOutcomes();
			SaveCurrentOutcomes();
			_testDataGridView.Invalidate(true);

			ClearAttachments();
			RefreshImagePane();
		}

		public void LoadCurrentResults()
		{
			SuspendLayout();
			_testDataGridView.SuspendLayout();

			// load outcomes
			ClearOutcomes();
			string outcomePath = Path.Combine(EnsureTestCaseAttachmentFolder(), "outcomes.txt");
			if (File.Exists(outcomePath))
			{
				string outcomes = File.ReadAllText(outcomePath);
				var outcomeTokens = outcomes.Split(',');

				if (outcomeTokens.Length == _testInfo.SimpleSteps.Count)
				{
					for (int stepIndex = 0; stepIndex < _testInfo.SimpleSteps.Count; stepIndex++)
					{
						var outcome = (TestOutcome) Enum.Parse(typeof (TestOutcome), outcomeTokens[stepIndex]);

						var step = _testInfo.SimpleSteps[stepIndex];
						step.Outcome = outcome;

						_testDataGridView[TestStepsDataGridView.OUTCOME_COLUMN, stepIndex].Value = outcome;
					}
					_testDataGridView.Invalidate(true);
				}
			}

			ClearAttachments();
			LoadAttachments();

			_testDataGridView.ResumeLayout();
			ResumeLayout();

			RefreshImagePane();
		}

		public void ShowResultsPanel()
		{
			_splitContainer.Panel2Collapsed = false;
		}

		public void HideResultsPanel()
		{
			_splitContainer.Panel2Collapsed = true;
		}

		#region Event handlers

		void TestDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == TestStepsDataGridView.OUTCOME_COLUMN)
				SaveCurrentOutcomes();
		}

		void TestDataGridView_SelectionChanged(object sender, EventArgs e)
		{
			var rowsWithSel = _testDataGridView.SelectedCells.Cast<DataGridViewCell>().Select(c => c.RowIndex).Distinct();
			_captureButton.Enabled = rowsWithSel.Count() == 1;
		}

		private void CaptureButton_Click(object sender, EventArgs e)
		{
			var image = Clipboard.GetImage();
			if (image == null)
			{
				MessageBox.Show("There is no image in the cipboard.", "Could Not Capture");
				return;
			}

			int imageIndex = CurrentAttachmentPaths.Count;
			string imagePath = GetImagePath(imageIndex);

			image.Save(imagePath, ImageFormat.Png);
			AppendImage(imageIndex, imagePath);
			CurrentAttachmentPaths.Add(imagePath);

			_attachmentListView.Invalidate(true);
		}

		private void DeleteButton_Click(object sender, EventArgs e)
		{
			if (_attachmentListView.SelectedItems.Count == 0)
				return;

			for (int attachmentIndex = _attachmentListView.Items.Count - 1;
				attachmentIndex >= 0;
				attachmentIndex--)
			{
				var listItem = _attachmentListView.Items[attachmentIndex];
				if (!listItem.Selected)
					continue;

				_attachmentImageList.Images.RemoveAt(attachmentIndex);

				string attachmentPath = (string) listItem.Tag;
				File.Delete(attachmentPath);
				listItem.Remove();

				CurrentAttachmentPaths.RemoveAt(attachmentIndex);
			}

			// fix up filenames
			for (int attachmentIndex = 0;
				attachmentIndex < CurrentAttachmentPaths.Count;
				attachmentIndex++)
			{
				string currentAttachmentPath = CurrentAttachmentPaths[attachmentIndex];
				string correctAttachmentPath = GetImagePath(attachmentIndex);

				if (currentAttachmentPath == correctAttachmentPath)
					continue;

				File.Move(currentAttachmentPath, correctAttachmentPath);
				
				var listViewItem = _attachmentListView.Items[attachmentIndex];
				listViewItem.Text = Path.GetFileName(correctAttachmentPath);
				listViewItem.Tag = correctAttachmentPath;

				CurrentAttachmentPaths[attachmentIndex] = correctAttachmentPath;
			}
		}

		private void AttachmentListView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			var hitLocation = _attachmentListView.HitTest(e.Location);
			if (hitLocation.Item == null)
				return;

			string attachmentPath = (string) hitLocation.Item.Tag;
			Process.Start(attachmentPath);
		}

		private void AttachmentListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			int selectedAttachmentCount = _attachmentListView.SelectedItems.Count;
			_deleteButton.Enabled =  selectedAttachmentCount > 0;

			int selectedIndex = selectedAttachmentCount == 1 ? _attachmentListView.SelectedIndices[0] : -1;
			_moveDownButton.Enabled = selectedAttachmentCount == 1 && selectedIndex < (_attachmentListView.Items.Count - 1);
			_moveUpButton.Enabled = selectedAttachmentCount == 1 && selectedIndex > 0;
		}

		private void TestDataGridView_CurrentCellChanged(object sender, EventArgs e)
		{
			int currentRow = _testDataGridView.CurrentRow == null
				? -1
				: _testDataGridView.CurrentRow.Index;

			if (_displayedRow == currentRow)
				return;

			_displayedRow = currentRow;

			_attachmentListView.Items.Clear();
			_attachmentImageList.Images.Clear();

			if (currentRow == -1)
				return;

			for (int attachmentIndex = 0; attachmentIndex < CurrentAttachmentPaths.Count; attachmentIndex++)
			{
				string attachmentPath = CurrentAttachmentPaths[attachmentIndex];
				AppendImage(attachmentIndex, attachmentPath);
			}
		}

		private void MoveUpButton_Click(object sender, EventArgs e)
		{
			MoveAttachment(_attachmentListView.SelectedIndices[0], MoveDirection.Up);
		}

		private void MoveDownButton_Click(object sender, EventArgs e)
		{
			MoveAttachment(_attachmentListView.SelectedIndices[0], MoveDirection.Down);
		}

		#endregion

		private enum MoveDirection { Up, Down }
		private void MoveAttachment(int attachmentIndex, MoveDirection moveDirection)
		{
			int direction = moveDirection == MoveDirection.Up ? -1 : 1;

			string movingFilePath = GetImagePath(attachmentIndex);
			string targetFilePath = GetImagePath(attachmentIndex + direction);

			string tmpFilePath = Path.Combine(EnsureTestCaseAttachmentFolder(), "TmpMovingImage.png");
			File.Move(targetFilePath, tmpFilePath);
			File.Move(movingFilePath, targetFilePath);
			File.Move(tmpFilePath, movingFilePath);

			ClearAttachments(false /* do not delete */);
			LoadAttachments();
			RefreshImagePane();

			_attachmentListView.SelectedIndices.Add(attachmentIndex + direction);
			_attachmentListView.Focus();
			_attachmentListView.FocusedItem = _attachmentListView.Items[attachmentIndex + direction];
		}

		private void LoadAttachments()
		{
			foreach (var filePath in Directory.GetFiles(EnsureTestCaseAttachmentFolder()))
			{
				string fileName = Path.GetFileName(filePath);

				int underscoreIndex = fileName.IndexOf('_');
				if (underscoreIndex == -1)
					continue;

				string stepNumberString = fileName.Substring(4, underscoreIndex - 4);
				int stepIndex = Int32.Parse(stepNumberString) - 1;

				if (_testInfo.SimpleSteps.Count < stepIndex + 1)
				{
					throw new InvalidOperationException(
						"The saved screenshots do not match the current test case: " + EnsureTestCaseAttachmentFolder());
				}

				_testInfo.SimpleSteps[stepIndex].AttachmentPaths.Add(filePath);
			}
		}

		private void AppendImage(int attachmentIndex, string attachmentPath)
		{
			Image listImage;
			using (var sourceImage = Image.FromFile(attachmentPath))
			{
				listImage = new Bitmap(sourceImage.Width, sourceImage.Height, PixelFormat.Format32bppArgb);
				using (var canvas = Graphics.FromImage(listImage))
					canvas.DrawImageUnscaled(sourceImage, 0, 0);
			}

			_attachmentImageList.Images.Add(listImage);

			_attachmentListView.Items.Add(new ListViewItem
			{
				Text = Path.GetFileName(attachmentPath),
				ImageIndex = attachmentIndex,
				Tag = attachmentPath
			});
		}

		private void RefreshImagePane()
		{
			_displayedRow = -1;
			TestDataGridView_CurrentCellChanged(null, null);

			_attachmentListView.SelectedItems.Clear();
			AttachmentListView_SelectedIndexChanged(null, null);
		}

		private string GetImagePath(int attachmentIndex)
		{
			string imageFileName = String.Format("Step{0}_Image{1}.png", _displayedRow + 1, attachmentIndex + 1);
			return Path.Combine(EnsureTestCaseAttachmentFolder(), imageFileName);
		}

		private string EnsureTestCaseAttachmentFolder()
		{
			string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			var attachmentFolder = Path.Combine(appData, @"TestStepsEditor\" + _testInfo.TestCase.Project.WitProject.Name + @"\" + _testInfo.WorkItemId);

			if (!Directory.Exists(attachmentFolder))
				Directory.CreateDirectory(attachmentFolder);

			return attachmentFolder;
		}

		private List<string> CurrentAttachmentPaths
		{
			get { return _testInfo.SimpleSteps[_displayedRow].AttachmentPaths; }
		}

		private void ClearAttachments(bool delete = true)
		{
			_attachmentListView.Items.Clear();
			_attachmentImageList.Images.Clear();
			foreach (var step in _testInfo.SimpleSteps)
			{
				if (delete)
				{
					foreach (string attachmentPath in step.AttachmentPaths)
						File.Delete(attachmentPath);
				}
				step.AttachmentPaths.Clear();
			}
		}

		private void ClearOutcomes()
		{
			for (int stepIndex = 0; stepIndex < _testInfo.SimpleSteps.Count; stepIndex++)
			{
				var step = _testInfo.SimpleSteps[stepIndex];
				step.Outcome = TestOutcome.Inconclusive;
				_testDataGridView[TestStepsDataGridView.OUTCOME_COLUMN, stepIndex].Value = TestOutcome.Inconclusive;
			}
		}
	}
}
