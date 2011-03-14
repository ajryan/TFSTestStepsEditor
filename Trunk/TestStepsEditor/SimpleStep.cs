using System;
using System.ComponentModel;
using System.Linq;

namespace TestStepsEditor
{
	public class SimpleStep
	{
		private readonly bool _isTestStep = true;

		public string Title
		{
			get { return _title; }
			set
			{
				if (_originalTitle == null)
					_originalTitle = _title;

				_title = value;
			}
		}
		private string _title;
		private string _originalTitle;

		public string ExpectedResult
		{
			get { return _expectedResult; }
			set
			{
				if (_originalExpectedResult == null)
					_originalExpectedResult = _expectedResult;

				_expectedResult = value;
			}
		}
		private string _expectedResult;
		private string _originalExpectedResult;

		[Browsable(false)]
		public bool Dirty
		{
			get
			{
				return
					(_originalTitle != null && _originalTitle != _title) ||
					(_originalExpectedResult != null && _originalExpectedResult != _expectedResult);
			}
		}

        public SimpleStep()
        {
            Title = String.Empty;
            ExpectedResult = String.Empty;
        }

		public SimpleStep(string title, string expectedResult, bool isTestStep = true)
		{
			_isTestStep = isTestStep;
			_title = title.Replace("\n", "\r\n");
			_expectedResult = expectedResult.Replace("\n", "\r\n");
		}

		public bool IsTestStep()
		{
			return _isTestStep;
		}

		public void ResetDirtyState()
		{
			_originalTitle = null;
			_originalExpectedResult = null;
		}
	}

	public class SimpleSteps : BindingList<SimpleStep>
	{
		private readonly int _originalCount;

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
			foreach (var simpleStep in this)
			{
				simpleStep.ResetDirtyState();
			}
		}
	}
}
