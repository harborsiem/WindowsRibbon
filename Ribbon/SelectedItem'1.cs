using System;
using System.Collections.Generic;

namespace RibbonLib
{
    /// <summary>
    /// Infos about the selected Item
    /// </summary>
    /// <typeparam name="T">RecentItemsPropertySet, GalleryItemPropertySet</typeparam>
    public sealed class SelectedItem<T> where T : class
    {
        internal SelectedItem(int selectedItemIndex, T propertySet)
        {
            SelectedItemIndex = selectedItemIndex;
            PropertySet = propertySet;
        }

        /// <summary>
        /// The selected Item index
        /// </summary>
        public int SelectedItemIndex { get; private set; }

        /// <summary>
        /// The selected PropertySet
        /// </summary>
        public T PropertySet { get; private set; }
    }
}
