using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
//using System.Drawing;

namespace UIRibbonTools
{
    class XamlConverter : BaseConverter
    {
        private const double IMAGE_DPI = 96.0;
        private static readonly int[] sizes = new int[] { 16, 20, 24, 32, 40, 48, 64 };

        public XamlConverter(string outputPath, OutputSelector selected)
            : base(outputPath, selected)
        {
        }

        protected override void OpenSave(string fileName)
        {
            Stream s = File.OpenRead(fileName);
            string bmpFile = Path.Combine(_outputPath, _fileNameWithoutExtension);
            Viewbox box = (Viewbox)XamlReader.Load(s);
            Canvas canvas = new Canvas();
            canvas.Children.Add(box);
            for (int i = 0; i < sizes.Length; i++)
            {
                canvas.Width = sizes[i];
                canvas.Height = sizes[i];
                box.Width = canvas.Width;
                box.Height = canvas.Height;
                SaveImage(canvas, bmpFile + "_" + (sizes[i]).ToString() + ".bmp");
            }
        }

        private void SaveImage(Canvas control, string path)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                GenerateImage(control, stream);
                System.Drawing.Bitmap img = AlphaBitmap.TryCreateAlphaBitmap(stream);
                if (_outputSelected == OutputSelector.Bitmap)
                    AlphaBitmap.SetTransparentRGB(img, TransparentRGBColor);
                Save(img, path);
            }
        }

        private static void GenerateImage(Canvas control, Stream result)
        {
            //Set background to white
            control.Background = Brushes.Transparent; //Brushes.White;

            Size controlSize = RetrieveDesiredSize(control);
            Rect rect = new Rect(0, 0, controlSize.Width, controlSize.Height);

            RenderTargetBitmap rtb = new RenderTargetBitmap((int)controlSize.Width, (int)controlSize.Height, IMAGE_DPI, IMAGE_DPI, PixelFormats.Pbgra32);

            control.Arrange(rect);
            rtb.Render(control);

            PngBitmapEncoder png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(rtb));
            png.Save(result);
        }

        private static Size RetrieveDesiredSize(Canvas control)
        {
            control.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            return control.DesiredSize;
        }

        private static void SaveImage(Viewbox control, string path)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                GenerateImage(control, stream);
                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                img.Save(path, System.Drawing.Imaging.ImageFormat.Bmp);
            }
        }

        private static void GenerateImage(Viewbox control, Stream result)
        {
            //Set background to white
            //control.Background = Brushes.White;

            Size controlSize = RetrieveDesiredSize(control);
            Rect rect = new Rect(0, 0, controlSize.Width, controlSize.Height);

            RenderTargetBitmap rtb = new RenderTargetBitmap((int)controlSize.Width, (int)controlSize.Height, IMAGE_DPI, IMAGE_DPI, PixelFormats.Pbgra32);

            control.Arrange(rect);
            rtb.Render(control);

            PngBitmapEncoder png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(rtb));
            png.Save(result);
        }

        private static Size RetrieveDesiredSize(Viewbox control)
        {
            control.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            return control.DesiredSize;
        }


        //following functions not used
        private static void SaveImage(Control control, string path)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                GenerateImage(control, stream);
                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                img.Save(path);
            }
        }

        private static void GenerateImage(Control control, Stream result)
        {
            //Set background to white
            control.Background = Brushes.White;

            Size controlSize = RetrieveDesiredSize(control);
            Rect rect = new Rect(0, 0, controlSize.Width, controlSize.Height);

            RenderTargetBitmap rtb = new RenderTargetBitmap((int)controlSize.Width, (int)controlSize.Height, IMAGE_DPI, IMAGE_DPI, PixelFormats.Pbgra32);

            control.Arrange(rect);
            rtb.Render(control);

            PngBitmapEncoder png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(rtb));
            png.Save(result);
        }

        private static Size RetrieveDesiredSize(Control control)
        {
            control.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            return control.DesiredSize;
        }
    }
}
