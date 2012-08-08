namespace TestStepsEditor.Gui
{
	partial class TestSuiteDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestSuiteDialog));
			this._cancelButton = new System.Windows.Forms.Button();
			this._okButton = new System.Windows.Forms.Button();
			this._promptLabel = new System.Windows.Forms.Label();
			this._suiteListBox = new System.Windows.Forms.ListBox();
			this._refreshButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// _cancelButton
			// 
			this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._cancelButton.Location = new System.Drawing.Point(202, 233);
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.Size = new System.Drawing.Size(75, 23);
			this._cancelButton.TabIndex = 1;
			this._cancelButton.Text = "Cancel";
			this._cancelButton.UseVisualStyleBackColor = true;
			this._cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
			// 
			// _okButton
			// 
			this._okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._okButton.Location = new System.Drawing.Point(121, 233);
			this._okButton.Name = "_okButton";
			this._okButton.Size = new System.Drawing.Size(75, 23);
			this._okButton.TabIndex = 2;
			this._okButton.Text = "OK";
			this._okButton.UseVisualStyleBackColor = true;
			this._okButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// _promptLabel
			// 
			this._promptLabel.AutoSize = true;
			this._promptLabel.Location = new System.Drawing.Point(13, 13);
			this._promptLabel.Name = "_promptLabel";
			this._promptLabel.Size = new System.Drawing.Size(101, 13);
			this._promptLabel.TabIndex = 3;
			this._promptLabel.Text = "Publish to test suite:";
			// 
			// _suiteListBox
			// 
			this._suiteListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._suiteListBox.FormattingEnabled = true;
			this._suiteListBox.Location = new System.Drawing.Point(16, 30);
			this._suiteListBox.Name = "_suiteListBox";
			this._suiteListBox.Size = new System.Drawing.Size(261, 186);
			this._suiteListBox.TabIndex = 4;
			// 
			// _refreshButton
			// 
			this._refreshButton.Location = new System.Drawing.Point(16, 233);
			this._refreshButton.Name = "_refreshButton";
			this._refreshButton.Size = new System.Drawing.Size(75, 23);
			this._refreshButton.TabIndex = 5;
			this._refreshButton.Text = "&Refresh";
			this._refreshButton.UseVisualStyleBackColor = true;
			this._refreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
			// 
			// TestSuiteDialog
			// 
			this.AcceptButton = this._okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this._cancelButton;
			this.ClientSize = new System.Drawing.Size(290, 268);
			this.Controls.Add(this._refreshButton);
			this.Controls.Add(this._suiteListBox);
			this.Controls.Add(this._promptLabel);
			this.Controls.Add(this._okButton);
			this.Controls.Add(this._cancelButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "TestSuiteDialog";
			this.Text = "Select Test Suite";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button _cancelButton;
		private System.Windows.Forms.Button _okButton;
		private System.Windows.Forms.Label _promptLabel;
		private System.Windows.Forms.ListBox _suiteListBox;
		private System.Windows.Forms.Button _refreshButton;
	}
}