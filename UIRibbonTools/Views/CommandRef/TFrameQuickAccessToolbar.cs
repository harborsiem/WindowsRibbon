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
    partial class TFrameQuickAccessToolbar : TFrameCommandRefObject
    {
        private Label Label2 { get => _label2; }
        private ComboBox ComboBoxCustomizeCommand { get => _comboBoxCustomizeCommand; }

        private TRibbonQuickAccessToolbar _quickAccessToolbar;

        public TFrameQuickAccessToolbar()
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
            this._comboBoxCustomizeCommand = new System.Windows.Forms.ComboBox();
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
            this._label2.Size = new System.Drawing.Size(105, 27);
            this._label2.TabIndex = 0;
            this._label2.Text = "Customize Command";
            this._label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _comboBoxCustomizeCommand
            // 
            this._comboBoxCustomizeCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LayoutPanel.SetColumnSpan(this._comboBoxCustomizeCommand, 3);
            this._comboBoxCustomizeCommand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxCustomizeCommand.Location = new System.Drawing.Point(123, 30);
            this._comboBoxCustomizeCommand.MaxDropDownItems = 20;
            this._comboBoxCustomizeCommand.Name = "_comboBoxCustomizeCommand";
            this._comboBoxCustomizeCommand.Size = new System.Drawing.Size(250, 21);
            this._comboBoxCustomizeCommand.TabIndex = 2;

            LabelHeader.Text = "  Quick Access Toolbar Properties";
            LabelHeader.ImageIndex = 19;
        }

        protected override void InitComponentStep3()
        {
            this.LayoutPanel.Controls.Add(this._label2, 0, 1);
            this.LayoutPanel.Controls.Add(this._comboBoxCustomizeCommand, 1, 1);

            base.InitComponentStep3();
        }

        protected override void InitResume()
        {

            base.InitResume();
        }

        protected override void InitEvents()
        {
            base.InitEvents();
            ComboBoxCustomizeCommand.SelectedIndexChanged += ComboBoxCustomizeCommandChange;
        }

        protected override void InitTooltips(IContainer components)
        {
            base.InitTooltips(components);
            new ToolTip(components).SetToolTip(ComboBoxCustomizeCommand, "The command to use to customize the quick access toolbar");
        }

        public override void Activate_()
        {
            ViewsFrame frameViews;
            TRibbonCommand cmd;
            base.Activate_();
            frameViews = Owner as ViewsFrame;
            if (ComboBoxCustomizeCommand.SelectedIndex >= 0)
            {
                cmd = RibbonCommandItem.Selected(ComboBoxCustomizeCommand);
                // ComboBoxCustomizeCommand.Items[ComboBoxCustomizeCommand.SelectedIndex] as TRibbonCommand;
            }
            else
                cmd = null;
            ComboBoxCustomizeCommand.Items.AddRange(frameViews.Commands.ToArray());
            if (cmd == null)
                ComboBoxCustomizeCommand.SelectedIndex = 0;
            else
            {
                ComboBoxCustomizeCommand.SelectedIndex = RibbonCommandItem.IndexOf(ComboBoxCustomizeCommand, cmd);
                //ComboBoxCustomizeCommand.Items.IndexOf(cmd);
                if (ComboBoxCustomizeCommand.SelectedIndex < 0)
                    ComboBoxCustomizeCommand.SelectedIndex = 0;
            }
        }

        private void ComboBoxCustomizeCommandChange(
            object sender, EventArgs e)
        {
            TRibbonCommand newRef;
            if (_quickAccessToolbar == null) //@ added
                return;

            if (ComboBoxCustomizeCommand.SelectedIndex < 0)
                newRef = null;
            else
            {
                newRef = RibbonCommandItem.Selected(ComboBoxCustomizeCommand);
                // ComboBoxCustomizeCommand.Items[ComboBoxCustomizeCommand.SelectedIndex] as TRibbonCommand;
            }
            if (newRef != _quickAccessToolbar.CustomizeCommandRef)
            {
                _quickAccessToolbar.CustomizeCommandRef = newRef;
                Modified();
            }
        }

        protected override void Initialize(TRibbonObject subject)
        {
            base.Initialize(subject);
            _quickAccessToolbar = subject as TRibbonQuickAccessToolbar;
            if (_quickAccessToolbar.CustomizeCommandRef == null)
                ComboBoxCustomizeCommand.SelectedIndex = 0;
            else
            {
                ComboBoxCustomizeCommand.SelectedIndex = RibbonCommandItem.IndexOf(ComboBoxCustomizeCommand, _quickAccessToolbar.CustomizeCommandRef);
                // ComboBoxCustomizeCommand.Items.IndexOf(FQuickAccessToolbar.CustomizeCommandRef);
            }
        }
    }
}
