using System;

namespace TestStepsEditor
{
	public class SimpleStep
	{
		private readonly bool _isTestStep = true;

		public string Title { get; set; }
		public string ExpectedResult { get; set; }

        public SimpleStep()
        {
            Title = String.Empty;
            ExpectedResult = String.Empty;
        }

		public SimpleStep(string title, string expectedResult, bool isTestStep = true)
		{
			_isTestStep = isTestStep;
			Title = title.Replace("\n", "\r\n");
			ExpectedResult = expectedResult.Replace("\n", "\r\n");
		}

		public bool IsTestStep()
		{
			return _isTestStep;
		}
	}
}
