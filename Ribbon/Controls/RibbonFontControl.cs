//*****************************************************************************
//
//  File:       RibbonFontControl.cs
//
//  Contents:   Helper class that wraps a ribbon font control.
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
    /// Helper class that wraps a ribbon font control.
    /// </summary>
    public class RibbonFontControl : BaseRibbonControl, 
        IFontControlPropertiesProvider,
        IEnabledPropertiesProvider, 
        IKeytipPropertiesProvider,
        IExecuteEventsProvider,
        IPreviewEventsProvider
    {
        private FontControlPropertiesProvider _fontControlPropertiesProvider;
        private EnabledPropertiesProvider _enabledPropertiesProvider;
        private KeytipPropertiesProvider _keytipPropertiesProvider;
        private ExecuteEventsProvider _executeEventsProvider;
        private PreviewEventsProvider _previewEventsProvider;

        /// <summary>
        /// Initializes a new instance of the Ribbon FontControl
        /// </summary>
        /// <param name="ribbon">Parent Ribbon control</param>
        /// <param name="commandId">Command id attached to this control</param>
        public RibbonFontControl(Ribbon ribbon, uint commandId)
            : base(ribbon, commandId)
        {
            AddPropertiesProvider(_fontControlPropertiesProvider = new FontControlPropertiesProvider(ribbon, commandId));
            AddPropertiesProvider(_enabledPropertiesProvider = new EnabledPropertiesProvider(ribbon, commandId));
            AddPropertiesProvider(_keytipPropertiesProvider = new KeytipPropertiesProvider(ribbon, commandId));

            AddEventsProvider(_executeEventsProvider = new ExecuteEventsProvider(this));
            AddEventsProvider(_previewEventsProvider = new PreviewEventsProvider(this));
        }

        #region IFontControlPropertiesProvider Members

        /// <summary>
        /// The selected font family name.
        /// </summary>
        public string Family
        {
            get
            {
                return _fontControlPropertiesProvider.Family;
            }
            set
            {
                _fontControlPropertiesProvider.Family = value;
            }
        }
        
        /// <summary>
        /// The size of the font.
        /// </summary>
        public decimal Size
        {
            get
            {
                return _fontControlPropertiesProvider.Size;
            }
            set
            {
                _fontControlPropertiesProvider.Size = value;
            }
        }

        /// <summary>
        /// Flag that indicates whether bold is selected.
        /// </summary>
        public FontProperties Bold
        {
            get
            {
                return _fontControlPropertiesProvider.Bold;
            }
            set
            {
                _fontControlPropertiesProvider.Bold = value;
            }
        }

        /// <summary>
        /// Flag that indicates whether italic is selected.
        /// </summary>
        public FontProperties Italic
        {
            get
            {
                return _fontControlPropertiesProvider.Italic;
            }
            set
            {
                _fontControlPropertiesProvider.Italic = value;
            }
        }
        
        /// <summary>
        /// Flag that indicates whether underline is selected.
        /// </summary>
        public FontUnderline Underline
        {
            get
            {
                return _fontControlPropertiesProvider.Underline;
            }
            set
            {
                _fontControlPropertiesProvider.Underline = value;
            }
        }

        /// <summary>
        /// Flag that indicates whether strikethrough is selected
        /// (sometimes called Strikeout).
        /// </summary>
        public FontProperties Strikethrough
        {
            get
            {
                return _fontControlPropertiesProvider.Strikethrough;
            }
            set
            {
                _fontControlPropertiesProvider.Strikethrough = value;
            }
        }

        /// <summary>
        /// Contains the text color if ForegroundColorType is set to RGB.
        /// The FontControl helper class expose this property as a .NET Color
        /// and handles internally the conversion to and from COLORREF structure.
        /// </summary>
        public Color ForegroundColor
        {
            get
            {
                return _fontControlPropertiesProvider.ForegroundColor;
            }
            set
            {
                _fontControlPropertiesProvider.ForegroundColor = value;
            }
        }

        /// <summary>
        /// Contains the background color if BackgroundColorType is set to RGB.
        /// The FontControl helper class expose this property as a .NET Color
        /// and handles internally the conversion to and from COLORREF structure.
        /// </summary>
        public Color BackgroundColor
        {
            get
            {
                return _fontControlPropertiesProvider.BackgroundColor;
            }
            set
            {
                _fontControlPropertiesProvider.BackgroundColor = value;
            }
        }

        /// <summary>
        /// Flag that indicates which one of the Subscript
        /// and Superscript buttons are selected, if any.
        /// </summary>
        public FontVerticalPosition VerticalPositioning
        {
            get
            {
                return _fontControlPropertiesProvider.VerticalPositioning;
            }
            set
            {
                _fontControlPropertiesProvider.VerticalPositioning = value;
            }
        }

        #endregion

        /// <summary>
        /// System.Drawing.FontStyle. A combination of some properties
        /// </summary>
        public FontStyle FontStyle
        {
            get
            {
                FontStyle fontStyle = FontStyle.Regular;
                if (this.Bold == FontProperties.Set)
                {
                    fontStyle |= FontStyle.Bold;
                }
                if (this.Italic == FontProperties.Set)
                {
                    fontStyle |= FontStyle.Italic;
                }
                if (this.Underline == FontUnderline.Set)
                {
                    fontStyle |= FontStyle.Underline;
                }
                if (this.Strikethrough == FontProperties.Set)
                {
                    fontStyle |= FontStyle.Strikeout;
                }
                return fontStyle;
            }
        }

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
