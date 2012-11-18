namespace TestStepsEditor.Gui
{
    partial class UnhandledExceptionDialog
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
            this._mainLabel = new System.Windows.Forms.Label();
            this._messageTextBox = new System.Windows.Forms.TextBox();
            this._infoTabControl = new System.Windows.Forms.TabControl();
            this._messageTabPage = new System.Windows.Forms.TabPage();
            this._detailTabPage = new System.Windows.Forms.TabPage();
            this._detailTextBox = new System.Windows.Forms.TextBox();
            this._exitButton = new System.Windows.Forms.Button();
            this._sendReportButton = new System.Windows.Forms.Button();
            this._infoTabControl.SuspendLayout();
            this._messageTabPage.SuspendLayout();
            this._detailTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainLabel
            // 
            this._mainLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._mainLabel.Location = new System.Drawing.Point(13, 13);
            this._mainLabel.Name = "_mainLabel";
            this._mainLabel.Size = new System.Drawing.Size(375, 51);
            this._mainLabel.TabIndex = 0;
            this._mainLabel.Text = "An unexpected error has occurred.";
            // 
            // _messageTextBox
            // 
            this._messageTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._messageTextBox.Location = new System.Drawing.Point(3, 3);
            this._messageTextBox.Multiline = true;
            this._messageTextBox.Name = "_messageTextBox";
            this._messageTextBox.ReadOnly = true;
            this._messageTextBox.Size = new System.Drawing.Size(358, 110);
            this._messageTextBox.TabIndex = 2;
            // 
            // _infoTabControl
            // 
            this._infoTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._infoTabControl.Controls.Add(this._messageTabPage);
            this._infoTabControl.Controls.Add(this._detailTabPage);
            this._infoTabControl.Location = new System.Drawing.Point(16, 67);
            this._infoTabControl.Name = "_infoTabControl";
            this._infoTabControl.SelectedIndex = 0;
            this._infoTabControl.Size = new System.Drawing.Size(372, 142);
            this._infoTabControl.TabIndex = 3;
            // 
            // _messageTabPage
            // 
            this._messageTabPage.Controls.Add(this._messageTextBox);
            this._messageTabPage.Location = new System.Drawing.Point(4, 22);
            this._messageTabPage.Name = "_messageTabPage";
            this._messageTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._messageTabPage.Size = new System.Drawing.Size(364, 116);
            this._messageTabPage.TabIndex = 0;
            this._messageTabPage.Text = "Message";
            this._messageTabPage.UseVisualStyleBackColor = true;
            // 
            // _detailTabPage
            // 
            this._detailTabPage.Controls.Add(this._detailTextBox);
            this._detailTabPage.Location = new System.Drawing.Point(4, 22);
            this._detailTabPage.Name = "_detailTabPage";
            this._detailTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._detailTabPage.Size = new System.Drawing.Size(364, 116);
            this._detailTabPage.TabIndex = 1;
            this._detailTabPage.Text = "Detail";
            this._detailTabPage.UseVisualStyleBackColor = true;
            // 
            // _detailTextBox
            // 
            this._detailTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._detailTextBox.Location = new System.Drawing.Point(3, 3);
            this._detailTextBox.Multiline = true;
            this._detailTextBox.Name = "_detailTextBox";
            this._detailTextBox.ReadOnly = true;
            this._detailTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._detailTextBox.Size = new System.Drawing.Size(358, 110);
            this._detailTextBox.TabIndex = 0;
            // 
            // _exitButton
            // 
            this._exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._exitButton.Location = new System.Drawing.Point(308, 215);
            this._exitButton.Name = "_exitButton";
            this._exitButton.Size = new System.Drawing.Size(75, 23);
            this._exitButton.TabIndex = 4;
            this._exitButton.Text = "E&xit";
            this._exitButton.UseVisualStyleBackColor = true;
            // 
            // _sendReportButton
            // 
            this._sendReportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._sendReportButton.Location = new System.Drawing.Point(220, 215);
            this._sendReportButton.Name = "_sendReportButton";
            this._sendReportButton.Size = new System.Drawing.Size(82, 23);
            this._sendReportButton.TabIndex = 5;
            this._sendReportButton.Text = "Send &Report";
            this._sendReportButton.UseVisualStyleBackColor = true;
            this._sendReportButton.Click += new System.EventHandler(this.SendReportButton_Click);
            // 
            // UnhandledExceptionDialog
            // 
            this.AcceptButton = this._sendReportButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._exitButton;
            this.ClientSize = new System.Drawing.Size(400, 250);
            this.ControlBox = false;
            this.Controls.Add(this._sendReportButton);
            this.Controls.Add(this._exitButton);
            this.Controls.Add(this._infoTabControl);
            this.Controls.Add(this._mainLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UnhandledExceptionDialog";
            this.ShowIcon = false;
            this.Text = "Unhandled Exception";
            this._infoTabControl.ResumeLayout(false);
            this._messageTabPage.ResumeLayout(false);
            this._messageTabPage.PerformLayout();
            this._detailTabPage.ResumeLayout(false);
            this._detailTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label _mainLabel;
        private System.Windows.Forms.TextBox _messageTextBox;
        private System.Windows.Forms.TabControl _infoTabControl;
        private System.Windows.Forms.TabPage _messageTabPage;
        private System.Windows.Forms.TabPage _detailTabPage;
        private System.Windows.Forms.TextBox _detailTextBox;
        private System.Windows.Forms.Button _exitButton;
        private System.Windows.Forms.Button _sendReportButton;
    }
}