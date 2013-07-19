namespace TestStepsEditor.Gui
{
    partial class SelectTestCase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectTestCase));
            this._testCaseListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this._copyListToClipboardButton = new System.Windows.Forms.Button();
            this._closeWindowButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _testCaseListBox
            // 
            this._testCaseListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._testCaseListBox.FormattingEnabled = true;
            this._testCaseListBox.Location = new System.Drawing.Point(12, 51);
            this._testCaseListBox.Name = "_testCaseListBox";
            this._testCaseListBox.Size = new System.Drawing.Size(531, 303);
            this._testCaseListBox.TabIndex = 0;
            this._testCaseListBox.SelectedIndexChanged += new System.EventHandler(this.TestCaseListBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.MinimumSize = new System.Drawing.Size(473, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(473, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Click on a test case entry below to automatically open it in the Test Case Editor" +
    ".\r\nWait for test case to open before selecting another test case.";
            // 
            // _copyListToClipboardButton
            // 
            this._copyListToClipboardButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._copyListToClipboardButton.Location = new System.Drawing.Point(12, 360);
            this._copyListToClipboardButton.Name = "_copyListToClipboardButton";
            this._copyListToClipboardButton.Size = new System.Drawing.Size(137, 23);
            this._copyListToClipboardButton.TabIndex = 2;
            this._copyListToClipboardButton.Text = "Copy List To Clipboard";
            this._copyListToClipboardButton.UseVisualStyleBackColor = true;
            this._copyListToClipboardButton.Click += new System.EventHandler(this.CopyListToClipboardButton_Click);
            // 
            // _closeWindowButton
            // 
            this._closeWindowButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._closeWindowButton.Location = new System.Drawing.Point(407, 360);
            this._closeWindowButton.Name = "_closeWindowButton";
            this._closeWindowButton.Size = new System.Drawing.Size(137, 23);
            this._closeWindowButton.TabIndex = 3;
            this._closeWindowButton.Text = "Close Window";
            this._closeWindowButton.UseVisualStyleBackColor = true;
            this._closeWindowButton.Click += new System.EventHandler(this.CloseWindowButton_Click);
            // 
            // SelectTestCase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 392);
            this.ControlBox = false;
            this.Controls.Add(this._closeWindowButton);
            this.Controls.Add(this._copyListToClipboardButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._testCaseListBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SelectTestCase";
            this.Text = "Test Case Picker";
            this.Load += new System.EventHandler(this.SelectTestCase_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox _testCaseListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _copyListToClipboardButton;
        private System.Windows.Forms.Button _closeWindowButton;
    }
}