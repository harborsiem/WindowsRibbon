namespace UIRibbonTools
{
    partial class SettingsForm
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
            this.pathGroupLayout = new System.Windows.Forms.TableLayoutPanel();
            this.ribbonCompilerLabel = new System.Windows.Forms.Label();
            this.ribbonCompilerText = new System.Windows.Forms.TextBox();
            this.compilerButton = new System.Windows.Forms.Button();
            this.resourceCompilerLabel = new System.Windows.Forms.Label();
            this.resourceCompilerText = new System.Windows.Forms.TextBox();
            this.resourceButton = new System.Windows.Forms.Button();
            this.linkerLabel = new System.Windows.Forms.Label();
            this.linkerText = new System.Windows.Forms.TextBox();
            this.linkerButton = new System.Windows.Forms.Button();
            this.pathGroup = new System.Windows.Forms.GroupBox();
            this.wrapperGroupLayout = new System.Windows.Forms.TableLayoutPanel();
            this.cSharpCheck = new System.Windows.Forms.CheckBox();
            this.vbCheck = new System.Windows.Forms.CheckBox();
            this.wrapperGroup = new System.Windows.Forms.GroupBox();
            this.extrasGroupLayout = new System.Windows.Forms.TableLayoutPanel();
            this.autoUpdateToolsPath = new System.Windows.Forms.CheckBox();
            this.deleteResFile = new System.Windows.Forms.CheckBox();
            this.allowPngImages = new System.Windows.Forms.CheckBox();
            this.allowChangingResourceName = new System.Windows.Forms.CheckBox();
            this.sizeButton = new System.Windows.Forms.Button();
            this.extrasGroup = new System.Windows.Forms.GroupBox();
            this.buttons = new System.Windows.Forms.TableLayoutPanel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.dialogLayout = new System.Windows.Forms.TableLayoutPanel();
            this.pathGroupLayout.SuspendLayout();
            this.pathGroup.SuspendLayout();
            this.wrapperGroupLayout.SuspendLayout();
            this.wrapperGroup.SuspendLayout();
            this.extrasGroupLayout.SuspendLayout();
            this.extrasGroup.SuspendLayout();
            this.buttons.SuspendLayout();
            this.dialogLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // pathGroupLayout
            // 
            this.pathGroupLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pathGroupLayout.AutoSize = true;
            this.pathGroupLayout.ColumnCount = 3;
            this.pathGroupLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pathGroupLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pathGroupLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.pathGroupLayout.Controls.Add(this.ribbonCompilerLabel, 0, 0);
            this.pathGroupLayout.Controls.Add(this.ribbonCompilerText, 1, 0);
            this.pathGroupLayout.Controls.Add(this.compilerButton, 2, 0);
            this.pathGroupLayout.Controls.Add(this.resourceCompilerLabel, 0, 1);
            this.pathGroupLayout.Controls.Add(this.resourceCompilerText, 1, 1);
            this.pathGroupLayout.Controls.Add(this.resourceButton, 2, 1);
            this.pathGroupLayout.Controls.Add(this.linkerLabel, 0, 2);
            this.pathGroupLayout.Controls.Add(this.linkerText, 1, 2);
            this.pathGroupLayout.Controls.Add(this.linkerButton, 2, 2);
            this.pathGroupLayout.Location = new System.Drawing.Point(3, 19);
            this.pathGroupLayout.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pathGroupLayout.Name = "pathGroupLayout";
            this.pathGroupLayout.RowCount = 3;
            this.pathGroupLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pathGroupLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pathGroupLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pathGroupLayout.Size = new System.Drawing.Size(688, 78);
            this.pathGroupLayout.TabIndex = 0;
            // 
            // ribbonCompilerLabel
            // 
            this.ribbonCompilerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ribbonCompilerLabel.AutoSize = true;
            this.ribbonCompilerLabel.Location = new System.Drawing.Point(3, 0);
            this.ribbonCompilerLabel.Name = "ribbonCompilerLabel";
            this.ribbonCompilerLabel.Size = new System.Drawing.Size(84, 26);
            this.ribbonCompilerLabel.TabIndex = 0;
            this.ribbonCompilerLabel.Text = "Ribbon Compiler";
            this.ribbonCompilerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ribbonCompilerText
            // 
            this.ribbonCompilerText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ribbonCompilerText.Location = new System.Drawing.Point(105, 3);
            this.ribbonCompilerText.Name = "ribbonCompilerText";
            this.ribbonCompilerText.ReadOnly = true;
            this.ribbonCompilerText.Size = new System.Drawing.Size(556, 20);
            this.ribbonCompilerText.TabIndex = 1;
            // 
            // compilerButton
            // 
            this.compilerButton.Location = new System.Drawing.Point(664, 0);
            this.compilerButton.Margin = new System.Windows.Forms.Padding(0);
            this.compilerButton.Name = "compilerButton";
            this.compilerButton.Size = new System.Drawing.Size(24, 24);
            this.compilerButton.TabIndex = 2;
            this.compilerButton.UseVisualStyleBackColor = true;
            // 
            // resourceCompilerLabel
            // 
            this.resourceCompilerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.resourceCompilerLabel.AutoSize = true;
            this.resourceCompilerLabel.Location = new System.Drawing.Point(3, 26);
            this.resourceCompilerLabel.Name = "resourceCompilerLabel";
            this.resourceCompilerLabel.Size = new System.Drawing.Size(96, 26);
            this.resourceCompilerLabel.TabIndex = 3;
            this.resourceCompilerLabel.Text = "Resource Compiler";
            this.resourceCompilerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // resourceCompilerText
            // 
            this.resourceCompilerText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resourceCompilerText.Location = new System.Drawing.Point(105, 29);
            this.resourceCompilerText.Name = "resourceCompilerText";
            this.resourceCompilerText.ReadOnly = true;
            this.resourceCompilerText.Size = new System.Drawing.Size(556, 20);
            this.resourceCompilerText.TabIndex = 4;
            // 
            // resourceButton
            // 
            this.resourceButton.Location = new System.Drawing.Point(664, 26);
            this.resourceButton.Margin = new System.Windows.Forms.Padding(0);
            this.resourceButton.Name = "resourceButton";
            this.resourceButton.Size = new System.Drawing.Size(24, 24);
            this.resourceButton.TabIndex = 5;
            this.resourceButton.UseVisualStyleBackColor = true;
            // 
            // linkerLabel
            // 
            this.linkerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.linkerLabel.AutoSize = true;
            this.linkerLabel.Location = new System.Drawing.Point(3, 52);
            this.linkerLabel.Name = "linkerLabel";
            this.linkerLabel.Size = new System.Drawing.Size(47, 26);
            this.linkerLabel.TabIndex = 6;
            this.linkerLabel.Text = "Link.exe";
            this.linkerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // linkerText
            // 
            this.linkerText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkerText.Location = new System.Drawing.Point(105, 55);
            this.linkerText.Name = "linkerText";
            this.linkerText.ReadOnly = true;
            this.linkerText.Size = new System.Drawing.Size(556, 20);
            this.linkerText.TabIndex = 7;
            // 
            // linkerButton
            // 
            this.linkerButton.Location = new System.Drawing.Point(664, 52);
            this.linkerButton.Margin = new System.Windows.Forms.Padding(0);
            this.linkerButton.Name = "linkerButton";
            this.linkerButton.Size = new System.Drawing.Size(24, 24);
            this.linkerButton.TabIndex = 8;
            this.linkerButton.UseVisualStyleBackColor = true;
            // 
            // pathGroup
            // 
            this.pathGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pathGroup.AutoSize = true;
            this.pathGroup.Controls.Add(this.pathGroupLayout);
            this.pathGroup.Location = new System.Drawing.Point(3, 3);
            this.pathGroup.Name = "pathGroup";
            this.pathGroup.Size = new System.Drawing.Size(694, 113);
            this.pathGroup.TabIndex = 1;
            this.pathGroup.TabStop = false;
            this.pathGroup.Text = "Path for Tools";
            // 
            // wrapperGroupLayout
            // 
            this.wrapperGroupLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wrapperGroupLayout.AutoSize = true;
            this.wrapperGroupLayout.ColumnCount = 1;
            this.wrapperGroupLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.wrapperGroupLayout.Controls.Add(this.cSharpCheck, 0, 0);
            this.wrapperGroupLayout.Controls.Add(this.vbCheck, 0, 1);
            this.wrapperGroupLayout.Location = new System.Drawing.Point(3, 19);
            this.wrapperGroupLayout.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.wrapperGroupLayout.Name = "wrapperGroupLayout";
            this.wrapperGroupLayout.RowCount = 2;
            this.wrapperGroupLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.wrapperGroupLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.wrapperGroupLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.wrapperGroupLayout.Size = new System.Drawing.Size(688, 46);
            this.wrapperGroupLayout.TabIndex = 0;
            // 
            // cSharpCheck
            // 
            this.cSharpCheck.AutoSize = true;
            this.cSharpCheck.Location = new System.Drawing.Point(3, 3);
            this.cSharpCheck.Name = "cSharpCheck";
            this.cSharpCheck.Size = new System.Drawing.Size(84, 17);
            this.cSharpCheck.TabIndex = 0;
            this.cSharpCheck.Text = "C# Wrapper";
            this.cSharpCheck.UseVisualStyleBackColor = true;
            // 
            // vbCheck
            // 
            this.vbCheck.AutoSize = true;
            this.vbCheck.Location = new System.Drawing.Point(3, 26);
            this.vbCheck.Name = "vbCheck";
            this.vbCheck.Size = new System.Drawing.Size(127, 17);
            this.vbCheck.TabIndex = 1;
            this.vbCheck.Text = "Visual Basic Wrapper";
            this.vbCheck.UseVisualStyleBackColor = true;
            // 
            // wrapperGroup
            // 
            this.wrapperGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wrapperGroup.AutoSize = true;
            this.wrapperGroup.Controls.Add(this.wrapperGroupLayout);
            this.wrapperGroup.Location = new System.Drawing.Point(3, 122);
            this.wrapperGroup.Name = "wrapperGroup";
            this.wrapperGroup.Size = new System.Drawing.Size(694, 81);
            this.wrapperGroup.TabIndex = 2;
            this.wrapperGroup.TabStop = false;
            this.wrapperGroup.Text = "Build Code Wrapper";
            // 
            // extrasGroupLayout
            // 
            this.extrasGroupLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.extrasGroupLayout.AutoSize = true;
            this.extrasGroupLayout.ColumnCount = 1;
            this.extrasGroupLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.extrasGroupLayout.Controls.Add(this.autoUpdateToolsPath, 0, 0);
            this.extrasGroupLayout.Controls.Add(this.deleteResFile, 0, 1);
            this.extrasGroupLayout.Controls.Add(this.allowPngImages, 0, 2);
            this.extrasGroupLayout.Controls.Add(this.allowChangingResourceName, 0, 3);
            this.extrasGroupLayout.Controls.Add(this.sizeButton, 0, 4);
            this.extrasGroupLayout.Location = new System.Drawing.Point(3, 19);
            this.extrasGroupLayout.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.extrasGroupLayout.Name = "extrasGroupLayout";
            this.extrasGroupLayout.RowCount = 5;
            this.extrasGroupLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.extrasGroupLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.extrasGroupLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.extrasGroupLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.extrasGroupLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.extrasGroupLayout.Size = new System.Drawing.Size(688, 121);
            this.extrasGroupLayout.TabIndex = 0;
            // 
            // autoUpdateToolsPath
            // 
            this.autoUpdateToolsPath.AutoSize = true;
            this.autoUpdateToolsPath.Location = new System.Drawing.Point(3, 3);
            this.autoUpdateToolsPath.Name = "autoUpdateToolsPath";
            this.autoUpdateToolsPath.Size = new System.Drawing.Size(138, 17);
            this.autoUpdateToolsPath.TabIndex = 0;
            this.autoUpdateToolsPath.Text = "Auto update Tools Path";
            this.autoUpdateToolsPath.UseVisualStyleBackColor = true;
            // 
            // deleteResFile
            // 
            this.deleteResFile.AutoSize = true;
            this.deleteResFile.Location = new System.Drawing.Point(3, 26);
            this.deleteResFile.Name = "deleteResFile";
            this.deleteResFile.Size = new System.Drawing.Size(124, 17);
            this.deleteResFile.TabIndex = 1;
            this.deleteResFile.Text = "Delete *.rc, *.res files";
            this.deleteResFile.UseVisualStyleBackColor = true;
            // 
            // allowPngImages
            // 
            this.allowPngImages.AutoSize = true;
            this.allowPngImages.Location = new System.Drawing.Point(3, 49);
            this.allowPngImages.Name = "allowPngImages";
            this.allowPngImages.Size = new System.Drawing.Size(116, 17);
            this.allowPngImages.TabIndex = 2;
            this.allowPngImages.Text = "Allow *.png Images";
            this.allowPngImages.UseVisualStyleBackColor = true;
            // 
            // allowChangingResourceName
            // 
            this.allowChangingResourceName.AutoSize = true;
            this.allowChangingResourceName.Location = new System.Drawing.Point(3, 72);
            this.allowChangingResourceName.Name = "allowChangingResourceName";
            this.allowChangingResourceName.Size = new System.Drawing.Size(270, 17);
            this.allowChangingResourceName.TabIndex = 3;
            this.allowChangingResourceName.Text = "Allow changing ResourceName (ResourceIdentifier)";
            this.allowChangingResourceName.UseVisualStyleBackColor = true;
            // 
            // sizeButton
            // 
            this.sizeButton.AutoSize = true;
            this.sizeButton.Location = new System.Drawing.Point(3, 95);
            this.sizeButton.Name = "sizeButton";
            this.sizeButton.Size = new System.Drawing.Size(193, 23);
            this.sizeButton.TabIndex = 4;
            this.sizeButton.Text = "Set current application size as default";
            this.sizeButton.UseVisualStyleBackColor = true;
            // 
            // extrasGroup
            // 
            this.extrasGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.extrasGroup.AutoSize = true;
            this.extrasGroup.Controls.Add(this.extrasGroupLayout);
            this.extrasGroup.Location = new System.Drawing.Point(3, 209);
            this.extrasGroup.Name = "extrasGroup";
            this.extrasGroup.Size = new System.Drawing.Size(694, 156);
            this.extrasGroup.TabIndex = 3;
            this.extrasGroup.TabStop = false;
            this.extrasGroup.Text = "Extras";
            // 
            // buttons
            // 
            this.buttons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttons.AutoSize = true;
            this.buttons.ColumnCount = 2;
            this.buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.buttons.Controls.Add(this.buttonCancel, 1, 0);
            this.buttons.Controls.Add(this.buttonOK, 0, 0);
            this.buttons.Location = new System.Drawing.Point(535, 371);
            this.buttons.Name = "buttons";
            this.buttons.RowCount = 1;
            this.buttons.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.buttons.Size = new System.Drawing.Size(162, 29);
            this.buttons.TabIndex = 0;
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
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(3, 3);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // dialogLayout
            // 
            this.dialogLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dialogLayout.AutoSize = true;
            this.dialogLayout.ColumnCount = 1;
            this.dialogLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dialogLayout.Controls.Add(this.pathGroup, 0, 0);
            this.dialogLayout.Controls.Add(this.wrapperGroup, 0, 1);
            this.dialogLayout.Controls.Add(this.extrasGroup, 0, 2);
            this.dialogLayout.Controls.Add(this.buttons, 0, 3);
            this.dialogLayout.Location = new System.Drawing.Point(12, 12);
            this.dialogLayout.Name = "dialogLayout";
            this.dialogLayout.RowCount = 4;
            this.dialogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dialogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dialogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dialogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dialogLayout.Size = new System.Drawing.Size(700, 403);
            this.dialogLayout.TabIndex = 0;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(724, 427);
            this.Controls.Add(this.dialogLayout);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1024, 466);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 466);
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.pathGroupLayout.ResumeLayout(false);
            this.pathGroupLayout.PerformLayout();
            this.pathGroup.ResumeLayout(false);
            this.pathGroup.PerformLayout();
            this.wrapperGroupLayout.ResumeLayout(false);
            this.wrapperGroupLayout.PerformLayout();
            this.wrapperGroup.ResumeLayout(false);
            this.wrapperGroup.PerformLayout();
            this.extrasGroupLayout.ResumeLayout(false);
            this.extrasGroupLayout.PerformLayout();
            this.extrasGroup.ResumeLayout(false);
            this.extrasGroup.PerformLayout();
            this.buttons.ResumeLayout(false);
            this.dialogLayout.ResumeLayout(false);
            this.dialogLayout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel dialogLayout;
        private System.Windows.Forms.GroupBox extrasGroup;
        private System.Windows.Forms.TableLayoutPanel extrasGroupLayout;
        private System.Windows.Forms.CheckBox autoUpdateToolsPath;
        private System.Windows.Forms.CheckBox deleteResFile;
        private System.Windows.Forms.GroupBox wrapperGroup;
        private System.Windows.Forms.TableLayoutPanel wrapperGroupLayout;
        private System.Windows.Forms.CheckBox cSharpCheck;
        private System.Windows.Forms.CheckBox vbCheck;
        private System.Windows.Forms.TableLayoutPanel buttons;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.GroupBox pathGroup;
        private System.Windows.Forms.TableLayoutPanel pathGroupLayout;
        private System.Windows.Forms.Label ribbonCompilerLabel;
        private System.Windows.Forms.Label resourceCompilerLabel;
        private System.Windows.Forms.Label linkerLabel;
        private System.Windows.Forms.TextBox ribbonCompilerText;
        private System.Windows.Forms.TextBox resourceCompilerText;
        private System.Windows.Forms.TextBox linkerText;
        private System.Windows.Forms.CheckBox allowChangingResourceName;
        private System.Windows.Forms.CheckBox allowPngImages;
        private System.Windows.Forms.Button sizeButton;
        private System.Windows.Forms.Button linkerButton;
        private System.Windows.Forms.Button resourceButton;
        private System.Windows.Forms.Button compilerButton;
    }
}
