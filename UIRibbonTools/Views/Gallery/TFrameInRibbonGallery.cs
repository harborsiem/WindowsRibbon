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
    partial class TFrameInRibbonGallery : TFrameGallery
    {
        private Label Label9 { get => _label9; }
        private NumericUpDown UpDownMinColumnsLarge { get => _upDownMinColumnsLarge; }
        private Label Label10 { get => _label10; }
        private Label Label11 { get => _label11; }
        private NumericUpDown UpDownMinColumnsMedium { get => _upDownMinColumnsMedium; }
        private Label Label12 { get => _label12; }
        private Label Label13 { get => _label13; }
        private NumericUpDown UpDownMaxColumnsMedium { get => _upDownMaxColumnsMedium; }
        private Label Label14 { get => _label14; }
        private Label Label15 { get => _label15; }
        private NumericUpDown UpDownMaxColumns { get => _upDownMaxColumns; }
        private Label Label16 { get => _label16; }
        private Label Label17 { get => _label17; }
        private NumericUpDown UpDownMaxRows { get => _upDownMaxRows; }
        private Label Label18 { get => _label18; }

        private TRibbonInRibbonGallery _inRibbonGallery;

        public TFrameInRibbonGallery()
        {
            bool designtime = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
            if (designtime)
                InitializeComponent();
        }

        protected override void InitComponentStep1()
        {
            if (components == null)
                components = new Container();
            this._label9 = new System.Windows.Forms.Label();
            this._upDownMinColumnsLarge = new System.Windows.Forms.NumericUpDown();
            this._label10 = new System.Windows.Forms.Label();
            this._label11 = new System.Windows.Forms.Label();
            this._upDownMinColumnsMedium = new System.Windows.Forms.NumericUpDown();
            this._label12 = new System.Windows.Forms.Label();
            this._label13 = new System.Windows.Forms.Label();
            this._upDownMaxColumnsMedium = new System.Windows.Forms.NumericUpDown();
            this._label14 = new System.Windows.Forms.Label();
            this._label15 = new System.Windows.Forms.Label();
            this._upDownMaxColumns = new System.Windows.Forms.NumericUpDown();
            this._label16 = new System.Windows.Forms.Label();
            this._label17 = new System.Windows.Forms.Label();
            this._upDownMaxRows = new System.Windows.Forms.NumericUpDown();
            this._label18 = new System.Windows.Forms.Label();
            base.InitComponentStep1();
        }

        protected override void InitSuspend()
        {
            ((System.ComponentModel.ISupportInitialize)(this._upDownMinColumnsLarge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._upDownMinColumnsMedium)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._upDownMaxColumnsMedium)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._upDownMaxColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._upDownMaxRows)).BeginInit();

            base.InitSuspend();
        }

        protected override void InitComponentStep2()
        {
            base.InitComponentStep2();
            // 
            // _label9
            // 
            this._label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label9.AutoSize = true;
            this._label9.Location = new System.Drawing.Point(3, 330);
            this._label9.Name = "_label9";
            this._label9.Size = new System.Drawing.Size(97, 26);
            this._label9.TabIndex = 0;
            this._label9.Text = "Min Columns Large";
            this._label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _upDownMinColumnsLarge
            // 
            this._upDownMinColumnsLarge.Location = new System.Drawing.Point(123, 333);
            this._upDownMinColumnsLarge.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this._upDownMinColumnsLarge.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this._upDownMinColumnsLarge.Name = "_upDownMinColumnsLarge";
            this._upDownMinColumnsLarge.Size = new System.Drawing.Size(81, 20);
            this._upDownMinColumnsLarge.TabIndex = 11;
            this._upDownMinColumnsLarge.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // _label10
            // 
            this._label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label10.AutoSize = true;
            this.LayoutPanel.SetColumnSpan(this._label10, 2);
            this._label10.Location = new System.Drawing.Point(210, 330);
            this._label10.Name = "_label10";
            this._label10.Size = new System.Drawing.Size(77, 26);
            this._label10.TabIndex = 0;
            this._label10.Text = "Use -1 for auto";
            this._label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _label11
            // 
            this._label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label11.AutoSize = true;
            this._label11.Location = new System.Drawing.Point(3, 356);
            this._label11.Name = "_label11";
            this._label11.Size = new System.Drawing.Size(107, 26);
            this._label11.TabIndex = 0;
            this._label11.Text = "Min Columns Medium";
            this._label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _upDownMinColumnsMedium
            // 
            this._upDownMinColumnsMedium.Location = new System.Drawing.Point(123, 359);
            this._upDownMinColumnsMedium.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this._upDownMinColumnsMedium.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this._upDownMinColumnsMedium.Name = "_upDownMinColumnsMedium";
            this._upDownMinColumnsMedium.Size = new System.Drawing.Size(81, 20);
            this._upDownMinColumnsMedium.TabIndex = 12;
            this._upDownMinColumnsMedium.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // _label12
            // 
            this._label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label12.AutoSize = true;
            this.LayoutPanel.SetColumnSpan(this._label12, 2);
            this._label12.Location = new System.Drawing.Point(210, 356);
            this._label12.Name = "_label12";
            this._label12.Size = new System.Drawing.Size(146, 26);
            this._label12.TabIndex = 0;
            this._label12.Text = "Use -1 for auto/default height";
            this._label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _label13
            // 
            this._label13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label13.AutoSize = true;
            this._label13.Location = new System.Drawing.Point(3, 382);
            this._label13.Name = "_label13";
            this._label13.Size = new System.Drawing.Size(110, 26);
            this._label13.TabIndex = 0;
            this._label13.Text = "Max Columns Medium";
            this._label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _upDownMaxColumnsMedium
            // 
            this._upDownMaxColumnsMedium.Location = new System.Drawing.Point(123, 385);
            this._upDownMaxColumnsMedium.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this._upDownMaxColumnsMedium.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this._upDownMaxColumnsMedium.Name = "_upDownMaxColumnsMedium";
            this._upDownMaxColumnsMedium.Size = new System.Drawing.Size(81, 20);
            this._upDownMaxColumnsMedium.TabIndex = 13;
            this._upDownMaxColumnsMedium.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // _label14
            // 
            this._label14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label14.AutoSize = true;
            this.LayoutPanel.SetColumnSpan(this._label14, 2);
            this._label14.Location = new System.Drawing.Point(210, 382);
            this._label14.Name = "_label14";
            this._label14.Size = new System.Drawing.Size(142, 26);
            this._label14.TabIndex = 0;
            this._label14.Text = "Use -1 for auto/default width";
            this._label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _label15
            // 
            this._label15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label15.AutoSize = true;
            this._label15.Location = new System.Drawing.Point(3, 408);
            this._label15.Name = "_label15";
            this._label15.Size = new System.Drawing.Size(70, 26);
            this._label15.TabIndex = 0;
            this._label15.Text = "Max Columns";
            this._label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _upDownMaxColumns
            // 
            this._upDownMaxColumns.Location = new System.Drawing.Point(123, 411);
            this._upDownMaxColumns.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this._upDownMaxColumns.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this._upDownMaxColumns.Name = "_upDownMaxColumns";
            this._upDownMaxColumns.Size = new System.Drawing.Size(81, 20);
            this._upDownMaxColumns.TabIndex = 14;
            this._upDownMaxColumns.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // _label16
            // 
            this._label16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label16.AutoSize = true;
            this.LayoutPanel.SetColumnSpan(this._label16, 2);
            this._label16.Location = new System.Drawing.Point(210, 408);
            this._label16.Name = "_label16";
            this._label16.Size = new System.Drawing.Size(146, 26);
            this._label16.TabIndex = 0;
            this._label16.Text = "Use -1 for auto/default height";
            this._label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _label17
            // 
            this._label17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label17.AutoSize = true;
            this._label17.Location = new System.Drawing.Point(3, 434);
            this._label17.Name = "_label17";
            this._label17.Size = new System.Drawing.Size(57, 26);
            this._label17.TabIndex = 0;
            this._label17.Text = "Max Rows";
            this._label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _upDownMaxRows
            // 
            this._upDownMaxRows.Location = new System.Drawing.Point(123, 437);
            this._upDownMaxRows.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this._upDownMaxRows.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this._upDownMaxRows.Name = "_upDownMaxRows";
            this._upDownMaxRows.Size = new System.Drawing.Size(81, 20);
            this._upDownMaxRows.TabIndex = 15;
            this._upDownMaxRows.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // _label18
            // 
            this._label18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._label18.AutoSize = true;
            this.LayoutPanel.SetColumnSpan(this._label18, 2);
            this._label18.Location = new System.Drawing.Point(210, 434);
            this._label18.Name = "_label18";
            this._label18.Size = new System.Drawing.Size(142, 26);
            this._label18.TabIndex = 0;
            this._label18.Text = "Use -1 for auto/default width";
            this._label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            LabelHeader.Text = "  InRibbonGallery Properties";
            LabelHeader.ImageIndex = 17;
        }

        protected override void InitComponentStep3()
        {
            this.LayoutPanel.Controls.Add(this._label9, 0, 8);
            this.LayoutPanel.Controls.Add(this._upDownMinColumnsLarge, 1, 8);
            this.LayoutPanel.Controls.Add(this._label10, 2, 8);
            this.LayoutPanel.Controls.Add(this._label11, 0, 9);
            this.LayoutPanel.Controls.Add(this._upDownMinColumnsMedium, 1, 9);
            this.LayoutPanel.Controls.Add(this._label12, 2, 9);
            this.LayoutPanel.Controls.Add(this._label13, 0, 10);
            this.LayoutPanel.Controls.Add(this._upDownMaxColumnsMedium, 1, 10);
            this.LayoutPanel.Controls.Add(this._label14, 2, 10);
            this.LayoutPanel.Controls.Add(this._label15, 0, 11);
            this.LayoutPanel.Controls.Add(this._upDownMaxColumns, 1, 11);
            this.LayoutPanel.Controls.Add(this._label16, 2, 11);
            this.LayoutPanel.Controls.Add(this._label17, 0, 12);
            this.LayoutPanel.Controls.Add(this._upDownMaxRows, 1, 12);
            this.LayoutPanel.Controls.Add(this._label18, 2, 12);

            base.InitComponentStep3();
        }

        protected override void InitResume()
        {
            ((System.ComponentModel.ISupportInitialize)(this._upDownMinColumnsLarge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._upDownMinColumnsMedium)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._upDownMaxColumnsMedium)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._upDownMaxColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._upDownMaxRows)).EndInit();

            base.InitResume();
        }

        protected override void InitEvents()
        {
            base.InitEvents();
            UpDownMinColumnsLarge.ValueChanged += EditMinColumnsLargeChange;
            UpDownMinColumnsMedium.ValueChanged += EditMinColumnsMediumChange;
            UpDownMaxColumnsMedium.ValueChanged += EditMaxColumnsMediumChange;
            UpDownMaxColumns.ValueChanged += EditMaxColumnsChange;
            UpDownMaxRows.ValueChanged += EditMaxRowsChange;
        }

        protected override void InitTooltips(IContainer components)
        {
            base.InitTooltips(components);
            new ToolTip(components).SetToolTip(UpDownMinColumnsLarge,
                "Minimum number of columns that the gallery displays in" + Environment.NewLine +
                "Large group layout, before switching to Medium");
            new ToolTip(components).SetToolTip(UpDownMinColumnsMedium,
                "Minimum number of columns that the gallery displays in" + Environment.NewLine +
                "Medium group layout, before switching to Small");
            new ToolTip(components).SetToolTip(UpDownMaxColumnsMedium,
                "Maximum number of columns that the gallery displays in" + Environment.NewLine +
                "Medium group layout, before switching to Large");
            new ToolTip(components).SetToolTip(UpDownMaxColumns, "Maximum number of columns that the gallery displays");
            new ToolTip(components).SetToolTip(UpDownMaxRows, "Maximum number of rows that the gallery displays");
        }

        private void EditMaxColumnsChange(object sender, EventArgs e)
        {
            if (UpDownMaxColumns.Value != _inRibbonGallery.MaxColumns)
            {
                _inRibbonGallery.MaxColumns = (int)UpDownMaxColumns.Value;
                Modified();
            }
        }

        private void EditMaxColumnsMediumChange(object sender, EventArgs e)
        {
            if (UpDownMaxColumnsMedium.Value != _inRibbonGallery.MaxColumnsMedium)
            {
                _inRibbonGallery.MaxColumnsMedium = (int)UpDownMaxColumnsMedium.Value;
                Modified();
            }
        }

        private void EditMaxRowsChange(object sender, EventArgs e)
        {
            if (UpDownMaxRows.Value != _inRibbonGallery.MaxRows)
            {
                _inRibbonGallery.MaxRows = (int)UpDownMaxRows.Value;
                Modified();
            }
        }

        private void EditMinColumnsLargeChange(object sender, EventArgs e)
        {
            if (UpDownMinColumnsLarge.Value != _inRibbonGallery.MinColumnsLarge)
            {
                _inRibbonGallery.MinColumnsLarge = (int)UpDownMinColumnsLarge.Value;
                Modified();
            }
        }

        private void EditMinColumnsMediumChange(object sender, EventArgs e)
        {
            if (UpDownMinColumnsMedium.Value != _inRibbonGallery.MinColumnsMedium)
            {
                _inRibbonGallery.MinColumnsMedium = (int)UpDownMinColumnsMedium.Value;
                Modified();
            }
        }

        protected override void Initialize(TRibbonObject subject)
        {
            base.Initialize(subject);
            _inRibbonGallery = subject as TRibbonInRibbonGallery;
            UpDownMinColumnsLarge.Value = _inRibbonGallery.MinColumnsLarge;
            UpDownMinColumnsMedium.Value = _inRibbonGallery.MinColumnsMedium;
            UpDownMaxColumnsMedium.Value = _inRibbonGallery.MaxColumnsMedium;
            UpDownMaxColumns.Value = _inRibbonGallery.MaxColumns;
            UpDownMaxRows.Value = _inRibbonGallery.MaxRows;
        }
    }
}
