namespace UIRibbonTools
{
    partial class TFrameApplicationMenu
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
            this.SuspendLayout();
            // 
            // FrameApplicationMenu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Name = "FrameApplicationMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox _groupBoxRecentItems;
        private System.Windows.Forms.CheckBox _checkBoxEnableRecentItems;
        private System.Windows.Forms.Label _labelMaxCount;
        private System.Windows.Forms.NumericUpDown _upDownMaxCount;
        private System.Windows.Forms.CheckBox _checkBoxEnablePinning;
        private System.Windows.Forms.Label _labelCaptionCommand;
        private System.Windows.Forms.ComboBox _comboBoxCaptionCommand;
        private System.Windows.Forms.TableLayoutPanel groupLayout;

    }
}
