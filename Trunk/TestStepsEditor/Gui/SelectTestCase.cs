using System;
using System.Text;
using System.Windows.Forms;
using Microsoft.TeamFoundation.TestManagement.Client;

namespace TestStepsEditor.Gui
{
    public partial class SelectTestCase : Form
    {
        private readonly MainForm _mainForm;

        private delegate void PopulateTestCaseList(ITestCase testCase);

        public SelectTestCase(MainForm mainTestCaseEditorForm)
        {
            this._mainForm = mainTestCaseEditorForm;
            InitializeComponent();
        }

        private void SelectTestCase_Load(object sender, EventArgs e)
        {
            foreach (var testCase in this._mainForm.TestCasesFromQuery)
            {
                this.AddTestCaseToList(testCase);
            }
        }

        private void TestCaseListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._mainForm.TestCaseNumber = this._testCaseListBox.SelectedItem.ToString().Trim().Split(':')[0];
        }

        private void AddTestCaseToList(ITestCase testCase)
        {
            if (this._testCaseListBox.InvokeRequired)
            {
                PopulateTestCaseList populateTestCaseList = this.AddTestCaseToList;
                this.Invoke(populateTestCaseList, testCase);
            }
            else
            {
                this._testCaseListBox.Items.Add(string.Format("{0}: {1}", testCase.Id, testCase.Title));
            }
        }

        private void CloseWindowButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CopyListToClipboardButton_Click(object sender, EventArgs e)
        {
            StringBuilder testCaseList = new StringBuilder();
            foreach (string entry in this._testCaseListBox.Items)
            {
                testCaseList.AppendLine(entry);
            }

            Clipboard.SetText(testCaseList.ToString());
        }
    }
}
