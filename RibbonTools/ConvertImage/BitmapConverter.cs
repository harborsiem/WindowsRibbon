using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;

namespace UIRibbonTools
{
    class BitmapConverter : BaseConverter
    {
        private readonly bool _inputIgnoreAlpha;

        public BitmapConverter(string outputPath, OutputSelector selected, bool inputIgnoreAlpha)
            : base(outputPath, selected)
        {
            _inputIgnoreAlpha = inputIgnoreAlpha;
        }

        //public static void LoadBmpWinApi(string fileName, string bmpPath)
        //{
        //    System.Drawing.Bitmap bmp = UIRibbonTools.AlphaBitmap.ImageFromFile(fileName);
        //    System.Drawing.Imaging.PixelFormat f = bmp.PixelFormat;
        //    BmpWriter writer = new BmpWriter();
        //    writer.Encode(bmp, Path.ChangeExtension(fileName, "v5bmp"), BmpEncoding.Encoding32BPP);

        //    bmp.Save(Path.ChangeExtension(fileName, "bmp1"), System.Drawing.Imaging.ImageFormat.Bmp);
        //}

        protected override void OpenSave(string fileName)
        {
            Bitmap bmp;
            if (_inputIgnoreAlpha)
            {
                bmp = new Bitmap(fileName);
                if (bmp.PixelFormat == PixelFormat.Format32bppRgb)
                {
                    bmp = bmp.Clone(new Rectangle(new Point(), bmp.Size), PixelFormat.Format24bppRgb);
                    bmp.MakeTransparent();
                }
            }
            else
                bmp = AlphaBitmap.TryAlphaBitmapFromFile(fileName);
            string bmpFile = Path.Combine(_outputPath, _fileNameWithoutExtension + ".bmp");
            if (bmp.PixelFormat == PixelFormat.Format32bppArgb && _outputSelected == OutputSelector.Bitmap)
            {
                AlphaBitmap.SetTransparentRGB(bmp, TransparentRGBColor);
            }
            Save(bmp, bmpFile);
        }

        protected override void Save(Bitmap bmp, string fileName)
        {
            if (_outputSelected == OutputSelector.BitmapV5 && bmp.PixelFormat != PixelFormat.Format32bppArgb)
            {
                bmp.Save(fileName, ImageFormat.Bmp);
                bmp.Dispose();
                return;
            }
            base.Save(bmp, fileName);
        }
    }
}
