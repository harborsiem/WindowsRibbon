namespace UIRibbonTools
{
    partial class XmlSourceFrame
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
                _marginTextPen.Dispose();
                _marginBrush.Dispose();
                _focusBrush.Dispose();
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
            this.treeViewXmlSource = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // treeViewXmlSource
            // 
            this.treeViewXmlSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewXmlSource.Location = new System.Drawing.Point(0, 0);
            this.treeViewXmlSource.Name = "treeViewXmlSource";
            this.treeViewXmlSource.Size = new System.Drawing.Size(465, 329);
            this.treeViewXmlSource.TabIndex = 0;
            // 
            // TFrameXmlSource
            // 
            this.Controls.Add(this.treeViewXmlSource);
            this.Name = "TFrameXmlSource";
            this.Size = new System.Drawing.Size(465, 329);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewXmlSource;
    }
}
