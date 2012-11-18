using System;
using System.ComponentModel;
using System.Linq;
using Microsoft.TeamFoundation.TestManagement.Client;
using NLog;

namespace TestStepsEditor.Tfs
{
	public class SimpleSteps : BindingList<SimpleStep>
	{
		private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

		private int _originalCount;

		public SimpleSteps(int originalCount)
		{
			_originalCount = originalCount;
		}
		
		public bool Dirty
		{
			get
			{
				return 
					_originalCount != this.Count ||
					this.Any(simpleStep => simpleStep.Dirty);
			}
		}

		public bool IsHtml { get; private set; }

		public void ResetDirtyState()
		{
			_originalCount = this.Count;

			foreach (var simpleStep in this)
				simpleStep.ResetDirtyState();
		}

		public static SimpleSteps Create(TestEditInfo testInfo)
		{
			_Logger.Info("New SimpleSteps from test case " + testInfo.WorkItemId);

			var newSteps = new SimpleSteps(testInfo.TestCase.Actions.Count);

			foreach (ITestAction action in testInfo.TestCase.Actions)
			{
				if (action is ITestStep)
				{
					var testStep = action as ITestStep;

					_Logger.Debug("Step " + testStep.Id);

					newSteps.IsHtml = newSteps.IsHtml || (testStep.Title.ToString().IndexOf("<HTML>", StringComparison.OrdinalIgnoreCase) != -1);
					newSteps.Add(new SimpleStep(testStep.Title.ToString(), testStep.ExpectedResult.ToString()));
				}
				else if (action is ISharedStepReference)
				{
					var sharedStep = action as ISharedStepReference;

					_Logger.Debug("Shared Step " + sharedStep.SharedStepId);

					newSteps.Add(new SimpleStep("Shared step ID " + sharedStep.SharedStepId, String.Empty, false));
				}
				else
				{
					_Logger.Debug("Unknown action of type " + action.GetType());

					newSteps.Add(new SimpleStep("Unknown action " + action.Id, String.Empty, false));
				}
			}
			return newSteps;
		}
	}
}
