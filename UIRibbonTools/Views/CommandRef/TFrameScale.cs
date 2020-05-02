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
    partial class TFrameScale : TFrameCommandRefObject
    {
        private Label Label2 { get => _label2; }
        private ComboBox ComboBoxSize { get => _comboBoxSize; }

        private TRibbonScale _scale;

        public TFrameScale()
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
            // _label2
            // 
            this._label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label2.AutoSize = true;
            this._label2.Location = new System.Drawing.Point(3, 27);
            this._label2.Name = "_label2";
            this._label2.Size = new System.Drawing.Size(27, 27);
            this._label2.TabIndex = 0;
            this._label2.Text = "Size";
            this._label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _comboBoxSize
            // 
            this._comboBoxSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LayoutPanel.SetColumnSpan(this._comboBoxSize, 3);
            this._comboBoxSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxSize.Items.AddRange(new object[] {
            "Popup",
            "Small",
            "Medium",
            "Large"});
            this._comboBoxSize.Location = new System.Drawing.Point(123, 30);
            this._comboBoxSize.MaxDropDownItems = 20;
            this._comboBoxSize.Name = "_comboBoxSize";
            this._comboBoxSize.Size = new System.Drawing.Size(250, 21);
            this._comboBoxSize.TabIndex = 2;

            LabelHeader.Text = "  Scale Properties";
            LabelHeader.ImageIndex = 29;
        }

        protected override void InitComponentStep3()
        {
            this.LayoutPanel.Controls.Add(this._label2, 0, 1);
            this.LayoutPanel.Controls.Add(this._comboBoxSize, 1, 1);
            base.InitComponentStep3();
        }

        protected override void InitResume()
        {

            base.InitResume();
        }

        protected override void InitEvents()
        {
            //base.InitEvents(); // don't do it, because of override Command event
            ComboBoxSize.SelectedIndexChanged += ComboBoxSizeChange;
            ComboBoxCommand.SelectedIndexChanged += ComboBoxCommandChange;
        }

        protected override void InitTooltips(IContainer components)
        {
            //base.InitTooltips(components);
            viewsTip.SetToolTip(ComboBoxCommand,
                "The command associated with the group of controls" + Environment.NewLine +
                "to which this scale applies.");
        }

        private void ComboBoxCommandChange(object sender, EventArgs e)
        {
            TRibbonCommand newRef;

            if (_scale != null)
            {
                if (ComboBoxCommand.SelectedIndex < 0)
                    newRef = null;
                else
                {
                    newRef = RibbonCommandItem.Selected(ComboBoxCommand);
                    //ComboBoxCommand.Items[ComboBoxCommand.SelectedIndex] as TRibbonCommand;
                }
                if (newRef != _scale.GroupRef)
                {
                    _scale.GroupRef = newRef;
                    Modified();
                }
            }
        }

        private void ComboBoxSizeChange(object sender, EventArgs e)
        {
            if (ComboBoxSize.SelectedIndex != (int)(_scale.Size))
            {
                _scale.Size = (RibbonGroupLayout)(ComboBoxSize.SelectedIndex);
                Modified();
            }
        }

        protected override void Initialize(TRibbonObject subject)
        {
            base.Initialize(subject);
            //ComboBoxCommand.SelectedIndexChanged += ComboBoxCommandChange; //not here, but in InitEvents
            _scale = subject as TRibbonScale;
            if (_scale.GroupRef == null)
                ComboBoxCommand.SelectedIndex = 0;
            else
                ComboBoxCommand.SelectedIndex = RibbonCommandItem.IndexOf(ComboBoxCommand, _scale.GroupRef);
            //ComboBoxCommand.Items.IndexOf(_scale.GroupRef);
            ComboBoxSize.SelectedIndex = (int)(_scale.Size);
        }
    }
}
