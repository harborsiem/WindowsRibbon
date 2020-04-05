/*
Frame
    TBaseFrame (BaseClass), only Header + Image
        TFrameColumnBreak
        TFrameContextMenu
        TFrameControlGroup
        TFrameControlSizeDefintion
        TFrameGroupSizeDefinition
        TFrameMiniToolbar
        TFrameSizeDefinition
        TFrameViewRibbon

        TFrameCommandRefObject (BaseClass), CommandLabel + CommandCombo
             TFrameApplicationMenu
             TFrameAppMenuGroup
             TFrameCheckBox
             TFrameComboBox
             TFrameContextMap
             TFrameDropDownColorPicker
             TFrameFloatieFontControl
                 TFrameFontControl
             TFrameHelpButton
             TFrameMenuGroup
             TFrameQatControl
             TFrameQuickAccessToolbar
             TFrameScale
             TFrameSpinner
             TFrameTabGroup
             TFrameToggleButton

             TFrameControl (BaseClass), ApplicationModes
                 TFrameButton
                 TFrameDropDownButton

                 TFrameGallery  (BaseClass)             
                     TFrameDropDownGallery
                     TFrameInRibbonGallery
                     TFrameSplitButtonGallery

                 TFrameGroup    
                 TFrameSplitButton
                 TFrameTab

Views 13 Rows, 3 Columns
*/

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
    partial class BaseFrame : UserControl
    {
        private TRibbonObject _subject;
        private TreeNode _subjectNode;
        private bool _updating;
        protected ViewsFrame Owner;
        protected PictureBox ImageSample { get => _imageSample; }
        protected TableLayoutPanel LayoutPanel { get => _layoutPanel; }
        protected ToolStripLabel LabelHeader { get => _labelHeader; }

        public BaseFrame()
        {
            bool designtime = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
            if (designtime)
                InitializeComponent();
            else
            {
                InitComponentStep1();
                toolStrip1.ImageList = ImageManager.ImageListTreeView_Views(components);
                InitSuspend();
                InitComponentStep2();
                InitComponentStep3();
                InitResume();
            }
        }

        public void SetOwner(ViewsFrame owner)
        {
            Owner = owner;
        }

        protected virtual void InitComponentStep1()
        {
            if (components == null)
                components = new Container();
            this._imageSample = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._blanks = new System.Windows.Forms.ToolStripLabel();
            this._labelHeader = new System.Windows.Forms.ToolStripLabel();
            this._layoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._panel = new System.Windows.Forms.Panel();
        }

        protected virtual void InitSuspend()
        {
            ((System.ComponentModel.ISupportInitialize)(this._imageSample)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this._layoutPanel.SuspendLayout();
            this._panel.SuspendLayout();
            this.SuspendLayout();
        }

        protected virtual void InitComponentStep2()
        {
            // 
            // _imageSample
            // 
            //this._imageSample.BackColor = Color.Black;
            //this._imageSample.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._imageSample.Location = new System.Drawing.Point(382, 3);
            //this._imageSample.Margin = new System.Windows.Forms.Padding(0);
            this._imageSample.Name = "_imageSample";
            this._imageSample.Size = new System.Drawing.Size(105, 105);
            this._imageSample.TabIndex = 0;
            this._imageSample.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._blanks,
            this._labelHeader});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(498, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "";
            // 
            // _blanks
            // 
            this._blanks.Name = "_blanks";
            this._blanks.Size = new System.Drawing.Size(13, 22);
            this._blanks.Text = "  ";
            // 
            // _labelHeader
            // 
            this._labelHeader.Name = "_labelHeader";
            this._labelHeader.Size = new System.Drawing.Size(51, 22);
            this._labelHeader.Text = "  Header";
            // 
            // _layoutPanel
            // 
            this._layoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top)
            | System.Windows.Forms.AnchorStyles.Left)));
            this._layoutPanel.AutoSize = true;
            this._layoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            //this._layoutPanel.BackColor = Color.Beige;
            this._layoutPanel.ColumnCount = 4;
            this._layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this._layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this._layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F)); // 145F)); //@ ?
            this._layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this._layoutPanel.Location = new System.Drawing.Point(3, 3);
            this._layoutPanel.Name = "_layoutPanel";
            this._layoutPanel.RowCount = 14;
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._layoutPanel.Size = new System.Drawing.Size(376, 80);
            this._layoutPanel.TabIndex = 0;
            // 
            // _panel
            // 
            this._panel.AutoScroll = true;
            //this._panel.BackColor = Color.Bisque;
            this._panel.Controls.Add(this._layoutPanel);
            this._panel.Controls.Add(this._imageSample);
            this._panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panel.Location = new System.Drawing.Point(0, 25);
            this._panel.Name = "_panel";
            this._panel.Size = new System.Drawing.Size(498, 215);
            this._panel.TabIndex = 0;
        }

        protected virtual void InitComponentStep3()
        {
            // 
            // TBaseFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._panel);
            this.Controls.Add(this.toolStrip1);
            this.Name = "TBaseFrame";
            this.Size = new System.Drawing.Size(469, 240);
        }

        protected virtual void InitResume()
        {
            ((System.ComponentModel.ISupportInitialize)(this._imageSample)).EndInit();
            this._layoutPanel.ResumeLayout(false);
            this._layoutPanel.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this._panel.ResumeLayout(false);
            this._panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
            InitEvents();
            InitTooltips(components);
        }

        protected virtual void InitEvents()
        {
        }

        protected virtual void InitTooltips(IContainer components)
        {
        }

        protected TreeNode SubjectNode { get { return _subjectNode; } }

        public TRibbonObject Subject { get { return _subject; } }

        public void FrameResize(object sender, EventArgs e)
        {
            ImageSample.Left = Width - ImageSample.Width - 4;
        }

        protected virtual void Initialize(TRibbonObject subject)
        {
            this._subject = subject;
        }

        protected void Modified()
        {
            if (!_updating)
            {
                ((MainForm)FindForm()).Modified();
                UpdateCurrentNode();
            }
        }

        public void ShowProperties(TRibbonObject subject, TreeNode node)
        {
            _updating = true;
            try
            {
                _subjectNode = node;
                Initialize(subject);
            }
            finally
            {
                _updating = false;
            }
        }

        protected void UpdateCurrentNode()
        {
            ViewsFrame FrameViews;

            FrameViews = Owner as ViewsFrame;
            FrameViews.UpdateCurrentNode();
        }
    }
}
