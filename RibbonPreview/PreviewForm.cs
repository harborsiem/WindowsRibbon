using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RibbonGenerator;
using RibbonLib.Controls;
using RibbonLib.Interop;

namespace RibbonPreview
{
    public partial class PreviewForm : Form
    {
        private PreviewRibbonItems ribbonItems;
        private List<RibbonTabGroup> tabGroups = new List<RibbonTabGroup>();
        private RibbonClassBuilder classBuilder;

        public PreviewForm()
        {
            ribbonItems = PreviewRibbonItems.Instance;
            InitializeComponent();
            ribbon.ResourceName = ribbonItems.RibbonResourceName;
            classBuilder = new RibbonClassBuilder(ribbon);

            Load += MainForm_Load;
            Shown += MainForm_Shown;
            InitializeApplicationModes();
            InitializeContextualTabs(); //RibbonTabGroup
            InitializeContextPopups();
            InitializeComboBoxes();
            InitializeColorization();
            checkedListBoxAppModes.ItemCheck += CheckListBoxAppModesClickCheck;
            checkedListBoxContextTabs.ItemCheck += CheckListBoxContextTabsClickCheck;
            listBoxContextPopups.SelectedIndexChanged += ListBoxContextPopupsClick;
            numericUpDownB_R.ValueChanged += BackgroundColor_ValueChanged;
            numericUpDownB_G.ValueChanged += BackgroundColor_ValueChanged;
            numericUpDownB_B.ValueChanged += BackgroundColor_ValueChanged;
            numericUpDownH_R.ValueChanged += HighlightColor_ValueChanged;
            numericUpDownH_G.ValueChanged += HighlightColor_ValueChanged;
            numericUpDownH_B.ValueChanged += HighlightColor_ValueChanged;
            numericUpDownT_R.ValueChanged += TextColor_ValueChanged;
            numericUpDownT_G.ValueChanged += TextColor_ValueChanged;
            numericUpDownT_B.ValueChanged += TextColor_ValueChanged;
            this.getColorsButton.Click += new System.EventHandler(this.GetColorsButton_Click);
            this.setColorsButton.Click += new System.EventHandler(this.SetColorsButton_Click);
        }

        private void TextColor_ValueChanged(object sender, EventArgs e)
        {
            Color color = Color.FromArgb((int)numericUpDownT_R.Value, (int)numericUpDownT_G.Value, (int)numericUpDownT_B.Value);
            textColorPanel.BackColor = color;
        }

        private void HighlightColor_ValueChanged(object sender, EventArgs e)
        {
            Color color = Color.FromArgb((int)numericUpDownH_R.Value, (int)numericUpDownH_G.Value, (int)numericUpDownH_B.Value);
            highlightColorPanel.BackColor = color;
        }

        private void BackgroundColor_ValueChanged(object sender, EventArgs e)
        {
            Color color = Color.FromArgb((int)numericUpDownB_R.Value, (int)numericUpDownB_G.Value, (int)numericUpDownB_B.Value);
            backgroundColorPanel.BackColor = color;
        }

        private void InitializeComboBoxes()
        {
            List<RibbonItem> selectedItems = new List<RibbonItem>();
            IList<RibbonItem> items = ribbonItems.Parser.Results.RibbonItems;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].RibbonClassName == "RibbonComboBox")
                {
                    selectedItems.Add(items[i]);
                }
            }
            for (int i = 0; i < selectedItems.Count; i++)
            {
                RibbonItem item = selectedItems[i];
                RibbonComboBox combo = (RibbonComboBox)classBuilder.BuildRibbonClass(item.RibbonClassName, item.CommandName, item.CommandId);
                combo.RepresentativeString = "XXXXXX";
            }
        }

        private void CheckListBoxAppModesClickCheck(object sender, ItemCheckEventArgs e)
        {
            int i, j;
            List<byte> AppModes = new List<byte>();
            for (i = 0; i < checkedListBoxAppModes.Items.Count; i++)
            {
                if (checkedListBoxAppModes.GetItemChecked(i) || (e.Index == i && e.NewValue == CheckState.Checked))
                {
                    if (!(e.Index == i && e.NewValue == CheckState.Unchecked))
                    {
                        j = int.Parse((string)checkedListBoxAppModes.Items[i]);
                        AppModes.Add((byte)j);
                    }
                }
            }
            if (AppModes.Count == 0)
            {
                AppModes.Add(0);
                checkedListBoxAppModes.SetItemChecked(0, true);
            }
            byte[] ba = AppModes.ToArray();
            ribbon.SetModes(ba);
        }

        private void InitializeApplicationModes()
        {
            int i;
            uint allApplicationModes = ribbonItems.Parser.Results.AllApplicationModes;
            checkedListBoxAppModes.Visible = (allApplicationModes != 0);
            labelAppModes.Visible = (allApplicationModes == 0);
            if (allApplicationModes != 0)
            {
                checkedListBoxAppModes.Items.Insert(0, "0");
                for (i = 1; i < 32; i++)
                {
                    if ((allApplicationModes & (1 << i)) != 0)
                    {
                        checkedListBoxAppModes.Items.Add(i.ToString());
                    }
                }
                checkedListBoxAppModes.SetItemChecked(0, true);
            }
        }

        private void CheckListBoxContextTabsClickCheck(object sender, ItemCheckEventArgs e)
        {
            RibbonTabGroup tabGroup = tabGroups[e.Index];
            if (e.NewValue == CheckState.Checked)
            {
                tabGroup.ContextAvailable = RibbonLib.Interop.ContextAvailability.Available;
            }
            if (e.NewValue == CheckState.Unchecked)
            {
                tabGroup.ContextAvailable = RibbonLib.Interop.ContextAvailability.NotAvailable;
            }
        }

        private void InitializeContextualTabs()
        {
            List<RibbonItem> selectedItems = new List<RibbonItem>();
            IList<RibbonItem> items = ribbonItems.Parser.Results.RibbonItems;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].RibbonClassName == "RibbonTabGroup")
                {
                    selectedItems.Add(items[i]);
                }
            }
            for (int i = 0; i < selectedItems.Count; i++)
            {
                RibbonItem item = selectedItems[i];
                checkedListBoxContextTabs.Items.Add(item);
                RibbonTabGroup tabGroup = (RibbonTabGroup)classBuilder.BuildRibbonClass(item.RibbonClassName, item.CommandName, item.CommandId);
                tabGroups.Add(tabGroup);
            }

            checkedListBoxContextTabs.Visible = (checkedListBoxContextTabs.Items.Count > 0);
            labelContextTabs.Visible = (checkedListBoxContextTabs.Items.Count == 0);
        }

        private void ListBoxContextPopupsClick(object sender, EventArgs e)
        {
            if (listBoxContextPopups.SelectedIndex < 0)
                return;

            RibbonItem item = (RibbonItem)(listBoxContextPopups.Items[listBoxContextPopups.SelectedIndex]);
            ribbon.ShowContextPopup(item.CommandId, Cursor.Position.X, Cursor.Position.Y);
        }

        private void InitializeContextPopups()
        {
            List<RibbonItem> selectedItems = new List<RibbonItem>();
            IList<RibbonItem> items = ribbonItems.Parser.Results.RibbonItems;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].IsContextPopup)
                {
                    selectedItems.Add(items[i]);
                }
            }
            for (int i = 0; i < selectedItems.Count; i++)
            {
                listBoxContextPopups.Items.Add(selectedItems[i]);
            }

            listBoxContextPopups.Visible = (listBoxContextPopups.Items.Count > 0);
            labelContextPopups.Visible = (listBoxContextPopups.Items.Count == 0);
        }

        private void InitializeColorization()
        {
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            //MessageBox.Show(rHeight.ToString() + "; " + tTop.ToString());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            int rHeight;
            int tTop;
            rHeight = ribbon.Height;
            tTop = tabControl.Top;
            tabControl.Top = rHeight;
            tabControl.Height = tabControl.Height + tTop - rHeight;
        }

        private void GetColorsButton_Click(object sender, EventArgs e)
        {
            RibbonColors colors = GetRibbonColors();
            SetRibbonColors(colors);
            //float b_h = colors.BackgroundColor.GetHue();
            //float b_s = colors.BackgroundColor.GetSaturation();
            //float b_b = colors.BackgroundColor.GetBrightness();
            //HSL b_hsl = new HSL();
            //b_hsl.H = (b_h / 360.0);
            //b_hsl.S = (b_s);
            //b_hsl.L = (b_b);

            //float h_h = colors.HighlightColor.GetHue();
            //float h_s = colors.HighlightColor.GetSaturation();
            //float h_b = colors.HighlightColor.GetBrightness();
            //HSL h_hsl = new HSL();
            //h_hsl.H = (h_h / 360.0);
            //h_hsl.S = (h_s);
            //h_hsl.L = (h_b); ;

            //float t_h = colors.TextColor.GetHue();
            //float t_s = colors.TextColor.GetSaturation();
            //float t_b = colors.TextColor.GetBrightness();
            //HSL t_hsl = new HSL();
            //t_hsl.H = (t_h / 360.0);
            //t_hsl.S = (t_s);
            //t_hsl.L = (t_b);

            //HSB b_hsb = ColorHelp.HSLToHSB(b_hsl);
            //HSB h_hsb = ColorHelp.HSLToHSB(h_hsl);
            //HSB t_hsb = ColorHelp.HSLToHSB(t_hsl);

            //uint backgroundresult = ColorHelper.HSBToUInt32(ColorHelper.HSLToHSB(ColorHelper.RGBToHSL(backgroundColor)));
            //uint highlightresult = ColorHelper.HSBToUInt32(ColorHelper.HSLToHSB(ColorHelper.RGBToHSL(highlightColor)));
            //uint textresult = ColorHelper.HSBToUInt32(ColorHelper.HSLToHSB(ColorHelper.RGBToHSL(textColor)));
            //uint backgroundresult = ColorHelp.RGBToUInt32(backgroundColor);
            //uint highlightresult = ColorHelp.RGBToUInt32(highlightColor);
            //uint textresult = ColorHelp.RGBToUInt32(textColor);
        }

        private void SetColorsButton_Click(object sender, EventArgs e)
        {
            ribbon.SetColors(backgroundColorPanel.BackColor, highlightColorPanel.BackColor, textColorPanel.BackColor);
        }

        private void SetRibbonColors(RibbonColors colors)
        {
            numericUpDownB_R.Value = colors.BackgroundColor.R;
            numericUpDownB_G.Value = colors.BackgroundColor.G;
            numericUpDownB_B.Value = colors.BackgroundColor.B;
            numericUpDownH_R.Value = colors.HighlightColor.R;
            numericUpDownH_G.Value = colors.HighlightColor.G;
            numericUpDownH_B.Value = colors.HighlightColor.B;
            numericUpDownT_R.Value = colors.TextColor.R;
            numericUpDownT_G.Value = colors.TextColor.G;
            numericUpDownT_B.Value = colors.TextColor.B;
            backgroundColorPanel.BackColor = colors.BackgroundColor;
            highlightColorPanel.BackColor = colors.HighlightColor;
            textColorPanel.BackColor = colors.TextColor;
        }

        internal RibbonColors GetRibbonColors()
        {
            RibbonColors colors = new RibbonColors();
            IPropertyStore propertyStore = (IPropertyStore)ribbon.Framework;
            PropVariant backgroundColorProp;
            PropVariant highlightColorProp;
            PropVariant textColorProp;

            // get ribbon colors
            propertyStore.GetValue(ref RibbonProperties.GlobalBackgroundColor, out backgroundColorProp);
            propertyStore.GetValue(ref RibbonProperties.GlobalHighlightColor, out highlightColorProp);
            propertyStore.GetValue(ref RibbonProperties.GlobalTextColor, out textColorProp);
            uint background = (uint)backgroundColorProp.Value;
            uint highlight = (uint)highlightColorProp.Value;
            uint text = (uint)textColorProp.Value;
            colors.BackgroundColor = ColorHelp.UInt32ToRGB(background);
            colors.HighlightColor = ColorHelp.UInt32ToRGB(highlight);
            colors.TextColor = ColorHelp.UInt32ToRGB(text);
            return colors;
        }

        internal class RibbonColors
        {
            public Color BackgroundColor;
            public Color HighlightColor;
            public Color TextColor;
        }
    }
}
