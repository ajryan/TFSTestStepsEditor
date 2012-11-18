using System;
using System.Windows.Forms;
using NLog;

namespace TestStepsEditor
{
	static class Program
	{
		static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

		[STAThread]
		static void Main()
		{
			_Logger.Info("Application Start");
			Application.ApplicationExit += 
				(sender, e) => _Logger.Info("Application Exit");

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}
