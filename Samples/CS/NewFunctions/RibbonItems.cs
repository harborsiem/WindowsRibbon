using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using RibbonLib.Controls.Events;
using RibbonLib.Interop;

namespace RibbonLib.Controls
{
    partial class RibbonItems
    {
        UICollection<QatCommandPropertySet> _qatSet;
        private IUIImage _checkNormal;
        private IUIImage _checkChecked;

        public void Init()
        {
            Ribbon.RibbonHeightChanged += Ribbon_RibbonHeightChanged;
            Ribbon.ViewCreated += Ribbon_ViewCreated;
            Ribbon.ViewDestroy += Ribbon_ViewDestroy;
            InRibbon.CategoriesReady += InRibbon_CategoriesReady;
            InRibbon.ItemsSourceReady += InRibbon_ItemsSourceReady;
            FontPicker.ExecuteEvent += FontPicker_ExecuteEvent;
            FontPicker.PreviewEvent += FontPicker_PreviewEvent;
            FontPicker.CancelPreviewEvent += FontPicker_CancelPreviewEvent;
            RecentItems.ExecuteEvent += RecentItems_ExecuteEvent;
            ComboBox.CategoriesReady += ComboBox_CategoriesReady;
            ComboBox.ItemsSourceReady += ComboBox_ItemsSourceReady;
            ComboBox.ExecuteEvent += ComboBox_ExecuteEvent;
            ComboBox.PreviewEvent += ComboBox_PreviewEvent;
            ComboBox.CancelPreviewEvent += ComboBox_CancelPreviewEvent;
            ComboBox.RepresentativeString = "XXXXXXXXXX";

            DropDownColor.ExecuteEvent += DropDownColor_ExecuteEvent;
            DropDownColor.PreviewEvent += DropDownColor_PreviewEvent;
            DropDownColor.CancelPreviewEvent += DropDownColor_CancelPreviewEvent;
            Qat.ExecuteEvent += Qat_ExecuteEvent;
        }

        #region Qat

        private void Qat_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            MessageBox.Show("From Qat customize button");
        }

        private void QatSet_ChangedEvent(object sender, UICollectionChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        #endregion Qat

        #region DropDownColorPicker

        private void DropDownColor_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            ColorPickerEventArgs args = ColorPickerEventArgs.Create(e);
            SwatchColorType colorType = args.ColorType;
            Color? color = args.RGBColor;
        }

        private void DropDownColor_PreviewEvent(object sender, ExecuteEventArgs e)
        {
            ColorPickerEventArgs args = ColorPickerEventArgs.Create(e);
            SwatchColorType colorType = args.ColorType;
            Color? color = args.RGBColor;
        }

        private void DropDownColor_CancelPreviewEvent(object sender, ExecuteEventArgs e)
        {
            ColorPickerEventArgs args = ColorPickerEventArgs.Create(e);
            SwatchColorType colorType = args.ColorType;
            Color? color = args.RGBColor;
        }

        #endregion DropDownColorPicker

        #region ComboBox

        private void ComboBox_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            GalleryItemEventArgs args = GalleryItemEventArgs.Create(e);
            SelectedItem<GalleryItemPropertySet> item = args.SelectedItem;
        }

        private void ComboBox_PreviewEvent(object sender, ExecuteEventArgs e)
        {
            GalleryItemEventArgs args = GalleryItemEventArgs.Create(e);
            SelectedItem<GalleryItemPropertySet> item = args.SelectedItem;
        }

        private void ComboBox_CancelPreviewEvent(object sender, ExecuteEventArgs e)
        {
            GalleryItemEventArgs args = GalleryItemEventArgs.Create(e);
            SelectedItem<GalleryItemPropertySet> item = args.SelectedItem;
        }

        private void ComboBox_CategoriesReady(object sender, EventArgs e)
        {
            ComboBox.GalleryCategories.Add(new GalleryItemPropertySet() { CategoryID = 0 });
        }

        private void ComboBox_ItemsSourceReady(object sender, EventArgs e)
        {
            ComboBox.GalleryItemItemsSource.Add(new GalleryItemPropertySet() { Label = "_1", CategoryID = 0 });
            ComboBox.GalleryItemItemsSource.Add(new GalleryItemPropertySet() { Label = "_2", CategoryID = 0 });
            ComboBox.GalleryItemItemsSource.Add(new GalleryItemPropertySet() { Label = "_3", CategoryID = 0 });
            ComboBox.GalleryItemItemsSource.Add(new GalleryItemPropertySet() { Label = "_4", CategoryID = 0 });
        }

        #endregion ComboBox

        #region RecentItems

        private void RecentItems_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            bool selectedChanged = false;
            bool pinnedChanged = false;
            RecentItemsEventArgs args = RecentItemsEventArgs.Create(sender, e);
            SelectedItem<RecentItemsPropertySet> item = args.SelectedItem;
            if (item != null)
            {
                selectedChanged = true;
				return;
            }
            IList<RecentItemsPropertySet> current = args.RecentItems;
            IList<RecentItemsPropertySet> old = args.OldRecentItems;
            if (current != null)
            {
                pinnedChanged = true;
            }
        }

        private void InitRecentItems()
        {
            // prepare list of recent items
            IList<RecentItemsPropertySet> recentItems = RecentItems.RecentItems;
            recentItems.Add(new RecentItemsPropertySet()
            {
                Label = "Recent item 1",
                LabelDescription = "Recent item 1 description",
                Pinned = true
            });
            recentItems.Add(new RecentItemsPropertySet()
            {
                Label = "Recent item 2",
                LabelDescription = "Recent item 2 description",
                Pinned = false
            });
        }

        #endregion RecentItems

        #region FontControl

        private void FontPicker_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            FontControlEventArgs args = FontControlEventArgs.Create(e);
            Dictionary<string, object> changedFontValues = args.ChangedValues;
            FontPropertyStore store = args.CurrentFontStore;
        }

        private void FontPicker_PreviewEvent(object sender, ExecuteEventArgs e)
        {
            FontControlEventArgs args = FontControlEventArgs.Create(e);
            Dictionary<string, object> changedFont = args.ChangedValues;
            FontPropertyStore store = args.CurrentFontStore;
            FontDeltaSize? dSize = store.DeltaSize;
        }

        private void FontPicker_CancelPreviewEvent(object sender, ExecuteEventArgs e)
        {
            FontControlEventArgs args = FontControlEventArgs.Create(e);
            Dictionary<string, object> changedFont = args.ChangedValues;
            FontPropertyStore store = args.CurrentFontStore;
        }

        #endregion FontControl

        #region InRibbon

        private void DrawCheckBoxImages()
        {
            float dpix;
            int widthAndHeight;
            using (Graphics g = Ribbon.CreateGraphics())
            {
                dpix = g.DpiX;
            }
            if (dpix <= 96.0)
                widthAndHeight = 16;
            else if (dpix <= 120.0)
                widthAndHeight = 20;
            else if (dpix <= 144.0)
                widthAndHeight = 24;
            else
                widthAndHeight = 32;
            Bitmap bitmap = new Bitmap(widthAndHeight, widthAndHeight);
            ControlPaint.DrawCheckBox(Graphics.FromImage(bitmap), 0, 0, bitmap.Width, bitmap.Height, ButtonState.Flat | ButtonState.Normal);
            IUIImage image = Ribbon.ConvertToUIImage(bitmap);
            bitmap.Dispose();
            _checkNormal = image;
            bitmap = new Bitmap(widthAndHeight, widthAndHeight);
            ControlPaint.DrawCheckBox(Graphics.FromImage(bitmap), 0, 0, bitmap.Width, bitmap.Height, ButtonState.Flat | ButtonState.Checked);
            image = Ribbon.ConvertToUIImage(bitmap);
            _checkChecked = image;
            bitmap.Dispose();
        }

        private void InRibbon_CategoriesReady(object sender, EventArgs e)
        {
            InRibbon.GalleryCategories.Add(new GalleryItemPropertySet() { CategoryID = 0 });
        }

        private void InRibbon_ItemsSourceReady(object sender, EventArgs e)
        {
            DrawCheckBoxImages();
            RibbonToggleButton toggle1 = new RibbonToggleButton(Ribbon, 500);
            toggle1.ExecuteEvent += Toggle1_ExecuteEvent;
            toggle1.Label = "T1";
            toggle1.SmallImage = _checkNormal;
            InRibbon.GalleryCommandItemsSource.Add(new GalleryCommandPropertySet()
            {
                CategoryID = 0,
                CommandID = toggle1.CommandID,
                CommandType = CommandType.Boolean,
                RibbonCtrl = toggle1
            });
            RibbonButton button1 = new RibbonButton(Ribbon, 501);
            button1.ExecuteEvent += Button1_ExecuteEvent;
            button1.Label = "B1";
            //toggle1.SmallImage = _checkNormal;
            InRibbon.GalleryCommandItemsSource.Add(new GalleryCommandPropertySet()
            {
                CategoryID = 0,
                CommandID = button1.CommandID,
                CommandType = CommandType.Action,
                RibbonCtrl = button1
            });
            RibbonCheckBox checkBox1 = new RibbonCheckBox(Ribbon, 502);
            checkBox1.ExecuteEvent += CheckBox1_ExecuteEvent;
            checkBox1.Label = "C1";
            checkBox1.SmallImage = _checkNormal;
            InRibbon.GalleryCommandItemsSource.Add(new GalleryCommandPropertySet()
            {
                CategoryID = 0,
                CommandID = checkBox1.CommandID,
                CommandType = CommandType.Boolean,
                RibbonCtrl = checkBox1
            });
            //no other Ribbon controls in the InRibbonGallery possible
        }

        private void CheckBox1_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            RibbonCheckBox b = sender as RibbonCheckBox;
            MessageBox.Show(b.Label, b.CommandID.ToString());
        }

        private void Button1_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            RibbonButton b = sender as RibbonButton;
            MessageBox.Show(b.Label, b.CommandID.ToString());
        }

        private void Toggle1_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            RibbonToggleButton b = sender as RibbonToggleButton;
            if (b != null)
            {
                if (b.BooleanValue)
                {
                    b.SmallImage = _checkChecked;
                }
                else
                {
                    b.SmallImage = _checkNormal;
                }
                MessageBox.Show(b.Label, b.CommandID.ToString());
            }
        }

        #endregion InRibbon

        private void Ribbon_RibbonHeightChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void Ribbon_ViewCreated(object sender, EventArgs e)
        {
            _qatSet = Qat.QatItemsSource;
            _qatSet.ChangedEvent += QatSet_ChangedEvent;
            InitRecentItems();
        }

        private void Ribbon_ViewDestroy(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        public void Load()
        {
        }
    }
}
