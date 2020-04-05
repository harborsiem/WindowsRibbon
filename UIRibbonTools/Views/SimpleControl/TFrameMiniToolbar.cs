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
    partial class TFrameMiniToolbar : BaseFrame
    {
        private Label Label1 { get => _label1; }
        private TextBox EditName { get => _editName; }

        private TRibbonMiniToolbar _toolbar;

        public TFrameMiniToolbar()
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
            this._editName = new System.Windows.Forms.TextBox();
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
            this._label1.Size = new System.Drawing.Size(35, 27);
            this._label1.TabIndex = 0;
            this._label1.Text = "Name";
            this._label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _editName
            // 
            this._editName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LayoutPanel.SetColumnSpan(this._editName, 3);
            this._editName.Location = new System.Drawing.Point(123, 3);
            this._editName.Name = "_editName";
            this._editName.Size = new System.Drawing.Size(250, 20);
            this._editName.TabIndex = 1;

            LabelHeader.Text = "  Mini Toolbar Properties";
            LabelHeader.ImageIndex = 33;
        }

        protected override void InitComponentStep3()
        {
            this.LayoutPanel.Controls.Add(this._label1, 0, 0);
            this.LayoutPanel.Controls.Add(this._editName, 1, 0);

            base.InitComponentStep3();
        }

        protected override void InitResume()
        {

            base.InitResume();
        }

        protected override void InitEvents()
        {
            base.InitEvents();
            EditName.TextChanged += EditNameChange;
        }

        protected override void InitTooltips(IContainer components)
        {
            base.InitTooltips(components);
            new ToolTip(components).SetToolTip(EditName, "Name of the mini toolbar");
        }

        private void EditNameChange(object sender, EventArgs e)
        {
            if (EditName.Text != _toolbar.Name)
            {
                _toolbar.Name = EditName.Text;
                Modified();
            }
        }

        protected override void Initialize(TRibbonObject subject)
        {
            base.Initialize(subject);
            _toolbar = subject as TRibbonMiniToolbar;
            EditName.Text = _toolbar.Name;
        }
    }
}
