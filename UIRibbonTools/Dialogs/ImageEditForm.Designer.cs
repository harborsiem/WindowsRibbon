namespace UIRibbonTools
{
    partial class ImageEditForm
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
            if (disposing)
            {
                Destroy();
            }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageEditForm));
            this.PaintBox = new System.Windows.Forms.PictureBox();
            this.topRightLayout = new System.Windows.Forms.TableLayoutPanel();
            this.Label2 = new System.Windows.Forms.Label();
            this.EditImageFile = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.ComboBoxMinDpi = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.EditResourceId = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.EditSymbol = new System.Windows.Forms.TextBox();
            this.RightButton = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.MemoHelp = new System.Windows.Forms.TextBox();
            this.buttonsPanel = new System.Windows.Forms.TableLayoutPanel();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.dialogLayout = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.PaintBox)).BeginInit();
            this.topRightLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EditResourceId)).BeginInit();
            this.buttonsPanel.SuspendLayout();
            this.dialogLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // PaintBox
            // 
            this.PaintBox.BackColor = System.Drawing.Color.Transparent;
            this.PaintBox.Location = new System.Drawing.Point(0, 0);
            this.PaintBox.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.PaintBox.Name = "PaintBox";
            this.PaintBox.Size = new System.Drawing.Size(64, 64);
            this.PaintBox.TabIndex = 2;
            this.PaintBox.TabStop = false;
            // 
            // topRightLayout
            // 
            this.topRightLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.topRightLayout.AutoSize = true;
            this.topRightLayout.ColumnCount = 3;
            this.topRightLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.topRightLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.topRightLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.topRightLayout.Controls.Add(this.Label2, 0, 0);
            this.topRightLayout.Controls.Add(this.EditImageFile, 1, 0);
            this.topRightLayout.Controls.Add(this.Label3, 0, 1);
            this.topRightLayout.Controls.Add(this.ComboBoxMinDpi, 1, 1);
            this.topRightLayout.Controls.Add(this.label6, 0, 2);
            this.topRightLayout.Controls.Add(this.EditResourceId, 1, 2);
            this.topRightLayout.Controls.Add(this.label7, 0, 3);
            this.topRightLayout.Controls.Add(this.EditSymbol, 1, 3);
            this.topRightLayout.Controls.Add(this.RightButton, 2, 0);
            this.topRightLayout.Location = new System.Drawing.Point(67, 0);
            this.topRightLayout.Margin = new System.Windows.Forms.Padding(0);
            this.topRightLayout.Name = "topRightLayout";
            this.topRightLayout.RowCount = 4;
            this.topRightLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.topRightLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.topRightLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.topRightLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.topRightLayout.Size = new System.Drawing.Size(359, 105);
            this.topRightLayout.TabIndex = 2;
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(3, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(55, 26);
            this.Label2.TabIndex = 0;
            this.Label2.Text = "Image File";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EditImageFile
            // 
            this.EditImageFile.Location = new System.Drawing.Point(135, 3);
            this.EditImageFile.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.EditImageFile.Name = "EditImageFile";
            this.EditImageFile.ReadOnly = true;
            this.EditImageFile.Size = new System.Drawing.Size(200, 20);
            this.EditImageFile.TabIndex = 1;
            // 
            // Label3
            // 
            this.Label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(3, 26);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(126, 27);
            this.Label3.TabIndex = 3;
            this.Label3.Text = "Minimum target resolution";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ComboBoxMinDpi
            // 
            this.ComboBoxMinDpi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxMinDpi.FormattingEnabled = true;
            this.ComboBoxMinDpi.Items.AddRange(new object[] {
            "auto",
            "96 dpi",
            "120 dpi",
            "144 dpi",
            "192 dpi"});
            this.ComboBoxMinDpi.Location = new System.Drawing.Point(135, 29);
            this.ComboBoxMinDpi.Name = "ComboBoxMinDpi";
            this.ComboBoxMinDpi.Size = new System.Drawing.Size(121, 21);
            this.ComboBoxMinDpi.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 26);
            this.label6.TabIndex = 5;
            this.label6.Text = "ID";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EditResourceId
            // 
            this.EditResourceId.Location = new System.Drawing.Point(135, 56);
            this.EditResourceId.Maximum = new decimal(new int[] {
            59999,
            0,
            0,
            0});
            this.EditResourceId.Name = "EditResourceId";
            this.EditResourceId.Size = new System.Drawing.Size(120, 20);
            this.EditResourceId.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 26);
            this.label7.TabIndex = 7;
            this.label7.Text = "Symbol";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EditSymbol
            // 
            this.EditSymbol.Location = new System.Drawing.Point(135, 82);
            this.EditSymbol.Name = "EditSymbol";
            this.EditSymbol.Size = new System.Drawing.Size(120, 20);
            this.EditSymbol.TabIndex = 8;
            // 
            // RightButton
            // 
            this.RightButton.Location = new System.Drawing.Point(335, 3);
            this.RightButton.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.RightButton.Name = "RightButton";
            this.RightButton.Size = new System.Drawing.Size(20, 20);
            this.RightButton.TabIndex = 2;
            this.RightButton.UseVisualStyleBackColor = true;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(0, 105);
            this.Label1.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(58, 13);
            this.Label1.TabIndex = 3;
            this.Label1.Text = "Quick Tips";
            // 
            // MemoHelp
            // 
            this.MemoHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MemoHelp.BackColor = System.Drawing.SystemColors.Info;
            this.dialogLayout.SetColumnSpan(this.MemoHelp, 2);
            this.MemoHelp.Location = new System.Drawing.Point(0, 121);
            this.MemoHelp.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.MemoHelp.MaximumSize = new System.Drawing.Size(500, 203);
            this.MemoHelp.Multiline = true;
            this.MemoHelp.Name = "MemoHelp";
            this.MemoHelp.ReadOnly = true;
            this.MemoHelp.Size = new System.Drawing.Size(426, 203);
            this.MemoHelp.TabIndex = 1;
            this.MemoHelp.Text = resources.GetString("MemoHelp.Text");
            this.MemoHelp.WordWrap = false;
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonsPanel.AutoSize = true;
            this.buttonsPanel.ColumnCount = 2;
            this.buttonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.buttonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.buttonsPanel.Controls.Add(this.buttonOk, 0, 0);
            this.buttonsPanel.Controls.Add(this.buttonCancel, 1, 0);
            this.buttonsPanel.Location = new System.Drawing.Point(264, 327);
            this.buttonsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.RowCount = 1;
            this.buttonsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.buttonsPanel.Size = new System.Drawing.Size(162, 29);
            this.buttonsPanel.TabIndex = 0;
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(3, 3);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(84, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // dialogLayout
            // 
            this.dialogLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dialogLayout.AutoSize = true;
            this.dialogLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dialogLayout.ColumnCount = 2;
            this.dialogLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.dialogLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.dialogLayout.Controls.Add(this.PaintBox, 0, 0);
            this.dialogLayout.Controls.Add(this.topRightLayout, 1, 0);
            this.dialogLayout.Controls.Add(this.Label1, 0, 1);
            this.dialogLayout.Controls.Add(this.MemoHelp, 0, 2);
            this.dialogLayout.Controls.Add(this.buttonsPanel, 1, 3);
            this.dialogLayout.Location = new System.Drawing.Point(9, 9);
            this.dialogLayout.Margin = new System.Windows.Forms.Padding(0);
            this.dialogLayout.Name = "dialogLayout";
            this.dialogLayout.RowCount = 4;
            this.dialogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dialogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dialogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dialogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dialogLayout.Size = new System.Drawing.Size(426, 356);
            this.dialogLayout.TabIndex = 0;
            // 
            // ImageEditForm
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(444, 374);
            this.Controls.Add(this.dialogLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImageEditForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Image";
            ((System.ComponentModel.ISupportInitialize)(this.PaintBox)).EndInit();
            this.topRightLayout.ResumeLayout(false);
            this.topRightLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EditResourceId)).EndInit();
            this.buttonsPanel.ResumeLayout(false);
            this.dialogLayout.ResumeLayout(false);
            this.dialogLayout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.TextBox MemoHelp;
        private System.Windows.Forms.PictureBox PaintBox;
        private System.Windows.Forms.TableLayoutPanel topRightLayout;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.TextBox EditImageFile;
        private System.Windows.Forms.Label Label3;
        private System.Windows.Forms.ComboBox ComboBoxMinDpi;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown EditResourceId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox EditSymbol;
        private System.Windows.Forms.Button RightButton;
        private System.Windows.Forms.TableLayoutPanel buttonsPanel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TableLayoutPanel dialogLayout;
    }
}
