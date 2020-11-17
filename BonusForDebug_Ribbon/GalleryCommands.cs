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
    public class GalleryCommands
    {
        private List<GalleryCommandPropertySet> _controlCommands = new List<GalleryCommandPropertySet>();
        private UICollectionChangedEvent _collectionChanged;
        private IRibbonControl _ribbonControl;

        public GalleryCommands(IRibbonControl ribbonControl, IUICollection fromGallery)
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
                GalleryCommandPropertySet galleryItem = GetCommandPropertySet(item);
                _controlCommands.Add(galleryItem);
            }
        }

        //~GalleryCommands()
        //{
        //    Dispose(false);
        //}

        private void Dispose(bool disposing)
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

        public ReadOnlyCollection<GalleryCommandPropertySet> ControlItemsSource { get { return _controlCommands.AsReadOnly(); } }
        //IList<GalleryCommandPropertySet>

        private GalleryCommandPropertySet GetCommandPropertySet(object item)
        {
            GalleryCommandPropertySet galleryItem;
            galleryItem = item as GalleryCommandPropertySet;
            if (galleryItem == null)
            {
                IUISimplePropertySet newItem = item as IUISimplePropertySet;
                if (newItem != null)
                {
                    PropVariant propVariant;
                    newItem.GetValue(ref RibbonProperties.CommandID, out propVariant);
                    uint cmdID = (uint)propVariant.Value;
                    newItem.GetValue(ref RibbonProperties.CategoryID, out propVariant);
                    uint categoryID = (uint)propVariant.Value;
                    newItem.GetValue(ref RibbonProperties.CommandType, out propVariant);
                    CommandType commandType = (CommandType)(uint)propVariant.Value;
                    galleryItem = new GalleryCommandPropertySet() { CommandID = cmdID, CommandType = commandType, CategoryID = categoryID };
                }
            }
            return galleryItem;
        }

        private void CollectionChanged_ChangedEvent(object sender, UICollectionChangedEventArgs e)
        {
            GalleryCommandPropertySet newGalleryItem = GetCommandPropertySet(e.NewItem);
            //GalleryCommandPropertySet oldGalleryItem = GetCommandPropertySet(e.OldItem);
            switch (e.Action)
            {
                case CollectionChange.Insert:
                    _controlCommands.Insert((int)e.NewIndex, newGalleryItem);
                    break;
                case CollectionChange.Remove:
                    _controlCommands.RemoveAt((int)e.OldIndex);
                    break;
                case CollectionChange.Replace:
                    _controlCommands[(int)e.NewIndex] = newGalleryItem;
                    break;
                case CollectionChange.Reset:
                    _controlCommands.Clear();
                    break;
                default:
                    break;
            }
        }
    }
}
