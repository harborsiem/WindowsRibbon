using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RibbonLib.Interop;
using System.Drawing;

namespace RibbonLib.Controls
{
    partial class RibbonItems
    {

        public void AfterInit()
        {
            TabHome.Label = "Home";
            InRibbonGallery.ItemsSourceReady += InRibbonGallery_ItemsSourceReady;
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
                g.FillRectangle(Brushes.Red, new Rectangle(new Point(3, 3) , new Size(26, 26)));
                g.DrawString((i + 1).ToString(), font, Brushes.White, 10f, 10f);
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
