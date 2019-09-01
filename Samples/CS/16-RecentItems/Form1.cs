using System;
using System.Windows.Forms;

using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Interop;
using RibbonLib.Controls.Events;
using System.Collections.Generic;

namespace _16_RecentItems
{
    public enum RibbonMarkupCommands : uint
    {
        cmdApplicationMenu = 1000,
        cmdButtonNew = 1001,
        cmdButtonOpen = 1002,
        cmdButtonSave = 1003,
        cmdButtonExit = 1004,
        cmdRecentItems = 1005,
    }

    public partial class Form1 : Form
    {
        private RibbonRecentItems _ribbonRecentItems;

        List<RecentItemsPropertySet> _recentItems;

        public Form1()
        {
            InitializeComponent();

            _ribbonRecentItems = new RibbonRecentItems(_ribbon, (uint)RibbonMarkupCommands.cmdRecentItems);

            _ribbonRecentItems.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_recentItems_ExecuteEvent);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitRecentItems();
        }

        private void InitRecentItems()
        {
            // prepare list of recent items
            _recentItems = new List<RecentItemsPropertySet>();
            _recentItems.Add(new RecentItemsPropertySet()
                             {
                                 Label = "Recent item 1",
                                 LabelDescription = "Recent item 1 description",
                                 Pinned = true
                             });
            _recentItems.Add(new RecentItemsPropertySet()
                             {
                                 Label = "Recent item 2",
                                 LabelDescription = "Recent item 2 description",
                                 Pinned = false
                             });

            _ribbonRecentItems.RecentItems = _recentItems;
        }

        void _recentItems_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            if (e.Key.PropertyKey == RibbonProperties.RecentItems)
            {
                // go over recent items
                object[] objectArray = (object[])e.CurrentValue.PropVariant.Value;
                for (int i = 0; i < objectArray.Length; ++i)
                {
                    IUISimplePropertySet propertySet = objectArray[i] as IUISimplePropertySet;

                    if (propertySet != null)
                    {
                        PropVariant propLabel;
                        propertySet.GetValue(ref RibbonProperties.Label, 
                                             out propLabel);
                        string label = (string)propLabel.Value;

                        PropVariant propLabelDescription;
                        propertySet.GetValue(ref RibbonProperties.LabelDescription, 
                                             out propLabelDescription);
                        string labelDescription = (string)propLabelDescription.Value;

                        PropVariant propPinned;
                        propertySet.GetValue(ref RibbonProperties.Pinned, 
                                             out propPinned);
                        bool pinned = (bool)propPinned.Value;

                        // update pinned value
                        _recentItems[i].Pinned = pinned;
                    }
                }
            }
            else if (e.Key.PropertyKey == RibbonProperties.SelectedItem)
            {
                // get selected item index
                uint selectedItem = (uint)e.CurrentValue.PropVariant.Value;

                // get selected item label
                PropVariant propLabel;
                e.CommandExecutionProperties.GetValue(ref RibbonProperties.Label, 
                                                    out propLabel);
                string label = (string)propLabel.Value;

                // get selected item label description
                PropVariant propLabelDescription;
                e.CommandExecutionProperties.GetValue(ref RibbonProperties.LabelDescription, 
                                                    out propLabelDescription);
                string labelDescription = (string)propLabelDescription.Value;

                // get selected item pinned value
                PropVariant propPinned;
                e.CommandExecutionProperties.GetValue(ref RibbonProperties.Pinned, 
                                                    out propPinned);
                bool pinned = (bool)propPinned.Value;
            }
        }
    }
}
