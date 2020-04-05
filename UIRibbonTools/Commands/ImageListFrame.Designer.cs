namespace UIRibbonTools
{
    partial class ImageListFrame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageListFrame));
            this.toolBarImages = new System.Windows.Forms.ToolStrip();
            this.toolButtonAddImage = new System.Windows.Forms.ToolStripDropDownButton();
            this.popupAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.popupAddMultiple = new System.Windows.Forms.ToolStripMenuItem();
            this.toolButtonRemoveImage = new System.Windows.Forms.ToolStripButton();
            this.toolButtonRemoveAllImages = new System.Windows.Forms.ToolStripButton();
            this.toolButtonEditImage = new System.Windows.Forms.ToolStripButton();
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewPanel = new System.Windows.Forms.Panel();
            this._imageList = new System.Windows.Forms.ImageList(this.components);
            this.toolBarImages.SuspendLayout();
            this.listViewPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolBarImages
            // 
            this.toolBarImages.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolButtonAddImage,
            this.toolButtonRemoveImage,
            this.toolButtonRemoveAllImages,
            this.toolButtonEditImage});
            this.toolBarImages.Location = new System.Drawing.Point(0, 0);
            this.toolBarImages.Name = "toolBarImages";
            this.toolBarImages.Size = new System.Drawing.Size(451, 25);
            this.toolBarImages.TabIndex = 0;
            // 
            // toolButtonAddImage
            // 
            this.toolButtonAddImage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.popupAdd,
            this.popupAddMultiple});
            this.toolButtonAddImage.Image = ((System.Drawing.Image)(resources.GetObject("toolButtonAddImage.Image")));
            this.toolButtonAddImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolButtonAddImage.Name = "toolButtonAddImage";
            this.toolButtonAddImage.Size = new System.Drawing.Size(58, 22);
            this.toolButtonAddImage.Text = "Add";
            // 
            // popupAdd
            // 
            this.popupAdd.Name = "popupAdd";
            this.popupAdd.Size = new System.Drawing.Size(180, 22);
            this.popupAdd.Text = "Add";
            // 
            // popupAddMultiple
            // 
            this.popupAddMultiple.Name = "popupAddMultiple";
            this.popupAddMultiple.Size = new System.Drawing.Size(180, 22);
            this.popupAddMultiple.Text = "Add Multiple";
            // 
            // toolButtonRemoveImage
            // 
            this.toolButtonRemoveImage.Image = ((System.Drawing.Image)(resources.GetObject("toolButtonRemoveImage.Image")));
            this.toolButtonRemoveImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolButtonRemoveImage.Name = "toolButtonRemoveImage";
            this.toolButtonRemoveImage.Size = new System.Drawing.Size(70, 22);
            this.toolButtonRemoveImage.Text = "Remove";
            // 
            // toolButtonRemoveAllImages
            // 
            this.toolButtonRemoveAllImages.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolButtonRemoveAllImages.Image = ((System.Drawing.Image)(resources.GetObject("toolButtonRemoveAllImages.Image")));
            this.toolButtonRemoveAllImages.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolButtonRemoveAllImages.Name = "toolButtonRemoveAllImages";
            this.toolButtonRemoveAllImages.Size = new System.Drawing.Size(71, 22);
            this.toolButtonRemoveAllImages.Text = "Remove All";
            // 
            // toolButtonEditImage
            // 
            this.toolButtonEditImage.Image = ((System.Drawing.Image)(resources.GetObject("toolButtonEditImage.Image")));
            this.toolButtonEditImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolButtonEditImage.Name = "toolButtonEditImage";
            this.toolButtonEditImage.Size = new System.Drawing.Size(47, 22);
            this.toolButtonEditImage.Text = "Edit";
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listView.FullRowSelect = true;
            this.listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.Margin = new System.Windows.Forms.Padding(0);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(451, 279);
            this.listView.TabIndex = 1;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 40;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "MinDPI";
            this.columnHeader2.Width = 51;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "ID";
            this.columnHeader3.Width = 40;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Symbol";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Source";
            this.columnHeader5.Width = 160;
            // 
            // listViewPanel
            // 
            this.listViewPanel.Controls.Add(this.listView);
            this.listViewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewPanel.Location = new System.Drawing.Point(0, 25);
            this.listViewPanel.Margin = new System.Windows.Forms.Padding(0);
            this.listViewPanel.Name = "listViewPanel";
            this.listViewPanel.Size = new System.Drawing.Size(451, 279);
            this.listViewPanel.TabIndex = 1;
            // 
            // _imageList
            // 
            this._imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this._imageList.ImageSize = new System.Drawing.Size(32, 32);
            this._imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ImageListFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listViewPanel);
            this.Controls.Add(this.toolBarImages);
            this.Name = "ImageListFrame";
            this.Size = new System.Drawing.Size(451, 304);
            this.toolBarImages.ResumeLayout(false);
            this.toolBarImages.PerformLayout();
            this.listViewPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolBarImages;
        private System.Windows.Forms.ToolStripDropDownButton toolButtonAddImage;
        private System.Windows.Forms.ToolStripButton toolButtonRemoveImage;
        private System.Windows.Forms.ToolStripButton toolButtonRemoveAllImages;
        private System.Windows.Forms.ToolStripButton toolButtonEditImage;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ImageList _imageList;
        private System.Windows.Forms.ToolStripMenuItem popupAdd;
        private System.Windows.Forms.ToolStripMenuItem popupAddMultiple;
        private System.Windows.Forms.Panel listViewPanel;
    }
}
