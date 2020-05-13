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
    partial class TFrameSizeDefinition : BaseFrame
    {
        private Label LabelName { get => _labelName; }
        private TextBox EditName { get => _editName; }
        private Label Label1 { get => _label1; }
        private TextBox MemoControlNameMap { get => _memoControlNameMap; }

        private TRibbonSizeDefinition _sizeDef;
        private bool _namesChanged;

        public TFrameSizeDefinition()
        {
            bool designtime = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
            if (designtime)
                InitializeComponent();
        }

        protected override void InitComponentStep1()
        {
            if (components == null)
                components = new Container();
            this._labelName = new System.Windows.Forms.Label();
            this._editName = new System.Windows.Forms.TextBox();
            this._label1 = new System.Windows.Forms.Label();
            this._memoControlNameMap = new System.Windows.Forms.TextBox();

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
            // _labelName
            // 
            this._labelName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._labelName.AutoSize = true;
            this._labelName.Location = new System.Drawing.Point(3, 0);
            this._labelName.Name = "_labelName";
            this._labelName.Size = new System.Drawing.Size(35, 27);
            this._labelName.TabIndex = 0;
            this._labelName.Text = "Name";
            this._labelName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            // 
            // _label1
            // 
            this._label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label1.AutoSize = true;
            this._label1.Location = new System.Drawing.Point(3, 27);
            this._label1.Name = "_label1";
            this._label1.Size = new System.Drawing.Size(95, 27);
            this._label1.TabIndex = 2;
            this._label1.Text = "Control Name Map";
            this._label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _memoControlNameMap
            // 
            this._memoControlNameMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LayoutPanel.SetColumnSpan(this._memoControlNameMap, 3);
            this._memoControlNameMap.Location = new System.Drawing.Point(123, 30);
            this._memoControlNameMap.Multiline = true;
            this._memoControlNameMap.Name = "_memoControlNameMap";
            this.LayoutPanel.SetRowSpan(this._memoControlNameMap, 3);
            this._memoControlNameMap.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._memoControlNameMap.Size = new System.Drawing.Size(250, 80);
            this._memoControlNameMap.TabIndex = 2;
            this._memoControlNameMap.WordWrap = false;

            LabelHeader.Text = "  SizeDefinition Properties";
            LabelHeader.ImageIndex = 25;
        }

        protected override void InitComponentStep3()
        {
            this.LayoutPanel.Controls.Add(this._labelName, 0, 0);
            this.LayoutPanel.Controls.Add(this._editName, 1, 0);
            this.LayoutPanel.Controls.Add(this._label1, 0, 1);
            this.LayoutPanel.Controls.Add(this._memoControlNameMap, 1, 1);

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
            EditName.KeyPress += EditNameKeyPress;
            MemoControlNameMap.TextChanged += MemoControlNameMapChange;
            MemoControlNameMap.Leave += MemoControlNameMapExit;
        }

        protected override void InitTooltips(IContainer components)
        {
            base.InitTooltips(components);
            viewsTip.SetToolTip(EditName,
                "Name of the size definition. This name is later used when" + Environment.NewLine +
                "you set custom size definitions for a group of controls.");
            viewsTip.SetToolTip(MemoControlNameMap,
                "Specify some (arbitrary) control names that you use later in" + Environment.NewLine +
                "the control size definitions. Use one control name per line.");
        }

        protected void EditNameChange(object sender, EventArgs e)
        {
            TRibbonRibbonSizeDefinition sizeDef;

            if (_sizeDef is TRibbonRibbonSizeDefinition)
            {
                sizeDef = (TRibbonRibbonSizeDefinition)(_sizeDef);
                if (EditName.Text != sizeDef.Name)
                {
                    sizeDef.Name = EditName.Text;
                    Modified();
                }
            }
        }

        private void EditNameKeyPress(object sender, KeyPressEventArgs e)
        {
            // Only allow valid Name/Symbol characters
            TextBox edit = sender as TextBox;
            if (e.KeyChar == (char)8 || e.KeyChar == '_' || char.IsLetter(e.KeyChar))
                return;
            if (char.IsDigit(e.KeyChar))
            {
                if (edit.SelectionStart == 0)
                    e.KeyChar = (char)0;
            }
            else
                e.KeyChar = (char)0;
            if (e.KeyChar == (char)0)
                e.Handled = true;
        }

        protected override void Initialize(TRibbonObject subject)
        {
            base.Initialize(subject);
            _sizeDef = subject as TRibbonSizeDefinition;
            LabelName.Enabled = (_sizeDef is TRibbonRibbonSizeDefinition);
            EditName.Enabled = LabelName.Enabled;
            if (EditName.Enabled)
                EditName.Text = ((TRibbonRibbonSizeDefinition)(_sizeDef)).Name;
            else
                EditName.Text = string.Empty;

            MemoControlNameMap.Clear();
            List<string> tmp = new List<string>(); //@ changed
            foreach (string s in _sizeDef.ControlNameMap.ControlNameDefinitions)
                tmp.Add(s);
            MemoControlNameMap.Lines = tmp.ToArray();

            _namesChanged = false;
        }

        protected void MemoControlNameMapChange(object sender, EventArgs e)
        {
            _namesChanged = true;
            Modified();
        }

        protected void MemoControlNameMapExit(object sender, EventArgs e)
        {
            string name;

            if (_namesChanged)
            {
                _sizeDef.ControlNameMap.Clear();
                foreach (string s in MemoControlNameMap.Lines)
                {
                    name = s.Trim();
                    if (!string.IsNullOrEmpty(name))
                        _sizeDef.ControlNameMap.Add(name);
                }
            }
        }
    }
}
