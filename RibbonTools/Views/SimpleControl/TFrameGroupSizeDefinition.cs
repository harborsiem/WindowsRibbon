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
    partial class TFrameGroupSizeDefinition : BaseFrame
    {
        private Label Label1 { get => _label1; }
        private ComboBox ComboBoxSize { get => _comboBoxSize; }

        private TRibbonGroupSizeDefinition _sizeDef;

        public TFrameGroupSizeDefinition()
        {
            bool designtime = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
            if (designtime)
                InitializeComponent();
        }

        protected override void InitComponentStep1()
        {
            if (components == null)
                components = new Container();
            this._label1 = new System.Windows.Forms.Label();
            this._comboBoxSize = new System.Windows.Forms.ComboBox();
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
            // _label1
            // 
            this._label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label1.AutoSize = true;
            this._label1.Location = new System.Drawing.Point(3, 0);
            this._label1.Name = "_label1";
            this._label1.Size = new System.Drawing.Size(27, 27);
            this._label1.TabIndex = 0;
            this._label1.Text = "Size";
            this._label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _comboBoxSize
            // 
            this._comboBoxSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LayoutPanel.SetColumnSpan(this._comboBoxSize, 3);
            this._comboBoxSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxSize.Items.AddRange(new object[] {
            "Large",
            "Medium",
            "Small"});
            this._comboBoxSize.Location = new System.Drawing.Point(123, 3);
            this._comboBoxSize.MaxDropDownItems = 30;
            this._comboBoxSize.Name = "_comboBoxSize";
            this._comboBoxSize.Size = new System.Drawing.Size(250, 21);
            this._comboBoxSize.TabIndex = 1;

            LabelHeader.Text = "  Group SizeDefinition Properties";
            LabelHeader.ImageIndex = 30;
        }

        protected override void InitComponentStep3()
        {
            this.LayoutPanel.Controls.Add(this._label1, 0, 0);
            this.LayoutPanel.Controls.Add(this._comboBoxSize, 1, 0);

            base.InitComponentStep3();
        }

        protected override void InitResume()
        {

            base.InitResume();
        }

        protected override void InitEvents()
        {
            base.InitEvents();
            ComboBoxSize.SelectedIndexChanged += ComboBoxSizeChange;
        }

        protected override void InitTooltips(IContainer components)
        {
            base.InitTooltips(components);
            viewsTip.SetToolTip(ComboBoxSize, "The command associated with this control.");
        }

        private void ComboBoxSizeChange(object sender, EventArgs e)
        {
            if (ComboBoxSize.SelectedIndex != (int)(_sizeDef.Size))
            {
                _sizeDef.Size = (RibbonGroupSizeType)(ComboBoxSize.SelectedIndex);
                Modified();
            }
        }

        protected override void Initialize(TRibbonObject subject)
        {
            base.Initialize(subject);
            _sizeDef = subject as TRibbonGroupSizeDefinition;
            ComboBoxSize.SelectedIndex = (int)(_sizeDef.Size);
        }
    }
}
