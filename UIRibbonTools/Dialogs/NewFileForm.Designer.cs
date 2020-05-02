namespace UIRibbonTools
{
    partial class NewFileForm
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
            this.templateGroupLayout = new System.Windows.Forms.TableLayoutPanel();
            this.emptyRadioButton = new System.Windows.Forms.RadioButton();
            this.wordPadRadioButton = new System.Windows.Forms.RadioButton();
            this.RadioGroupTemplate = new System.Windows.Forms.GroupBox();
            this.saveGroupLayout = new System.Windows.Forms.TableLayoutPanel();
            this.directoryButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.EditDirectory = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.EditFilename = new System.Windows.Forms.TextBox();
            this.GroupBoxPath = new System.Windows.Forms.GroupBox();
            this.buttons = new System.Windows.Forms.TableLayoutPanel();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.dialogLayout = new System.Windows.Forms.TableLayoutPanel();
            this.templateGroupLayout.SuspendLayout();
            this.RadioGroupTemplate.SuspendLayout();
            this.saveGroupLayout.SuspendLayout();
            this.GroupBoxPath.SuspendLayout();
            this.buttons.SuspendLayout();
            this.dialogLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // templateGroupLayout
            // 
            this.templateGroupLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.templateGroupLayout.AutoSize = true;
            this.templateGroupLayout.ColumnCount = 1;
            this.templateGroupLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.templateGroupLayout.Controls.Add(this.emptyRadioButton, 0, 0);
            this.templateGroupLayout.Controls.Add(this.wordPadRadioButton, 0, 1);
            this.templateGroupLayout.Location = new System.Drawing.Point(3, 19);
            this.templateGroupLayout.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.templateGroupLayout.Name = "templateGroupLayout";
            this.templateGroupLayout.RowCount = 2;
            this.templateGroupLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.templateGroupLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.templateGroupLayout.Size = new System.Drawing.Size(334, 46);
            this.templateGroupLayout.TabIndex = 0;
            // 
            // emptyRadioButton
            // 
            this.emptyRadioButton.AutoSize = true;
            this.emptyRadioButton.Checked = true;
            this.emptyRadioButton.Location = new System.Drawing.Point(3, 3);
            this.emptyRadioButton.Name = "emptyRadioButton";
            this.emptyRadioButton.Size = new System.Drawing.Size(206, 17);
            this.emptyRadioButton.TabIndex = 0;
            this.emptyRadioButton.TabStop = true;
            this.emptyRadioButton.Text = "Create a new empty Ribbon document";
            this.emptyRadioButton.UseVisualStyleBackColor = true;
            // 
            // wordPadRadioButton
            // 
            this.wordPadRadioButton.AutoSize = true;
            this.wordPadRadioButton.Location = new System.Drawing.Point(3, 26);
            this.wordPadRadioButton.Name = "wordPadRadioButton";
            this.wordPadRadioButton.Size = new System.Drawing.Size(312, 17);
            this.wordPadRadioButton.TabIndex = 1;
            this.wordPadRadioButton.Text = "Create a new Ribbon document using the WordPad template";
            this.wordPadRadioButton.UseVisualStyleBackColor = true;
            // 
            // RadioGroupTemplate
            // 
            this.RadioGroupTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RadioGroupTemplate.AutoSize = true;
            this.RadioGroupTemplate.Controls.Add(this.templateGroupLayout);
            this.RadioGroupTemplate.Location = new System.Drawing.Point(3, 3);
            this.RadioGroupTemplate.Name = "RadioGroupTemplate";
            this.RadioGroupTemplate.Size = new System.Drawing.Size(340, 81);
            this.RadioGroupTemplate.TabIndex = 1;
            this.RadioGroupTemplate.TabStop = false;
            this.RadioGroupTemplate.Text = "Template";
            // 
            // saveGroupLayout
            // 
            this.saveGroupLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saveGroupLayout.AutoSize = true;
            this.saveGroupLayout.ColumnCount = 3;
            this.saveGroupLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.saveGroupLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.saveGroupLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.saveGroupLayout.Controls.Add(this.directoryButton, 2, 0);
            this.saveGroupLayout.Controls.Add(this.label1, 0, 0);
            this.saveGroupLayout.Controls.Add(this.EditDirectory, 1, 0);
            this.saveGroupLayout.Controls.Add(this.label2, 0, 1);
            this.saveGroupLayout.Controls.Add(this.EditFilename, 1, 1);
            this.saveGroupLayout.Location = new System.Drawing.Point(3, 19);
            this.saveGroupLayout.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.saveGroupLayout.Name = "saveGroupLayout";
            this.saveGroupLayout.RowCount = 2;
            this.saveGroupLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.saveGroupLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.saveGroupLayout.Size = new System.Drawing.Size(334, 53);
            this.saveGroupLayout.TabIndex = 4;
            // 
            // directoryButton
            // 
            this.directoryButton.Location = new System.Drawing.Point(307, 0);
            this.directoryButton.Margin = new System.Windows.Forms.Padding(0);
            this.directoryButton.Name = "directoryButton";
            this.directoryButton.Size = new System.Drawing.Size(24, 24);
            this.directoryButton.TabIndex = 2;
            this.directoryButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Directory";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EditDirectory
            // 
            this.EditDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EditDirectory.Location = new System.Drawing.Point(58, 3);
            this.EditDirectory.Name = "EditDirectory";
            this.EditDirectory.Size = new System.Drawing.Size(246, 20);
            this.EditDirectory.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 27);
            this.label2.TabIndex = 3;
            this.label2.Text = "Filename";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EditFilename
            // 
            this.EditFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saveGroupLayout.SetColumnSpan(this.EditFilename, 2);
            this.EditFilename.Location = new System.Drawing.Point(58, 29);
            this.EditFilename.Name = "EditFilename";
            this.EditFilename.Size = new System.Drawing.Size(273, 20);
            this.EditFilename.TabIndex = 4;
            this.EditFilename.Text = "RibbonMarkup.xml";
            // 
            // GroupBoxPath
            // 
            this.GroupBoxPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxPath.AutoSize = true;
            this.GroupBoxPath.Controls.Add(this.saveGroupLayout);
            this.GroupBoxPath.Location = new System.Drawing.Point(3, 90);
            this.GroupBoxPath.Name = "GroupBoxPath";
            this.GroupBoxPath.Size = new System.Drawing.Size(340, 88);
            this.GroupBoxPath.TabIndex = 2;
            this.GroupBoxPath.TabStop = false;
            this.GroupBoxPath.Text = "Save to Path && Filename";
            // 
            // buttons
            // 
            this.buttons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttons.ColumnCount = 2;
            this.buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.buttons.Controls.Add(this.ButtonOk, 0, 0);
            this.buttons.Controls.Add(this.ButtonCancel, 1, 0);
            this.buttons.Location = new System.Drawing.Point(181, 184);
            this.buttons.Name = "buttons";
            this.buttons.RowCount = 1;
            this.buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.buttons.Size = new System.Drawing.Size(162, 29);
            this.buttons.TabIndex = 0;
            // 
            // ButtonOk
            // 
            this.ButtonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonOk.Enabled = false;
            this.ButtonOk.Location = new System.Drawing.Point(3, 3);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(75, 23);
            this.ButtonOk.TabIndex = 0;
            this.ButtonOk.Text = "OK";
            this.ButtonOk.UseVisualStyleBackColor = true;
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Location = new System.Drawing.Point(84, 3);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 1;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            // 
            // dialogLayout
            // 
            this.dialogLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dialogLayout.AutoSize = true;
            this.dialogLayout.ColumnCount = 1;
            this.dialogLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dialogLayout.Controls.Add(this.RadioGroupTemplate, 0, 0);
            this.dialogLayout.Controls.Add(this.GroupBoxPath, 0, 1);
            this.dialogLayout.Controls.Add(this.buttons, 0, 2);
            this.dialogLayout.Location = new System.Drawing.Point(9, 9);
            this.dialogLayout.Margin = new System.Windows.Forms.Padding(0);
            this.dialogLayout.Name = "dialogLayout";
            this.dialogLayout.RowCount = 3;
            this.dialogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dialogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dialogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dialogLayout.Size = new System.Drawing.Size(346, 216);
            this.dialogLayout.TabIndex = 0;
            // 
            // NewFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 234);
            this.Controls.Add(this.dialogLayout);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewFileForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Ribbon Document";
            this.templateGroupLayout.ResumeLayout(false);
            this.templateGroupLayout.PerformLayout();
            this.RadioGroupTemplate.ResumeLayout(false);
            this.RadioGroupTemplate.PerformLayout();
            this.saveGroupLayout.ResumeLayout(false);
            this.saveGroupLayout.PerformLayout();
            this.GroupBoxPath.ResumeLayout(false);
            this.GroupBoxPath.PerformLayout();
            this.buttons.ResumeLayout(false);
            this.dialogLayout.ResumeLayout(false);
            this.dialogLayout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel templateGroupLayout;
        private System.Windows.Forms.RadioButton wordPadRadioButton;
        private System.Windows.Forms.RadioButton emptyRadioButton;
        private System.Windows.Forms.GroupBox RadioGroupTemplate;
        private System.Windows.Forms.GroupBox GroupBoxPath;
        private System.Windows.Forms.TableLayoutPanel saveGroupLayout;
        private System.Windows.Forms.TextBox EditFilename;
        private System.Windows.Forms.TextBox EditDirectory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel buttons;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Button directoryButton;
        private System.Windows.Forms.TableLayoutPanel dialogLayout;
    }
}
