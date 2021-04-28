namespace UIRibbonTools
{
    partial class ConvertImageForm
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
            this.inSelectorLayout = new System.Windows.Forms.TableLayoutPanel();
            this.inSelectorLabel = new System.Windows.Forms.Label();
            this.inSelectorCombo = new System.Windows.Forms.ComboBox();
            this.inIgnoreAlphaCheck = new System.Windows.Forms.CheckBox();
            this.inFilesLabel = new System.Windows.Forms.Label();
            this.inSelectButton = new System.Windows.Forms.Button();
            this.inPathLabel = new System.Windows.Forms.Label();
            this.inPathTextBox = new System.Windows.Forms.TextBox();
            this.inSelectorGroup = new System.Windows.Forms.GroupBox();
            this.outSelectorLayout = new System.Windows.Forms.TableLayoutPanel();
            this.outSelectorLabel = new System.Windows.Forms.Label();
            this.outCombo = new System.Windows.Forms.ComboBox();
            this.outIgnoreAlphaCheck = new System.Windows.Forms.CheckBox();
            this.outFolderLabel = new System.Windows.Forms.Label();
            this.outSelectButton = new System.Windows.Forms.Button();
            this.outPathLabel = new System.Windows.Forms.Label();
            this.outPathTextBox = new System.Windows.Forms.TextBox();
            this.outSelectorGroup = new System.Windows.Forms.GroupBox();
            this.convertButton = new System.Windows.Forms.Button();
            this.dialogLayout = new System.Windows.Forms.TableLayoutPanel();
            this.inSelectorLayout.SuspendLayout();
            this.inSelectorGroup.SuspendLayout();
            this.outSelectorLayout.SuspendLayout();
            this.outSelectorGroup.SuspendLayout();
            this.dialogLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // inSelectorLayout
            // 
            this.inSelectorLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inSelectorLayout.AutoSize = true;
            this.inSelectorLayout.ColumnCount = 2;
            this.inSelectorLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.inSelectorLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.inSelectorLayout.Controls.Add(this.inSelectorLabel, 0, 0);
            this.inSelectorLayout.Controls.Add(this.inSelectorCombo, 1, 0);
            this.inSelectorLayout.Controls.Add(this.inIgnoreAlphaCheck, 1, 1);
            this.inSelectorLayout.Controls.Add(this.inFilesLabel, 0, 2);
            this.inSelectorLayout.Controls.Add(this.inSelectButton, 1, 2);
            this.inSelectorLayout.Controls.Add(this.inPathLabel, 0, 3);
            this.inSelectorLayout.Controls.Add(this.inPathTextBox, 1, 3);
            this.inSelectorLayout.Location = new System.Drawing.Point(3, 16);
            this.inSelectorLayout.Margin = new System.Windows.Forms.Padding(0);
            this.inSelectorLayout.Name = "inSelectorLayout";
            this.inSelectorLayout.RowCount = 4;
            this.inSelectorLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.inSelectorLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.inSelectorLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.inSelectorLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.inSelectorLayout.Size = new System.Drawing.Size(297, 105);
            this.inSelectorLayout.TabIndex = 0;
            // 
            // inSelectorLabel
            // 
            this.inSelectorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.inSelectorLabel.AutoSize = true;
            this.inSelectorLabel.Location = new System.Drawing.Point(3, 0);
            this.inSelectorLabel.Name = "inSelectorLabel";
            this.inSelectorLabel.Size = new System.Drawing.Size(36, 27);
            this.inSelectorLabel.TabIndex = 0;
            this.inSelectorLabel.Text = "Inputs";
            this.inSelectorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // inSelectorCombo
            // 
            this.inSelectorCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inSelectorCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.inSelectorCombo.FormattingEnabled = true;
            this.inSelectorCombo.Items.AddRange(new object[] {
            "Show Bitmap Infos (*.bmp, *.png, *.ico)",
            "Bitmap (*.bmp, *.png, ...)",
            "Icon",
            "Standard-Icons.com Gif",
            "VS-ImageLib XAML"});
            this.inSelectorCombo.Location = new System.Drawing.Point(45, 3);
            this.inSelectorCombo.Name = "inSelectorCombo";
            this.inSelectorCombo.Size = new System.Drawing.Size(249, 21);
            this.inSelectorCombo.TabIndex = 1;
            // 
            // inIgnoreAlphaCheck
            // 
            this.inIgnoreAlphaCheck.AutoSize = true;
            this.inIgnoreAlphaCheck.Location = new System.Drawing.Point(45, 30);
            this.inIgnoreAlphaCheck.Name = "inIgnoreAlphaCheck";
            this.inIgnoreAlphaCheck.Size = new System.Drawing.Size(128, 17);
            this.inIgnoreAlphaCheck.TabIndex = 3;
            this.inIgnoreAlphaCheck.Text = "Ignore Alpha Channel";
            this.inIgnoreAlphaCheck.UseVisualStyleBackColor = true;
            // 
            // inFilesLabel
            // 
            this.inFilesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.inFilesLabel.AutoSize = true;
            this.inFilesLabel.Location = new System.Drawing.Point(3, 50);
            this.inFilesLabel.Name = "inFilesLabel";
            this.inFilesLabel.Size = new System.Drawing.Size(28, 29);
            this.inFilesLabel.TabIndex = 2;
            this.inFilesLabel.Text = "Files";
            this.inFilesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // inSelectButton
            // 
            this.inSelectButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inSelectButton.Location = new System.Drawing.Point(45, 53);
            this.inSelectButton.Name = "inSelectButton";
            this.inSelectButton.Size = new System.Drawing.Size(249, 23);
            this.inSelectButton.TabIndex = 4;
            this.inSelectButton.Text = "Select";
            this.inSelectButton.UseVisualStyleBackColor = true;
            // 
            // inPathLabel
            // 
            this.inPathLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.inPathLabel.AutoSize = true;
            this.inPathLabel.Location = new System.Drawing.Point(3, 79);
            this.inPathLabel.Name = "inPathLabel";
            this.inPathLabel.Size = new System.Drawing.Size(29, 26);
            this.inPathLabel.TabIndex = 7;
            this.inPathLabel.Text = "Path";
            this.inPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // inPathTextBox
            // 
            this.inPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inPathTextBox.Location = new System.Drawing.Point(45, 82);
            this.inPathTextBox.Name = "inPathTextBox";
            this.inPathTextBox.ReadOnly = true;
            this.inPathTextBox.Size = new System.Drawing.Size(249, 20);
            this.inPathTextBox.TabIndex = 8;
            // 
            // inSelectorGroup
            // 
            this.inSelectorGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inSelectorGroup.AutoSize = true;
            this.inSelectorGroup.Controls.Add(this.inSelectorLayout);
            this.inSelectorGroup.Location = new System.Drawing.Point(3, 3);
            this.inSelectorGroup.Name = "inSelectorGroup";
            this.inSelectorGroup.Size = new System.Drawing.Size(303, 137);
            this.inSelectorGroup.TabIndex = 0;
            this.inSelectorGroup.TabStop = false;
            this.inSelectorGroup.Text = "Input Selector";
            // 
            // outSelectorLayout
            // 
            this.outSelectorLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outSelectorLayout.AutoSize = true;
            this.outSelectorLayout.ColumnCount = 2;
            this.outSelectorLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.outSelectorLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.outSelectorLayout.Controls.Add(this.outSelectorLabel, 0, 0);
            this.outSelectorLayout.Controls.Add(this.outCombo, 1, 0);
            this.outSelectorLayout.Controls.Add(this.outIgnoreAlphaCheck, 1, 1);
            this.outSelectorLayout.Controls.Add(this.outFolderLabel, 0, 2);
            this.outSelectorLayout.Controls.Add(this.outSelectButton, 1, 2);
            this.outSelectorLayout.Controls.Add(this.outPathLabel, 0, 3);
            this.outSelectorLayout.Controls.Add(this.outPathTextBox, 1, 3);
            this.outSelectorLayout.Location = new System.Drawing.Point(3, 16);
            this.outSelectorLayout.Margin = new System.Windows.Forms.Padding(0);
            this.outSelectorLayout.Name = "outSelectorLayout";
            this.outSelectorLayout.RowCount = 4;
            this.outSelectorLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.outSelectorLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.outSelectorLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.outSelectorLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.outSelectorLayout.Size = new System.Drawing.Size(298, 105);
            this.outSelectorLayout.TabIndex = 0;
            // 
            // outSelectorLabel
            // 
            this.outSelectorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.outSelectorLabel.AutoSize = true;
            this.outSelectorLabel.Location = new System.Drawing.Point(3, 0);
            this.outSelectorLabel.Name = "outSelectorLabel";
            this.outSelectorLabel.Size = new System.Drawing.Size(44, 27);
            this.outSelectorLabel.TabIndex = 0;
            this.outSelectorLabel.Text = "Outputs";
            this.outSelectorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // outCombo
            // 
            this.outCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.outCombo.FormattingEnabled = true;
            this.outCombo.Items.AddRange(new object[] {
            "Bitmap V5",
            "Bitmap",
            "Png"});
            this.outCombo.Location = new System.Drawing.Point(53, 3);
            this.outCombo.Name = "outCombo";
            this.outCombo.Size = new System.Drawing.Size(242, 21);
            this.outCombo.TabIndex = 1;
            // 
            // outIgnoreAlphaCheck
            // 
            this.outIgnoreAlphaCheck.AutoSize = true;
            this.outIgnoreAlphaCheck.Location = new System.Drawing.Point(53, 30);
            this.outIgnoreAlphaCheck.Name = "outIgnoreAlphaCheck";
            this.outIgnoreAlphaCheck.Size = new System.Drawing.Size(128, 17);
            this.outIgnoreAlphaCheck.TabIndex = 3;
            this.outIgnoreAlphaCheck.Text = "Ignore Alpha Channel";
            this.outIgnoreAlphaCheck.UseVisualStyleBackColor = true;
            // 
            // outFolderLabel
            // 
            this.outFolderLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.outFolderLabel.AutoSize = true;
            this.outFolderLabel.Location = new System.Drawing.Point(3, 50);
            this.outFolderLabel.Name = "outFolderLabel";
            this.outFolderLabel.Size = new System.Drawing.Size(36, 29);
            this.outFolderLabel.TabIndex = 2;
            this.outFolderLabel.Text = "Folder";
            this.outFolderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // outSelectButton
            // 
            this.outSelectButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outSelectButton.Location = new System.Drawing.Point(53, 53);
            this.outSelectButton.Name = "outSelectButton";
            this.outSelectButton.Size = new System.Drawing.Size(242, 23);
            this.outSelectButton.TabIndex = 5;
            this.outSelectButton.Text = "Select";
            this.outSelectButton.UseVisualStyleBackColor = true;
            // 
            // outPathLabel
            // 
            this.outPathLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.outPathLabel.AutoSize = true;
            this.outPathLabel.Location = new System.Drawing.Point(3, 79);
            this.outPathLabel.Name = "outPathLabel";
            this.outPathLabel.Size = new System.Drawing.Size(29, 26);
            this.outPathLabel.TabIndex = 6;
            this.outPathLabel.Text = "Path";
            this.outPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // outPathTextBox
            // 
            this.outPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outPathTextBox.Location = new System.Drawing.Point(53, 82);
            this.outPathTextBox.Name = "outPathTextBox";
            this.outPathTextBox.ReadOnly = true;
            this.outPathTextBox.Size = new System.Drawing.Size(242, 20);
            this.outPathTextBox.TabIndex = 7;
            // 
            // outSelectorGroup
            // 
            this.outSelectorGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outSelectorGroup.AutoSize = true;
            this.outSelectorGroup.Controls.Add(this.outSelectorLayout);
            this.outSelectorGroup.Location = new System.Drawing.Point(312, 3);
            this.outSelectorGroup.Name = "outSelectorGroup";
            this.outSelectorGroup.Size = new System.Drawing.Size(304, 137);
            this.outSelectorGroup.TabIndex = 1;
            this.outSelectorGroup.TabStop = false;
            this.outSelectorGroup.Text = "Output Selector";
            // 
            // convertButton
            // 
            this.convertButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.convertButton.Location = new System.Drawing.Point(541, 146);
            this.convertButton.Name = "convertButton";
            this.convertButton.Size = new System.Drawing.Size(75, 23);
            this.convertButton.TabIndex = 2;
            this.convertButton.Text = "Convert";
            this.convertButton.UseVisualStyleBackColor = true;
            // 
            // dialogLayout
            // 
            this.dialogLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dialogLayout.AutoSize = true;
            this.dialogLayout.ColumnCount = 2;
            this.dialogLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.dialogLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.dialogLayout.Controls.Add(this.inSelectorGroup, 0, 0);
            this.dialogLayout.Controls.Add(this.outSelectorGroup, 1, 0);
            this.dialogLayout.Controls.Add(this.convertButton, 1, 1);
            this.dialogLayout.Location = new System.Drawing.Point(9, 9);
            this.dialogLayout.Margin = new System.Windows.Forms.Padding(0);
            this.dialogLayout.Name = "dialogLayout";
            this.dialogLayout.RowCount = 2;
            this.dialogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dialogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dialogLayout.Size = new System.Drawing.Size(619, 172);
            this.dialogLayout.TabIndex = 2;
            // 
            // ConvertImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 190);
            this.Controls.Add(this.dialogLayout);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(2048, 229);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(653, 229);
            this.Name = "ConvertImageForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Image Converter";
            this.inSelectorLayout.ResumeLayout(false);
            this.inSelectorLayout.PerformLayout();
            this.inSelectorGroup.ResumeLayout(false);
            this.inSelectorGroup.PerformLayout();
            this.outSelectorLayout.ResumeLayout(false);
            this.outSelectorLayout.PerformLayout();
            this.outSelectorGroup.ResumeLayout(false);
            this.outSelectorGroup.PerformLayout();
            this.dialogLayout.ResumeLayout(false);
            this.dialogLayout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox inSelectorGroup;
        private System.Windows.Forms.TableLayoutPanel inSelectorLayout;
        private System.Windows.Forms.Label inSelectorLabel;
        private System.Windows.Forms.ComboBox inSelectorCombo;
        private System.Windows.Forms.Label inFilesLabel;
        private System.Windows.Forms.CheckBox inIgnoreAlphaCheck;
        private System.Windows.Forms.GroupBox outSelectorGroup;
        private System.Windows.Forms.TableLayoutPanel outSelectorLayout;
        private System.Windows.Forms.Label outSelectorLabel;
        private System.Windows.Forms.ComboBox outCombo;
        private System.Windows.Forms.Label outFolderLabel;
        private System.Windows.Forms.CheckBox outIgnoreAlphaCheck;
        private System.Windows.Forms.TableLayoutPanel dialogLayout;
        private System.Windows.Forms.Button inSelectButton;
        private System.Windows.Forms.Button outSelectButton;
        private System.Windows.Forms.TextBox inPathTextBox;
        private System.Windows.Forms.Label inPathLabel;
        private System.Windows.Forms.Label outPathLabel;
        private System.Windows.Forms.TextBox outPathTextBox;
        private System.Windows.Forms.Button convertButton;
    }
}

