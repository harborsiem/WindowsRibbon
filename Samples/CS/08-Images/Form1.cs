using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Controls.Events;
using RibbonLib.Interop;

namespace _08_Images
{
    public enum RibbonMarkupCommands : uint 
    {
         cmdTabDrop = 1012,
         cmdGroupDrop = 1015,
         cmdButtonDropA = 1008,
         cmdButtonDropB = 1009,
         cmdButtonDropC = 1010,
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

            _tabDrop = new RibbonTab(_ribbon, (uint)RibbonMarkupCommands.cmdTabDrop);
            _buttonDropA = new RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonDropA);
            _buttonDropB = new RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonDropB);
            _buttonDropC = new RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonDropC);

            _buttonDropA.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonDropA_ExecuteEvent);
            _buttonDropB.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonDropB_ExecuteEvent);
        }

        void _buttonDropA_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            // load bitmap from file
            Bitmap bitmap = new System.Drawing.Bitmap(@"..\..\Res\Drop32.bmp");
            bitmap.MakeTransparent();

            // set large image property
            _buttonDropA.LargeImage = _ribbon.ConvertToUIImage(bitmap);
        }

        void _buttonDropB_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            List<int> supportedImageSizes = new List<int>() { 32, 48, 64 };

            Bitmap bitmap;
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
            string exitStatus = exitOn ? "on" : "off";

            bitmapFileName.AppendFormat(@"..\..\Res\Exit{0}{1}.bmp", exitStatus, selectedImageSize);

            bitmap = new System.Drawing.Bitmap(bitmapFileName.ToString());
            bitmap.MakeTransparent();

            _buttonDropB.LargeImage = _ribbon.ConvertToUIImage(bitmap);
        }
    }
}
