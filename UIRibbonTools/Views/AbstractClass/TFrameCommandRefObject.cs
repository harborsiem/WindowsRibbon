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
    partial class TFrameCommandRefObject : BaseFrame, IActivate
    {
        private TRibbonCommandRefObject _commandRefSubject;
        protected Label Label1 { get => _label1; }
        protected ComboBox ComboBoxCommand { get => _comboBoxCommand; }

        public TFrameCommandRefObject()
        {
            bool designtime = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
            if (designtime)
            {
                InitializeComponent();
                InitializeBaseComponent();
            }
        }

        protected override void InitComponentStep1()
        {
            if (components == null)
                components = new Container();
            this._label1 = new System.Windows.Forms.Label();
            this._comboBoxCommand = new System.Windows.Forms.ComboBox();
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
            this._label1.Location = new System.Drawing.Point(3, 5);
            this._label1.Name = "_label1";
            this._label1.Size = new System.Drawing.Size(54, 13);
            this._label1.TabIndex = 0;
            this._label1.Text = "Command";
            this._label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _comboBoxCommand
            // 
            this._comboBoxCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this._comboBoxCommand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxCommand.FormattingEnabled = true;
            this._comboBoxCommand.Location = new System.Drawing.Point(103, 3);
            this._comboBoxCommand.Name = "_comboBoxCommand";
            this._comboBoxCommand.Size = new System.Drawing.Size(250, 21);
            this._comboBoxCommand.TabIndex = 1;
            LabelHeader.Text = "  Properties";
        }

        protected override void InitComponentStep3()
        {
            LayoutPanel.Controls.Add(_label1, 0, 0);
            LayoutPanel.Controls.Add(_comboBoxCommand, 1, 0);
            LayoutPanel.SetColumnSpan(_comboBoxCommand, 3);
            base.InitComponentStep3();
        }

        protected override void InitResume()
        {
            base.InitResume();
        }

        protected override void InitEvents()
        {
            base.InitEvents();
            ComboBoxCommand.SelectedIndexChanged += ComboBoxCommandChange;
        }

        protected override void InitTooltips(IContainer components)
        {
            base.InitTooltips(components);
            new ToolTip(components).SetToolTip(ComboBoxCommand, "The command associated with this control.");
        }

        private void InitializeBaseComponent()
        {
            this._label1 = new System.Windows.Forms.Label();
            this._comboBoxCommand = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // _label1
            // 
            this._label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label1.AutoSize = true;
            this._label1.Location = new System.Drawing.Point(3, 0);
            this._label1.Name = "_label1";
            this._label1.Size = new System.Drawing.Size(54, 13);
            this._label1.TabIndex = 0;
            this._label1.Text = "Command";
            this._label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _comboBoxCommand
            // 
            this._comboBoxCommand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxCommand.MaxDropDownItems = 50;
            this._comboBoxCommand.FormattingEnabled = true;
            this._comboBoxCommand.Location = new System.Drawing.Point(83, 3);
            this._comboBoxCommand.Name = "_comboBoxCommand";
            this._comboBoxCommand.Size = new System.Drawing.Size(250, 21);
            this._comboBoxCommand.TabIndex = 1;

            LabelHeader.Text = "  Properties";
            LayoutPanel.SuspendLayout();
            LayoutPanel.Controls.Add(_label1, 0, 0);
            LayoutPanel.Controls.Add(_comboBoxCommand, 1, 0);
            LayoutPanel.SetColumnSpan(_comboBoxCommand, 3);
            LayoutPanel.ResumeLayout();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        protected TRibbonCommandRefObject CommandRefSubject { get { return _commandRefSubject; } }

        public virtual void Activate_()
        {
            ViewsFrame frameViews;
            string currentCmd;

            frameViews = Owner as ViewsFrame;
            currentCmd = ComboBoxCommand.Text;
            ComboBoxCommand.Items.AddRange(frameViews.Commands.ToArray());
            if (string.IsNullOrEmpty(currentCmd))
                ComboBoxCommand.SelectedIndex = 0;
            else
            {
                ComboBoxCommand.SelectedIndex = RibbonCommandItem.IndexOf(ComboBoxCommand, currentCmd);
                // ComboBoxCommand.Items.IndexOf(currentCmd);
                if (ComboBoxCommand.SelectedIndex < 0)
                    ComboBoxCommand.SelectedIndex = 0;
            }
        }

        private void ComboBoxCommandChange(object sender, EventArgs e)
        {
            TRibbonCommand newRef;

            if (_commandRefSubject != null)
            {
                if (ComboBoxCommand.SelectedIndex < 0)
                    newRef = null;
                else
                {
                    newRef = RibbonCommandItem.Selected(ComboBoxCommand);
                    // ComboBoxCommand.Items[ComboBoxCommand.SelectedIndex] as TRibbonCommand;
                }
                if (newRef != _commandRefSubject.CommandRef)
                {
                    _commandRefSubject.CommandRef = newRef;
                    Modified();
                }
            }
        }

        protected override void Initialize(TRibbonObject subject)
        {
            base.Initialize(subject);
            if (subject is TRibbonCommandRefObject)
            {
                _commandRefSubject = subject as TRibbonCommandRefObject;
                if (_commandRefSubject.CommandRef == null)
                    ComboBoxCommand.SelectedIndex = 0;
                else
                    ComboBoxCommand.SelectedIndex = RibbonCommandItem.IndexOf(ComboBoxCommand, _commandRefSubject.CommandRef);
                //ComboBoxCommand.Items.IndexOf(FCommandRefSubject.CommandRef);
            }
        }
    }
}
