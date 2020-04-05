namespace UIRibbonTools
{
    partial class ApplicationModesForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CheckListBoxModes = new System.Windows.Forms.CheckedListBox();
            this.buttons2 = new System.Windows.Forms.TableLayoutPanel();
            this.ButtonCheckAll = new System.Windows.Forms.Button();
            this.ButtonClearAll = new System.Windows.Forms.Button();
            this.buttons = new System.Windows.Forms.TableLayoutPanel();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.dialogLayout = new System.Windows.Forms.TableLayoutPanel();
            this.buttons2.SuspendLayout();
            this.buttons.SuspendLayout();
            this.dialogLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.dialogLayout.SetColumnSpan(this.label1, 2);
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(326, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select 1 or more application modes in which this control is available.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.dialogLayout.SetColumnSpan(this.label2, 2);
            this.label2.Location = new System.Drawing.Point(3, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(228, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "There are 32 user-definable application modes.";
            // 
            // CheckListBoxModes
            // 
            this.CheckListBoxModes.CheckOnClick = true;
            this.dialogLayout.SetColumnSpan(this.CheckListBoxModes, 2);
            this.CheckListBoxModes.FormattingEnabled = true;
            this.CheckListBoxModes.Items.AddRange(new object[] {
            "Mode 0",
            "Mode 1",
            "Mode 2",
            "Mode 3",
            "Mode 4",
            "Mode 5",
            "Mode 6",
            "Mode 7",
            "Mode 8",
            "Mode 9",
            "Mode 10",
            "Mode 11",
            "Mode 12",
            "Mode 13",
            "Mode 14",
            "Mode 15",
            "Mode 16",
            "Mode 17",
            "Mode 18",
            "Mode 19",
            "Mode 20",
            "Mode 21",
            "Mode 22",
            "Mode 23",
            "Mode 24",
            "Mode 25",
            "Mode 26",
            "Mode 27",
            "Mode 28",
            "Mode 29",
            "Mode 30",
            "Mode 31"});
            this.CheckListBoxModes.Location = new System.Drawing.Point(3, 41);
            this.CheckListBoxModes.MultiColumn = true;
            this.CheckListBoxModes.Name = "CheckListBoxModes";
            this.CheckListBoxModes.Size = new System.Drawing.Size(484, 124);
            this.CheckListBoxModes.TabIndex = 4;
            // 
            // buttons2
            // 
            this.buttons2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttons2.AutoSize = true;
            this.buttons2.ColumnCount = 2;
            this.buttons2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.buttons2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.buttons2.Controls.Add(this.ButtonCheckAll, 0, 0);
            this.buttons2.Controls.Add(this.ButtonClearAll, 1, 0);
            this.buttons2.Location = new System.Drawing.Point(3, 171);
            this.buttons2.Name = "buttons2";
            this.buttons2.RowCount = 1;
            this.buttons2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.buttons2.Size = new System.Drawing.Size(162, 29);
            this.buttons2.TabIndex = 1;
            // 
            // ButtonCheckAll
            // 
            this.ButtonCheckAll.Location = new System.Drawing.Point(3, 3);
            this.ButtonCheckAll.Name = "ButtonCheckAll";
            this.ButtonCheckAll.Size = new System.Drawing.Size(75, 23);
            this.ButtonCheckAll.TabIndex = 0;
            this.ButtonCheckAll.Text = "Check All";
            this.ButtonCheckAll.UseVisualStyleBackColor = true;
            // 
            // ButtonClearAll
            // 
            this.ButtonClearAll.Location = new System.Drawing.Point(84, 3);
            this.ButtonClearAll.Name = "ButtonClearAll";
            this.ButtonClearAll.Size = new System.Drawing.Size(75, 23);
            this.ButtonClearAll.TabIndex = 1;
            this.ButtonClearAll.Text = "Clear All";
            this.ButtonClearAll.UseVisualStyleBackColor = true;
            // 
            // buttons
            // 
            this.buttons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttons.AutoSize = true;
            this.buttons.ColumnCount = 2;
            this.buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.buttons.Controls.Add(this.ButtonOk, 0, 0);
            this.buttons.Controls.Add(this.ButtonCancel, 1, 0);
            this.buttons.Location = new System.Drawing.Point(325, 171);
            this.buttons.Name = "buttons";
            this.buttons.RowCount = 1;
            this.buttons.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.buttons.Size = new System.Drawing.Size(162, 29);
            this.buttons.TabIndex = 0;
            // 
            // ButtonOk
            // 
            this.ButtonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
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
            this.dialogLayout.ColumnCount = 2;
            this.dialogLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.dialogLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.dialogLayout.Controls.Add(this.label1, 0, 0);
            this.dialogLayout.Controls.Add(this.label2, 0, 1);
            this.dialogLayout.Controls.Add(this.CheckListBoxModes, 0, 2);
            this.dialogLayout.Controls.Add(this.buttons2, 0, 3);
            this.dialogLayout.Controls.Add(this.buttons, 1, 3);
            this.dialogLayout.Location = new System.Drawing.Point(9, 9);
            this.dialogLayout.Margin = new System.Windows.Forms.Padding(0);
            this.dialogLayout.Name = "dialogLayout";
            this.dialogLayout.RowCount = 4;
            this.dialogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dialogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dialogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dialogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dialogLayout.Size = new System.Drawing.Size(490, 203);
            this.dialogLayout.TabIndex = 0;
            // 
            // ApplicationModesForm
            // 
            this.AcceptButton = this.ButtonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.ButtonCancel;
            this.ClientSize = new System.Drawing.Size(508, 221);
            this.Controls.Add(this.dialogLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ApplicationModesForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Application Modes";
            this.buttons2.ResumeLayout(false);
            this.buttons.ResumeLayout(false);
            this.dialogLayout.ResumeLayout(false);
            this.dialogLayout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel dialogLayout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox CheckListBoxModes;
        private System.Windows.Forms.TableLayoutPanel buttons;
        private System.Windows.Forms.Button ButtonClearAll;
        private System.Windows.Forms.Button ButtonCheckAll;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.TableLayoutPanel buttons2;
    }
}
