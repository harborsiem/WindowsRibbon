//*****************************************************************************
//
//  File:       RibbonSplitButtonGallery.cs
//
//  Contents:   Helper class that wraps a ribbon split button gallery control.
//
//*****************************************************************************

using RibbonLib.Controls.Events;
using RibbonLib.Controls.Properties;
using RibbonLib.Interop;
using System;

namespace RibbonLib.Controls
{
    /// <summary>
    /// Helper class that wraps a ribbon split button gallery control.
    /// </summary>
    public class RibbonSplitButtonGallery : BaseRibbonControl,
        IBooleanValuePropertyProvider,
        IGalleryPropertiesProvider,
        IGallery2PropertiesProvider,
        IEnabledPropertiesProvider, 
        IKeytipPropertiesProvider,
        ILabelPropertiesProvider,
        IImagePropertiesProvider,
        ITooltipPropertiesProvider,
        IExecuteEventsProvider,
        IPreviewEventsProvider
    {
        private BooleanValuePropertyProvider _booleanValuePropertyProvider;
        private GalleryPropertiesProvider _galleryPropertiesProvider;
        private EnabledPropertiesProvider _enabledPropertiesProvider;
        private KeytipPropertiesProvider _keytipPropertiesProvider;
        private LabelPropertiesProvider _labelPropertiesProvider;
        private ImagePropertiesProvider _imagePropertiesProvider;
        private TooltipPropertiesProvider _tooltipPropertiesProvider;
        private ExecuteEventsProvider _executeEventsProvider;
        private PreviewEventsProvider _previewEventsProvider;

        /// <summary>
        /// Initializes a new instance of the Ribbon SplitButtonGallery
        /// </summary>
        /// <param name="ribbon">Parent Ribbon control</param>
        /// <param name="commandId">Command id attached to this control</param>
        public RibbonSplitButtonGallery(Ribbon ribbon, uint commandId)
            : base(ribbon, commandId)
        {
            AddPropertiesProvider(_booleanValuePropertyProvider = new BooleanValuePropertyProvider(ribbon, commandId));
            AddPropertiesProvider(_galleryPropertiesProvider = new GalleryPropertiesProvider(ribbon, commandId, this));
            AddPropertiesProvider(_enabledPropertiesProvider = new EnabledPropertiesProvider(ribbon, commandId));
            AddPropertiesProvider(_keytipPropertiesProvider = new KeytipPropertiesProvider(ribbon, commandId));
            AddPropertiesProvider(_labelPropertiesProvider = new LabelPropertiesProvider(ribbon, commandId));
            AddPropertiesProvider(_imagePropertiesProvider = new ImagePropertiesProvider(ribbon, commandId));
            AddPropertiesProvider(_tooltipPropertiesProvider = new TooltipPropertiesProvider(ribbon, commandId));

            AddEventsProvider(_executeEventsProvider = new ExecuteEventsProvider(this));
            AddEventsProvider(_previewEventsProvider = new PreviewEventsProvider(this));
        }

        #region IBooleanValuePropertyProvider Members

        /// <summary>
        /// Get or set the Checked state.
        /// </summary>
        public bool BooleanValue
        {
            get
            {
                return _booleanValuePropertyProvider.BooleanValue;
            }
            set
            {
                _booleanValuePropertyProvider.BooleanValue = value;
            }
        }

        #endregion

        #region IGalleryPropertiesProvider Members

        /// <summary>
        /// The list of categories. 
        /// Also exposed as an UICollection of GalleryItemPropertySet elements
        /// </summary>
        public UICollection<GalleryItemPropertySet> GCategories => _galleryPropertiesProvider.GCategories;

        /// <summary>
        /// The list of SplitButtonGallery items.
        /// It is exposed as an UICollection where every element
        /// in the collection is of type: GalleryItemPropertySet
        /// </summary>
        public UICollection<GalleryItemPropertySet> GItemItemsSource => _galleryPropertiesProvider.GItemItemsSource;

        /// <summary>
        /// The list of SplitButtonGallery items.
        /// It is exposed as an UICollection where every element
        /// in the collection is of type: GalleryCommandPropertySet
        /// </summary>
        public UICollection<GalleryCommandPropertySet> GCommandItemsSource => _galleryPropertiesProvider.GalleryCommand.GCommandItemsSource;

        /// <summary>
        /// The list of categories. 
        /// Also exposed as an IUICollection of IUISimplePropertySet elements
        /// </summary>
        public IUICollection Categories
        {
            get
            {
                return _galleryPropertiesProvider.Categories;
            }
        }

        /// <summary>
        /// The list of SplitButtonGallery items.
        /// It is exposed as an IUICollection where every element
        /// in the collection is of type: IUISimplePropertySet
        /// </summary>
        public IUICollection ItemsSource
        {
            get
            {
                return _galleryPropertiesProvider.ItemsSource;
            }
        }

        /// <summary>
        /// The index of the selected item in the SplitButtonGallery.
        /// If nothing is selected returns UI_Collection_InvalidIndex,
        /// which is a fancy way to say -1
        /// </summary>
        public uint SelectedItem
        {
            get
            {
                return _galleryPropertiesProvider.SelectedItem;
            }
            set
            {
                _galleryPropertiesProvider.SelectedItem = value;
            }
        }

        /// <summary>
        /// Event provider which only fired once.
        /// In this event you can initialize the Categories
        /// Now one can work with the Categories.
        /// </summary>
        public event EventHandler<EventArgs> CategoriesReady
        {
            add
            {
                _galleryPropertiesProvider.CategoriesReady += value;
            }
            remove
            {
                _galleryPropertiesProvider.CategoriesReady -= value;
            }
        }

        /// <summary>
        /// Event provider which only fired once.
        /// In this event you can initialize the ItemsSource
        /// Now one can work with the ItemsSource.
        /// </summary>
        public event EventHandler<EventArgs> ItemsSourceReady
        {
            add
            {
                _galleryPropertiesProvider.ItemsSourceReady += value;
            }
            remove
            {
                _galleryPropertiesProvider.ItemsSourceReady -= value;
            }
        }

        #endregion

        #region IEnabledPropertiesProvider Members

        /// <summary>
        /// Get or set the Enabled state.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return _enabledPropertiesProvider.Enabled;
            }
            set
            {
                _enabledPropertiesProvider.Enabled = value;
            }
        }

        #endregion

        #region IKeytipPropertiesProvider Members

        /// <summary>
        /// The keytip or key sequence that is used to access the command using the Alt key.
        /// This keytip appears when the user presses the Alt key to navigate the ribbon.
        /// The Ribbon Framework will automatically apply keytips to every command.
        /// However, if you want more control over the keytips used, you can specify them yourself.
        /// A keytip is not limited to a single character.
        /// </summary>
        public string Keytip
        {
            get
            {
                return _keytipPropertiesProvider.Keytip;
            }
            set
            {
                _keytipPropertiesProvider.Keytip = value;
            }
        }

        #endregion

        #region ILabelPropertiesProvider Members

        /// <summary>
        /// This is the label of the command as it will appear on the ribbon or context popups.
        /// </summary>
        public string Label
        {
            get
            {
                return _labelPropertiesProvider.Label;
            }
            set
            {
                _labelPropertiesProvider.Label = value;
            }
        }

        #endregion

        #region IImagePropertiesProvider Members

        /// <summary>
        /// Large images
        /// For setting the Image, use method Ribbon.ConvertToUIImage(Bitmap)
        /// </summary>
        public IUIImage LargeImage
        {
            get
            {
                return _imagePropertiesProvider.LargeImage;
            }
            set
            {
                _imagePropertiesProvider.LargeImage = value;
            }
        }

        /// <summary>
        /// Small images
        /// For setting the Image, use method Ribbon.ConvertToUIImage(Bitmap)
        /// </summary>
        public IUIImage SmallImage
        {
            get
            {
                return _imagePropertiesProvider.SmallImage;
            }
            set
            {
                _imagePropertiesProvider.SmallImage = value;
            }
        }

        /// <summary>
        /// Large images for use with high-contrast system settings
        /// For setting the Image, use method Ribbon.ConvertToUIImage(Bitmap)
        /// </summary>
        public IUIImage LargeHighContrastImage
        {
            get
            {
                return _imagePropertiesProvider.LargeHighContrastImage;
            }
            set
            {
                _imagePropertiesProvider.LargeHighContrastImage = value;
            }
        }

        /// <summary>
        /// Small images for use with high-contrast system settings
        /// For setting the Image, use method Ribbon.ConvertToUIImage(Bitmap)
        /// </summary>
        public IUIImage SmallHighContrastImage
        {
            get
            {
                return _imagePropertiesProvider.SmallHighContrastImage;
            }
            set
            {
                _imagePropertiesProvider.SmallHighContrastImage = value;
            }
        }

        #endregion

        #region ITooltipPropertiesProvider Members

        /// <summary>
        /// The title of the tooltip (hint) that appear when the user hovers the mouse over the command.
        /// This title is displayed in bold at the top of the tooltip.
        /// </summary>
        public string TooltipTitle
        {
            get
            {
                return _tooltipPropertiesProvider.TooltipTitle;
            }
            set
            {
                _tooltipPropertiesProvider.TooltipTitle = value;
            }
        }

        /// <summary>
        /// The description of the tooltip as it appears below the title.
        /// </summary>
        public string TooltipDescription
        {
            get
            {
                return _tooltipPropertiesProvider.TooltipDescription;
            }
            set
            {
                _tooltipPropertiesProvider.TooltipDescription = value;
            }
        }

        #endregion

        #region IExecuteEventsProvider Members

        /// <summary>
        /// Event provider similar to a "Selected Changed" event.
        /// </summary>
        public event EventHandler<ExecuteEventArgs> ExecuteEvent
        {
            add
            {
                _executeEventsProvider.ExecuteEvent += value;
            }
            remove
            {
                _executeEventsProvider.ExecuteEvent -= value;
            }
        }

        #endregion

        #region IPreviewEventsProvider Members

        /// <summary>
        /// Event provider for a preview.
        /// This is when the mouse enters the control.
        /// </summary>
        public event EventHandler<ExecuteEventArgs> PreviewEvent
        {
            add
            {
                _previewEventsProvider.PreviewEvent += value;
            }
            remove
            {
                _previewEventsProvider.PreviewEvent -= value;
            }
        }

        /// <summary>
        /// Event provider when the preview is cancelled.
        /// This is when the mouse leaves the control.
        /// </summary>
        public event EventHandler<ExecuteEventArgs> CancelPreviewEvent
        {
            add
            {
                _previewEventsProvider.CancelPreviewEvent += value;
            }
            remove
            {
                _previewEventsProvider.CancelPreviewEvent -= value;
            }
        }

        #endregion
    }
}
