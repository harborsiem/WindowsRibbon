using System;
using System.Windows.Forms;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Controls.Events;
using RibbonLib.Interop;

namespace _06_ComboBox
{
    public enum RibbonMarkupCommands : uint 
    {
         cmdButtonDropA = 1008,
         cmdButtonDropB = 1009,
         cmdButtonDropC = 1010,
         cmdButtonDropD = 1011,
         cmdButtonDropE = 1012,
         cmdButtonDropF = 1013,
         cmdTabDrop = 1014,
         cmdGroupDrop = 1015,
         cmdGroupMore = 1017,
         cmdComboBox1 = 1018,
         cmdComboBox2 = 1019,
         cmdTabSecond = 1020,
         cmdGroupSecond = 1021,
         cmdComboBox3 = 1022,
    }
        
    public partial class Form1 : Form
    {
        private RibbonButton _buttonDropA;
        private RibbonButton _buttonDropB;
        private RibbonButton _buttonDropC;
        private RibbonButton _buttonDropD;
        private RibbonButton _buttonDropE;
        private RibbonButton _buttonDropF;
        private RibbonComboBox _comboBox1;
        private RibbonComboBox _comboBox2;
        private RibbonComboBox _comboBox3;

        private UICollectionChangedEvent _uiCollectionChangedEvent;

        public Form1()
        {
            InitializeComponent();

            _buttonDropA = new RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonDropA);
            _buttonDropB = new RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonDropB);
            _buttonDropC = new RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonDropC);
            _buttonDropD = new RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonDropD);
            _buttonDropE = new RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonDropE);
            _buttonDropF = new RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonDropF);
            _comboBox1 = new RibbonComboBox(_ribbon, (uint)RibbonMarkupCommands.cmdComboBox1);
            _comboBox2 = new RibbonComboBox(_ribbon, (uint)RibbonMarkupCommands.cmdComboBox2);
            _comboBox3 = new RibbonComboBox(_ribbon, (uint)RibbonMarkupCommands.cmdComboBox3);
            _uiCollectionChangedEvent = new UICollectionChangedEvent();

            _buttonDropA.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonDropA_ExecuteEvent);
            _buttonDropB.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonDropB_ExecuteEvent);
            _buttonDropC.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonDropC_ExecuteEvent);
            _buttonDropD.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonDropD_ExecuteEvent);
            _buttonDropE.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonDropE_ExecuteEvent);
            _buttonDropF.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonDropF_ExecuteEvent);

            InitComboBoxes();
        }

        void _buttonDropA_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            // get selected item index from combo box 1
            uint selectedItemIndex = _comboBox1.SelectedItem;

            if (selectedItemIndex == Constants.UI_Collection_InvalidIndex)
            {
                MessageBox.Show("No item is selected in simple combo");
            }
            else
            {
                object selectedItem;
                _comboBox1.ItemsSource.GetItem(selectedItemIndex, out selectedItem);
                IUISimplePropertySet uiItem = (IUISimplePropertySet)selectedItem;
                PropVariant itemLabel;
                uiItem.GetValue(ref RibbonProperties.Label, out itemLabel);
                MessageBox.Show("Selected item in simple combo is: " + (string)itemLabel.Value);
            }
        }

        void _buttonDropB_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            // get string value from combo box 2
            string stringValue = _comboBox2.StringValue;
            MessageBox.Show("String value in advanced combo is: " + stringValue);
        }

        void _buttonDropC_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            // enumerate over items
            IEnumUnknown itemsSource = (IEnumUnknown)_comboBox1.ItemsSource;
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
        }

        void _buttonDropD_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            _uiCollectionChangedEvent.Attach(_comboBox1.ItemsSource);
            _uiCollectionChangedEvent.ChangedEvent += new EventHandler<UICollectionChangedEventArgs>(_uiCollectionChangedEvent_ChangedEvent);
        }

        void _buttonDropE_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            IUICollection itemsSource1 = _comboBox1.ItemsSource;
            uint count;
            itemsSource1.GetCount(out count);
            ++count;
            itemsSource1.Add(new GalleryItemPropertySet() { Label = "Label " + count.ToString(), CategoryID = Constants.UI_Collection_InvalidIndex });
        }

        void _buttonDropF_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            _uiCollectionChangedEvent.ChangedEvent -= new EventHandler<UICollectionChangedEventArgs>(_uiCollectionChangedEvent_ChangedEvent);
            _uiCollectionChangedEvent.Detach();
        }

        void _uiCollectionChangedEvent_ChangedEvent(object sender, UICollectionChangedEventArgs e)
        {
            MessageBox.Show("Got ChangedEvent. Action = " + e.Action.ToString());
        }

        private void InitComboBoxes()
        {
            _comboBox1.RepresentativeString = "Label 1";
            _comboBox2.RepresentativeString = "XXXXXXXXXXX";
            _comboBox3.RepresentativeString = "XXXXXXXXXXX";

            _comboBox1.Label = "Simple Combo";
            _comboBox2.Label = "Advanced Combo";
            _comboBox3.Label = "Another Combo";
           
            _comboBox1.ItemsSourceReady += new EventHandler<EventArgs>(_comboBox1_ItemsSourceReady);
            
            _comboBox2.CategoriesReady += new EventHandler<EventArgs>(_comboBox2_CategoriesReady);
            _comboBox2.ItemsSourceReady += new EventHandler<EventArgs>(_comboBox2_ItemsSourceReady);

            _comboBox3.ItemsSourceReady += new EventHandler<EventArgs>(_comboBox3_ItemsSourceReady);
        }
        
        void _comboBox1_ItemsSourceReady(object sender, EventArgs e)
        {       
            // set combobox1 items
            IUICollection itemsSource1 = _comboBox1.ItemsSource;
            itemsSource1.Clear();
            itemsSource1.Add(new GalleryItemPropertySet() { Label = "Label 1", CategoryID = Constants.UI_Collection_InvalidIndex });
            itemsSource1.Add(new GalleryItemPropertySet() { Label = "Label 2", CategoryID = Constants.UI_Collection_InvalidIndex });
            itemsSource1.Add(new GalleryItemPropertySet() { Label = "Label 3", CategoryID = Constants.UI_Collection_InvalidIndex });
        }

        void _comboBox2_CategoriesReady(object sender, EventArgs e)
        {
            // set _comboBox2 categories
            IUICollection categories2 = _comboBox2.Categories;
            categories2.Clear();
            categories2.Add(new GalleryItemPropertySet() { Label = "Category 1", CategoryID = 1 });
            categories2.Add(new GalleryItemPropertySet() { Label = "Category 2", CategoryID = 2 });
        }

        void _comboBox2_ItemsSourceReady(object sender, EventArgs e)
        {
            // set _comboBox2 items
            IUICollection itemsSource2 = _comboBox2.ItemsSource;
            itemsSource2.Clear();
            itemsSource2.Add(new GalleryItemPropertySet() { Label = "Label 1", CategoryID = 1 });
            itemsSource2.Add(new GalleryItemPropertySet() { Label = "Label 2", CategoryID = 1 });
            itemsSource2.Add(new GalleryItemPropertySet() { Label = "Label 3", CategoryID = 2 });
        }

        void _comboBox3_ItemsSourceReady(object sender, EventArgs e)
        {
            // set combobox3 items
            IUICollection itemsSource3 = _comboBox3.ItemsSource;
            itemsSource3.Clear();
            itemsSource3.Add(new GalleryItemPropertySet() { Label = "Label 1", CategoryID = Constants.UI_Collection_InvalidIndex });
            itemsSource3.Add(new GalleryItemPropertySet() { Label = "Label 2", CategoryID = Constants.UI_Collection_InvalidIndex });
            itemsSource3.Add(new GalleryItemPropertySet() { Label = "Label 3", CategoryID = Constants.UI_Collection_InvalidIndex });
        }
    }
}
