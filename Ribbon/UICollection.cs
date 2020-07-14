//*****************************************************************************
//
//  File:       UICollection.cs
//
//  Contents:   Helper class that provides an implementation of the 
//              IUICollection interface.
//
//*****************************************************************************

using System.Collections.Generic;
using RibbonLib.Interop;

namespace RibbonLib
{
    /// <summary>
    /// Helper class that provides an implementation of the 
    /// IUICollection interface.
    /// </summary>
    public class UICollection : IUICollection
    {
        private List<object> _items = new List<object>();

        #region IUICollection Members

        /// <summary>
        /// Retrieves the count of the collection
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public HRESULT GetCount(out uint count)
        {
            count = (uint)_items.Count;
            return HRESULT.S_OK;
        }

        /// <summary>
        /// Retrieves an item
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public HRESULT GetItem(uint index, out object item)
        {
            item = _items[(int)index];
            return HRESULT.S_OK;
        }

        /// <summary>
        /// Adds an item to the end
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public HRESULT Add(object item)
        {
            _items.Add(item);
            return HRESULT.S_OK;
        }

        /// <summary>
        /// Inserts an item
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public HRESULT Insert(uint index, object item)
        {
            _items.Insert((int)index, item);
            return HRESULT.S_OK;
        }

        /// <summary>
        /// Removes an item at the specified position
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public HRESULT RemoveAt(uint index)
        {
            _items.RemoveAt((int)index);
            return HRESULT.S_OK;
        }

        /// <summary>
        /// Replaces an item at the specified position
        /// </summary>
        /// <param name="indexReplaced"></param>
        /// <param name="itemReplaceWith"></param>
        /// <returns></returns>
        public HRESULT Replace(uint indexReplaced, object itemReplaceWith)
        {
            _items[(int)indexReplaced] = itemReplaceWith;
            return HRESULT.S_OK;
        }

        /// <summary>
        /// Clear the collection
        /// </summary>
        /// <returns></returns>
        public HRESULT Clear()
        {
            _items.Clear();
            return HRESULT.S_OK;
        }

        #endregion
    }
}
