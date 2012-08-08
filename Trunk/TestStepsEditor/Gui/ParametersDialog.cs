using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Microsoft.TeamFoundation.TestManagement.Client;
using TestStepsEditor.Tfs;

namespace TestStepsEditor.Gui
{
	public partial class ParametersDialog : Form
	{
		public ParametersDialog(ITestCase testCase)
		{
			InitializeComponent();
			this.Text = String.Format(
				"{0} - {1} {2}",
				this.Text, testCase.Id, testCase.Title);

			var parameters = GetParameters(testCase);

			foreach (var parameter in parameters)
				_parametersListView.Columns.Add(parameter.Name, parameter.Name);

			for (int valueIndex = 0; valueIndex < parameters[0].Values.Count; valueIndex++)
			{
				var valuesList = parameters.Select(parameter => parameter.Values[valueIndex]).ToArray();

				_parametersListView.Items.Add(
					new ListViewItem(valuesList));
			}
		}

		private void CloseButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private static List<SimpleParameter> GetParameters(ITestCase testCase)
		{
			var parametersList = new List<SimpleParameter>(testCase.TestParameters.Count);
			
			var dataTable = testCase.Data.Tables[0];
			for (int colIndex = 0; colIndex < dataTable.Columns.Count; colIndex++)
			{
				var dataColumn = dataTable.Columns[colIndex];

				var values = new List<string>();
				for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
					values.Add(dataTable.Rows[rowIndex][dataColumn].ToString());
				parametersList.Add(new SimpleParameter(dataColumn.ColumnName, values));
			}

			return parametersList;
		}
	}
}
