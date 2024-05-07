using System;
using System.Drawing;
using System.Windows.Forms;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Controls.Events;

namespace RibbonLib.Controls
{
    partial class RibbonItems
    {
        public void Init()
        {
            ButtonListColors.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonListColors_ExecuteEvent);
        }

        void _buttonListColors_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            Color[] colors = DropDownColorPickerThemeColors.ThemeColors;
            string[] colorsTooltips = DropDownColorPickerThemeColors.ThemeColorsTooltips;

            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

            for (int i = 0; i < colors.Length; ++i)
            {
                stringBuilder.AppendFormat("{0} = {1}\n", colorsTooltips[i], colors[i].ToString());
            }

            MessageBox.Show(stringBuilder.ToString());
        }

        private void InitDropDownColorPickers()
        {
            // common properties
            DropDownColorPickerThemeColors.Label = "Theme Colors";
            DropDownColorPickerThemeColors.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_themeColors_ExecuteEvent);

            // set labels
            DropDownColorPickerThemeColors.AutomaticColorLabel = "My Automatic";
            DropDownColorPickerThemeColors.MoreColorsLabel = "My More Colors";
            DropDownColorPickerThemeColors.NoColorLabel = "My No Color";
            DropDownColorPickerThemeColors.RecentColorsCategoryLabel = "My Recent Colors";
            DropDownColorPickerThemeColors.StandardColorsCategoryLabel = "My Standard Colors";
            DropDownColorPickerThemeColors.ThemeColorsCategoryLabel = "My Theme Colors";

            // set colors
            DropDownColorPickerThemeColors.ThemeColorsTooltips = new string[] { "yellow", "green", "red", "blue" };
            DropDownColorPickerThemeColors.ThemeColors = new Color[] { Color.Yellow, Color.Green, Color.Red, Color.Blue };
        }

        void _themeColors_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            MessageBox.Show("Selected color is " + DropDownColorPickerThemeColors.Color.ToString());
        }

        public void Load()
        {
            InitDropDownColorPickers();
        }

    }
}
