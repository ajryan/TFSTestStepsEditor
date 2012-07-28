using System;
using System.ComponentModel;
using System.Linq;
using Microsoft.TeamFoundation.TestManagement.Client;

namespace TestStepsEditor.Tfs
{
	public class SimpleSteps : BindingList<SimpleStep>
	{
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

		public void ResetDirtyState()
		{
			_originalCount = this.Count;

			foreach (var simpleStep in this)
				simpleStep.ResetDirtyState();
		}

		public static SimpleSteps Create(TestEditInfo testInfo)
		{
			var newSteps = new SimpleSteps(testInfo.TestCase.Actions.Count);

			foreach (ITestAction action in testInfo.TestCase.Actions)
			{
				if (action is ITestStep)
				{
					var testStep = action as ITestStep;
					newSteps.Add(new SimpleStep(testStep.Title.ToString(), testStep.ExpectedResult.ToString()));
				}
				else if (action is ISharedStepReference)
				{
					var sharedStep = action as ISharedStepReference;
					newSteps.Add(new SimpleStep("Shared step ID " + sharedStep.SharedStepId, String.Empty, false));
				}
				else
				{
					newSteps.Add(new SimpleStep("Unknown action " + action.Id, String.Empty, false));
				}
			}
			return newSteps;
		}
	}
}
