//*****************************************************************************
//
//  File:       UICollection.cs
//
//  Contents:   Helper class that provides an implementation of the 
//              IUICollection interface.
//
//*****************************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using RibbonLib.Controls;
using RibbonLib.Interop;

namespace RibbonLib
{
    /// <summary>
    /// The uiCollection member
    /// </summary>
    public enum CollectionType
    {
        /// <summary>
        /// The uiCollection member ItemsSource of a Gallery Control
        /// </summary>
        ItemsSource,
        /// <summary>
        /// The uiCollection member Categories of a Gallery Control
        /// </summary>
        Categories,
        /// <summary>
        /// The uiCollection member ItemsSource of a Qat Control
        /// </summary>
        QatItemsSource
    }

    /// <summary>
    /// Helper class that provides an implementation of the 
    /// IUICollection interface.
    /// </summary>
    /// <typeparam name="T">An AbstractPropertySet</typeparam>
    public class UICollection<T> : IUICollection, IEnumerable<T> where T : AbstractPropertySet, new()
    {
        private List<T> _items;
        private IUICollection _uiCollection;
        private BaseRibbonControl _ctrl;
        private Type _typeofT;
        private CollectionType _colType;
        //private CollectionChange marker = CollectionChange.None;
        private bool _detachEvent;
        private UICollectionChangedEvent _changedEvent;
        private PropertySetEnumerator _propset;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiCollection"></param>
        /// <param name="ctrl"></param>
        /// <param name="colType"></param>
        public UICollection(IUICollection uiCollection, BaseRibbonControl ctrl, CollectionType colType)
        {
            if (uiCollection == null)
                throw new ArgumentNullException(nameof(uiCollection));
            if (ctrl == null || !(ctrl.CommandType == CommandType.Collection || ctrl.CommandType == CommandType.Commandcollection))
                throw new ArgumentException("Ribbon control is not a Collection or CommandCollection", nameof(ctrl));
            _uiCollection = uiCollection;
            _typeofT = typeof(T);
            _colType = colType;
            _ctrl = ctrl;
            if (ctrl is RibbonQuickAccessToolbar)
                if (colType == CollectionType.QatItemsSource && _typeofT == typeof(QatCommandPropertySet))
                {
                    Initialize();
                    return;
                }
                else
                    throw new ArgumentException("RibbonQuickAccessToolbar with T or " + nameof(colType) + " not allowed");
            if (colType == CollectionType.Categories)
                if (_typeofT == typeof(GalleryItemPropertySet))
                {
                    Initialize();
                    return;
                }
                else
                    throw new ArgumentException("T is not a valid Type: GalleryItemPropertySet");
            if (!((ctrl.CommandType == CommandType.Commandcollection && _typeofT == typeof(GalleryCommandPropertySet))
                || (ctrl.CommandType == CommandType.Collection && _typeofT == typeof(GalleryItemPropertySet))))
                throw new ArgumentException("T is not a valid Type: GalleryItemPropertySet or GalleryCommandPropertySet");
            Initialize();
        }

        private void Initialize()
        {
            _items = new List<T>();
            _propset = new PropertySetEnumerator(this);
            foreach (T item in _propset)
            {
                _items.Add(item);
            }
            _changedEvent = new UICollectionChangedEvent();
            _changedEvent.ChangedEvent += _changedEvent_ChangedEvent;
            _changedEvent.Attach(_uiCollection);
        }

        private void _changedEvent_ChangedEvent(object sender, UICollectionChangedEventArgs e)
        {
            if (!_detachEvent)
            {
                if (!(e.NewItem is T newGalleryItem))
                    newGalleryItem = _propset.FromPropertySet((IUISimplePropertySet)e.NewItem);
                //if (!(e.OldItem is T oldGalleryItem))
                //oldGalleryItem = _propset.FromPropertySet((IUISimplePropertySet)e.OldItem);
                switch (e.Action)
                {
                    case CollectionChange.Insert:
                        _items.Insert((int)e.NewIndex, newGalleryItem);
                        break;
                    case CollectionChange.Remove:
                        _items.RemoveAt((int)e.OldIndex);
                        break;
                    case CollectionChange.Replace:
                        _items[(int)e.NewIndex] = newGalleryItem;
                        break;
                    case CollectionChange.Reset:
                        _items.Clear();
                        break;
                    default:
                        break;
                }
            }
            _detachEvent = false;
            //marker = CollectionChange.None;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_items).GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            _items.Add(item);
            //marker = CollectionChange.Insert;
            _detachEvent = true;
            HRESULT hr = _uiCollection.Add(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
            //marker = CollectionChange.Remove;
            _detachEvent = true;
            HRESULT hr = _uiCollection.RemoveAt((uint)index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, T item)
        {
            _items.Insert(index, item);
            //marker = CollectionChange.Insert;
            _detachEvent = true;
            _uiCollection.Insert((uint)index, item);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            _items.Clear();
            //marker = CollectionChange.Reset;
            _detachEvent = true;
            _uiCollection.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get { return _items[index]; }
            set
            {
                _items[index] = value;
                //marker = CollectionChange.Replace;
                _detachEvent = true;
                _uiCollection.Replace((uint)index, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count => _items.Count;

        /// <summary>
        /// Returns a List from uiCollection
        /// </summary>
        public ReadOnlyCollection<T> CollectionList { get { return _items.AsReadOnly(); } }

        #region IUICollection Members

        /// <summary>
        /// Retrieves the count of the collection
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        HRESULT IUICollection.GetCount(out uint count)
        {
            HRESULT hr = _uiCollection.GetCount(out count);
            //count = (uint)_items.Count;
            return hr;
        }

        /// <summary>
        /// Retrieves an item
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        HRESULT IUICollection.GetItem(uint index, out object item)
        {
            HRESULT hr = _uiCollection.GetItem(index, out item);
            //item = _items[(int)index];
            return hr;
        }

        /// <summary>
        /// Adds an item to the end
        /// </summary>
        /// <param name="item">Must be an object of type T</param>
        /// <returns></returns>
        HRESULT IUICollection.Add(object item)
        {
            HRESULT hr;
            hr = _uiCollection.Add(item);
            return hr;
            //return HRESULT.E_INVALIDARG;
        }

        /// <summary>
        /// Inserts an item
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item">Must be an object of type T</param>
        /// <returns></returns>
        HRESULT IUICollection.Insert(uint index, object item)
        {
            HRESULT hr;
            hr = _uiCollection.Insert(index, item);
            return hr;
            //return HRESULT.E_INVALIDARG;
        }

        /// <summary>
        /// Removes an item at the specified position
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        HRESULT IUICollection.RemoveAt(uint index)
        {
            HRESULT hr = _uiCollection.RemoveAt(index);
            return hr;
        }

        /// <summary>
        /// Replaces an item at the specified position
        /// </summary>
        /// <param name="indexReplaced"></param>
        /// <param name="itemReplaceWith">Must be an object of type T</param>
        /// <returns></returns>
        HRESULT IUICollection.Replace(uint indexReplaced, object itemReplaceWith)
        {
            HRESULT hr;
            hr = _uiCollection.Replace(indexReplaced, itemReplaceWith);
            return hr;
            //return HRESULT.E_INVALIDARG;
        }

        /// <summary>
        /// Clear the collection
        /// </summary>
        /// <returns></returns>
        HRESULT IUICollection.Clear()
        {
            HRESULT hr = _uiCollection.Clear();
            return hr;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        private sealed class PropertySetEnumerator : IEnumerable<T>, IEnumerator<T>
        {
            private UICollection<T> _caller;
            private IEnumUnknown _enumUnknown;
            private T _current;
            private Ribbon _ribbon;

            /// <summary>
            /// Ctor
            /// </summary>
            /// <param name="caller">UICollection of T</param>
            public PropertySetEnumerator(UICollection<T> caller)
            {
                _caller = caller;
                _ribbon = caller._ctrl._ribbon;
                _enumUnknown = (IEnumUnknown)caller._uiCollection;
                Reset();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator)this;
            }

            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                return (IEnumerator<T>)this;
            }

            /// <summary>
            /// 
            /// </summary>
            public T Current
            {
                get
                {
                    return _current;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return _current;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public bool MoveNext()
            {
                uint fetched;
                object[] rgelt = new object[1];
                _enumUnknown.Next(1, rgelt, out fetched);
                if (fetched == 1)
                {
                    IUISimplePropertySet uiItem = (IUISimplePropertySet)rgelt[0];
                    _current = uiItem as T;
                    if (_current == null)
                    {
                        _current = FromPropertySet(uiItem);
                    }
                    else
                    {
                        GalleryCommandPropertySet gSet = _current as GalleryCommandPropertySet;
                        if (gSet != null && gSet.RibbonCtrl == null)
                        {
                            uint cmdId = gSet.CommandID;
                            if (!_ribbon.MapRibbonControls.TryGetValue(cmdId, out IRibbonControl ctrl))
                                ctrl = null;
                            gSet.RibbonCtrl = ctrl;
                        }
                    }
                }
                else
                {
                    _enumUnknown.Reset();
                    _current = (T)null;
                }
                return fetched == 0 ? false : true;
            }

            internal T FromPropertySet(IUISimplePropertySet uiItem)
            {
                if (uiItem == null)
                    return null;

                PropVariant variant = new PropVariant();
                HRESULT hr;
                if (_caller._typeofT == typeof(QatCommandPropertySet))
                {

                    hr = uiItem.GetValue(ref RibbonProperties.CommandID, out variant);
                    uint cmdId = PropVariant.UnsafeNativeMethods.PropVariantToUInt32WithDefault(ref variant, 0);
                    if (!_ribbon.MapRibbonControls.TryGetValue(cmdId, out IRibbonControl ctrl))
                        ctrl = null;
                    QatCommandPropertySet result = new QatCommandPropertySet()
                    {
                        CommandID = cmdId,
                        RibbonCtrl = ctrl
                    };
                    return result as T;
                }
                if (_caller._typeofT == typeof(GalleryCommandPropertySet))
                {
                    hr = uiItem.GetValue(ref RibbonProperties.CommandID, out variant);
                    uint cmdId = PropVariant.UnsafeNativeMethods.PropVariantToUInt32WithDefault(ref variant, 0);
                    PropVariant.UnsafeNativeMethods.PropVariantClear(ref variant);
                    hr = uiItem.GetValue(ref RibbonProperties.CategoryID, out variant);
                    uint catId = PropVariant.UnsafeNativeMethods.PropVariantToUInt32WithDefault(ref variant, Constants.UI_Collection_InvalidIndex);
                    PropVariant.UnsafeNativeMethods.PropVariantClear(ref variant);
                    hr = uiItem.GetValue(ref RibbonProperties.CommandType, out variant);
                    CommandType cType = (CommandType)PropVariant.UnsafeNativeMethods.PropVariantToUInt32WithDefault(ref variant, 0);
                    if (!_ribbon.MapRibbonControls.TryGetValue(cmdId, out IRibbonControl ctrl))
                        ctrl = null;
                    GalleryCommandPropertySet result = new GalleryCommandPropertySet()
                    {
                        CommandID = cmdId,
                        RibbonCtrl = ctrl,
                        CategoryID = catId,
                        CommandType = cType
                    };
                    return result as T;
                }
                if (_caller._typeofT == typeof(GalleryItemPropertySet))
                {
                    hr = uiItem.GetValue(ref RibbonProperties.Label, out variant);
                    string label = PropVariant.UnsafeNativeMethods.PropVariantToStringWithDefault(ref variant, string.Empty).ToString();
                    PropVariant.UnsafeNativeMethods.PropVariantClear(ref variant);
                    hr = uiItem.GetValue(ref RibbonProperties.CategoryID, out variant);
                    uint catId = PropVariant.UnsafeNativeMethods.PropVariantToUInt32WithDefault(ref variant, Constants.UI_Collection_InvalidIndex);
                    PropVariant.UnsafeNativeMethods.PropVariantClear(ref variant);
                    hr = uiItem.GetValue(ref RibbonProperties.ItemImage, out variant);
                    IUIImage itemImage = (hr == HRESULT.S_OK && variant.VarType == VarEnum.VT_UNKNOWN) ? (IUIImage)variant.Value : null;
                    GalleryItemPropertySet result = new GalleryItemPropertySet()
                    {
                        Label = label,
                        CategoryID = catId,
                        ItemImage = itemImage
                    };
                    return result as T;
                }
                return null;
            }

            bool IEnumerator.MoveNext()
            {
                return MoveNext();
            }

            /// <summary>
            /// 
            /// </summary>
            public void Reset()
            {
                _enumUnknown.Reset();
                _current = null;
            }

            void IEnumerator.Reset()
            {
                Reset();
            }

            void IDisposable.Dispose()
            {
                Reset();
            }
        }
    }
}
