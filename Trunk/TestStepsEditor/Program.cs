using System;
using System.Windows.Forms;
using NLog;
using TestStepsEditor.Gui;

namespace TestStepsEditor
{
	static class Program
	{
		static readonly Logger _Logger = LogManager.GetCurrentClassLogger();
		static readonly object _ExLock = new object();
		static bool _HandlingThreadEx = false;
		
		[STAThread]
		static void Main()
		{
			_Logger.Info("Application Start");
			
			Application.ApplicationExit += (sender, e) => _Logger.Info("Application Exit");

			Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
			AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}

		private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
		{
			if (_HandlingThreadEx) return;

			lock (_ExLock)
			{
				if (_HandlingThreadEx) return;
				_HandlingThreadEx = true;

				var reporter = new ExceptionReporter(unhandledExceptionEventArgs.ExceptionObject);
				reporter.ReportException();
			}
		}
	}
}
