//#define Old
using System;
using System.Windows.Forms;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Controls.Events;
using RibbonLib.Interop;

namespace RibbonLib.Controls
{
    partial class RibbonItems
    {
#if Old
        private UICollectionChangedEvent _uiCollectionChangedEvent;
#endif

        public void Init()
        {
#if Old
            _uiCollectionChangedEvent = new UICollectionChangedEvent();
#endif
            ButtonDropA.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonDropA_ExecuteEvent);
            ButtonDropB.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonDropB_ExecuteEvent);
            ButtonDropC.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonDropC_ExecuteEvent);
            ButtonDropD.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonDropD_ExecuteEvent);
            ButtonDropE.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonDropE_ExecuteEvent);
            ButtonDropF.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonDropF_ExecuteEvent);

            InitComboBoxes();
        }

        void _buttonDropA_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            // get selected item index from combo box 1
            uint selectedItemIndex = ComboBox1.SelectedItem;

            if (selectedItemIndex == Constants.UI_Collection_InvalidIndex)
            {
                MessageBox.Show("No item is selected in simple combo");
            }
            else
            {
#if Old
                object selectedItem;
                ComboBox1.ItemsSource.GetItem(selectedItemIndex, out selectedItem);
                IUISimplePropertySet uiItem = (IUISimplePropertySet)selectedItem;
                PropVariant itemLabel;
                uiItem.GetValue(ref RibbonProperties.Label, out itemLabel);
                MessageBox.Show("Selected item in simple combo is: " + (string)itemLabel.Value);
#else
                string label = ComboBox1.GalleryItemItemsSource[(int)selectedItemIndex].Label;
                MessageBox.Show("Selected item in simple combo is: " + label);
#endif
            }
        }

        void _buttonDropB_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            // get string value from combo box 2
            string stringValue = ComboBox2.StringValue;
            MessageBox.Show("String value in advanced combo is: " + stringValue);
        }

        void _buttonDropC_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
#if Old
            // enumerate over items
            IEnumUnknown itemsSource = (IEnumUnknown)ComboBox1.ItemsSource;
            itemsSource.Reset();
            object[] items = new object[1];
            uint fetchedItem;
            while (itemsSource.Next(1, items, out fetchedItem) == HRESULT.S_OK)
            {
                IUISimplePropertySet uiItem = (IUISimplePropertySet)items[0];
                PropVariant itemLabel;
                uiItem.GetValue(ref RibbonProperties.Label, out itemLabel);
                MessageBox.Show("Label = " + (string)itemLabel.Value);
            }
#else
            foreach (GalleryItemPropertySet propSet in ComboBox1.GalleryItemItemsSource)
            {
                string label = propSet.Label;
                MessageBox.Show("Label = " + label);
            }
#endif
        }

        void _buttonDropD_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
#if Old
            _uiCollectionChangedEvent.Attach(ComboBox1.ItemsSource);
            _uiCollectionChangedEvent.ChangedEvent += new EventHandler<UICollectionChangedEventArgs>(_uiCollectionChangedEvent_ChangedEvent);
#else
            ComboBox1.GalleryItemItemsSource.ChangedEvent += new EventHandler<UICollectionChangedEventArgs>(_uiCollectionChangedEvent_ChangedEvent);
#endif
        }

        void _buttonDropE_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
#if Old
            IUICollection itemsSource1 = ComboBox1.ItemsSource;
            uint count;
            itemsSource1.GetCount(out count);
            ++count;
            itemsSource1.Add(new GalleryItemPropertySet() { Label = "Label " + count.ToString(), CategoryID = Constants.UI_Collection_InvalidIndex });
#else
            UICollection<GalleryItemPropertySet> itemsSource1 = ComboBox1.GalleryItemItemsSource;
            int count = itemsSource1.Count;
            count++;
            itemsSource1.Add(new GalleryItemPropertySet() { Label = "Label " + count.ToString(), CategoryID = Constants.UI_Collection_InvalidIndex });
#endif
        }

        void _buttonDropF_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
#if Old
            _uiCollectionChangedEvent.ChangedEvent -= new EventHandler<UICollectionChangedEventArgs>(_uiCollectionChangedEvent_ChangedEvent);
            _uiCollectionChangedEvent.Detach();
#else
            ComboBox1.GalleryItemItemsSource.ChangedEvent -= new EventHandler<UICollectionChangedEventArgs>(_uiCollectionChangedEvent_ChangedEvent);
#endif
        }

        void _uiCollectionChangedEvent_ChangedEvent(object sender, UICollectionChangedEventArgs e)
        {
            MessageBox.Show("Got ChangedEvent. Action = " + e.Action.ToString());
        }

        private void InitComboBoxes()
        {
            ComboBox1.RepresentativeString = "Label 1";
            ComboBox2.RepresentativeString = "XXXXXXXXXXX";
            ComboBox3.RepresentativeString = "XXXXXXXXXXX";

            ComboBox1.Label = "Simple Combo";
            ComboBox2.Label = "Advanced Combo";
            ComboBox3.Label = "Another Combo";

            ComboBox1.ItemsSourceReady += new EventHandler<EventArgs>(_comboBox1_ItemsSourceReady);

            ComboBox2.CategoriesReady += new EventHandler<EventArgs>(_comboBox2_CategoriesReady);
            ComboBox2.ItemsSourceReady += new EventHandler<EventArgs>(_comboBox2_ItemsSourceReady);

            ComboBox3.ItemsSourceReady += new EventHandler<EventArgs>(_comboBox3_ItemsSourceReady);
        }

        void _comboBox1_ItemsSourceReady(object sender, EventArgs e)
        {
            // set combobox1 items
#if Old
            IUICollection itemsSource1 = ComboBox1.ItemsSource;
#else
            UICollection<GalleryItemPropertySet> itemsSource1 = ComboBox1.GalleryItemItemsSource;
#endif
            itemsSource1.Clear();
            itemsSource1.Add(new GalleryItemPropertySet() { Label = "Label 1", CategoryID = Constants.UI_Collection_InvalidIndex });
            itemsSource1.Add(new GalleryItemPropertySet() { Label = "Label 2", CategoryID = Constants.UI_Collection_InvalidIndex });
            itemsSource1.Add(new GalleryItemPropertySet() { Label = "Label 3", CategoryID = Constants.UI_Collection_InvalidIndex });

        }

        void _comboBox2_CategoriesReady(object sender, EventArgs e)
        {
            // set _comboBox2 categories
#if Old
            IUICollection categories2 = ComboBox2.Categories;
#else
            UICollection<GalleryItemPropertySet> categories2 = ComboBox2.GalleryCategories;
#endif
            categories2.Clear();
            categories2.Add(new GalleryItemPropertySet() { Label = "Category 1", CategoryID = 1 });
            categories2.Add(new GalleryItemPropertySet() { Label = "Category 2", CategoryID = 2 });
        }

        void _comboBox2_ItemsSourceReady(object sender, EventArgs e)
        {
            // set _comboBox2 items
#if Old
            IUICollection itemsSource2 = ComboBox2.ItemsSource;
#else
            UICollection<GalleryItemPropertySet> itemsSource2 = ComboBox2.GalleryItemItemsSource;
#endif
            itemsSource2.Clear();
            itemsSource2.Add(new GalleryItemPropertySet() { Label = "Label 1", CategoryID = 1 });
            itemsSource2.Add(new GalleryItemPropertySet() { Label = "Label 2", CategoryID = 1 });
            itemsSource2.Add(new GalleryItemPropertySet() { Label = "Label 3", CategoryID = 2 });
        }

        void _comboBox3_ItemsSourceReady(object sender, EventArgs e)
        {
            // set combobox3 items
#if Old
            IUICollection itemsSource3 = ComboBox3.ItemsSource;
#else
            UICollection<GalleryItemPropertySet> itemsSource3 = ComboBox3.GalleryItemItemsSource;
#endif
            itemsSource3.Clear();
            itemsSource3.Add(new GalleryItemPropertySet() { Label = "Label 1", CategoryID = Constants.UI_Collection_InvalidIndex });
            itemsSource3.Add(new GalleryItemPropertySet() { Label = "Label 2", CategoryID = Constants.UI_Collection_InvalidIndex });
            itemsSource3.Add(new GalleryItemPropertySet() { Label = "Label 3", CategoryID = Constants.UI_Collection_InvalidIndex });
        }

        public void Load()
        {
        }

    }
}
