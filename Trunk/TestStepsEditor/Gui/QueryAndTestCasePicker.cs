using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.TeamFoundation.TestManagement.Client;

namespace TestStepsEditor.Gui
{
	public partial class QueryAndTestCasePicker : Form
	{
		private ITestManagementTeamProject _project;
		private List<ITestCase> _testCaseList = new List<ITestCase>();
		private int _testCaseNumber;

		private delegate void PopulateTestCaseList();

		public QueryAndTestCasePicker()
		{
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

		public int TestCaseNumber 
		{
			get
			{
				return _testCaseNumber;
			}

			private set
			{
				if (value > 0 && value != this._testCaseNumber)
				{
					this._testCaseNumber = value;
					OnTestCaseNumberSet(EventArgs.Empty);
				}
			}
		}

		private void RunQueryButton_Click(object sender, EventArgs e)
		{
			if (this._queryTreeView.SelectedNode == null || this._queryTreeView.SelectedNode.Tag == null)
			{
				MessageBox.Show("Please click on an actual query in the tree and try again.");
				return;
			}

			try
			{
				this._testCaseList.Clear();
				ITestCaseQuery query = (ITestCaseQuery)this._queryTreeView.SelectedNode.Tag;
				IEnumerable<ITestCase> testCaseEnumerable = query.Execute();
				IEnumerator<ITestCase> testCaseEnumerator = testCaseEnumerable.GetEnumerator();
				while (testCaseEnumerator.MoveNext())
				{
					this._testCaseList.Add(testCaseEnumerator.Current);
				}

				if (this._testCaseList.Count == 0)
				{
					return;
				}
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				MessageBox.Show(message, "Exception");
			}

			if (this._testCaseList == null || this._testCaseList.Count == 0)
			{
				MessageBox.Show("No test cases were retrieved by this query, please select another query and try again.");
				return;
			}

			this.PopulateTestList();
		}

		private void QueryPicker_Load(object sender, EventArgs e)
		{
			this._queryTreeView.Nodes.Clear();
			this._testCaseListBox.Items.Clear();
			this._queryTreeView.Nodes.Add("My Queries");
			this._queryTreeView.Nodes.Add("Team Queries");
			IList<ITestCaseQuery> allQueries = _project.Queries;

			foreach (ITestCaseQuery query in allQueries)
			{
				if (!query.QueryText.Contains("WorkItemLinks") && (query.QueryText.Contains("= 'Test Case'") || query.QueryText.Contains("in group 'Test Case Category'")))
				{
					this.PopulateTreeView(query);
				}
			}

			this._queryTreeView.ExpandAll();
			this._queryTreeView.TopNode = this._queryTreeView.Nodes[0];
		}

		private void CloseButton_Click(object sender, EventArgs e)
		{
			this.Hide();
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

		private void PopulateTestList()
		{
			if (this._testCaseListBox.InvokeRequired)
			{
				PopulateTestCaseList populateTestCaseList = this.PopulateTestList;
				this.Invoke(populateTestCaseList);
			}
			else
			{
				this._testCaseListBox.Items.Clear();
				foreach (ITestCase testCase in this._testCaseList)
				{
					this._testCaseListBox.Items.Add(string.Format("{0}: {1}", testCase.Id, testCase.Title));
				}

				this.SetControlsState(true);
			}
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

			workingNode.Nodes.Add(new TreeNode { Text = string.Format("Query: {0}", queryName), Name = queryName, Tag = query });
		}

		private void LoadSelectedTestCaseButton_Click(object sender, EventArgs e)
		{
			if (this._testCaseListBox.SelectedItem == null)
			{
				MessageBox.Show("Please click on a test case entry in the list and try again.");
				return;
			}
			
			int working;
			if (int.TryParse(this._testCaseListBox.SelectedItem.ToString().Split(':')[0], out working))
			{ 
				this.TestCaseNumber = working;
			}
		}

		public event EventHandler TestCaseNumberSet;

		protected virtual void OnTestCaseNumberSet(EventArgs e)
		{
			if (TestCaseNumberSet != null)
			{
				TestCaseNumberSet(this, e);
			}
		}

		private void SwitchToQuerySelectorButton_Click(object sender, EventArgs e)
		{
			this.SetControlsState(false);
		}

		private void SwitchToTestCaseSelectorButton_Click(object sender, EventArgs e)
		{
			this.SetControlsState(true);
		}

		private void SetControlsState(bool viewTestCasesList)
		{
			this._switchToQuerySelectorButton.Visible = this._loadSelectedTestCaseButton.Visible = this._testCaseListBox.Visible = viewTestCasesList;
			this._switchToTestCaseSelectorButton.Visible = this._runQueryButton.Visible = this._queryTreeView.Visible = !viewTestCasesList;
			this._instructionLabel.Text = viewTestCasesList ? @"Select a test case from the list and click the 'Load' button." : @"Select a query to execute:"; 
		}

		private void QueryAndTestCasePicker_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			this.Hide();
		}
	}
}
