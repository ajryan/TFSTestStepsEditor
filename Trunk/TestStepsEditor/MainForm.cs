using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.TestManagement.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TestStepsEditor
{
    public partial class MainForm : Form
    {
        private TfsTeamProjectCollection _tfs;
        private ITestManagementTeamProject _testproject;
        private ITestBase _testCase;
        private BindingList<SimpleStep> _simpleSteps;

        public MainForm()
        {
            InitializeComponent();

        	try
        	{
				ServerSettings.Load(out _tfs, out _testproject);
				if (_testproject != null)
					_projectLabel.Text = _testproject.WitProject.Name;
        	}
        	catch (Exception ex)
        	{
        		MessageBox.Show("Could not load previous TFS connection.\n\nError:\n" + ex.Message);
        		_projectLabel.Text = "[Not connected]";
        	}
			
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (_testCase == null || _simpleSteps == null || _simpleSteps.Count == 0)
                return;

            try
            {
                Enabled = false;

                int stepNumber = 0;
                foreach (SimpleStep step in _simpleSteps)
                {
                    if (_testCase.Actions.Count <= stepNumber)
                    {
                        _testCase.Actions.Add(_testCase.CreateTestStep());
                    }

					// directly apply to existing ITestSteps
					if (_testCase.Actions[stepNumber] is ITestStep)
					{
						((ITestStep) _testCase.Actions[stepNumber]).Title = new ParameterizedString(step.Title);
						((ITestStep) _testCase.Actions[stepNumber]).ExpectedResult = new ParameterizedString(step.ExpectedResult);
					}
					// current action is not ITestStep: if user-entered data is a step
					else if (step.IsTestStep())
					{
						// insert a new action at this point
						var newAction = _testCase.CreateTestStep();
						newAction.Title = new ParameterizedString(step.Title);
						newAction.ExpectedResult = new ParameterizedString(step.ExpectedResult);

						_testCase.Actions.Insert(stepNumber, newAction);
					}

                	 stepNumber++;
                }

                while (stepNumber < _testCase.Actions.Count)
                    _testCase.Actions.RemoveAt(stepNumber);

                _testCase.Save();
            }
            finally
            {
                Enabled = true;
            }

            MessageBox.Show("Save successful.");
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            int workItemId;
            if (!Int32.TryParse(_workItemTextBox.Text, out workItemId))
                return;

            try
            {
                _testCase = _testproject.TestCases.Find(workItemId) ??
                            (ITestBase) _testproject.SharedSteps.Find(workItemId);
            }
            catch (DeniedOrNotExistException ex)
            {
                MessageBox.Show("Could not load test case: " + ex.Message, "Test Case Load Error");
                return;
            }
            if (_testCase == null)
            {
                MessageBox.Show("Input test case ID not found.", "Test Case Load Error");
                return;
            }

            _testStepsGridView.SuspendLayout();
            Enabled = false;

            _simpleSteps = new BindingList<SimpleStep>();
            foreach (ITestAction action in _testCase.Actions)
            {
				if (action is ITestStep)
				{
					var testStep = action as ITestStep;
					_simpleSteps.Add(new SimpleStep(testStep.Title.ToString(), testStep.ExpectedResult.ToString()));
				}
				else if (action is ISharedStepReference)
				{
					var sharedStep = action as ISharedStepReference;
					_simpleSteps.Add(new SimpleStep("Shared step ID " + sharedStep.SharedStepId, String.Empty, false));
				}
				else
				{
					_simpleSteps.Add(new SimpleStep("Unknown action " + action.Id, String.Empty, false));
				}
            }

            _testStepsGridView.DataSource = _simpleSteps;
            Enabled = true;
            _testStepsGridView.ResumeLayout(true);
        }

        private void ChangeProjectButton_Click(object sender, EventArgs e)
        {
            ServerSettings.LoadProjectSelectionFromUser(out _tfs, out _testproject);
            if (_testproject != null)
                _projectLabel.Text = _testproject.WitProject.Name;

            // clear current test info
            if (_simpleSteps != null)
                _simpleSteps.Clear();
            _testCase = null;
        }

        private void WorkItemTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                LoadButton_Click(null, null);
        }

        private void TestStepsGridView_CurrentCellChanged(object sender, EventArgs e)
        {
        	_rowLabel.Text = _testStepsGridView.CurrentRow == null
				? "Row:"
				: String.Format("Row:{0}", _testStepsGridView.CurrentRow.Index + 1);
        }

    	private void InsertButton_Click(object sender, EventArgs e)
        {
            if (_testStepsGridView.CurrentRow != null)
            {
                _simpleSteps.Insert(_testStepsGridView.CurrentRow.Index, new SimpleStep());
                _testStepsGridView.Focus();
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (_testStepsGridView.CurrentRow != null)
            {
                _simpleSteps.RemoveAt(_testStepsGridView.CurrentRow.Index);
                _testStepsGridView.Focus();
            }
        }

    }
}
