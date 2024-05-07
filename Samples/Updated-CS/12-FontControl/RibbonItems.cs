//#define Old
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Controls.Events;
using RibbonLib.Interop;
using _12_FontControl;

namespace RibbonLib.Controls
{
    partial class RibbonItems
    {
        //First write a text to the RichTextBox RichTextBox1 and mark it as selected.
        //Then you can see what happend with Preview, CancelPreview and Execute by the ribbon fontcontrol

        private Form1 _form;

        public void Init(Form1 form)
        {
            _form = form;
            RichFont.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_richFont_ExecuteEvent);
            RichFont.PreviewEvent += new EventHandler<ExecuteEventArgs>(_richFont_OnPreview);
            RichFont.CancelPreviewEvent += new EventHandler<ExecuteEventArgs>(_richFont_OnCancelPreview);
        }

        void _richFont_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
#if DEBUG
            PrintFontControlProperties(RichFont);
#if Old
            PrintChangedProperties(e.CommandExecutionProperties);
#else
            FontControlEventArgs args = FontControlEventArgs.Create(sender, e);
            PrintChangedProperties(args.ChangedFontValues);
#endif
#endif
            // skip if selected font is not valid
            if ((RichFont.Family == null) ||
                 (RichFont.Family.Trim() == string.Empty) ||
                 (RichFont.Size == 0))
            {
                return;
            }

            // prepare font style
            FontStyle fontStyle = FontStyle.Regular;
            if (RichFont.Bold == FontProperties.Set)
            {
                fontStyle |= FontStyle.Bold;
            }
            if (RichFont.Italic == FontProperties.Set)
            {
                fontStyle |= FontStyle.Italic;
            }
            if (RichFont.Underline == FontUnderline.Set)
            {
                fontStyle |= FontStyle.Underline;
            }
            if (RichFont.Strikethrough == FontProperties.Set)
            {
                fontStyle |= FontStyle.Strikeout;
            }

            // set selected font
            // creating a new font can't fail if the font doesn't support the requested style
            // or if the font family name doesn't exist
            try
            {
                _form.RichTextBox1.SelectionFont = new Font(RichFont.Family, (float)RichFont.Size, fontStyle);
            }
            catch (ArgumentException)
            {
            }

            // set selected colors
            _form.RichTextBox1.SelectionColor = RichFont.ForegroundColor;
            _form.RichTextBox1.SelectionBackColor = RichFont.BackgroundColor;

            // set subscript / superscript
            switch (RichFont.VerticalPositioning)
            {
                case FontVerticalPosition.NotSet:
                case FontVerticalPosition.NotAvailable:
                    _form.RichTextBox1.SelectionCharOffset = 0;
                    break;

                case FontVerticalPosition.SuperScript:
                    _form.RichTextBox1.SelectionCharOffset = 10;
                    break;

                case FontVerticalPosition.SubScript:
                    _form.RichTextBox1.SelectionCharOffset = -10;
                    break;
            }
        }

        void _richFont_OnPreview(object sender, ExecuteEventArgs e)
        {
#if Old
            PropVariant propChangesProperties;
            e.CommandExecutionProperties.GetValue(ref RibbonProperties.FontProperties_ChangedProperties, out propChangesProperties);
            IPropertyStore changedProperties = (IPropertyStore)propChangesProperties.Value;
            UpdateRichTextBox(changedProperties);
#else
            FontControlEventArgs args = FontControlEventArgs.Create(sender, e);
            Dictionary<FontPropertiesEnum, object> dict = args.ChangedFontValues;
            FontPropertyStore store = args.CurrentFontStore;
            UpdateRichTextBox(dict);
            //UpdateRichTextBox(store);
#endif
        }

        void _richFont_OnCancelPreview(object sender, ExecuteEventArgs e)
        {
#if Old
            IPropertyStore fontProperties = (IPropertyStore)e.CurrentValue.PropVariant.Value;

            UpdateRichTextBox(fontProperties);
#else
            FontControlEventArgs args = FontControlEventArgs.Create(sender, e);
            Dictionary<FontPropertiesEnum, object> dict = args.ChangedFontValues;
            FontPropertyStore store = args.CurrentFontStore;
            //UpdateRichTextBox(dict);
            UpdateRichTextBox(store);
#endif
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

#if Old
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
#else

        private static void PrintChangedProperties(Dictionary<FontPropertiesEnum, object> changedProps)
        {
            Debug.WriteLine("");
            Debug.WriteLine("FontControl changed properties:");
            if (changedProps != null)
            {
                foreach (KeyValuePair<FontPropertiesEnum, object> pair in changedProps)
                {
                    Debug.WriteLine("FontProperties_" + pair.Key.ToString()); // + ": " + pair.Value.ToString());
                }
            }
        }
#endif

#if Old
        private void UpdateRichTextBox(IPropertyStore propertyStore)
        {
            FontPropertyStore fontPropertyStore = new FontPropertyStore(propertyStore);
            PropVariant propValue;

            FontStyle fontStyle;
            string family;
            float size;

            if (_form.RichTextBox1.SelectionFont != null)
            {
                fontStyle = _form.RichTextBox1.SelectionFont.Style;
                family = _form.RichTextBox1.SelectionFont.FontFamily.Name;
                size = _form.RichTextBox1.SelectionFont.Size;
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
                _form.RichTextBox1.SelectionFont = new Font(family, size, fontStyle);
            }
            catch (ArgumentException)
            {
            }
        }
#else
        private void UpdateRichTextBox(Dictionary<FontPropertiesEnum, object> changedProps)
        {
            string family = null;
            float? size = null;
            if (changedProps != null)
            {
                if (changedProps.ContainsKey(FontPropertiesEnum.Family))
                    family = (string)changedProps[FontPropertiesEnum.Family];
                if (changedProps.ContainsKey(FontPropertiesEnum.Size))
                    size = (float)(decimal)changedProps[FontPropertiesEnum.Size];
            }
            UpdateRichTextBox(family, size);
        }

        private void UpdateRichTextBox(FontPropertyStore propertyStore)
        {
            UpdateRichTextBox(propertyStore.Family, (float)propertyStore.Size);
        }

        private void UpdateRichTextBox(string newFamily, float? newSize)
        {
            FontStyle fontStyle;
            string family;
            float size;

            if (_form.RichTextBox1.SelectionFont != null)
            {
                fontStyle = _form.RichTextBox1.SelectionFont.Style;
                family = _form.RichTextBox1.SelectionFont.FontFamily.Name;
                size = _form.RichTextBox1.SelectionFont.Size;
            }
            else
            {
                fontStyle = FontStyle.Regular;
                family = string.Empty;
                size = 0;
            }
            if (newFamily != null)
                family = newFamily;
            if (newSize != null)
                size = (float)newSize;

            // creating a new font can't fail if the font doesn't support the requested style
            // or if the font family name doesn't exist
            try
            {
                _form.RichTextBox1.SelectionFont = new Font(family, size, fontStyle);
            }
            catch (ArgumentException)
            {
            }
        }

#endif

        internal void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            // update font control font
            if (_form.RichTextBox1.SelectionFont != null)
            {
                RichFont.Family = _form.RichTextBox1.SelectionFont.FontFamily.Name;
                RichFont.Size = (decimal)_form.RichTextBox1.SelectionFont.Size;
                RichFont.Bold = _form.RichTextBox1.SelectionFont.Bold ? FontProperties.Set : FontProperties.NotSet;
                RichFont.Italic = _form.RichTextBox1.SelectionFont.Italic ? FontProperties.Set : FontProperties.NotSet;
                RichFont.Underline = _form.RichTextBox1.SelectionFont.Underline ? FontUnderline.Set : FontUnderline.NotSet;
                RichFont.Strikethrough = _form.RichTextBox1.SelectionFont.Strikeout ? FontProperties.Set : FontProperties.NotSet;
            }
            else
            {
                RichFont.Family = string.Empty;
                RichFont.Size = 0;
                RichFont.Bold = FontProperties.NotAvailable;
                RichFont.Italic = FontProperties.NotAvailable;
                RichFont.Underline = FontUnderline.NotAvailable;
                RichFont.Strikethrough = FontProperties.NotAvailable;
            }

            // update font control colors
            RichFont.ForegroundColor = _form.RichTextBox1.SelectionColor;
            RichFont.BackgroundColor = _form.RichTextBox1.SelectionBackColor;

            // update font control vertical positioning
            switch (_form.RichTextBox1.SelectionCharOffset)
            {
                case 0:
                    RichFont.VerticalPositioning = FontVerticalPosition.NotSet;
                    break;

                case 10:
                    RichFont.VerticalPositioning = FontVerticalPosition.SuperScript;
                    break;

                case -10:
                    RichFont.VerticalPositioning = FontVerticalPosition.SubScript;
                    break;
            }
        }

        public void Load()
        {
        }

    }
}
