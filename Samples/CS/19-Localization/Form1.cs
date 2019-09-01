using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Controls.Events;
using RibbonLib.Interop;
using System.IO;

namespace _19_Localization
{
    public enum RibbonMarkupCommands : uint 
    {
         cmdTab = 1012,
         cmdGroup = 1015,
         cmdButtonOne = 1008,
         cmdButtonTwo = 1009,
         cmdButtonThree = 1010,
    }

    public partial class Form1 : Form
    {
        private bool exitOn = false;
        private RibbonTab _tabDrop;
        private RibbonButton _buttonDropA;
        private RibbonButton _buttonDropB;
        private RibbonButton _buttonDropC;

        public Form1()
        {
            InitializeComponent();

            _tabDrop = new RibbonTab(_ribbonControl, (uint)RibbonMarkupCommands.cmdTab);
            _buttonDropA = new RibbonButton(_ribbonControl, (uint)RibbonMarkupCommands.cmdButtonOne);
            _buttonDropB = new RibbonButton(_ribbonControl, (uint)RibbonMarkupCommands.cmdButtonTwo);
            _buttonDropC = new RibbonButton(_ribbonControl, (uint)RibbonMarkupCommands.cmdButtonThree);

            _buttonDropA.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonDropA_ExecuteEvent);
            _buttonDropB.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonDropB_ExecuteEvent);
        }

        void _buttonDropA_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            // load bitmap from file
            Bitmap bitmap = GetResourceBitmap("Drop32.bmp");
            bitmap.MakeTransparent();

            // set large image property
            _buttonDropA.LargeImage = _ribbonControl.ConvertToUIImage(bitmap);
        }

        void _buttonDropB_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            List<int> supportedImageSizes = new List<int>() { 32, 48, 64 };

            StringBuilder bitmapFileName = new StringBuilder();

            int selectedImageSize;
            if (supportedImageSizes.Contains(SystemInformation.IconSize.Width))
            {
                selectedImageSize = SystemInformation.IconSize.Width;
            }
            else
            {
                selectedImageSize = 32;
            }

            exitOn = !exitOn;
            string exitStatus = exitOn ? "On" : "Off";

            var bitmap = GetResourceBitmap(string.Format("Exit{0}{1}.bmp", exitStatus, selectedImageSize));
            bitmap.MakeTransparent();

            _buttonDropB.LargeImage = _ribbonControl.ConvertToUIImage(bitmap);
        }

        Bitmap GetResourceBitmap(string name)
        {
            string resourceName = string.Format("_19_Localization.Res.{0}", name);
            using(var stream = this.GetType().Assembly.GetManifestResourceStream(resourceName))
            {
                var bitmap = new Bitmap(stream);
                return bitmap;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

    }
}
