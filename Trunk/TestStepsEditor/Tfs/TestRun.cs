using System;
using System.Linq;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.TestManagement.Client;
using NLog;

namespace TestStepsEditor.Tfs
{
	public class TestRun
	{
		private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

		private readonly TestEditInfo _testEditInfo;
		private readonly TeamFoundationIdentity _currentIdentity;
		private readonly ITestPoint _testPoint;

		public TestRun(
			TestEditInfo testEditInfo,
			ITestPoint testPoint,
			TeamFoundationIdentity currentIdentity)
		{
			_testEditInfo = testEditInfo;
			_testPoint = testPoint;
			_currentIdentity = currentIdentity;
		}

		// TODO: messageboxes into exception throws
		public void Publish()
		{
			_Logger.Info("Publish test case " + _testEditInfo.WorkItemId);

			_Logger.Debug("Create run");
			var tfsRun = _testPoint.Plan.CreateTestRun(false);
			
			tfsRun.DateStarted = DateTime.Now;
			tfsRun.AddTestPoint(_testPoint, _currentIdentity);
			tfsRun.DateCompleted = DateTime.Now;
			tfsRun.Save(); // so results object is created
			
			_Logger.Debug("QueryResults");
			var result = tfsRun.QueryResults()[0];
			result.Owner = _currentIdentity;
			result.RunBy = _currentIdentity;
			result.State = TestResultState.Completed;
			result.DateStarted = DateTime.Now;
			result.Duration = new TimeSpan(0L);
			result.DateCompleted = DateTime.Now.AddMinutes(0.0);

			_Logger.Debug("CreateIteration");
			var iteration = result.CreateIteration(1);
			iteration.DateStarted = DateTime.Now;
			iteration.DateCompleted = DateTime.Now;
			iteration.Duration = new TimeSpan(0L);
			iteration.Comment = "Run from TFS Test Steps Editor by " + _currentIdentity.DisplayName;

			for (int actionIndex = 0; actionIndex < _testEditInfo.TestCase.Actions.Count; actionIndex++)
			{
				_Logger.Debug("Action " + actionIndex);
				var testAction = _testEditInfo.TestCase.Actions[actionIndex];
				if (testAction is ISharedStepReference)
					continue;

				var userStep = _testEditInfo.SimpleSteps[actionIndex];

				_Logger.Debug("Create step result for action " + testAction.Id);
				var stepResult = iteration.CreateStepResult(testAction.Id);
				stepResult.ErrorMessage = String.Empty;
				stepResult.Outcome = userStep.Outcome;

				foreach (var attachmentPath in userStep.AttachmentPaths)
				{
					_Logger.Debug("Create attachment from " + attachmentPath);
					var attachment = stepResult.CreateAttachment(attachmentPath);
					stepResult.Attachments.Add(attachment);
				}

				iteration.Actions.Add(stepResult);
			}

			var overallOutcome = _testEditInfo.SimpleSteps.Any(s => s.Outcome != TestOutcome.Passed)
				? TestOutcome.Failed
				: TestOutcome.Passed;

			_Logger.Info("Overall outcome: " + overallOutcome);

			iteration.Outcome = overallOutcome;

			_Logger.Debug("Add iteration");
			result.Iterations.Add(iteration);

			result.Outcome = overallOutcome;
			result.Save(false);
		}
	}
}
