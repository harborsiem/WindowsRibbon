//*****************************************************************************
//
//  File:       GalleryPropertiesProvider.cs
//
//  Contents:   Definition for gallery properties provider 
//
//*****************************************************************************

using RibbonLib.Interop;
using System;
using System.Threading;

namespace RibbonLib.Controls.Properties
{
    /// <summary>
    /// Definition for gallery properties provider interface
    /// </summary>
    public interface IGalleryPropertiesProvider
    {
        /// <summary>
        /// Categories property, Managed version
        /// </summary>
        UICollection<GalleryItemPropertySet> GalleryCategories { get; }

        /// <summary>
        /// Items source property
        /// </summary>
        UICollection<GalleryItemPropertySet> GalleryItemItemsSource { get; }

        /// <summary>
        /// Categories property
        /// </summary>
        IUICollection Categories { get; }

        /// <summary>
        /// Items source property
        /// </summary>
        IUICollection ItemsSource { get; }

        /// <summary>
        /// Invalidate GalleryCategories or Categories if one change a value
        /// </summary>
        void InvalidateCategories();

        /// <summary>
        /// Invalidate GalleryItemItemsSource or ItemsSource if one change a value
        /// </summary>
        void InvalidateItemsSource();

        /// <summary>
        /// Selected item property
        /// </summary>
        uint SelectedItem { get; set; }

        /// <summary>
        /// Called when the Categories property is ready to be initialized
        /// </summary>
        event EventHandler<EventArgs> CategoriesReady;

        /// <summary>
        /// Called when the ItemsSource property is ready to be initialized
        /// </summary>
        event EventHandler<EventArgs> ItemsSourceReady;
    }

    /// <summary>
    /// Implementation of IGalleryPropertiesProvider
    /// </summary>
    public class GalleryPropertiesProvider : BasePropertiesProvider, IGalleryPropertiesProvider
    {
        private object _sender;
        private bool categoriesReadyFired;
        private bool itemsSourceReadyFired;
        internal GalleryCommandProperties GalleryCommand { get; private set; }

        /// <summary>
        /// GalleryPropertiesProvider ctor
        /// </summary>
        /// <param name="ribbon">parent ribbon</param>
        /// <param name="commandId">ribbon control command id</param>
        /// <param name="sender">ribbon control that instantiate the provider</param>
        public GalleryPropertiesProvider(Ribbon ribbon, uint commandId, object sender)
            : base(ribbon, commandId)
        {
            _sender = sender;

            // add supported properties
            _supportedProperties.Add(RibbonProperties.Categories);
            _supportedProperties.Add(RibbonProperties.ItemsSource);
            _supportedProperties.Add(RibbonProperties.SelectedItem);
            GalleryCommand = new GalleryCommandProperties();
        }

        private uint? _selectedItem;

        /// <summary>
        /// Handles IUICommandHandler.UpdateProperty function for the supported properties
        /// </summary>
        /// <param name="key">The Property Key to update</param>
        /// <param name="currentValue">A pointer to the current value for key. This parameter can be null</param>
        /// <param name="newValue">When this method returns, contains a pointer to the new value for key</param>
        /// <returns>Returns S_OK if successful, or an error value otherwise</returns>
        protected override HRESULT UpdatePropertyImpl(ref PropertyKey key, PropVariantRef currentValue, ref PropVariant newValue)
        {
            if (key == RibbonProperties.Categories)
            {
                if (!categoriesReadyFired)
                {
                    GalleryCategories = new UICollection<GalleryItemPropertySet>((IUICollection)currentValue.PropVariant.Value, (BaseRibbonControl)_sender, CollectionType.Categories);
                    categoriesReadyFired = true;

                    if (CategoriesReady != null)
                    {
                        try
                        {
                            CategoriesReady(_sender, EventArgs.Empty);
                        }
                        catch (Exception ex)
                        {
                            ThreadExceptionEventArgs e = new ThreadExceptionEventArgs(ex);
                            if (_ribbon.OnRibbonEventException(_sender, e))
                                return HRESULT.E_FAIL;
                            Environment.FailFast(ex.StackTrace);
                            return HRESULT.E_ABORT;
                        }

                    }
                }
            }
            else if (key == RibbonProperties.ItemsSource)
            {
                if (!itemsSourceReadyFired)
                {
                    itemsSourceReadyFired = true;
                    BaseRibbonControl brc = (BaseRibbonControl)_sender;
                    if (brc.CommandType == CommandType.Collection)
                        GalleryItemItemsSource = new UICollection<GalleryItemPropertySet>((IUICollection)currentValue.PropVariant.Value, brc, CollectionType.ItemsSource);
                    else if (brc.CommandType == CommandType.Commandcollection)
                        GalleryCommand.GalleryCommandItemsSource = new UICollection<GalleryCommandPropertySet>((IUICollection)currentValue.PropVariant.Value, brc, CollectionType.ItemsSource);

                    if (ItemsSourceReady != null)
                    {
                        try
                        {
                            ItemsSourceReady(_sender, EventArgs.Empty);
                        }
                        catch (Exception ex)
                        {
                            ThreadExceptionEventArgs e = new ThreadExceptionEventArgs(ex);
                            if (_ribbon.OnRibbonEventException(_sender, e))
                                return HRESULT.E_FAIL;
                            Environment.FailFast(ex.StackTrace);
                            return HRESULT.E_ABORT;
                        }
                    }
                }
            }
            else if (key == RibbonProperties.SelectedItem)
            {
                if (_selectedItem.HasValue)
                {
                    newValue.SetUInt(_selectedItem.Value);
                }
            }

            return HRESULT.S_OK;
        }

        #region IGalleryPropertiesProvider Members

        /// <summary>
        /// Categories property
        /// </summary>
        public UICollection<GalleryItemPropertySet> GalleryCategories { get; private set; }

        /// <summary>
        /// Items source property for Item
        /// </summary>
        public UICollection<GalleryItemPropertySet> GalleryItemItemsSource { get; private set; }

        /// <summary>
        /// Categories property
        /// </summary>
        public IUICollection Categories
        {
            get
            {
                if (_ribbon.Initialized)
                {
                    PropVariant unknownValue;
                    HRESULT hr = _ribbon.Framework.GetUICommandProperty(_commandID, ref RibbonProperties.Categories, out unknownValue);
                    if (hr.Succeeded)
                    {
                        return (IUICollection)unknownValue.Value;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Items source property
        /// </summary>
        public IUICollection ItemsSource
        {
            get
            {
                if (_ribbon.Initialized)
                {
                    PropVariant unknownValue;
                    HRESULT hr = _ribbon.Framework.GetUICommandProperty(_commandID, ref RibbonProperties.ItemsSource, out unknownValue);
                    if (hr.Succeeded)
                    {
                        return (IUICollection)unknownValue.Value;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Invalidate GalleryCategories or Categories if one change a value
        /// </summary>
        public void InvalidateCategories()
        {
            if (_ribbon.Framework != null)
            {
                    _ribbon.Framework.InvalidateUICommand(_commandID, Invalidations.Property, PropertyKeyRef.From(RibbonProperties.Categories));
            }
        }

        /// <summary>
        /// Invalidate GalleryItemItemsSource or ItemsSource if one change a value
        /// </summary>
        public void InvalidateItemsSource()
        {
            if (_ribbon.Framework != null)
            {
                    _ribbon.Framework.InvalidateUICommand(_commandID, Invalidations.Property, PropertyKeyRef.From(RibbonProperties.ItemsSource));
            }
        }

        /// <summary>
        /// Selected item property
        /// </summary>
        public uint SelectedItem
        {
            get
            {
                if (_ribbon.Initialized)
                {
                    PropVariant uintValue;
                    HRESULT hr = _ribbon.Framework.GetUICommandProperty(_commandID, ref RibbonProperties.SelectedItem, out uintValue);
                    if (hr.Succeeded)
                    {
                        return (uint)uintValue.Value;
                    }
                }

                return _selectedItem.GetValueOrDefault(Constants.UI_Collection_InvalidIndex);
            }
            set
            {
                _selectedItem = value;

                if (_ribbon.Initialized)
                {
                    PropVariant uintValue = PropVariant.FromObject(value);
                    HRESULT hr = _ribbon.Framework.SetUICommandProperty(_commandID, ref RibbonProperties.SelectedItem, ref uintValue);
                }
            }
        }

        /// <summary>
        /// Called when the Categories property is ready to be initialized
        /// </summary>
        public event EventHandler<EventArgs> CategoriesReady;

        /// <summary>
        /// Called when the ItemsSource property is ready to be initialized
        /// </summary>
        public event EventHandler<EventArgs> ItemsSourceReady;

        #endregion
    }
}
