using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TestStepsEditor.Gui
{
	public partial class StringGeneratorForm : Form
	{
		private const string LOWERCASE_ALPHA = "abcdefghijklmnopqrstuvwxyz";
		private const string UPPERCASE_ALPHA = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		private const string DIGITS = "0123456789";

		private static Size _LastSize = new Size(583, 295);
		private static bool _LastUpper = true;
		private static bool _LastLower = true;
		private static bool _LastDigits = true;
		private static bool _LastSpecific = false;
		private static string _LastSpecificChars = String.Empty;
		private static int _LastLength = 10;
		private static bool _LastRandomRadio = true;
		

		public StringGeneratorForm()
		{
			InitializeComponent();

			_lengthUpDown.Maximum = Int32.MaxValue;

			Size = _LastSize;
			_upperCaseCheckBox.Checked = _LastUpper;
			_lowerCaseCheckBox.Checked = _LastLower;
			_digitsCheckBox.Checked = _LastDigits;
			_specificCharactersCheckBox.Checked = _LastSpecific;
			_specificCharsTextBox.Text = _LastSpecificChars;
			_lengthUpDown.Value = _LastLength;
			
			_randomRadioButton.Checked = _LastRandomRadio;
			_preDefinedRadioButton.Checked = !_LastRandomRadio;

			string localUserAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			string predefinedStringsPath = Path.Combine(localUserAppDataPath, "TestStepsEditorStrings.txt");
			if (File.Exists(predefinedStringsPath))
			{
				string[] predefinedStrings = File.ReadAllLines(predefinedStringsPath);
				_predefinedListBox.DataSource = predefinedStrings;
			}

			TopLevel = true;
			KeyPreview = true;

			Load += (o, e) => RandomPreDefinedRadioButton_CheckedChanged(null, null);
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

		private void GenerateButton_Click(object sender, EventArgs e)
		{
			string text = _randomRadioButton.Checked
				? GenerateRandom()
				: GeneratePreDefined();

			if (!String.IsNullOrEmpty(text))
				Clipboard.SetText(text);

			_LastSize = Size;
			_LastUpper = _upperCaseCheckBox.Checked;
			_LastLower = _lowerCaseCheckBox.Checked;
			_LastDigits = _digitsCheckBox.Checked;
			_LastSpecific = _specificCharactersCheckBox.Checked;
			_LastSpecificChars = _specificCharsTextBox.Text;
			_LastLength = (int) _lengthUpDown.Value;
			_LastRandomRadio = _randomRadioButton.Checked;

			DialogResult = DialogResult.OK;
		}

		private string GenerateRandom()
		{
			bool lower = _lowerCaseCheckBox.Checked;
			bool upper = _upperCaseCheckBox.Checked;
			bool digit = _digitsCheckBox.Checked;
			bool specific = _specificCharactersCheckBox.Checked && _specificCharsTextBox.Text.Length > 0;

			if ((!lower && !upper && !digit && !specific) || _lengthUpDown.Value < 1)
			{
				MessageBox.Show("No characters selected to generate.", "Nothing to Generate");
				return null;
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

			return new string(resultChars);
		}

		private string GeneratePreDefined()
		{
			return (string) _predefinedListBox.SelectedValue;
		}

		private void SpecificCharsCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			_specificCharsTextBox.Enabled = _specificCharactersCheckBox.Checked;
		}

		private void RandomPreDefinedRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			bool randomChecked = _randomRadioButton.Checked;
			_randomGroupBox.Enabled = randomChecked;
			_predefinedGroupBox.Enabled = !randomChecked;

			if (randomChecked)
			{
				_lengthUpDown.Focus();
				_generateButton.Text = "&Generate to Clipboard";
			}
			else
			{
				_predefinedListBox.Focus();
				_generateButton.Text = "&Copy to Clipboard";
			}
		}

		private void PredefinedListBox_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			int itemIndex = _predefinedListBox.IndexFromPoint(e.Location);
			if (itemIndex == ListBox.NoMatches)
				return;

			GenerateButton_Click(null, null);
		}
	}
}
