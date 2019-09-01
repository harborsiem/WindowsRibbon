namespace _09_Galleries
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.imageListLines = new System.Windows.Forms.ImageList(this.components);
            this.imageListBrushes = new System.Windows.Forms.ImageList(this.components);
            this.imageListShapes = new System.Windows.Forms.ImageList(this.components);
            this._ribbon = new RibbonLib.Ribbon();
            this.SuspendLayout();
            // 
            // imageListLines
            // 
            this.imageListLines.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListLines.ImageStream")));
            this.imageListLines.TransparentColor = System.Drawing.Color.Gray;
            this.imageListLines.Images.SetKeyName(0, "line01.bmp");
            this.imageListLines.Images.SetKeyName(1, "line02.bmp");
            this.imageListLines.Images.SetKeyName(2, "line03.bmp");
            this.imageListLines.Images.SetKeyName(3, "line04.bmp");
            this.imageListLines.Images.SetKeyName(4, "line05.bmp");
            this.imageListLines.Images.SetKeyName(5, "line06.bmp");
            this.imageListLines.Images.SetKeyName(6, "line07.bmp");
            this.imageListLines.Images.SetKeyName(7, "line08.bmp");
            this.imageListLines.Images.SetKeyName(8, "line09.bmp");
            this.imageListLines.Images.SetKeyName(9, "line10.bmp");
            this.imageListLines.Images.SetKeyName(10, "line11.bmp");
            this.imageListLines.Images.SetKeyName(11, "line12.bmp");
            // 
            // imageListBrushes
            // 
            this.imageListBrushes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListBrushes.ImageStream")));
            this.imageListBrushes.TransparentColor = System.Drawing.Color.Gray;
            this.imageListBrushes.Images.SetKeyName(0, "brush1.bmp");
            this.imageListBrushes.Images.SetKeyName(1, "brush2.bmp");
            this.imageListBrushes.Images.SetKeyName(2, "brush3.bmp");
            this.imageListBrushes.Images.SetKeyName(3, "brush4.bmp");
            this.imageListBrushes.Images.SetKeyName(4, "brush5.bmp");
            this.imageListBrushes.Images.SetKeyName(5, "brush6.bmp");
            this.imageListBrushes.Images.SetKeyName(6, "brush7.bmp");
            this.imageListBrushes.Images.SetKeyName(7, "brush8.bmp");
            this.imageListBrushes.Images.SetKeyName(8, "brush9.bmp");
            // 
            // imageListShapes
            // 
            this.imageListShapes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListShapes.ImageStream")));
            this.imageListShapes.TransparentColor = System.Drawing.Color.Gray;
            this.imageListShapes.Images.SetKeyName(0, "shape01.bmp");
            this.imageListShapes.Images.SetKeyName(1, "shape02.bmp");
            this.imageListShapes.Images.SetKeyName(2, "shape03.bmp");
            this.imageListShapes.Images.SetKeyName(3, "shape04.bmp");
            this.imageListShapes.Images.SetKeyName(4, "shape05.bmp");
            this.imageListShapes.Images.SetKeyName(5, "shape06.bmp");
            this.imageListShapes.Images.SetKeyName(6, "shape07.bmp");
            this.imageListShapes.Images.SetKeyName(7, "shape08.bmp");
            this.imageListShapes.Images.SetKeyName(8, "shape09.bmp");
            this.imageListShapes.Images.SetKeyName(9, "shape10.bmp");
            this.imageListShapes.Images.SetKeyName(10, "shape11.bmp");
            this.imageListShapes.Images.SetKeyName(11, "shape12.bmp");
            this.imageListShapes.Images.SetKeyName(12, "shape13.bmp");
            this.imageListShapes.Images.SetKeyName(13, "shape14.bmp");
            this.imageListShapes.Images.SetKeyName(14, "shape15.bmp");
            this.imageListShapes.Images.SetKeyName(15, "shape16.bmp");
            this.imageListShapes.Images.SetKeyName(16, "shape17.bmp");
            this.imageListShapes.Images.SetKeyName(17, "shape18.bmp");
            this.imageListShapes.Images.SetKeyName(18, "shape19.bmp");
            this.imageListShapes.Images.SetKeyName(19, "shape20.bmp");
            this.imageListShapes.Images.SetKeyName(20, "shape21.bmp");
            this.imageListShapes.Images.SetKeyName(21, "shape22.bmp");
            this.imageListShapes.Images.SetKeyName(22, "shape23.bmp");
            // 
            // _ribbon
            // 
            this._ribbon.Location = new System.Drawing.Point(0, 0);
            this._ribbon.Minimized = false;
            this._ribbon.Name = "_ribbon";
            this._ribbon.ResourceName = "_09_Galleries.RibbonMarkup.ribbon";
            this._ribbon.ShortcutTableResourceName = null;
            this._ribbon.Size = new System.Drawing.Size(501, 100);
            this._ribbon.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 428);
            this.Controls.Add(this._ribbon);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageListLines;
        private System.Windows.Forms.ImageList imageListBrushes;
        private System.Windows.Forms.ImageList imageListShapes;
        private RibbonLib.Ribbon _ribbon;
    }
}

