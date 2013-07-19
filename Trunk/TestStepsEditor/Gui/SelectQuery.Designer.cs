namespace TestStepsEditor.Gui
{
    partial class SelectQuery
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
            this._queryListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this._runQueryButton = new System.Windows.Forms.Button();
            this._closeButton = new System.Windows.Forms.Button();
            this._queryTreeView = new System.Windows.Forms.TreeView();
            this._showTreeViewRadioButton = new System.Windows.Forms.RadioButton();
            this._showFlatListRadioButton = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // _queryListBox
            // 
            this._queryListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._queryListBox.DisplayMember = "Name";
            this._queryListBox.FormattingEnabled = true;
            this._queryListBox.Location = new System.Drawing.Point(13, 39);
            this._queryListBox.Name = "_queryListBox";
            this._queryListBox.Size = new System.Drawing.Size(335, 277);
            this._queryListBox.TabIndex = 0;
            this._queryListBox.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select a query to execute:";
            // 
            // _runQueryButton
            // 
            this._runQueryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._runQueryButton.Location = new System.Drawing.Point(13, 322);
            this._runQueryButton.Name = "_runQueryButton";
            this._runQueryButton.Size = new System.Drawing.Size(335, 23);
            this._runQueryButton.TabIndex = 2;
            this._runQueryButton.Text = "Run Selected Query and Show Test Case List";
            this._runQueryButton.UseVisualStyleBackColor = true;
            this._runQueryButton.Click += new System.EventHandler(this.RunQueryButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._closeButton.Location = new System.Drawing.Point(13, 351);
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(81, 23);
            this._closeButton.TabIndex = 3;
            this._closeButton.Text = "Close";
            this._closeButton.UseVisualStyleBackColor = true;
            this._closeButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // _queryTreeView
            // 
            this._queryTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._queryTreeView.Location = new System.Drawing.Point(13, 39);
            this._queryTreeView.Name = "_queryTreeView";
            this._queryTreeView.Size = new System.Drawing.Size(335, 277);
            this._queryTreeView.TabIndex = 5;
            // 
            // _showTreeViewRadioButton
            // 
            this._showTreeViewRadioButton.AutoSize = true;
            this._showTreeViewRadioButton.Checked = true;
            this._showTreeViewRadioButton.Location = new System.Drawing.Point(208, 11);
            this._showTreeViewRadioButton.Name = "_showTreeViewRadioButton";
            this._showTreeViewRadioButton.Size = new System.Drawing.Size(73, 17);
            this._showTreeViewRadioButton.TabIndex = 6;
            this._showTreeViewRadioButton.TabStop = true;
            this._showTreeViewRadioButton.Text = "Tree View";
            this._showTreeViewRadioButton.UseVisualStyleBackColor = true;
            this._showTreeViewRadioButton.CheckedChanged += new System.EventHandler(this.TreeViewRadioButton_CheckedChanged);
            // 
            // _showFlatListRadioButton
            // 
            this._showFlatListRadioButton.AutoSize = true;
            this._showFlatListRadioButton.Location = new System.Drawing.Point(287, 11);
            this._showFlatListRadioButton.Name = "_showFlatListRadioButton";
            this._showFlatListRadioButton.Size = new System.Drawing.Size(61, 17);
            this._showFlatListRadioButton.TabIndex = 7;
            this._showFlatListRadioButton.TabStop = true;
            this._showFlatListRadioButton.Text = "Flat List";
            this._showFlatListRadioButton.UseVisualStyleBackColor = true;
            // 
            // SelectQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 383);
            this.ControlBox = false;
            this.Controls.Add(this._showFlatListRadioButton);
            this.Controls.Add(this._showTreeViewRadioButton);
            this.Controls.Add(this._queryTreeView);
            this.Controls.Add(this._closeButton);
            this.Controls.Add(this._runQueryButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._queryListBox);
            this.Name = "SelectQuery";
            this.Text = "QueryPicker";
            this.Load += new System.EventHandler(this.QueryPicker_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox _queryListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _runQueryButton;
        private System.Windows.Forms.Button _closeButton;
        private System.Windows.Forms.TreeView _queryTreeView;
        private System.Windows.Forms.RadioButton _showTreeViewRadioButton;
        private System.Windows.Forms.RadioButton _showFlatListRadioButton;
    }
}