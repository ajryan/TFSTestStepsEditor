using System;
using Microsoft.TeamFoundation.TestManagement.Client;
using NLog;
using TestStepsEditor.Gui;

namespace TestStepsEditor.Tfs
{
	public class TestEditInfo
	{
		private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

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

			_Logger.Info("Save test case " + WorkItemId);

			int stepNumber = 0;
			foreach (SimpleStep step in SimpleSteps)
			{
				// do not save empty final step
				if (stepNumber == SimpleSteps.Count - 1 &&
					String.IsNullOrEmpty(step.Title) &&
					String.IsNullOrEmpty(step.ExpectedResult))
				{
					_Logger.Debug("Skip empty final step.");
					break;
				}

				if (TestCase.Actions.Count <= stepNumber)
				{
					_Logger.Debug("Add action.");
					TestCase.Actions.Add(TestCase.CreateTestStep());
				}

				// directly apply to existing ITestSteps
				if (TestCase.Actions[stepNumber] is ITestStep)
				{
					_Logger.Debug("Populate existing action at " + stepNumber);

					PopulateTestStep((ITestStep) TestCase.Actions[stepNumber], step.Title, step.ExpectedResult);
				}
				// current action is not ITestStep: if user-entered data is a step
				else if (step.IsTestStep())
				{
					_Logger.Debug("Insert action at " + stepNumber);

					// insert a new action at this point
					var newAction = TestCase.CreateTestStep();
					PopulateTestStep(newAction, step.Title, step.ExpectedResult);
					TestCase.Actions.Insert(stepNumber, newAction);
				}

				stepNumber++;
			}

			while (stepNumber < TestCase.Actions.Count)
			{
				_Logger.Debug("Remove overflow step number " + stepNumber);
				TestCase.Actions.RemoveAt(stepNumber);
			}

			_Logger.Debug("TFS Save.");

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
