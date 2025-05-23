using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using RibbonLib.Interop;

namespace RibbonLib.Controls.Events
{
    /// <summary>
    /// The EventArgs for RibbonFontControl
    /// </summary>
    public sealed class FontControlEventArgs : EventArgs
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="currentFontStore"></param>
        /// <param name="changedValues"></param>
        private FontControlEventArgs(FontPropertyStore currentFontStore, Dictionary<FontPropertiesEnum, object> changedValues)
        {
            CurrentFontStore = currentFontStore;
            ChangedFontValues = changedValues;
        }

        /// <summary>
        /// Current Font Properties Store
        /// </summary>
        public FontPropertyStore CurrentFontStore { get; private set; }

        /// <summary>
        /// The changed values, can be null
		/// Key is an enum for the Font control properties defined at the end of this class
		/// like Family, Size, ...
        /// </summary>
        public Dictionary<FontPropertiesEnum, object> ChangedFontValues { get; private set; }

        /// <summary>
        /// Creates a FontControlEventArgs from ExecuteEventArgs of a RibbonFontControl event
        /// </summary>
        /// <param name="sender">Parameter from event: sender</param>
        /// <param name="e">Parameters from event: ExecuteEventArgs</param>
        /// <returns></returns>
        public static FontControlEventArgs Create(object sender, ExecuteEventArgs e)
        {
            if (!(sender is RibbonFontControl))
                throw new ArgumentException("Not a RibbonFontControl", nameof(sender));
            if (e == null)
                throw new ArgumentNullException(nameof(e));
            return Create(ref e.Key.PropertyKey, ref e.CurrentValue.PropVariant, e.CommandExecutionProperties);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="currentValue"></param>
        /// <param name="commandExecutionProperties"></param>
        /// <returns></returns>
        private static FontControlEventArgs Create(ref PropertyKey key, ref PropVariant currentValue, IUISimplePropertySet commandExecutionProperties)
        {
            HRESULT hr;
            IPropertyStore currentStore = null;
            FontPropertyStore fontStore = null;
            if (key == RibbonProperties.FontProperties)
            {
                currentStore = (IPropertyStore)currentValue.Value;
                fontStore = new FontPropertyStore(currentStore);
            }
            Dictionary<FontPropertiesEnum, object> keys = null;
            PropVariant varChanges;
            if (commandExecutionProperties != null)
            {
                hr = commandExecutionProperties.GetValue(RibbonProperties.FontProperties_ChangedProperties, out varChanges);
                if (varChanges.VarType != VarEnum.VT_EMPTY)
                {
                    keys = new Dictionary<FontPropertiesEnum, object>();
                    IPropertyStore store = (IPropertyStore)varChanges.Value;
                    Solution11(store, keys);
                    PropVariant.UnsafeNativeMethods.PropVariantClear(ref varChanges);
                }
            }
            FontControlEventArgs e = new FontControlEventArgs(fontStore, keys);
            return e;
        }

        //private static void Solution1(IPropertyStore store, Dictionary<string, object> keys)
        //{
        //    HRESULT hr;
        //    uint count;
        //    PropVariant value = default(PropVariant);
        //    object propValue;
        //    store.GetCount(out count);
        //    for (uint i = 0; i < count; i++)
        //    {
        //        PropertyKey key;
        //        hr = store.GetAt(i, out key);
        //        if (key == RibbonProperties.FontProperties_Family)
        //        {
        //            hr = store.GetValue(RibbonProperties.FontProperties_Family, out value);
        //            if (hr == HRESULT.S_OK)
        //            {
        //                propValue = (string)value.Value;
        //                keys.Add(RibbonProperties.GetPropertyKeyName(ref RibbonProperties.FontProperties_Family), propValue);
        //            }
        //        }
        //        else if (key == RibbonProperties.FontProperties_Size)
        //        {
        //            hr = store.GetValue(RibbonProperties.FontProperties_Size, out value);
        //            if (hr == HRESULT.S_OK)
        //            {
        //                propValue = (decimal)value.Value;
        //                keys.Add(RibbonProperties.GetPropertyKeyName(ref RibbonProperties.FontProperties_Size), propValue);
        //            }
        //        }
        //        else if (key == RibbonProperties.FontProperties_Bold)
        //        {
        //            hr = store.GetValue(RibbonProperties.FontProperties_Bold, out value);
        //            if (hr == HRESULT.S_OK)
        //            {
        //                propValue = (FontProperties)(uint)value.Value;
        //                keys.Add(RibbonProperties.GetPropertyKeyName(ref RibbonProperties.FontProperties_Bold), propValue);
        //            }
        //        }
        //        else if (key == RibbonProperties.FontProperties_Italic)
        //        {
        //            hr = store.GetValue(RibbonProperties.FontProperties_Italic, out value);
        //            if (hr == HRESULT.S_OK)
        //            {
        //                propValue = (FontProperties)(uint)value.Value;
        //                keys.Add(RibbonProperties.GetPropertyKeyName(ref RibbonProperties.FontProperties_Italic), propValue);
        //            }
        //        }
        //        else if (key == RibbonProperties.FontProperties_Underline)
        //        {
        //            hr = store.GetValue(RibbonProperties.FontProperties_Underline, out value);
        //            if (hr == HRESULT.S_OK)
        //            {
        //                propValue = (FontUnderline)(uint)value.Value;
        //                keys.Add(RibbonProperties.GetPropertyKeyName(ref RibbonProperties.FontProperties_Underline), propValue);
        //            }
        //        }
        //        else if (key == RibbonProperties.FontProperties_Strikethrough)
        //        {
        //            hr = store.GetValue(RibbonProperties.FontProperties_Strikethrough, out value);
        //            if (hr == HRESULT.S_OK)
        //            {
        //                propValue = (FontProperties)(uint)value.Value;
        //                keys.Add(RibbonProperties.GetPropertyKeyName(ref RibbonProperties.FontProperties_Strikethrough), propValue);
        //            }
        //        }
        //        else if (key == RibbonProperties.FontProperties_ForegroundColor)
        //        {
        //            hr = store.GetValue(RibbonProperties.FontProperties_ForegroundColor, out value);
        //            if (hr == HRESULT.S_OK)
        //            {
        //                propValue = ColorTranslator.FromWin32((int)(uint)value.Value);
        //                keys.Add(RibbonProperties.GetPropertyKeyName(ref RibbonProperties.FontProperties_ForegroundColor), propValue);
        //            }
        //        }
        //        else if (key == RibbonProperties.FontProperties_BackgroundColor)
        //        {
        //            hr = store.GetValue(RibbonProperties.FontProperties_BackgroundColor, out value);
        //            if (hr == HRESULT.S_OK)
        //            {
        //                propValue = ColorTranslator.FromWin32((int)(uint)value.Value);
        //                keys.Add(RibbonProperties.GetPropertyKeyName(ref RibbonProperties.FontProperties_BackgroundColor), propValue);
        //            }
        //        }
        //        else if (key == RibbonProperties.FontProperties_ForegroundColorType)
        //        {
        //            hr = store.GetValue(RibbonProperties.FontProperties_ForegroundColorType, out value);
        //            if (hr == HRESULT.S_OK)
        //            {
        //                propValue = (SwatchColorType)(uint)value.Value;
        //                keys.Add(RibbonProperties.GetPropertyKeyName(ref RibbonProperties.FontProperties_ForegroundColorType), propValue);
        //            }
        //        }
        //        else if (key == RibbonProperties.FontProperties_BackgroundColorType)
        //        {
        //            hr = store.GetValue(RibbonProperties.FontProperties_BackgroundColorType, out value);
        //            if (hr == HRESULT.S_OK)
        //            {
        //                propValue = (SwatchColorType)(uint)value.Value;
        //                keys.Add(RibbonProperties.GetPropertyKeyName(ref RibbonProperties.FontProperties_BackgroundColorType), propValue);
        //            }
        //        }
        //        else if (key == RibbonProperties.FontProperties_VerticalPositioning)
        //        {
        //            hr = store.GetValue(RibbonProperties.FontProperties_VerticalPositioning, out value);
        //            if (hr == HRESULT.S_OK)
        //            {
        //                propValue = (FontVerticalPosition)(uint)value.Value;
        //                keys.Add(RibbonProperties.GetPropertyKeyName(ref RibbonProperties.FontProperties_VerticalPositioning), propValue);
        //            }
        //        }
        //        else if (key == RibbonProperties.FontProperties_DeltaSize)
        //        {
        //            hr = store.GetValue(RibbonProperties.FontProperties_DeltaSize, out value);
        //            if (hr == HRESULT.S_OK)
        //            {
        //                propValue = (FontDeltaSize)(uint)value.Value;
        //                keys.Add(RibbonProperties.GetPropertyKeyName(ref RibbonProperties.FontProperties_DeltaSize), propValue);
        //            }
        //        }
        //    }
        //}

        private static void Solution11(IPropertyStore store, Dictionary<FontPropertiesEnum, object> keys)
        {
            HRESULT hr;
            uint count;
            PropVariant value = default(PropVariant);
            object propValue;
            store.GetCount(out count);
            for (uint i = 0; i < count; i++)
            {
                PropertyKey key;
                hr = store.GetAt(i, out key);
                if (key == RibbonProperties.FontProperties_Family)
                {
                    hr = store.GetValue(RibbonProperties.FontProperties_Family, out value);
                    if (hr == HRESULT.S_OK)
                    {
                        propValue = (string)value.Value;
                        keys.Add(FontPropertiesEnum.Family, propValue);
                    }
                }
                else if (key == RibbonProperties.FontProperties_Size)
                {
                    hr = store.GetValue(RibbonProperties.FontProperties_Size, out value);
                    if (hr == HRESULT.S_OK)
                    {
                        propValue = (decimal)value.Value;
                        keys.Add(FontPropertiesEnum.Size, propValue);
                    }
                }
                else if (key == RibbonProperties.FontProperties_Bold)
                {
                    hr = store.GetValue(RibbonProperties.FontProperties_Bold, out value);
                    if (hr == HRESULT.S_OK)
                    {
                        propValue = (FontPropertiesEnum)(uint)value.Value;
                        keys.Add(FontPropertiesEnum.Bold, propValue);
                    }
                }
                else if (key == RibbonProperties.FontProperties_Italic)
                {
                    hr = store.GetValue(RibbonProperties.FontProperties_Italic, out value);
                    if (hr == HRESULT.S_OK)
                    {
                        propValue = (FontPropertiesEnum)(uint)value.Value;
                        keys.Add(FontPropertiesEnum.Italic, propValue);
                    }
                }
                else if (key == RibbonProperties.FontProperties_Underline)
                {
                    hr = store.GetValue(RibbonProperties.FontProperties_Underline, out value);
                    if (hr == HRESULT.S_OK)
                    {
                        propValue = (FontUnderline)(uint)value.Value;
                        keys.Add(FontPropertiesEnum.Underline, propValue);
                    }
                }
                else if (key == RibbonProperties.FontProperties_Strikethrough)
                {
                    hr = store.GetValue(RibbonProperties.FontProperties_Strikethrough, out value);
                    if (hr == HRESULT.S_OK)
                    {
                        propValue = (FontPropertiesEnum)(uint)value.Value;
                        keys.Add(FontPropertiesEnum.Strikethrough, propValue);
                    }
                }
                else if (key == RibbonProperties.FontProperties_ForegroundColor)
                {
                    hr = store.GetValue(RibbonProperties.FontProperties_ForegroundColor, out value);
                    if (hr == HRESULT.S_OK)
                    {
                        propValue = ColorTranslator.FromWin32((int)(uint)value.Value);
                        keys.Add(FontPropertiesEnum.ForegroundColor, propValue);
                    }
                }
                else if (key == RibbonProperties.FontProperties_BackgroundColor)
                {
                    hr = store.GetValue(RibbonProperties.FontProperties_BackgroundColor, out value);
                    if (hr == HRESULT.S_OK)
                    {
                        propValue = ColorTranslator.FromWin32((int)(uint)value.Value);
                        keys.Add(FontPropertiesEnum.BackgroundColor, propValue);
                    }
                }
                else if (key == RibbonProperties.FontProperties_ForegroundColorType)
                {
                    hr = store.GetValue(RibbonProperties.FontProperties_ForegroundColorType, out value);
                    if (hr == HRESULT.S_OK)
                    {
                        uint result;
                        if (value.VarType == VarEnum.VT_I4)
                            result = (uint)(int)value.Value; //Bug in UIRibbon
                        else
                            result = (uint)value.Value;
                        propValue = (SwatchColorType)result;
                        keys.Add(FontPropertiesEnum.ForegroundColorType, propValue);
                    }
                }
                else if (key == RibbonProperties.FontProperties_BackgroundColorType)
                {
                    hr = store.GetValue(RibbonProperties.FontProperties_BackgroundColorType, out value);
                    if (hr == HRESULT.S_OK)
                    {
                        uint result;
                        if (value.VarType == VarEnum.VT_I4)
                            result = (uint)(int)value.Value; //Bug in UIRibbon
                        else
                            result = (uint)value.Value;
                        propValue = (SwatchColorType)(uint)result;
                        keys.Add(FontPropertiesEnum.BackgroundColorType, propValue);
                    }
                }
                else if (key == RibbonProperties.FontProperties_VerticalPositioning)
                {
                    hr = store.GetValue(RibbonProperties.FontProperties_VerticalPositioning, out value);
                    if (hr == HRESULT.S_OK)
                    {
                        propValue = (FontVerticalPosition)(uint)value.Value;
                        keys.Add(FontPropertiesEnum.VerticalPositioning, propValue);
                    }
                }
                else if (key == RibbonProperties.FontProperties_DeltaSize)
                {
                    hr = store.GetValue(RibbonProperties.FontProperties_DeltaSize, out value);
                    if (hr == HRESULT.S_OK)
                    {
                        propValue = (FontDeltaSize)(uint)value.Value;
                        keys.Add(FontPropertiesEnum.DeltaSize, propValue);
                    }
                }
            }
        }

        //    private static void Solution2(IPropertyStore store, Dictionary<string, object> keys)
        //    {
        //        HRESULT hr;
        //        object propValue;
        //        PropVariant value = default(PropVariant);
        //        hr = store.GetValue(RibbonProperties.FontProperties_BackgroundColor, out value);
        //        if (hr == HRESULT.S_OK)
        //        {
        //            propValue = ColorTranslator.FromWin32((int)(uint)value.Value);
        //            keys.Add(RibbonProperties.GetPropertyKeyName(ref RibbonProperties.FontProperties_BackgroundColor), propValue);
        //        }
        //        hr = store.GetValue(RibbonProperties.FontProperties_BackgroundColorType, out value);
        //        if (hr == HRESULT.S_OK)
        //        {
        //            propValue = (SwatchColorType)(uint)value.Value;
        //            keys.Add(RibbonProperties.GetPropertyKeyName(ref RibbonProperties.FontProperties_BackgroundColorType), propValue);
        //        }
        //        hr = store.GetValue(RibbonProperties.FontProperties_Bold, out value);
        //        if (hr == HRESULT.S_OK)
        //        {
        //            propValue = (FontProperties)(uint)value.Value;
        //            keys.Add(RibbonProperties.GetPropertyKeyName(ref RibbonProperties.FontProperties_Bold), propValue);
        //        }
        //        hr = store.GetValue(RibbonProperties.FontProperties_DeltaSize, out value);
        //        if (hr == HRESULT.S_OK)
        //        {
        //            propValue = (FontDeltaSize)(uint)value.Value;
        //            keys.Add(RibbonProperties.GetPropertyKeyName(ref RibbonProperties.FontProperties_DeltaSize), propValue);
        //        }
        //        hr = store.GetValue(RibbonProperties.FontProperties_Family, out value);
        //        if (hr == HRESULT.S_OK)
        //        {
        //            propValue = (string)value.Value;
        //            keys.Add(RibbonProperties.GetPropertyKeyName(ref RibbonProperties.FontProperties_Family), propValue);
        //        }
        //        hr = store.GetValue(RibbonProperties.FontProperties_ForegroundColor, out value);
        //        if (hr == HRESULT.S_OK)
        //        {
        //            propValue = ColorTranslator.FromWin32((int)(uint)value.Value);
        //            keys.Add(RibbonProperties.GetPropertyKeyName(ref RibbonProperties.FontProperties_ForegroundColor), propValue);
        //        }
        //        hr = store.GetValue(RibbonProperties.FontProperties_ForegroundColorType, out value);
        //        if (hr == HRESULT.S_OK)
        //        {
        //            propValue = (SwatchColorType)(uint)value.Value;
        //            keys.Add(RibbonProperties.GetPropertyKeyName(ref RibbonProperties.FontProperties_ForegroundColorType), propValue);
        //        }
        //        hr = store.GetValue(RibbonProperties.FontProperties_Italic, out value);
        //        if (hr == HRESULT.S_OK)
        //        {
        //            propValue = (FontProperties)(uint)value.Value;
        //            keys.Add(RibbonProperties.GetPropertyKeyName(ref RibbonProperties.FontProperties_Italic), propValue);
        //        }
        //        hr = store.GetValue(RibbonProperties.FontProperties_Size, out value);
        //        if (hr == HRESULT.S_OK)
        //        {
        //            propValue = (decimal)value.Value;
        //            keys.Add(RibbonProperties.GetPropertyKeyName(ref RibbonProperties.FontProperties_Size), propValue);
        //        }
        //        hr = store.GetValue(RibbonProperties.FontProperties_Strikethrough, out value);
        //        if (hr == HRESULT.S_OK)
        //        {
        //            propValue = (FontProperties)(uint)value.Value;
        //            keys.Add(RibbonProperties.GetPropertyKeyName(ref RibbonProperties.FontProperties_Strikethrough), propValue);
        //        }
        //        hr = store.GetValue(RibbonProperties.FontProperties_Underline, out value);
        //        if (hr == HRESULT.S_OK)
        //        {
        //            propValue = (FontUnderline)(uint)value.Value;
        //            keys.Add(RibbonProperties.GetPropertyKeyName(ref RibbonProperties.FontProperties_Underline), propValue);
        //        }
        //        hr = store.GetValue(RibbonProperties.FontProperties_VerticalPositioning, out value);
        //        if (hr == HRESULT.S_OK)
        //        {
        //            propValue = (FontVerticalPosition)(uint)value.Value;
        //            keys.Add(RibbonProperties.GetPropertyKeyName(ref RibbonProperties.FontProperties_VerticalPositioning), propValue);
        //        }
        //    }
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public enum FontPropertiesEnum
    {
        None = 0,
        Family = 301,
        Size,
        Bold,
        Italic,
        Underline,
        Strikethrough,
        VerticalPositioning,
        ForegroundColor,
        BackgroundColor,
        ForegroundColorType,
        BackgroundColorType,
        ChangedProperties,
        DeltaSize,
    }
#pragma warning restore CS1591
}
