using System;
using NLog;
using TestStepsEditor.Gui;

namespace TestStepsEditor
{
	public class ExceptionReporter
	{
		private readonly object _exceptionObject;
		private readonly Exception _exception;

		public ExceptionReporter(object exceptionObject)
		{
			_exceptionObject = exceptionObject;
			_exception = exceptionObject as Exception;
		}


		public void ReportException()
		{
			try
			{
				ReportExceptionUnsafe();
			}
			catch
			{
				// not a darn thing
			}
		}

		private void ReportExceptionUnsafe()
		{
			var logger = LogManager.GetCurrentClassLogger();

			if (_exception != null)
			{
				logger.FatalException("Reporting Exception", _exception);
			}
			else
			{
				logger.Error(
					"Reporting Exception Object. Type {0}; ToString {1}",
					_exceptionObject.GetType(), _exceptionObject.ToString());
			}

			var dialog = new UnhandledExceptionDialog(_exceptionObject);
			dialog.ShowDialog();
		}
	}
}
