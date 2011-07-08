namespace TestStepsEditor
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
			((System.ComponentModel.ISupportInitialize)(this._lengthUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// _lengthUpDown
			// 
			this._lengthUpDown.Location = new System.Drawing.Point(59, 17);
			this._lengthUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this._lengthUpDown.Name = "_lengthUpDown";
			this._lengthUpDown.Size = new System.Drawing.Size(101, 20);
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
			this._lengthLabel.Location = new System.Drawing.Point(13, 19);
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
			this._upperCaseCheckBox.Location = new System.Drawing.Point(13, 48);
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
			this._lowerCaseCheckBox.Location = new System.Drawing.Point(13, 71);
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
			this._digitsCheckBox.Location = new System.Drawing.Point(13, 95);
			this._digitsCheckBox.Name = "_digitsCheckBox";
			this._digitsCheckBox.Size = new System.Drawing.Size(113, 17);
			this._digitsCheckBox.TabIndex = 4;
			this._digitsCheckBox.Text = "1234567890 &digits";
			this._digitsCheckBox.UseVisualStyleBackColor = true;
			// 
			// _generateButton
			// 
			this._generateButton.Location = new System.Drawing.Point(12, 187);
			this._generateButton.Name = "_generateButton";
			this._generateButton.Size = new System.Drawing.Size(148, 23);
			this._generateButton.TabIndex = 7;
			this._generateButton.Text = "&Generate to Clipboard";
			this._generateButton.UseVisualStyleBackColor = true;
			this._generateButton.Click += new System.EventHandler(this.GenerateButton_Click);
			// 
			// _specificCharactersCheckBox
			// 
			this._specificCharactersCheckBox.AutoSize = true;
			this._specificCharactersCheckBox.Location = new System.Drawing.Point(13, 118);
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
			this._specificCharsTextBox.Location = new System.Drawing.Point(30, 141);
			this._specificCharsTextBox.Name = "_specificCharsTextBox";
			this._specificCharsTextBox.Size = new System.Drawing.Size(130, 20);
			this._specificCharsTextBox.TabIndex = 6;
			// 
			// StringGeneratorForm
			// 
			this.AcceptButton = this._generateButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(172, 222);
			this.Controls.Add(this._specificCharsTextBox);
			this.Controls.Add(this._specificCharactersCheckBox);
			this.Controls.Add(this._generateButton);
			this.Controls.Add(this._digitsCheckBox);
			this.Controls.Add(this._lowerCaseCheckBox);
			this.Controls.Add(this._upperCaseCheckBox);
			this.Controls.Add(this._lengthLabel);
			this.Controls.Add(this._lengthUpDown);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "StringGeneratorForm";
			this.Text = "String Generator";
			((System.ComponentModel.ISupportInitialize)(this._lengthUpDown)).EndInit();
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
	}
}