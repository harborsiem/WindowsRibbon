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
    partial class TFrameControl : TFrameCommandRefObject
    {
        const string RS_ALL = "(all)";

        private ImageList _imageList;

        protected Label LabelApplicationModes { get => _labelApplicationModes; }
        protected TextBox EditApplicationModes { get => _editApplicationModes; } //: TButtonedEdit;
        protected Button RightButton { get => _rightButton; }

        private TRibbonControl _control;

        public TFrameControl()
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
            _imageList = ImageManager.ImageListAppModes_Shared(components);
            this._labelApplicationModes = new System.Windows.Forms.Label();
            this._editApplicationModes = new System.Windows.Forms.TextBox();
            this._rightButton = new Button();
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
            // _labelApplicationModes
            // 
            this._labelApplicationModes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._labelApplicationModes.AutoSize = true;
            this._labelApplicationModes.Location = new System.Drawing.Point(3, 0);
            this._labelApplicationModes.Margin = new System.Windows.Forms.Padding(3);
            this._labelApplicationModes.Name = "_labelApplicationModes";
            this._labelApplicationModes.Size = new System.Drawing.Size(91, 13);
            this._labelApplicationModes.TabIndex = 2;
            this._labelApplicationModes.Text = "Application Modes";
            this._labelApplicationModes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _editApplicationModes
            // 
            this._editApplicationModes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this._editApplicationModes.Location = new System.Drawing.Point(103, 0);
            this._editApplicationModes.Margin = new Padding(3, 3, 0, 3);
            this._editApplicationModes.Name = "_editApplicationModes";
            this._editApplicationModes.ReadOnly = true;
            this._editApplicationModes.Size = new System.Drawing.Size(226, 20);
            this._editApplicationModes.TabIndex = 3;
            // 
            // _rightButton
            // 
            this._rightButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._rightButton.ImageIndex = 0;
            this._rightButton.ImageList = _imageList;
            this._rightButton.Location = new System.Drawing.Point(229, 0);
            this._rightButton.Margin = new Padding(0, 3, 3, 3);
            this._rightButton.Name = "_rightButton";
            this._rightButton.Size = new System.Drawing.Size(20, 20);
            this._rightButton.TabIndex = 4;

            LabelHeader.Text = "  Control Properties";
        }

        protected override void InitComponentStep3()
        {
            LayoutPanel.Controls.Add(_labelApplicationModes, 0, 1);
            LayoutPanel.Controls.Add(_editApplicationModes, 1, 1);
            LayoutPanel.Controls.Add(_rightButton, 3, 1);
            LayoutPanel.SetColumnSpan(_editApplicationModes, 2);
            base.InitComponentStep3();
        }

        protected override void InitResume()
        {
            base.InitResume();
        }

        protected override void InitEvents()
        {
            base.InitEvents();
            RightButton.Click += EditApplicationModesRightButtonClick;
            RightButton.MouseEnter += RightButton_MouseEnter;
            RightButton.MouseLeave += RightButton_MouseLeave;
            RightButton.EnabledChanged += RightButton_EnabledChanged;
        }

        protected override void InitTooltips(IContainer components)
        {
            base.InitTooltips(components);
            new ToolTip(components).SetToolTip(EditApplicationModes, "The application modes in which this control is available.");
        }

        private void InitializeBaseComponent()
        {
            this._labelApplicationModes = new System.Windows.Forms.Label();
            this._editApplicationModes = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _labelApplicationModes
            // 
            this._labelApplicationModes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._labelApplicationModes.AutoSize = true;
            this._labelApplicationModes.Location = new System.Drawing.Point(344, 159);
            this._labelApplicationModes.Margin = new System.Windows.Forms.Padding(3);
            this._labelApplicationModes.Name = "_labelApplicationModes";
            this._labelApplicationModes.Size = new System.Drawing.Size(91, 13);
            this._labelApplicationModes.TabIndex = 2;
            this._labelApplicationModes.Text = "Application Modes";
            this._labelApplicationModes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _editApplicationModes
            // 
            this._editApplicationModes.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            this._editApplicationModes.Location = new System.Drawing.Point(347, 188);
            this._editApplicationModes.Name = "_editApplicationModes";
            this._editApplicationModes.ReadOnly = true;
            this._editApplicationModes.Size = new System.Drawing.Size(166, 20);
            this._editApplicationModes.TabIndex = 3;

            LabelHeader.Text = "  Properties";
            LayoutPanel.SuspendLayout();
            LayoutPanel.Controls.Add(_labelApplicationModes, 0, 1);
            LayoutPanel.Controls.Add(_editApplicationModes, 1, 1);
            LayoutPanel.SetColumnSpan(_editApplicationModes, 2);
            LayoutPanel.ResumeLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void RightButton_EnabledChanged(object sender, EventArgs e)
        {
            if (RightButton.Enabled)
                RightButton.ImageIndex = 0;
            else
                RightButton.ImageIndex = 2;
        }

        private void RightButton_MouseLeave(object sender, EventArgs e)
        {
            RightButton.ImageIndex = 0;
        }

        private void RightButton_MouseEnter(object sender, EventArgs e)
        {
            RightButton.ImageIndex = 1;
        }

        protected TRibbonControl Control { get { return _control; } }

        private string ApplicationModesToString(uint appModes)
        {
            string result;
            if (appModes == 0xFFFFFFFF)
                result = RS_ALL;
            else
            {
                result = string.Empty;
                for (int i = 0; i < 32; i++)
                {
                    if ((appModes & (1 << i)) != 0)
                    {
                        if (!string.IsNullOrEmpty(result))
                            result = result + ",";
                        result = result + (i.ToString());
                    }
                }
            }
            return result;
        }

        private void EditApplicationModesRightButtonClick(object sender, EventArgs e)
        {
            ApplicationModesForm form;
            form = new ApplicationModesForm(_control.ApplicationModes);
            //form.AppModes = FControl.ApplicationModes;
            try
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (form.AppModes != _control.ApplicationModes)
                    {
                        _control.ApplicationModes = form.AppModes;
                        EditApplicationModes.Text = ApplicationModesToString(_control.ApplicationModes);
                        Modified();
                    }
                }
            }
            finally
            {
                //form.Close();
            }
        }

        protected override void Initialize(TRibbonObject subject)
        {
            base.Initialize(subject);
            _control = subject as TRibbonControl;
            //RightButton.Click += EditApplicationModesRightButtonClick;
            LabelApplicationModes.Enabled = _control.SupportApplicationModes();
            EditApplicationModes.Enabled = _control.SupportApplicationModes();
            RightButton.Enabled = _control.SupportApplicationModes();
            if (EditApplicationModes.Enabled)
                EditApplicationModes.Text = ApplicationModesToString(_control.ApplicationModes);
            else
                EditApplicationModes.Text = string.Empty;
        }
    }
}
