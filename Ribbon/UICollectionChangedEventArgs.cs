//*****************************************************************************
//
//  File:       UICollectionChangedEventArgs.cs
//
//  Contents:   UI collection changed event arguments
//
//*****************************************************************************

using System;
using RibbonLib.Interop;

namespace RibbonLib
{
    /// <summary>
    /// 
    /// </summary>
    public class UICollectionChangedEventArgs : EventArgs
    {
        private CollectionChange _action;
        private uint _oldIndex;
        private object _oldItem;
        private uint _newIndex;
        private object _newItem;

        /// <summary>
        /// EventArgs when UICollection changed
        /// </summary>
        /// <param name="action"></param>
        /// <param name="oldIndex"></param>
        /// <param name="oldItem"></param>
        /// <param name="newIndex"></param>
        /// <param name="newItem"></param>
        public UICollectionChangedEventArgs(CollectionChange action, uint oldIndex, object oldItem, uint newIndex, object newItem)
        {
            _action = action;
            _oldIndex = oldIndex;
            _oldItem = oldItem;
            _newIndex = newIndex;
            _newItem = newItem;
        }

        /// <summary>
        /// Collection change action
        /// </summary>
        public CollectionChange Action
        {
            get
            {
                return _action;
            }
        }

        /// <summary>
        /// The old index
        /// </summary>
        public uint OldIndex
        {
            get
            {
                return _oldIndex;
            }
        }

        /// <summary>
        /// The old item
        /// </summary>
        public object OldItem
        {
            get
            {
                return _oldItem;
            }
        }

        /// <summary>
        /// The new Index
        /// </summary>
        public uint NewIndex
        {
            get
            {
                return _newIndex;
            }
        }

        /// <summary>
        /// The new Item
        /// </summary>
        public object NewItem
        {
            get
            {
                return _newItem;
            }
        }
    }
}
