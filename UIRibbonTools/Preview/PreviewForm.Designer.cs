namespace UIRibbonTools
{
    partial class PreviewForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewForm));
            this.ribbon = new RibbonLib.Ribbon();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabSheetAppModes = new System.Windows.Forms.TabPage();
            this.checkedListBoxAppModes = new System.Windows.Forms.CheckedListBox();
            this.labelAppModes = new System.Windows.Forms.Label();
            this.tabSheetContextTabs = new System.Windows.Forms.TabPage();
            this.checkedListBoxContextTabs = new System.Windows.Forms.CheckedListBox();
            this.labelContextTabs = new System.Windows.Forms.Label();
            this.tabSheetContextPopups = new System.Windows.Forms.TabPage();
            this.listBoxContextPopups = new System.Windows.Forms.ListBox();
            this.labelContextPopups = new System.Windows.Forms.Label();
            this.tabSheetColorize = new System.Windows.Forms.TabPage();
            this.setColorsButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.backgroundGroupBox1 = new System.Windows.Forms.GroupBox();
            this.backgroundLayout = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownB_R = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownB_G = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownB_B = new System.Windows.Forms.NumericUpDown();
            this.highlightGroup = new System.Windows.Forms.GroupBox();
            this.highlightLayout = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownH_R = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownH_G = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownH_B = new System.Windows.Forms.NumericUpDown();
            this.textColorGroup = new System.Windows.Forms.GroupBox();
            this.textLayout = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownT_R = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownT_G = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDownT_B = new System.Windows.Forms.NumericUpDown();
            this.backgroundColorPanel = new System.Windows.Forms.Panel();
            this.highlightColorPanel = new System.Windows.Forms.Panel();
            this.textColorPanel = new System.Windows.Forms.Panel();
            this.getColorsButton = new System.Windows.Forms.Button();
            this.backgroundButton = new System.Windows.Forms.Button();
            this.highlightButton = new System.Windows.Forms.Button();
            this.textButton = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabSheetAppModes.SuspendLayout();
            this.tabSheetContextTabs.SuspendLayout();
            this.tabSheetContextPopups.SuspendLayout();
            this.tabSheetColorize.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.backgroundGroupBox1.SuspendLayout();
            this.backgroundLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownB_R)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownB_G)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownB_B)).BeginInit();
            this.highlightGroup.SuspendLayout();
            this.highlightLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownH_R)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownH_G)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownH_B)).BeginInit();
            this.textColorGroup.SuspendLayout();
            this.textLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownT_R)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownT_G)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownT_B)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.Minimized = false;
            this.ribbon.Name = "ribbon";
            this.ribbon.ResourceName = null;
            this.ribbon.ShortcutTableResourceName = null;
            this.ribbon.Size = new System.Drawing.Size(800, 114);
            this.ribbon.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabSheetAppModes);
            this.tabControl.Controls.Add(this.tabSheetContextTabs);
            this.tabControl.Controls.Add(this.tabSheetContextPopups);
            this.tabControl.Controls.Add(this.tabSheetColorize);
            this.tabControl.Location = new System.Drawing.Point(0, 134);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 316);
            this.tabControl.TabIndex = 1;
            // 
            // tabSheetAppModes
            // 
            this.tabSheetAppModes.Controls.Add(this.checkedListBoxAppModes);
            this.tabSheetAppModes.Controls.Add(this.labelAppModes);
            this.tabSheetAppModes.Location = new System.Drawing.Point(4, 22);
            this.tabSheetAppModes.Name = "tabSheetAppModes";
            this.tabSheetAppModes.Padding = new System.Windows.Forms.Padding(3);
            this.tabSheetAppModes.Size = new System.Drawing.Size(792, 290);
            this.tabSheetAppModes.TabIndex = 0;
            this.tabSheetAppModes.Text = "Application Modes";
            this.tabSheetAppModes.UseVisualStyleBackColor = true;
            // 
            // checkedListBoxAppModes
            // 
            this.checkedListBoxAppModes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxAppModes.CheckOnClick = true;
            this.checkedListBoxAppModes.ColumnWidth = 200;
            this.checkedListBoxAppModes.FormattingEnabled = true;
            this.checkedListBoxAppModes.Location = new System.Drawing.Point(8, 19);
            this.checkedListBoxAppModes.MultiColumn = true;
            this.checkedListBoxAppModes.Name = "checkedListBoxAppModes";
            this.checkedListBoxAppModes.Size = new System.Drawing.Size(776, 259);
            this.checkedListBoxAppModes.TabIndex = 1;
            // 
            // labelAppModes
            // 
            this.labelAppModes.AutoSize = true;
            this.labelAppModes.Location = new System.Drawing.Point(9, 3);
            this.labelAppModes.Name = "labelAppModes";
            this.labelAppModes.Size = new System.Drawing.Size(201, 13);
            this.labelAppModes.TabIndex = 0;
            this.labelAppModes.Text = "* There are no application modes defined";
            // 
            // tabSheetContextTabs
            // 
            this.tabSheetContextTabs.Controls.Add(this.checkedListBoxContextTabs);
            this.tabSheetContextTabs.Controls.Add(this.labelContextTabs);
            this.tabSheetContextTabs.Location = new System.Drawing.Point(4, 22);
            this.tabSheetContextTabs.Name = "tabSheetContextTabs";
            this.tabSheetContextTabs.Padding = new System.Windows.Forms.Padding(3);
            this.tabSheetContextTabs.Size = new System.Drawing.Size(792, 290);
            this.tabSheetContextTabs.TabIndex = 1;
            this.tabSheetContextTabs.Text = "Contextual Tabs";
            this.tabSheetContextTabs.UseVisualStyleBackColor = true;
            // 
            // checkedListBoxContextTabs
            // 
            this.checkedListBoxContextTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxContextTabs.CheckOnClick = true;
            this.checkedListBoxContextTabs.ColumnWidth = 500;
            this.checkedListBoxContextTabs.FormattingEnabled = true;
            this.checkedListBoxContextTabs.Location = new System.Drawing.Point(8, 19);
            this.checkedListBoxContextTabs.MultiColumn = true;
            this.checkedListBoxContextTabs.Name = "checkedListBoxContextTabs";
            this.checkedListBoxContextTabs.Size = new System.Drawing.Size(776, 259);
            this.checkedListBoxContextTabs.TabIndex = 1;
            // 
            // labelContextTabs
            // 
            this.labelContextTabs.AutoSize = true;
            this.labelContextTabs.Location = new System.Drawing.Point(9, 3);
            this.labelContextTabs.Name = "labelContextTabs";
            this.labelContextTabs.Size = new System.Drawing.Size(188, 13);
            this.labelContextTabs.TabIndex = 0;
            this.labelContextTabs.Text = "* There are no contextual tabs defined";
            // 
            // tabSheetContextPopups
            // 
            this.tabSheetContextPopups.Controls.Add(this.listBoxContextPopups);
            this.tabSheetContextPopups.Controls.Add(this.labelContextPopups);
            this.tabSheetContextPopups.Location = new System.Drawing.Point(4, 22);
            this.tabSheetContextPopups.Name = "tabSheetContextPopups";
            this.tabSheetContextPopups.Size = new System.Drawing.Size(792, 290);
            this.tabSheetContextPopups.TabIndex = 2;
            this.tabSheetContextPopups.Text = "Context Popups";
            this.tabSheetContextPopups.UseVisualStyleBackColor = true;
            // 
            // listBoxContextPopups
            // 
            this.listBoxContextPopups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxContextPopups.FormattingEnabled = true;
            this.listBoxContextPopups.Location = new System.Drawing.Point(8, 19);
            this.listBoxContextPopups.Name = "listBoxContextPopups";
            this.listBoxContextPopups.Size = new System.Drawing.Size(776, 251);
            this.listBoxContextPopups.TabIndex = 1;
            // 
            // labelContextPopups
            // 
            this.labelContextPopups.AutoSize = true;
            this.labelContextPopups.Location = new System.Drawing.Point(9, 3);
            this.labelContextPopups.Name = "labelContextPopups";
            this.labelContextPopups.Size = new System.Drawing.Size(189, 13);
            this.labelContextPopups.TabIndex = 0;
            this.labelContextPopups.Text = "* There are no context popups defined";
            // 
            // tabSheetColorize
            // 
            this.tabSheetColorize.Controls.Add(this.setColorsButton);
            this.tabSheetColorize.Controls.Add(this.tableLayoutPanel1);
            this.tabSheetColorize.Controls.Add(this.getColorsButton);
            this.tabSheetColorize.Location = new System.Drawing.Point(4, 22);
            this.tabSheetColorize.Name = "tabSheetColorize";
            this.tabSheetColorize.Size = new System.Drawing.Size(792, 290);
            this.tabSheetColorize.TabIndex = 3;
            this.tabSheetColorize.Text = "Colorize";
            this.tabSheetColorize.UseVisualStyleBackColor = true;
            // 
            // setColorsButton
            // 
            this.setColorsButton.Location = new System.Drawing.Point(89, 180);
            this.setColorsButton.Name = "setColorsButton";
            this.setColorsButton.Size = new System.Drawing.Size(75, 23);
            this.setColorsButton.TabIndex = 2;
            this.setColorsButton.Text = "Set Colors";
            this.setColorsButton.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.textButton, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.highlightButton, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.backgroundGroupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.highlightGroup, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textColorGroup, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.backgroundColorPanel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.highlightColorPanel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.textColorPanel, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.backgroundButton, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(618, 171);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // backgroundGroupBox1
            // 
            this.backgroundGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.backgroundGroupBox1.Controls.Add(this.backgroundLayout);
            this.backgroundGroupBox1.Location = new System.Drawing.Point(3, 3);
            this.backgroundGroupBox1.Name = "backgroundGroupBox1";
            this.backgroundGroupBox1.Size = new System.Drawing.Size(200, 100);
            this.backgroundGroupBox1.TabIndex = 0;
            this.backgroundGroupBox1.TabStop = false;
            this.backgroundGroupBox1.Text = "BackgroundColor";
            // 
            // backgroundLayout
            // 
            this.backgroundLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.backgroundLayout.ColumnCount = 2;
            this.backgroundLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.backgroundLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.backgroundLayout.Controls.Add(this.label1, 0, 0);
            this.backgroundLayout.Controls.Add(this.numericUpDownB_R, 1, 0);
            this.backgroundLayout.Controls.Add(this.label2, 0, 1);
            this.backgroundLayout.Controls.Add(this.numericUpDownB_G, 1, 1);
            this.backgroundLayout.Controls.Add(this.label3, 0, 2);
            this.backgroundLayout.Controls.Add(this.numericUpDownB_B, 1, 2);
            this.backgroundLayout.Location = new System.Drawing.Point(3, 19);
            this.backgroundLayout.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.backgroundLayout.Name = "backgroundLayout";
            this.backgroundLayout.RowCount = 3;
            this.backgroundLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.backgroundLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.backgroundLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.backgroundLayout.Size = new System.Drawing.Size(194, 78);
            this.backgroundLayout.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Red";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDownB_R
            // 
            this.numericUpDownB_R.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownB_R.Location = new System.Drawing.Point(100, 3);
            this.numericUpDownB_R.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownB_R.Name = "numericUpDownB_R";
            this.numericUpDownB_R.Size = new System.Drawing.Size(91, 20);
            this.numericUpDownB_R.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "Green";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDownB_G
            // 
            this.numericUpDownB_G.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownB_G.Location = new System.Drawing.Point(100, 29);
            this.numericUpDownB_G.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownB_G.Name = "numericUpDownB_G";
            this.numericUpDownB_G.Size = new System.Drawing.Size(91, 20);
            this.numericUpDownB_G.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 26);
            this.label3.TabIndex = 3;
            this.label3.Text = "Blue";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDownB_B
            // 
            this.numericUpDownB_B.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownB_B.Location = new System.Drawing.Point(100, 55);
            this.numericUpDownB_B.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownB_B.Name = "numericUpDownB_B";
            this.numericUpDownB_B.Size = new System.Drawing.Size(91, 20);
            this.numericUpDownB_B.TabIndex = 6;
            // 
            // highlightGroup
            // 
            this.highlightGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.highlightGroup.Controls.Add(this.highlightLayout);
            this.highlightGroup.Location = new System.Drawing.Point(209, 3);
            this.highlightGroup.Name = "highlightGroup";
            this.highlightGroup.Size = new System.Drawing.Size(200, 100);
            this.highlightGroup.TabIndex = 1;
            this.highlightGroup.TabStop = false;
            this.highlightGroup.Text = "HighlightColor";
            // 
            // highlightLayout
            // 
            this.highlightLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.highlightLayout.ColumnCount = 2;
            this.highlightLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.highlightLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.highlightLayout.Controls.Add(this.label4, 0, 0);
            this.highlightLayout.Controls.Add(this.numericUpDownH_R, 1, 0);
            this.highlightLayout.Controls.Add(this.label5, 0, 1);
            this.highlightLayout.Controls.Add(this.numericUpDownH_G, 1, 1);
            this.highlightLayout.Controls.Add(this.label6, 0, 2);
            this.highlightLayout.Controls.Add(this.numericUpDownH_B, 1, 2);
            this.highlightLayout.Location = new System.Drawing.Point(3, 19);
            this.highlightLayout.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.highlightLayout.Name = "highlightLayout";
            this.highlightLayout.RowCount = 3;
            this.highlightLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.highlightLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.highlightLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.highlightLayout.Size = new System.Drawing.Size(194, 78);
            this.highlightLayout.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 26);
            this.label4.TabIndex = 1;
            this.label4.Text = "Red";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDownH_R
            // 
            this.numericUpDownH_R.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownH_R.Location = new System.Drawing.Point(100, 3);
            this.numericUpDownH_R.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownH_R.Name = "numericUpDownH_R";
            this.numericUpDownH_R.Size = new System.Drawing.Size(91, 20);
            this.numericUpDownH_R.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 26);
            this.label5.TabIndex = 2;
            this.label5.Text = "Green";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDownH_G
            // 
            this.numericUpDownH_G.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownH_G.Location = new System.Drawing.Point(100, 29);
            this.numericUpDownH_G.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownH_G.Name = "numericUpDownH_G";
            this.numericUpDownH_G.Size = new System.Drawing.Size(91, 20);
            this.numericUpDownH_G.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 26);
            this.label6.TabIndex = 3;
            this.label6.Text = "Blue";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDownH_B
            // 
            this.numericUpDownH_B.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownH_B.Location = new System.Drawing.Point(100, 55);
            this.numericUpDownH_B.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownH_B.Name = "numericUpDownH_B";
            this.numericUpDownH_B.Size = new System.Drawing.Size(91, 20);
            this.numericUpDownH_B.TabIndex = 6;
            // 
            // textColorGroup
            // 
            this.textColorGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textColorGroup.Controls.Add(this.textLayout);
            this.textColorGroup.Location = new System.Drawing.Point(415, 3);
            this.textColorGroup.Name = "textColorGroup";
            this.textColorGroup.Size = new System.Drawing.Size(200, 100);
            this.textColorGroup.TabIndex = 2;
            this.textColorGroup.TabStop = false;
            this.textColorGroup.Text = "TextColor";
            // 
            // textLayout
            // 
            this.textLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textLayout.ColumnCount = 2;
            this.textLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.textLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.textLayout.Controls.Add(this.label7, 0, 0);
            this.textLayout.Controls.Add(this.numericUpDownT_R, 1, 0);
            this.textLayout.Controls.Add(this.label8, 0, 1);
            this.textLayout.Controls.Add(this.numericUpDownT_G, 1, 1);
            this.textLayout.Controls.Add(this.label9, 0, 2);
            this.textLayout.Controls.Add(this.numericUpDownT_B, 1, 2);
            this.textLayout.Location = new System.Drawing.Point(3, 19);
            this.textLayout.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.textLayout.Name = "textLayout";
            this.textLayout.RowCount = 3;
            this.textLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.textLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.textLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.textLayout.Size = new System.Drawing.Size(194, 78);
            this.textLayout.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 26);
            this.label7.TabIndex = 1;
            this.label7.Text = "Red";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDownT_R
            // 
            this.numericUpDownT_R.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownT_R.Location = new System.Drawing.Point(100, 3);
            this.numericUpDownT_R.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownT_R.Name = "numericUpDownT_R";
            this.numericUpDownT_R.Size = new System.Drawing.Size(91, 20);
            this.numericUpDownT_R.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 26);
            this.label8.TabIndex = 2;
            this.label8.Text = "Green";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDownT_G
            // 
            this.numericUpDownT_G.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownT_G.Location = new System.Drawing.Point(100, 29);
            this.numericUpDownT_G.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownT_G.Name = "numericUpDownT_G";
            this.numericUpDownT_G.Size = new System.Drawing.Size(91, 20);
            this.numericUpDownT_G.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 26);
            this.label9.TabIndex = 3;
            this.label9.Text = "Blue";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDownT_B
            // 
            this.numericUpDownT_B.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownT_B.Location = new System.Drawing.Point(100, 55);
            this.numericUpDownT_B.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownT_B.Name = "numericUpDownT_B";
            this.numericUpDownT_B.Size = new System.Drawing.Size(91, 20);
            this.numericUpDownT_B.TabIndex = 6;
            // 
            // backgroundColorPanel
            // 
            this.backgroundColorPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.backgroundColorPanel.Location = new System.Drawing.Point(3, 121);
            this.backgroundColorPanel.Name = "backgroundColorPanel";
            this.backgroundColorPanel.Size = new System.Drawing.Size(200, 18);
            this.backgroundColorPanel.TabIndex = 3;
            // 
            // highlightColorPanel
            // 
            this.highlightColorPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.highlightColorPanel.Location = new System.Drawing.Point(209, 121);
            this.highlightColorPanel.Name = "highlightColorPanel";
            this.highlightColorPanel.Size = new System.Drawing.Size(200, 18);
            this.highlightColorPanel.TabIndex = 4;
            // 
            // textColorPanel
            // 
            this.textColorPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textColorPanel.Location = new System.Drawing.Point(415, 121);
            this.textColorPanel.Name = "textColorPanel";
            this.textColorPanel.Size = new System.Drawing.Size(200, 18);
            this.textColorPanel.TabIndex = 5;
            // 
            // getColorsButton
            // 
            this.getColorsButton.Location = new System.Drawing.Point(8, 180);
            this.getColorsButton.Name = "getColorsButton";
            this.getColorsButton.Size = new System.Drawing.Size(75, 23);
            this.getColorsButton.TabIndex = 0;
            this.getColorsButton.Text = "Get Colors";
            this.getColorsButton.UseVisualStyleBackColor = true;
            // 
            // backgroundButton
            // 
            this.backgroundButton.AutoSize = true;
            this.backgroundButton.Location = new System.Drawing.Point(3, 145);
            this.backgroundButton.Name = "backgroundButton";
            this.backgroundButton.Size = new System.Drawing.Size(94, 23);
            this.backgroundButton.TabIndex = 6;
            this.backgroundButton.Text = "Set Background";
            this.backgroundButton.UseVisualStyleBackColor = true;
            // 
            // highlightButton
            // 
            this.highlightButton.AutoSize = true;
            this.highlightButton.Location = new System.Drawing.Point(209, 145);
            this.highlightButton.Name = "highlightButton";
            this.highlightButton.Size = new System.Drawing.Size(94, 23);
            this.highlightButton.TabIndex = 7;
            this.highlightButton.Text = "Set Highlight";
            this.highlightButton.UseVisualStyleBackColor = true;
            // 
            // textButton
            // 
            this.textButton.AutoSize = true;
            this.textButton.Location = new System.Drawing.Point(415, 145);
            this.textButton.Name = "textButton";
            this.textButton.Size = new System.Drawing.Size(94, 23);
            this.textButton.TabIndex = 8;
            this.textButton.Text = "Set Text";
            this.textButton.UseVisualStyleBackColor = true;
            // 
            // PreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 462);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PreviewForm";
            this.ShowInTaskbar = false;
            this.Text = "Ribbon Preview";
            this.tabControl.ResumeLayout(false);
            this.tabSheetAppModes.ResumeLayout(false);
            this.tabSheetAppModes.PerformLayout();
            this.tabSheetContextTabs.ResumeLayout(false);
            this.tabSheetContextTabs.PerformLayout();
            this.tabSheetContextPopups.ResumeLayout(false);
            this.tabSheetContextPopups.PerformLayout();
            this.tabSheetColorize.ResumeLayout(false);
            this.tabSheetColorize.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.backgroundGroupBox1.ResumeLayout(false);
            this.backgroundLayout.ResumeLayout(false);
            this.backgroundLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownB_R)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownB_G)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownB_B)).EndInit();
            this.highlightGroup.ResumeLayout(false);
            this.highlightLayout.ResumeLayout(false);
            this.highlightLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownH_R)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownH_G)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownH_B)).EndInit();
            this.textColorGroup.ResumeLayout(false);
            this.textLayout.ResumeLayout(false);
            this.textLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownT_R)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownT_G)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownT_B)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private RibbonLib.Ribbon ribbon;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabSheetAppModes;
        private System.Windows.Forms.CheckedListBox checkedListBoxAppModes;
        private System.Windows.Forms.Label labelAppModes;
        private System.Windows.Forms.TabPage tabSheetContextTabs;
        private System.Windows.Forms.TabPage tabSheetContextPopups;
        private System.Windows.Forms.TabPage tabSheetColorize;
        private System.Windows.Forms.CheckedListBox checkedListBoxContextTabs;
        private System.Windows.Forms.Label labelContextTabs;
        private System.Windows.Forms.Label labelContextPopups;
        private System.Windows.Forms.ListBox listBoxContextPopups;
        private System.Windows.Forms.Button getColorsButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox textColorGroup;
        private System.Windows.Forms.TableLayoutPanel textLayout;
        private System.Windows.Forms.NumericUpDown numericUpDownT_B;
        private System.Windows.Forms.NumericUpDown numericUpDownT_G;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDownT_R;
        private System.Windows.Forms.GroupBox highlightGroup;
        private System.Windows.Forms.TableLayoutPanel highlightLayout;
        private System.Windows.Forms.NumericUpDown numericUpDownH_B;
        private System.Windows.Forms.NumericUpDown numericUpDownH_G;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownH_R;
        private System.Windows.Forms.GroupBox backgroundGroupBox1;
        private System.Windows.Forms.TableLayoutPanel backgroundLayout;
        private System.Windows.Forms.NumericUpDown numericUpDownB_B;
        private System.Windows.Forms.NumericUpDown numericUpDownB_G;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownB_R;
        private System.Windows.Forms.Panel textColorPanel;
        private System.Windows.Forms.Panel highlightColorPanel;
        private System.Windows.Forms.Panel backgroundColorPanel;
        private System.Windows.Forms.Button setColorsButton;
        private System.Windows.Forms.Button textButton;
        private System.Windows.Forms.Button highlightButton;
        private System.Windows.Forms.Button backgroundButton;
    }
}

