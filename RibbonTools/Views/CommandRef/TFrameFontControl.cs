using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIRibbonTools
{
    [DesignTimeVisible(false)]
    partial class TFrameFontControl : TFrameFloatieFontControl
    {
        private static Image sample = ImageManager.FontControlSample();

        private Label Label2 { get => _label2; }
        private ComboBox ComboBoxFontType { get => _comboBoxFontType; }
        private CheckBox CheckBoxStrikethrough { get => _checkBoxStrikethrough; }
        private CheckBox CheckBoxUnderline { get => _checkBoxUnderline; }
        private CheckBox CheckBoxHighlight { get => _checkBoxHighlight; }
        private CheckBox CheckBoxGrowShrink { get => _checkBoxGrowShrink; }

        private TRibbonFontControl _fontControl;

        public TFrameFontControl()
        {
            bool designtime = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
            if (designtime)
                InitializeComponent();
        }

        protected override void InitComponentStep1()
        {
            if (components == null)
                components = new Container();
            this._label2 = new System.Windows.Forms.Label();
            this._comboBoxFontType = new System.Windows.Forms.ComboBox();
            this._checkBoxStrikethrough = new System.Windows.Forms.CheckBox();
            this._checkBoxUnderline = new System.Windows.Forms.CheckBox();
            this._checkBoxHighlight = new System.Windows.Forms.CheckBox();
            this._checkBoxGrowShrink = new System.Windows.Forms.CheckBox();
            base.InitComponentStep1();
        }

        protected override void InitSuspend()
        {

            base.InitSuspend();
        }

        protected override void InitComponentStep2()
        {
            base.InitComponentStep2();
            // 
            // _label2
            // 
            this._label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label2.AutoSize = true;
            this._label2.Location = new System.Drawing.Point(3, 27);
            this._label2.Name = "_label2";
            this._label2.Size = new System.Drawing.Size(55, 27);
            this._label2.TabIndex = 7;
            this._label2.Text = "Font Type";
            this._label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _comboBoxFontType
            // 
            this._comboBoxFontType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LayoutPanel.SetColumnSpan(this._comboBoxFontType, 3);
            this._comboBoxFontType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxFontType.Items.AddRange(new object[] {
            "Font only",
            "Font with Color",
            "Rich Font"});
            this._comboBoxFontType.Location = new System.Drawing.Point(123, 30);
            this._comboBoxFontType.MaxDropDownItems = 5;
            this._comboBoxFontType.Name = "_comboBoxFontType";
            this._comboBoxFontType.Size = new System.Drawing.Size(250, 21);
            this._comboBoxFontType.TabIndex = 2;
            // 
            // _checkBoxStrikethrough
            // 
            this._checkBoxStrikethrough.AutoSize = true;
            this.LayoutPanel.SetColumnSpan(this._checkBoxStrikethrough, 2);
            this._checkBoxStrikethrough.Location = new System.Drawing.Point(3, 165);
            this._checkBoxStrikethrough.Name = "_checkBoxStrikethrough";
            this._checkBoxStrikethrough.Size = new System.Drawing.Size(154, 17);
            this._checkBoxStrikethrough.TabIndex = 8;
            this._checkBoxStrikethrough.Text = "Strikethrough button visible";
            // 
            // _checkBoxUnderline
            // 
            this._checkBoxUnderline.AutoSize = true;
            this.LayoutPanel.SetColumnSpan(this._checkBoxUnderline, 2);
            this._checkBoxUnderline.Location = new System.Drawing.Point(3, 192);
            this._checkBoxUnderline.Name = "_checkBoxUnderline";
            this._checkBoxUnderline.Size = new System.Drawing.Size(136, 17);
            this._checkBoxUnderline.TabIndex = 9;
            this._checkBoxUnderline.Text = "Underline button visible";
            // 
            // _checkBoxHighlight
            // 
            this._checkBoxHighlight.AutoSize = true;
            this.LayoutPanel.SetColumnSpan(this._checkBoxHighlight, 2);
            this._checkBoxHighlight.Location = new System.Drawing.Point(3, 219);
            this._checkBoxHighlight.Name = "_checkBoxHighlight";
            this._checkBoxHighlight.Size = new System.Drawing.Size(132, 17);
            this._checkBoxHighlight.TabIndex = 10;
            this._checkBoxHighlight.Text = "Highlight button visible";
            // 
            // _checkBoxGrowShrink
            // 
            this._checkBoxGrowShrink.AutoSize = true;
            this.LayoutPanel.SetColumnSpan(this._checkBoxGrowShrink, 2);
            this._checkBoxGrowShrink.Location = new System.Drawing.Point(3, 246);
            this._checkBoxGrowShrink.Name = "_checkBoxGrowShrink";
            this._checkBoxGrowShrink.Size = new System.Drawing.Size(195, 17);
            this._checkBoxGrowShrink.TabIndex = 11;
            this._checkBoxGrowShrink.Text = "Grow and shrink buttongroup visible";
            LabelHeader.Text = "  FontControl Properties";
            LabelHeader.ImageIndex = 13;
        }

        protected override void InitComponentStep3()
        {
            this.LayoutPanel.Controls.Add(this._label2, 0, 1);
            this.LayoutPanel.Controls.Add(this._comboBoxFontType, 1, 1);
            this.LayoutPanel.Controls.Add(this._checkBoxStrikethrough, 0, 6);
            this.LayoutPanel.Controls.Add(this._checkBoxUnderline, 0, 7);
            this.LayoutPanel.Controls.Add(this._checkBoxHighlight, 0, 8);
            this.LayoutPanel.Controls.Add(this._checkBoxGrowShrink, 0, 9);

            base.InitComponentStep3();
        }

        protected override void InitResume()
        {

            base.InitResume();
        }

        protected override void InitEvents()
        {
            base.InitEvents();
            ComboBoxFontType.SelectedIndexChanged += ComboBoxFontTypeChange;
            CheckBoxStrikethrough.Click += CheckBoxStrikethroughClick;
            CheckBoxUnderline.Click += CheckBoxUnderlineClick;
            CheckBoxHighlight.Click += CheckBoxHighlightClick;
            CheckBoxGrowShrink.Click += CheckBoxGrowShrinkClick;
        }

        protected override void InitTooltips(IContainer components)
        {
            base.InitTooltips(components);
            viewsTip.SetToolTip(ComboBoxFontType, "The type of font control to show");
            viewsTip.SetToolTip(CheckBoxStrikethrough, "Whether the strikethrough button is shown in the font control");
            viewsTip.SetToolTip(CheckBoxUnderline, "Whether the underline button is shown in the font control");
            viewsTip.SetToolTip(CheckBoxHighlight, "Whether the highlight button is shown in the font control");
            viewsTip.SetToolTip(CheckBoxGrowShrink, "Since Windows 8" + Environment.NewLine +
                "Whether the grow and shrink buttongroup is shown in the font control");
        }

        private void CheckBoxGrowShrinkClick(object sender, EventArgs e)
        {
            if (CheckBoxGrowShrink.Checked != _fontControl.IsGrowShrinkButtonGroupVisible)
            {
                _fontControl.IsGrowShrinkButtonGroupVisible = CheckBoxGrowShrink.Checked;
                Modified();
            }
        }

        private void CheckBoxHighlightClick(object sender, EventArgs e)
        {
            if (CheckBoxHighlight.Checked != _fontControl.IsHighlightButtonVisible)
            {
                _fontControl.IsHighlightButtonVisible = CheckBoxHighlight.Checked;
                Modified();
            }
        }

        private void CheckBoxStrikethroughClick(object sender, EventArgs e)
        {
            if (CheckBoxStrikethrough.Checked != _fontControl.IsStrikethroughButtonVisible)
            {
                _fontControl.IsStrikethroughButtonVisible = CheckBoxStrikethrough.Checked;
                Modified();
            }
        }

        private void CheckBoxUnderlineClick(object sender, EventArgs e)
        {
            if (CheckBoxUnderline.Checked != _fontControl.IsUnderlineButtonVisible)
            {
                _fontControl.IsUnderlineButtonVisible = CheckBoxUnderline.Checked;
                Modified();
            }
        }

        private void ComboBoxFontTypeChange(object sender, EventArgs e)
        {
            if (ComboBoxFontType.SelectedIndex != (int)(_fontControl.FontType))
            {
                _fontControl.FontType = (RibbonFontType)(ComboBoxFontType.SelectedIndex);
                if (_fontControl.FontType != RibbonFontType.FontOnly)
                    _fontControl.IsGrowShrinkButtonGroupVisible = true;
                else
                    _fontControl.IsGrowShrinkButtonGroupVisible = false;
                UpdateControls();
                Modified();
            }
        }

        protected override void Initialize(TRibbonObject subject)
        {
            base.Initialize(subject);
            _fontControl = subject as TRibbonFontControl;
            ComboBoxFontType.SelectedIndex = (int)(_fontControl.FontType);
            UpdateControls();
        }

        private void UpdateControls()
        {
            if (_fontControl.FontType == RibbonFontType.RichFont)
            {
                CheckBoxStrikethrough.Checked = true;
                CheckBoxUnderline.Checked = true;
                CheckBoxHighlight.Checked = true;
                CheckBoxStrikethrough.Enabled = false;
                CheckBoxUnderline.Enabled = false;
                CheckBoxHighlight.Enabled = false;
            }
            else
            {
                CheckBoxStrikethrough.Enabled = true;
                CheckBoxUnderline.Enabled = true;
                CheckBoxHighlight.Enabled = true;
                CheckBoxStrikethrough.Checked = _fontControl.IsStrikethroughButtonVisible;
                CheckBoxUnderline.Checked = _fontControl.IsUnderlineButtonVisible;
                CheckBoxHighlight.Checked = _fontControl.IsHighlightButtonVisible;
            }
            if (_fontControl.FontType == RibbonFontType.FontOnly)
            {
                CheckBoxGrowShrink.Checked = false;
                CheckBoxGrowShrink.Enabled = false;
            }
            else
            {
                CheckBoxGrowShrink.Enabled = true;
                CheckBoxGrowShrink.Checked = _fontControl.IsGrowShrinkButtonGroupVisible;
            }
        }

        protected override Image SetImageSample()
        {
            return sample;
        }
    }
}
