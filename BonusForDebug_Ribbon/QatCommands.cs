using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Interop;

namespace RibbonLib
{
    public sealed class QatCommands : IDisposable
    {
        private List<GalleryCommandPropertySet> _controlCommands = new List<GalleryCommandPropertySet>();
        private UICollectionChangedEvent _collectionChanged;

        public QatCommands(RibbonQuickAccessToolbar qat)
        {
            if (qat == null)
            {
                throw new ArgumentNullException(nameof(qat));
            }
            IUICollection qatitemsSource = qat.ItemsSource;
            if (qatitemsSource == null)
            {
                throw new ArgumentException("Qat not initialized");
            }
            _collectionChanged = new UICollectionChangedEvent();
            _collectionChanged.ChangedEvent += CollectionChanged_ChangedEvent;
            _collectionChanged.Attach(qatitemsSource);
            uint count;
            object item;
            qatitemsSource.GetCount(out count);
            for (uint i = 0; i < count; i++)
            {
                qatitemsSource.GetItem(i, out item);
                GalleryCommandPropertySet galleryItem = GetCommandPropertySet(item);
                _controlCommands.Add(galleryItem);
            }
        }

        //~QatCommands()
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
                    galleryItem = new GalleryCommandPropertySet() { CommandID = cmdID };
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
