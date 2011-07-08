using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.TeamFoundation.TestManagement.Client;

namespace TestStepsEditor
{
	internal class TestEditInfo
	{
		public int WorkItemId { get; set; }
		public ITestBase TestCase { get; set; }
		public SimpleSteps SimpleSteps { get; set; }
		public DataGridView DataGridView { get; set; }
		public string Message { get; set; }

		public TestEditInfo(int workItemId)
		{
			WorkItemId = workItemId;
		}
			
		public TestEditInfo(ITestBase testCase, SimpleSteps simpleSteps, DataGridView dataGridView)
		{
			WorkItemId = testCase.Id;
			TestCase = testCase;
			SimpleSteps = simpleSteps;
			DataGridView = dataGridView;
		}
	}
}