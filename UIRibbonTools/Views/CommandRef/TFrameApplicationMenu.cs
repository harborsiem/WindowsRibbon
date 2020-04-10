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
    partial class TFrameApplicationMenu : TFrameCommandRefObject
    {
        //private GroupBox GroupBoxRecentItems;
        private CheckBox CheckBoxEnableRecentItems { get => _checkBoxEnableRecentItems; }
        private Label LabelMaxCount { get => _labelMaxCount; }
        private NumericUpDown UpDownMaxCount { get => _upDownMaxCount; }
        private CheckBox CheckBoxEnablePinning { get => _checkBoxEnablePinning; }
        private Label LabelCaptionCommand { get => _labelCaptionCommand; }
        private ComboBox ComboBoxCaptionCommand { get => _comboBoxCaptionCommand; }

        private TRibbonApplicationMenu _appMenu;

        public TFrameApplicationMenu()
        {
            bool designtime = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
            if (designtime)
                InitializeComponent();
        }

        protected override void InitComponentStep1()
        {
            if (components == null)
                components = new Container();
            this.groupLayout = new System.Windows.Forms.TableLayoutPanel();
            this._checkBoxEnableRecentItems = new System.Windows.Forms.CheckBox();
            this._labelCaptionCommand = new System.Windows.Forms.Label();
            this._comboBoxCaptionCommand = new System.Windows.Forms.ComboBox();
            this._labelMaxCount = new System.Windows.Forms.Label();
            this._upDownMaxCount = new System.Windows.Forms.NumericUpDown();
            this._checkBoxEnablePinning = new System.Windows.Forms.CheckBox();
            this._groupBoxRecentItems = new System.Windows.Forms.GroupBox();
            base.InitComponentStep1();
        }

        protected override void InitSuspend()
        {
            this.groupLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._upDownMaxCount)).BeginInit();
            this._groupBoxRecentItems.SuspendLayout();

            base.InitSuspend();
        }

        protected override void InitComponentStep2()
        {
            base.InitComponentStep2();

            // 
            // groupLayout
            // 
            this.groupLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupLayout.ColumnCount = 3;
            this.groupLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 114F));
            this.groupLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this.groupLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, ThirdColumnWidth + 18F));
            this.groupLayout.Controls.Add(this._checkBoxEnableRecentItems, 0, 0);
            this.groupLayout.Controls.Add(this._labelCaptionCommand, 0, 1);
            this.groupLayout.Controls.Add(this._comboBoxCaptionCommand, 1, 1);
            this.groupLayout.Controls.Add(this._labelMaxCount, 0, 2);
            this.groupLayout.Controls.Add(this._upDownMaxCount, 1, 2);
            this.groupLayout.Controls.Add(this._checkBoxEnablePinning, 0, 3);
            this.groupLayout.Location = new System.Drawing.Point(3, 18);
            this.groupLayout.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.groupLayout.Name = "groupLayout";
            this.groupLayout.RowCount = 4;
            this.groupLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.groupLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.groupLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.groupLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.groupLayout.Size = new System.Drawing.Size(364, 99);
            this.groupLayout.TabIndex = 0;
            // 
            // _checkBoxEnableRecentItems
            // 
            this._checkBoxEnableRecentItems.AutoSize = true;
            this.groupLayout.SetColumnSpan(this._checkBoxEnableRecentItems, 2);
            this._checkBoxEnableRecentItems.Location = new System.Drawing.Point(3, 3);
            this._checkBoxEnableRecentItems.Name = "_checkBoxEnableRecentItems";
            this._checkBoxEnableRecentItems.Size = new System.Drawing.Size(125, 17);
            this._checkBoxEnableRecentItems.TabIndex = 1;
            this._checkBoxEnableRecentItems.Text = "Enable Recent Items";
            // 
            // _labelCaptionCommand
            // 
            this._labelCaptionCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._labelCaptionCommand.AutoSize = true;
            this._labelCaptionCommand.Location = new System.Drawing.Point(3, 23);
            this._labelCaptionCommand.Name = "_labelCaptionCommand";
            this._labelCaptionCommand.Size = new System.Drawing.Size(93, 27);
            this._labelCaptionCommand.TabIndex = 0;
            this._labelCaptionCommand.Text = "Caption Command";
            this._labelCaptionCommand.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _comboBoxCaptionCommand
            // 
            this._comboBoxCaptionCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupLayout.SetColumnSpan(this._comboBoxCaptionCommand, 2);
            this._comboBoxCaptionCommand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxCaptionCommand.Location = new System.Drawing.Point(117, 26);
            this._comboBoxCaptionCommand.MaxDropDownItems = 30;
            this._comboBoxCaptionCommand.Name = "_comboBoxCaptionCommand";
            this._comboBoxCaptionCommand.Size = new System.Drawing.Size(244, 21);
            this._comboBoxCaptionCommand.TabIndex = 2;
            // 
            // _labelMaxCount
            // 
            this._labelMaxCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._labelMaxCount.Location = new System.Drawing.Point(3, 50);
            this._labelMaxCount.Name = "_labelMaxCount";
            this._labelMaxCount.Size = new System.Drawing.Size(100, 26);
            this._labelMaxCount.TabIndex = 0;
            this._labelMaxCount.Text = "Max Count";
            this._labelMaxCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _upDownMaxCount
            // 
            this._upDownMaxCount.Location = new System.Drawing.Point(117, 53);
            this._upDownMaxCount.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this._upDownMaxCount.Name = "_upDownMaxCount";
            this._upDownMaxCount.Size = new System.Drawing.Size(81, 20);
            this._upDownMaxCount.TabIndex = 3;
            this._upDownMaxCount.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // _checkBoxEnablePinning
            // 
            this._checkBoxEnablePinning.AutoSize = true;
            this.groupLayout.SetColumnSpan(this._checkBoxEnablePinning, 2);
            this._checkBoxEnablePinning.Location = new System.Drawing.Point(3, 79);
            this._checkBoxEnablePinning.Name = "_checkBoxEnablePinning";
            this._checkBoxEnablePinning.Size = new System.Drawing.Size(97, 17);
            this._checkBoxEnablePinning.TabIndex = 4;
            this._checkBoxEnablePinning.Text = "Enable Pinning";
            // 
            // _groupBoxRecentItems
            // 
            this._groupBoxRecentItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LayoutPanel.SetColumnSpan(this._groupBoxRecentItems, 4);
            this._groupBoxRecentItems.Controls.Add(this.groupLayout);
            this._groupBoxRecentItems.Location = new System.Drawing.Point(3, 30);
            this._groupBoxRecentItems.Name = "_groupBoxRecentItems";
            this._groupBoxRecentItems.Size = new System.Drawing.Size(370, 133);
            this._groupBoxRecentItems.TabIndex = 2;
            this._groupBoxRecentItems.TabStop = false;

            LabelHeader.Text = "  Application Menu Properties";
            LabelHeader.ImageIndex = 18;
        }

        protected override void InitComponentStep3()
        {
            this.LayoutPanel.Controls.Add(this._groupBoxRecentItems, 0, 1);

            base.InitComponentStep3();
        }

        protected override void InitResume()
        {
            this.groupLayout.ResumeLayout(false);
            this.groupLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._upDownMaxCount)).EndInit();
            this._groupBoxRecentItems.ResumeLayout(false);

            base.InitResume();
        }

        protected override void InitEvents()
        {
            base.InitEvents();
            UpDownMaxCount.ValueChanged += EditMaxCountChange;
            CheckBoxEnablePinning.Click += CheckBoxEnablePinningClick;
            ComboBoxCaptionCommand.SelectedIndexChanged += ComboBoxCaptionCommandChange;
            CheckBoxEnableRecentItems.Click += CheckBoxEnableRecentItemsClick;
        }

        protected override void InitTooltips(IContainer components)
        {
            base.InitTooltips(components);
            new ToolTip(components).SetToolTip(UpDownMaxCount, "Maximum number of recent items in the application menu");
            new ToolTip(components).SetToolTip(CheckBoxEnablePinning, "Whether recent items can be pinned to the application menu.");
            new ToolTip(components).SetToolTip(ComboBoxCaptionCommand,
                "The command that contains the caption display" + Environment.NewLine +
                "at the top of the recent items list.");
            new ToolTip(components).SetToolTip(CheckBoxEnableRecentItems, "Whether the application menu supports recent items");
        }

        public override void Activate_()
        {
            ViewsFrame frameViews;
            string currentCmd;

            base.Activate_();
            frameViews = Owner as ViewsFrame;
            currentCmd = ComboBoxCaptionCommand.Text;
            ComboBoxCaptionCommand.Items.AddRange(frameViews.Commands.ToArray());
            if (string.IsNullOrEmpty(currentCmd))
                ComboBoxCaptionCommand.SelectedIndex = 0;
            else
            {
                ComboBoxCaptionCommand.SelectedIndex = RibbonCommandItem.IndexOf(ComboBoxCaptionCommand, currentCmd);
                //ComboBoxCaptionCommand.Items.IndexOf(currentCmd);
                if (ComboBoxCaptionCommand.SelectedIndex < 0)
                    ComboBoxCaptionCommand.SelectedIndex = 0;
            }
        }

        private void CheckBoxEnablePinningClick(object sender, EventArgs e)
        {
            if ((_appMenu.RecentItems != null) && (CheckBoxEnablePinning.Checked != _appMenu.RecentItems.EnablePinning))
            {
                _appMenu.RecentItems.EnablePinning = CheckBoxEnablePinning.Checked;
                Modified();
            }
        }

        private void CheckBoxEnableRecentItemsClick(object sender, EventArgs e)
        {
            _appMenu.EnableRecentItems(CheckBoxEnableRecentItems.Checked);
            UpdateControls();
            Modified();
        }

        private void ComboBoxCaptionCommandChange(object sender, EventArgs e)
        {
            TRibbonCommand newRef;

            if (_appMenu == null) //@ added
                return;
            if (_appMenu.RecentItems != null)
            {
                if (ComboBoxCaptionCommand.SelectedIndex < 0)
                    newRef = null;
                else
                {
                    newRef = RibbonCommandItem.Selected(ComboBoxCaptionCommand);
                    // ComboBoxCaptionCommand.Items[ComboBoxCaptionCommand.SelectedIndex] as TRibbonCommand;
                }
                if (newRef != _appMenu.RecentItems.CommandRef)
                {
                    _appMenu.RecentItems.CommandRef = newRef;
                    Modified();
                }
            }
        }

        private void EditMaxCountChange(object sender, EventArgs e)
        {
            if ((_appMenu.RecentItems != null) && (UpDownMaxCount.Value != _appMenu.RecentItems.MaxCount))
            {
                _appMenu.RecentItems.MaxCount = (int)UpDownMaxCount.Value;
                Modified();
            }
        }

        protected override void Initialize(TRibbonObject subject)
        {
            base.Initialize(subject);
            _appMenu = subject as TRibbonApplicationMenu;
            CheckBoxEnableRecentItems.Checked = (_appMenu.RecentItems != null);
            UpdateControls();
        }

        private void UpdateControls()
        {
            LabelCaptionCommand.Enabled = (_appMenu.RecentItems != null);
            ComboBoxCaptionCommand.Enabled = (_appMenu.RecentItems != null);
            LabelMaxCount.Enabled = (_appMenu.RecentItems != null);
            UpDownMaxCount.Enabled = (_appMenu.RecentItems != null);
            CheckBoxEnablePinning.Enabled = (_appMenu.RecentItems != null);

            if (_appMenu.RecentItems != null)
            {
                if (_appMenu.RecentItems.CommandRef == null)
                    ComboBoxCaptionCommand.SelectedIndex = 0;
                else
                    ComboBoxCaptionCommand.SelectedIndex = RibbonCommandItem.IndexOf(ComboBoxCaptionCommand, _appMenu.RecentItems.CommandRef);
                //ComboBoxCaptionCommand.Items.IndexOf(FAppMenu.RecentItems.CommandRef);
                UpDownMaxCount.Value = _appMenu.RecentItems.MaxCount;
                CheckBoxEnablePinning.Checked = _appMenu.RecentItems.EnablePinning;
            }
            else
            {
                ComboBoxCaptionCommand.SelectedIndex = -1;
                UpDownMaxCount.Value = 0;
                CheckBoxEnablePinning.Checked = false;
            }
        }
    }
}
