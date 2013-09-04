namespace TestStepsEditor.Gui
{
	partial class QueryAndTestCasePicker
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
			this._runQueryButton = new System.Windows.Forms.Button();
			this._loadSelectedTestCaseButton = new System.Windows.Forms.Button();
			this._queryTestsSplitContainer = new TestStepsEditor.Gui.SplitContainerEx();
			this._queryTreeView = new System.Windows.Forms.TreeView();
			this._testCaseListBox = new System.Windows.Forms.ListBox();
			((System.ComponentModel.ISupportInitialize)(this._queryTestsSplitContainer)).BeginInit();
			this._queryTestsSplitContainer.Panel1.SuspendLayout();
			this._queryTestsSplitContainer.Panel2.SuspendLayout();
			this._queryTestsSplitContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// _runQueryButton
			// 
			this._runQueryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._runQueryButton.Location = new System.Drawing.Point(12, 375);
			this._runQueryButton.Name = "_runQueryButton";
			this._runQueryButton.Size = new System.Drawing.Size(132, 23);
			this._runQueryButton.TabIndex = 2;
			this._runQueryButton.Text = "Run Selected Query";
			this._runQueryButton.UseVisualStyleBackColor = true;
			this._runQueryButton.Click += new System.EventHandler(this.RunQueryButton_Click);
			// 
			// _loadSelectedTestCaseButton
			// 
			this._loadSelectedTestCaseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._loadSelectedTestCaseButton.Location = new System.Drawing.Point(499, 375);
			this._loadSelectedTestCaseButton.Name = "_loadSelectedTestCaseButton";
			this._loadSelectedTestCaseButton.Size = new System.Drawing.Size(132, 23);
			this._loadSelectedTestCaseButton.TabIndex = 6;
			this._loadSelectedTestCaseButton.Text = "Load Selected Test Case";
			this._loadSelectedTestCaseButton.UseVisualStyleBackColor = true;
			this._loadSelectedTestCaseButton.Click += new System.EventHandler(this.LoadSelectedTestCaseButton_Click);
			// 
			// _queryTestsSplitContainer
			// 
			this._queryTestsSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this._queryTestsSplitContainer.Location = new System.Drawing.Point(13, 12);
			this._queryTestsSplitContainer.Name = "_queryTestsSplitContainer";
			// 
			// _queryTestsSplitContainer.Panel1
			// 
			this._queryTestsSplitContainer.Panel1.Controls.Add(this._queryTreeView);
			// 
			// _queryTestsSplitContainer.Panel2
			// 
			this._queryTestsSplitContainer.Panel2.Controls.Add(this._testCaseListBox);
			this._queryTestsSplitContainer.Size = new System.Drawing.Size(618, 357);
			this._queryTestsSplitContainer.SplitterDistance = 300;
			this._queryTestsSplitContainer.TabIndex = 7;
			// 
			// _queryTreeView
			// 
			this._queryTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this._queryTreeView.Location = new System.Drawing.Point(0, 0);
			this._queryTreeView.Name = "_queryTreeView";
			this._queryTreeView.Size = new System.Drawing.Size(300, 357);
			this._queryTreeView.TabIndex = 5;
			this._queryTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.QueryTreeView_NodeMouseDoubleClick);
			// 
			// _testCaseListBox
			// 
			this._testCaseListBox.DisplayMember = "Name";
			this._testCaseListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this._testCaseListBox.FormattingEnabled = true;
			this._testCaseListBox.IntegralHeight = false;
			this._testCaseListBox.Location = new System.Drawing.Point(0, 0);
			this._testCaseListBox.Name = "_testCaseListBox";
			this._testCaseListBox.Size = new System.Drawing.Size(314, 357);
			this._testCaseListBox.TabIndex = 0;
			this._testCaseListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TestCaseListBox_MouseDoubleClick);
			// 
			// QueryAndTestCasePicker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(643, 410);
			this.Controls.Add(this._queryTestsSplitContainer);
			this.Controls.Add(this._loadSelectedTestCaseButton);
			this.Controls.Add(this._runQueryButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(414, 402);
			this.Name = "QueryAndTestCasePicker";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Query and Test Case Picker";
			this.Load += new System.EventHandler(this.QueryPicker_Load);
			this._queryTestsSplitContainer.Panel1.ResumeLayout(false);
			this._queryTestsSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._queryTestsSplitContainer)).EndInit();
			this._queryTestsSplitContainer.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button _runQueryButton;
		private System.Windows.Forms.TreeView _queryTreeView;
		private System.Windows.Forms.ListBox _testCaseListBox;
		private System.Windows.Forms.Button _loadSelectedTestCaseButton;
		private SplitContainerEx _queryTestsSplitContainer;
	}
}