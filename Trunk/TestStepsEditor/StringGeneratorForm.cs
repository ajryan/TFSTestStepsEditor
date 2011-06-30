using System;
using System.Windows.Forms;

namespace TestStepsEditor
{
	public partial class StringGeneratorForm : Form
	{
		private const string LOWERCASE_ALPHA = "abcdefghijklmnopqrstuvwxyz";
		private const string UPPERCASE_ALPHA = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		private const string DIGITS = "0123456789";

		private static bool _LastUpper = true;
		private static bool _LastLower = true;
		private static bool _LastDigits = true;
		private static bool _LastSpecific = false;
		private static string _LastSpecificChars = String.Empty;
		private static int _LastLength = 10;

		public StringGeneratorForm()
		{
			InitializeComponent();

			_lengthUpDown.Maximum = Int32.MaxValue;

			_upperCaseCheckBox.Checked = _LastUpper;
			_lowerCaseCheckBox.Checked = _LastLower;
			_digitsCheckBox.Checked = _LastDigits;
			_specificCharactersCheckBox.Checked = _LastSpecific;
			_specificCharsTextBox.Text = _LastSpecificChars;
			_lengthUpDown.Value = _LastLength;
			
			TopLevel = true;
			KeyPreview = true;
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Escape)
			{
				DialogResult = DialogResult.Cancel;
				return true;
			}

			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void GenerateButton_Click(object sender, System.EventArgs e)
		{
			bool lower = _lowerCaseCheckBox.Checked;
			bool upper = _upperCaseCheckBox.Checked;
			bool digit = _digitsCheckBox.Checked;
			bool specific = _specificCharactersCheckBox.Checked && _specificCharsTextBox.Text.Length > 0;

			if ((!lower && !upper && !digit && !specific) || _lengthUpDown.Value < 1)
			{
				MessageBox.Show("No characters selected to generate.", "Nothing to Generate");
				return;
			}

			string allowedChars = String.Empty;
			if (lower) allowedChars += LOWERCASE_ALPHA;
			if (upper) allowedChars += UPPERCASE_ALPHA;
			if (digit) allowedChars += DIGITS;
			if (specific) allowedChars += _specificCharsTextBox.Text;

			int allowedCharsLen = allowedChars.Length - 1;

			var rnd = new Random();
			int length = (int) _lengthUpDown.Value;
			char[] resultChars = new char[length];
			for (int resultIndex = 0; resultIndex < length; resultIndex++)
			{
				resultChars[resultIndex] = allowedChars[rnd.Next(0, allowedCharsLen)];
			}

			string result = new string(resultChars);
			Clipboard.SetText(result);

			_LastUpper = _upperCaseCheckBox.Checked;
			_LastLower = _lowerCaseCheckBox.Checked;
			_LastDigits = _digitsCheckBox.Checked;
			_LastSpecific = _specificCharactersCheckBox.Checked;
			_LastSpecificChars = _specificCharsTextBox.Text;
			_LastLength = length;

			DialogResult = DialogResult.OK;
		}

		private void SpecificCharsCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			_specificCharsTextBox.Enabled = _specificCharactersCheckBox.Checked;
		}
	}
}
