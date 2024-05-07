using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Controls.Events;

namespace RibbonLib.Controls
{
    partial class RibbonItems
    {
        private bool exitOn = false;
        private RibbonButton _buttonDropA;
        private RibbonButton _buttonDropB;
        private RibbonButton _buttonDropC;

        public void Init()
        {
            _buttonDropA = ButtonOne;
            _buttonDropB = ButtonTwo;
            _buttonDropC = ButtonThree;
            _buttonDropA.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonDropA_ExecuteEvent);
            _buttonDropB.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonDropB_ExecuteEvent);
        }

        void _buttonDropA_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            // load bitmap from file
            Bitmap bitmap = GetResourceBitmap("Drop32.bmp");
            bitmap.MakeTransparent();

            // set large image property
            _buttonDropA.LargeImage = Ribbon.ConvertToUIImage(bitmap);
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

            _buttonDropB.LargeImage = Ribbon.ConvertToUIImage(bitmap);
        }

        Bitmap GetResourceBitmap(string name)
        {
            string resourceName = string.Format("_19_Localization.Res.{0}", name);
            using (var stream = this.GetType().Assembly.GetManifestResourceStream(resourceName))
            {
                var bitmap = new Bitmap(stream);
                return bitmap;
            }
        }

        public void Load()
        {
        }

    }
}
