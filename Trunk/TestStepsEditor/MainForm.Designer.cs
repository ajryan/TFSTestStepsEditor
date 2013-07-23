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
			this._toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this._deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this._insertAboveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._insertBelowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
			this._findToolStrip = new System.Windows.Forms.ToolStrip();
			this._findToolStripLabel = new System.Windows.Forms.ToolStripLabel();
			this._findToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
			this._findToolStripButton = new System.Windows.Forms.ToolStripButton();
			this._replaceToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
			this._replaceToolStripSplitButton = new System.Windows.Forms.ToolStripSplitButton();
			this._replaceAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.replaceInSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._testStateToolStripLabel = new System.Windows.Forms.ToolStripLabel();
			this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
			this._witToolStrip = new System.Windows.Forms.ToolStrip();
			this._workItemToolStripLabel = new System.Windows.Forms.ToolStripLabel();
			this._workItemIdToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
			this._loadToolStripButton = new System.Windows.Forms.ToolStripButton();
			this._reloadCurrentTestCaseToolStripButton = new System.Windows.Forms.ToolStripButton();
			this._saveToolStripButton = new System.Windows.Forms.ToolStripButton();
			this._closeToolStripButton = new System.Windows.Forms.ToolStripButton();
			this._selectQueryToolStripButton = new System.Windows.Forms.ToolStripButton();
			this._resultsToolStripButton = new System.Windows.Forms.ToolStripDropDownButton();
			this._enableResultsModeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._publishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._loadResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._saveCurrentResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._clearResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this._insertStepToolStripSplitButton = new System.Windows.Forms.ToolStripSplitButton();
			this.insertStepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.insertStepBelowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._deleteStepToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this._changeProjectToolStripButton = new System.Windows.Forms.ToolStripButton();
			this._stringGeneratorToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
			this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
			this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
			this._toolStripContainer = new System.Windows.Forms.ToolStripContainer();
			this._testTabControl = new System.Windows.Forms.TabControl();
			this._loadTestBackgroundWorker = new System.ComponentModel.BackgroundWorker();
			this._saveTestBackgroundWorker = new System.ComponentModel.BackgroundWorker();
			this._publishTestBackgroundWorker = new System.ComponentModel.BackgroundWorker();
			this._testGridContextMenu.SuspendLayout();
			this._findToolStrip.SuspendLayout();
			this._witToolStrip.SuspendLayout();
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
            this._pasteToolStripMenuItem,
            this._toolStripSeparator1,
            this._deleteToolStripMenuItem,
            this._toolStripSeparator2,
            this._insertAboveToolStripMenuItem,
            this._insertBelowToolStripMenuItem});
			this._testGridContextMenu.Name = "_testGridContextMenu";
			this._testGridContextMenu.Size = new System.Drawing.Size(162, 126);
			// 
			// _copyToolStripMenuItem
			// 
			this._copyToolStripMenuItem.Name = "_copyToolStripMenuItem";
			this._copyToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this._copyToolStripMenuItem.Text = "&Copy";
			this._copyToolStripMenuItem.Click += new System.EventHandler(this.TestGridContext_Copy_Click);
			// 
			// _pasteToolStripMenuItem
			// 
			this._pasteToolStripMenuItem.Name = "_pasteToolStripMenuItem";
			this._pasteToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this._pasteToolStripMenuItem.Text = "&Paste";
			this._pasteToolStripMenuItem.Click += new System.EventHandler(this.TestGridContext_Paste_Click);
			// 
			// _toolStripSeparator1
			// 
			this._toolStripSeparator1.Name = "_toolStripSeparator1";
			this._toolStripSeparator1.Size = new System.Drawing.Size(158, 6);
			// 
			// _deleteToolStripMenuItem
			// 
			this._deleteToolStripMenuItem.Name = "_deleteToolStripMenuItem";
			this._deleteToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this._deleteToolStripMenuItem.Text = "&Delete";
			this._deleteToolStripMenuItem.Click += new System.EventHandler(this.TestGridContext_Delete_Click);
			// 
			// _toolStripSeparator2
			// 
			this._toolStripSeparator2.Name = "_toolStripSeparator2";
			this._toolStripSeparator2.Size = new System.Drawing.Size(158, 6);
			// 
			// _insertAboveToolStripMenuItem
			// 
			this._insertAboveToolStripMenuItem.Name = "_insertAboveToolStripMenuItem";
			this._insertAboveToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this._insertAboveToolStripMenuItem.Text = "Insert &Above";
			this._insertAboveToolStripMenuItem.Click += new System.EventHandler(this.TestGridContext_InsertAbove_Click);
			// 
			// _insertBelowToolStripMenuItem
			// 
			this._insertBelowToolStripMenuItem.Name = "_insertBelowToolStripMenuItem";
			this._insertBelowToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this._insertBelowToolStripMenuItem.Text = "Insert &Below";
			this._insertBelowToolStripMenuItem.Click += new System.EventHandler(this.TestGrid_InsertBelow_Click);
			// 
			// BottomToolStripPanel
			// 
			this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
			this.BottomToolStripPanel.Name = "BottomToolStripPanel";
			this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
			// 
			// _findToolStrip
			// 
			this._findToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this._findToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._findToolStripLabel,
            this._findToolStripTextBox,
            this._findToolStripButton,
            this._replaceToolStripTextBox,
            this._replaceToolStripSplitButton,
            this._testStateToolStripLabel});
			this._findToolStrip.Location = new System.Drawing.Point(0, 0);
			this._findToolStrip.Name = "_findToolStrip";
			this._findToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this._findToolStrip.Size = new System.Drawing.Size(1180, 25);
			this._findToolStrip.Stretch = true;
			this._findToolStrip.TabIndex = 0;
			this._findToolStrip.TabStop = true;
			// 
			// _findToolStripLabel
			// 
			this._findToolStripLabel.Name = "_findToolStripLabel";
			this._findToolStripLabel.Size = new System.Drawing.Size(34, 22);
			this._findToolStripLabel.Text = "&Find";
			this._findToolStripLabel.ToolTipText = "Enter a string to search through the current test case. Not case sensitive.";
			// 
			// _findToolStripTextBox
			// 
			this._findToolStripTextBox.AcceptsReturn = true;
			this._findToolStripTextBox.Name = "_findToolStripTextBox";
			this._findToolStripTextBox.Size = new System.Drawing.Size(200, 25);
			this._findToolStripTextBox.ToolTipText = "Enter a string to search through the current test case. Not case sensitive.";
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
			this._findToolStripButton.ToolTipText = "Find the next match (F3)";
			this._findToolStripButton.Click += new System.EventHandler(this.FindButton_Click);
			// 
			// _replaceToolStripTextBox
			// 
			this._replaceToolStripTextBox.AcceptsReturn = true;
			this._replaceToolStripTextBox.Name = "_replaceToolStripTextBox";
			this._replaceToolStripTextBox.Size = new System.Drawing.Size(200, 25);
			this._replaceToolStripTextBox.ToolTipText = "Enter a string to replace the find string. Replace searches are case sensitive.";
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
			this._replaceToolStripSplitButton.Size = new System.Drawing.Size(165, 22);
			this._replaceToolStripSplitButton.Text = "&Replace in selection";
			this._replaceToolStripSplitButton.ToolTipText = "Replace all Find matches in the selected cells with the Replace string. (Alt + R)" +
    "";
			this._replaceToolStripSplitButton.ButtonClick += new System.EventHandler(this.ReplaceSelectionButton_Click);
			// 
			// _replaceAllToolStripMenuItem
			// 
			this._replaceAllToolStripMenuItem.Name = "_replaceAllToolStripMenuItem";
			this._replaceAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
			this._replaceAllToolStripMenuItem.Size = new System.Drawing.Size(265, 24);
			this._replaceAllToolStripMenuItem.Text = "Replace &all";
			this._replaceAllToolStripMenuItem.ToolTipText = "Replace all Find matches in the entire test case with the Replace string. (Alt + " +
    "A)";
			this._replaceAllToolStripMenuItem.Click += new System.EventHandler(this.ReplaceAllButton_Click);
			// 
			// replaceInSelectionToolStripMenuItem
			// 
			this.replaceInSelectionToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.replaceInSelectionToolStripMenuItem.Name = "replaceInSelectionToolStripMenuItem";
			this.replaceInSelectionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R)));
			this.replaceInSelectionToolStripMenuItem.Size = new System.Drawing.Size(265, 24);
			this.replaceInSelectionToolStripMenuItem.Text = "&Replace in selection";
			this.replaceInSelectionToolStripMenuItem.ToolTipText = "Replace all Find matches in the selected cells with the Replace string. (Alt + R)" +
    "";
			this.replaceInSelectionToolStripMenuItem.Click += new System.EventHandler(this.ReplaceSelectionButton_Click);
			// 
			// _testStateToolStripLabel
			// 
			this._testStateToolStripLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this._testStateToolStripLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this._testStateToolStripLabel.Name = "_testStateToolStripLabel";
			this._testStateToolStripLabel.Size = new System.Drawing.Size(113, 22);
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
			// _witToolStrip
			// 
			this._witToolStrip.AutoSize = false;
			this._witToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this._witToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._workItemToolStripLabel,
            this._workItemIdToolStripComboBox,
            this._loadToolStripButton,
            this._reloadCurrentTestCaseToolStripButton,
            this._saveToolStripButton,
            this._closeToolStripButton,
            this._selectQueryToolStripButton,
            this._resultsToolStripButton,
            this.toolStripSeparator2,
            this._insertStepToolStripSplitButton,
            this._deleteStepToolStripButton,
            this.toolStripSeparator1,
            this._changeProjectToolStripButton,
            this._stringGeneratorToolStripButton});
			this._witToolStrip.Location = new System.Drawing.Point(0, 0);
			this._witToolStrip.Name = "_witToolStrip";
			this._witToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this._witToolStrip.Size = new System.Drawing.Size(1180, 27);
			this._witToolStrip.Stretch = true;
			this._witToolStrip.TabIndex = 0;
			this._witToolStrip.TabStop = true;
			// 
			// _workItemToolStripLabel
			// 
			this._workItemToolStripLabel.Name = "_workItemToolStripLabel";
			this._workItemToolStripLabel.Size = new System.Drawing.Size(79, 24);
			this._workItemToolStripLabel.Text = "&Work Item";
			// 
			// _workItemIdToolStripComboBox
			// 
			this._workItemIdToolStripComboBox.Name = "_workItemIdToolStripComboBox";
			this._workItemIdToolStripComboBox.Size = new System.Drawing.Size(121, 27);
			this._workItemIdToolStripComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
			// 
			// _loadToolStripButton
			// 
			this._loadToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_loadToolStripButton.Image")));
			this._loadToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._loadToolStripButton.Name = "_loadToolStripButton";
			this._loadToolStripButton.Size = new System.Drawing.Size(59, 24);
			this._loadToolStripButton.Text = "&Load";
			this._loadToolStripButton.ToolTipText = "Load a test case from TFS for edit. Multiple test cases are opened in their own t" +
    "abs.";
			this._loadToolStripButton.Click += new System.EventHandler(this.LoadButton_Click);
			// 
			// _reloadCurrentTestCaseToolStripButton
			// 
			this._reloadCurrentTestCaseToolStripButton.Image = global::TestStepsEditor.Properties.Resources.arrow_refresh;
			this._reloadCurrentTestCaseToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._reloadCurrentTestCaseToolStripButton.Name = "_reloadCurrentTestCaseToolStripButton";
			this._reloadCurrentTestCaseToolStripButton.Size = new System.Drawing.Size(71, 24);
			this._reloadCurrentTestCaseToolStripButton.Text = "&Reload";
			this._reloadCurrentTestCaseToolStripButton.ToolTipText = "Reloads a the original steps of the currently selected test case";
			this._reloadCurrentTestCaseToolStripButton.Click += new System.EventHandler(this.RefreshCurrentTestCaseToolStripButton_Click);
			// 
			// _saveToolStripButton
			// 
			this._saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_saveToolStripButton.Image")));
			this._saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._saveToolStripButton.Name = "_saveToolStripButton";
			this._saveToolStripButton.Size = new System.Drawing.Size(60, 24);
			this._saveToolStripButton.Text = "&Save";
			this._saveToolStripButton.ToolTipText = "Save the test case in the current tab (Alt + S)";
			this._saveToolStripButton.Click += new System.EventHandler(this.SaveButton_Click);
			// 
			// _closeToolStripButton
			// 
			this._closeToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_closeToolStripButton.Image")));
			this._closeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._closeToolStripButton.Name = "_closeToolStripButton";
			this._closeToolStripButton.Size = new System.Drawing.Size(62, 24);
			this._closeToolStripButton.Text = "Clos&e";
			this._closeToolStripButton.ToolTipText = "Close the test case in the current tab. Will prompt if there are unsaved changes." +
    "";
			this._closeToolStripButton.Click += new System.EventHandler(this.CloseCurentButton_Click);
			// 
			// _selectQueryToolStripButton
			// 
			this._selectQueryToolStripButton.Image = global::TestStepsEditor.Properties.Resources.QueryIcon;
			this._selectQueryToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._selectQueryToolStripButton.Name = "_selectQueryToolStripButton";
			this._selectQueryToolStripButton.Size = new System.Drawing.Size(111, 24);
			this._selectQueryToolStripButton.Text = "Select &Query";
			this._selectQueryToolStripButton.ToolTipText = "Select Test Case Query";
			this._selectQueryToolStripButton.Click += new System.EventHandler(this.SelectQueryToolStripButton_Click);
			// 
			// _resultsToolStripButton
			// 
			this._resultsToolStripButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._enableResultsModeMenuItem,
            this._publishToolStripMenuItem,
            this._loadResultsToolStripMenuItem,
            this._saveCurrentResultsToolStripMenuItem,
            this._clearResultsToolStripMenuItem});
			this._resultsToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_resultsToolStripButton.Image")));
			this._resultsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._resultsToolStripButton.Name = "_resultsToolStripButton";
			this._resultsToolStripButton.Size = new System.Drawing.Size(83, 24);
			this._resultsToolStripButton.Text = "Results";
			// 
			// _enableResultsModeMenuItem
			// 
			this._enableResultsModeMenuItem.CheckOnClick = true;
			this._enableResultsModeMenuItem.Name = "_enableResultsModeMenuItem";
			this._enableResultsModeMenuItem.Size = new System.Drawing.Size(289, 24);
			this._enableResultsModeMenuItem.Text = "Enable Results Mode";
			this._enableResultsModeMenuItem.CheckedChanged += new System.EventHandler(this.EnableResultsModeToolStripMenuItem_CheckedChanged);
			// 
			// _publishToolStripMenuItem
			// 
			this._publishToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F);
			this._publishToolStripMenuItem.Name = "_publishToolStripMenuItem";
			this._publishToolStripMenuItem.Size = new System.Drawing.Size(289, 24);
			this._publishToolStripMenuItem.Text = "Publish to TFS";
			this._publishToolStripMenuItem.ToolTipText = "Publish the current test result to TFS, including outcomes and screenshots.";
			this._publishToolStripMenuItem.Click += new System.EventHandler(this.PublishButton_Click);
			// 
			// _loadResultsToolStripMenuItem
			// 
			this._loadResultsToolStripMenuItem.Name = "_loadResultsToolStripMenuItem";
			this._loadResultsToolStripMenuItem.Size = new System.Drawing.Size(289, 24);
			this._loadResultsToolStripMenuItem.Text = "Load results from .ZIP";
			this._loadResultsToolStripMenuItem.ToolTipText = "Load all outcomes and screenshots from a .ZIP file. Does not affect step titles o" +
    "r expected results.";
			this._loadResultsToolStripMenuItem.Click += new System.EventHandler(this.LoadResultsButton_Click);
			// 
			// _saveCurrentResultsToolStripMenuItem
			// 
			this._saveCurrentResultsToolStripMenuItem.Name = "_saveCurrentResultsToolStripMenuItem";
			this._saveCurrentResultsToolStripMenuItem.Size = new System.Drawing.Size(289, 24);
			this._saveCurrentResultsToolStripMenuItem.Text = "Save current results to .ZIP";
			this._saveCurrentResultsToolStripMenuItem.ToolTipText = "Save the current outcomes and screenshots to a .ZIP file that may be loaded later" +
    ".";
			this._saveCurrentResultsToolStripMenuItem.Click += new System.EventHandler(this.SaveCurrentResultsButton_Click);
			// 
			// _clearResultsToolStripMenuItem
			// 
			this._clearResultsToolStripMenuItem.Name = "_clearResultsToolStripMenuItem";
			this._clearResultsToolStripMenuItem.Size = new System.Drawing.Size(289, 24);
			this._clearResultsToolStripMenuItem.Text = "Clear outcomes and screenshots";
			this._clearResultsToolStripMenuItem.ToolTipText = "Reset all outcomes to \"inconclusive\" and remove all screenshots. A .ZIP of the cu" +
    "rrent results will be automatically saved to your desktop.";
			this._clearResultsToolStripMenuItem.Click += new System.EventHandler(this.ClearResultsToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
			// 
			// _insertStepToolStripSplitButton
			// 
			this._insertStepToolStripSplitButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertStepToolStripMenuItem,
            this.insertStepBelowToolStripMenuItem});
			this._insertStepToolStripSplitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._insertStepToolStripSplitButton.Name = "_insertStepToolStripSplitButton";
			this._insertStepToolStripSplitButton.Size = new System.Drawing.Size(97, 24);
			this._insertStepToolStripSplitButton.Text = "&Insert Step";
			this._insertStepToolStripSplitButton.ToolTipText = "Insert a step above the current step.";
			this._insertStepToolStripSplitButton.ButtonClick += new System.EventHandler(this.InsertButton_Click);
			// 
			// insertStepToolStripMenuItem
			// 
			this.insertStepToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.insertStepToolStripMenuItem.Name = "insertStepToolStripMenuItem";
			this.insertStepToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.I)));
			this.insertStepToolStripMenuItem.Size = new System.Drawing.Size(235, 24);
			this.insertStepToolStripMenuItem.Text = "&Insert Step";
			this.insertStepToolStripMenuItem.ToolTipText = "Insert a step above the current step.";
			this.insertStepToolStripMenuItem.Click += new System.EventHandler(this.InsertButton_Click);
			// 
			// insertStepBelowToolStripMenuItem
			// 
			this.insertStepBelowToolStripMenuItem.Name = "insertStepBelowToolStripMenuItem";
			this.insertStepBelowToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.B)));
			this.insertStepBelowToolStripMenuItem.Size = new System.Drawing.Size(235, 24);
			this.insertStepBelowToolStripMenuItem.Text = "Insert Step &Below";
			this.insertStepBelowToolStripMenuItem.ToolTipText = "Insert a step below the current step.";
			this.insertStepBelowToolStripMenuItem.Click += new System.EventHandler(this.InsertBelowButton_Click);
			// 
			// _deleteStepToolStripButton
			// 
			this._deleteStepToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_deleteStepToolStripButton.Image")));
			this._deleteStepToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._deleteStepToolStripButton.Name = "_deleteStepToolStripButton";
			this._deleteStepToolStripButton.Size = new System.Drawing.Size(103, 24);
			this._deleteStepToolStripButton.Text = "&Delete Step";
			this._deleteStepToolStripButton.ToolTipText = "Delete the currently-selected step.";
			this._deleteStepToolStripButton.Click += new System.EventHandler(this.DeleteButton_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
			// 
			// _changeProjectToolStripButton
			// 
			this._changeProjectToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this._changeProjectToolStripButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this._changeProjectToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_changeProjectToolStripButton.Image")));
			this._changeProjectToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._changeProjectToolStripButton.Name = "_changeProjectToolStripButton";
			this._changeProjectToolStripButton.Size = new System.Drawing.Size(134, 24);
			this._changeProjectToolStripButton.Text = "&Change Project";
			this._changeProjectToolStripButton.Click += new System.EventHandler(this.ChangeProjectButton_Click);
			// 
			// _stringGeneratorToolStripButton
			// 
			this._stringGeneratorToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_stringGeneratorToolStripButton.Image")));
			this._stringGeneratorToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._stringGeneratorToolStripButton.Name = "_stringGeneratorToolStripButton";
			this._stringGeneratorToolStripButton.Size = new System.Drawing.Size(134, 24);
			this._stringGeneratorToolStripButton.Text = "String &Generator";
			this._stringGeneratorToolStripButton.ToolTipText = "Put strings into the clipboard for pasting into a test case or application.";
			this._stringGeneratorToolStripButton.Click += new System.EventHandler(this.StringGeneratorButton_Click);
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
			this._toolStripContainer.BottomToolStripPanel.Controls.Add(this._findToolStrip);
			// 
			// _toolStripContainer.ContentPanel
			// 
			this._toolStripContainer.ContentPanel.Controls.Add(this._testTabControl);
			this._toolStripContainer.ContentPanel.Margin = new System.Windows.Forms.Padding(4);
			this._toolStripContainer.ContentPanel.Size = new System.Drawing.Size(1180, 571);
			this._toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this._toolStripContainer.LeftToolStripPanelVisible = false;
			this._toolStripContainer.Location = new System.Drawing.Point(0, 0);
			this._toolStripContainer.Margin = new System.Windows.Forms.Padding(4);
			this._toolStripContainer.Name = "_toolStripContainer";
			this._toolStripContainer.RightToolStripPanelVisible = false;
			this._toolStripContainer.Size = new System.Drawing.Size(1180, 623);
			this._toolStripContainer.TabIndex = 15;
			this._toolStripContainer.Text = "toolStripContainer1";
			// 
			// _toolStripContainer.TopToolStripPanel
			// 
			this._toolStripContainer.TopToolStripPanel.Controls.Add(this._witToolStrip);
			// 
			// _testTabControl
			// 
			this._testTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this._testTabControl.ItemSize = new System.Drawing.Size(0, 20);
			this._testTabControl.Location = new System.Drawing.Point(0, 0);
			this._testTabControl.Margin = new System.Windows.Forms.Padding(4);
			this._testTabControl.Multiline = true;
			this._testTabControl.Name = "_testTabControl";
			this._testTabControl.SelectedIndex = 0;
			this._testTabControl.Size = new System.Drawing.Size(1180, 571);
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
			// _publishTestBackgroundWorker
			// 
			this._publishTestBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.PublishTestBackgroundWorker_DoWork);
			this._publishTestBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.PublishTestBackgroundWorker_RunWorkerCompleted);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1180, 623);
			this.Controls.Add(this._toolStripContainer);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MinimumSize = new System.Drawing.Size(394, 606);
			this.Name = "MainForm";
			this.Text = "Test Steps Editor";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Closing);
			this._testGridContextMenu.ResumeLayout(false);
			this._findToolStrip.ResumeLayout(false);
			this._findToolStrip.PerformLayout();
			this._witToolStrip.ResumeLayout(false);
			this._witToolStrip.PerformLayout();
			this._toolStripContainer.BottomToolStripPanel.ResumeLayout(false);
			this._toolStripContainer.BottomToolStripPanel.PerformLayout();
			this._toolStripContainer.ContentPanel.ResumeLayout(false);
			this._toolStripContainer.TopToolStripPanel.ResumeLayout(false);
			this._toolStripContainer.ResumeLayout(false);
			this._toolStripContainer.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion
		private System.Windows.Forms.ContextMenuStrip _testGridContextMenu;
		private System.Windows.Forms.ToolStripMenuItem _copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem _pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
		private System.Windows.Forms.ToolStrip _findToolStrip;
		private System.Windows.Forms.ToolStripTextBox _findToolStripTextBox;
		private System.Windows.Forms.ToolStripButton _findToolStripButton;
		private System.Windows.Forms.ToolStripTextBox _replaceToolStripTextBox;
		private System.Windows.Forms.ToolStripSplitButton _replaceToolStripSplitButton;
		private System.Windows.Forms.ToolStripMenuItem _replaceAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripLabel _testStateToolStripLabel;
		private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
		private System.Windows.Forms.ToolStrip _witToolStrip;
		private System.Windows.Forms.ToolStripLabel _workItemToolStripLabel;
		private System.Windows.Forms.ToolStripButton _loadToolStripButton;
		private System.Windows.Forms.ToolStripButton _saveToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSplitButton _insertStepToolStripSplitButton;
		private System.Windows.Forms.ToolStripButton _deleteStepToolStripButton;
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
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton _stringGeneratorToolStripButton;
		private System.Windows.Forms.ToolStripMenuItem insertStepToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem insertStepBelowToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator _toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem _deleteToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator _toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem _insertAboveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem _insertBelowToolStripMenuItem;
		private System.ComponentModel.BackgroundWorker _publishTestBackgroundWorker;
		private System.Windows.Forms.ToolStripDropDownButton _resultsToolStripButton;
		private System.Windows.Forms.ToolStripMenuItem _loadResultsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem _saveCurrentResultsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem _publishToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem _clearResultsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem _enableResultsModeMenuItem;
		private System.Windows.Forms.ToolStripButton _selectQueryToolStripButton;
		private System.Windows.Forms.ToolStripButton _reloadCurrentTestCaseToolStripButton;
	}

}

