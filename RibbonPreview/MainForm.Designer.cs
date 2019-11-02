namespace RibbonPreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildRibbonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extrasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previewRibbonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.openToolStrip = new System.Windows.Forms.ToolStripButton();
            this.openPreviewToolStrip = new System.Windows.Forms.ToolStripButton();
            this.buildToolStrip = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.previewToolStrip = new System.Windows.Forms.ToolStripButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.extrasToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(478, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.openPreviewToolStripMenuItem,
            this.buildRibbonToolStripMenuItem,
            this.toolStripSeparator2,
            this.helpToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.ToolTipText = "RibbonMarkup.xml";
            // 
            // openPreviewToolStripMenuItem
            // 
            this.openPreviewToolStripMenuItem.Name = "openPreviewToolStripMenuItem";
            this.openPreviewToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.openPreviewToolStripMenuItem.Text = "Open and Preview";
            this.openPreviewToolStripMenuItem.ToolTipText = "RibbonMarkup.xml";
            // 
            // buildRibbonToolStripMenuItem
            // 
            this.buildRibbonToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("buildRibbonToolStripMenuItem.Image")));
            this.buildRibbonToolStripMenuItem.Name = "buildRibbonToolStripMenuItem";
            this.buildRibbonToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.buildRibbonToolStripMenuItem.Text = "Build Ribbon";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(167, 6);
            this.toolStripSeparator2.Visible = false;
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Enabled = false;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Visible = false;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(167, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // extrasToolStripMenuItem
            // 
            this.extrasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.previewRibbonToolStripMenuItem});
            this.extrasToolStripMenuItem.Name = "extrasToolStripMenuItem";
            this.extrasToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.extrasToolStripMenuItem.Text = "Extras";
            // 
            // previewRibbonToolStripMenuItem
            // 
            this.previewRibbonToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("previewRibbonToolStripMenuItem.Image")));
            this.previewRibbonToolStripMenuItem.Name = "previewRibbonToolStripMenuItem";
            this.previewRibbonToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.previewRibbonToolStripMenuItem.Text = "Preview Ribbon";
            this.previewRibbonToolStripMenuItem.ToolTipText = "Preview Ribbon";
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStrip,
            this.openPreviewToolStrip,
            this.buildToolStrip,
            this.toolStripSeparator1,
            this.previewToolStrip});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(478, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip";
            // 
            // openToolStrip
            // 
            this.openToolStrip.Image = ((System.Drawing.Image)(resources.GetObject("openToolStrip.Image")));
            this.openToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStrip.Name = "openToolStrip";
            this.openToolStrip.Size = new System.Drawing.Size(56, 22);
            this.openToolStrip.Text = "Open";
            this.openToolStrip.ToolTipText = "Open RibbonMarkup.xml";
            // 
            // openPreviewToolStrip
            // 
            this.openPreviewToolStrip.Image = ((System.Drawing.Image)(resources.GetObject("openPreviewToolStrip.Image")));
            this.openPreviewToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openPreviewToolStrip.Name = "openPreviewToolStrip";
            this.openPreviewToolStrip.Size = new System.Drawing.Size(123, 22);
            this.openPreviewToolStrip.Text = "Open and Preview";
            this.openPreviewToolStrip.ToolTipText = "Open RibbonMarkup.xml and Preview";
            // 
            // buildToolStrip
            // 
            this.buildToolStrip.Image = ((System.Drawing.Image)(resources.GetObject("buildToolStrip.Image")));
            this.buildToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buildToolStrip.Name = "buildToolStrip";
            this.buildToolStrip.Size = new System.Drawing.Size(54, 22);
            this.buildToolStrip.Text = "Build";
            this.buildToolStrip.ToolTipText = "Build *.ribbon";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // previewToolStrip
            // 
            this.previewToolStrip.Image = ((System.Drawing.Image)(resources.GetObject("previewToolStrip.Image")));
            this.previewToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.previewToolStrip.Name = "previewToolStrip";
            this.previewToolStrip.Size = new System.Drawing.Size(68, 22);
            this.previewToolStrip.Text = "Preview";
            this.previewToolStrip.ToolTipText = "Preview Ribbon";
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 49);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(478, 401);
            this.textBox1.TabIndex = 2;
            this.textBox1.WordWrap = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 450);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildRibbonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extrasToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previewRibbonToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton openToolStrip;
        private System.Windows.Forms.ToolStripButton buildToolStrip;
        private System.Windows.Forms.ToolStripButton previewToolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem openPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton openPreviewToolStrip;
        private System.Windows.Forms.TextBox textBox1;
    }
}
