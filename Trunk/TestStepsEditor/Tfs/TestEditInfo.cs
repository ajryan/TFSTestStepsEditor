using System;
using Microsoft.TeamFoundation.TestManagement.Client;
using TestStepsEditor.Gui;

namespace TestStepsEditor.Tfs
{
	public class TestEditInfo
	{
		public int WorkItemId { get; set; }
		public ITestBase TestCase { get; set; }
		public SimpleSteps SimpleSteps { get; set; }
		public TestEditUserControl TestEditControl { get; set; }
		public string Message { get; set; }

		public TestEditInfo(int workItemId)
		{
			WorkItemId = workItemId;
		}
			
		public TestEditInfo(ITestBase testCase, SimpleSteps simpleSteps, TestEditUserControl testEditControl)
		{
			WorkItemId = testCase.Id;
			TestCase = testCase;
			SimpleSteps = simpleSteps;
			TestEditControl = testEditControl;
		}

		public void Save()
		{
			if (TestCase == null || SimpleSteps == null || SimpleSteps.Count == 0)
				return;

			int stepNumber = 0;
			foreach (SimpleStep step in SimpleSteps)
			{
				// do not save empty final step
				if (stepNumber == SimpleSteps.Count - 1 &&
					String.IsNullOrEmpty(step.Title) &&
					String.IsNullOrEmpty(step.ExpectedResult))
				{
					break;
				}

				if (TestCase.Actions.Count <= stepNumber)
				{
					TestCase.Actions.Add(TestCase.CreateTestStep());
				}

				// directly apply to existing ITestSteps
				if (TestCase.Actions[stepNumber] is ITestStep)
				{
					PopulateTestStep((ITestStep) TestCase.Actions[stepNumber], step.Title, step.ExpectedResult);
				}
				// current action is not ITestStep: if user-entered data is a step
				else if (step.IsTestStep())
				{
					// insert a new action at this point
					var newAction = TestCase.CreateTestStep();
					PopulateTestStep(newAction, step.Title, step.ExpectedResult);
					TestCase.Actions.Insert(stepNumber, newAction);
				}

				stepNumber++;
			}

			while (stepNumber < TestCase.Actions.Count)
				TestCase.Actions.RemoveAt(stepNumber);

			TestCase.Save();
			SimpleSteps.ResetDirtyState();
		}

		private void PopulateTestStep(ITestStep step, string title, string expectedResult)
		{
			step.Title = GetStepText(title);
			step.ExpectedResult = GetStepText(expectedResult);
			step.TestStepType = String.IsNullOrWhiteSpace(expectedResult) ? TestStepType.ActionStep : TestStepType.ValidateStep;
		}

		private ParameterizedString GetStepText(string text)
		{
			if (!SimpleSteps.IsHtml)
				return new ParameterizedString(text);

			string[] textTokens = text.Split(new []{Environment.NewLine}, StringSplitOptions.None);
			return new ParameterizedString(
				"<HTML><BODY><P>" + String.Join("</P><P>", textTokens	) + "</P></BODY></HTML>");
		}
	}
}
