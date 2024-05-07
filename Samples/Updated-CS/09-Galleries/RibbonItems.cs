//#define Old
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Controls.Events;
using RibbonLib.Interop;
using _09_Galleries;

namespace RibbonLib.Controls
{
    partial class RibbonItems
    {
        private RibbonButton[] _buttons;
        private Form1 _form;

        public void Init(Form1 form)
        {
            _form = form;
            DropDownGallery.ItemsSourceReady += new EventHandler<EventArgs>(_dropDownGallery_ItemsSourceReady);
            SplitButtonGallery.CategoriesReady += new EventHandler<EventArgs>(_splitButtonGallery_CategoriesReady);
            SplitButtonGallery.ItemsSourceReady += new EventHandler<EventArgs>(_splitButtonGallery_ItemsSourceReady);
            InRibbonGallery.ItemsSourceReady += new EventHandler<EventArgs>(_inRibbonGallery_ItemsSourceReady);

            DropDownGallery.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_dropDownGallery_ExecuteEvent);
            DropDownGallery.PreviewEvent += new EventHandler<ExecuteEventArgs>(_dropDownGallery_OnPreview);
            DropDownGallery.CancelPreviewEvent += new EventHandler<ExecuteEventArgs>(_dropDownGallery_OnCancelPreview);
        }

        void _dropDownGallery_ItemsSourceReady(object sender, EventArgs e)
        {
            // set label
            DropDownGallery.Label = "Size";

            // set _dropDownGallery items
#if Old
            IUICollection itemsSource = DropDownGallery.ItemsSource;
#else
            UICollection<GalleryItemPropertySet> itemsSource = DropDownGallery.GalleryItemItemsSource;
#endif
            itemsSource.Clear();
            foreach (Image image in _form.ImageListLines.Images)
            {
                itemsSource.Add(new GalleryItemPropertySet()
                {
                    ItemImage = Ribbon.ConvertToUIImage((Bitmap)image)
                });
            }
        }

        void _splitButtonGallery_CategoriesReady(object sender, EventArgs e)
        {
            // set _splitButtonGallery categories
#if Old
            IUICollection categories = SplitButtonGallery.Categories;
#else
            UICollection<GalleryItemPropertySet> categories = SplitButtonGallery.GalleryCategories;
#endif
            categories.Clear();
            categories.Add(new GalleryItemPropertySet() { Label = "Category 1", CategoryID = 1 });
        }

        void _splitButtonGallery_ItemsSourceReady(object sender, EventArgs e)
        {
            // set label
            SplitButtonGallery.Label = "Brushes";

            // prepare helper classes for commands
            _buttons = new RibbonButton[_form.ImageListBrushes.Images.Count];
            uint i;
            for (i = 0; i < _buttons.Length; ++i)
            {
                _buttons[i] = new RibbonButton(Ribbon, 2000 + i)
                {
                    Label = "Label " + i.ToString(),
                    LargeImage = Ribbon.ConvertToUIImage((Bitmap)_form.ImageListBrushes.Images[(int)i])
                };
            }

            // set _splitButtonGallery items
#if Old
            IUICollection itemsSource = SplitButtonGallery.ItemsSource;
#else
            UICollection<GalleryCommandPropertySet> itemsSource = SplitButtonGallery.GalleryCommandItemsSource;
#endif
            itemsSource.Clear();
            i = 0;
            foreach (Image image in _form.ImageListBrushes.Images)
            {
                itemsSource.Add(new GalleryCommandPropertySet()
                {
                    CommandID = 2000 + i++,
                    CommandType = CommandType.Action,
                    CategoryID = 1
                });
            }
        }

        void _inRibbonGallery_ItemsSourceReady(object sender, EventArgs e)
        {
            // set _inRibbonGallery items
#if Old
            IUICollection itemsSource = InRibbonGallery.ItemsSource;
#else
            UICollection<GalleryItemPropertySet> itemsSource = InRibbonGallery.GalleryItemItemsSource;
#endif
            itemsSource.Clear();
            foreach (Image image in _form.ImageListShapes.Images)
            {
                itemsSource.Add(new GalleryItemPropertySet()
                {
                    ItemImage = Ribbon.ConvertToUIImage((Bitmap)image)
                });
            }
        }

        void _dropDownGallery_OnCancelPreview(object sender, ExecuteEventArgs e)
        {
#if !Old
            GalleryItemEventArgs args = GalleryItemEventArgs.Create(sender, e);
            SelectedItem<GalleryItemPropertySet> item = args.SelectedItem;
#endif
            Debug.WriteLine("DropDownGallery::OnCancelPreview");
        }

        void _dropDownGallery_OnPreview(object sender, ExecuteEventArgs e)
        {
#if !Old
            GalleryItemEventArgs args = GalleryItemEventArgs.Create(sender, e);
            SelectedItem<GalleryItemPropertySet> item = args.SelectedItem;
#endif
            Debug.WriteLine("DropDownGallery::OnPreview");
        }

        void _dropDownGallery_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
#if !Old
            GalleryItemEventArgs args = GalleryItemEventArgs.Create(sender, e);
            SelectedItem<GalleryItemPropertySet> item = args.SelectedItem;
            Debug.WriteLine("DropDownGallery::Selected = " + item.SelectedItemIndex);
#endif
            Debug.WriteLine("DropDownGallery::ExecuteEvent");
        }

        public void Load()
        {
        }

    }
}
