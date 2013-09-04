using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.TeamFoundation.TestManagement.Client;

namespace TestStepsEditor.Gui
{
	public partial class QueryAndTestCasePicker : Form
	{
		public event EventHandler TestCaseNumberSet;

		private readonly List<ITestCase> _testCaseList = new List<ITestCase>();
		private readonly ITestManagementTeamProject _project;

		private int _testCaseNumber;

		public QueryAndTestCasePicker(ITestManagementTeamProject project)
		{
			InitializeComponent();

			_project = project;
		}

		public int TestCaseNumber 
		{
			get
			{
				return _testCaseNumber;
			}

			private set
			{
				if (value > 0 && value != _testCaseNumber)
				{
					_testCaseNumber = value;
					OnTestCaseNumberSet(EventArgs.Empty);
				}
			}
		}

		private void QueryTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			RunQueryButton_Click(this, null);
		}

		private void RunQueryButton_Click(object sender, EventArgs e)
		{
			if (_queryTreeView.SelectedNode == null || _queryTreeView.SelectedNode.Tag == null)
			{
				MessageBox.Show(@"Please click on an actual query in the tree and try again.");
				return;
			}

			try
			{
				_testCaseList.Clear();
				ITestCaseQuery query = (ITestCaseQuery) _queryTreeView.SelectedNode.Tag;
				_testCaseList.AddRange(query.Execute());
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				MessageBox.Show(message, @"Exception");
			}

			if (_testCaseList == null || _testCaseList.Count == 0)
			{
				MessageBox.Show(@"No test cases were retrieved by this query, please select another query and try again.");
				return;
			}

			PopulateTestList();
		}
		
		private void QueryPicker_Load(object sender, EventArgs e)
		{
			_queryTreeView.Nodes.Clear();
			_testCaseListBox.Items.Clear();
			_queryTreeView.Nodes.Add("My Queries");
			_queryTreeView.Nodes.Add("Team Queries");

			foreach (ITestCaseQuery query in _project.Queries)
			{
				if (!query.QueryText.Contains("WorkItemLinks") && 
					(
						query.QueryText.Contains("= 'Test Case'") || 
						query.QueryText.Contains("in group 'Test Case Category'") ||
                        query.QueryText.Contains("in group 'Microsoft.TestCaseCategory'")
					))
				{
					PopulateTreeView(query);
				}
			}

			_queryTreeView.ExpandAll();
			_queryTreeView.TopNode = _queryTreeView.Nodes[0];
		}

		private void PopulateTreeView(ITestCaseQuery query)
		{
			if (string.IsNullOrEmpty(query.Owner))
			{
				AddToMyQueries(query);
			}
			else
			{
				AddToTeamQueries(query);
			}
		}

		private void PopulateTestList()
		{
			if (_testCaseListBox.InvokeRequired)
			{
				Invoke((Action)(PopulateTestList));
			}
			else
			{
				_testCaseListBox.Items.Clear();
				foreach (ITestCase testCase in _testCaseList)
				{
					_testCaseListBox.Items.Add(string.Format("{0}: {1}", testCase.Id, testCase.Title));
				}
			}
		}

		private void AddToTeamQueries(ITestCaseQuery query)
		{
			AddToTree(query, _queryTreeView.Nodes[1]);
		}

		private void AddToMyQueries(ITestCaseQuery query)
		{
			AddToTree(query, _queryTreeView.Nodes[0]);
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

		private void TestCaseListBox_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			var itemIndex = _testCaseListBox.IndexFromPoint(e.Location);
			if (itemIndex != ListBox.NoMatches)
			{
				_testCaseListBox.SelectedIndex = itemIndex;
				LoadSelectedTestCaseButton_Click(this, null);
			}
		}

		private void LoadSelectedTestCaseButton_Click(object sender, EventArgs e)
		{
			if (_testCaseListBox.SelectedItem == null)
			{
				MessageBox.Show(@"Please click on a test case entry in the list and try again.");
				return;
			}
			
			int working;
			if (int.TryParse(_testCaseListBox.SelectedItem.ToString().Split(':')[0], out working))
			{ 
				TestCaseNumber = working;
			}
		}

		protected virtual void OnTestCaseNumberSet(EventArgs e)
		{
			if (TestCaseNumberSet != null)
			{
				TestCaseNumberSet(this, e);
			}
		}
	}
}
