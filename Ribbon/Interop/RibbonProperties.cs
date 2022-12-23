//****************************************************************************
//
//  File:       RibbonProperties.cs
//
//  Contents:   Properties related to the Windows Ribbon Framework, based on 
//              UIRibbon.idl from windows 7 SDK
//
//****************************************************************************

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace RibbonLib.Interop
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <summary>
    /// Properties related to the Windows Ribbon Framework, based on 
    /// UIRibbon.idl from windows 7 SDK
    /// </summary>
    public static class RibbonProperties
    {
        static RibbonProperties()
        {
            InitPropertyKeyNames();
        }

        public static string KeyToString(PropertyKey key)
        {
            byte[] guid = key.FormatId.ToByteArray();
            int index = BitConverter.ToInt32(guid, 0);
            string name = RibbonProperties.GetPropertyKeyName(index);
            if (string.IsNullOrEmpty(name))
                return null;
            string veString;
            VarEnum ve = (VarEnum)(key.PropertyId);
            if ((ve & VarEnum.VT_VECTOR) != 0)
                veString = "VT_VECTOR" + " | " + (ve & ~VarEnum.VT_VECTOR).ToString();
            else if ((ve & VarEnum.VT_ARRAY) != 0)
                veString = "VT_ARRAY" + " | " + (ve & ~VarEnum.VT_ARRAY).ToString();
            else
                veString = ve.ToString();
            return "PROPERTYKEY." + name + " : " + veString + " : " + key.FormatId.ToString();
        }

        private static Dictionary<int, string> propertyKeyNames;

        public static string GetPropertyKeyName(int key)
        {
            if (propertyKeyNames.TryGetValue(key, out var value))
            {
                return value;
            }
            return string.Empty;
        }

        private static void InitPropertyKeyNames()
        {
            propertyKeyNames = new Dictionary<int, string>();
            propertyKeyNames.Add(1, nameof(Enabled));
            propertyKeyNames.Add(2, nameof(LabelDescription));
            propertyKeyNames.Add(3, nameof(Keytip));
            propertyKeyNames.Add(4, nameof(Label));
            propertyKeyNames.Add(5, nameof(TooltipDescription));
            propertyKeyNames.Add(6, nameof(TooltipTitle));
            propertyKeyNames.Add(7, nameof(LargeImage));
            propertyKeyNames.Add(8, nameof(LargeHighContrastImage));
            propertyKeyNames.Add(9, nameof(SmallImage));
            propertyKeyNames.Add(10, nameof(SmallHighContrastImage));

            propertyKeyNames.Add(100, nameof(CommandID));
            propertyKeyNames.Add(101, nameof(ItemsSource));
            propertyKeyNames.Add(102, nameof(Categories));
            propertyKeyNames.Add(103, nameof(CategoryID));
            propertyKeyNames.Add(104, nameof(SelectedItem));
            propertyKeyNames.Add(105, nameof(CommandType));
            propertyKeyNames.Add(106, nameof(ItemImage));

            propertyKeyNames.Add(200, nameof(BooleanValue));
            propertyKeyNames.Add(201, nameof(DecimalValue));
            propertyKeyNames.Add(202, nameof(StringValue));
            propertyKeyNames.Add(203, nameof(MaxValue));
            propertyKeyNames.Add(204, nameof(MinValue));
            propertyKeyNames.Add(205, nameof(Increment));
            propertyKeyNames.Add(206, nameof(DecimalPlaces));
            propertyKeyNames.Add(207, nameof(FormatString));
            propertyKeyNames.Add(208, nameof(RepresentativeString));

            propertyKeyNames.Add(300, nameof(FontProperties));
            propertyKeyNames.Add(301, nameof(FontProperties_Family));
            propertyKeyNames.Add(302, nameof(FontProperties_Size));
            propertyKeyNames.Add(303, nameof(FontProperties_Bold));
            propertyKeyNames.Add(304, nameof(FontProperties_Italic));
            propertyKeyNames.Add(305, nameof(FontProperties_Underline));
            propertyKeyNames.Add(306, nameof(FontProperties_Strikethrough));
            propertyKeyNames.Add(307, nameof(FontProperties_VerticalPositioning));
            propertyKeyNames.Add(308, nameof(FontProperties_ForegroundColor));
            propertyKeyNames.Add(309, nameof(FontProperties_BackgroundColor));
            propertyKeyNames.Add(310, nameof(FontProperties_ForegroundColorType));
            propertyKeyNames.Add(311, nameof(FontProperties_BackgroundColorType));
            propertyKeyNames.Add(312, nameof(FontProperties_ChangedProperties));
            propertyKeyNames.Add(313, nameof(FontProperties_DeltaSize));

            propertyKeyNames.Add(350, nameof(RecentItems));
            propertyKeyNames.Add(351, nameof(Pinned));

            propertyKeyNames.Add(400, nameof(Color));
            propertyKeyNames.Add(401, nameof(ColorType));
            propertyKeyNames.Add(402, nameof(ColorMode));
            propertyKeyNames.Add(403, nameof(ThemeColorsCategoryLabel));
            propertyKeyNames.Add(404, nameof(StandardColorsCategoryLabel));
            propertyKeyNames.Add(405, nameof(RecentColorsCategoryLabel));
            propertyKeyNames.Add(406, nameof(AutomaticColorLabel));
            propertyKeyNames.Add(407, nameof(NoColorLabel));
            propertyKeyNames.Add(408, nameof(MoreColorsLabel));
            propertyKeyNames.Add(409, nameof(ThemeColors));
            propertyKeyNames.Add(410, nameof(StandardColors));
            propertyKeyNames.Add(411, nameof(ThemeColorsTooltips));
            propertyKeyNames.Add(412, nameof(StandardColorsTooltips));

            propertyKeyNames.Add(1000, nameof(Viewable));
            propertyKeyNames.Add(1001, nameof(Minimized));
            propertyKeyNames.Add(1002, nameof(QuickAccessToolbarDock));

            propertyKeyNames.Add(1100, nameof(ContextAvailable));

            propertyKeyNames.Add(2000, nameof(GlobalBackgroundColor));
            propertyKeyNames.Add(2001, nameof(GlobalHighlightColor));
            propertyKeyNames.Add(2002, nameof(GlobalTextColor));
            //propertyKeyNames.Add(2003, nameof(ApplicationButtonColor));
            //propertyKeyNames.Add(2004, nameof(DarkModeRibbon));
        }

        // Core command properties
        public static PropertyKey Enabled = CreateRibbonPropertyKey(1, VarEnum.VT_BOOL);
        public static PropertyKey LabelDescription = CreateRibbonPropertyKey(2, VarEnum.VT_LPWSTR);
        public static PropertyKey Keytip = CreateRibbonPropertyKey(3, VarEnum.VT_LPWSTR);
        public static PropertyKey Label = CreateRibbonPropertyKey(4, VarEnum.VT_LPWSTR);
        public static PropertyKey TooltipDescription = CreateRibbonPropertyKey(5, VarEnum.VT_LPWSTR);
        public static PropertyKey TooltipTitle = CreateRibbonPropertyKey(6, VarEnum.VT_LPWSTR);
        public static PropertyKey LargeImage = CreateRibbonPropertyKey(7, VarEnum.VT_UNKNOWN); // IUIImage
        public static PropertyKey LargeHighContrastImage = CreateRibbonPropertyKey(8, VarEnum.VT_UNKNOWN); // IUIImage
        public static PropertyKey SmallImage = CreateRibbonPropertyKey(9, VarEnum.VT_UNKNOWN); // IUIImage
        public static PropertyKey SmallHighContrastImage = CreateRibbonPropertyKey(10, VarEnum.VT_UNKNOWN); // IUIImage

        // Collections properties
        public static PropertyKey CommandID = CreateRibbonPropertyKey(100, VarEnum.VT_UI4);
        public static PropertyKey ItemsSource = CreateRibbonPropertyKey(101, VarEnum.VT_UNKNOWN); // IEnumUnknown or IUICollection
        public static PropertyKey Categories = CreateRibbonPropertyKey(102, VarEnum.VT_UNKNOWN); // IEnumUnknown or IUICollection
        public static PropertyKey CategoryID = CreateRibbonPropertyKey(103, VarEnum.VT_UI4);
        public static PropertyKey SelectedItem = CreateRibbonPropertyKey(104, VarEnum.VT_UI4);
        public static PropertyKey CommandType = CreateRibbonPropertyKey(105, VarEnum.VT_UI4);
        public static PropertyKey ItemImage = CreateRibbonPropertyKey(106, VarEnum.VT_UNKNOWN); // IUIImage

        // Control properties
        public static PropertyKey BooleanValue = CreateRibbonPropertyKey(200, VarEnum.VT_BOOL);
        public static PropertyKey DecimalValue = CreateRibbonPropertyKey(201, VarEnum.VT_DECIMAL);
        public static PropertyKey StringValue = CreateRibbonPropertyKey(202, VarEnum.VT_LPWSTR);
        public static PropertyKey MaxValue = CreateRibbonPropertyKey(203, VarEnum.VT_DECIMAL);
        public static PropertyKey MinValue = CreateRibbonPropertyKey(204, VarEnum.VT_DECIMAL);
        public static PropertyKey Increment = CreateRibbonPropertyKey(205, VarEnum.VT_DECIMAL);
        public static PropertyKey DecimalPlaces = CreateRibbonPropertyKey(206, VarEnum.VT_UI4);
        public static PropertyKey FormatString = CreateRibbonPropertyKey(207, VarEnum.VT_LPWSTR);
        public static PropertyKey RepresentativeString = CreateRibbonPropertyKey(208, VarEnum.VT_LPWSTR);

        // Font control properties
        public static PropertyKey FontProperties = CreateRibbonPropertyKey(300, VarEnum.VT_UNKNOWN); // IPropertyStore
        public static PropertyKey FontProperties_Family = CreateRibbonPropertyKey(301, VarEnum.VT_LPWSTR);
        public static PropertyKey FontProperties_Size = CreateRibbonPropertyKey(302, VarEnum.VT_DECIMAL);
        public static PropertyKey FontProperties_Bold = CreateRibbonPropertyKey(303, VarEnum.VT_UI4); // UI_FONTPROPERTIES
        public static PropertyKey FontProperties_Italic = CreateRibbonPropertyKey(304, VarEnum.VT_UI4); // UI_FONTPROPERTIES
        public static PropertyKey FontProperties_Underline = CreateRibbonPropertyKey(305, VarEnum.VT_UI4); // UI_FONTPROPERTIES
        public static PropertyKey FontProperties_Strikethrough = CreateRibbonPropertyKey(306, VarEnum.VT_UI4); // UI_FONTPROPERTIES
        public static PropertyKey FontProperties_VerticalPositioning = CreateRibbonPropertyKey(307, VarEnum.VT_UI4); // UI_FONTVERTICALPOSITION
        public static PropertyKey FontProperties_ForegroundColor = CreateRibbonPropertyKey(308, VarEnum.VT_UI4); // COLORREF
        public static PropertyKey FontProperties_BackgroundColor = CreateRibbonPropertyKey(309, VarEnum.VT_UI4); // COLORREF
        public static PropertyKey FontProperties_ForegroundColorType = CreateRibbonPropertyKey(310, VarEnum.VT_UI4); // UI_SWATCHCOLORTYPE
        public static PropertyKey FontProperties_BackgroundColorType = CreateRibbonPropertyKey(311, VarEnum.VT_UI4); // UI_SWATCHCOLORTYPE
        public static PropertyKey FontProperties_ChangedProperties = CreateRibbonPropertyKey(312, VarEnum.VT_UNKNOWN); // IPropertyStore
        public static PropertyKey FontProperties_DeltaSize = CreateRibbonPropertyKey(313, VarEnum.VT_UI4); // UI_FONTDELTASIZE 

        // Application menu properties
        public static PropertyKey RecentItems = CreateRibbonPropertyKey(350, (VarEnum.VT_ARRAY | VarEnum.VT_UNKNOWN));
        public static PropertyKey Pinned = CreateRibbonPropertyKey(351, VarEnum.VT_BOOL);

        // Color picker properties
        public static PropertyKey Color = CreateRibbonPropertyKey(400, VarEnum.VT_UI4); // COLORREF
        public static PropertyKey ColorType = CreateRibbonPropertyKey(401, VarEnum.VT_UI4); // UI_SWATCHCOLORTYPE
        public static PropertyKey ColorMode = CreateRibbonPropertyKey(402, VarEnum.VT_UI4); // UI_SWATCHCOLORMODE
        public static PropertyKey ThemeColorsCategoryLabel = CreateRibbonPropertyKey(403, VarEnum.VT_LPWSTR);
        public static PropertyKey StandardColorsCategoryLabel = CreateRibbonPropertyKey(404, VarEnum.VT_LPWSTR);
        public static PropertyKey RecentColorsCategoryLabel = CreateRibbonPropertyKey(405, VarEnum.VT_LPWSTR);
        public static PropertyKey AutomaticColorLabel = CreateRibbonPropertyKey(406, VarEnum.VT_LPWSTR);
        public static PropertyKey NoColorLabel = CreateRibbonPropertyKey(407, VarEnum.VT_LPWSTR);
        public static PropertyKey MoreColorsLabel = CreateRibbonPropertyKey(408, VarEnum.VT_LPWSTR);
        public static PropertyKey ThemeColors = CreateRibbonPropertyKey(409, (VarEnum.VT_VECTOR | VarEnum.VT_UI4));
        public static PropertyKey StandardColors = CreateRibbonPropertyKey(410, (VarEnum.VT_VECTOR | VarEnum.VT_UI4));
        public static PropertyKey ThemeColorsTooltips = CreateRibbonPropertyKey(411, (VarEnum.VT_VECTOR | VarEnum.VT_LPWSTR));
        public static PropertyKey StandardColorsTooltips = CreateRibbonPropertyKey(412, (VarEnum.VT_VECTOR | VarEnum.VT_LPWSTR));

        // Ribbon properties
        public static PropertyKey Viewable = CreateRibbonPropertyKey(1000, VarEnum.VT_BOOL);
        public static PropertyKey Minimized = CreateRibbonPropertyKey(1001, VarEnum.VT_BOOL);
        public static PropertyKey QuickAccessToolbarDock = CreateRibbonPropertyKey(1002, VarEnum.VT_UI4);

        // Contextual tabset properties
        public static PropertyKey ContextAvailable = CreateRibbonPropertyKey(1100, VarEnum.VT_UI4);

        // Color properties
        // These are specified using hue, saturation and brightness.  The background, highlight and text colors of all controls
        // will be adjusted to the specified hues and saturations.  The brightness does not represent a component of a specific
        // color, but rather the overall brightness of the controls - 0x00 is darkest, 0xFF is lightest.
        public static PropertyKey GlobalBackgroundColor = CreateRibbonPropertyKey(2000, VarEnum.VT_UI4); // UI_HSBCOLOR
        public static PropertyKey GlobalHighlightColor = CreateRibbonPropertyKey(2001, VarEnum.VT_UI4); // UI_HSBCOLOR
        public static PropertyKey GlobalTextColor = CreateRibbonPropertyKey(2002, VarEnum.VT_UI4); // UI_HSBCOLOR

        public static PropertyKey CreateRibbonPropertyKey(Int32 index, VarEnum id)
        {
            return new PropertyKey(
                new Guid(index, 0x7363, 0x696e, new byte[] { 0x84, 0x41, 0x79, 0x8a, 0xcf, 0x5a, 0xeb, 0xb7 }),
                (uint)id);
        }

        /// <summary>
        /// Get the name of a given PropertyKey
        /// </summary>
        /// <param name="propertyKey">PropertyKey</param>
        /// <returns>Name of the PropertyKey</returns>
        /// <remarks>This function is used for debugging.</remarks>
        public static string GetPropertyKeyName(ref PropertyKey propertyKey)
        {
            byte[] guid = propertyKey.FormatId.ToByteArray();
            int index = BitConverter.ToInt32(guid, 0);
            return GetPropertyKeyName(index);

            //FieldInfo[] fields = typeof(RibbonProperties).GetFields();

            //foreach (FieldInfo field in fields)
            //{
            //    if (field.FieldType == typeof(PropertyKey))
            //    {
            //        PropertyKey currentPropertyKey = (PropertyKey)field.GetValue(null);
            //        if (currentPropertyKey.FormatId == propertyKey.FormatId)
            //        {
            //            return field.Name;
            //        }
            //    }
            //}

            //return string.Empty;
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
