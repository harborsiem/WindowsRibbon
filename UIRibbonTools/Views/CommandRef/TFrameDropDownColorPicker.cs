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
    partial class TFrameDropDownColorPicker : TFrameCommandRefObject
    {
        private static Image sample = ImageManager.DropDownColorPickerSample();

        private Label Label2 { get => _label2; }
        private ComboBox ComboBoxTemplate { get => _comboBoxTemplate; }
        private ComboBox ComboBoxChipSize { get => _comboBoxChipSize; }
        private Label Label3 { get => _label3; }
        private Label Label4 { get => _label4; }
        private NumericUpDown UpDownColumns { get => _upDownColumns; }
        private Label Label5 { get => _label5; }
        private NumericUpDown UpDownRecentRows { get => _upDownRecentRows; }
        private Label Label6 { get => _label6; }
        private NumericUpDown UpDownStandardRows { get => _upDownStandardRows; }
        private Label Label7 { get => _label7; }
        private NumericUpDown UpDownThemeRows { get => _upDownThemeRows; }
        private CheckBox CheckBoxAutoColor { get => _checkBoxAutoColor; }
        private CheckBox CheckBoxNoColor { get => _checkBoxNoColor; }

        private TRibbonDropDownColorPicker _colorPicker;

        public TFrameDropDownColorPicker()
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
            this._comboBoxTemplate = new System.Windows.Forms.ComboBox();
            this._comboBoxChipSize = new System.Windows.Forms.ComboBox();
            this._label3 = new System.Windows.Forms.Label();
            this._label4 = new System.Windows.Forms.Label();
            this._upDownColumns = new System.Windows.Forms.NumericUpDown();
            this._label5 = new System.Windows.Forms.Label();
            this._upDownRecentRows = new System.Windows.Forms.NumericUpDown();
            this._label6 = new System.Windows.Forms.Label();
            this._upDownStandardRows = new System.Windows.Forms.NumericUpDown();
            this._label7 = new System.Windows.Forms.Label();
            this._upDownThemeRows = new System.Windows.Forms.NumericUpDown();
            this._checkBoxAutoColor = new System.Windows.Forms.CheckBox();
            this._checkBoxNoColor = new System.Windows.Forms.CheckBox();
            base.InitComponentStep1();
        }

        protected override void InitSuspend()
        {
            ((System.ComponentModel.ISupportInitialize)(this._upDownColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._upDownRecentRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._upDownStandardRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._upDownThemeRows)).BeginInit();

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
            this._label2.Size = new System.Drawing.Size(78, 27);
            this._label2.TabIndex = 0;
            this._label2.Text = "Color Template";
            this._label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _comboBoxTemplate
            // 
            this._comboBoxTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LayoutPanel.SetColumnSpan(this._comboBoxTemplate, 3);
            this._comboBoxTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxTemplate.Items.AddRange(new object[] {
            "Theme Colors",
            "Standard Colors",
            "Highlight Colors"});
            this._comboBoxTemplate.Location = new System.Drawing.Point(123, 30);
            this._comboBoxTemplate.Name = "_comboBoxTemplate";
            this._comboBoxTemplate.Size = new System.Drawing.Size(250, 21);
            this._comboBoxTemplate.TabIndex = 1;
            // 
            // _comboBoxChipSize
            // 
            this._comboBoxChipSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LayoutPanel.SetColumnSpan(this._comboBoxChipSize, 3);
            this._comboBoxChipSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxChipSize.Items.AddRange(new object[] {
            "Small",
            "Medium",
            "Large"});
            this._comboBoxChipSize.Location = new System.Drawing.Point(123, 57);
            this._comboBoxChipSize.Name = "_comboBoxChipSize";
            this._comboBoxChipSize.Size = new System.Drawing.Size(250, 21);
            this._comboBoxChipSize.TabIndex = 2;
            // 
            // _label3
            // 
            this._label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label3.AutoSize = true;
            this._label3.Location = new System.Drawing.Point(3, 54);
            this._label3.Name = "_label3";
            this._label3.Size = new System.Drawing.Size(51, 27);
            this._label3.TabIndex = 2;
            this._label3.Text = "Chip Size";
            this._label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _label4
            // 
            this._label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label4.AutoSize = true;
            this._label4.Location = new System.Drawing.Point(3, 81);
            this._label4.Name = "_label4";
            this._label4.Size = new System.Drawing.Size(47, 27);
            this._label4.TabIndex = 3;
            this._label4.Text = "Columns";
            this._label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _upDownColumns
            // 
            this._upDownColumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this._upDownColumns.Location = new System.Drawing.Point(123, 84);
            this._upDownColumns.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this._upDownColumns.Name = "_upDownColumns";
            this._upDownColumns.Size = new System.Drawing.Size(81, 20);
            this._upDownColumns.TabIndex = 4;
            this._upDownColumns.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // _label5
            // 
            this._label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label5.AutoSize = true;
            this._label5.Location = new System.Drawing.Point(3, 108);
            this._label5.Name = "_label5";
            this._label5.Size = new System.Drawing.Size(99, 27);
            this._label5.TabIndex = 5;
            this._label5.Text = "Recent Color Rows";
            this._label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _upDownRecentRows
            // 
            this._upDownRecentRows.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this._upDownRecentRows.Location = new System.Drawing.Point(123, 111);
            this._upDownRecentRows.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this._upDownRecentRows.Name = "_upDownRecentRows";
            this._upDownRecentRows.Size = new System.Drawing.Size(81, 20);
            this._upDownRecentRows.TabIndex = 6;
            this._upDownRecentRows.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // _label6
            // 
            this._label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label6.AutoSize = true;
            this._label6.Location = new System.Drawing.Point(3, 135);
            this._label6.Name = "_label6";
            this._label6.Size = new System.Drawing.Size(107, 27);
            this._label6.TabIndex = 7;
            this._label6.Text = "Standard Color Rows";
            this._label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _upDownStandardRows
            // 
            this._upDownStandardRows.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this._upDownStandardRows.Location = new System.Drawing.Point(123, 138);
            this._upDownStandardRows.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this._upDownStandardRows.Name = "_upDownStandardRows";
            this._upDownStandardRows.Size = new System.Drawing.Size(81, 20);
            this._upDownStandardRows.TabIndex = 8;
            this._upDownStandardRows.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // _label7
            // 
            this._label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label7.AutoSize = true;
            this._label7.Location = new System.Drawing.Point(3, 162);
            this._label7.Name = "_label7";
            this._label7.Size = new System.Drawing.Size(97, 27);
            this._label7.TabIndex = 9;
            this._label7.Text = "Theme Color Rows";
            this._label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _upDownThemeRows
            // 
            this._upDownThemeRows.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this._upDownThemeRows.Location = new System.Drawing.Point(123, 165);
            this._upDownThemeRows.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this._upDownThemeRows.Name = "_upDownThemeRows";
            this._upDownThemeRows.Size = new System.Drawing.Size(81, 20);
            this._upDownThemeRows.TabIndex = 10;
            this._upDownThemeRows.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // _checkBoxAutoColor
            // 
            this._checkBoxAutoColor.AutoSize = true;
            this.LayoutPanel.SetColumnSpan(this._checkBoxAutoColor, 3);
            this._checkBoxAutoColor.Location = new System.Drawing.Point(3, 192);
            this._checkBoxAutoColor.Name = "_checkBoxAutoColor";
            this._checkBoxAutoColor.Size = new System.Drawing.Size(165, 17);
            this._checkBoxAutoColor.TabIndex = 11;
            this._checkBoxAutoColor.Text = "Automatic Color button visible";
            // 
            // _checkBoxNoColor
            // 
            this._checkBoxNoColor.AutoSize = true;
            this.LayoutPanel.SetColumnSpan(this._checkBoxNoColor, 3);
            this._checkBoxNoColor.Location = new System.Drawing.Point(3, 219);
            this._checkBoxNoColor.Name = "_checkBoxNoColor";
            this._checkBoxNoColor.Size = new System.Drawing.Size(132, 17);
            this._checkBoxNoColor.TabIndex = 12;
            this._checkBoxNoColor.Text = "No Color button visible";

            LabelHeader.Text = "  DropDownColorPicker Properties";
            LabelHeader.ImageIndex = 4;
        }

        protected override void InitComponentStep3()
        {
            this.LayoutPanel.Controls.Add(this._label2, 0, 1);
            this.LayoutPanel.Controls.Add(this._comboBoxTemplate, 1, 1);
            this.LayoutPanel.Controls.Add(this._label3, 0, 2);
            this.LayoutPanel.Controls.Add(this._comboBoxChipSize, 1, 2);
            this.LayoutPanel.Controls.Add(this._label4, 0, 3);
            this.LayoutPanel.Controls.Add(this._upDownColumns, 1, 3);
            this.LayoutPanel.Controls.Add(this._label5, 0, 4);
            this.LayoutPanel.Controls.Add(this._upDownRecentRows, 1, 4);
            this.LayoutPanel.Controls.Add(this._label6, 0, 5);
            this.LayoutPanel.Controls.Add(this._upDownStandardRows, 1, 5);
            this.LayoutPanel.Controls.Add(this._label7, 0, 6);
            this.LayoutPanel.Controls.Add(this._upDownThemeRows, 1, 6);
            this.LayoutPanel.Controls.Add(this._checkBoxAutoColor, 0, 7);
            this.LayoutPanel.Controls.Add(this._checkBoxNoColor, 0, 8);
            base.InitComponentStep3();
        }

        protected override void InitResume()
        {
            ((System.ComponentModel.ISupportInitialize)(this._upDownColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._upDownRecentRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._upDownStandardRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._upDownThemeRows)).EndInit();

            base.InitResume();
        }

        protected override void InitEvents()
        {
            base.InitEvents();
            ComboBoxTemplate.SelectedIndexChanged += ComboBoxTemplateChange;
            ComboBoxChipSize.SelectedIndexChanged += ComboBoxChipSizeChange;
            UpDownColumns.ValueChanged += EditColumnsChange;
            UpDownRecentRows.ValueChanged += EditRecentRowsChange;
            UpDownStandardRows.ValueChanged += EditStandardRowsChange;
            UpDownThemeRows.ValueChanged += EditThemeRowsChange;
            CheckBoxAutoColor.Click += CheckBoxAutoColorClick;
            CheckBoxNoColor.Click += CheckBoxNoColorClick;
        }

        protected override void InitTooltips(IContainer components)
        {
            base.InitTooltips(components);
            viewsTip.SetToolTip(ComboBoxTemplate, "The type of drop down to show");
            viewsTip.SetToolTip(ComboBoxChipSize, "Size of the color swatches");
            viewsTip.SetToolTip(UpDownColumns, "Number of columns of color swatches");
            viewsTip.SetToolTip(UpDownRecentRows, "Number of color swatch rows in the Recent Colors area");
            viewsTip.SetToolTip(UpDownStandardRows, "Number of color swatch rows in the Standard Colors area");
            viewsTip.SetToolTip(UpDownThemeRows, "Number of color swatch rows in the Theme Colors area");
            viewsTip.SetToolTip(CheckBoxAutoColor, "Whether the Automatic Color button is visible");
            viewsTip.SetToolTip(CheckBoxNoColor, "Whether the No Color button is visible");
        }

        private void CheckBoxAutoColorClick(object sender, EventArgs e)
        {
            if (CheckBoxAutoColor.Checked != _colorPicker.IsAutomaticColorButtonVisible)
            {
                _colorPicker.IsAutomaticColorButtonVisible = CheckBoxAutoColor.Checked;
                Modified();
            }
        }

        private void CheckBoxNoColorClick(object sender, EventArgs e)
        {
            if (CheckBoxNoColor.Checked != _colorPicker.IsNoColorButtonVisible)
            {
                _colorPicker.IsNoColorButtonVisible = CheckBoxNoColor.Checked;
                Modified();
            }
        }

        private void ComboBoxChipSizeChange(object sender, EventArgs e)
        {
            if (ComboBoxChipSize.SelectedIndex != (int)(_colorPicker.ChipSize))
            {
                _colorPicker.ChipSize = (RibbonChipSize)(ComboBoxChipSize.SelectedIndex);
                Modified();
            }
        }

        private void ComboBoxTemplateChange(object sender, EventArgs e)
        {
            if (ComboBoxTemplate.SelectedIndex != (int)(_colorPicker.ColorTemplate))
            {
                _colorPicker.ColorTemplate = (RibbonColorTemplate)(ComboBoxTemplate.SelectedIndex);
                Modified();
            }
        }

        private void EditColumnsChange(object sender, EventArgs e)
        {
            if (UpDownColumns.Value != _colorPicker.Columns)
            {
                _colorPicker.Columns = (int)UpDownColumns.Value;
                Modified();
            }
        }

        private void EditRecentRowsChange(object sender, EventArgs e)
        {
            if (UpDownRecentRows.Value != _colorPicker.RecentColorGridRows)
            {
                _colorPicker.RecentColorGridRows = (int)UpDownRecentRows.Value;
                Modified();
            }
        }

        private void EditStandardRowsChange(object sender, EventArgs e)
        {
            if (UpDownStandardRows.Value != _colorPicker.StandardColorGridRows)
            {
                _colorPicker.StandardColorGridRows = (int)UpDownStandardRows.Value;
                Modified();
            }
        }

        private void EditThemeRowsChange(object sender, EventArgs e)
        {
            if (UpDownThemeRows.Value != _colorPicker.ThemeColorGridRows)
            {
                _colorPicker.ThemeColorGridRows = (int)UpDownThemeRows.Value;
                Modified();
            }
        }

        protected override void Initialize(TRibbonObject subject)
        {
            base.Initialize(subject);
            _colorPicker = subject as TRibbonDropDownColorPicker;
            ComboBoxTemplate.SelectedIndex = (int)(_colorPicker.ColorTemplate);
            ComboBoxChipSize.SelectedIndex = (int)(_colorPicker.ChipSize);
            UpDownColumns.Value = _colorPicker.Columns;
            UpDownRecentRows.Value = _colorPicker.RecentColorGridRows;
            UpDownStandardRows.Value = _colorPicker.StandardColorGridRows;
            UpDownThemeRows.Value = _colorPicker.ThemeColorGridRows;
            CheckBoxAutoColor.Checked = _colorPicker.IsAutomaticColorButtonVisible;
            CheckBoxNoColor.Checked = _colorPicker.IsNoColorButtonVisible;
        }

        protected override Image SetImageSample()
        {
            return sample;
        }
    }
}
