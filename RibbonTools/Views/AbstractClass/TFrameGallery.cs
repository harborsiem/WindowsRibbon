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
    partial class TFrameGallery : TFrameControl
    {
        protected Label Label2 { get => _label2; }
        protected ComboBox ComboBoxGalleryType { get => _comboBoxGalleryType; }
        protected CheckBox CheckBoxHasLargeItems { get => _checkBoxHasLargeItems; }
        protected Label Label3 { get => _label3; }
        protected NumericUpDown UpDownItemWidth { get => _upDownItemWidth; }
        protected Label Label4 { get => _label4; }
        protected Label Label5 { get => _label5; }
        protected NumericUpDown UpDownItemHeight { get => _upDownItemHeight; }
        protected Label Label6 { get => _label6; }
        protected Label Label7 { get => _label7; }
        protected ComboBox ComboBoxTextPosition { get => _comboBoxTextPosition; }
        protected GroupBox GroupBox1 { get => _groupBox1; }
        protected Label Label8 { get => _label8; }
        protected ComboBox ComboBoxLayoutType { get => _comboBoxLayoutType; }
        protected Label LabelRowCount { get => _labelRowCount; }
        protected NumericUpDown UpDownRows { get => _upDownRows; }
        protected Label LabelRowCountInfo { get => _labelRowCountInfo; }
        protected Label LabelColumnCount { get => _labelColumnCount; }
        protected NumericUpDown UpDownColumns { get => _upDownColumns; }
        protected Label LabelGripper { get => _labelGripper; }
        protected ComboBox ComboBoxGripper { get => _comboBoxGripper; }

        private TRibbonGallery _gallery;

        //resourcestring
        public const string RS_NONE = "None";
        public const string RS_VERTICAL = "Vertical";
        public const string RS_CORNER = "Corner";
        // Layout Type
        public const int LT_DEFAULT = 0;
        public const int LT_VERTICAL = 1;
        public const int LT_FLOW = 2;

        public TFrameGallery()
        {
            bool designtime = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
            if (designtime)
                InitializeComponent();
        }

        private void DisableControlsInAppMenu()
        {
            if (_gallery.Parent.ObjectType() == RibbonObjectType.AppMenuGroup)
            {
                _label2.Enabled = false;
                _comboBoxGalleryType.Enabled = false;
                _label7.Enabled = false;
                _comboBoxTextPosition.Enabled = false;
                _label3.Enabled = false;
                _label4.Enabled = false;
                _upDownItemHeight.Enabled = false;
                _label5.Enabled = false;
                _label6.Enabled = false;
                _upDownItemWidth.Enabled = false;
                _checkBoxHasLargeItems.Enabled = false;
                _label8.Enabled = false;
                _comboBoxLayoutType.Enabled = false;
                _groupBox1.Enabled = false;
            }
        }

        protected override void InitComponentStep1()
        {
            if (components == null)
                components = new Container();
            this._label2 = new System.Windows.Forms.Label();
            this._comboBoxGalleryType = new System.Windows.Forms.ComboBox();
            this._label7 = new System.Windows.Forms.Label();
            this._comboBoxTextPosition = new System.Windows.Forms.ComboBox();
            this._label3 = new System.Windows.Forms.Label();
            this._upDownItemWidth = new System.Windows.Forms.NumericUpDown();
            this._label4 = new System.Windows.Forms.Label();
            this._label5 = new System.Windows.Forms.Label();
            this._upDownItemHeight = new System.Windows.Forms.NumericUpDown();
            this._label6 = new System.Windows.Forms.Label();
            this._checkBoxHasLargeItems = new System.Windows.Forms.CheckBox();
            this.groupLayout = new System.Windows.Forms.TableLayoutPanel();
            this._label8 = new System.Windows.Forms.Label();
            this._comboBoxLayoutType = new System.Windows.Forms.ComboBox();
            this._labelRowCount = new System.Windows.Forms.Label();
            this._upDownRows = new System.Windows.Forms.NumericUpDown();
            this._labelRowCountInfo = new System.Windows.Forms.Label();
            this._labelColumnCount = new System.Windows.Forms.Label();
            this._upDownColumns = new System.Windows.Forms.NumericUpDown();
            this._labelGripper = new System.Windows.Forms.Label();
            this._comboBoxGripper = new System.Windows.Forms.ComboBox();
            this._groupBox1 = new System.Windows.Forms.GroupBox();
            base.InitComponentStep1();
        }

        protected override void InitSuspend()
        {
            ((System.ComponentModel.ISupportInitialize)(this._upDownItemWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._upDownItemHeight)).BeginInit();
            this.groupLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._upDownRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._upDownColumns)).BeginInit();
            this._groupBox1.SuspendLayout();
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
            this._label2.Size = new System.Drawing.Size(66, 27);
            this._label2.TabIndex = 0;
            this._label2.Text = "Gallery Type";
            this._label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _comboBoxGalleryType
            // 
            this.LayoutPanel.SetColumnSpan(this._comboBoxGalleryType, 3);
            this._comboBoxGalleryType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this._comboBoxGalleryType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxGalleryType.FormattingEnabled = true;
            this._comboBoxGalleryType.Items.AddRange(new object[] {
            "Items",
            "Commands"});
            this._comboBoxGalleryType.Location = new System.Drawing.Point(123, 57);
            this._comboBoxGalleryType.MaxDropDownItems = 30;
            this._comboBoxGalleryType.Name = "_comboBoxGalleryType";
            this._comboBoxGalleryType.Size = new System.Drawing.Size(250, 21);
            this._comboBoxGalleryType.TabIndex = 5;
            // 
            // _label7
            // 
            this._label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label7.AutoSize = true;
            this._label7.Location = new System.Drawing.Point(3, 81);
            this._label7.Name = "_label7";
            this._label7.Size = new System.Drawing.Size(68, 27);
            this._label7.TabIndex = 0;
            this._label7.Text = "Text Position";
            this._label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _comboBoxTextPosition
            // 
            this.LayoutPanel.SetColumnSpan(this._comboBoxTextPosition, 3);
            this._comboBoxTextPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this._comboBoxTextPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxTextPosition.FormattingEnabled = true;
            this._comboBoxTextPosition.Items.AddRange(new object[] {
            "Bottom",
            "Hide",
            "Left",
            "Overlap",
            "Right",
            "Top"});
            this._comboBoxTextPosition.Location = new System.Drawing.Point(123, 84);
            this._comboBoxTextPosition.MaxDropDownItems = 30;
            this._comboBoxTextPosition.Name = "_comboBoxTextPosition";
            this._comboBoxTextPosition.Size = new System.Drawing.Size(250, 21);
            this._comboBoxTextPosition.TabIndex = 6;
            // 
            // _label3
            // 
            this._label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label3.AutoSize = true;
            this._label3.Location = new System.Drawing.Point(3, 108);
            this._label3.Name = "_label3";
            this._label3.Size = new System.Drawing.Size(58, 26);
            this._label3.TabIndex = 0;
            this._label3.Text = "Item Width";
            this._label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _upDownItemWidth
            // 
            this._upDownItemWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this._upDownItemWidth.Location = new System.Drawing.Point(123, 111);
            this._upDownItemWidth.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this._upDownItemWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this._upDownItemWidth.Name = "_upDownItemWidth";
            this._upDownItemWidth.Size = new System.Drawing.Size(81, 20);
            this._upDownItemWidth.TabIndex = 7;
            this._upDownItemWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // _label4
            // 
            this._label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label4.AutoSize = true;
            this.LayoutPanel.SetColumnSpan(this._label4, 2);
            this._label4.Location = new System.Drawing.Point(210, 108);
            this._label4.Name = "_label4";
            this._label4.Size = new System.Drawing.Size(142, 26);
            this._label4.TabIndex = 0;
            this._label4.Text = "Use -1 for auto/default width";
            this._label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _label5
            // 
            this._label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label5.AutoSize = true;
            this._label5.Location = new System.Drawing.Point(3, 134);
            this._label5.Name = "_label5";
            this._label5.Size = new System.Drawing.Size(61, 26);
            this._label5.TabIndex = 0;
            this._label5.Text = "Item Height";
            this._label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _upDownItemHeight
            // 
            this._upDownItemHeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this._upDownItemHeight.Location = new System.Drawing.Point(123, 137);
            this._upDownItemHeight.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this._upDownItemHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this._upDownItemHeight.Name = "_upDownItemHeight";
            this._upDownItemHeight.Size = new System.Drawing.Size(81, 20);
            this._upDownItemHeight.TabIndex = 8;
            this._upDownItemHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // _label6
            // 
            this._label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label6.AutoSize = true;
            this.LayoutPanel.SetColumnSpan(this._label6, 2);
            this._label6.Location = new System.Drawing.Point(210, 134);
            this._label6.Name = "_label6";
            this._label6.Size = new System.Drawing.Size(146, 26);
            this._label6.TabIndex = 0;
            this._label6.Text = "Use -1 for auto/default height";
            this._label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _checkBoxHasLargeItems
            // 
            this._checkBoxHasLargeItems.AutoSize = true;
            this._checkBoxHasLargeItems.Location = new System.Drawing.Point(3, 163);
            this._checkBoxHasLargeItems.Name = "_checkBoxHasLargeItems";
            this._checkBoxHasLargeItems.Size = new System.Drawing.Size(98, 17);
            this._checkBoxHasLargeItems.TabIndex = 9;
            this._checkBoxHasLargeItems.Text = "Has large items";
            this._checkBoxHasLargeItems.UseVisualStyleBackColor = true;
            // 
            // groupLayout
            // 
            this.groupLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupLayout.AutoSize = true;
            this.groupLayout.ColumnCount = 3;
            this.groupLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 114F));
            this.groupLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this.groupLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, ThirdColumnWidth + 18F));
            this.groupLayout.Controls.Add(this._label8, 0, 0);
            this.groupLayout.Controls.Add(this._comboBoxLayoutType, 1, 0);
            this.groupLayout.Controls.Add(this._labelRowCount, 0, 1);
            this.groupLayout.Controls.Add(this._upDownRows, 1, 1);
            this.groupLayout.Controls.Add(this._labelRowCountInfo, 2, 1);
            this.groupLayout.Controls.Add(this._labelColumnCount, 0, 2);
            this.groupLayout.Controls.Add(this._upDownColumns, 1, 2);
            this.groupLayout.Controls.Add(this._labelGripper, 0, 3);
            this.groupLayout.Controls.Add(this._comboBoxGripper, 1, 3);
            this.groupLayout.Location = new System.Drawing.Point(3, 19);
            this.groupLayout.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.groupLayout.Name = "groupLayout";
            this.groupLayout.RowCount = 4;
            this.groupLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.groupLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.groupLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.groupLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.groupLayout.Size = new System.Drawing.Size(364, 106);
            this.groupLayout.TabIndex = 0;
            // 
            // _label8
            // 
            this._label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label8.AutoSize = true;
            this._label8.Location = new System.Drawing.Point(3, 0);
            this._label8.Name = "_label8";
            this._label8.Size = new System.Drawing.Size(66, 27);
            this._label8.TabIndex = 0;
            this._label8.Text = "Layout Type";
            this._label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _comboBoxLayoutType
            // 
            this._comboBoxLayoutType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupLayout.SetColumnSpan(this._comboBoxLayoutType, 2);
            this._comboBoxLayoutType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxLayoutType.FormattingEnabled = true;
            this._comboBoxLayoutType.Items.AddRange(new object[] {
            "Default Layout",
            "Vertical Layout",
            "Flow Layout"});
            this._comboBoxLayoutType.Location = new System.Drawing.Point(117, 3);
            this._comboBoxLayoutType.MaxDropDownItems = 30;
            this._comboBoxLayoutType.Name = "_comboBoxLayoutType";
            this._comboBoxLayoutType.Size = new System.Drawing.Size(244, 21);
            this._comboBoxLayoutType.TabIndex = 1;
            // 
            // _labelRowCount
            // 
            this._labelRowCount.AllowDrop = true;
            this._labelRowCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._labelRowCount.AutoSize = true;
            this._labelRowCount.Location = new System.Drawing.Point(3, 27);
            this._labelRowCount.Name = "_labelRowCount";
            this._labelRowCount.Size = new System.Drawing.Size(60, 26);
            this._labelRowCount.TabIndex = 0;
            this._labelRowCount.Text = "Row Count";
            this._labelRowCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _upDownRows
            // 
            this._upDownRows.Location = new System.Drawing.Point(117, 30);
            this._upDownRows.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this._upDownRows.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this._upDownRows.Name = "_upDownRows";
            this._upDownRows.Size = new System.Drawing.Size(81, 20);
            this._upDownRows.TabIndex = 2;
            this._upDownRows.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // _labelRowCountInfo
            // 
            this._labelRowCountInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._labelRowCountInfo.AutoSize = true;
            this._labelRowCountInfo.Location = new System.Drawing.Point(204, 27);
            this._labelRowCountInfo.Name = "_labelRowCountInfo";
            this._labelRowCountInfo.Size = new System.Drawing.Size(88, 26);
            this._labelRowCountInfo.TabIndex = 0;
            this._labelRowCountInfo.Text = "Use -1 for default";
            this._labelRowCountInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _labelColumnCount
            // 
            this._labelColumnCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._labelColumnCount.AutoSize = true;
            this._labelColumnCount.Location = new System.Drawing.Point(3, 53);
            this._labelColumnCount.Name = "_labelColumnCount";
            this._labelColumnCount.Size = new System.Drawing.Size(73, 26);
            this._labelColumnCount.TabIndex = 0;
            this._labelColumnCount.Text = "Column Count";
            this._labelColumnCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _upDownColumns
            // 
            this._upDownColumns.Location = new System.Drawing.Point(117, 56);
            this._upDownColumns.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this._upDownColumns.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._upDownColumns.Name = "_upDownColumns";
            this._upDownColumns.Size = new System.Drawing.Size(81, 20);
            this._upDownColumns.TabIndex = 3;
            this._upDownColumns.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // _labelGripper
            // 
            this._labelGripper.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._labelGripper.AutoSize = true;
            this._labelGripper.Location = new System.Drawing.Point(3, 79);
            this._labelGripper.Name = "_labelGripper";
            this._labelGripper.Size = new System.Drawing.Size(41, 27);
            this._labelGripper.TabIndex = 0;
            this._labelGripper.Text = "Gripper";
            this._labelGripper.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _comboBoxGripper
            // 
            this._comboBoxGripper.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupLayout.SetColumnSpan(this._comboBoxGripper, 2);
            this._comboBoxGripper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxGripper.FormattingEnabled = true;
            this._comboBoxGripper.Items.AddRange(new object[] {
            "None",
            "Vertical",
            "Corner"});
            this._comboBoxGripper.Location = new System.Drawing.Point(117, 82);
            this._comboBoxGripper.MaxDropDownItems = 30;
            this._comboBoxGripper.Name = "_comboBoxGripper";
            this._comboBoxGripper.Size = new System.Drawing.Size(244, 21);
            this._comboBoxGripper.TabIndex = 4;
            // 
            // _groupBox1
            // 
            this.LayoutPanel.SetColumnSpan(this._groupBox1, 4);
            this._groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom)));
            this._groupBox1.AutoSize = true;
            this._groupBox1.Controls.Add(this.groupLayout);
            this._groupBox1.Location = new System.Drawing.Point(3, 186);
            this._groupBox1.Name = "_groupBox1";
            this._groupBox1.Size = new System.Drawing.Size(370, 141);
            this._groupBox1.TabIndex = 10;
            this._groupBox1.TabStop = false;
            this._groupBox1.Text = "Menu Layout";
        }

        protected override void InitComponentStep3()
        {
            this.LayoutPanel.Controls.Add(this._label2, 0, 2);
            this.LayoutPanel.Controls.Add(this._comboBoxGalleryType, 1, 2);
            this.LayoutPanel.Controls.Add(this._label7, 0, 3);
            this.LayoutPanel.Controls.Add(this._comboBoxTextPosition, 1, 3);
            this.LayoutPanel.Controls.Add(this._label3, 0, 4);
            this.LayoutPanel.Controls.Add(this._upDownItemWidth, 1, 4);
            this.LayoutPanel.Controls.Add(this._label4, 2, 4);
            this.LayoutPanel.Controls.Add(this._label5, 0, 5);
            this.LayoutPanel.Controls.Add(this._upDownItemHeight, 1, 5);
            this.LayoutPanel.Controls.Add(this._label6, 2, 5);
            this.LayoutPanel.Controls.Add(this._checkBoxHasLargeItems, 0, 6);
            this.LayoutPanel.Controls.Add(this._groupBox1, 0, 7);
            base.InitComponentStep3();
        }

        protected override void InitResume()
        {
            ((System.ComponentModel.ISupportInitialize)(this._upDownItemWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._upDownItemHeight)).EndInit();
            this.groupLayout.ResumeLayout(false);
            this.groupLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._upDownRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._upDownColumns)).EndInit();
            this._groupBox1.ResumeLayout(false);
            base.InitResume();
        }

        protected override void InitEvents()
        {
            base.InitEvents();
            ComboBoxGalleryType.SelectedIndexChanged += ComboBoxGalleryTypeChange;
            CheckBoxHasLargeItems.Click += CheckBoxHasLargeItemsClick;
            UpDownItemWidth.ValueChanged += EditItemWidthChange;
            UpDownItemHeight.ValueChanged += EditItemHeightChange;
            ComboBoxTextPosition.SelectedIndexChanged += ComboBoxTextPositionChange;
            ComboBoxLayoutType.SelectedIndexChanged += ComboBoxLayoutTypeChange;
            UpDownRows.ValueChanged += EditRowsChange;
            UpDownColumns.ValueChanged += EditColumnsChange;
            ComboBoxGripper.SelectedIndexChanged += ComboBoxGripperChange;
        }

        protected override void InitTooltips(IContainer components)
        {
            base.InitTooltips(components);
            viewsTip.SetToolTip(ComboBoxGalleryType, "Whether the gallery shows items or commands");
            viewsTip.SetToolTip(CheckBoxHasLargeItems, "Whether the gallery shows large or small images");
            viewsTip.SetToolTip(UpDownItemWidth, "Width of the items in the gallery");
            viewsTip.SetToolTip(UpDownItemHeight, "Height of the items in the gallery");
            viewsTip.SetToolTip(ComboBoxTextPosition, "Position of the text for the items");
            viewsTip.SetToolTip(ComboBoxLayoutType, "How the items are layed out in the gallery");
            viewsTip.SetToolTip(UpDownRows, "Number of rows in the layout");
            viewsTip.SetToolTip(UpDownColumns, "Number of columns in the flow layout");
            viewsTip.SetToolTip(ComboBoxGripper, "How the items are layed out in the gallery");
        }

        private void InitializeBaseComponent()
        {
            LabelHeader.Text = "  Gallery";
            LayoutPanel.SuspendLayout();
            //LayoutPanel.Controls.Add(_labelApplicationModes, 0, 1);
            //_editApplicationModes.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            //LayoutPanel.Controls.Add(_editApplicationModes, 1, 1);
            //LayoutPanel.SetColumnSpan(_editApplicationModes, 2);
            LayoutPanel.ResumeLayout();
        }

        private void CheckBoxHasLargeItemsClick(object sender, EventArgs e)
        {
            if (CheckBoxHasLargeItems.Checked != _gallery.HasLargeItems)
            {
                _gallery.HasLargeItems = CheckBoxHasLargeItems.Checked;
                Modified();
            }
        }

        private void ComboBoxGalleryTypeChange(object sender, EventArgs e)
        {
            if (ComboBoxGalleryType.SelectedIndex != (int)(_gallery.GalleryType))
            {
                _gallery.GalleryType = (RibbonGalleryType)(ComboBoxGalleryType.SelectedIndex);
                Modified();
            }
        }

        private void ComboBoxGripperChange(object sender, EventArgs e)
        {
            int gripperIndex;
            TRibbonVerticalMenuLayout verticalLayout;
            TRibbonFlowMenuLayout flowLayout;

            verticalLayout = null;
            flowLayout = null;
            gripperIndex = -1;
            if (_gallery.MenuLayout != null)
            {
                if (_gallery.MenuLayout is TRibbonVerticalMenuLayout)
                {
                    verticalLayout = (TRibbonVerticalMenuLayout)(_gallery.MenuLayout);
                    gripperIndex = (int)(verticalLayout.Gripper);
                }
                else if (_gallery.MenuLayout is TRibbonFlowMenuLayout)
                {
                    flowLayout = (TRibbonFlowMenuLayout)(_gallery.MenuLayout);
                    gripperIndex = (int)(flowLayout.Gripper);
                }
                else
                    Debug.Assert(false);

                if (gripperIndex != ComboBoxGripper.SelectedIndex)
                {
                    if (verticalLayout != null)
                        verticalLayout.Gripper = (RibbonSingleColumnGripperType)(ComboBoxGripper.SelectedIndex);
                    else if (flowLayout != null)
                        flowLayout.Gripper = (RibbonMultiColumnGripperType)(ComboBoxGripper.SelectedIndex);
                    Modified();
                }
            }
        }

        private void ComboBoxLayoutTypeChange(object sender, EventArgs e)
        {
            switch (ComboBoxLayoutType.SelectedIndex)
            {
                case LT_DEFAULT:
                    if (_gallery.MenuLayout != null)
                    {
                        _gallery.RemoveMenuLayout();
                        Modified();
                        UpdateControls();
                    }
                    break;
                case LT_VERTICAL:
                    if ((_gallery.MenuLayout == null) || (!(_gallery.MenuLayout is TRibbonVerticalMenuLayout)))
                    {
                        _gallery.CreateVerticalMenuLayout();
                        Modified();
                        UpdateControls();
                    }
                    break;
                case LT_FLOW:
                    if ((_gallery.MenuLayout == null) || (!(_gallery.MenuLayout is TRibbonFlowMenuLayout)))
                    {
                        _gallery.CreateFlowMenuLayout();
                        Modified();
                        UpdateControls();
                    }
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        private void ComboBoxTextPositionChange(object sender, EventArgs e)
        {
            if (ComboBoxTextPosition.SelectedIndex != (int)(_gallery.TextPosition))
            {
                _gallery.TextPosition = (RibbonTextPosition)(ComboBoxTextPosition.SelectedIndex);
                Modified();
            }
        }

        private void EditColumnsChange(object sender, EventArgs e)
        {
            TRibbonFlowMenuLayout layout;
            if ((_gallery.MenuLayout != null) && (_gallery.MenuLayout is TRibbonFlowMenuLayout))
            {
                layout = (TRibbonFlowMenuLayout)(_gallery.MenuLayout);
                if (UpDownColumns.Value != layout.Columns)
                {
                    layout.Columns = (int)UpDownColumns.Value;
                    Modified();
                }
            }
        }

        private void EditItemHeightChange(object sender, EventArgs e)
        {
            if (UpDownItemHeight.Value != _gallery.ItemHeight)
            {
                _gallery.ItemHeight = (int)UpDownItemHeight.Value;
                Modified();
            }
        }

        private void EditItemWidthChange(object sender, EventArgs e)
        {
            if (UpDownItemWidth.Value != _gallery.ItemWidth)
            {
                _gallery.ItemWidth = (int)UpDownItemWidth.Value;
                Modified();
            }
        }

        private void EditRowsChange(object sender, EventArgs e)
        {
            if ((_gallery.MenuLayout != null) && (UpDownRows.Value != _gallery.MenuLayout.Rows))
            {
                _gallery.MenuLayout.Rows = (int)UpDownRows.Value;
                Modified();
            }
        }

        protected override void Initialize(TRibbonObject subject)
        {
            base.Initialize(subject);
            _gallery = subject as TRibbonGallery;
            ComboBoxGalleryType.SelectedIndex = (int)(_gallery.GalleryType);
            ComboBoxTextPosition.SelectedIndex = (int)(_gallery.TextPosition);
            UpDownItemWidth.Value = _gallery.ItemWidth;
            UpDownItemHeight.Value = _gallery.ItemHeight;
            CheckBoxHasLargeItems.Checked = _gallery.HasLargeItems;
            if (_gallery.MenuLayout != null)
            {
                if (_gallery.MenuLayout is TRibbonVerticalMenuLayout)
                    ComboBoxLayoutType.SelectedIndex = LT_VERTICAL;
                else if (_gallery.MenuLayout is TRibbonFlowMenuLayout)
                    ComboBoxLayoutType.SelectedIndex = LT_FLOW;
                else
                    Debug.Assert(false);
            }
            else
                ComboBoxLayoutType.SelectedIndex = LT_DEFAULT;
            DisableControlsInAppMenu(); //added
            UpdateControls();
        }

        private void UpdateControls()
        {
            LabelRowCount.Enabled = (ComboBoxLayoutType.SelectedIndex != LT_DEFAULT);
            UpDownRows.Enabled = LabelRowCount.Enabled;
            UpDownRows.Enabled = LabelRowCount.Enabled;
            LabelRowCountInfo.Enabled = LabelRowCount.Enabled;
            if ((UpDownRows.Enabled) && (_gallery.MenuLayout != null))
                UpDownRows.Value = _gallery.MenuLayout.Rows;
            else
                UpDownRows.Value = -1;

            LabelColumnCount.Enabled = (ComboBoxLayoutType.SelectedIndex == LT_FLOW);
            UpDownColumns.Enabled = LabelColumnCount.Enabled;
            UpDownColumns.Enabled = LabelColumnCount.Enabled;
            if ((UpDownColumns.Enabled) && (_gallery.MenuLayout != null) && (_gallery.MenuLayout is TRibbonFlowMenuLayout))
            {
                UpDownColumns.Value = ((TRibbonFlowMenuLayout)(_gallery.MenuLayout)).Columns;
                UpDownColumns.Minimum = 1;
            }
            else
            {
                UpDownColumns.Minimum = -1;
                UpDownColumns.Value = -1;
            }

            LabelGripper.Enabled = (ComboBoxLayoutType.SelectedIndex != LT_DEFAULT);
            ComboBoxGripper.Enabled = LabelGripper.Enabled;
            if (ComboBoxGripper.Enabled)
            {
                ComboBoxGripper.Items.Clear();
                ComboBoxGripper.Items.Add(RS_NONE);
                ComboBoxGripper.Items.Add(RS_VERTICAL);
                if (ComboBoxLayoutType.SelectedIndex == LT_FLOW)
                    ComboBoxGripper.Items.Add(RS_CORNER);
                if (_gallery.MenuLayout != null)
                {
                    if (_gallery.MenuLayout is TRibbonVerticalMenuLayout)
                        ComboBoxGripper.SelectedIndex = (int)((TRibbonVerticalMenuLayout)(_gallery.MenuLayout)).Gripper;
                    else if (_gallery.MenuLayout is TRibbonFlowMenuLayout)
                        ComboBoxGripper.SelectedIndex = (int)((TRibbonFlowMenuLayout)(_gallery.MenuLayout)).Gripper;
                    else
                        Debug.Assert(false);
                }
            }
            else
                ComboBoxGripper.SelectedIndex = -1;
        }
    }
}
