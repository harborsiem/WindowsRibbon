using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RibbonLib.Interop;
using System.Drawing;
using System.Windows.Forms;
using MainForm;

namespace RibbonLib.Controls {
    partial class RibbonItems {

        public Control BelowRibbon { get; set; }

        public void AfterInit()
        {
            Hidden1.Enabled = false;
            Hidden2.Enabled = false;
            ButtonLabel.Enabled = false;
            Combo1.RepresentativeString = "XXXXXX";
            Combo2.RepresentativeString = "XXXXXX";
            TabHome.Label = "Home";
            InRibbonGallery.ItemsSourceReady += InRibbonGallery_ItemsSourceReady;
            ButtonDate.ExecuteEvent += ButtonDateTime_ExecuteEvent;
            ButtonDate.Label = DateTime.Now.ToShortDateString();
            Ribbon.RibbonHeightChanged += Ribbon_RibbonHeightChanged;
        }

        private void ButtonDateTime_ExecuteEvent(object sender, Events.ExecuteEventArgs e)
        {
            DatePicker dialog = new DatePicker();
            int x = System.Windows.Forms.Cursor.Position.X;
            int y = System.Windows.Forms.Cursor.Position.Y;
            dialog.Location = new Point(x, y + 10);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ButtonDate.Label = dialog.Label;
            }
        }

        private void Ribbon_RibbonHeightChanged(object sender, EventArgs e)
        {
            Control ctrl = BelowRibbon;
            int height = Ribbon.Height;
            Rectangle bounds = ctrl.Bounds;
            bounds.Height -= (height + ctrl.Margin.Top - bounds.Y);
            bounds.Y = height + ctrl.Margin.Top;
            ctrl.Bounds = bounds;
        }

        public void OnLoad()
        {
            //ButtonCut.Label = "Cut1";
        }

        public void OnShown()
        {

        }

        private void InRibbonGallery_ItemsSourceReady(object sender, EventArgs e)
        {
            // set _inRibbonGallery items
            IUICollection itemsSource = InRibbonGallery.ItemsSource;
            InRibbonGallery.ExecuteEvent += InRibbonGallery_ExecuteEvent;
            itemsSource.Clear();
            Font font = new Font("Segoe UI", 9f);
            for (int i = 0; i < 64; i++)
            {
                Bitmap image = new Bitmap(32, 32);
                Graphics g = Graphics.FromImage(image);
                g.FillRectangle(Brushes.Red, new Rectangle(new Point(3, 3), new Size(26, 26)));
                float x;
                if (i < 9)
                    x = 10f;
                else
                    x = 7f;
                g.DrawString((i + 1).ToString(), font, Brushes.White, x, 8f);
                g.Dispose();

                itemsSource.Add(new GalleryItemPropertySet()
                {
                    ItemImage = Ribbon.ConvertToUIImage((Bitmap)image),
                    CategoryID = (uint)(i + 1),
                    //Label = (i + 1).ToString(),
                });
                uint count;
                itemsSource.GetCount(out count);
                object item;
                itemsSource.GetItem(count - 1, out item);
                GalleryItemPropertySet set = item as GalleryItemPropertySet;
                if (set != null)
                {

                }

            }
        }

        private void InRibbonGallery_ExecuteEvent(object sender, Events.ExecuteEventArgs e)
        {
            IUISimplePropertySet set = e.CommandExecutionProperties;
        }
    }
}
