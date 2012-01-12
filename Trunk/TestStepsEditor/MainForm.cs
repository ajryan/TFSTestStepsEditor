using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.TestManagement.Client;

namespace TestStepsEditor
{
	public partial class MainForm : Form
	{
		private enum InsertLocation
		{
			Above,
			Below
		}

		private const int TITLE_COLUMN = 0;
		private const int EXPECTED_RESULT_COLUMN = 1;
		private const int DONE_COLUMN = 2;

		private TfsTeamProjectCollection _tfs;
		private ITestManagementTeamProject _testProject;
		private UserPreferences _userPreferences;
		private readonly Dictionary<int, TestEditInfo> _tabTestInfoMap = new Dictionary<int, TestEditInfo>();

		public MainForm()
		{
			InitializeComponent();
			
			_toolStripContainer.Enabled = false;

			_userPreferences = UserPreferences.Load();
			ApplyUserDisplayPreferences();

			Shown += (
				(o, e) =>
				{
					try
					{
						UseWaitCursor = true;
						_testStateToolStripLabel.Text = "Connecting...";
						Application.DoEvents();
						
						ApplyUserServerPreferences();
						_testStateToolStripLabel.Text = _testProject != null? "Connected." : String.Empty;

						_workItemIdToolStripComboBox.Focus();
						_toolStripContainer.Enabled = true;
					}
					catch (Exception ex)
					{
						_toolStripContainer.Enabled = true;

						MessageBox.Show("Could not load previous TFS connection.\n\nError:\n" + ex.Message, "Error Restoring Previous Connection");
						
						_changeProjectToolStripButton.Text = "(not connected)";
						_testStateToolStripLabel.Text = String.Empty;

						_userPreferences.TfsUri = null;
						_userPreferences.TestProject = null;

					}
					finally
					{
						UseWaitCursor = false;
					}
				});
		}

		private void ApplyUserServerPreferences()
		{
			if (_userPreferences == null ||
				_userPreferences.TfsUri == null ||
				String.IsNullOrEmpty(_userPreferences.TestProject))
			{
				return;
			}

			_tfs = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(_userPreferences.TfsUri);
			_tfs.Connect(ConnectOptions.IncludeServices);
			_testProject = _tfs.GetService<ITestManagementService>().GetTeamProject(_userPreferences.TestProject);

			_changeProjectToolStripButton.Text = _userPreferences.TestProject;
		}

		private void ApplyUserDisplayPreferences()
		{
			if (_userPreferences == null)
				return;

			_workItemIdToolStripComboBox.ComboBox.DataSource = _userPreferences.WorkItemIdMru;

			// if the saved location + size is not fully on-screen, do not override defaults
			bool useSavedLocation = false;
			if (_userPreferences.WindowSize.HasValue && _userPreferences.WindowLocation.HasValue)
			{
				Rectangle savedPosition = new Rectangle(
					_userPreferences.WindowLocation.Value.X,
					_userPreferences.WindowLocation.Value.Y,
					_userPreferences.WindowSize.Value.Width,
					_userPreferences.WindowSize.Value.Height);

				useSavedLocation = Screen.AllScreens.Any(screen => screen.WorkingArea.Contains(savedPosition));
			}

			if (useSavedLocation)
			{
				if (_userPreferences.WindowSize.Value.Height == -1)
					WindowState = FormWindowState.Maximized;
				else
					Size = _userPreferences.WindowSize.Value;
			
				StartPosition = FormStartPosition.Manual;
				Location = _userPreferences.WindowLocation.Value;
			}

			if (_userPreferences.WorkItemToolbarTop.HasValue && _userPreferences.WorkItemToolbarTop.Value)
			{
				if (_toolStripContainer.BottomToolStripPanel.Contains(_witToolStrip))
				{
					_toolStripContainer.BottomToolStripPanel.Controls.Remove(_witToolStrip);
					_toolStripContainer.TopToolStripPanel.Controls.Add(_witToolStrip);
				}
			}

			if (_userPreferences.FindToolbarTop.HasValue && _userPreferences.FindToolbarTop.Value)
			{
				if (_toolStripContainer.BottomToolStripPanel.Contains(_findToolStrip))
				{
					_toolStripContainer.BottomToolStripPanel.Controls.Remove(_findToolStrip);

					// remove and re-add wit toolstrip so it stays on top
					bool witTop = _toolStripContainer.TopToolStripPanel.Contains(_witToolStrip);
					if (witTop)
						_toolStripContainer.TopToolStripPanel.Controls.Remove(_witToolStrip);

					_toolStripContainer.TopToolStripPanel.Controls.Add(_findToolStrip);

					if (witTop)
						_toolStripContainer.TopToolStripPanel.Controls.Add(_witToolStrip);
				}
			}
		}

		private DataGridView CurrentGridView
		{
			get
			{
				if (_testTabControl.TabPages.Count < 1 || _tabTestInfoMap.Count < 1)
					return null;

				return _tabTestInfoMap[_testTabControl.SelectedIndex].DataGridView;
			}
		}

		private SimpleSteps CurrentSimpleSteps
		{
			get
			{
				if (_testTabControl.TabPages.Count < 1 || _tabTestInfoMap.Count < 1)
					return null;

				return _tabTestInfoMap[_testTabControl.SelectedIndex].SimpleSteps;
			}
		}

		private const int MENU_ALWAYS_ON_TOP = 0x1;
		private const int MENU_ABOUT = 0x2;

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);

			IntPtr hSysMenu = NativeMethods.GetSystemMenu(this.Handle, false);
			NativeMethods.AppendMenu(hSysMenu, NativeMethods.MF_SEPARATOR, new IntPtr(0), string.Empty);
			NativeMethods.AppendMenu(hSysMenu, NativeMethods.MF_STRING, new IntPtr(MENU_ALWAYS_ON_TOP), "Toggle &Always On Top");
			NativeMethods.AppendMenu(hSysMenu, NativeMethods.MF_STRING, new IntPtr(MENU_ABOUT), "About &TestStepsEditor...");
		}

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			if (m.Msg != NativeMethods.WM_SYSCOMMAND)
				return;

			switch ((int)m.WParam)
			{
				case MENU_ALWAYS_ON_TOP:
					this.TopMost = !this.TopMost;
					break;

				case MENU_ABOUT:
					{
						string version = ApplicationDeployment.IsNetworkDeployed
							? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
							: Assembly.GetExecutingAssembly().GetName().Version.ToString();
						
						MessageBox.Show(
							"Lonza TFS Test Steps Editor\r\nVersion: " + version,
							"About TFS Test Steps Editor");

						break;
					}
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (msg.Msg != 256)
				return base.ProcessCmdKey(ref msg, keyData);
			
			if (keyData == Keys.F3)
			{
				_findToolStripButton.PerformClick();
				return true;
			}

			if (keyData == Keys.Escape)
			{
				if (CurrentGridView != null)
					CurrentGridView.Focus();
				return true;
			}
				
			Component focusedComponent = NativeMethods.GetFocusedControl();

			if (keyData == (Keys.V | Keys.Control) &&
				focusedComponent == CurrentGridView)
			{
				PasteClipboard();
				return true;
			}

			if (keyData == (Keys.S | Keys.Control))
			{
				_saveToolStripButton.PerformClick();
				return true;
			}

			if (keyData == (Keys.F | Keys.Control))
			{
				_findToolStripTextBox.Focus();
				return true;
			}

			return base.ProcessCmdKey(ref msg, keyData);
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
					"Confirm Exit",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button2);

				if (confirmExit == DialogResult.No)
					e.Cancel = true;

				break;
			}

			if (!e.Cancel)
			{
				_userPreferences.FindToolbarTop = _toolStripContainer.TopToolStripPanel.Contains(_findToolStrip);
				_userPreferences.WorkItemToolbarTop = _toolStripContainer.TopToolStripPanel.Contains(_witToolStrip);

				if (WindowState != FormWindowState.Minimized)
				{
					_userPreferences.WindowLocation = Location;
					_userPreferences.WindowSize = WindowState == FormWindowState.Maximized ? new Size(-1, -1) : Size;
				}

				_userPreferences.SaveToRegistry();
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
					"Confirm Close",
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
			if (_testTabControl.TabCount < 1 || CurrentGridView == null)
				return;

			_testStateToolStripLabel.Text = "Saving...";
			_toolStripContainer.Enabled = false;

			CurrentGridView.EndEdit();
			UseWaitCursor = true;

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
					// do not save empty final step
					if (stepNumber == simpleSteps.Count - 1 &&
						String.IsNullOrEmpty(step.Title) &&
						String.IsNullOrEmpty(step.ExpectedResult))
					{
						break;
					}

					if (testCase.Actions.Count <= stepNumber)
					{
						testCase.Actions.Add(testCase.CreateTestStep());
					}

					// directly apply to existing ITestSteps
					if (testCase.Actions[stepNumber] is ITestStep)
					{
						((ITestStep)testCase.Actions[stepNumber]).Title = new ParameterizedString(step.Title);
						((ITestStep)testCase.Actions[stepNumber]).ExpectedResult = new ParameterizedString(step.ExpectedResult);
						((ITestStep)testCase.Actions[stepNumber]).TestStepType = String.IsNullOrWhiteSpace(step.ExpectedResult)? TestStepType.ActionStep : TestStepType.ValidateStep);
					}
					// current action is not ITestStep: if user-entered data is a step
					else if (step.IsTestStep())
					{
						// insert a new action at this point
						var newAction = testCase.CreateTestStep();
						newAction.Title = new ParameterizedString(step.Title);
						newAction.ExpectedResult = new ParameterizedString(step.ExpectedResult);
						newAction.TestStepType = String.IsNullOrWhiteSpace(step.ExpectedResult)? TestStepType.ActionStep : TestStepType.ValidateStep;

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
			UseWaitCursor = false;
			CurrentGridView.Focus();

			string result = e.Result as string;
			if (!String.IsNullOrWhiteSpace(result))
			{
				_testStateToolStripLabel.Text = "Error.";
				MessageBox.Show(result, "Error Saving Test Case");
			}
			else
			{
				_testStateToolStripLabel.Text = "Saved.";
			}
		}

		private void LoadButton_Click(object sender, EventArgs e)
		{
			if (_testProject == null)
				return;

			int workItemId;
			if (!Int32.TryParse(_workItemIdToolStripComboBox.Text, out workItemId))
				return;

			foreach (var testEditKvp in _tabTestInfoMap)
			{
				if (testEditKvp.Value.WorkItemId == workItemId)
				{
					_testTabControl.SelectedIndex = testEditKvp.Key;
					CurrentGridView.Focus();
					return;
				}
			}

			_testStateToolStripLabel.Text = "Loading...";
			_toolStripContainer.Enabled = false;
			UseWaitCursor = true;

			_loadTestBackgroundWorker.RunWorkerAsync(new TestEditInfo(workItemId));
		}
		
		private void LoadTestBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			var testInfo = e.Argument as TestEditInfo;

			try
			{
				testInfo.TestCase =
					_testProject.TestCases.Find(testInfo.WorkItemId) ??
					(ITestBase) _testProject.SharedSteps.Find(testInfo.WorkItemId);

				if (testInfo.TestCase == null)
					testInfo.Message = "Input test case ID not found.";
			}
			catch (Exception ex)
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

				MessageBox.Show(testInfo.Message, "Error Loading Test Case");
				UseWaitCursor = false;
				return;
			}

			SimpleSteps newSteps = TryCreateSimpleSteps(testInfo);
			if (newSteps == null)
			{
				UseWaitCursor = false;
				return;
			}

			testInfo.SimpleSteps = newSteps;
			
			var newDataGridView = CreateTestTabAndDataGridView(testInfo.TestCase.Id + " " + testInfo.TestCase.Title);
			if (newDataGridView == null)
			{
				UseWaitCursor = false;
				return;
			}

			try
			{
				testInfo.DataGridView = newDataGridView;

				newDataGridView.SuspendLayout();

				newDataGridView.DataSource = testInfo.SimpleSteps;

				newDataGridView.Columns[TITLE_COLUMN].SortMode = DataGridViewColumnSortMode.NotSortable;
				newDataGridView.Columns[EXPECTED_RESULT_COLUMN].SortMode = DataGridViewColumnSortMode.NotSortable;
				newDataGridView.Columns[DONE_COLUMN].SortMode = DataGridViewColumnSortMode.NotSortable;

				newDataGridView.Columns[TITLE_COLUMN].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
				newDataGridView.Columns[EXPECTED_RESULT_COLUMN].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
				newDataGridView.Columns[DONE_COLUMN].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
				newDataGridView.Columns[DONE_COLUMN].Width = 40;

				newDataGridView.ResumeLayout(true);

				_toolStripContainer.Enabled = true;
				UseWaitCursor = false;
				newDataGridView.Focus();

				int newTabIndex = _testTabControl.TabCount - 1;
				_tabTestInfoMap[newTabIndex] = testInfo;

				_testStateToolStripLabel.Text = "Loaded.";

				_userPreferences.AddWorkItemIdMru(testInfo.TestCase.Id);
				_userPreferences.SaveToRegistry();
				
				_workItemIdToolStripComboBox.Text = String.Empty;
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					"Could not display test case: " + ex.Message,
					"Error Displaying Test Case");

				_testTabControl.TabPages.RemoveAt(_testTabControl.TabCount - 1);
			}
			finally
			{
				UseWaitCursor = false;
			}
		}

		private void ChangeProjectButton_Click(object sender, EventArgs e)
		{
			var newServerSettings = UserPreferences.LoadProjectSelectionFromUser();
			if (newServerSettings == null)
				return;

			_userPreferences = newServerSettings;

			_workItemIdToolStripComboBox.ComboBox.DataSource = _userPreferences.WorkItemIdMru;

			if (!String.IsNullOrEmpty(_userPreferences.TestProject))
				ApplyUserServerPreferences();

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
			if (CurrentGridView == null)
				return;

			InsertStep(CurrentGridView.CurrentRow, InsertLocation.Above);
		}

		private void InsertBelowButton_Click(object sender, EventArgs e)
		{
			if (CurrentGridView == null)
				return;

			InsertStep(CurrentGridView.CurrentRow, InsertLocation.Below);
		}

		private void DeleteButton_Click(object sender, EventArgs e)
		{
			DeleteCurrentRow();
		}

		private void FindButton_Click(object sender, EventArgs e)
		{
			if (CurrentGridView == null)
				return;

			var searcher = new DataGridViewSearcher(CurrentGridView);

			bool notFound = searcher.Search(_findToolStripTextBox.Text) == DataGridViewSearcher.Result.NotMatched;
			
			if (notFound)
				MessageBox.Show("\"" + _findToolStripTextBox.Text + "\" was not found.", "Not Found");
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

			// deselect pass/fail column before copying
			foreach (DataGridViewCell cell in CurrentGridView.SelectedCells)
			{
				if (cell.ColumnIndex > EXPECTED_RESULT_COLUMN)
					cell.Selected = false;
			}

			DataObject clipboardData = CurrentGridView.GetClipboardContent();
			if (clipboardData != null)
				Clipboard.SetDataObject(clipboardData);
		}

		private void TestGridContext_Paste_Click(object sender, EventArgs e)
		{
			PasteClipboard();
		}

		private void TestGridContext_Delete_Click(object sender, EventArgs e)
		{
			if (CurrentGridView.SelectedRows.Count == 0)
			{
				DeleteCurrentRow();
			}
			else
			{
				DeleteRows(
					CurrentGridView.SelectedRows
					.Cast<DataGridViewRow>()
					.Select(row => row.Index).ToList());
			}
		}

		private void TestGridContext_InsertAbove_Click(object sender, EventArgs e)
		{
			var insertRow = CurrentGridView.SelectedRows.Count == 0
				? CurrentGridView.CurrentRow
				: CurrentGridView.SelectedRows.Cast<DataGridViewRow>().OrderBy(r => r.Index).First();

			InsertStep(insertRow, InsertLocation.Above);
		}

		private void TestGrid_InsertBelow_Click(object sender, EventArgs e)
		{
			var insertRow = CurrentGridView.SelectedRows.Count == 0
				? CurrentGridView.CurrentRow
				: CurrentGridView.SelectedRows.Cast<DataGridViewRow>().OrderBy(r => r.Index).Last();

			InsertStep(insertRow, InsertLocation.Below);
		}

		private void StringGeneratorButton_Click(object sender, EventArgs e)
		{
			using (var strGenForm = new StringGeneratorForm())
			{
				if (CurrentGridView != null && CurrentGridView.CurrentCell != null && CurrentGridView.CurrentRow != null)
				{
					strGenForm.StartPosition = FormStartPosition.Manual;

					var currentCellRect = CurrentGridView.GetCellDisplayRectangle(
						CurrentGridView.CurrentCell.ColumnIndex,
						CurrentGridView.CurrentRow.Index,
						false);

					strGenForm.Location = CurrentGridView.PointToScreen(
						new Point(currentCellRect.Left + 4, currentCellRect.Bottom + 4));
				}

				strGenForm.ShowDialog(this);
			}
		}

		#endregion Event handlers

		private void InsertStep(DataGridViewRow insertRow, InsertLocation location)
		{
			if (CurrentGridView == null)
				return;

			int insertRowIndex = insertRow == null? -1 : insertRow.Index;

			if (location == InsertLocation.Below && insertRowIndex != -1)
				insertRowIndex++;

			int currentCol = insertRowIndex == -1? 0 : CurrentGridView.CurrentCell.ColumnIndex;

			if (insertRowIndex != -1)
			{
				CurrentSimpleSteps.Insert(insertRowIndex, new SimpleStep());
			}
			else
			{
				CurrentSimpleSteps.AddNew();
				insertRowIndex = 0;
			}

			CurrentGridView.CurrentCell = CurrentGridView[currentCol, insertRowIndex];
			CurrentGridView.Focus();
		}

		private void ReplaceInCells(bool selectedOnly)
		{
			if (CurrentGridView == null)
				return;

			var searcher = new DataGridViewSearcher(CurrentGridView);
			bool notReplaced = searcher.Replace(
				_findToolStripTextBox.Text, 
				_replaceToolStripTextBox.Text,
				selectedOnly) == DataGridViewSearcher.Result.NotMatched;

			if (notReplaced)
				MessageBox.Show("\"" + _findToolStripTextBox.Text + "\" was not found.", "No Replacements Performed");
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
					"Error Loading Test Case");

				return null;
			}
		}

		private DataGridView CreateTestTabAndDataGridView(string tabTitle)
		{
			string newTabKey = _testTabControl.TabCount.ToString();

			var newDataGridView = new NumberedDataGridView
			{
				Dock = DockStyle.Fill,
				AllowUserToAddRows = false,
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
					bool addedRow = false;
					if (rowIndex == CurrentGridView.RowCount)
					{
						CurrentSimpleSteps.AddNew();
						addedRow = true;
					}

					if (rowIndex >= CurrentGridView.RowCount || clipboardLine.Length <= 0)
						break;
					
					string[] clipboardLineTokens = clipboardLine.Split('\t');
					for (int tokenIndex = 0; tokenIndex < clipboardLineTokens.Length; ++tokenIndex)
					{
						// do not paste into pass/fail column
						if (colIndex + tokenIndex > EXPECTED_RESULT_COLUMN)
							break;
						
						DataGridViewCell targetCell = CurrentGridView[colIndex + tokenIndex, rowIndex];

						// done pasting when we reach an unselected cell, except when we added the row
						if (!targetCell.Selected && !addedRow)
							return;

						if (targetCell.Value == null || 
							(targetCell.Value.ToString() != clipboardLineTokens[tokenIndex]))
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
				MessageBox.Show("The pasted data is in the wrong format for the target cell(s)", "Cannot Paste");
			}
		}

		private void DeleteCurrentRow()
		{
			if (CurrentGridView == null || CurrentGridView.CurrentRow == null)
				return;

			DeleteRows(new List<int> { CurrentGridView.CurrentRow.Index });
		}

		private void DeleteRows(List<int> rowIndexes)
		{
			if (CurrentGridView == null)
				return;

			int currentCol = CurrentGridView.CurrentCell == null? 0 : CurrentGridView.CurrentCell.ColumnIndex;
			int focusRow = Math.Min(CurrentGridView.RowCount - 2, rowIndexes.Min());

			foreach (int removeIndex in rowIndexes.OrderByDescending(r => r))
				CurrentSimpleSteps.RemoveAt(removeIndex);

			if (CurrentGridView.RowCount == 0)
				return;

			CurrentGridView.CurrentCell = CurrentGridView[currentCol, focusRow];
			CurrentGridView.Focus();
		}
	}
}
