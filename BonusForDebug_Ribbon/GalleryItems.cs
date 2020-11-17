using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Interop;

namespace RibbonLib
{
    public class GalleryItems : GalleryCategories
    {
        public GalleryItems(IRibbonControl ribbonControl, IUICollection fromGallery) : base(ribbonControl, fromGallery)
        {
        }

        protected override GalleryItemPropertySet GetGalleryItem(IUISimplePropertySet newItem, string label, uint categoryID)
        {
            if (_ribbonControl is RibbonComboBox)
                return base.GetGalleryItem(newItem, label, categoryID);
            PropVariant propVariant = PropVariant.Empty;
            newItem.GetValue(ref RibbonProperties.ItemImage, out propVariant);
            IUIImage itemImage = (IUIImage)propVariant.Value;
            return new GalleryItemPropertySet() { Label = label, ItemImage = itemImage, CategoryID = categoryID };
        }

        //[Serializable]
        //public struct Enumerator : IEnumerator<GalleryItemPropertySet>, System.Collections.IEnumerator
        //{
        //    private GalleryItems _caller;
        //    private IUICollection _collection;
        //    private IEnumUnknown _enumUnknown;
        //    //private List<GalleryItemPropertySet> list;
        //    private int index;
        //    private GalleryItemPropertySet _current;

        //    internal Enumerator(GalleryItems caller, IUICollection collection)
        //    {
        //        _caller = caller;
        //        //this.list = null;
        //        this._collection = collection;
        //        _enumUnknown = (IEnumUnknown)collection;
        //        index = 0;
        //        _current = default(GalleryItemPropertySet);
        //    }

        //    public void Dispose()
        //    {
        //    }

        //    public bool MoveNext()
        //    {
        //        _collection.GetCount(out uint count);
        //        if (((uint)index < count))
        //        {
        //            object[] items = new object[1];
        //            uint fetchedItem;
        //            if (_enumUnknown.Next(1, items, out fetchedItem) == HRESULT.S_OK)
        //            {
        //                _current = _caller.GetItemPropertySet(items[0]);
        //                return true;
        //            }
        //        }
        //        return MoveNextRare(count);
        //    }

        //    private bool MoveNextRare(uint count)
        //    {
        //        index = (int)count + 1;
        //        _current = default(GalleryItemPropertySet);
        //        return false;
        //    }

        //    public GalleryItemPropertySet Current
        //    {
        //        get
        //        {
        //            return _current;
        //        }
        //    }

        //    Object System.Collections.IEnumerator.Current
        //    {
        //        get
        //        {
        //            _collection.GetCount(out uint count);
        //            if (index == 0 || index == (int)count + 1)
        //            {
        //                throw new InvalidOperationException("Enum can't happen");
        //            }
        //            return Current;
        //        }
        //    }

        //    void System.Collections.IEnumerator.Reset()
        //    {
        //        _enumUnknown.Reset();
        //        index = 0;
        //        _current = default(GalleryItemPropertySet);
        //    }
        //}
    }
}
