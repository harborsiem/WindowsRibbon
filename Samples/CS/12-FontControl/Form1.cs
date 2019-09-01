using System;
using System.Drawing;
using System.Windows.Forms;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Controls.Events;
using RibbonLib.Interop;
using System.Diagnostics;

namespace _12_FontControl
{
    public enum RibbonMarkupCommands : uint 
    {
         cmdTabMain = 1001,
         cmdGroupRichFont = 1002,
         cmdRichFont = 1003,
    }

    public partial class Form1 : Form
    {
        private RibbonFontControl _richFont;

        public Form1()
        {
            InitializeComponent();

            _richFont = new RibbonFontControl(_ribbon, (uint)RibbonMarkupCommands.cmdRichFont);

            _richFont.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_richFont_ExecuteEvent);
            _richFont.PreviewEvent += new EventHandler<ExecuteEventArgs>(_richFont_OnPreview);
            _richFont.CancelPreviewEvent += new EventHandler<ExecuteEventArgs>(_richFont_OnCancelPreview);
        }
                      
        void _richFont_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
#if DEBUG
            PrintFontControlProperties(_richFont);
            PrintChangedProperties(e.CommandExecutionProperties);
#endif
            // skip if selected font is not valid
            if ((_richFont.Family == null) ||
                 (_richFont.Family.Trim() == string.Empty) ||
                 (_richFont.Size == 0))
            {
                return;
            }

            // prepare font style
            FontStyle fontStyle = FontStyle.Regular;
            if (_richFont.Bold == FontProperties.Set)
            {
                fontStyle |= FontStyle.Bold;
            }
            if (_richFont.Italic == FontProperties.Set)
            {
                fontStyle |= FontStyle.Italic;
            }
            if (_richFont.Underline == FontUnderline.Set)
            {
                fontStyle |= FontStyle.Underline;
            }
            if (_richFont.Strikethrough == FontProperties.Set)
            {
                fontStyle |= FontStyle.Strikeout;
            }

            // set selected font
            // creating a new font can't fail if the font doesn't support the requested style
            // or if the font family name doesn't exist
            try
            {
                richTextBox1.SelectionFont = new Font(_richFont.Family, (float)_richFont.Size, fontStyle);
            }
            catch (ArgumentException)
            {
            }

            // set selected colors
            richTextBox1.SelectionColor = _richFont.ForegroundColor;
            richTextBox1.SelectionBackColor = _richFont.BackgroundColor;

            // set subscript / superscript
            switch (_richFont.VerticalPositioning)
            {
                case FontVerticalPosition.NotSet:
                case FontVerticalPosition.NotAvailable:
                    richTextBox1.SelectionCharOffset = 0;
                    break;

                case FontVerticalPosition.SuperScript:
                    richTextBox1.SelectionCharOffset = 10;
                    break;

                case FontVerticalPosition.SubScript:
                    richTextBox1.SelectionCharOffset = -10;
                    break;
            }
        }

        void _richFont_OnPreview(object sender, ExecuteEventArgs e)
        {
            PropVariant propChangesProperties;
            e.CommandExecutionProperties.GetValue(ref RibbonProperties.FontProperties_ChangedProperties, out propChangesProperties);
            IPropertyStore changedProperties = (IPropertyStore)propChangesProperties.Value;

            UpdateRichTextBox(changedProperties);
        }

        void _richFont_OnCancelPreview(object sender, ExecuteEventArgs e)
        {
            IPropertyStore fontProperties = (IPropertyStore)e.CurrentValue.PropVariant.Value;

            UpdateRichTextBox(fontProperties);
        }

        private static void PrintFontControlProperties(RibbonFontControl fontControl)
        {
            Debug.WriteLine("");
            Debug.WriteLine("FontControl current properties:");
            Debug.WriteLine("Family: " + fontControl.Family);
            Debug.WriteLine("Size: " + fontControl.Size.ToString());
            Debug.WriteLine("Bold: " + fontControl.Bold.ToString());
            Debug.WriteLine("Italic: " + fontControl.Italic.ToString());
            Debug.WriteLine("Underline: " + fontControl.Underline.ToString());
            Debug.WriteLine("Strikethrough: " + fontControl.Strikethrough.ToString());
            Debug.WriteLine("ForegroundColor: " + fontControl.ForegroundColor.ToString());
            Debug.WriteLine("BackgroundColor: " + fontControl.BackgroundColor.ToString());
            Debug.WriteLine("VerticalPositioning: " + fontControl.VerticalPositioning.ToString());
        }

        private static void PrintChangedProperties(IUISimplePropertySet commandExecutionProperties)
        {
            PropVariant propChangesProperties;
            commandExecutionProperties.GetValue(ref RibbonProperties.FontProperties_ChangedProperties, out propChangesProperties);
            IPropertyStore changedProperties = (IPropertyStore)propChangesProperties.Value;
            uint changedPropertiesNumber;
            changedProperties.GetCount(out changedPropertiesNumber);

            Debug.WriteLine("");
            Debug.WriteLine("FontControl changed properties:");
            for (uint i = 0; i < changedPropertiesNumber; ++i)
            {
                PropertyKey propertyKey;
                changedProperties.GetAt(i, out propertyKey);
                Debug.WriteLine(RibbonProperties.GetPropertyKeyName(ref propertyKey));
            }
        }

        private void UpdateRichTextBox(IPropertyStore propertyStore)
        {
            FontPropertyStore fontPropertyStore = new FontPropertyStore(propertyStore);
            PropVariant propValue;

            FontStyle fontStyle;
            string family;
            float size;

            if (richTextBox1.SelectionFont != null)
            {
                fontStyle = richTextBox1.SelectionFont.Style;
                family = richTextBox1.SelectionFont.FontFamily.Name;
                size = richTextBox1.SelectionFont.Size;
            }
            else
            {
                fontStyle = FontStyle.Regular;
                family = string.Empty;
                size = 0;
            }

            if (propertyStore.GetValue(ref RibbonProperties.FontProperties_Family, out propValue) == HRESULT.S_OK)
            {
                family = fontPropertyStore.Family;
            }
            if (propertyStore.GetValue(ref RibbonProperties.FontProperties_Size, out propValue) == HRESULT.S_OK)
            {
                size = (float)fontPropertyStore.Size;
            }

            // creating a new font can't fail if the font doesn't support the requested style
            // or if the font family name doesn't exist
            try
            {
                richTextBox1.SelectionFont = new Font(family, size, fontStyle);
            }
            catch (ArgumentException)
            { 
            }
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            // update font control font
            if (richTextBox1.SelectionFont != null)
            {
                _richFont.Family = richTextBox1.SelectionFont.FontFamily.Name;
                _richFont.Size = (decimal)richTextBox1.SelectionFont.Size;
                _richFont.Bold = richTextBox1.SelectionFont.Bold ? FontProperties.Set : FontProperties.NotSet;
                _richFont.Italic = richTextBox1.SelectionFont.Italic ? FontProperties.Set : FontProperties.NotSet;
                _richFont.Underline = richTextBox1.SelectionFont.Underline ? FontUnderline.Set : FontUnderline.NotSet;
                _richFont.Strikethrough = richTextBox1.SelectionFont.Strikeout ? FontProperties.Set : FontProperties.NotSet;
            }
            else
            {
                _richFont.Family = string.Empty;
                _richFont.Size = 0;
                _richFont.Bold = FontProperties.NotAvailable;
                _richFont.Italic = FontProperties.NotAvailable;
                _richFont.Underline = FontUnderline.NotAvailable;
                _richFont.Strikethrough = FontProperties.NotAvailable;
            }

            // update font control colors
            _richFont.ForegroundColor = richTextBox1.SelectionColor;
            _richFont.BackgroundColor = richTextBox1.SelectionBackColor;

            // update font control vertical positioning
            switch (richTextBox1.SelectionCharOffset)
            { 
                case 0:
                    _richFont.VerticalPositioning = FontVerticalPosition.NotSet;
                    break;

                case 10:
                    _richFont.VerticalPositioning = FontVerticalPosition.SuperScript;
                    break;

                case -10:
                    _richFont.VerticalPositioning = FontVerticalPosition.SubScript;
                    break;
            }
        }
    }
}
