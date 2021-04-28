using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace UIRibbonTools
{
    class SpecialGifConverter : BaseConverter
    {
        public SpecialGifConverter(string outputPath, OutputSelector selected)
            : base(outputPath, selected)
        { }

        protected override void OpenSave(string fileName)
        {
            if (File.Exists(fileName))
            {
                //var brush = new SolidBrush(Color.White);
                Bitmap image = new Bitmap(fileName);
                //image must be of Size(78, 50);
                int width = 48;
                int height = 48;
                Bitmap clone48 = image.Clone(new Rectangle(1, 1, width, height), PixelFormat.Format32bppRgb);
                Scale(48, 40, clone48);

                Bitmap clone24 = image.Clone(new Rectangle(53, 13, 24, 24), PixelFormat.Format32bppRgb);
                Scale(48, 64, clone48);
                Scale(48, 32, clone48);
                Scale(24, 20, clone24);
                Scale(24, 16, clone24);

                //OuterTransparent(clone48);
                clone48.MakeTransparent(clone48.GetPixel(0, 0));
                if (_outputSelected == OutputSelector.Bitmap)
                    SetTransparentRGB(clone48, TransparentRGBColor);
                string outFileName = Path.Combine(_outputPath, _fileNameWithoutExtension + "_48.bmp");
                Save(clone48, outFileName);
                clone24.MakeTransparent(clone24.GetPixel(0, 0));
                if (_outputSelected == OutputSelector.Bitmap)
                    SetTransparentRGB(clone24, TransparentRGBColor);
                outFileName = Path.Combine(_outputPath, _fileNameWithoutExtension + "_24.bmp");
                Save(clone24, outFileName);
            }
        }

        private void Scale(int srcSize, int destSize, Bitmap source)
        {
            Bitmap bmp = new Bitmap(destSize, destSize, PixelFormat.Format32bppRgb);
            using (Graphics graph = Graphics.FromImage(bmp))
            {
                // uncomment for higher quality output
                //graph.InterpolationMode = InterpolationMode.High;
                //graph.CompositingQuality = CompositingQuality.HighQuality;

                //graph.SmoothingMode = SmoothingMode.AntiAlias;

                graph.DrawImage(source, new Rectangle(0, 0, destSize, destSize), new Rectangle(0, 0, srcSize, srcSize), GraphicsUnit.Pixel);
            }
            //OuterTransparent(bmp);
            bmp.MakeTransparent(Color.White);
            if (_outputSelected == OutputSelector.Bitmap)
                SetTransparentRGB(bmp, TransparentRGBColor);
            string outFileName = Path.Combine(_outputPath, _fileNameWithoutExtension + "_" + destSize + ".bmp");
            Save(bmp, outFileName);
        }

        private static Bitmap SetTransparentRGB(Bitmap bitmap, int transparentRGB)
        {
            int x, y;
            IntPtr p;
            //Color a1 = bitmap.GetPixel(0, 0);
            //Color a2 = bitmap.GetPixel(bitmap.Width - 1, 0);
            //Color a3 = bitmap.GetPixel(0, bitmap.Height - 1);
            //if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
            //    bitmap.MakeTransparent(bitmap.GetPixel(0, 0));
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            //data.PixelFormat = PixelFormat.Format32bppArgb;
            uint transparentColor = (uint)Marshal.ReadInt32(data.Scan0);
            for (y = 0; y < bitmap.Height; y++)
            {
                p = data.Scan0 + y * 4 * bitmap.Width;
                for (x = 0; x < bitmap.Width; x++)
                {
                    uint value = (uint)Marshal.ReadInt32(p);
                    if ((value & 0xff000000) == 0)
                        Marshal.WriteInt32(p, transparentRGB);
                    //else
                    //    Marshal.WriteInt32(p, unchecked((int)(0xff000000 | value)));
                    p = p + 4;
                }
            }

            bitmap.UnlockBits(data);
            return bitmap;
        }

        /*
        static Bitmap OuterTransparent(Bitmap bitmap)
        {
            int[] content;
            int i, x, y;
            IntPtr p;
            Color a1 = bitmap.GetPixel(0, 0);
            int pix00 = a1.ToArgb();
            int fakeTrans = Color.Magenta.ToArgb();
            //Color a2 = bitmap.GetPixel(bitmap.Width - 1, 0);
            //Color a3 = bitmap.GetPixel(0, bitmap.Height - 1);
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            //uint transparentColor = (uint)Marshal.ReadInt32(data.Scan0);
            int length = bitmap.Width * bitmap.Height;
            content = new int[length];
            Marshal.Copy(data.Scan0, content, 0, length);
            bool stop;
            int value;
            for (y = 0; y < bitmap.Height; y++)
            {
                stop = false;
                i = y * bitmap.Width;
                for (x = 0; x < bitmap.Width; x++)
                {
                    value = content[i];
                    if (value == pix00)
                    {
                        content[i] = fakeTrans;
                    }
                    else
                    {
                        break;
                    }
                    i++;
                }
                i = (y + 1) * bitmap.Width - 1;
                for (x = bitmap.Width - 1; x >= 0; x--)
                {
                    value = content[i];
                    if (value == pix00)
                    {
                        content[i] = fakeTrans;
                    }
                    else
                    {
                        break;
                    }
                    i--;
                }
                if (y == 0)
                {
                    i = y * bitmap.Width;
                    for (x = 0; x < bitmap.Width; x++)
                    {
                        value = content[i];
                        if (value == pix00)
                        {
                            content[i] = fakeTrans;
                        }
                        i++;
                    }
                }
                if (y == bitmap.Width - 1)
                {
                    i = y * bitmap.Width;
                    for (x = 0; x < bitmap.Width; x++)
                    {
                        value = content[i];
                        if (value == pix00)
                        {
                            content[i] = fakeTrans;
                        }
                        i++;
                    }
                }
            }
            Marshal.Copy(content, 0, data.Scan0, length);
            bitmap.UnlockBits(data);
            return bitmap;
        }
        */
    }
}
