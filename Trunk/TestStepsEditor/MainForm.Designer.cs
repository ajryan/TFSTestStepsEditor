namespace TestStepsEditor
{
	partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this._testGridContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this._copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
			this._bottomToolStrip = new System.Windows.Forms.ToolStrip();
			this._findToolStripLabel = new System.Windows.Forms.ToolStripLabel();
			this._findToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
			this._findToolStripButton = new System.Windows.Forms.ToolStripButton();
			this._replaceToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
			this._replaceToolStripSplitButton = new System.Windows.Forms.ToolStripSplitButton();
			this._replaceAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.replaceInSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._testStateToolStripLabel = new System.Windows.Forms.ToolStripLabel();
			this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
			this._topToolStrip = new System.Windows.Forms.ToolStrip();
			this._workItemToolStripLabel = new System.Windows.Forms.ToolStripLabel();
			this._workItemIdToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
			this._loadToolStripButton = new System.Windows.Forms.ToolStripButton();
			this._saveToolStripButton = new System.Windows.Forms.ToolStripButton();
			this._closeToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this._insertStepToolStripButton = new System.Windows.Forms.ToolStripButton();
			this._deleteStepToolStripButton = new System.Windows.Forms.ToolStripButton();
			this._changeProjectToolStripButton = new System.Windows.Forms.ToolStripButton();
			this._connStateToolStripLabel = new System.Windows.Forms.ToolStripLabel();
			this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
			this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
			this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
			this._toolStripContainer = new System.Windows.Forms.ToolStripContainer();
			this._testTabControl = new System.Windows.Forms.TabControl();
			this._loadTestBackgroundWorker = new System.ComponentModel.BackgroundWorker();
			this._saveTestBackgroundWorker = new System.ComponentModel.BackgroundWorker();
			this._testGridContextMenu.SuspendLayout();
			this._bottomToolStrip.SuspendLayout();
			this._topToolStrip.SuspendLayout();
			this._toolStripContainer.BottomToolStripPanel.SuspendLayout();
			this._toolStripContainer.ContentPanel.SuspendLayout();
			this._toolStripContainer.TopToolStripPanel.SuspendLayout();
			this._toolStripContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// _testGridContextMenu
			// 
			this._testGridContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._copyToolStripMenuItem,
            this._pasteToolStripMenuItem});
			this._testGridContextMenu.Name = "_testGridContextMenu";
			this._testGridContextMenu.Size = new System.Drawing.Size(103, 48);
			// 
			// _copyToolStripMenuItem
			// 
			this._copyToolStripMenuItem.Name = "_copyToolStripMenuItem";
			this._copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
			this._copyToolStripMenuItem.Text = "&Copy";
			this._copyToolStripMenuItem.Click += new System.EventHandler(this.TestGridContext_Copy_Click);
			// 
			// _pasteToolStripMenuItem
			// 
			this._pasteToolStripMenuItem.Name = "_pasteToolStripMenuItem";
			this._pasteToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
			this._pasteToolStripMenuItem.Text = "&Paste";
			this._pasteToolStripMenuItem.Click += new System.EventHandler(this.TestGridContext_Paste_Click);
			// 
			// BottomToolStripPanel
			// 
			this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
			this.BottomToolStripPanel.Name = "BottomToolStripPanel";
			this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
			// 
			// _bottomToolStrip
			// 
			this._bottomToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this._bottomToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._findToolStripLabel,
            this._findToolStripTextBox,
            this._findToolStripButton,
            this._replaceToolStripTextBox,
            this._replaceToolStripSplitButton,
            this._testStateToolStripLabel});
			this._bottomToolStrip.Location = new System.Drawing.Point(0, 0);
			this._bottomToolStrip.Name = "_bottomToolStrip";
			this._bottomToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this._bottomToolStrip.Size = new System.Drawing.Size(784, 25);
			this._bottomToolStrip.Stretch = true;
			this._bottomToolStrip.TabIndex = 0;
			this._bottomToolStrip.TabStop = true;
			// 
			// _findToolStripLabel
			// 
			this._findToolStripLabel.Name = "_findToolStripLabel";
			this._findToolStripLabel.Size = new System.Drawing.Size(30, 22);
			this._findToolStripLabel.Text = "&Find";
			// 
			// _findToolStripTextBox
			// 
			this._findToolStripTextBox.AcceptsReturn = true;
			this._findToolStripTextBox.Name = "_findToolStripTextBox";
			this._findToolStripTextBox.Size = new System.Drawing.Size(200, 25);
			this._findToolStripTextBox.ToolTipText = "Find";
			this._findToolStripTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
			// 
			// _findToolStripButton
			// 
			this._findToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this._findToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_findToolStripButton.Image")));
			this._findToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._findToolStripButton.Name = "_findToolStripButton";
			this._findToolStripButton.Size = new System.Drawing.Size(23, 22);
			this._findToolStripButton.Text = "Find next (F3)";
			this._findToolStripButton.Click += new System.EventHandler(this.FindButton_Click);
			// 
			// _replaceToolStripTextBox
			// 
			this._replaceToolStripTextBox.AcceptsReturn = true;
			this._replaceToolStripTextBox.Name = "_replaceToolStripTextBox";
			this._replaceToolStripTextBox.Size = new System.Drawing.Size(200, 25);
			this._replaceToolStripTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
			// 
			// _replaceToolStripSplitButton
			// 
			this._replaceToolStripSplitButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._replaceAllToolStripMenuItem,
            this.replaceInSelectionToolStripMenuItem});
			this._replaceToolStripSplitButton.Image = ((System.Drawing.Image)(resources.GetObject("_replaceToolStripSplitButton.Image")));
			this._replaceToolStripSplitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._replaceToolStripSplitButton.Name = "_replaceToolStripSplitButton";
			this._replaceToolStripSplitButton.Size = new System.Drawing.Size(143, 22);
			this._replaceToolStripSplitButton.Text = "&Replace in selection";
			this._replaceToolStripSplitButton.ButtonClick += new System.EventHandler(this.ReplaceSelectionButton_Click);
			// 
			// _replaceAllToolStripMenuItem
			// 
			this._replaceAllToolStripMenuItem.Name = "_replaceAllToolStripMenuItem";
			this._replaceAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
			this._replaceAllToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
			this._replaceAllToolStripMenuItem.Text = "Replace &all";
			this._replaceAllToolStripMenuItem.Click += new System.EventHandler(this.ReplaceAllButton_Click);
			// 
			// replaceInSelectionToolStripMenuItem
			// 
			this.replaceInSelectionToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.replaceInSelectionToolStripMenuItem.Name = "replaceInSelectionToolStripMenuItem";
			this.replaceInSelectionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R)));
			this.replaceInSelectionToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
			this.replaceInSelectionToolStripMenuItem.Text = "&Replace in selection";
			this.replaceInSelectionToolStripMenuItem.Click += new System.EventHandler(this.ReplaceSelectionButton_Click);
			// 
			// _testStateToolStripLabel
			// 
			this._testStateToolStripLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this._testStateToolStripLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this._testStateToolStripLabel.Name = "_testStateToolStripLabel";
			this._testStateToolStripLabel.Size = new System.Drawing.Size(90, 22);
			this._testStateToolStripLabel.Text = "(no test loaded)";
			this._testStateToolStripLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// TopToolStripPanel
			// 
			this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
			this.TopToolStripPanel.Name = "TopToolStripPanel";
			this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
			// 
			// _topToolStrip
			// 
			this._topToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this._topToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._workItemToolStripLabel,
            this._workItemIdToolStripComboBox,
            this._loadToolStripButton,
            this._saveToolStripButton,
            this._closeToolStripButton,
            this.toolStripSeparator1,
            this._insertStepToolStripButton,
            this._deleteStepToolStripButton,
            this._changeProjectToolStripButton,
            this._connStateToolStripLabel});
			this._topToolStrip.Location = new System.Drawing.Point(0, 0);
			this._topToolStrip.Name = "_topToolStrip";
			this._topToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this._topToolStrip.Size = new System.Drawing.Size(784, 25);
			this._topToolStrip.Stretch = true;
			this._topToolStrip.TabIndex = 0;
			this._topToolStrip.TabStop = true;
			// 
			// _workItemToolStripLabel
			// 
			this._workItemToolStripLabel.Name = "_workItemToolStripLabel";
			this._workItemToolStripLabel.Size = new System.Drawing.Size(62, 22);
			this._workItemToolStripLabel.Text = "&Work Item";
			// 
			// _workItemIdToolStripComboBox
			// 
			this._workItemIdToolStripComboBox.Name = "_workItemIdToolStripComboBox";
			this._workItemIdToolStripComboBox.Size = new System.Drawing.Size(121, 25);
			this._workItemIdToolStripComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
			// 
			// _loadToolStripButton
			// 
			this._loadToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_loadToolStripButton.Image")));
			this._loadToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._loadToolStripButton.Name = "_loadToolStripButton";
			this._loadToolStripButton.Size = new System.Drawing.Size(53, 22);
			this._loadToolStripButton.Text = "&Load";
			this._loadToolStripButton.Click += new System.EventHandler(this.LoadButton_Click);
			// 
			// _saveToolStripButton
			// 
			this._saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_saveToolStripButton.Image")));
			this._saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._saveToolStripButton.Name = "_saveToolStripButton";
			this._saveToolStripButton.Size = new System.Drawing.Size(51, 22);
			this._saveToolStripButton.Text = "&Save";
			this._saveToolStripButton.Click += new System.EventHandler(this.SaveButton_Click);
			// 
			// _closeToolStripButton
			// 
			this._closeToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_closeToolStripButton.Image")));
			this._closeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._closeToolStripButton.Name = "_closeToolStripButton";
			this._closeToolStripButton.Size = new System.Drawing.Size(56, 22);
			this._closeToolStripButton.Text = "Clos&e";
			this._closeToolStripButton.Click += new System.EventHandler(this.CloseCurentButton_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// _insertStepToolStripButton
			// 
			this._insertStepToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_insertStepToolStripButton.Image")));
			this._insertStepToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._insertStepToolStripButton.Name = "_insertStepToolStripButton";
			this._insertStepToolStripButton.Size = new System.Drawing.Size(82, 22);
			this._insertStepToolStripButton.Text = "&Insert Step";
			this._insertStepToolStripButton.Click += new System.EventHandler(this.InsertButton_Click);
			// 
			// _deleteStepToolStripButton
			// 
			this._deleteStepToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_deleteStepToolStripButton.Image")));
			this._deleteStepToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._deleteStepToolStripButton.Name = "_deleteStepToolStripButton";
			this._deleteStepToolStripButton.Size = new System.Drawing.Size(86, 22);
			this._deleteStepToolStripButton.Text = "&Delete Step";
			this._deleteStepToolStripButton.Click += new System.EventHandler(this.DeleteButton_Click);
			// 
			// _changeProjectToolStripButton
			// 
			this._changeProjectToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this._changeProjectToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_changeProjectToolStripButton.Image")));
			this._changeProjectToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._changeProjectToolStripButton.Name = "_changeProjectToolStripButton";
			this._changeProjectToolStripButton.Size = new System.Drawing.Size(108, 22);
			this._changeProjectToolStripButton.Text = "&Change Project";
			this._changeProjectToolStripButton.Click += new System.EventHandler(this.ChangeProjectButton_Click);
			// 
			// _connStateToolStripLabel
			// 
			this._connStateToolStripLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this._connStateToolStripLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this._connStateToolStripLabel.Name = "_connStateToolStripLabel";
			this._connStateToolStripLabel.Size = new System.Drawing.Size(96, 22);
			this._connStateToolStripLabel.Text = "(not connected)";
			// 
			// RightToolStripPanel
			// 
			this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
			this.RightToolStripPanel.Name = "RightToolStripPanel";
			this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
			// 
			// LeftToolStripPanel
			// 
			this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
			this.LeftToolStripPanel.Name = "LeftToolStripPanel";
			this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
			// 
			// ContentPanel
			// 
			this.ContentPanel.Size = new System.Drawing.Size(772, 387);
			// 
			// _toolStripContainer
			// 
			// 
			// _toolStripContainer.BottomToolStripPanel
			// 
			this._toolStripContainer.BottomToolStripPanel.Controls.Add(this._bottomToolStrip);
			// 
			// _toolStripContainer.ContentPanel
			// 
			this._toolStripContainer.ContentPanel.Controls.Add(this._testTabControl);
			this._toolStripContainer.ContentPanel.Size = new System.Drawing.Size(784, 412);
			this._toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this._toolStripContainer.LeftToolStripPanelVisible = false;
			this._toolStripContainer.Location = new System.Drawing.Point(0, 0);
			this._toolStripContainer.Name = "_toolStripContainer";
			this._toolStripContainer.RightToolStripPanelVisible = false;
			this._toolStripContainer.Size = new System.Drawing.Size(784, 462);
			this._toolStripContainer.TabIndex = 15;
			this._toolStripContainer.Text = "toolStripContainer1";
			// 
			// _toolStripContainer.TopToolStripPanel
			// 
			this._toolStripContainer.TopToolStripPanel.Controls.Add(this._topToolStrip);
			// 
			// _testTabControl
			// 
			this._testTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this._testTabControl.ItemSize = new System.Drawing.Size(0, 20);
			this._testTabControl.Location = new System.Drawing.Point(0, 0);
			this._testTabControl.Multiline = true;
			this._testTabControl.Name = "_testTabControl";
			this._testTabControl.SelectedIndex = 0;
			this._testTabControl.Size = new System.Drawing.Size(784, 412);
			this._testTabControl.TabIndex = 0;
			// 
			// _loadTestBackgroundWorker
			// 
			this._loadTestBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.LoadTestBackgroundWorker_DoWork);
			this._loadTestBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.LoadTestBackgroundWorker_RunWorkerCompleted);
			// 
			// _saveTestBackgroundWorker
			// 
			this._saveTestBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.SaveTestBackgroundWorker_DoWork);
			this._saveTestBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.SaveTestBackgroundWorker_RunWorkerCompleted);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 462);
			this.Controls.Add(this._toolStripContainer);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MinimumSize = new System.Drawing.Size(800, 500);
			this.Name = "MainForm";
			this.Text = "Test Steps Editor";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Closing);
			this._testGridContextMenu.ResumeLayout(false);
			this._bottomToolStrip.ResumeLayout(false);
			this._bottomToolStrip.PerformLayout();
			this._topToolStrip.ResumeLayout(false);
			this._topToolStrip.PerformLayout();
			this._toolStripContainer.BottomToolStripPanel.ResumeLayout(false);
			this._toolStripContainer.BottomToolStripPanel.PerformLayout();
			this._toolStripContainer.ContentPanel.ResumeLayout(false);
			this._toolStripContainer.TopToolStripPanel.ResumeLayout(false);
			this._toolStripContainer.TopToolStripPanel.PerformLayout();
			this._toolStripContainer.ResumeLayout(false);
			this._toolStripContainer.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip _testGridContextMenu;
		private System.Windows.Forms.ToolStripMenuItem _copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem _pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
		private System.Windows.Forms.ToolStrip _bottomToolStrip;
		private System.Windows.Forms.ToolStripTextBox _findToolStripTextBox;
		private System.Windows.Forms.ToolStripButton _findToolStripButton;
		private System.Windows.Forms.ToolStripTextBox _replaceToolStripTextBox;
		private System.Windows.Forms.ToolStripSplitButton _replaceToolStripSplitButton;
		private System.Windows.Forms.ToolStripMenuItem _replaceAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripLabel _testStateToolStripLabel;
		private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
		private System.Windows.Forms.ToolStrip _topToolStrip;
		private System.Windows.Forms.ToolStripLabel _workItemToolStripLabel;
		private System.Windows.Forms.ToolStripButton _loadToolStripButton;
		private System.Windows.Forms.ToolStripButton _saveToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton _insertStepToolStripButton;
		private System.Windows.Forms.ToolStripButton _deleteStepToolStripButton;
		private System.Windows.Forms.ToolStripLabel _connStateToolStripLabel;
		private System.Windows.Forms.ToolStripButton _changeProjectToolStripButton;
		private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
		private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
		private System.Windows.Forms.ToolStripContentPanel ContentPanel;
		private System.Windows.Forms.ToolStripContainer _toolStripContainer;
		private System.ComponentModel.BackgroundWorker _loadTestBackgroundWorker;
		private System.ComponentModel.BackgroundWorker _saveTestBackgroundWorker;
		private System.Windows.Forms.ToolStripLabel _findToolStripLabel;
		private System.Windows.Forms.ToolStripMenuItem replaceInSelectionToolStripMenuItem;
		private System.Windows.Forms.TabControl _testTabControl;
		private System.Windows.Forms.ToolStripComboBox _workItemIdToolStripComboBox;
		private System.Windows.Forms.ToolStripButton _closeToolStripButton;
	}
}

