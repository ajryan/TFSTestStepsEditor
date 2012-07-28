namespace TestStepsEditor.Gui
{
	partial class TestEditUserControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestEditUserControl));
            this._splitContainer = new TestStepsEditor.Gui.SplitContainerEx();
            this._moveDownButton = new System.Windows.Forms.Button();
            this._moveUpButton = new System.Windows.Forms.Button();
            this._deleteButton = new System.Windows.Forms.Button();
            this._captureButton = new System.Windows.Forms.Button();
            this._attachmentListView = new TestStepsEditor.Gui.ListViewEx();
            this._attachmentImageList = new System.Windows.Forms.ImageList(this.components);
            this._toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).BeginInit();
            this._splitContainer.Panel2.SuspendLayout();
            this._splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // _splitContainer
            // 
            this._splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer.Location = new System.Drawing.Point(0, 0);
            this._splitContainer.Name = "_splitContainer";
            this._splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _splitContainer.Panel2
            // 
            this._splitContainer.Panel2.Controls.Add(this._moveDownButton);
            this._splitContainer.Panel2.Controls.Add(this._moveUpButton);
            this._splitContainer.Panel2.Controls.Add(this._deleteButton);
            this._splitContainer.Panel2.Controls.Add(this._captureButton);
            this._splitContainer.Panel2.Controls.Add(this._attachmentListView);
            this._splitContainer.Panel2MinSize = 0;
            this._splitContainer.Size = new System.Drawing.Size(703, 470);
            this._splitContainer.SplitterDistance = 357;
            this._splitContainer.SplitterWidth = 10;
            this._splitContainer.TabIndex = 1;
            // 
            // _moveDownButton
            // 
            this._moveDownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._moveDownButton.Image = ((System.Drawing.Image)(resources.GetObject("_moveDownButton.Image")));
            this._moveDownButton.Location = new System.Drawing.Point(660, 69);
            this._moveDownButton.Name = "_moveDownButton";
            this._moveDownButton.Size = new System.Drawing.Size(36, 23);
            this._moveDownButton.TabIndex = 4;
            this._moveDownButton.UseVisualStyleBackColor = true;
            this._moveDownButton.Click += new System.EventHandler(this.MoveDownButton_Click);
            // 
            // _moveUpButton
            // 
            this._moveUpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._moveUpButton.Image = ((System.Drawing.Image)(resources.GetObject("_moveUpButton.Image")));
            this._moveUpButton.Location = new System.Drawing.Point(621, 69);
            this._moveUpButton.Name = "_moveUpButton";
            this._moveUpButton.Size = new System.Drawing.Size(36, 23);
            this._moveUpButton.TabIndex = 3;
            this._moveUpButton.UseVisualStyleBackColor = true;
            this._moveUpButton.Click += new System.EventHandler(this.MoveUpButton_Click);
            // 
            // _deleteButton
            // 
            this._deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._deleteButton.Location = new System.Drawing.Point(621, 40);
            this._deleteButton.Name = "_deleteButton";
            this._deleteButton.Size = new System.Drawing.Size(75, 23);
            this._deleteButton.TabIndex = 2;
            this._deleteButton.Text = "Delete";
            this._toolTip.SetToolTip(this._deleteButton, "Delete the currently selected screenshot(s)");
            this._deleteButton.UseVisualStyleBackColor = true;
            this._deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // _captureButton
            // 
            this._captureButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._captureButton.Location = new System.Drawing.Point(621, 11);
            this._captureButton.Name = "_captureButton";
            this._captureButton.Size = new System.Drawing.Size(75, 23);
            this._captureButton.TabIndex = 1;
            this._captureButton.Text = "&Capture";
            this._toolTip.SetToolTip(this._captureButton, "Save the image in the clipboard as a validation screenshot.");
            this._captureButton.UseVisualStyleBackColor = true;
            this._captureButton.Click += new System.EventHandler(this.CaptureButton_Click);
            // 
            // _attachmentListView
            // 
            this._attachmentListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._attachmentListView.LargeImageList = this._attachmentImageList;
            this._attachmentListView.Location = new System.Drawing.Point(0, 3);
            this._attachmentListView.Name = "_attachmentListView";
            this._attachmentListView.Size = new System.Drawing.Size(615, 91);
            this._attachmentListView.SmallImageList = this._attachmentImageList;
            this._attachmentListView.TabIndex = 0;
            this._attachmentListView.UseCompatibleStateImageBehavior = false;
            this._attachmentListView.View = System.Windows.Forms.View.Tile;
            this._attachmentListView.SelectedIndexChanged += new System.EventHandler(this.AttachmentListView_SelectedIndexChanged);
            this._attachmentListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.AttachmentListView_MouseDoubleClick);
            // 
            // _attachmentImageList
            // 
            this._attachmentImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this._attachmentImageList.ImageSize = new System.Drawing.Size(32, 32);
            this._attachmentImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // TestEditUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._splitContainer);
            this.Name = "TestEditUserControl";
            this.Size = new System.Drawing.Size(703, 470);
            this._splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).EndInit();
            this._splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private SplitContainerEx _splitContainer;
		private ListViewEx _attachmentListView;
		private System.Windows.Forms.Button _captureButton;
		private System.Windows.Forms.ImageList _attachmentImageList;
		private System.Windows.Forms.Button _deleteButton;
		private System.Windows.Forms.ToolTip _toolTip;
		private System.Windows.Forms.Button _moveDownButton;
		private System.Windows.Forms.Button _moveUpButton;
	}
}
