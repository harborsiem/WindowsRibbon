using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace UIRibbonTools
{
    class IconConverter : BaseConverter
    {
        public IconConverter(string outputPath, OutputSelector selected)
            : base(outputPath, selected)
        {
        }

        protected override void OpenSave(string fileName)
        {
            System.Drawing.Icon ico = new System.Drawing.Icon(fileName);
            System.Drawing.Bitmap bmp = ico.ToBitmap();
            string bmpFile = Path.Combine(_outputPath, _fileNameWithoutExtension + ".bmp");
            if (bmp.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb && _outputSelected == OutputSelector.Bitmap)
            {
                AlphaBitmap.SetTransparentRGB(bmp, TransparentRGBColor);
            }
            Save(bmp, bmpFile);
        }
    }
}
