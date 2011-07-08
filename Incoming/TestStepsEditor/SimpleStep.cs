using System;
using System.ComponentModel;

namespace TestStepsEditor
{
	public class SimpleStep
	{
		private readonly bool _isTestStep = true;

		public string Title
		{
			get
			{
				return _title;
			}

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
			get
			{
				return _expectedResult;
			}

			set
			{
				if (_originalExpectedResult == null)
					_originalExpectedResult = _expectedResult;

				_expectedResult = value;
			}
		}
		private string _expectedResult;
		private string _originalExpectedResult;

		public bool Done { get; set; }

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
			_title = title.Replace("\n", "\r\n");
			_expectedResult = expectedResult.Replace("\n", "\r\n");
			_isTestStep = isTestStep;
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
}
