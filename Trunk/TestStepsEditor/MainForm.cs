using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.TestManagement.Client;
using Microsoft.Win32;
using System.Diagnostics;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TestStepsEditor
{
    public partial class MainForm : Form
    {
        private TfsTeamProjectCollection _tfs;
        private ITestManagementTeamProject _testproject;
        private ITestCase _testCase;
        private BindingList<SimpleStep> _simpleSteps;

        public MainForm()
        {
            InitializeComponent();

            ServerSettings.Load(out _tfs, out _testproject);
            if (_testproject != null)
                _projectLabel.Text = _testproject.WitProject.Name;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (_testCase == null || _simpleSteps == null || _simpleSteps.Count == 0)
                return;

            try
            {
                this.Enabled = false;

                int stepNumber = 0;
                foreach (SimpleStep step in _simpleSteps)
                {
                    if (_testCase.Actions.Count <= stepNumber)
                    {
                        _testCase.Actions.Add(_testCase.CreateTestStep());
                    }

                    (_testCase.Actions[stepNumber] as ITestStep).Title = new ParameterizedString(step.Title);
                    (_testCase.Actions[stepNumber] as ITestStep).ExpectedResult = new ParameterizedString(step.ExpectedResult);

                    stepNumber++;
                }

                while (stepNumber < _testCase.Actions.Count)
                    _testCase.Actions.RemoveAt(stepNumber);

                _testCase.Save();
            }
            finally
            {
                this.Enabled = true;
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
                _testCase = _testproject.TestCases.Find(workItemId);
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
            this.Enabled = false;

            _simpleSteps = new BindingList<SimpleStep>();//_testCase.Actions.Count);
            foreach (ITestAction action in _testCase.Actions)
            {
                var testStep = action as ITestStep;
                _simpleSteps.Add(new SimpleStep(testStep.Title.ToString(), testStep.ExpectedResult.ToString()));
            }

            _testStepsGridView.DataSource = _simpleSteps;
            this.Enabled = true;
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
            if ((int)e.KeyChar == 13)
                LoadButton_Click(null, null);
        }

        private void TestStepsGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            if (_testStepsGridView.CurrentRow == null)
                _rowLabel.Text = "Row:";
            else
                _rowLabel.Text = String.Format("Row:{0}", _testStepsGridView.CurrentRow.Index + 1);
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
