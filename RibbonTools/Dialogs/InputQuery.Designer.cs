namespace UIRibbonTools
{
    partial class InputQuery
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
            this.label = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.dialogLayout = new System.Windows.Forms.TableLayoutPanel();
            this.buttonLayoutPanel.SuspendLayout();
            this.dialogLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label.AutoEllipsis = true;
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(3, 0);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(266, 29);
            this.label.TabIndex = 1;
            this.label.Text = "Label_Label_Label_Label_Label_Label_Label_Label";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label.UseMnemonic = false;
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.Location = new System.Drawing.Point(275, 3);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(328, 23);
            this.textBox.TabIndex = 2;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(3, 3);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(87, 27);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(96, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(87, 27);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonLayoutPanel
            // 
            this.buttonLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLayoutPanel.AutoSize = true;
            this.buttonLayoutPanel.ColumnCount = 2;
            this.dialogLayout.SetColumnSpan(this.buttonLayoutPanel, 2);
            this.buttonLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.buttonLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.buttonLayoutPanel.Controls.Add(this.buttonOK, 0, 0);
            this.buttonLayoutPanel.Controls.Add(this.buttonCancel, 1, 0);
            this.buttonLayoutPanel.Location = new System.Drawing.Point(420, 29);
            this.buttonLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.buttonLayoutPanel.Name = "buttonLayoutPanel";
            this.buttonLayoutPanel.RowCount = 1;
            this.buttonLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.buttonLayoutPanel.Size = new System.Drawing.Size(186, 33);
            this.buttonLayoutPanel.TabIndex = 0;
            // 
            // dialogLayout
            // 
            this.dialogLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dialogLayout.AutoSize = true;
            this.dialogLayout.ColumnCount = 2;
            this.dialogLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.dialogLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dialogLayout.Controls.Add(this.label, 0, 0);
            this.dialogLayout.Controls.Add(this.textBox, 1, 0);
            this.dialogLayout.Controls.Add(this.buttonLayoutPanel, 0, 1);
            this.dialogLayout.Location = new System.Drawing.Point(9, 9);
            this.dialogLayout.Margin = new System.Windows.Forms.Padding(0);
            this.dialogLayout.Name = "dialogLayout";
            this.dialogLayout.RowCount = 2;
            this.dialogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dialogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dialogLayout.Size = new System.Drawing.Size(606, 62);
            this.dialogLayout.TabIndex = 0;
            // 
            // InputQuery
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(624, 80);
            this.Controls.Add(this.dialogLayout);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1024, 119);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 119);
            this.Name = "InputQuery";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "InputQuery";
            this.buttonLayoutPanel.ResumeLayout(false);
            this.dialogLayout.ResumeLayout(false);
            this.dialogLayout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel dialogLayout;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TableLayoutPanel buttonLayoutPanel;
    }
}
