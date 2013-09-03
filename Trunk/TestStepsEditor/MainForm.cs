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
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using NLog;
using TestStepsEditor.Gui;
using TestStepsEditor.Preferences;
using TestStepsEditor.Tfs;

namespace TestStepsEditor
{
	public partial class MainForm : Form
	{
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();
		private TfsTeamProjectCollection _tfs;
		private ITestManagementTeamProject _testProject;
		private readonly UserPreferences _userPreferences = new UserPreferences();
		private readonly Dictionary<int, TestEditInfo> _tabTestInfoMap = new Dictionary<int, TestEditInfo>();
		private QueryAndTestCasePicker _queryAndTestCasePicker;

		public MainForm()
		{
			_logger.Debug("Initialize main form");

			InitializeComponent();
			_toolStripContainer.Enabled = false;

			_userPreferences.Load();

			ApplyUserDisplayPreferences();

			Shown += (
				(o, e) =>
				{
					try
					{
						_testStateToolStripLabel.Text = "Connecting...";
						ApplyUserServerPreferences();
						_testStateToolStripLabel.Text = "Connected.";

						_workItemIdToolStripComboBox.Focus();
						_toolStripContainer.Enabled = true;
					}
					catch (Exception ex)
					{
						_toolStripContainer.Enabled = true;

						MessageBox.Show("Could not load previous TFS connection.\n\nError:\n" + ex.Message, "Error Restoring Previous Connection");
						_testStateToolStripLabel.Text = _changeProjectToolStripButton.Text = "(not connected)";
					}
				});
		}
		
		private void ApplyUserServerPreferences()
		{
			if (_userPreferences.TfsUri.Value == null ||
				String.IsNullOrEmpty(_userPreferences.TestProject))
			{
				return;
			}

			_logger.Info("Apply server preferences. TFS URI: {0}; TestProject: {1}",
				_userPreferences.TfsUri.Value, _userPreferences.TestProject);

			_tfs = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(_userPreferences.TfsUri);
			_tfs.Connect(ConnectOptions.IncludeServices);
			_testProject = _tfs.GetService<ITestManagementService>().GetTeamProject(_userPreferences.TestProject);

			_changeProjectToolStripButton.Text = _userPreferences.TestProject;
		}

		private void ApplyUserDisplayPreferences()
		{
			_logger.Info("Apply display preferences.");

			if (_workItemIdToolStripComboBox.ComboBox != null)
				_workItemIdToolStripComboBox.ComboBox.DataSource = _userPreferences.WorkItemIdMru.Value;

			// if the saved location + size is not fully on-screen, do not override defaults
			bool useSavedLocation = false;
			if (!_userPreferences.WindowSize.Value.IsEmpty && !_userPreferences.WindowLocation.Value.IsEmpty)
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

			if (_userPreferences.WorkItemToolbarTop)
			{
				if (_toolStripContainer.BottomToolStripPanel.Contains(_witToolStrip))
				{
					_toolStripContainer.BottomToolStripPanel.Controls.Remove(_witToolStrip);
					_toolStripContainer.TopToolStripPanel.Controls.Add(_witToolStrip);
				}
			}

			if (_userPreferences.FindToolbarTop)
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
							"TFS Test Steps Editor\r\nVersion: " + version,
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
			if (e.Cancel)
			{
				// SelectQueryAndTestCases window sets e.Cancel to false to stop the child/dependent 
				// window from actually closing and instead just hides it, so for the main form close
				// set it back to default value so the main form can close if we want it to close.
				e.Cancel = false;
			}

			if (_tabTestInfoMap.Values.Any(testInfo => testInfo.SimpleSteps.Dirty))
			{
				_logger.Info("Close main form: dirty test cases.");

				var confirmExit = MessageBox.Show(
					"One or more open tests has not been saved. Are you sure you want to exit?",
					"Confirm Exit",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button2);

				if (confirmExit == DialogResult.No)
				{
					e.Cancel = true;
					return;
				}
			}

			_logger.Info("Close main form.");

			_userPreferences.FindToolbarTop.Value = _toolStripContainer.TopToolStripPanel.Contains(_findToolStrip);
			_userPreferences.WorkItemToolbarTop.Value = _toolStripContainer.TopToolStripPanel.Contains(_witToolStrip);

			if (WindowState != FormWindowState.Minimized)
			{
				_userPreferences.WindowLocation.Value = Location;
				_userPreferences.WindowSize.Value = WindowState == FormWindowState.Maximized ? new Size(-1, -1) : Size;
			}

			_userPreferences.Save();
		}

		private void CloseCurentButton_Click(object sender, EventArgs e)
		{
			this.CloseCurrentTab();
		}

		private void SaveButton_Click(object sender, EventArgs e)
		{
			if (_testTabControl.TabCount < 1)
				return;

			_logger.Info("Save current test.");

			_testStateToolStripLabel.Text = "Saving...";
			_toolStripContainer.Enabled = false;

			CurrentGridView.EndEdit();
			UseWaitCursor = true;

			_saveTestBackgroundWorker.RunWorkerAsync(
				_tabTestInfoMap[_testTabControl.SelectedIndex]);
		}

		private void SaveTestBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			var saveTestInfo = (TestEditInfo) e.Argument;
			try
			{
				saveTestInfo.Save();
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
			_logger.Info("Save current test complete.");

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
			int workingId;
			if (!int.TryParse(this._workItemIdToolStripComboBox.Text, out workingId))
			{
				return;
			}

			this.GetTestCaseData(workingId);
		}

		private void LoadTestBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			var testInfo = (TestEditInfo) e.Argument;

			try
			{
				testInfo.TestCase =
					_testProject.TestCases.Find(testInfo.WorkItemId) ??
					(ITestBase) _testProject.SharedSteps.Find(testInfo.WorkItemId);

				if (testInfo.TestCase == null)
				{
					_logger.Info("Could not find test case");
					testInfo.Message = "Input test case ID not found.";
				}
				else if (testInfo.TestCase.WorkItem.Project.Name != _testProject.WitProject.Name)
				{
					_logger.Info("Wrong project.");

					testInfo.Message = String.Format(
						"Test case is from project {0}. Current project is {1}.", 
						testInfo.TestCase.WorkItem.Project.Name,
						_testProject.WitProject.Name);

					testInfo.TestCase = null;
				}
			}
			catch (Exception ex)
			{
				_logger.InfoException("Error loading test case.", ex);

				var deniedException = ex as DeniedOrNotExistException;

				string detailedMessage = deniedException != null ? deniedException.Message : ex.ToString();
				testInfo.Message = "Could not load test case:\r\n" + detailedMessage;
			}

			e.Result = testInfo;
		}

		private void LoadTestBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			_logger.Info("Load test completed.");

			var testInfo = (TestEditInfo) e.Result;
			if (!String.IsNullOrWhiteSpace(testInfo.Message))
			{
				UseWaitCursor = false;
				_toolStripContainer.Enabled = true;
				_testStateToolStripLabel.Text = "Error.";

				MessageBox.Show(testInfo.Message, "Error Loading Test Case");
				return;
			}

			SimpleSteps newSteps = TryCreateSimpleSteps(testInfo);
			if (newSteps == null)
				return;

			testInfo.SimpleSteps = newSteps;
			
			var newEditControl = CreateTestTabAndEditControl(testInfo);
			
			try
			{
				_logger.Info("New test edit control.");

				testInfo.TestEditControl = newEditControl;

				newEditControl.TestDataGridView.LoadSteps(_enableResultsModeMenuItem.Checked);
				newEditControl.LoadCurrentResults();

				_toolStripContainer.Enabled = true;
				UseWaitCursor = false;
				newEditControl.Focus();

				int newTabIndex = _testTabControl.TabCount - 1;
				_tabTestInfoMap[newTabIndex] = testInfo;

				_testStateToolStripLabel.Text = "Loaded.";

				_userPreferences.AddWorkItemIdMru(testInfo.TestCase.Id);
				_userPreferences.Save();

				EnableResultsModeToolStripMenuItem_CheckedChanged(null, null);
				_workItemIdToolStripComboBox.Text = String.Empty;

				// if the test case has parameters, show parameters dialog
				if (testInfo.TestCase.TestParameters.Any() && testInfo.TestCase is ITestCase)
				{
					var testCase = (ITestCase)(testInfo.TestCase);
					var paramsDialog = new ParametersDialog(testCase);
					paramsDialog.Show();
				}
			}
			catch (Exception ex)
			{
				_toolStripContainer.Enabled = true;
				UseWaitCursor = false;

				MessageBox.Show(
					"Error loading test case: " + ex.Message,
					"Error Loading Test Case");

				_testTabControl.TabPages.RemoveAt(_testTabControl.TabCount - 1);
			}
		}

		private void ChangeProjectButton_Click(object sender, EventArgs e)
		{
			_logger.Info("Change project.");

			_userPreferences.LoadProjectSelectionFromUser();
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

			CurrentGridView.InsertStep(CurrentGridView.CurrentRow, InsertLocation.Above);
		}

		private void InsertBelowButton_Click(object sender, EventArgs e)
		{
			if (CurrentGridView == null)
				return;

			CurrentGridView.InsertStep(CurrentGridView.CurrentRow, InsertLocation.Below);
		}

		private void DeleteButton_Click(object sender, EventArgs e)
		{
			if (CurrentGridView == null)
				return;

			CurrentGridView.DeleteCurrentRow();
		}

		private void FindButton_Click(object sender, EventArgs e)
		{
			_logger.Debug("Find " + _findToolStripTextBox.Text);

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

			CurrentGridView.CopyToClipboard();
		}

		private void TestGridContext_Paste_Click(object sender, EventArgs e)
		{
			PasteClipboard();
		}

		private void TestGridContext_Delete_Click(object sender, EventArgs e)
		{
			if (CurrentGridView == null)
				return;

			if (CurrentGridView.SelectedRows.Count == 0)
			{
				CurrentGridView.DeleteCurrentRow();
			}
			else
			{
				CurrentGridView.DeleteRows(
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

			CurrentGridView.InsertStep(insertRow, InsertLocation.Above);
		}

		private void TestGrid_InsertBelow_Click(object sender, EventArgs e)
		{
			var insertRow = CurrentGridView.SelectedRows.Count == 0
				? CurrentGridView.CurrentRow
				: CurrentGridView.SelectedRows.Cast<DataGridViewRow>().OrderBy(r => r.Index).Last();

			CurrentGridView.InsertStep(insertRow, InsertLocation.Below);
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

		private void PublishButton_Click(object sender, EventArgs e)
		{
			if (CurrentTestEditInfo == null)
				return;

			if (CurrentSimpleSteps.Dirty)
			{
				MessageBox.Show("Please save the current test case before publishing.", "Cannot Publish");
				return;
			}

			_logger.Info("Publish test case " + CurrentTestEditInfo.WorkItemId);

			var suiteDialog = new TestSuiteDialog(_testProject, CurrentTestEditInfo);
			var suiteSelectResult = suiteDialog.ShowDialog(this);
			if (suiteSelectResult == DialogResult.Cancel || suiteDialog.SelectedTestPoint == null)
			{
				_logger.Info("Publish cancelled.");
				return;
			}

			var testRun = new TestRun(
				CurrentTestEditInfo,
				suiteDialog.SelectedTestPoint.TestPoint,
				_tfs.AuthorizedIdentity);

			_testStateToolStripLabel.Text = "Publishing...";
			_toolStripContainer.Enabled = false;
			UseWaitCursor = true;
			
			_publishTestBackgroundWorker.RunWorkerAsync(testRun);
		}

		private void PublishTestBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			var testRun = (TestRun) e.Argument;
			try
			{
				testRun.Publish();
				e.Result = null;
			}
			catch (Exception ex)
			{
				var reporter = new ExceptionReporter(ex);
				reporter.ReportException();

				e.Result = "Could not publish test case: " + ex.Message;
			}
		}

		private void PublishTestBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			_logger.Info("Publish complete.");

			_testStateToolStripLabel.Text = "Publish complete.";
			_toolStripContainer.Enabled = true;
			UseWaitCursor = false;

			var errorMessage = e.Result as string;
			if (errorMessage == null)
				return;

			MessageBox.Show(
				"Error publishing test case: " + errorMessage,
				"Error Publishing");
		}

		private void SaveCurrentResultsButton_Click(object sender, EventArgs e)
		{
			if (CurrentTestEditControl == null)
				return;

			_logger.Info("Save results.");

			try
			{
				CurrentTestEditControl.SaveCurrentResultsToZip();
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					"Error saving current results to disk: " + ex.Message,
					"Error Saving Results");
			}
		}

		private void LoadResultsButton_Click(object sender, EventArgs e)
		{
			if (CurrentTestEditControl == null)
				return;

			_logger.Info("Load results.");
			CurrentTestEditControl.LoadResultsFromZip();
		}

		private void ClearResultsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (CurrentTestEditControl == null)
				return;

			_logger.Info("Reset results.");
			CurrentTestEditControl.ResetResults();
		}

		private void EnableResultsModeToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
		{
			if (CurrentTestEditControl == null)
				return;

			_logger.Info("Switch results mode: " + _enableResultsModeMenuItem.Checked);

			if (_enableResultsModeMenuItem.Checked)
			{
				CurrentTestEditControl.ShowResultsPanel();
				CurrentGridView.ShowOutcomeColumn();

				_publishToolStripMenuItem.Enabled = true;
				_saveCurrentResultsToolStripMenuItem.Enabled = true;
				_loadResultsToolStripMenuItem.Enabled = true;
				_clearResultsToolStripMenuItem.Enabled = true;
			}
			else
			{
				CurrentTestEditControl.HideResultsPanel();
				CurrentGridView.HideOutcomeColumn();

				_publishToolStripMenuItem.Enabled = false;
				_saveCurrentResultsToolStripMenuItem.Enabled = false;
				_loadResultsToolStripMenuItem.Enabled = false;
				_clearResultsToolStripMenuItem.Enabled = false;
			}
		}

		private void SelectQueryToolStripButton_Click(object sender, EventArgs e)
		{
			if (this._testProject == null)
			{
				MessageBox.Show("Please confirm you have connectivity to the TFS server and select an available test project, then try again.", "No Test Project Available");
				return;
			}

			if (this._queryAndTestCasePicker == null || this._queryAndTestCasePicker.Project != this._testProject)
			{
				this._queryAndTestCasePicker = new QueryAndTestCasePicker { Project = this._testProject, Owner = this };
			}

			this._queryAndTestCasePicker.StartPosition = FormStartPosition.Manual;
			// set the location of the window to be on top of the test steps editor window
			this._queryAndTestCasePicker.Location = new Point(this.Location.X + 25, this.Location.Y + 25);
			// subscribe to the event that fires when the test case number is set from the test case picker list
			this._queryAndTestCasePicker.TestCaseNumberSet += this.QueryAndTestCasePicker_TestCaseNumberSet;
			// show it as a modeless window
			this._queryAndTestCasePicker.Show();
		}

		private void QueryAndTestCasePicker_TestCaseNumberSet(object sender, EventArgs e)
		{
			this.GetTestCaseData(this._queryAndTestCasePicker.TestCaseNumber);
		}

		#endregion Event handlers

		private void GetTestCaseData(int testCaseNumber)
		{
			if (this._testProject == null)
			{
				MessageBox.Show("Please connect to a Test Project first.", "No Connection");
				return;
			}

			foreach (var testEditKvp in this._tabTestInfoMap)
			{
				if (testEditKvp.Value.WorkItemId == testCaseNumber)
				{
					this._testTabControl.SelectedIndex = testEditKvp.Key;
					this.CurrentGridView.Focus();
					return;
				}
			}

			this._logger.Info("Load test " + testCaseNumber);

			this._testStateToolStripLabel.Text = "Loading...";
			this._toolStripContainer.Enabled = false;
			this.UseWaitCursor = true;

			try
			{
				this._loadTestBackgroundWorker.RunWorkerAsync(new TestEditInfo(testCaseNumber));
			}
			catch (InvalidOperationException)
			{
				// loadTestBackgroundWorker can only run one instance at a time 
				// so consume the exception and give a message to make it a 
				// graceful experience
				MessageBox.Show("Please wait until the previous test case load completes and try again.");
			}
		}

		private void CloseCurrentTab()
		{
			if (this._testTabControl.SelectedTab == null)
			{
				return;
			}

			_tabTestInfoMap.Values.Any(testInfo => testInfo.SimpleSteps.Dirty);
			if (this.CurrentSimpleSteps.Dirty)
			{
				this._logger.Info("Discard current test Changes: dirty.");

				var sureClose = MessageBox.Show(
					"The test case steps have been modified. Are you sure?",
					"Confirm Discard Changes",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button2);

				if (sureClose == DialogResult.No)
				{
					return;
				}
			}

			this._logger.Info("Close current test.");

			int removedIndex = this._testTabControl.SelectedIndex;
			this._testTabControl.TabPages.RemoveAt(removedIndex);

			this._tabTestInfoMap.Remove(removedIndex);

			for (int tabIndex = removedIndex + 1; tabIndex <= this._testTabControl.TabCount; tabIndex++)
			{
				var movedTestInfo = this._tabTestInfoMap[tabIndex];
				this._tabTestInfoMap.Remove(tabIndex);
				this._tabTestInfoMap[tabIndex - 1] = movedTestInfo;
			}
		}

		private void ReplaceInCells(bool selectedOnly)
		{
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
				return SimpleSteps.Create(testInfo);
			}
			catch (Exception ex)
			{
				var exReporter = new ExceptionReporter(ex);
				exReporter.ReportException();

				MessageBox.Show(
					"Could not load steps from test case: " + ex.Message,
					"Error Loading Test Case");

				return null;
			}
		}

		private TestEditUserControl CreateTestTabAndEditControl(TestEditInfo testEditInfo)
		{
			var newEditControl = TestEditUserControl.Create(testEditInfo, _testGridContextMenu, _enableResultsModeMenuItem.Checked);
			newEditControl.Dock = DockStyle.Fill;

			string newTabKey = _testTabControl.TabCount.ToString();
			string tabTitle = testEditInfo.TestCase.Id + " " + testEditInfo.TestCase.Title;

			_testTabControl.TabPages.Add(newTabKey, tabTitle);
			_testTabControl.TabPages[newTabKey].Controls.Add(newEditControl);
			_testTabControl.SelectedIndex = _testTabControl.TabCount - 1;

			return newEditControl;
		}

		private void PasteClipboard()
		{
			if (CurrentGridView == null)
				return;

			try
			{
				CurrentGridView.PasteClipboard();
			}
			catch (Exception)
			{
				MessageBox.Show("The pasted data is in the wrong format for the target cell(s)", "Cannot Paste");
			}
		}

		private TestEditUserControl CurrentTestEditControl
		{
			get
			{
				if (_testTabControl.TabPages.Count < 1 || _tabTestInfoMap.Count < 1)
					return null;

				return _tabTestInfoMap[_testTabControl.SelectedIndex].TestEditControl;
			}
		}

		private TestStepsDataGridView CurrentGridView
		{
			get
			{
				if (_testTabControl.TabPages.Count < 1 || _tabTestInfoMap.Count < 1)
					return null;

				return _tabTestInfoMap[_testTabControl.SelectedIndex].TestEditControl.TestDataGridView;
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

		private TestEditInfo CurrentTestEditInfo
		{
			get
			{
				if (_testTabControl.TabPages.Count < 1 || _tabTestInfoMap.Count < 1)
					return null;

				return _tabTestInfoMap[_testTabControl.SelectedIndex];
			}
		}
	}
}
