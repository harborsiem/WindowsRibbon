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
    partial class TFrameGroup : TFrameControl
    {
        private Label Label2 { get => _label2; }
        private ComboBox ComboBoxSizeDefinition { get => _comboBoxSizeDefinition; }
        private Label LabelCustomSizeDefinition { get => _labelCustomSizeDefinition; }
        private ComboBox ComboBoxCustomSizeDefinition { get => _comboBoxCustomSizeDefinition; }

        private TRibbonGroup _group;

        public TFrameGroup()
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
            this._comboBoxSizeDefinition = new System.Windows.Forms.ComboBox();
            this._labelCustomSizeDefinition = new System.Windows.Forms.Label();
            this._comboBoxCustomSizeDefinition = new System.Windows.Forms.ComboBox();

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
            this._label2.Size = new System.Drawing.Size(74, 27);
            this._label2.TabIndex = 0;
            this._label2.Text = "Size Definition";
            this._label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _comboBoxSizeDefinition
            // 
            this._comboBoxSizeDefinition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LayoutPanel.SetColumnSpan(this._comboBoxSizeDefinition, 3);
            this._comboBoxSizeDefinition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxSizeDefinition.Items.AddRange(new object[] {
            "Custom",
            "Advanced (see Tree on the left)",
            "1 Large Button",
            "2 Large Buttons",
            "3 Buttons",
            "3 Buttons, 1 Big and 2 Small",
            "3 Buttons and 1 Check Box",
            "4 Buttons",
            "5 Buttons",
            "5 or 6 Buttons",
            "6 Buttons",
            "6 Buttons in 2 Columns",
            "7 Buttons",
            "8 Buttons",
            "8 Buttons, last 3 Small",
            "9 Buttons",
            "10 Buttons",
            "11 Button",
            "1 Font Control",
            "Int Font Only",
            "Int Rich Font",
            "Int Font With Color",
            "1 In-Ribbon Gallery",
            "Big Buttons and Small Buttons or Inputs",
            "In-Ribbon Gallery and Big Button",
            "In-Ribbon Gallery and Buttons, Gallery scales first",
            "In-Ribbon Gallery and 3 Buttons",
            "Button Groups and Inputs",
            "Button Groups"});
            this._comboBoxSizeDefinition.Location = new System.Drawing.Point(123, 57);
            this._comboBoxSizeDefinition.MaxDropDownItems = 30;
            this._comboBoxSizeDefinition.Name = "_comboBoxSizeDefinition";
            this._comboBoxSizeDefinition.Size = new System.Drawing.Size(250, 21);
            this._comboBoxSizeDefinition.TabIndex = 5;
            // 
            // _labelCustomSizeDefinition
            // 
            this._labelCustomSizeDefinition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._labelCustomSizeDefinition.AutoSize = true;
            this._labelCustomSizeDefinition.Location = new System.Drawing.Point(3, 81);
            this._labelCustomSizeDefinition.Name = "_labelCustomSizeDefinition";
            this._labelCustomSizeDefinition.Size = new System.Drawing.Size(112, 27);
            this._labelCustomSizeDefinition.TabIndex = 0;
            this._labelCustomSizeDefinition.Text = "Custom Size Definition";
            this._labelCustomSizeDefinition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _comboBoxCustomSizeDefinition
            // 
            this._comboBoxCustomSizeDefinition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LayoutPanel.SetColumnSpan(this._comboBoxCustomSizeDefinition, 3);
            this._comboBoxCustomSizeDefinition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxCustomSizeDefinition.Location = new System.Drawing.Point(123, 84);
            this._comboBoxCustomSizeDefinition.MaxDropDownItems = 30;
            this._comboBoxCustomSizeDefinition.Name = "_comboBoxCustomSizeDefinition";
            this._comboBoxCustomSizeDefinition.Size = new System.Drawing.Size(250, 21);
            this._comboBoxCustomSizeDefinition.TabIndex = 6;

            LabelHeader.Text = "  Group Properties";
            LabelHeader.ImageIndex = 20;
        }

        protected override void InitComponentStep3()
        {
            this.LayoutPanel.Controls.Add(this._label2, 0, 2);
            this.LayoutPanel.Controls.Add(this._comboBoxSizeDefinition, 1, 2);
            this.LayoutPanel.Controls.Add(this._labelCustomSizeDefinition, 0, 3);
            this.LayoutPanel.Controls.Add(this._comboBoxCustomSizeDefinition, 1, 3);
            base.InitComponentStep3();
        }

        protected override void InitResume()
        {

            base.InitResume();
        }

        protected override void InitEvents()
        {
            base.InitEvents();
            ComboBoxSizeDefinition.SelectedIndexChanged += ComboBoxSizeDefinitionChange;
            ComboBoxCustomSizeDefinition.SelectedIndexChanged += ComboBoxCustomSizeDefinitionChange;
        }

        protected override void InitTooltips(IContainer components)
        {
            base.InitTooltips(components);
            viewsTip.SetToolTip(ComboBoxSizeDefinition, "Specifies how the controls in this group are layed out and scaled");
            viewsTip.SetToolTip(ComboBoxCustomSizeDefinition,
                "Specify a custom size definition that you have declared" + Environment.NewLine +
                "under the \"Size Definitions\" node of the ribbon");
        }

        protected void ComboBoxCustomSizeDefinitionChange(object sender, EventArgs e)
        {
            if (ComboBoxCustomSizeDefinition.SelectedIndex >= 0)
            {
                _group.CustomSizeDefinition = ComboBoxCustomSizeDefinition.Text;
                Modified();
            }
        }

        private void ComboBoxSizeDefinitionChange(object sender, EventArgs e)
        {
            if (ComboBoxSizeDefinition.SelectedIndex != (int)(_group.BasicSizeDefinition))
            {
                _group.BasicSizeDefinition = (RibbonBasicSizeDefinition)(ComboBoxSizeDefinition.SelectedIndex);
                UpdateControls();
                Modified();
            }
        }

        protected override void Initialize(TRibbonObject subject)
        {
            TRibbonViewRibbon ribbon;
            base.Initialize(subject);
            _group = subject as TRibbonGroup;
            ComboBoxSizeDefinition.SelectedIndex = (int)(_group.BasicSizeDefinition);

            ComboBoxCustomSizeDefinition.Items.Clear();
            ribbon = _group.Owner.Application.Ribbon;
            if (ribbon != null)
                foreach (TRibbonRibbonSizeDefinition sizeDef in ribbon.SizeDefinitions)
                    ComboBoxCustomSizeDefinition.Items.Add(new RibbonSizeDef(sizeDef.DisplayName(), sizeDef));
            //ComboBoxCustomSizeDefinition.Items.Add(sizeDef);
            ComboBoxCustomSizeDefinition.SelectedIndex =
              RibbonSizeDef.IndexOf(ComboBoxCustomSizeDefinition, _group.CustomSizeDefinition);
            //ComboBoxCustomSizeDefinition.Items.IndexOf(FGroup.CustomSizeDefinition);

            UpdateControls();
        }

        private void UpdateControls()
        {
            ViewsFrame FrameViews;

            void RemoveSizeDefNode() //DeleteSizeDefNode
            {
                for (int i = SubjectNode.Nodes.Count; i >= 0; i--)
                    if (SubjectNode.Nodes[i].ImageIndex == ViewsFrame.II_SIZE_DEF)
                    {
                        SubjectNode.Nodes[i].Remove();
                        break;
                    }
            }

            LabelCustomSizeDefinition.Enabled = (_group.BasicSizeDefinition == RibbonBasicSizeDefinition.Custom)
              && (ComboBoxCustomSizeDefinition.Items.Count > 0);
            ComboBoxCustomSizeDefinition.Enabled = LabelCustomSizeDefinition.Enabled;
            if (!ComboBoxCustomSizeDefinition.Enabled)
                ComboBoxCustomSizeDefinition.SelectedIndex = -1;

            FrameViews = Owner as ViewsFrame;
            if (_group.BasicSizeDefinition == RibbonBasicSizeDefinition.Advanced)
            {
                if (_group.SizeDefinition == null)
                {
                    _group.CreateAdvancedSizeDefinition();
                    RemoveSizeDefNode();
                    FrameViews.AddSizeDefinition(SubjectNode, _group.SizeDefinition);
                }
            }
            else
            {
                if (_group.SizeDefinition != null)
                {
                    _group.RemoveAdvancedSizeDefinition();
                    RemoveSizeDefNode();
                }
            }
        }
    }
}
