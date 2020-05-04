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
    partial class TFrameViewRibbon : BaseFrame
    {
        private static Image sample = ImageManager.ViewRibbonSample();

        private Label Label1 { get => _label1; }
        private TextBox EditName { get => _editName; }
        private Label Label2 { get => _label2; }
        private ComboBox ComboBoxGroupSpacing { get => _comboBoxGroupSpacing; }

        private TRibbonViewRibbon _ribbon;

        public TFrameViewRibbon()
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
            this._label2 = new System.Windows.Forms.Label();
            this._comboBoxGroupSpacing = new System.Windows.Forms.ComboBox();
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
            // 
            // _label2
            // 
            this._label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label2.AutoSize = true;
            this._label2.Location = new System.Drawing.Point(3, 27);
            this._label2.Name = "_label2";
            this._label2.Size = new System.Drawing.Size(78, 27);
            this._label2.TabIndex = 0;
            this._label2.Text = "Group Spacing";
            this._label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _comboBoxGroupSpacing
            // 
            this._comboBoxGroupSpacing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LayoutPanel.SetColumnSpan(this._comboBoxGroupSpacing, 3);
            this._comboBoxGroupSpacing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxGroupSpacing.Items.AddRange(new object[] {
            "Small",
            "Medium",
            "Large"});
            this._comboBoxGroupSpacing.Location = new System.Drawing.Point(123, 30);
            this._comboBoxGroupSpacing.MaxDropDownItems = 30;
            this._comboBoxGroupSpacing.Name = "_comboBoxGroupSpacing";
            this._comboBoxGroupSpacing.Size = new System.Drawing.Size(250, 21);
            this._comboBoxGroupSpacing.TabIndex = 2;

            //EditName.ReadOnly = true;
            ComboBoxGroupSpacing.SelectedIndex = 0;

            LabelHeader.Text = "  Ribbon Properties";
            LabelHeader.ImageIndex = 23;
        }

        protected override void InitComponentStep3()
        {
            this.LayoutPanel.Controls.Add(this._label1, 0, 0);
            this.LayoutPanel.Controls.Add(this._editName, 1, 0);
            this.LayoutPanel.Controls.Add(this._label2, 0, 1);
            this.LayoutPanel.Controls.Add(this._comboBoxGroupSpacing, 1, 1);

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
            ComboBoxGroupSpacing.SelectedIndexChanged += ComboBoxGroupSpacingChange;
        }

        protected override void InitTooltips(IContainer components)
        {
            base.InitTooltips(components);
            viewsTip.SetToolTip(EditName, "Name of the ribbon.");
            viewsTip.SetToolTip(ComboBoxGroupSpacing, "Spacing of Groups");
        }

        private void ComboBoxGroupSpacingChange(object sender, EventArgs e)
        {
            if (ComboBoxGroupSpacing.SelectedIndex != (int)(_ribbon.GroupSpacing))
            {
                _ribbon.GroupSpacing = (RibbonGroupSpacing)(ComboBoxGroupSpacing.SelectedIndex);
                Modified();
            }
        }

        private void EditNameChange(object sender, EventArgs e)
        {
            if (EditName.Text != _ribbon.Name)
            {
                _ribbon.Name = EditName.Text;
                Modified();
            }
        }

        protected override void Initialize(TRibbonObject subject)
        {
            base.Initialize(subject);
            _ribbon = subject as TRibbonViewRibbon;
            EditName.Text = _ribbon.Name;
            ComboBoxGroupSpacing.SelectedIndex = (int)(_ribbon.GroupSpacing);
        }

        protected override Image SetImageSample()
        {
            return sample;
        }
    }
}
