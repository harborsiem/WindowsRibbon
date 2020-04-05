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
    partial class TFrameControlGroup : BaseFrame
    {
        private Label Label3 { get => _label3; }
        private NumericUpDown UpDownSequence { get => _upDownSequence; }
        private Label Label1 { get => _label1; }

        private TRibbonControlGroup _group;

        public TFrameControlGroup()
        {
            bool designtime = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
            if (designtime)
                InitializeComponent();
        }

        protected override void InitComponentStep1()
        {
            if (components == null)
                components = new Container();
            this._label3 = new System.Windows.Forms.Label();
            this._upDownSequence = new System.Windows.Forms.NumericUpDown();
            this._label1 = new System.Windows.Forms.Label();

            base.InitComponentStep1();
        }

        protected override void InitSuspend()
        {
            ((System.ComponentModel.ISupportInitialize)(this._upDownSequence)).BeginInit();

            base.InitSuspend();
        }

        protected override void InitComponentStep2()
        {
            base.InitComponentStep2();
            // 
            // _label3
            // 
            this._label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label3.AutoSize = true;
            this._label3.Location = new System.Drawing.Point(3, 0);
            this._label3.Name = "_label3";
            this._label3.Size = new System.Drawing.Size(94, 27);
            this._label3.TabIndex = 0;
            this._label3.Text = "Sequence number";
            this._label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _upDownSequence
            // 
            this._upDownSequence.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this._upDownSequence.Location = new System.Drawing.Point(123, 3);
            this._upDownSequence.Maximum = new decimal(new int[] {
            59999,
            0,
            0,
            0});
            this._upDownSequence.Name = "_upDownSequence";
            this._upDownSequence.Size = new System.Drawing.Size(81, 20);
            this._upDownSequence.TabIndex = 1;
            this._upDownSequence.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // _label1
            // 
            this._label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label1.AutoSize = true;
            this.LayoutPanel.SetColumnSpan(this._label1, 2);
            this._label1.Location = new System.Drawing.Point(210, 0);
            this._label1.Name = "_label1";
            this._label1.Size = new System.Drawing.Size(83, 27);
            this._label1.TabIndex = 0;
            this._label1.Text = "(0 for automatic)";
            this._label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            LabelHeader.Text = "  Control Group Properties";
            LabelHeader.ImageIndex = 14;
        }

        protected override void InitComponentStep3()
        {
            this.LayoutPanel.Controls.Add(this._label3, 0, 0);
            this.LayoutPanel.Controls.Add(this._upDownSequence, 1, 0);
            this.LayoutPanel.Controls.Add(this._label1, 2, 0);

            base.InitComponentStep3();
        }

        protected override void InitResume()
        {
            ((System.ComponentModel.ISupportInitialize)(this._upDownSequence)).EndInit();

            base.InitResume();
        }

        protected override void InitEvents()
        {
            base.InitEvents();
            UpDownSequence.ValueChanged += EditSequenceChange;
        }

        protected override void InitTooltips(IContainer components)
        {
            base.InitTooltips(components);
            //new ToolTip(components).SetToolTip(UpDownSequence, "???Name of the context menu");
        }

        private void EditSequenceChange(object sender, EventArgs e)
        {
            if (UpDownSequence.Value != _group.SequenceNumber)
            {
                _group.SequenceNumber = (int)UpDownSequence.Value;
                Modified();
            }
        }

        protected override void Initialize(TRibbonObject subject)
        {
            base.Initialize(subject);
            _group = subject as TRibbonControlGroup;
            UpDownSequence.Value = _group.SequenceNumber;
        }
    }
}
