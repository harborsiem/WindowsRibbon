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
    public class GalleryCategories
    {
        private List<GalleryItemPropertySet> _controlItems = new List<GalleryItemPropertySet>();
        private UICollectionChangedEvent _collectionChanged;
        protected IRibbonControl _ribbonControl;

        public GalleryCategories(IRibbonControl ribbonControl, IUICollection fromGallery)
        {
            if (ribbonControl == null)
            {
                throw new ArgumentNullException(nameof(ribbonControl));
            }
            _ribbonControl = ribbonControl;
            if (fromGallery == null)
            {
                throw new ArgumentException(ribbonControl.ToString() + " not initialized");
            }
            _collectionChanged = new UICollectionChangedEvent();
            _collectionChanged.ChangedEvent += CollectionChanged_ChangedEvent;
            _collectionChanged.Attach(fromGallery);
            uint count;
            object item;
            fromGallery.GetCount(out count);
            for (uint i = 0; i < count; i++)
            {
                fromGallery.GetItem(i, out item);
                GalleryItemPropertySet galleryItem = GetItemPropertySet(item);
                _controlItems.Add(galleryItem);
            }
        }

        //~GalleryItems()
        //{
        //    Dispose(false);
        //}

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_collectionChanged != null)
                {
                    _collectionChanged.ChangedEvent -= CollectionChanged_ChangedEvent;
                    _collectionChanged.Detach();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            //GC.SuppressFinalize(this);
        }

        public ReadOnlyCollection<GalleryItemPropertySet> ControlCollection { get { return _controlItems.AsReadOnly(); } }
        //IList<GalleryItemPropertySet>

        protected GalleryItemPropertySet GetItemPropertySet(object item)
        {
            GalleryItemPropertySet galleryItem;
            galleryItem = item as GalleryItemPropertySet;
            if (galleryItem == null)
            {
                IUISimplePropertySet newItem = item as IUISimplePropertySet;
                if (newItem != null)
                {
                    galleryItem = GetGalleryItem(newItem);
                }
            }
            return galleryItem;
        }

        private GalleryItemPropertySet GetGalleryItem(IUISimplePropertySet newItem)
        {
            PropVariant propVariant;
            newItem.GetValue(ref RibbonProperties.Label, out propVariant);
            string label = (string)propVariant.Value;
            newItem.GetValue(ref RibbonProperties.CategoryID, out propVariant);
            uint categoryID = (uint)propVariant.Value;
            return GetGalleryItem(newItem, label, categoryID);
        }

        protected virtual GalleryItemPropertySet GetGalleryItem(IUISimplePropertySet newItem, string label, uint categoryID)
        {
            return new GalleryItemPropertySet() { Label = label, CategoryID = categoryID };
        }

        private void CollectionChanged_ChangedEvent(object sender, UICollectionChangedEventArgs e)
        {
            GalleryItemPropertySet newGalleryItem = GetItemPropertySet(e.NewItem);
            //GalleryItemPropertySet oldGalleryItem = GetItemPropertySet(e.OldItem);
            switch (e.Action)
            {
                case CollectionChange.Insert:
                    _controlItems.Insert((int)e.NewIndex, newGalleryItem);
                    break;
                case CollectionChange.Remove:
                    _controlItems.RemoveAt((int)e.OldIndex);
                    break;
                case CollectionChange.Replace:
                    _controlItems[(int)e.NewIndex] = newGalleryItem;
                    break;
                case CollectionChange.Reset:
                    _controlItems.Clear();
                    break;
                default:
                    break;
            }
        }
    }
}
