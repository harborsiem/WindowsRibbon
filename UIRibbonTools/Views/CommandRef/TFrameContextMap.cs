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
    partial class TFrameContextMap : TFrameCommandRefObject
    {
        private Label Label2 { get => _label2; }
        private ComboBox ComboBoxMiniToolbar { get => _comboBoxMiniToolbar; }
        private Label Label3 { get => _label3; }
        private ComboBox ComboBoxContextMenu { get => _comboBoxContextMenu; }

        private TRibbonContextMap _contextMap;

        //resourcestring
        const string RS_NONE = "(none)";

        public TFrameContextMap()
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
            this._comboBoxMiniToolbar = new System.Windows.Forms.ComboBox();
            this._label3 = new System.Windows.Forms.Label();
            this._comboBoxContextMenu = new System.Windows.Forms.ComboBox();

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
            this._label2.Location = new System.Drawing.Point(3, 54);
            this._label2.Name = "_label2";
            this._label2.Size = new System.Drawing.Size(65, 27);
            this._label2.TabIndex = 0;
            this._label2.Text = "Mini Toolbar";
            this._label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _comboBoxMiniToolbar
            // 
            this._comboBoxMiniToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LayoutPanel.SetColumnSpan(this._comboBoxMiniToolbar, 3);
            this._comboBoxMiniToolbar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxMiniToolbar.Location = new System.Drawing.Point(123, 57);
            this._comboBoxMiniToolbar.MaxDropDownItems = 30;
            this._comboBoxMiniToolbar.Name = "_comboBoxMiniToolbar";
            this._comboBoxMiniToolbar.Size = new System.Drawing.Size(250, 21);
            this._comboBoxMiniToolbar.TabIndex = 5;
            // 
            // _label3
            // 
            this._label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label3.AutoSize = true;
            this._label3.Location = new System.Drawing.Point(3, 81);
            this._label3.Name = "_label3";
            this._label3.Size = new System.Drawing.Size(73, 27);
            this._label3.TabIndex = 0;
            this._label3.Text = "Context Menu";
            this._label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _comboBoxContextMenu
            // 
            this._comboBoxContextMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LayoutPanel.SetColumnSpan(this._comboBoxContextMenu, 3);
            this._comboBoxContextMenu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxContextMenu.Location = new System.Drawing.Point(123, 84);
            this._comboBoxContextMenu.MaxDropDownItems = 30;
            this._comboBoxContextMenu.Name = "_comboBoxContextMenu";
            this._comboBoxContextMenu.Size = new System.Drawing.Size(250, 21);
            this._comboBoxContextMenu.TabIndex = 6;

            LabelHeader.Text = "  Context Map Properties";
            LabelHeader.ImageIndex = 35;
        }

        protected override void InitComponentStep3()
        {
            this.LayoutPanel.Controls.Add(this._label2, 0, 2);
            this.LayoutPanel.Controls.Add(this._comboBoxMiniToolbar, 1, 2);
            this.LayoutPanel.Controls.Add(this._label3, 0, 3);
            this.LayoutPanel.Controls.Add(this._comboBoxContextMenu, 1, 3);

            base.InitComponentStep3();
        }

        protected override void InitResume()
        {

            base.InitResume();
        }

        protected override void InitEvents()
        {
            base.InitEvents();
            ComboBoxMiniToolbar.SelectedIndexChanged += ComboBoxMiniToolbarChange;
            ComboBoxContextMenu.SelectedIndexChanged += ComboBoxContextMenuChange;
        }

        protected override void InitTooltips(IContainer components)
        {
            base.InitTooltips(components);
            viewsTip.SetToolTip(ComboBoxMiniToolbar, "The mini toolbar to use in this mapping");
            viewsTip.SetToolTip(ComboBoxContextMenu, "The context menu to use in this mapping");
        }

        private void ComboBoxContextMenuChange(object sender, EventArgs e)
        {
            TRibbonContextMenu newRef;
            if (ComboBoxContextMenu.SelectedIndex <= 0)
                newRef = null;
            else
                newRef = _contextMap.Owner.FindContextMenu(ComboBoxContextMenu.Text);
            if (newRef != _contextMap.ContextMenuRef)
            {
                _contextMap.ContextMenuRef = newRef;
                Modified();
            }
        }

        private void ComboBoxMiniToolbarChange(object sender, EventArgs e)
        {
            TRibbonMiniToolbar newRef;

            if (ComboBoxMiniToolbar.SelectedIndex <= 0)
                newRef = null;
            else
                newRef = _contextMap.Owner.FindMiniToolbar(ComboBoxMiniToolbar.Text);
            if (newRef != _contextMap.MiniToolbarRef)
            {
                _contextMap.MiniToolbarRef = newRef;
                Modified();
            }
        }

        protected override void Initialize(TRibbonObject subject)
        {
            base.Initialize(subject);
            _contextMap = subject as TRibbonContextMap;

            ComboBoxMiniToolbar.BeginUpdate();
            try
            {
                ComboBoxMiniToolbar.Items.Clear();
                ComboBoxMiniToolbar.Items.Add(RS_NONE);
                foreach (TRibbonMiniToolbar toolbar in _contextMap.ContextPopup.MiniToolbars)
                    if (!string.IsNullOrEmpty(toolbar.Name))
                        ComboBoxMiniToolbar.Items.Add(toolbar.Name);
            }
            finally
            {
                ComboBoxMiniToolbar.EndUpdate();
            }
            if (_contextMap.MiniToolbarRef != null)
                ComboBoxMiniToolbar.SelectedIndex = ComboBoxMiniToolbar.Items.IndexOf(_contextMap.MiniToolbarRef.Name);
            if (ComboBoxMiniToolbar.SelectedIndex < 0)
                ComboBoxMiniToolbar.SelectedIndex = 0;

            ComboBoxContextMenu.BeginUpdate();
            try
            {
                ComboBoxContextMenu.Items.Clear();
                ComboBoxContextMenu.Items.Add(RS_NONE);
                foreach (TRibbonContextMenu Menu in _contextMap.ContextPopup.ContextMenus)
                    if (!string.IsNullOrEmpty(Menu.Name))
                        ComboBoxContextMenu.Items.Add(Menu.Name);
            }
            finally
            {
                ComboBoxContextMenu.EndUpdate();
            }
            if (_contextMap.ContextMenuRef != null)
                ComboBoxContextMenu.SelectedIndex = ComboBoxContextMenu.Items.IndexOf(_contextMap.ContextMenuRef.Name);
            if (ComboBoxContextMenu.SelectedIndex < 0)
                ComboBoxContextMenu.SelectedIndex = 0;
        }
    }
}
