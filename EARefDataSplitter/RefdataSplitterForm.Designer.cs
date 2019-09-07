namespace EARefDataSplitter
{
    partial class RefDataSplitterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RefDataSplitterForm));
            this.browseRefDataFileButton = new System.Windows.Forms.Button();
            this.refdataTextBox = new System.Windows.Forms.TextBox();
            this.refdataFileLabel = new System.Windows.Forms.Label();
            this.refdataTreeView = new BrightIdeasSoftware.TreeListView();
            this.exportButton = new System.Windows.Forms.Button();
            this.AddIncludedScriptsCheckBox = new System.Windows.Forms.CheckBox();
            this.nameColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.scriptTreeImages = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.refdataTreeView)).BeginInit();
            this.SuspendLayout();
            // 
            // browseRefDataFileButton
            // 
            this.browseRefDataFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseRefDataFileButton.Location = new System.Drawing.Point(393, 29);
            this.browseRefDataFileButton.Name = "browseRefDataFileButton";
            this.browseRefDataFileButton.Size = new System.Drawing.Size(24, 20);
            this.browseRefDataFileButton.TabIndex = 15;
            this.browseRefDataFileButton.Text = "...";
            this.browseRefDataFileButton.UseVisualStyleBackColor = true;
            this.browseRefDataFileButton.Click += new System.EventHandler(this.browseRefDataFileButton_Click);
            // 
            // refdataTextBox
            // 
            this.refdataTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.refdataTextBox.Location = new System.Drawing.Point(12, 29);
            this.refdataTextBox.MinimumSize = new System.Drawing.Size(153, 20);
            this.refdataTextBox.Name = "refdataTextBox";
            this.refdataTextBox.Size = new System.Drawing.Size(375, 20);
            this.refdataTextBox.TabIndex = 14;
            this.refdataTextBox.TextChanged += new System.EventHandler(this.refdataTextBox_TextChanged);
            // 
            // refdataFileLabel
            // 
            this.refdataFileLabel.Location = new System.Drawing.Point(12, 9);
            this.refdataFileLabel.Name = "refdataFileLabel";
            this.refdataFileLabel.Size = new System.Drawing.Size(141, 23);
            this.refdataFileLabel.TabIndex = 16;
            this.refdataFileLabel.Text = "Refdata file";
            // 
            // refdataTreeView
            // 
            this.refdataTreeView.AllColumns.Add(this.nameColumn);
            this.refdataTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.refdataTreeView.CellEditUseWholeCell = false;
            this.refdataTreeView.CheckBoxes = true;
            this.refdataTreeView.CheckedAspectName = "selected";
            this.refdataTreeView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumn});
            this.refdataTreeView.Cursor = System.Windows.Forms.Cursors.Default;
            this.refdataTreeView.HideSelection = false;
            this.refdataTreeView.Location = new System.Drawing.Point(12, 66);
            this.refdataTreeView.Name = "refdataTreeView";
            this.refdataTreeView.ShowGroups = false;
            this.refdataTreeView.ShowImagesOnSubItems = true;
            this.refdataTreeView.Size = new System.Drawing.Size(405, 310);
            this.refdataTreeView.SmallImageList = this.scriptTreeImages;
            this.refdataTreeView.TabIndex = 17;
            this.refdataTreeView.TriStateCheckBoxes = true;
            this.refdataTreeView.UseCompatibleStateImageBehavior = false;
            this.refdataTreeView.View = System.Windows.Forms.View.Details;
            this.refdataTreeView.VirtualMode = true;
            // 
            // exportButton
            // 
            this.exportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exportButton.Location = new System.Drawing.Point(342, 387);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(75, 23);
            this.exportButton.TabIndex = 18;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // AddIncludedScriptsCheckBox
            // 
            this.AddIncludedScriptsCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AddIncludedScriptsCheckBox.AutoSize = true;
            this.AddIncludedScriptsCheckBox.Checked = true;
            this.AddIncludedScriptsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AddIncludedScriptsCheckBox.Location = new System.Drawing.Point(15, 391);
            this.AddIncludedScriptsCheckBox.Name = "AddIncludedScriptsCheckBox";
            this.AddIncludedScriptsCheckBox.Size = new System.Drawing.Size(121, 17);
            this.AddIncludedScriptsCheckBox.TabIndex = 19;
            this.AddIncludedScriptsCheckBox.Text = "Add included scripts";
            this.AddIncludedScriptsCheckBox.UseVisualStyleBackColor = true;
            this.AddIncludedScriptsCheckBox.CheckedChanged += new System.EventHandler(this.AddIncludedScriptsCheckBox_CheckedChanged);
            // 
            // nameColumn
            // 
            this.nameColumn.AspectName = "name";
            this.nameColumn.FillsFreeSpace = true;
            this.nameColumn.HeaderCheckBox = true;
            this.nameColumn.Text = "Scripts";
            // 
            // scriptTreeImages
            // 
            this.scriptTreeImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("scriptTreeImages.ImageStream")));
            this.scriptTreeImages.TransparentColor = System.Drawing.Color.Transparent;
            this.scriptTreeImages.Images.SetKeyName(0, "Script");
            this.scriptTreeImages.Images.SetKeyName(1, "ScriptGroup");
            // 
            // RefDataSplitterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 422);
            this.Controls.Add(this.AddIncludedScriptsCheckBox);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.refdataTreeView);
            this.Controls.Add(this.browseRefDataFileButton);
            this.Controls.Add(this.refdataTextBox);
            this.Controls.Add(this.refdataFileLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RefDataSplitterForm";
            this.Text = "Reference Data Splitter";
            ((System.ComponentModel.ISupportInitialize)(this.refdataTreeView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button browseRefDataFileButton;
        private System.Windows.Forms.TextBox refdataTextBox;
        private System.Windows.Forms.Label refdataFileLabel;
        private BrightIdeasSoftware.TreeListView refdataTreeView;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.CheckBox AddIncludedScriptsCheckBox;
        private BrightIdeasSoftware.OLVColumn nameColumn;
        private System.Windows.Forms.ImageList scriptTreeImages;
    }
}

