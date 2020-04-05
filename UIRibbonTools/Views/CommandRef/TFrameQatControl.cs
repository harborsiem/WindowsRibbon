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
    partial class TFrameQatControl : TFrameCommandRefObject
    {
        private CheckBox CheckBoxIsChecked { get => _checkBoxIsChecked; }

        private TRibbonQatControl _qatControl;

        public TFrameQatControl()
        {
            bool designtime = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
            if (designtime)
                InitializeComponent();
        }

        protected override void InitComponentStep1()
        {
            if (components == null)
                components = new Container();
            this._checkBoxIsChecked = new System.Windows.Forms.CheckBox();
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
            // _checkBoxIsChecked
            // 
            this._checkBoxIsChecked.AutoSize = true;
            this._checkBoxIsChecked.Location = new System.Drawing.Point(3, 30);
            this._checkBoxIsChecked.Name = "_checkBoxIsChecked";
            this._checkBoxIsChecked.Size = new System.Drawing.Size(80, 17);
            this._checkBoxIsChecked.TabIndex = 1;
            this._checkBoxIsChecked.Text = "Is Checked";

            LabelHeader.Text = "  Quick Access Toolbar Control Properties";
            //LabelHeader.ImageIndex = 0;
        }

        protected override void InitComponentStep3()
        {
            this.LayoutPanel.Controls.Add(this._checkBoxIsChecked, 0, 1);
            base.InitComponentStep3();
        }

        protected override void InitResume()
        {

            base.InitResume();
        }

        protected override void InitEvents()
        {
            base.InitEvents();
            CheckBoxIsChecked.Click += CheckBoxIsCheckedClick;
        }

        protected override void InitTooltips(IContainer components)
        {
            base.InitTooltips(components);
            new ToolTip(components).SetToolTip(CheckBoxIsChecked,
                "Whether this control is displayed by default" + Environment.NewLine +
                "on the Quick Access Toolbar");
        }

        private void CheckBoxIsCheckedClick(object sender, EventArgs e)
        {
            if (CheckBoxIsChecked.Checked != _qatControl.IsChecked)
            {
                _qatControl.IsChecked = CheckBoxIsChecked.Checked;
                Modified();
            }
        }

        protected override void Initialize(TRibbonObject subject)
        {
            base.Initialize(subject);
            _qatControl = subject as TRibbonQatControl;
            CheckBoxIsChecked.Checked = _qatControl.IsChecked;
        }
    }
}
