using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Controls.Events;

namespace RibbonLib.Controls
{
    partial class RibbonItems
    {
        private bool exitOn = false;

        public void Init()
        {
            ButtonDropA.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonDropA_ExecuteEvent);
            ButtonDropB.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonDropB_ExecuteEvent);
        }

        void _buttonDropA_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            // load bitmap from file
            Bitmap bitmap = new System.Drawing.Bitmap(@"..\..\Res\Drop32.bmp");
            bitmap.MakeTransparent();

            // set large image property
            ButtonDropA.LargeImage = Ribbon.ConvertToUIImage(bitmap);
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

            ButtonDropB.LargeImage = Ribbon.ConvertToUIImage(bitmap);
        }

        public void Load()
        {
        }

    }
}
