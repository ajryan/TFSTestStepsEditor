using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.TeamFoundation.TestManagement.Client;

namespace TestStepsEditor.Tfs
{
	public class SimpleStep
	{
		private readonly bool _isTestStep = true;

		public SimpleStep()
		{
			Title = String.Empty;
			ExpectedResult = String.Empty;
			Outcome = TestOutcome.Inconclusive;
			AttachmentPaths = new List<string>();
		}

		public SimpleStep(string title, string expectedResult, bool isTestStep = true)
		{
			_title = CleanText(title);
			_expectedResult = CleanText(expectedResult);

			Outcome = TestOutcome.Inconclusive;
			AttachmentPaths = new List<string>();

			_isTestStep = isTestStep;
		}

		/// Step title
		public string Title
		{
			get { return _title; }
			set
			{
				if (_originalTitle == null) _originalTitle = _title;
				_title = value;
			}
		}
		private string _title;
		private string _originalTitle;

		/// Expected result
		public string ExpectedResult
		{
			get { return _expectedResult; }

			set
			{
				if (_originalExpectedResult == null) _originalExpectedResult = _expectedResult;
				_expectedResult = value;
			}
		}
		private string _expectedResult;
		private string _originalExpectedResult;

		/// Outcome of executed step
		public TestOutcome Outcome { get; set; }

		/// List of paths to step attachments
		[Browsable(false)]
		public List<string> AttachmentPaths { get; set; }

		/// Has the step changed since being loaded
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

		public bool IsTestStep()
		{
			return _isTestStep;
		}

		public void ResetDirtyState()
		{
			_originalTitle = null;
			_originalExpectedResult = null;
		}

		private static string CleanText(string text)
		{
			string cleaned = text.Replace("\n", "\r\n");
			if (cleaned.IndexOf("<HTML>", StringComparison.OrdinalIgnoreCase) == -1)
				return cleaned;

			return cleaned
				.Substring(12, cleaned.Length - 12 - 14)
				.Replace("<P>", "")
				.Replace("</P>", "\r\n")
				.TrimEnd('\r', '\n');
		}
	}
}
