using System;
using Microsoft.TeamFoundation.TestManagement.Client;

namespace TestStepsEditor.Tfs
{
	public class SimpleTestPoint
	{
		public string Title { get; set; }
		public ITestPoint TestPoint { get; set; }
	}
}
