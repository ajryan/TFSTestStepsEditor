using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Forms;
using NLog;
using NLog.Layouts;
using NLog.Targets;
using NLog.Targets.Wrappers;

namespace TestStepsEditor.Gui
{
	public partial class UnhandledExceptionDialog : Form
	{
		public UnhandledExceptionDialog()
		{
			InitializeComponent();
		}

		public UnhandledExceptionDialog(object exceptionObject)
		{
			InitializeComponent();

			string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			string logPath = Path.Combine(localAppData, @"TestStepsEditor\Log.txt");

			_mainLabel.Text = String.Format("An unhandled error occured. Please report the error so we can fix it. " +
											"A log file has been saved to {0} that can be emailed to ryan.aidan@gmail.com.",
											logPath);

			var exception = exceptionObject as Exception;
			if (exception != null)
			{
				_messageTextBox.Text = exception.Message;
				_detailTextBox.Text = exception.ToString();
			}
			else
			{
				_messageTextBox.Text = "Error of type " + exceptionObject.GetType();
				_detailTextBox.Text = "Error object dump: " + exceptionObject.ToString();
			}
		}

		private void SendReportButton_Click(object sender, EventArgs e)
		{
			_sendReportButton.Enabled = false;

			try
			{
				var wrapper = (AsyncTargetWrapper) LogManager.Configuration.FindTargetByName("logFile");
				wrapper.Flush(x => { });

				var fileTarget = (FileTarget) wrapper.WrappedTarget;
				fileTarget.Flush(x => { });
				var fileNameLayout = (SimpleLayout) fileTarget.FileName;
				var fileName = fileNameLayout.Render(new LogEventInfo()).Replace(@"/", @"\");
				string logBody = File.ReadAllText(fileName);

				string body = String.Format(
					"Message: {0}\r\n\r\nDump: {1}\r\n\r\nLog Body:\r\n{2}",
					_messageTextBox.Text,
					_detailTextBox.Text,
					logBody);

				string bodyEncoded = "errorReport=" + HttpUtility.UrlEncode(body);
				var bodyBytes = Encoding.UTF8.GetBytes(bodyEncoded);

				var request = WebRequest.Create("http://teststepseditor.apphb.com/ErrorReporting/Report");
				request.Method = "POST";
				request.ContentType = "application/x-www-form-urlencoded";
				request.ContentLength = bodyBytes.Length;

				var requestStream = request.GetRequestStream();
				requestStream.Write(bodyBytes, 0, bodyBytes.Length);
				requestStream.Close();

				string responseBody = "";
				var response = (HttpWebResponse) request.GetResponse();
				using (var responseStream = response.GetResponseStream())
				{
					
					using (var reader = new StreamReader(responseStream, Encoding.UTF8))
					{
						string line;
						while ((line = reader.ReadLine()) != null)
							responseBody += line;
					}
				}

				if (String.IsNullOrWhiteSpace(responseBody))
					responseBody = "<no response from server>";

				MessageBox.Show("Send error report: " + responseBody.Replace("\"", String.Empty));
			}
			catch (Exception ex)
			{
				_sendReportButton.Enabled = true;
				MessageBox.Show("Could not report error: " + ex.Message);
			}
		}
	}
}
