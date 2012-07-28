namespace TestStepsEditor.Gui
{
	partial class StringGeneratorForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StringGeneratorForm));
			this._lengthUpDown = new System.Windows.Forms.NumericUpDown();
			this._lengthLabel = new System.Windows.Forms.Label();
			this._upperCaseCheckBox = new System.Windows.Forms.CheckBox();
			this._lowerCaseCheckBox = new System.Windows.Forms.CheckBox();
			this._digitsCheckBox = new System.Windows.Forms.CheckBox();
			this._generateButton = new System.Windows.Forms.Button();
			this._specificCharactersCheckBox = new System.Windows.Forms.CheckBox();
			this._specificCharsTextBox = new System.Windows.Forms.TextBox();
			this._predefinedGroupBox = new System.Windows.Forms.GroupBox();
			this._predefinedListBox = new System.Windows.Forms.ListBox();
			this._preDefinedRadioButton = new System.Windows.Forms.RadioButton();
			this._randomGroupBox = new System.Windows.Forms.GroupBox();
			this._randomRadioButton = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this._lengthUpDown)).BeginInit();
			this._predefinedGroupBox.SuspendLayout();
			this._randomGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// _lengthUpDown
			// 
			this._lengthUpDown.Location = new System.Drawing.Point(80, 22);
			this._lengthUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this._lengthUpDown.Name = "_lengthUpDown";
			this._lengthUpDown.Size = new System.Drawing.Size(125, 20);
			this._lengthUpDown.TabIndex = 1;
			this._lengthUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// _lengthLabel
			// 
			this._lengthLabel.AutoSize = true;
			this._lengthLabel.Location = new System.Drawing.Point(31, 24);
			this._lengthLabel.Name = "_lengthLabel";
			this._lengthLabel.Size = new System.Drawing.Size(40, 13);
			this._lengthLabel.TabIndex = 0;
			this._lengthLabel.Text = "&Length";
			// 
			// _upperCaseCheckBox
			// 
			this._upperCaseCheckBox.AutoSize = true;
			this._upperCaseCheckBox.Checked = true;
			this._upperCaseCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this._upperCaseCheckBox.Location = new System.Drawing.Point(34, 59);
			this._upperCaseCheckBox.Name = "_upperCaseCheckBox";
			this._upperCaseCheckBox.Size = new System.Drawing.Size(147, 17);
			this._upperCaseCheckBox.TabIndex = 2;
			this._upperCaseCheckBox.Text = "&UPPER CASE characters";
			this._upperCaseCheckBox.UseVisualStyleBackColor = true;
			// 
			// _lowerCaseCheckBox
			// 
			this._lowerCaseCheckBox.AutoSize = true;
			this._lowerCaseCheckBox.Checked = true;
			this._lowerCaseCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this._lowerCaseCheckBox.Location = new System.Drawing.Point(34, 82);
			this._lowerCaseCheckBox.Name = "_lowerCaseCheckBox";
			this._lowerCaseCheckBox.Size = new System.Drawing.Size(130, 17);
			this._lowerCaseCheckBox.TabIndex = 3;
			this._lowerCaseCheckBox.Text = "&lower case characters";
			this._lowerCaseCheckBox.UseVisualStyleBackColor = true;
			// 
			// _digitsCheckBox
			// 
			this._digitsCheckBox.AutoSize = true;
			this._digitsCheckBox.Checked = true;
			this._digitsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this._digitsCheckBox.Location = new System.Drawing.Point(34, 105);
			this._digitsCheckBox.Name = "_digitsCheckBox";
			this._digitsCheckBox.Size = new System.Drawing.Size(113, 17);
			this._digitsCheckBox.TabIndex = 4;
			this._digitsCheckBox.Text = "1234567890 &digits";
			this._digitsCheckBox.UseVisualStyleBackColor = true;
			// 
			// _generateButton
			// 
			this._generateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._generateButton.Location = new System.Drawing.Point(412, 236);
			this._generateButton.Name = "_generateButton";
			this._generateButton.Size = new System.Drawing.Size(148, 23);
			this._generateButton.TabIndex = 4;
			this._generateButton.Text = "&Generate to Clipboard";
			this._generateButton.UseVisualStyleBackColor = true;
			this._generateButton.Click += new System.EventHandler(this.GenerateButton_Click);
			// 
			// _specificCharactersCheckBox
			// 
			this._specificCharactersCheckBox.AutoSize = true;
			this._specificCharactersCheckBox.Location = new System.Drawing.Point(34, 128);
			this._specificCharactersCheckBox.Name = "_specificCharactersCheckBox";
			this._specificCharactersCheckBox.Size = new System.Drawing.Size(117, 17);
			this._specificCharactersCheckBox.TabIndex = 5;
			this._specificCharactersCheckBox.Text = "&Specific characters";
			this._specificCharactersCheckBox.UseVisualStyleBackColor = true;
			this._specificCharactersCheckBox.CheckedChanged += new System.EventHandler(this.SpecificCharsCheckBox_CheckedChanged);
			// 
			// _specificCharsTextBox
			// 
			this._specificCharsTextBox.Enabled = false;
			this._specificCharsTextBox.Location = new System.Drawing.Point(50, 151);
			this._specificCharsTextBox.Name = "_specificCharsTextBox";
			this._specificCharsTextBox.Size = new System.Drawing.Size(155, 20);
			this._specificCharsTextBox.TabIndex = 6;
			// 
			// _predefinedGroupBox
			// 
			this._predefinedGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._predefinedGroupBox.Controls.Add(this._predefinedListBox);
			this._predefinedGroupBox.Location = new System.Drawing.Point(224, 12);
			this._predefinedGroupBox.Name = "_predefinedGroupBox";
			this._predefinedGroupBox.Size = new System.Drawing.Size(336, 218);
			this._predefinedGroupBox.TabIndex = 3;
			this._predefinedGroupBox.TabStop = false;
			// 
			// _predefinedListBox
			// 
			this._predefinedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._predefinedListBox.FormattingEnabled = true;
			this._predefinedListBox.Location = new System.Drawing.Point(5, 22);
			this._predefinedListBox.Name = "_predefinedListBox";
			this._predefinedListBox.Size = new System.Drawing.Size(324, 186);
			this._predefinedListBox.TabIndex = 0;
			this._predefinedListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PredefinedListBox_MouseDoubleClick);
			// 
			// _preDefinedRadioButton
			// 
			this._preDefinedRadioButton.AutoSize = true;
			this._preDefinedRadioButton.Location = new System.Drawing.Point(234, 9);
			this._preDefinedRadioButton.Name = "_preDefinedRadioButton";
			this._preDefinedRadioButton.Size = new System.Drawing.Size(79, 17);
			this._preDefinedRadioButton.TabIndex = 1;
			this._preDefinedRadioButton.TabStop = true;
			this._preDefinedRadioButton.Text = "Pre-defined";
			this._preDefinedRadioButton.UseVisualStyleBackColor = true;
			this._preDefinedRadioButton.CheckedChanged += new System.EventHandler(this.RandomPreDefinedRadioButton_CheckedChanged);
			// 
			// _randomGroupBox
			// 
			this._randomGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this._randomGroupBox.Controls.Add(this._lengthLabel);
			this._randomGroupBox.Controls.Add(this._specificCharsTextBox);
			this._randomGroupBox.Controls.Add(this._specificCharactersCheckBox);
			this._randomGroupBox.Controls.Add(this._digitsCheckBox);
			this._randomGroupBox.Controls.Add(this._lengthUpDown);
			this._randomGroupBox.Controls.Add(this._upperCaseCheckBox);
			this._randomGroupBox.Controls.Add(this._lowerCaseCheckBox);
			this._randomGroupBox.Location = new System.Drawing.Point(12, 12);
			this._randomGroupBox.Name = "_randomGroupBox";
			this._randomGroupBox.Size = new System.Drawing.Size(211, 218);
			this._randomGroupBox.TabIndex = 2;
			this._randomGroupBox.TabStop = false;
			// 
			// _randomRadioButton
			// 
			this._randomRadioButton.AutoSize = true;
			this._randomRadioButton.Checked = true;
			this._randomRadioButton.Location = new System.Drawing.Point(21, 10);
			this._randomRadioButton.Name = "_randomRadioButton";
			this._randomRadioButton.Size = new System.Drawing.Size(65, 17);
			this._randomRadioButton.TabIndex = 0;
			this._randomRadioButton.TabStop = true;
			this._randomRadioButton.Text = "Random";
			this._randomRadioButton.UseVisualStyleBackColor = true;
			this._randomRadioButton.CheckedChanged += new System.EventHandler(this.RandomPreDefinedRadioButton_CheckedChanged);
			// 
			// StringGeneratorForm
			// 
			this.AcceptButton = this._generateButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(575, 271);
			this.Controls.Add(this._randomRadioButton);
			this.Controls.Add(this._preDefinedRadioButton);
			this.Controls.Add(this._generateButton);
			this.Controls.Add(this._randomGroupBox);
			this.Controls.Add(this._predefinedGroupBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "StringGeneratorForm";
			this.Text = "String Generator";
			((System.ComponentModel.ISupportInitialize)(this._lengthUpDown)).EndInit();
			this._predefinedGroupBox.ResumeLayout(false);
			this._randomGroupBox.ResumeLayout(false);
			this._randomGroupBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown _lengthUpDown;
		private System.Windows.Forms.Label _lengthLabel;
		private System.Windows.Forms.CheckBox _upperCaseCheckBox;
		private System.Windows.Forms.CheckBox _lowerCaseCheckBox;
		private System.Windows.Forms.CheckBox _digitsCheckBox;
		private System.Windows.Forms.Button _generateButton;
		private System.Windows.Forms.CheckBox _specificCharactersCheckBox;
		private System.Windows.Forms.TextBox _specificCharsTextBox;
		private System.Windows.Forms.GroupBox _predefinedGroupBox;
		private System.Windows.Forms.RadioButton _preDefinedRadioButton;
		private System.Windows.Forms.ListBox _predefinedListBox;
		private System.Windows.Forms.GroupBox _randomGroupBox;
		private System.Windows.Forms.RadioButton _randomRadioButton;
	}
}
