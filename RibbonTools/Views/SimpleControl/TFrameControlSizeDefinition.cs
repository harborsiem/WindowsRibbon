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
    partial class TFrameControlSizeDefinition : BaseFrame
    {
        private Label Label1 { get => _label1; }
        private ComboBox ComboBoxControlName { get => _comboBoxControlName; }
        private Label Label2 { get => _label2; }
        private ComboBox ComboBoxImageSize { get => _comboBoxImageSize; }
        private CheckBox CheckBoxIsLabelVisible { get => _checkBoxIsLabelVisible; }
        private CheckBox CheckBoxIsImageVisible { get => _checkBoxIsImageVisible; }
        private CheckBox CheckBoxIsPopup { get => _checkBoxIsPopup; }

        private TRibbonControlSizeDefinition _sizeDef;

        public TFrameControlSizeDefinition()
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
            this._comboBoxControlName = new System.Windows.Forms.ComboBox();
            this._label2 = new System.Windows.Forms.Label();
            this._comboBoxImageSize = new System.Windows.Forms.ComboBox();
            this._checkBoxIsLabelVisible = new System.Windows.Forms.CheckBox();
            this._checkBoxIsImageVisible = new System.Windows.Forms.CheckBox();
            this._checkBoxIsPopup = new System.Windows.Forms.CheckBox();
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
            this._label1.Size = new System.Drawing.Size(71, 27);
            this._label1.TabIndex = 0;
            this._label1.Text = "Control Name";
            this._label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _comboBoxControlName
            // 
            this._comboBoxControlName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LayoutPanel.SetColumnSpan(this._comboBoxControlName, 3);
            this._comboBoxControlName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxControlName.Location = new System.Drawing.Point(123, 3);
            this._comboBoxControlName.MaxDropDownItems = 30;
            this._comboBoxControlName.Name = "_comboBoxControlName";
            this._comboBoxControlName.Size = new System.Drawing.Size(250, 21);
            this._comboBoxControlName.TabIndex = 1;
            // 
            // _label2
            // 
            this._label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label2.AutoSize = true;
            this._label2.Location = new System.Drawing.Point(3, 27);
            this._label2.Name = "_label2";
            this._label2.Size = new System.Drawing.Size(59, 27);
            this._label2.TabIndex = 0;
            this._label2.Text = "Image Size";
            this._label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _comboBoxImageSize
            // 
            this._comboBoxImageSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LayoutPanel.SetColumnSpan(this._comboBoxImageSize, 3);
            this._comboBoxImageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxImageSize.Items.AddRange(new object[] {
            "Large",
            "Small"});
            this._comboBoxImageSize.Location = new System.Drawing.Point(123, 30);
            this._comboBoxImageSize.MaxDropDownItems = 30;
            this._comboBoxImageSize.Name = "_comboBoxImageSize";
            this._comboBoxImageSize.Size = new System.Drawing.Size(250, 21);
            this._comboBoxImageSize.TabIndex = 2;
            // 
            // _checkBoxIsLabelVisible
            // 
            this._checkBoxIsLabelVisible.AutoSize = true;
            this._checkBoxIsLabelVisible.Location = new System.Drawing.Point(3, 57);
            this._checkBoxIsLabelVisible.Name = "_checkBoxIsLabelVisible";
            this._checkBoxIsLabelVisible.Size = new System.Drawing.Size(84, 17);
            this._checkBoxIsLabelVisible.TabIndex = 3;
            this._checkBoxIsLabelVisible.Text = "Label visible";
            this._checkBoxIsLabelVisible.UseVisualStyleBackColor = true;
            // 
            // _checkBoxIsImageVisible
            // 
            this._checkBoxIsImageVisible.AutoSize = true;
            this._checkBoxIsImageVisible.Location = new System.Drawing.Point(3, 84);
            this._checkBoxIsImageVisible.Name = "_checkBoxIsImageVisible";
            this._checkBoxIsImageVisible.Size = new System.Drawing.Size(87, 17);
            this._checkBoxIsImageVisible.TabIndex = 4;
            this._checkBoxIsImageVisible.Text = "Image visible";
            this._checkBoxIsImageVisible.UseVisualStyleBackColor = true;
            // 
            // _checkBoxIsPopup
            // 
            this._checkBoxIsPopup.AutoSize = true;
            this._checkBoxIsPopup.Location = new System.Drawing.Point(3, 111);
            this._checkBoxIsPopup.Name = "_checkBoxIsPopup";
            this._checkBoxIsPopup.Size = new System.Drawing.Size(68, 17);
            this._checkBoxIsPopup.TabIndex = 5;
            this._checkBoxIsPopup.Text = "Is Popup";
            this._checkBoxIsPopup.UseVisualStyleBackColor = true;

            LabelHeader.Text = "  ControlSizeDefinition Properties";
            LabelHeader.ImageIndex = 31;
        }

        protected override void InitComponentStep3()
        {
            this.LayoutPanel.Controls.Add(this._label1, 0, 0);
            this.LayoutPanel.Controls.Add(this._comboBoxControlName, 1, 0);
            this.LayoutPanel.Controls.Add(this._label2, 0, 1);
            this.LayoutPanel.Controls.Add(this._comboBoxImageSize, 1, 1);
            this.LayoutPanel.Controls.Add(this._checkBoxIsLabelVisible, 0, 2);
            this.LayoutPanel.Controls.Add(this._checkBoxIsImageVisible, 0, 3);
            this.LayoutPanel.Controls.Add(this._checkBoxIsPopup, 0, 4);

            base.InitComponentStep3();
        }

        protected override void InitResume()
        {

            base.InitResume();
        }

        protected override void InitEvents()
        {
            base.InitEvents();
            ComboBoxControlName.SelectedIndexChanged += ComboBoxControlNameChange;
            ComboBoxImageSize.SelectedIndexChanged += ComboBoxImageSizeChange;
            CheckBoxIsLabelVisible.Click += CheckBoxIsLabelVisibleClick;
            CheckBoxIsImageVisible.Click += CheckBoxIsImageVisibleClick;
            CheckBoxIsPopup.Click += CheckBoxIsPopupClick;
        }

        protected override void InitTooltips(IContainer components)
        {
            base.InitTooltips(components);
            viewsTip.SetToolTip(ComboBoxControlName, "The name of the control to which this size definition applies.");
            viewsTip.SetToolTip(ComboBoxImageSize, "Size of the images for this definition");
            viewsTip.SetToolTip(CheckBoxIsLabelVisible, "Whether the control label is visible in this definition");
            viewsTip.SetToolTip(CheckBoxIsImageVisible, "Whether the control image is visible in this definition");
        }

        private void CheckBoxIsImageVisibleClick(
            object sender, EventArgs e)
        {
            if (CheckBoxIsImageVisible.Checked != _sizeDef.IsImageVisible)
            {
                _sizeDef.IsImageVisible = CheckBoxIsImageVisible.Checked;
                Modified();
            }
        }

        private void CheckBoxIsLabelVisibleClick(
            object sender, EventArgs e)
        {
            if (CheckBoxIsLabelVisible.Checked != _sizeDef.IsLabelVisible)
            {
                _sizeDef.IsLabelVisible = CheckBoxIsLabelVisible.Checked;
                Modified();
            }
        }

        private void CheckBoxIsPopupClick(object sender, EventArgs e)
        {
            if (CheckBoxIsPopup.Checked != _sizeDef.IsPopup)
            {
                _sizeDef.IsPopup = CheckBoxIsPopup.Checked;
                Modified();
            }
        }

        private void ComboBoxControlNameChange(object sender, EventArgs e)
        {
            string controlName;

            controlName = ComboBoxControlName.Text;
            if (controlName != _sizeDef.ControlName)
            {
                _sizeDef.ControlName = controlName;
                Modified();
            }
        }

        private void ComboBoxImageSizeChange(object sender, EventArgs e)
        {
            if (ComboBoxImageSize.SelectedIndex != (int)(_sizeDef.ImageSize))
            {
                _sizeDef.ImageSize = (RibbonImageSize)(ComboBoxImageSize.SelectedIndex);
                Modified();
            }
        }

        protected override void Initialize(TRibbonObject subject)
        {
            base.Initialize(subject);
            _sizeDef = subject as TRibbonControlSizeDefinition;
            ComboBoxControlName.Items.Clear();
            foreach (string controlName in _sizeDef.OwnerDefinition.ControlNameMap.ControlNameDefinitions)
                ComboBoxControlName.Items.Add(controlName);
            if (_sizeDef.ControlName != null)
                ComboBoxControlName.SelectedIndex = ComboBoxControlName.Items.IndexOf(_sizeDef.ControlName);
            else
                ComboBoxControlName.SelectedIndex = -1;
            ComboBoxImageSize.SelectedIndex = (int)(_sizeDef.ImageSize);
            CheckBoxIsLabelVisible.Checked = _sizeDef.IsLabelVisible;
            CheckBoxIsImageVisible.Checked = _sizeDef.IsImageVisible;
            CheckBoxIsPopup.Checked = _sizeDef.IsPopup;
        }
    }
}
