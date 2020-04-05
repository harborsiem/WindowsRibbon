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
    partial class TFrameColumnBreak : BaseFrame
    {
        private CheckBox CheckBoxShowSeparator { get => _checkBoxShowSeparator; }

        private TRibbonColumnBreak _columnBreak;

        public TFrameColumnBreak()
        {
            bool designtime = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
            if (designtime)
                InitializeComponent();
        }

        protected override void InitComponentStep1()
        {
            if (components == null)
                components = new Container();
            this._checkBoxShowSeparator = new System.Windows.Forms.CheckBox();
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
            // _checkBoxShowSeparator
            // 
            this._checkBoxShowSeparator.AutoSize = true;
            this._checkBoxShowSeparator.Location = new System.Drawing.Point(3, 3);
            this._checkBoxShowSeparator.Name = "_checkBoxShowSeparator";
            this._checkBoxShowSeparator.Size = new System.Drawing.Size(100, 17);
            this._checkBoxShowSeparator.TabIndex = 0;
            this._checkBoxShowSeparator.Text = "Show separator";
            this._checkBoxShowSeparator.UseVisualStyleBackColor = true;

            LabelHeader.Text = "  Column Break Properties";
            LabelHeader.ImageIndex = 31;
        }

        protected override void InitComponentStep3()
        {
            this.LayoutPanel.Controls.Add(this._checkBoxShowSeparator, 0, 0);

            base.InitComponentStep3();
        }

        protected override void InitResume()
        {

            base.InitResume();
        }

        protected override void InitEvents()
        {
            base.InitEvents();
            CheckBoxShowSeparator.Click += CheckBoxShowSeparatorClick;
        }

        protected override void InitTooltips(IContainer components)
        {
            base.InitTooltips(components);
        }

        private void CheckBoxShowSeparatorClick(object sender, EventArgs e)
        {
            if (_columnBreak.ShowSeparator != CheckBoxShowSeparator.Checked)
            {
                _columnBreak.ShowSeparator = CheckBoxShowSeparator.Checked;
                Modified();
            }
        }

        protected override void Initialize(TRibbonObject subject)
        {
            base.Initialize(subject);
            _columnBreak = subject as TRibbonColumnBreak;
            CheckBoxShowSeparator.Checked = _columnBreak.ShowSeparator;
        }
    }
}
