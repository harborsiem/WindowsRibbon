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
    partial class TFrameComboBox : TFrameCommandRefObject
    {
        private CheckBox CheckBoxEditable { get => _checkBoxEditable; }
        private CheckBox CheckBoxResizeable { get => _checkBoxResizeable; }
        private CheckBox CheckBoxAutoComplete { get => _checkBoxAutoComplete; }

        private TRibbonComboBox _comboBox;

        public TFrameComboBox()
        {
            bool designtime = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
            if (designtime)
                InitializeComponent();
        }

        protected override void InitComponentStep1()
        {
            if (components == null)
                components = new Container();
            this._checkBoxEditable = new System.Windows.Forms.CheckBox();
            this._checkBoxResizeable = new System.Windows.Forms.CheckBox();
            this._checkBoxAutoComplete = new System.Windows.Forms.CheckBox();
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
            // _checkBoxEditable
            // 
            this._checkBoxEditable.AutoSize = true;
            this._checkBoxEditable.Location = new System.Drawing.Point(3, 30);
            this._checkBoxEditable.Name = "_checkBoxEditable";
            this._checkBoxEditable.Size = new System.Drawing.Size(64, 17);
            this._checkBoxEditable.TabIndex = 2;
            this._checkBoxEditable.Text = "Editable";
            // 
            // _checkBoxResizeable
            // 
            this._checkBoxResizeable.AutoSize = true;
            this._checkBoxResizeable.Location = new System.Drawing.Point(3, 57);
            this._checkBoxResizeable.Name = "_checkBoxResizeable";
            this._checkBoxResizeable.Size = new System.Drawing.Size(78, 17);
            this._checkBoxResizeable.TabIndex = 3;
            this._checkBoxResizeable.Text = "Resizeable";
            // 
            // _checkBoxAutoComplete
            // 
            this._checkBoxAutoComplete.AutoSize = true;
            this.LayoutPanel.SetColumnSpan(this._checkBoxAutoComplete, 2);
            this._checkBoxAutoComplete.Location = new System.Drawing.Point(3, 84);
            this._checkBoxAutoComplete.Name = "_checkBoxAutoComplete";
            this._checkBoxAutoComplete.Size = new System.Drawing.Size(135, 17);
            this._checkBoxAutoComplete.TabIndex = 4;
            this._checkBoxAutoComplete.Text = "Auto-complete enabled";

            LabelHeader.Text = "  ComboBox Properties";
            LabelHeader.ImageIndex = 7;
        }

        protected override void InitComponentStep3()
        {
            this.LayoutPanel.Controls.Add(this._checkBoxEditable, 0, 1);
            this.LayoutPanel.Controls.Add(this._checkBoxResizeable, 0, 2);
            this.LayoutPanel.Controls.Add(this._checkBoxAutoComplete, 0, 3);
            base.InitComponentStep3();
        }

        protected override void InitResume()
        {

            base.InitResume();
        }

        protected override void InitEvents()
        {
            base.InitEvents();
            CheckBoxEditable.Click += CheckBoxEditableClick;
            CheckBoxResizeable.Click += CheckBoxResizeableClick;
            CheckBoxAutoComplete.Click += CheckBoxAutoCompleteClick;
        }

        protected override void InitTooltips(IContainer components)
        {
            base.InitTooltips(components);
            new ToolTip(components).SetToolTip(CheckBoxEditable, "Whether the user can enter text in the combo box");
            new ToolTip(components).SetToolTip(CheckBoxResizeable, "Whether the user can resize the drop down list vertically");
            new ToolTip(components).SetToolTip(CheckBoxAutoComplete, "Whether the combo box supports auto-completion");
        }

        private void CheckBoxAutoCompleteClick(object sender, EventArgs e)
        {
            if (CheckBoxAutoComplete.Checked != _comboBox.IsAutoCompleteEnabled)
            {
                _comboBox.IsAutoCompleteEnabled = CheckBoxAutoComplete.Checked;
                Modified();
            }
        }

        private void CheckBoxEditableClick(object sender, EventArgs e)
        {
            if (CheckBoxEditable.Checked != _comboBox.IsEditable)
            {
                _comboBox.IsEditable = CheckBoxEditable.Checked;
                Modified();
            }
        }

        private void CheckBoxResizeableClick(object sender, EventArgs e)
        {
            if (CheckBoxResizeable.Checked != (_comboBox.ResizeType == RibbonComboBoxResizeType.VerticalResize))
            {
                if (CheckBoxResizeable.Checked)
                    _comboBox.ResizeType = RibbonComboBoxResizeType.VerticalResize;
                else
                    _comboBox.ResizeType = RibbonComboBoxResizeType.NoResize;
                Modified();
            }
        }

        protected override void Initialize(TRibbonObject subject)
        {
            base.Initialize(subject);
            _comboBox = subject as TRibbonComboBox;
            CheckBoxEditable.Checked = _comboBox.IsEditable;
            CheckBoxResizeable.Checked = (_comboBox.ResizeType == RibbonComboBoxResizeType.VerticalResize);
            CheckBoxAutoComplete.Checked = _comboBox.IsAutoCompleteEnabled;
        }
    }
}
