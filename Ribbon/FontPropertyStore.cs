//*****************************************************************************
//
//  File:       FontPropertyStore.cs
//
//  Contents:   Helper class that wraps an IPropertyStore interface that 
//              contains font properties
//
//*****************************************************************************

using System;
using System.Drawing;
using RibbonLib.Interop;

namespace RibbonLib
{
    /// <summary>
    /// Helper class that wraps an IPropertyStore interface that 
    /// contains font properties
    /// </summary>
    public class FontPropertyStore
    {
        private IPropertyStore _propertyStore;

        /// <summary>
        /// Initializes a new instance of the FontPropertyStore
        /// </summary>
        /// <param name="propertyStore"></param>
        public FontPropertyStore(IPropertyStore propertyStore)
        {
            if (propertyStore == null)
            {
                throw new ArgumentException("Parameter propertyStore cannot be null.", "propertyStore");
            }
            _propertyStore = propertyStore;
        }

        /// <summary>
        /// The selected font family name.
        /// </summary>
        public string Family
        {
            get
            {
                PropVariant propFamily;
                HRESULT hr = _propertyStore.GetValue(ref RibbonProperties.FontProperties_Family, out propFamily);
                return (string)propFamily.Value;
            }
        }

        /// <summary>
        /// The size of the font.
        /// </summary>
        public decimal Size
        {
            get
            {
                PropVariant propSize;
                HRESULT hr = _propertyStore.GetValue(ref RibbonProperties.FontProperties_Size, out propSize);
                return (decimal)propSize.Value;
            }
        }

        /// <summary>
        /// Flag that indicates whether bold is selected.
        /// </summary>
        public FontProperties Bold
        {
            get
            {
                PropVariant propBold;
                HRESULT hr = _propertyStore.GetValue(ref RibbonProperties.FontProperties_Bold, out propBold);
                return (FontProperties)(uint)propBold.Value;
            }
        }

        /// <summary>
        /// Flag that indicates whether italic is selected.
        /// </summary>
        public FontProperties Italic
        {
            get
            {
                PropVariant propItalic;
                HRESULT hr = _propertyStore.GetValue(ref RibbonProperties.FontProperties_Italic, out propItalic);
                return (FontProperties)(uint)propItalic.Value;
            }
        }

        /// <summary>
        /// Flag that indicates whether underline is selected.
        /// </summary>
        public FontUnderline Underline
        {
            get
            {
                PropVariant propUnderline;
                HRESULT hr = _propertyStore.GetValue(ref RibbonProperties.FontProperties_Underline, out propUnderline);
                return (FontUnderline)(uint)propUnderline.Value;
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
                PropVariant propStrikethrough;
                HRESULT hr = _propertyStore.GetValue(ref RibbonProperties.FontProperties_Strikethrough, out propStrikethrough);
                return (FontProperties)(uint)propStrikethrough.Value;
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
                PropVariant propForegroundColor;
                HRESULT hr = _propertyStore.GetValue(ref RibbonProperties.FontProperties_ForegroundColor, out propForegroundColor);
                return ColorTranslator.FromWin32((int)(uint)propForegroundColor.Value);
            }
        }

        /// <summary>
        /// The text color type. Valid values are RGB and Automatic. 
        /// If RGB is selected, the user should get the color from the ForegroundColor property. 
        /// If Automatic is selected the user should use SystemColors.WindowText.
        /// </summary>
        public SwatchColorType ForegroundColorType
        {
            get
            {
                PropVariant propForegroundColorType;
                HRESULT hr = _propertyStore.GetValue(ref RibbonProperties.FontProperties_ForegroundColorType, out propForegroundColorType);
                uint result;
                if (propForegroundColorType.VarType == System.Runtime.InteropServices.VarEnum.VT_I4)
                    result = (uint)(int)propForegroundColorType.Value; //Bug in UIRibbon
                else
                    result = (uint)propForegroundColorType.Value;
                return (SwatchColorType)(uint)propForegroundColorType.Value;
            }
        }

        /// <summary>
        /// Indicated whether the "Grow Font" or "Shrink Font" buttons were pressed.
        /// </summary>
        public FontDeltaSize? DeltaSize
        {
            get
            {
                PropVariant propDeltaSize;
                HRESULT hr = _propertyStore.GetValue(ref RibbonProperties.FontProperties_DeltaSize, out propDeltaSize);
                return (FontDeltaSize?)(uint?)propDeltaSize.Value;
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
                PropVariant propBackgroundColor;
                HRESULT hr = _propertyStore.GetValue(ref RibbonProperties.FontProperties_BackgroundColor, out propBackgroundColor);
                return ColorTranslator.FromWin32((int)(uint)propBackgroundColor.Value);
            }
        }

        /// <summary>
        /// The background color type. Valid values are RGB and NoColor. 
        /// If RGB is selected, the user should get the color from the BackgroundColor property.
        /// If NoColor is selected the user should use SystemColors.Window.
        /// </summary>
        public SwatchColorType BackgroundColorType
        {
            get
            {
                PropVariant propBackgroundColorType;
                HRESULT hr = _propertyStore.GetValue(ref RibbonProperties.FontProperties_BackgroundColorType, out propBackgroundColorType);
                uint result;
                if (propBackgroundColorType.VarType == System.Runtime.InteropServices.VarEnum.VT_I4)
                    result = (uint)(int)propBackgroundColorType.Value; //Bug in UIRibbon
                else
                    result = (uint)propBackgroundColorType.Value;
                return (SwatchColorType)result;
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
                PropVariant propVerticalPositioning;
                HRESULT hr = _propertyStore.GetValue(ref RibbonProperties.FontProperties_VerticalPositioning, out propVerticalPositioning);
                return (FontVerticalPosition)(uint)propVerticalPositioning.Value;
            }
        }

    }
}
