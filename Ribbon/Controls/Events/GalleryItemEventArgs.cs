using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RibbonLib.Interop;

namespace RibbonLib.Controls.Events
{
    /// <summary>
    /// The EventArgs for GalleryItem
    /// </summary>
    public sealed class GalleryItemEventArgs : EventArgs
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="selected"></param>
        private GalleryItemEventArgs(SelectedItem<GalleryItemPropertySet> selected)
        {
            SelectedItem = selected;
        }

        /// <summary>
        /// The selected Item index
        /// </summary>
        public SelectedItem<GalleryItemPropertySet> SelectedItem { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e">Parameters from event: ExecuteEventArgs</param>
        /// <returns></returns>
        public static GalleryItemEventArgs Create(ExecuteEventArgs e)
        {
            return Create(ref e.Key.PropertyKey, ref e.CurrentValue.PropVariant, e.CommandExecutionProperties);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="currentValue"></param>
        /// <param name="commandExecutionProperties"></param>
        /// <returns></returns>
        public static GalleryItemEventArgs Create(ref PropertyKey key, ref PropVariant currentValue, IUISimplePropertySet commandExecutionProperties)
        {
            GalleryItemPropertySet propSet = null;
            SelectedItem<GalleryItemPropertySet> selected = null;
            int selectedItemIndex = -1;
            if (key == RibbonProperties.SelectedItem)
            {
                // get selected item index
                selectedItemIndex = (int)(uint)currentValue.Value;

                if (commandExecutionProperties != null)
                    propSet = GetGalleryItemProperty(commandExecutionProperties);
                selected = new SelectedItem<GalleryItemPropertySet>(selectedItemIndex, propSet);
            }
            GalleryItemEventArgs e = new GalleryItemEventArgs(selected);
            return e;
        }

        /// <summary>
        /// returns a GalleryItemPropertySet from IUISimplePropertySet
        /// </summary>
        /// <param name="commandExecutionProperties"></param>
        /// <returns>GalleryItemPropertySet</returns>
        internal static GalleryItemPropertySet GetGalleryItemProperty(IUISimplePropertySet commandExecutionProperties)
        {
            GalleryItemPropertySet propSet = commandExecutionProperties as GalleryItemPropertySet;

            if (propSet == null)
            {
                propSet = new GalleryItemPropertySet();
                // get item label
                commandExecutionProperties.GetValue(ref RibbonProperties.Label, out PropVariant propLabel);
                propSet.Label = (string)propLabel.Value;
                PropVariant.UnsafeNativeMethods.PropVariantClear(ref propLabel);

                // get item CategoryID value
                commandExecutionProperties.GetValue(ref RibbonProperties.CategoryID, out PropVariant propCategoryID);
                propSet.CategoryID = (uint)propCategoryID.Value;

                // get item ItemImage value
                commandExecutionProperties.GetValue(ref RibbonProperties.ItemImage, out PropVariant propItemImage);
                propSet.ItemImage = (IUIImage)propItemImage.Value;
            }
            return propSet;
        }
    }
}
