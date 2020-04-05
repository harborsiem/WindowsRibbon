using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.ComponentModel;

namespace UIRibbonTools
{
    static class ImageManager
    {
        private static Assembly s_ass;
        private static ImageList s_commands;
        private static ImageList s_views;

        static ImageManager()
        {
            s_ass = Assembly.GetExecutingAssembly();
        }

        public static ImageList ImageList_Edit(IContainer components)
        {
            ImageList imageList = new ImageList(components);
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageList_Edit.bmp");
            Bitmap bitmap = new Bitmap(stream);
            imageList.Images.AddStrip(bitmap);
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.ImageSize = new Size(16, 16);
            imageList.TransparentColor = Color.Black;
            return imageList;
        }

        public static ImageList ImageList_NewFile(IContainer components)
        {
            ImageList imageList = new ImageList(components);
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageList_NewFile.bmp");
            Bitmap bitmap = new Bitmap(stream);
            imageList.Images.AddStrip(bitmap);
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.ImageSize = new Size(16, 16);
            imageList.TransparentColor = Color.Black;
            return imageList;
        }

        public static ImageList ImageListAppModes_Shared(IContainer components)
        {
            ImageList imageList = new ImageList(components);
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageListAppModes_Shared.bmp");
            Bitmap bitmap = new Bitmap(stream);
            imageList.Images.AddStrip(bitmap);
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.ImageSize = new Size(16, 16);
            imageList.TransparentColor = Color.Black;
            return imageList;
        }

        public static ImageList Images_Main(IContainer components)
        {
            ImageList imageList = new ImageList(components);
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.Images_Main.bmp");
            Bitmap bitmap = new Bitmap(stream);
            imageList.Images.AddStrip(bitmap);
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.ImageSize = new Size(16, 16);
            imageList.TransparentColor = Color.Black;
            return imageList;
        }

        public static ImageList ImageListToolbars_Commands(IContainer components)
        {
            if (s_commands != null)
                return s_commands;
            ImageList imageList = new ImageList(components);
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageListToolbars_Commands.bmp");
            Bitmap bitmap = new Bitmap(stream);
            imageList.Images.AddStrip(bitmap);
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.ImageSize = new Size(16, 16);
            imageList.TransparentColor = Color.Black;
            s_commands = imageList;
            return imageList;
        }

        public static ImageList ImageListToolbars_ImageList(IContainer components)
        {
            ImageList imageList = new ImageList(components);
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageListToolbars_ImageList.bmp");
            Bitmap bitmap = new Bitmap(stream);
            imageList.Images.AddStrip(bitmap);
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.ImageSize = new Size(16, 16);
            imageList.TransparentColor = Color.Black;
            return imageList;
        }

        public static ImageList ImageListTreeView_Views(IContainer components)
        {
            if (s_views != null)
                return s_views;
            ImageList imageList = new ImageList(components);
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageListTreeView_Views.bmp");
            Bitmap bitmap = new Bitmap(stream);
            imageList.Images.AddStrip(bitmap);
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.ImageSize = new Size(16, 16);
            imageList.TransparentColor = Color.Black;
            s_views = imageList;
            return imageList;
        }
    }
}
