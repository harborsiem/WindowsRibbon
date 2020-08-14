using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace UIRibbonTools
{
    [DesignTimeVisible(false)]
    partial class TFrameSplitButton : TFrameControl
    {
        private static Image sample = ImageManager.SplitButtonSample();

        private Label Label2 { get => _label2; }
        private ComboBox ComboBoxButtonItemType { get => _comboBoxButtonItemType; }

        private TRibbonSplitButton _button;

        // Button Item type
        const int BI_NONE = 0;
        const int BI_BUTTON = 1;
        const int BI_TOGGLE_BUTTON = 2;

        public TFrameSplitButton()
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
            this._comboBoxButtonItemType = new System.Windows.Forms.ComboBox();

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
            this._label2.Size = new System.Drawing.Size(89, 27);
            this._label2.TabIndex = 0;
            this._label2.Text = "Button (Top) Item";
            this._label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _comboBoxButtonItemType
            // 
            this._comboBoxButtonItemType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LayoutPanel.SetColumnSpan(this._comboBoxButtonItemType, 3);
            this._comboBoxButtonItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxButtonItemType.Items.AddRange(new object[] {
            "None",
            "Button",
            "Toggle Button"});
            this._comboBoxButtonItemType.Location = new System.Drawing.Point(123, 57);
            this._comboBoxButtonItemType.MaxDropDownItems = 30;
            this._comboBoxButtonItemType.Name = "_comboBoxButtonItemType";
            this._comboBoxButtonItemType.Size = new System.Drawing.Size(250, 21);
            this._comboBoxButtonItemType.TabIndex = 5;

            LabelHeader.Text = "  Split Button Properties";
            LabelHeader.ImageIndex = 2;
        }

        protected override void InitComponentStep3()
        {
            this.LayoutPanel.Controls.Add(this._label2, 0, 2);
            this.LayoutPanel.Controls.Add(this._comboBoxButtonItemType, 1, 2);

            base.InitComponentStep3();
        }

        protected override void InitResume()
        {

            base.InitResume();
        }

        protected override void InitEvents()
        {
            base.InitEvents();
            ComboBoxButtonItemType.SelectedIndexChanged += ComboBoxButtonItemTypeChange;
        }

        protected override void InitTooltips(IContainer components)
        {
            base.InitTooltips(components);
            viewsTip.SetToolTip(ComboBoxButtonItemType,
                "The type of item to use for the button (top) part of the control" + Environment.NewLine +
                " (see tree on the left for details)");
        }

        private void ComboBoxButtonItemTypeChange(object sender, EventArgs e)
        {
            TreeNode itemNode;
            ViewsFrame frameViews;
            if (IsInInitialize)
                return;
            Debug.Assert(SubjectNode.Nodes.Count > 0);
            itemNode = SubjectNode.Nodes[0];
            frameViews = Owner as ViewsFrame;
            switch (ComboBoxButtonItemType.SelectedIndex)
            {
                case BI_NONE:
                    {
                        if (_button.ButtonItem != null)
                        {
                            _button.RemoveButtonItem();
                            RemoveChildren(itemNode);
                            Modified();
                        }
                    }
                    break;

                case BI_BUTTON:
                    {
                        if ((_button.ButtonItem == null) || (_button.ButtonItem.ObjectType() == RibbonObjectType.ToggleButton))
                        {
                            _button.CreateButtonItem();
                            RemoveChildren(itemNode);
                            frameViews.AddControl(itemNode, _button.ButtonItem);
                            SubjectNode.TreeView.SelectedNode = SubjectNode;
                            Modified();
                        }
                    }
                    break;
                case BI_TOGGLE_BUTTON:
                    {
                        if ((_button.ButtonItem == null) || (_button.ButtonItem.ObjectType() == RibbonObjectType.Button))
                        {
                            _button.CreateToggleButtonItem();
                            RemoveChildren(itemNode);
                            frameViews.AddControl(itemNode, _button.ButtonItem);
                            SubjectNode.TreeView.SelectedNode = SubjectNode;
                            Modified();
                        }
                    }
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        private void RemoveChildren(TreeNode node) //DeleteChildren
        {
            if (node.Nodes.Count > 0)
                node.Nodes[0].Remove();
        }

        protected override void Initialize(TRibbonObject subject)
        {
            base.Initialize(subject);
            _button = subject as TRibbonSplitButton;
            if (_button.ButtonItem == null)
                ComboBoxButtonItemType.SelectedIndex = BI_NONE;
            else if (_button.ButtonItem.ObjectType() == RibbonObjectType.Button)
                ComboBoxButtonItemType.SelectedIndex = BI_BUTTON;
            else
                ComboBoxButtonItemType.SelectedIndex = BI_TOGGLE_BUTTON;
        }

        protected override Image SetImageSample()
        {
            return sample;
        }
    }
}
