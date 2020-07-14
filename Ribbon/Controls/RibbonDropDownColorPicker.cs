//*****************************************************************************
//
//  File:       RibbonDropDownColorPicker.cs
//
//  Contents:   Helper class that wraps a ribbon drop down color picker control.
//
//*****************************************************************************

using System.Drawing;
using RibbonLib.Controls.Events;
using RibbonLib.Controls.Properties;
using RibbonLib.Interop;
using System;

namespace RibbonLib.Controls
{
    /// <summary>
    /// Helper class that wraps a ribbon drop down color picker control.
    /// </summary>
    public class RibbonDropDownColorPicker : BaseRibbonControl,
        IColorPickerPropertiesProvider,
        IEnabledPropertiesProvider,
        IKeytipPropertiesProvider,
        ILabelPropertiesProvider,
        IImagePropertiesProvider,
        ITooltipPropertiesProvider,
        IExecuteEventsProvider,
        IPreviewEventsProvider
    {
        private ColorPickerPropertiesProvider _colorPickerPropertiesProvider;
        private EnabledPropertiesProvider _enabledPropertiesProvider;
        private KeytipPropertiesProvider _keytipPropertiesProvider;
        private LabelPropertiesProvider _labelPropertiesProvider;
        private ImagePropertiesProvider _imagePropertiesProvider;
        private TooltipPropertiesProvider _tooltipPropertiesProvider;
        private ExecuteEventsProvider _executeEventsProvider;
        private PreviewEventsProvider _previewEventsProvider;

        /// <summary>
        /// Initializes a new instance of the Ribbon DropDownColorPicker
        /// </summary>
        /// <param name="ribbon">Parent Ribbon control</param>
        /// <param name="commandId">Command id attached to this control</param>
        public RibbonDropDownColorPicker(Ribbon ribbon, uint commandId)
            : base(ribbon, commandId)
        {
            AddPropertiesProvider(_colorPickerPropertiesProvider = new ColorPickerPropertiesProvider(ribbon, commandId));
            AddPropertiesProvider(_enabledPropertiesProvider = new EnabledPropertiesProvider(ribbon, commandId));
            AddPropertiesProvider(_keytipPropertiesProvider = new KeytipPropertiesProvider(ribbon, commandId));
            AddPropertiesProvider(_labelPropertiesProvider = new LabelPropertiesProvider(ribbon, commandId));
            AddPropertiesProvider(_imagePropertiesProvider = new ImagePropertiesProvider(ribbon, commandId));
            AddPropertiesProvider(_tooltipPropertiesProvider = new TooltipPropertiesProvider(ribbon, commandId));

            AddEventsProvider(_executeEventsProvider = new ExecuteEventsProvider(this));
            AddEventsProvider(_previewEventsProvider = new PreviewEventsProvider(this));
        }

        #region IColorPickerPropertiesProvider Members

        /// <summary>
        /// Defines the label for the "Automatic" color button.
        /// </summary>
        public string AutomaticColorLabel
        {
            get
            {
                return _colorPickerPropertiesProvider.AutomaticColorLabel;
            }
            set
            {
                _colorPickerPropertiesProvider.AutomaticColorLabel = value;
            }
        }

        /// <summary>
        /// The selected color.
        /// </summary>
        public Color Color
        {
            get
            {
                return _colorPickerPropertiesProvider.Color;
            }
            set
            {
                _colorPickerPropertiesProvider.Color = value;
            }
        }

        /// <summary>
        /// The type of selected color.
        /// Can be: NoColor, Automatic or RGB (meaning specific color).
        /// </summary>
        public SwatchColorType ColorType
        {
            get
            {
                return _colorPickerPropertiesProvider.ColorType;
            }
            set
            {
                _colorPickerPropertiesProvider.ColorType = value;
            }
        }

        /// <summary>
        /// Defines the label for the "More colors..." button.
        /// </summary>
        public string MoreColorsLabel
        {
            get
            {
                return _colorPickerPropertiesProvider.MoreColorsLabel;
            }
            set
            {
                _colorPickerPropertiesProvider.MoreColorsLabel = value;
            }
        }

        /// <summary>
        /// Defines the label for the "No color" button.
        /// </summary>
        public string NoColorLabel
        {
            get
            {
                return _colorPickerPropertiesProvider.NoColorLabel;
            }
            set
            {
                _colorPickerPropertiesProvider.NoColorLabel = value;
            }
        }

        /// <summary>
        /// Defines the label for the "Recent colors" category.
        /// </summary>
        public string RecentColorsCategoryLabel
        {
            get
            {
                return _colorPickerPropertiesProvider.RecentColorsCategoryLabel;
            }
            set
            {
                _colorPickerPropertiesProvider.RecentColorsCategoryLabel = value;
            }
        }

        /// <summary>
        /// Defines the colors for the "Standard colors".
        /// </summary>
        public Color[] StandardColors
        {
            get
            {
                return _colorPickerPropertiesProvider.StandardColors;
            }
            set
            {
                _colorPickerPropertiesProvider.StandardColors = value;
            }
        }

        /// <summary>
        /// Defines the label for the "Standard colors" category.
        /// </summary>
        public string StandardColorsCategoryLabel
        {
            get
            {
                return _colorPickerPropertiesProvider.StandardColorsCategoryLabel;
            }
            set
            {
                _colorPickerPropertiesProvider.StandardColorsCategoryLabel = value;
            }
        }

        /// <summary>
        /// Defines the tooltips for the "Standard colors".
        /// </summary>
        public string[] StandardColorsTooltips
        {
            get
            {
                return _colorPickerPropertiesProvider.StandardColorsTooltips;
            }
            set
            {
                _colorPickerPropertiesProvider.StandardColorsTooltips = value;
            }
        }

        /// <summary>
        /// Defines the colors for the "Theme colors".
        /// </summary>
        public Color[] ThemeColors
        {
            get
            {
                return _colorPickerPropertiesProvider.ThemeColors;
            }
            set
            {
                _colorPickerPropertiesProvider.ThemeColors = value;
            }
        }

        /// <summary>
        /// Defines the label for the "Theme colors" category.
        /// </summary>
        public string ThemeColorsCategoryLabel
        {
            get
            {
                return _colorPickerPropertiesProvider.ThemeColorsCategoryLabel;
            }
            set
            {
                _colorPickerPropertiesProvider.ThemeColorsCategoryLabel = value;
            }
        }

        /// <summary>
        /// Defines the tooltips for the "Theme colors".
        /// </summary>
        public string[] ThemeColorsTooltips
        {
            get
            {
                return _colorPickerPropertiesProvider.ThemeColorsTooltips;
            }
            set
            {
                _colorPickerPropertiesProvider.ThemeColorsTooltips = value;
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
