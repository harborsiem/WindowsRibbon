using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace UIRibbonTools
{
    public abstract class BaseConverter
    {
        protected static readonly int TransparentRGBColor = Color.LightGray.ToArgb() & 0xffffff;

        protected string _fileNameWithoutExtension;
        protected string _filesDirectory;
        protected string _outputPath;
        protected OutputSelector _outputSelected;

        protected BaseConverter(string outputPath, OutputSelector selected)
        {
            _outputPath = outputPath;
            _outputSelected = selected;
        }

        public void Convert(string[] fileNames)
        {
            for (int i = 0; i < fileNames.Length; i++)
            {
                _filesDirectory = Path.GetDirectoryName(fileNames[i]);
                _fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileNames[i]);
                OpenSave(fileNames[i]);
            }
        }

        protected abstract void OpenSave(string fileName);

        protected virtual void Save(Bitmap bmp, string fileName)
        {
            switch (_outputSelected)
            {
                case OutputSelector.Bitmap:
                    bmp.Save(fileName, ImageFormat.Bmp);
                    break;
                case OutputSelector.BitmapV5:
                    BmpEncoder v5Encoder = new BmpEncoder();
                    v5Encoder.Encode(bmp, fileName, BmpEncoding.Encoding32BPP);
                    break;
                case OutputSelector.Png:
                    string pngFile = Path.ChangeExtension(fileName, ".png");
                    bmp.Save(pngFile, ImageFormat.Png);
                    break;
            }
            bmp.Dispose();
        }
    }
}
