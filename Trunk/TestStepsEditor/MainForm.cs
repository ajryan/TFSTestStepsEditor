using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Microsoft.TeamFoundation.TestManagement.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TestStepsEditor
{
	public partial class MainForm : Form
	{
		private ServerSettings _serverSettings;
		private readonly Dictionary<int, TestEditInfo> _tabTestInfoMap = new Dictionary<int, TestEditInfo>();

		public MainForm()
		{
			InitializeComponent();
			
			_toolStripContainer.Enabled = false;

			Shown += (
				(o, e) =>
				{
					try
					{
						_testStateToolStripLabel.Text = "Connecting...";

						_serverSettings = ServerSettings.Load();
						if (_serverSettings.TestProject != null)
			         		_connStateToolStripLabel.Text = _serverSettings.TestProject.WitProject.Name;

						_toolStripContainer.Enabled = true;

						_workItemIdToolStripComboBox.ComboBox.DataSource = _serverSettings.WorkItemIdMru;
						_workItemIdToolStripComboBox.Focus();

						_testStateToolStripLabel.Text = "Connected.";
					}
					catch (Exception ex)
					{
						_toolStripContainer.Enabled = true;

						MessageBox.Show("Could not load previous TFS connection.\n\nError:\n" + ex.Message);
						_connStateToolStripLabel.Text = "(not connected)";
					}
				});
		}

		private DataGridView CurrentGridView
		{
			get
			{
				return _tabTestInfoMap[_testTabControl.SelectedIndex].DataGridView;
			}
		}

		private SimpleSteps CurrentSimpleSteps
		{
			get
			{
				return _tabTestInfoMap[_testTabControl.SelectedIndex].SimpleSteps;
			}
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);

			IntPtr hSysMenu = Win32.GetSystemMenu(this.Handle, false);
			Win32.AppendMenu(hSysMenu, Win32.MF_SEPARATOR, 0, string.Empty);
			Win32.AppendMenu(hSysMenu, Win32.MF_STRING, 0x1, "Toggle &Always On Top");
		}

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			if (m.Msg == Win32.WM_SYSCOMMAND && (int)m.WParam == 0x1)
			{
				this.TopMost = !this.TopMost;
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			bool handled = false;

			if (msg.Msg == 256)
			{
				if (keyData == Keys.F3)
				{
					_findToolStripButton.PerformClick();
				}
				else
				{
					Component focusedComponent = Win32.GetFocusedControl();

					if ((keyData & Keys.V) == Keys.V &&
					    ModifierKeys == Keys.Control &&
					    focusedComponent == CurrentGridView)
					{
						PasteClipboard();
						handled = true;
					}

					if ((keyData & Keys.S) == Keys.S &&
						ModifierKeys == Keys.Control)
					{
						_saveToolStripButton.PerformClick();
						handled = true;
					}
				}
			}

			return handled || base.ProcessCmdKey(ref msg, keyData);
		}

		#region Event handlers

		private void MainForm_Closing(object sender, FormClosingEventArgs e)
		{
			foreach (var testInfo in _tabTestInfoMap.Values)
			{
				if (!testInfo.SimpleSteps.Dirty)
					continue;

				var confirmExit = MessageBox.Show(
					"One or more open tests has not been saved. Are you sure you want to exit?",
					"Confirm exit",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button2);

				if (confirmExit == DialogResult.No)
					e.Cancel = true;

				return;
			}
		}

		private void CloseCurentButton_Click(object sender, EventArgs e)
		{
			if (_testTabControl.SelectedTab == null)
				return;

			if (CurrentSimpleSteps.Dirty)
			{
				var sureClose = MessageBox.Show(
					"The test case has been modified. Are you sure you want to close?",
					"Confirm close",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button2);

				if (sureClose == DialogResult.No)
					return;
			}

			int removedIndex = _testTabControl.SelectedIndex;
			_testTabControl.TabPages.RemoveAt(removedIndex);
			
			_tabTestInfoMap.Remove(removedIndex);
			
			for (int tabIndex = removedIndex + 1; tabIndex <= _testTabControl.TabCount; tabIndex++)
			{
				var movedTestInfo = _tabTestInfoMap[tabIndex];
				_tabTestInfoMap.Remove(tabIndex);
				_tabTestInfoMap[tabIndex - 1] = movedTestInfo;
			}
		}

		private void SaveButton_Click(object sender, EventArgs e)
		{
			_testStateToolStripLabel.Text = "Saving...";
			_toolStripContainer.Enabled = false;
			
			_saveTestBackgroundWorker.RunWorkerAsync(
				_tabTestInfoMap[_testTabControl.SelectedIndex]);
		}

		private void SaveTestBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			var saveTestInfo = e.Argument as TestEditInfo;
			var testCase = saveTestInfo.TestCase;
			var simpleSteps = saveTestInfo.SimpleSteps;

			if (testCase == null || simpleSteps == null || simpleSteps.Count == 0)
				return;

			try
			{
				int stepNumber = 0;
				foreach (SimpleStep step in simpleSteps)
				{
					if (testCase.Actions.Count <= stepNumber)
					{
						testCase.Actions.Add(testCase.CreateTestStep());
					}

					// directly apply to existing ITestSteps
					if (testCase.Actions[stepNumber] is ITestStep)
					{
						((ITestStep)testCase.Actions[stepNumber]).Title = new ParameterizedString(step.Title);
						((ITestStep)testCase.Actions[stepNumber]).ExpectedResult = new ParameterizedString(step.ExpectedResult);
					}
					// current action is not ITestStep: if user-entered data is a step
					else if (step.IsTestStep())
					{
						// insert a new action at this point
						var newAction = testCase.CreateTestStep();
						newAction.Title = new ParameterizedString(step.Title);
						newAction.ExpectedResult = new ParameterizedString(step.ExpectedResult);

						testCase.Actions.Insert(stepNumber, newAction);
					}

					stepNumber++;
				}

				while (stepNumber < testCase.Actions.Count)
					testCase.Actions.RemoveAt(stepNumber);

				testCase.Save();
				simpleSteps.ResetDirtyState();

				e.Result = String.Empty;
			}
			catch (Exception ex)
			{
				string errorMessage = "An error occured while attempting to save the test case.";
				Exception innerEx = ex;
				while (innerEx != null)
				{
					errorMessage += Environment.NewLine + innerEx.Message;
					innerEx = innerEx.InnerException;
				}

				e.Result = errorMessage;
			}
		}

		private void SaveTestBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			_toolStripContainer.Enabled = true;

			string result = e.Result as string;
			if (!String.IsNullOrWhiteSpace(result))
			{
				_testStateToolStripLabel.Text = "Error.";
				MessageBox.Show(result, "Error saving test case.");
			}
			else
			{
				_testStateToolStripLabel.Text = "Saved.";
			}
		}

		private void LoadButton_Click(object sender, EventArgs e)
		{
			int workItemId;
			if (!Int32.TryParse(_workItemIdToolStripComboBox.Text, out workItemId))
				return;

			foreach (var testEditKvp in _tabTestInfoMap)
			{
				if (testEditKvp.Value.WorkItemId == workItemId)
				{
					_testTabControl.SelectedIndex = testEditKvp.Key;
					return;
				}
			}

			_testStateToolStripLabel.Text = "Loading...";
			_toolStripContainer.Enabled = false;

			_loadTestBackgroundWorker.RunWorkerAsync(new TestEditInfo(workItemId));
		}
		
		private void LoadTestBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			var testInfo = e.Argument as TestEditInfo;

			try
			{
				testInfo.TestCase =
					_serverSettings.TestProject.TestCases.Find(testInfo.WorkItemId)
					?? (ITestBase) _serverSettings.TestProject.SharedSteps.Find(testInfo.WorkItemId);

				if (testInfo.TestCase == null)
					testInfo.Message = "Input test case ID not found.";
			}
			catch (DeniedOrNotExistException ex)
			{
				testInfo.Message = "Could not load test case: " + ex.Message;
			}

			e.Result = testInfo;
		}

		private void LoadTestBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			var testInfo = e.Result as TestEditInfo;
			if (!String.IsNullOrWhiteSpace(testInfo.Message))
			{
				_toolStripContainer.Enabled = true;
				_testStateToolStripLabel.Text = "Error.";

				MessageBox.Show(testInfo.Message, "Could not load test case");
				return;
			}

			SimpleSteps newSteps = TryCreateSimpleSteps(testInfo);
			if (newSteps == null)
				return;

			testInfo.SimpleSteps = newSteps;
			
			var newDataGridView = CreateTestTabAndDataGridView(testInfo.TestCase.Id + " " + testInfo.TestCase.Title);
			if (newDataGridView == null)
				return;

			try
			{
				testInfo.DataGridView = newDataGridView;

				newDataGridView.SuspendLayout();

				newDataGridView.DataSource = testInfo.SimpleSteps;

				_toolStripContainer.Enabled = true;
				newDataGridView.ResumeLayout(true);

				newDataGridView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
				newDataGridView.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

				newDataGridView.Focus();

				int newTabIndex = _testTabControl.TabCount - 1;
				_tabTestInfoMap[newTabIndex] = testInfo;

				_testStateToolStripLabel.Text = "Loaded.";
				_workItemIdToolStripComboBox.SelectedIndex = -1;

				_serverSettings.AddWorkItemIdMru(testInfo.TestCase.Id);
				_serverSettings.SaveToRegistry();
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					"Could not display test case: " + ex.Message,
					"Error displaying test case");

				_testTabControl.TabPages.RemoveAt(_testTabControl.TabCount - 1);
			}
		}

		private void ChangeProjectButton_Click(object sender, EventArgs e)
		{
			var newServerSettings = ServerSettings.LoadProjectSelectionFromUser();
			if (newServerSettings == null)
				return;

			_serverSettings = newServerSettings;

			_workItemIdToolStripComboBox.ComboBox.DataSource = _serverSettings.WorkItemIdMru;

			if (_serverSettings.TestProject != null)
				_connStateToolStripLabel.Text = _serverSettings.TestProject.WitProject.Name;

			_tabTestInfoMap.Clear();
			_testTabControl.TabPages.Clear();
		}

		private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar != 13)
				return;
			
			if (sender == _workItemIdToolStripComboBox)
			{
				_loadToolStripButton.PerformClick();
			}
			if (sender == _findToolStripTextBox)
			{
				_findToolStripButton.PerformClick();
			}
			if (sender == _replaceToolStripTextBox)
			{
				_replaceToolStripSplitButton.PerformClick();
			}

			e.Handled = true;
		}

		private void InsertButton_Click(object sender, EventArgs e)
		{
			if (CurrentGridView == null || CurrentGridView.CurrentRow == null)
				return;
			
			_tabTestInfoMap[_testTabControl.SelectedIndex]
				.SimpleSteps.Insert(CurrentGridView.CurrentRow.Index, new SimpleStep());

			CurrentGridView.Focus();
		}

		private void DeleteButton_Click(object sender, EventArgs e)
		{
			if (CurrentGridView == null || CurrentGridView.CurrentRow == null)
				return;

			_tabTestInfoMap[_testTabControl.SelectedIndex]
				.SimpleSteps.RemoveAt(CurrentGridView.CurrentRow.Index);

			CurrentGridView.Focus();
		}

		private void FindButton_Click(object sender, EventArgs e)
		{
			string findText = _findToolStripTextBox.Text.Trim();
			if (String.IsNullOrEmpty(findText))
				return;

			var searcher = new DataGridViewSearcher(CurrentGridView);

			bool found = searcher.Search(findText);
			
			if (!found)
				MessageBox.Show("\"" + findText + "\" was not found.", "Not Found");
		}

		private void ReplaceAllButton_Click(object sender, EventArgs e)
		{
			ReplaceInCells(false /* not selected only */);
		}

		private void ReplaceSelectionButton_Click(object sender, EventArgs e)
		{
			ReplaceInCells(true /* selected only */);
		}

		private void TestGridContext_Copy_Click(object sender, EventArgs e)
		{
			if (CurrentGridView == null)
				return;

			DataObject clipboardData = CurrentGridView.GetClipboardContent();
			if (clipboardData != null)
				Clipboard.SetDataObject(clipboardData);
		}

		private void TestGridContext_Paste_Click(object sender, EventArgs e)
		{
			PasteClipboard();
		}

#endregion Event handlers

		private void ReplaceInCells(bool selectedOnly)
		{
			string findText = _findToolStripTextBox.Text.Trim();
			if (String.IsNullOrEmpty(findText))
				return;

			string replaceText = _replaceToolStripTextBox.Text.Trim();
			if (String.IsNullOrEmpty(replaceText))
				return;

			var searcher = new DataGridViewSearcher(CurrentGridView);
			bool replaced = searcher.Replace(findText, replaceText, selectedOnly);

			if (!replaced)
				MessageBox.Show("\"" + findText + "\" was not found.", "No Replacements Performed");
		}

		private static SimpleSteps TryCreateSimpleSteps(TestEditInfo testInfo)
		{
			try
			{
				var newSteps = new SimpleSteps(testInfo.TestCase.Actions.Count);

				foreach (ITestAction action in testInfo.TestCase.Actions)
				{
					if (action is ITestStep)
					{
						var testStep = action as ITestStep;
						newSteps.Add(new SimpleStep(testStep.Title.ToString(), testStep.ExpectedResult.ToString()));
					}
					else if (action is ISharedStepReference)
					{
						var sharedStep = action as ISharedStepReference;
						newSteps.Add(new SimpleStep("Shared step ID " + sharedStep.SharedStepId, String.Empty, false));
					}
					else
					{
						newSteps.Add(new SimpleStep("Unknown action " + action.Id, String.Empty, false));
					}
				}
				return newSteps;
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					"Could not load steps from test case: " + ex.Message,
					"Could not load test case");

				return null;
			}
		}

		private DataGridView CreateTestTabAndDataGridView(string tabTitle)
		{
			string newTabKey = _testTabControl.TabCount.ToString();

			var newDataGridView = new NumberedDataGridView
			{
				Dock = DockStyle.Fill,
				AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
				AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders,
				ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText,
				ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
				ContextMenuStrip = _testGridContextMenu,
				DefaultCellStyle = new DataGridViewCellStyle
				{
					Alignment = DataGridViewContentAlignment.TopLeft,
					WrapMode = DataGridViewTriState.True
				}
			};

			_testTabControl.TabPages.Add(newTabKey, tabTitle);
			_testTabControl.TabPages[newTabKey].Controls.Add(newDataGridView);
			_testTabControl.SelectedIndex = (_testTabControl.TabCount - 1);

			return newDataGridView;
		}

		private void PasteClipboard()
		{
			if (CurrentGridView == null)
				return;

			try
			{
				string clipboardText = Clipboard.GetText();
				if (String.IsNullOrWhiteSpace(clipboardText))
					return;

				int selectedCount = CurrentGridView.GetCellCount(DataGridViewElementStates.Selected);

				if (selectedCount == 0 && CurrentGridView.CurrentCell == null)
					return;

				int rowIndex = CurrentGridView.CurrentCell.RowIndex;
				int colIndex = CurrentGridView.CurrentCell.ColumnIndex;

				if (selectedCount > 1)
				{
					List<DataGridViewCell> selectedCells = new List<DataGridViewCell>(selectedCount);
					selectedCells.AddRange(CurrentGridView.SelectedCells.Cast<DataGridViewCell>());

					rowIndex = selectedCells.Min(c => c.RowIndex);
					colIndex = selectedCells.Min(c => c.ColumnIndex);
				}
				
				string[] clipboardLines = clipboardText.Split(new []{'\n'}, StringSplitOptions.RemoveEmptyEntries);
				foreach (string clipboardLine in clipboardLines)
				{
					if (rowIndex >= CurrentGridView.RowCount || clipboardLine.Length <= 0)
						break;
					
					string[] clipboardLineTokens = clipboardLine.Split('\t');
					for (int tokenIndex = 0; tokenIndex < clipboardLineTokens.Length; ++tokenIndex)
					{
						if (colIndex + tokenIndex >= CurrentGridView.ColumnCount)
							break;
						
						DataGridViewCell targetCell = CurrentGridView[colIndex + tokenIndex, rowIndex];
						if (targetCell.Value.ToString() != clipboardLineTokens[tokenIndex])
						{
							targetCell.Value = Convert.ChangeType(
								clipboardLineTokens[tokenIndex], targetCell.ValueType);
						}
					}
					rowIndex++;
				}
			}
			catch (FormatException)
			{
				MessageBox.Show("The pasted data is in the wrong format for the target cell(s)");
			}
		}
	}
}
