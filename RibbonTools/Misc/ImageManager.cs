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

        public static Image ApplicationMenuSample()
        {
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageSample_ApplicationMenu.png");
            return new Bitmap(stream);
        }

        public static Image ButtonSample()
        {
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageSample_Button.png");
            return new Bitmap(stream);
        }

        public static Image CheckBoxSample()
        {
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageSample_CheckBox.png");
            return new Bitmap(stream);
        }

        public static Image ComboBoxSample()
        {
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageSample_ComboBox.png");
            return new Bitmap(stream);
        }

        public static Image ContextMapSample()
        {
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageSample_ContextMap.png");
            return new Bitmap(stream);
        }

        public static Image ContextMenuSample()
        {
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageSample_ContextMenu.png");
            return new Bitmap(stream);
        }

        public static Image ControlGroupSample()
        {
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageSample_ControlGroup.png");
            return new Bitmap(stream);
        }

        public static Image DropDownButtonSample()
        {
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageSample_DropDownButton.png");
            return new Bitmap(stream);
        }

        public static Image DropDownColorPickerSample()
        {
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageSample_DropDownColorPicker.png");
            return new Bitmap(stream);
        }

        public static Image DropDownGallerySample()
        {
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageSample_DropDownGallery.png");
            return new Bitmap(stream);
        }

        public static Image FloatyFontControlSample()
        {
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageSample_FloatyFontControl.png");
            return new Bitmap(stream);
        }

        public static Image FontControlSample()
        {
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageSample_FontControl.png");
            return new Bitmap(stream);
        }

        //public static Image GallerySample()
        //{
        //    Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageSample_Gallery.png");
        //    return new Bitmap(stream);
        //}

        public static Image GroupSample()
        {
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageSample_Group.png");
            return new Bitmap(stream);
        }

        public static Image HelpSample()
        {
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageSample_Help.png");
            return new Bitmap(stream);
        }

        public static Image InRibbonGallerySample()
        {
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageSample_InRibbonGallery.png");
            return new Bitmap(stream);
        }

        public static Image MenuGroupSample()
        {
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageSample_MenuGroup.png");
            return new Bitmap(stream);
        }

        public static Image MenuGroup1Sample()
        {
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageSample_MenuGroup1.png");
            return new Bitmap(stream);
        }

        public static Image MiniToolBarSample()
        {
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageSample_MiniToolBar.png");
            return new Bitmap(stream);
        }

        public static Image QATControlSample()
        {
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageSample_QATControl.png");
            return new Bitmap(stream);
        }

        public static Image QuickAccessToolBarSample()
        {
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageSample_QuickAccessToolBar.png");
            return new Bitmap(stream);
        }

        public static Image SpinnerSample()
        {
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageSample_Spinner.png");
            return new Bitmap(stream);
        }

        public static Image SplitButtonSample()
        {
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageSample_SplitButton.png");
            return new Bitmap(stream);
        }

        public static Image SplitButtonGallerySample()
        {
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageSample_SplitButtonGallery.png");
            return new Bitmap(stream);
        }

        public static Image TabSample()
        {
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageSample_Tab.png");
            return new Bitmap(stream);
        }

        public static Image TabGroupSample()
        {
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageSample_TabGroup.png");
            return new Bitmap(stream);
        }

        public static Image ToggleButtonSample()
        {
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageSample_ToggleButton.png");
            return new Bitmap(stream);
        }

        public static Image ViewRibbonSample()
        {
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageSample_ViewRibbon.png");
            return new Bitmap(stream);
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

        public static ImageList ImageList_Settings(IContainer components)
        {
            ImageList imageList = new ImageList(components);
            Stream stream = s_ass.GetManifestResourceStream("UIRibbonTools.Images.ImageList_Settings.bmp");
            Bitmap bitmap = new Bitmap(stream);
            imageList.Images.AddStrip(bitmap);
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.ImageSize = new Size(16, 16);
            imageList.TransparentColor = Color.Black;
            return imageList;
        }
    }
}
