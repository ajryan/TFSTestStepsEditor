using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

using Microsoft.TeamFoundation.TestManagement.Client;

namespace TestStepsEditor.Gui
{
    public partial class SelectQuery : Form
    {
        private ITestManagementTeamProject _project;
        private readonly MainForm _mainForm;

        public SelectQuery(MainForm mainTestCaseEditorForm)
        {
            this._mainForm = mainTestCaseEditorForm;
            InitializeComponent();
        }

        public ITestManagementTeamProject Project
        {
            get
            {
                return _project;
            }

            set
            {
                _project = value;
            }
        }

        private void RunQueryButton_Click(object sender, EventArgs e)
        {
            try
            {
                ITestCaseQuery query = this._showTreeViewRadioButton.Checked ? 
                    (ITestCaseQuery)this._queryTreeView.SelectedNode.Tag : 
                    (ITestCaseQuery)this._queryListBox.SelectedItem;
                this._mainForm.TestCasesFromQuery = query.Execute();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                MessageBox.Show(message);
            }

            this.Close();
        }

        private void QueryPicker_Load(object sender, EventArgs e)
        {
            this._queryTreeView.Nodes.Clear();
            this._queryListBox.Items.Clear();
            this._queryTreeView.Nodes.Add("My Queries");
            this._queryTreeView.Nodes.Add("Team Queries");
            IList<ITestCaseQuery> allQueries = _project.Queries;

            foreach (ITestCaseQuery query in allQueries)
            {
                if (query.QueryText.Contains("= 'Test Case'") || query.QueryText.Contains("in group 'Test Case Category'"))
                {
                    this.PopulateFlatList(query);
                    this.PopulateTreeView(query);
                }
            }               
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this._mainForm.TestCasesFromQuery = null;
            this.Close();
        }

        private void PopulateTreeView(ITestCaseQuery query)
        {
            if (string.IsNullOrEmpty(query.Owner))
            {
                this.AddToMyQueries(query);
            }
            else
            {
                this.AddToTeamQueries(query);
            }
        }

        private void PopulateFlatList(ITestCaseQuery query)
        {
            this._queryListBox.Items.Add(query);
        }

        private void AddToTeamQueries(ITestCaseQuery query)
        {
            this.AddToTree(query, this._queryTreeView.Nodes[1]);
        }

        private void AddToMyQueries(ITestCaseQuery query)
        {
            this.AddToTree(query, this._queryTreeView.Nodes[0]);
        }
        
        private void AddToTree(ITestCaseQuery query, TreeNode workingNode)
        {
            string working = query.Name.Replace("«", string.Empty).Trim();
            string queryName;
            string[] path = null;
            string[] parts = working.Split('»');
            if (parts.Length == 2)
            {
                path = parts[0].Split('⁄');
                queryName = parts[1];
            }
            else
            {
                queryName = parts[0];
            }

            if (path != null)
            {
                foreach (string part in path)
                {
                    TreeNode newNode = new TreeNode { Text = part, Name = part };
                    TreeNode[] foundNode = workingNode.Nodes.Find(newNode.Name, false);

                    if (foundNode.Length == 0)
                    {
                        workingNode.Nodes.Add(newNode);
                        workingNode = workingNode.Nodes[workingNode.Nodes.Count - 1];
                    }
                    else
                    {
                        workingNode = foundNode[0];
                    }
                }
            }

            workingNode.Nodes.Add(new TreeNode { Text = queryName, Name = queryName, Tag = query });
        }
        
        private void CopyListButton_Click(object sender, EventArgs e)
        {
            StringBuilder queryList = new StringBuilder();
            foreach (string entry in _queryListBox.Items)
            {
                queryList.AppendLine(entry);
            }

            Clipboard.SetText(queryList.ToString());
        }

        private void TreeViewRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this._queryTreeView.Visible = this._showTreeViewRadioButton.Checked;
            this._queryListBox.Visible = this._showFlatListRadioButton.Checked;
        }
    }
}
