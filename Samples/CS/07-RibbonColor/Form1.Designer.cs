namespace _07_RibbonColor
{
    partial class Form1
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
            this._ribbon = new RibbonLib.Ribbon();
            this.SuspendLayout();
            // 
            // _ribbon
            // 
            this._ribbon.Location = new System.Drawing.Point(0, 0);
            this._ribbon.Minimized = false;
            this._ribbon.Name = "_ribbon";
            this._ribbon.ResourceName = "_07_RibbonColor.RibbonMarkup.ribbon";
            this._ribbon.ShortcutTableResourceName = null;
            this._ribbon.Size = new System.Drawing.Size(501, 100);
            this._ribbon.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 428);
            this.Controls.Add(this._ribbon);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private RibbonLib.Ribbon _ribbon;

    }
}

