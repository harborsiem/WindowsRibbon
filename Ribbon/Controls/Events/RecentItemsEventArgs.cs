using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using RibbonLib.Interop;
//using Windows.Win32;
//using Windows.Win32.Foundation;
//using Windows.Win32.UI.Ribbon;
//using Windows.Win32.System.Com.StructuredStorage;
//using Windows.Win32.UI.Shell.PropertiesSystem;
//using Windows.Win32.System.Com;

namespace RibbonLib.Controls.Events
{
    /// <summary>
    /// The EventArgs for RecentItems
    /// </summary>
    public sealed class RecentItemsEventArgs : EventArgs
    {
        private static RibbonRecentItems s_ribbonRecent;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="selected"></param>
        /// <param name="propertySets"></param>
        /// <param name="oldPropertySets"></param>
        private RecentItemsEventArgs(SelectedItem<RecentItemsPropertySet> selected, IList<RecentItemsPropertySet> propertySets, IList<RecentItemsPropertySet> oldPropertySets)
        {
            SelectedItem = selected;
            RecentItems = propertySets;
            OldRecentItems = oldPropertySets;
        }

        /// <summary>
        /// SelectedRecentItem, can be null
        /// </summary>
        public SelectedItem<RecentItemsPropertySet> SelectedItem { get; private set; }

        /// <summary>
        /// Current RecentItemsPropertySets, can be null
        /// </summary>
        public IList<RecentItemsPropertySet> RecentItems { get; private set; }

        /// <summary>
        /// Old RecentItemsPropertySets, can be null
        /// </summary>
        public IList<RecentItemsPropertySet> OldRecentItems { get; private set; }

        /// <summary>
        /// Creates a RecentItemsEventArgs from ExecuteEventArgs of a RibbonRecentItems event
        /// </summary>
        /// <param name="sender">Parameters from event: sender = RibbonControl</param>
        /// <param name="e">Parameters from event: ExecuteEventArgs</param>
        /// <returns></returns>
        public static RecentItemsEventArgs Create(object sender, ExecuteEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));
            return Create(sender, ref e.Key.PropertyKey, ref e.CurrentValue.PropVariant, e.CommandExecutionProperties);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="key"></param>
        /// <param name="currentValue"></param>
        /// <param name="commandExecutionProperties"></param>
        /// <returns></returns>
        public static RecentItemsEventArgs Create(object sender, ref PropertyKey key, ref PropVariant currentValue, IUISimplePropertySet commandExecutionProperties)
        {
            SelectedItem<RecentItemsPropertySet> selectedRecentItem = null;
            IList<RecentItemsPropertySet> recentItems = null;
            IList<RecentItemsPropertySet> oldRecentItems = null;
            s_ribbonRecent = sender as RibbonRecentItems;
            if (s_ribbonRecent == null)
            {
                throw new ArgumentException("Parameter is not RibbonRecentItems", nameof(sender));
            }

            if (key == RibbonProperties.RecentItems)
            {
                if (currentValue.VarType == (VarEnum.VT_ARRAY | VarEnum.VT_UNKNOWN))
                {
                    // go over recent items
                    object[] objectArray = (object[])currentValue.Value;
                    recentItems = s_ribbonRecent.RecentItems;
                    oldRecentItems = new List<RecentItemsPropertySet>();
                    for (int i = 0; i < objectArray.Length; ++i)
                    {
                        IUISimplePropertySet propertySet = objectArray[i] as IUISimplePropertySet;

                        if (propertySet != null)
                        {
                            oldRecentItems.Add(recentItems[i].Clone());
                            RecentItemsPropertySet propSet = GetRecentItemProperties(propertySet, i);
                        }
                    }
                }
            }
            else if (key == RibbonProperties.SelectedItem)
            {
                // get selected item index
                uint selectedItemIndex = (uint)currentValue.Value;

                RecentItemsPropertySet propSet = GetRecentItemProperties(commandExecutionProperties, (int)selectedItemIndex);

                selectedRecentItem = new SelectedItem<RecentItemsPropertySet>((int)selectedItemIndex, propSet);
            }
            RecentItemsEventArgs e = new RecentItemsEventArgs(selectedRecentItem, recentItems, oldRecentItems);
            return e;
        }

        /// <summary>
        /// returns a RecentItemsPropertySet from IUISimplePropertySet
        /// </summary>
        /// <param name="commandExecutionProperties"></param>
        /// <param name="index">RibbonRecentItems.RecentItems index</param>
        /// <returns>RecentItemsPropertySet</returns>
        internal static RecentItemsPropertySet GetRecentItemProperties(IUISimplePropertySet commandExecutionProperties, int index)
        {
            RecentItemsPropertySet propSet = s_ribbonRecent.RecentItems[index]; //new RecentItemsPropertySet();

            // Get only the Pinned property, because the string values are the same
            //// get item label
            //commandExecutionProperties.GetValue(ref RibbonProperties.Label, out PropVariant propLabel);
            //propSet.Label = (string)propLabel.Value;
            //PropVariant.UnsafeNativeMethods.PropVariantClear(ref propLabel);

            //// get item label description
            //commandExecutionProperties.GetValue(ref RibbonProperties.LabelDescription, out PropVariant propLabelDescription);
            //propSet.LabelDescription = (string)propLabelDescription.Value;
            //PropVariant.UnsafeNativeMethods.PropVariantClear(ref propLabelDescription);

            // get item pinned value
            commandExecutionProperties.GetValue(ref RibbonProperties.Pinned, out PropVariant propPinned);
            propSet.Pinned = (bool)propPinned.Value;
            return propSet;
        }
    }
}
