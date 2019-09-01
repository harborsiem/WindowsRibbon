using System;
using System.Drawing;
using System.Windows.Forms;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Controls.Events;
using RibbonLib.Interop;
using System.Diagnostics;

namespace _09_Galleries
{
    public enum RibbonMarkupCommands : uint 
    {
         cmdTabMain = 1000,
         cmdGroupDropDownGallery = 1001,
         cmdDropDownGallery = 1002,
         cmdCommandSpace = 1003,
         cmdGroupSplitButtonGallery = 1004,
         cmdSplitButtonGallery = 1005,
         cmdGroupInRibbonGallery = 1006,
         cmdInRibbonGallery = 1007,
    }

    public partial class Form1 : Form
    {
        private RibbonDropDownGallery _dropDownGallery;
        private RibbonSplitButtonGallery _splitButtonGallery;
        private RibbonButton[] _buttons;
        private RibbonInRibbonGallery _inRibbonGallery;

        public Form1()
        {
            InitializeComponent();

            _dropDownGallery = new RibbonDropDownGallery(_ribbon, (uint)RibbonMarkupCommands.cmdDropDownGallery);
            _splitButtonGallery = new RibbonSplitButtonGallery(_ribbon, (uint)RibbonMarkupCommands.cmdSplitButtonGallery);
            _inRibbonGallery = new RibbonInRibbonGallery(_ribbon, (uint)RibbonMarkupCommands.cmdInRibbonGallery);

            _dropDownGallery.ItemsSourceReady += new EventHandler<EventArgs>(_dropDownGallery_ItemsSourceReady);
            _splitButtonGallery.CategoriesReady += new EventHandler<EventArgs>(_splitButtonGallery_CategoriesReady);
            _splitButtonGallery.ItemsSourceReady += new EventHandler<EventArgs>(_splitButtonGallery_ItemsSourceReady);
            _inRibbonGallery.ItemsSourceReady += new EventHandler<EventArgs>(_inRibbonGallery_ItemsSourceReady);

            _dropDownGallery.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_dropDownGallery_ExecuteEvent);
            _dropDownGallery.PreviewEvent += new EventHandler<ExecuteEventArgs>(_dropDownGallery_OnPreview);
            _dropDownGallery.CancelPreviewEvent += new EventHandler<ExecuteEventArgs>(_dropDownGallery_OnCancelPreview);
        }

        void _dropDownGallery_ItemsSourceReady(object sender, EventArgs e)
        {
            // set label
            _dropDownGallery.Label = "Size";

            // set _dropDownGallery items
            IUICollection itemsSource = _dropDownGallery.ItemsSource;
            itemsSource.Clear();
            foreach (Image image in imageListLines.Images)
            {
                itemsSource.Add(new GalleryItemPropertySet()
                {
                    ItemImage = _ribbon.ConvertToUIImage((Bitmap)image)
                });
            }
        }

        void _splitButtonGallery_CategoriesReady(object sender, EventArgs e)
        {
            // set _splitButtonGallery categories
            IUICollection categories = _splitButtonGallery.Categories;
            categories.Clear();
            categories.Add(new GalleryItemPropertySet() { Label = "Category 1", CategoryID = 1 });
        }

        void _splitButtonGallery_ItemsSourceReady(object sender, EventArgs e)
        {
            // set label
            _splitButtonGallery.Label = "Brushes";

            // prepare helper classes for commands
            _buttons = new RibbonButton[imageListBrushes.Images.Count];
            uint i;
            for (i = 0; i < _buttons.Length; ++i)
            {
                _buttons[i] = new RibbonButton(_ribbon, 2000 + i)
                {
                    Label = "Label " + i.ToString(),
                    LargeImage = _ribbon.ConvertToUIImage((Bitmap)imageListBrushes.Images[(int)i])
                };
            }

            // set _splitButtonGallery items
            IUICollection itemsSource = _splitButtonGallery.ItemsSource;
            itemsSource.Clear();
            i = 0;
            foreach (Image image in imageListBrushes.Images)
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
            IUICollection itemsSource = _inRibbonGallery.ItemsSource;
            itemsSource.Clear();
            foreach (Image image in imageListShapes.Images)
            {
                itemsSource.Add(new GalleryItemPropertySet()
                {
                    ItemImage = _ribbon.ConvertToUIImage((Bitmap)image)
                });
            }
        }

        void _dropDownGallery_OnCancelPreview(object sender, ExecuteEventArgs e)
        {
            Debug.WriteLine("DropDownGallery::OnCancelPreview");
        }

        void _dropDownGallery_OnPreview(object sender, ExecuteEventArgs e)
        {
            Debug.WriteLine("DropDownGallery::OnPreview");
        }

        void _dropDownGallery_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            Debug.WriteLine("DropDownGallery::ExecuteEvent");
        }
    }
}
