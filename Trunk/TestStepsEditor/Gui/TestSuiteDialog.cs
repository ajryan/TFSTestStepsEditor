using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Microsoft.TeamFoundation.TestManagement.Client;
using TestStepsEditor.Tfs;

namespace TestStepsEditor.Gui
{
	public partial class TestSuiteDialog : Form
	{
		private static List<SimpleTestPoint> _RelatedTestPoints;

		private readonly BackgroundWorker _loadBackgroundWorker;

		private readonly ITestManagementTeamProject _testProject;
		private readonly TestEditInfo _testEditInfo;

		public SimpleTestPoint SelectedTestPoint
		{
			get { return (SimpleTestPoint) _suiteListBox.SelectedItem; }
		}

		public TestSuiteDialog(ITestManagementTeamProject testProject, TestEditInfo testEditInfo)
		{
			_loadBackgroundWorker = new BackgroundWorker();

			_testProject = testProject;
			_testEditInfo = testEditInfo;
	
			InitializeComponent();

			this.Load += (e, o) => LoadRelatedTestPoints();
		}

		private void LoadRelatedTestPoints()
		{
			_loadBackgroundWorker.DoWork += LoadBackgroundWorker_DoWork;
			_loadBackgroundWorker.RunWorkerCompleted += LoadBackgroundWorker_RunWorkerCompleted;

			UseWaitCursor = true;
			_loadBackgroundWorker.RunWorkerAsync();
		}

		private void LoadBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				if (_RelatedTestPoints != null)
				{
					e.Result = _RelatedTestPoints;
					return;
				}

				var relatedTestPoints = new List<ITestPoint>();

				foreach (var plan in _testProject.TestPlans.Query("SELECT * FROM TestPlan"))
				{
					string query = String.Format("SELECT * FROM TestPoint WHERE TestCaseId = {0}", _testEditInfo.TestCase.Id);
					relatedTestPoints.AddRange(plan.QueryTestPoints(query));
				}

				var displayTestPoints = new List<SimpleTestPoint>();
				var allSuites = _testProject.TestSuites.Query("SELECT * FROM TestSuite");

				foreach (var testPoint in relatedTestPoints)
				{
					var suiteId = testPoint.SuiteId;
					var suite = allSuites.Single(s => s.Id == suiteId);
					displayTestPoints.Add(
						new SimpleTestPoint
						{
							Title = suite.Plan.Name + " - " + suite.Title,
							TestPoint = testPoint
						});
				}

				_RelatedTestPoints = displayTestPoints;
				e.Result = displayTestPoints;
			}
			catch (Exception ex)
			{
				e.Result = ex.Message;
			}
		}

		private void LoadBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			UseWaitCursor = false;
			if (e.Result is string)
			{
				MessageBox.Show(
					"Could not load test suites for the current test: " + (string) e.Result,
					"Error Loading Suites");
			}
			else
			{
				var relatedTestPoints = e.Result as List<SimpleTestPoint>;
				if (relatedTestPoints == null || relatedTestPoints.Count == 0)
				{
					MessageBox.Show(
						"The current test case does not exist in any test suites for the current test project. Cannot publish.",
						"Cannot Publish");

					return;
				}

				_suiteListBox.DataSource = relatedTestPoints;
				_suiteListBox.DisplayMember = "Title";
				_suiteListBox.ValueMember = "TestPoint";
			}
		}

		private void CancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void OkButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void RefreshButton_Click(object sender, EventArgs e)
		{
			UseWaitCursor = true;
			_RelatedTestPoints = null;
			_loadBackgroundWorker.RunWorkerAsync();
		}
	}
}
