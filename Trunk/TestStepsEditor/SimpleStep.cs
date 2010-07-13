using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestStepsEditor
{
	public class SimpleStep
	{
		public string Title { get; set; }
		public string ExpectedResult { get; set; }

		public SimpleStep(string title, string expectedResult)
		{
			Title = title.Replace("\n", "\r\n");
			ExpectedResult = expectedResult.Replace("\n", "\r\n");
		}
	}
}
