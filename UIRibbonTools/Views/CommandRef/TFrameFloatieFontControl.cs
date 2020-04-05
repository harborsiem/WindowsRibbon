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
    partial class TFrameFloatieFontControl : TFrameCommandRefObject
    {
        private Label Label3 { get => _label3; }
        private Label Label5 { get => _label5; }
        protected NumericUpDown UpDownMinFontSize { get => _upDownMinFontSize; }
        protected NumericUpDown UpDownMaxFontSize { get => _upDownMaxFontSize; }
        protected CheckBox CheckBoxTrueTypeOnly { get => _checkBoxTrueTypeOnly; }
        protected CheckBox CheckBoxVerticalFonts { get => _checkBoxVerticalFonts; }

        private TRibbonFloatieFontControl _floatControl;

        public TFrameFloatieFontControl()
        {
            bool designtime = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
            if (designtime)
                InitializeComponent();
        }

        protected override void InitComponentStep1()
        {
            if (components == null)
                components = new Container();
            this._label3 = new System.Windows.Forms.Label();
            this._upDownMinFontSize = new System.Windows.Forms.NumericUpDown();
            this._label5 = new System.Windows.Forms.Label();
            this._upDownMaxFontSize = new System.Windows.Forms.NumericUpDown();
            this._checkBoxTrueTypeOnly = new System.Windows.Forms.CheckBox();
            this._checkBoxVerticalFonts = new System.Windows.Forms.CheckBox();
            base.InitComponentStep1();
        }

        protected override void InitSuspend()
        {
            ((System.ComponentModel.ISupportInitialize)(this._upDownMinFontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._upDownMaxFontSize)).BeginInit();

            base.InitSuspend();
        }

        protected override void InitComponentStep2()
        {
            base.InitComponentStep2();

            // 
            // _label3
            // 
            this._label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label3.AutoSize = true;
            this._label3.Location = new System.Drawing.Point(3, 54);
            this._label3.Name = "_label3";
            this._label3.Size = new System.Drawing.Size(90, 27);
            this._label3.TabIndex = 0;
            this._label3.Text = "Minimum font size";
            this._label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _upDownMinFontSize
            // 
            this._upDownMinFontSize.Location = new System.Drawing.Point(123, 57);
            this._upDownMinFontSize.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this._upDownMinFontSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._upDownMinFontSize.Name = "_upDownMinFontSize";
            this._upDownMinFontSize.Size = new System.Drawing.Size(81, 20);
            this._upDownMinFontSize.TabIndex = 4;
            this._upDownMinFontSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // _label5
            // 
            this._label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label5.AutoSize = true;
            this._label5.Location = new System.Drawing.Point(3, 81);
            this._label5.Name = "_label5";
            this._label5.Size = new System.Drawing.Size(93, 27);
            this._label5.TabIndex = 5;
            this._label5.Text = "Maximum font size";
            this._label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _upDownMaxFontSize
            // 
            this._upDownMaxFontSize.Location = new System.Drawing.Point(123, 84);
            this._upDownMaxFontSize.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this._upDownMaxFontSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._upDownMaxFontSize.Name = "_upDownMaxFontSize";
            this._upDownMaxFontSize.Size = new System.Drawing.Size(81, 20);
            this._upDownMaxFontSize.TabIndex = 6;
            this._upDownMaxFontSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // _checkBoxTrueTypeOnly
            // 
            this._checkBoxTrueTypeOnly.AutoSize = true;
            this.LayoutPanel.SetColumnSpan(this._checkBoxTrueTypeOnly, 2);
            this._checkBoxTrueTypeOnly.Location = new System.Drawing.Point(3, 111);
            this._checkBoxTrueTypeOnly.Name = "_checkBoxTrueTypeOnly";
            this._checkBoxTrueTypeOnly.Size = new System.Drawing.Size(127, 17);
            this._checkBoxTrueTypeOnly.TabIndex = 5;
            this._checkBoxTrueTypeOnly.Text = "Show True Type only";
            // 
            // _checkBoxVerticalFonts
            // 
            this._checkBoxVerticalFonts.AutoSize = true;
            this.LayoutPanel.SetColumnSpan(this._checkBoxVerticalFonts, 2);
            this._checkBoxVerticalFonts.Location = new System.Drawing.Point(3, 138);
            this._checkBoxVerticalFonts.Name = "_checkBoxVerticalFonts";
            this._checkBoxVerticalFonts.Size = new System.Drawing.Size(116, 17);
            this._checkBoxVerticalFonts.TabIndex = 6;
            this._checkBoxVerticalFonts.Text = "Show vertical fonts";

            LabelHeader.Text = "  Floatie FontControl Properties";
            LabelHeader.ImageIndex = 12;
        }

        protected override void InitComponentStep3()
        {
            this.LayoutPanel.Controls.Add(this._label3, 0, 2);
            this.LayoutPanel.Controls.Add(this._upDownMinFontSize, 1, 2);
            this.LayoutPanel.Controls.Add(this._label5, 0, 3);
            this.LayoutPanel.Controls.Add(this._upDownMaxFontSize, 1, 3);
            this.LayoutPanel.Controls.Add(this._checkBoxTrueTypeOnly, 0, 4);
            this.LayoutPanel.Controls.Add(this._checkBoxVerticalFonts, 0, 5);

            base.InitComponentStep3();
        }

        protected override void InitResume()
        {
            ((System.ComponentModel.ISupportInitialize)(this._upDownMinFontSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._upDownMaxFontSize)).EndInit();

            base.InitResume();
        }

        protected override void InitEvents()
        {
            base.InitEvents();
            UpDownMinFontSize.ValueChanged += EditMinFontSizeChange;
            UpDownMaxFontSize.ValueChanged += EditMaxFontSizeChange;
            CheckBoxTrueTypeOnly.Click += CheckBoxTrueTypeOnlyClick;
            CheckBoxVerticalFonts.Click += CheckBoxVerticalFontsClick;
        }

        protected override void InitTooltips(IContainer components)
        {
            base.InitTooltips(components);
            new ToolTip(components).SetToolTip(UpDownMinFontSize, "Minimum size of the font the user can select");
            new ToolTip(components).SetToolTip(UpDownMaxFontSize, "Maximum size of the font the user can select");
            new ToolTip(components).SetToolTip(CheckBoxTrueTypeOnly, "Whether only True Type fonts are shown in the font list");
            new ToolTip(components).SetToolTip(CheckBoxVerticalFonts, "Whether vertical fonts are shown in the font list");
        }

        protected void CheckBoxTrueTypeOnlyClick(object sender, EventArgs e)
        {
            if (CheckBoxTrueTypeOnly.Checked != _floatControl.ShowTrueTypeOnly)
            {
                _floatControl.ShowTrueTypeOnly = CheckBoxTrueTypeOnly.Checked;
                Modified();
            }
        }

        protected void CheckBoxVerticalFontsClick(object sender, EventArgs e)
        {
            if (CheckBoxVerticalFonts.Checked != _floatControl.ShowVerticalFonts)
            {
                _floatControl.ShowVerticalFonts = CheckBoxVerticalFonts.Checked;
                Modified();
            }
        }

        protected void EditMaxFontSizeChange(object sender, EventArgs e)
        {
            if (UpDownMaxFontSize.Value != _floatControl.MaximumFontSize)
            {
                _floatControl.MaximumFontSize = (int)UpDownMaxFontSize.Value;
                Modified();
            }
        }

        protected void EditMinFontSizeChange(object sender, EventArgs e)
        {
            if (UpDownMinFontSize.Value != _floatControl.MinimumFontSize)
            {
                _floatControl.MinimumFontSize = (int)UpDownMinFontSize.Value;
                Modified();
            }
        }

        protected override void Initialize(TRibbonObject subject)
        {
            base.Initialize(subject);
            _floatControl = subject as TRibbonFloatieFontControl;
            UpDownMinFontSize.Value = _floatControl.MinimumFontSize;
            UpDownMaxFontSize.Value = _floatControl.MaximumFontSize;
            CheckBoxTrueTypeOnly.Checked = _floatControl.ShowTrueTypeOnly;
            CheckBoxVerticalFonts.Checked = _floatControl.ShowVerticalFonts;
        }
    }
}
