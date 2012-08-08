namespace TestStepsEditor.Gui
{
	partial class ParametersDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParametersDialog));
			this._parametersListView = new System.Windows.Forms.ListView();
			this._closeButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// _parametersListView
			// 
			this._parametersListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._parametersListView.GridLines = true;
			this._parametersListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this._parametersListView.Location = new System.Drawing.Point(13, 13);
			this._parametersListView.MultiSelect = false;
			this._parametersListView.Name = "_parametersListView";
			this._parametersListView.ShowGroups = false;
			this._parametersListView.Size = new System.Drawing.Size(265, 214);
			this._parametersListView.TabIndex = 0;
			this._parametersListView.UseCompatibleStateImageBehavior = false;
			this._parametersListView.View = System.Windows.Forms.View.Details;
			// 
			// _closeButton
			// 
			this._closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._closeButton.Location = new System.Drawing.Point(202, 233);
			this._closeButton.Name = "_closeButton";
			this._closeButton.Size = new System.Drawing.Size(75, 23);
			this._closeButton.TabIndex = 1;
			this._closeButton.Text = "Close";
			this._closeButton.UseVisualStyleBackColor = true;
			this._closeButton.Click += new System.EventHandler(this.CloseButton_Click);
			// 
			// ParametersDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this._closeButton;
			this.ClientSize = new System.Drawing.Size(290, 268);
			this.Controls.Add(this._closeButton);
			this.Controls.Add(this._parametersListView);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ParametersDialog";
			this.Text = "Test Parameters";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView _parametersListView;
		private System.Windows.Forms.Button _closeButton;

	}
}