namespace UIRibbonTools
{
    partial class BaseFrame
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
            this._imageSample = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._blanks = new System.Windows.Forms.ToolStripLabel();
            this._labelHeader = new System.Windows.Forms.ToolStripLabel();
            this._layoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._panel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this._imageSample)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this._panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _imageSample
            // 
            //this._imageSample.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._imageSample.Location = new System.Drawing.Point(382, 3);
            //this._imageSample.Margin = new System.Windows.Forms.Padding(0);
            this._imageSample.Name = "_imageSample";
            this._imageSample.Size = new System.Drawing.Size(105, 105);
            this._imageSample.TabIndex = 0;
            this._imageSample.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._blanks,
            this._labelHeader});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(498, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _blanks
            // 
            this._blanks.Name = "_blanks";
            this._blanks.Size = new System.Drawing.Size(13, 22);
            this._blanks.Text = "  ";
            // 
            // _labelHeader
            // 
            this._labelHeader.Name = "_labelHeader";
            this._labelHeader.Size = new System.Drawing.Size(51, 22);
            this._labelHeader.Text = "  Header";
            // 
            // _layoutPanel
            // 
            this._layoutPanel.ColumnCount = 4;
            this._layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this._layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this._layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            this._layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this._layoutPanel.Location = new System.Drawing.Point(3, 3);
            this._layoutPanel.Name = "_layoutPanel";
            this._layoutPanel.RowCount = 14;
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layoutPanel.Size = new System.Drawing.Size(376, 209);
            this._layoutPanel.TabIndex = 0;
            // 
            // _panel
            // 
            this._panel.AutoScroll = true;
            this._panel.Controls.Add(this._layoutPanel);
            this._panel.Controls.Add(this._imageSample);
            this._panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panel.Location = new System.Drawing.Point(0, 25);
            this._panel.Name = "_panel";
            this._panel.Size = new System.Drawing.Size(498, 215);
            this._panel.TabIndex = 0;
            // 
            // TBaseFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._panel);
            this.Controls.Add(this.toolStrip1);
            this.Name = "TBaseFrame";
            this.Size = new System.Drawing.Size(498, 240);
            ((System.ComponentModel.ISupportInitialize)(this._imageSample)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this._panel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox _imageSample;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel _labelHeader;
        private System.Windows.Forms.TableLayoutPanel _layoutPanel;
        private System.Windows.Forms.Panel _panel;
        private System.Windows.Forms.ToolStripLabel _blanks;
    }
}
