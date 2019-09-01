using System;
using System.Drawing;
using System.Windows.Forms;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Controls.Events;
using RibbonLib.Interop;

namespace _11_DropDownColorPicker
{
    public enum RibbonMarkupCommands : uint 
    {
         cmdTab = 999,
         cmdButtonsGroup = 1000,
         cmdDropDownColorPickerGroup = 1001,
         cmdDropDownColorPickerThemeColors = 1002,
         cmdDropDownColorPickerStandardColors = 1003,
         cmdDropDownColorPickerHighlightColors = 1004,
         cmdButtonListColors = 1006,
    }

    public partial class Form1 : Form
    {
        private RibbonGroup _groupButtons;
        private RibbonGroup _groupColors;
        private RibbonButton _buttonListColors;
        private RibbonDropDownColorPicker _themeColors;
        private RibbonDropDownColorPicker _standardColors;
        private RibbonDropDownColorPicker _highlightColors;

        public Form1()
        {
            InitializeComponent();

            _groupButtons = new RibbonGroup(_ribbon, (uint)RibbonMarkupCommands.cmdButtonsGroup);
            _groupColors = new RibbonGroup(_ribbon, (uint)RibbonMarkupCommands.cmdDropDownColorPickerGroup);
            _buttonListColors = new RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonListColors);
            _themeColors = new RibbonDropDownColorPicker(_ribbon, (uint)RibbonMarkupCommands.cmdDropDownColorPickerThemeColors);
            _standardColors = new RibbonDropDownColorPicker(_ribbon, (uint)RibbonMarkupCommands.cmdDropDownColorPickerStandardColors);
            _highlightColors = new RibbonDropDownColorPicker(_ribbon, (uint)RibbonMarkupCommands.cmdDropDownColorPickerHighlightColors);

            _buttonListColors.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonListColors_ExecuteEvent);
        }

        void _buttonListColors_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            Color[] colors = _themeColors.ThemeColors;
            string[] colorsTooltips = _themeColors.ThemeColorsTooltips;

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
            _themeColors.Label = "Theme Colors";
            _themeColors.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_themeColors_ExecuteEvent);

            // set labels
            _themeColors.AutomaticColorLabel = "My Automatic";
            _themeColors.MoreColorsLabel = "My More Colors";
            _themeColors.NoColorLabel = "My No Color";
            _themeColors.RecentColorsCategoryLabel = "My Recent Colors";
            _themeColors.StandardColorsCategoryLabel = "My Standard Colors";
            _themeColors.ThemeColorsCategoryLabel = "My Theme Colors";

            // set colors
            _themeColors.ThemeColorsTooltips = new string[] { "yellow", "green", "red", "blue" };
            _themeColors.ThemeColors = new Color[] { Color.Yellow, Color.Green, Color.Red, Color.Blue };
        }

        void _themeColors_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            MessageBox.Show("Selected color is " + _themeColors.Color.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitDropDownColorPickers();
        }
    }
}
