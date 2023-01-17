using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using RibbonLib.Interop;
//using System.Threading.Tasks;
//using Windows.Win32;
//using Windows.Win32.Foundation;
//using Windows.Win32.UI.Ribbon;
//using Windows.Win32.System.Com.StructuredStorage;
//using Windows.Win32.UI.Shell.PropertiesSystem;
//using Windows.Win32.System.Com;

namespace RibbonLib.Controls.Events
{
    /// <summary>
    /// The EventArgs for RibbonDropDownColorPicker
    /// </summary>
    public sealed class ColorPickerEventArgs : EventArgs
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="colorType"></param>
        /// <param name="color"></param>
        private ColorPickerEventArgs(SwatchColorType colorType, Color? color)
        {
            ColorType = colorType;
            RGBColor= color;
        }

        /// <summary>
        /// The selected Item index
        /// </summary>
        public SwatchColorType ColorType { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Color? RGBColor { get; private set;}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e">Parameters from event: ExecuteEventArgs</param>
        /// <returns></returns>
        public static ColorPickerEventArgs Create(ExecuteEventArgs e)
        {
            return Create(ref e.Key.PropertyKey, ref e.CurrentValue.PropVariant, e.CommandExecutionProperties);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="currentValue"></param>
        /// <param name="commandExecutionProperties"></param>
        /// <returns></returns>
        public static ColorPickerEventArgs Create(ref PropertyKey key, ref PropVariant currentValue, IUISimplePropertySet commandExecutionProperties)
        {
            Color? color= null;
            SwatchColorType colorType = (SwatchColorType)(uint)currentValue.Value;
            PropVariant propColor = default(PropVariant);
            if (commandExecutionProperties != null)
            {
                commandExecutionProperties.GetValue(RibbonProperties.Color, out propColor);
                uint colorref = (uint)propColor.Value;
                color = ColorTranslator.FromWin32((int)colorref);
            }
            ColorPickerEventArgs e = new ColorPickerEventArgs(colorType, color);
            return e;
        }
    }
}
