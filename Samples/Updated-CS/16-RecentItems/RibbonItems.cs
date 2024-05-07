//#define Old
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Controls.Events;
using RibbonLib.Interop;

namespace RibbonLib.Controls
{
    partial class RibbonItems
    {
        private IList<RecentItemsPropertySet> _recentItems;

        public void Init()
        {
            RecentItems.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_recentItems_ExecuteEvent);
        }

        private void InitRecentItems()
        {
            // prepare list of recent items
#if Old
            _recentItems = new List<RecentItemsPropertySet>();
#else
            _recentItems = RecentItems.RecentItems;
#endif
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
#if Old
            RecentItems.RecentItems = _recentItems;
#endif
        }

        void _recentItems_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
#if Old
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
#else
            RecentItemsEventArgs args = RecentItemsEventArgs.Create(sender, e);
            if (args.RecentItems != null)
            {
                for (int i = 0; i < args.RecentItems.Count; ++i) {
                    string label = args.RecentItems[i].Label;
                    string labelDescription = args.RecentItems[i].LabelDescription;
                    bool pinned = args.RecentItems[i].Pinned;
                }
            }
            if (args.SelectedItem != null)
            {
                SelectedItem<RecentItemsPropertySet> selected = args.SelectedItem;
                int selectedItem = selected.SelectedItemIndex;
                string label = args.SelectedItem.PropertySet.Label;
                string labelDescription = args.SelectedItem.PropertySet.LabelDescription;
                bool pinned = args.SelectedItem.PropertySet.Pinned;
            }
            int maxCount = RecentItems.MaxCount;
#endif
        }

        public void Load()
        {
            InitRecentItems();
        }
    }
}
