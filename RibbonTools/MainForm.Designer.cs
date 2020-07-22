namespace UIRibbonTools
{
    partial class MainForm
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
            if (disposing)
            {
                Destroy();
            }
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this._nN1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this._nN3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this._nN2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProject = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBuild = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.autoGenerateIdsForAllResources = new System.Windows.Forms.ToolStripMenuItem();
            this.autoGenerateIdsForAllCommands = new System.Windows.Forms.ToolStripMenuItem();
            this.setresourcename = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTutorial = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWebSite = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDotnetWebSite = new System.Windows.Forms.ToolStripMenuItem();
            this._nN4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuMSDN = new System.Windows.Forms.ToolStripMenuItem();
            this.toolVersion = new System.Windows.Forms.ToolStripTextBox();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.toolButtonSave = new System.Windows.Forms.ToolStripButton();
            this._nN5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolButtonBuild = new System.Windows.Forms.ToolStripButton();
            this.toolButtonPreview = new System.Windows.Forms.ToolStripButton();
            this.toolPreviewLanguageCombo = new System.Windows.Forms.ToolStripComboBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusModified = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusHints = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabPageCommands = new System.Windows.Forms.TabPage();
            this._commandsFrame = new UIRibbonTools.CommandsFrame();
            this.tabPageViews = new System.Windows.Forms.TabPage();
            this._viewsFrame = new UIRibbonTools.ViewsFrame();
            this.tabPageXmlSource = new System.Windows.Forms.TabPage();
            this._xmlSourceFrame = new UIRibbonTools.XmlSourceFrame();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.memoMessages = new System.Windows.Forms.TextBox();
            this.splitterLog = new System.Windows.Forms.SplitContainer();
            this._timerRestoreLog = new System.Windows.Forms.Timer(this.components);
            this.mainMenuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.tabPageCommands.SuspendLayout();
            this.tabPageViews.SuspendLayout();
            this.tabPageXmlSource.SuspendLayout();
            this.tabControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitterLog)).BeginInit();
            this.splitterLog.Panel1.SuspendLayout();
            this.splitterLog.Panel2.SuspendLayout();
            this.splitterLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuProject,
            this.menuHelp});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(905, 24);
            this.mainMenuStrip.TabIndex = 0;
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNew,
            this.menuOpen,
            this._nN1,
            this.menuSave,
            this.menuSaveAs,
            this._nN3,
            this.menuSettings,
            this._nN2,
            this.menuExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(37, 20);
            this.menuFile.Text = "File";
            // 
            // menuNew
            // 
            this.menuNew.Name = "menuNew";
            this.menuNew.Size = new System.Drawing.Size(116, 22);
            this.menuNew.Text = "New";
            // 
            // menuOpen
            // 
            this.menuOpen.Name = "menuOpen";
            this.menuOpen.Size = new System.Drawing.Size(116, 22);
            this.menuOpen.Text = "Open";
            // 
            // _nN1
            // 
            this._nN1.Name = "_nN1";
            this._nN1.Size = new System.Drawing.Size(113, 6);
            // 
            // menuSave
            // 
            this.menuSave.Name = "menuSave";
            this.menuSave.Size = new System.Drawing.Size(116, 22);
            this.menuSave.Text = "Save";
            // 
            // menuSaveAs
            // 
            this.menuSaveAs.Name = "menuSaveAs";
            this.menuSaveAs.Size = new System.Drawing.Size(116, 22);
            this.menuSaveAs.Text = "Save as";
            // 
            // _nN3
            // 
            this._nN3.Name = "_nN3";
            this._nN3.Size = new System.Drawing.Size(113, 6);
            // 
            // menuSettings
            // 
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.Size = new System.Drawing.Size(116, 22);
            this.menuSettings.Text = "Settings";
            // 
            // _nN2
            // 
            this._nN2.Name = "_nN2";
            this._nN2.Size = new System.Drawing.Size(113, 6);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(116, 22);
            this.menuExit.Text = "Exit";
            // 
            // menuProject
            // 
            this.menuProject.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuBuild,
            this.menuPreview,
            this.autoGenerateIdsForAllResources,
            this.autoGenerateIdsForAllCommands,
            this.setresourcename});
            this.menuProject.Name = "menuProject";
            this.menuProject.Size = new System.Drawing.Size(56, 20);
            this.menuProject.Text = "Project";
            // 
            // menuBuild
            // 
            this.menuBuild.Name = "menuBuild";
            this.menuBuild.Size = new System.Drawing.Size(264, 22);
            this.menuBuild.Text = "Build";
            // 
            // menuPreview
            // 
            this.menuPreview.Name = "menuPreview";
            this.menuPreview.Size = new System.Drawing.Size(264, 22);
            this.menuPreview.Text = "Preview";
            // 
            // autoGenerateIdsForAllResources
            // 
            this.autoGenerateIdsForAllResources.Name = "autoGenerateIdsForAllResources";
            this.autoGenerateIdsForAllResources.Size = new System.Drawing.Size(264, 22);
            this.autoGenerateIdsForAllResources.Text = "Auto generate IDs for all resources";
            // 
            // autoGenerateIdsForAllCommands
            // 
            this.autoGenerateIdsForAllCommands.Name = "autoGenerateIdsForAllCommands";
            this.autoGenerateIdsForAllCommands.Size = new System.Drawing.Size(264, 22);
            this.autoGenerateIdsForAllCommands.Text = "Auto generate IDs for all commands";
            // 
            // setresourcename
            // 
            this.setresourcename.Name = "setresourcename";
            this.setresourcename.Size = new System.Drawing.Size(264, 22);
            this.setresourcename.Text = "Set Ribbon resource name";
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuTutorial,
            this.menuWebSite,
            this.menuDotnetWebSite,
            this._nN4,
            this.menuMSDN,
            this.toolVersion});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(44, 20);
            this.menuHelp.Text = "Help";
            // 
            // menuTutorial
            // 
            this.menuTutorial.Name = "menuTutorial";
            this.menuTutorial.Size = new System.Drawing.Size(180, 22);
            this.menuTutorial.Text = "Tutorial";
            // 
            // menuWebSite
            // 
            this.menuWebSite.Name = "menuWebSite";
            this.menuWebSite.Size = new System.Drawing.Size(180, 22);
            this.menuWebSite.Text = "WebSite";
            // 
            // menuDotnetWebSite
            // 
            this.menuDotnetWebSite.Name = "menuDotnetWebSite";
            this.menuDotnetWebSite.Size = new System.Drawing.Size(180, 22);
            this.menuDotnetWebSite.Text = "WebSite";
            // 
            // _nN4
            // 
            this._nN4.Name = "_nN4";
            this._nN4.Size = new System.Drawing.Size(177, 6);
            // 
            // menuMSDN
            // 
            this.menuMSDN.Name = "menuMSDN";
            this.menuMSDN.Size = new System.Drawing.Size(180, 22);
            this.menuMSDN.Text = "MSDN";
            // 
            // toolVersion
            // 
            this.toolVersion.Name = "toolVersion";
            this.toolVersion.ReadOnly = true;
            this.toolVersion.Size = new System.Drawing.Size(100, 23);
            this.toolVersion.Text = "Version: 1.1.0.0";
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolButtonOpen,
            this.toolButtonSave,
            this._nN5,
            this.toolButtonBuild,
            this.toolButtonPreview,
            this.toolPreviewLanguageCombo});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(905, 25);
            this.toolStrip.TabIndex = 1;
            // 
            // toolButtonOpen
            // 
            this.toolButtonOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolButtonOpen.Image")));
            this.toolButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolButtonOpen.Name = "toolButtonOpen";
            this.toolButtonOpen.Size = new System.Drawing.Size(56, 22);
            this.toolButtonOpen.Text = "Open";
            // 
            // toolButtonSave
            // 
            this.toolButtonSave.Image = ((System.Drawing.Image)(resources.GetObject("toolButtonSave.Image")));
            this.toolButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolButtonSave.Name = "toolButtonSave";
            this.toolButtonSave.Size = new System.Drawing.Size(51, 22);
            this.toolButtonSave.Text = "Save";
            // 
            // _nN5
            // 
            this._nN5.Name = "_nN5";
            this._nN5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolButtonBuild
            // 
            this.toolButtonBuild.Image = ((System.Drawing.Image)(resources.GetObject("toolButtonBuild.Image")));
            this.toolButtonBuild.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolButtonBuild.Name = "toolButtonBuild";
            this.toolButtonBuild.Size = new System.Drawing.Size(54, 22);
            this.toolButtonBuild.Text = "Build";
            // 
            // toolButtonPreview
            // 
            this.toolButtonPreview.Image = ((System.Drawing.Image)(resources.GetObject("toolButtonPreview.Image")));
            this.toolButtonPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolButtonPreview.Name = "toolButtonPreview";
            this.toolButtonPreview.Size = new System.Drawing.Size(68, 22);
            this.toolButtonPreview.Text = "Preview";
            // 
            // toolPreviewLanguageCombo
            // 
            this.toolPreviewLanguageCombo.AutoToolTip = true;
            this.toolPreviewLanguageCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolPreviewLanguageCombo.Items.AddRange(new object[] {
            "Neutral"});
            this.toolPreviewLanguageCombo.Name = "toolPreviewLanguageCombo";
            this.toolPreviewLanguageCombo.Size = new System.Drawing.Size(80, 25);
            this.toolPreviewLanguageCombo.ToolTipText = "Preview Language";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusModified,
            this.statusHints});
            this.statusStrip.Location = new System.Drawing.Point(0, 619);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(905, 22);
            this.statusStrip.TabIndex = 3;
            // 
            // statusModified
            // 
            this.statusModified.AutoSize = false;
            this.statusModified.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statusModified.Name = "statusModified";
            this.statusModified.Size = new System.Drawing.Size(60, 17);
            // 
            // statusHints
            // 
            this.statusHints.Name = "statusHints";
            this.statusHints.Size = new System.Drawing.Size(0, 17);
            // 
            // tabPageCommands
            // 
            this.tabPageCommands.Controls.Add(this._commandsFrame);
            this.tabPageCommands.Location = new System.Drawing.Point(4, 22);
            this.tabPageCommands.Name = "tabPageCommands";
            this.tabPageCommands.Size = new System.Drawing.Size(897, 484);
            this.tabPageCommands.TabIndex = 0;
            this.tabPageCommands.Text = "Commands";
            this.tabPageCommands.UseVisualStyleBackColor = true;
            // 
            // _commandsFrame
            // 
            this._commandsFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this._commandsFrame.Location = new System.Drawing.Point(0, 0);
            this._commandsFrame.Name = "_commandsFrame";
            this._commandsFrame.Size = new System.Drawing.Size(897, 484);
            this._commandsFrame.TabIndex = 0;
            // 
            // tabPageViews
            // 
            this.tabPageViews.Controls.Add(this._viewsFrame);
            this.tabPageViews.Location = new System.Drawing.Point(4, 22);
            this.tabPageViews.Name = "tabPageViews";
            this.tabPageViews.Size = new System.Drawing.Size(897, 484);
            this.tabPageViews.TabIndex = 1;
            this.tabPageViews.Text = "Views";
            this.tabPageViews.UseVisualStyleBackColor = true;
            // 
            // _viewsFrame
            // 
            this._viewsFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this._viewsFrame.Location = new System.Drawing.Point(0, 0);
            this._viewsFrame.Name = "_viewsFrame";
            this._viewsFrame.Size = new System.Drawing.Size(897, 484);
            this._viewsFrame.TabIndex = 0;
            // 
            // tabPageXmlSource
            // 
            this.tabPageXmlSource.Controls.Add(this._xmlSourceFrame);
            this.tabPageXmlSource.Location = new System.Drawing.Point(4, 22);
            this.tabPageXmlSource.Name = "tabPageXmlSource";
            this.tabPageXmlSource.Size = new System.Drawing.Size(897, 484);
            this.tabPageXmlSource.TabIndex = 2;
            this.tabPageXmlSource.Text = "XML Source";
            this.tabPageXmlSource.UseVisualStyleBackColor = true;
            // 
            // _xmlSourceFrame
            // 
            this._xmlSourceFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this._xmlSourceFrame.Location = new System.Drawing.Point(0, 0);
            this._xmlSourceFrame.Name = "_xmlSourceFrame";
            this._xmlSourceFrame.Size = new System.Drawing.Size(897, 484);
            this._xmlSourceFrame.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageCommands);
            this.tabControl.Controls.Add(this.tabPageViews);
            this.tabControl.Controls.Add(this.tabPageXmlSource);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(905, 510);
            this.tabControl.TabIndex = 0;
            // 
            // memoMessages
            // 
            this.memoMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoMessages.Location = new System.Drawing.Point(0, 0);
            this.memoMessages.Multiline = true;
            this.memoMessages.Name = "memoMessages";
            this.memoMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.memoMessages.Size = new System.Drawing.Size(905, 56);
            this.memoMessages.TabIndex = 0;
            // 
            // splitterLog
            // 
            this.splitterLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitterLog.Location = new System.Drawing.Point(0, 49);
            this.splitterLog.Name = "splitterLog";
            this.splitterLog.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitterLog.Panel1
            // 
            this.splitterLog.Panel1.Controls.Add(this.tabControl);
            // 
            // splitterLog.Panel2
            // 
            this.splitterLog.Panel2.Controls.Add(this.memoMessages);
            this.splitterLog.Size = new System.Drawing.Size(905, 570);
            this.splitterLog.SplitterDistance = 510;
            this.splitterLog.TabIndex = 2;
            // 
            // _timerRestoreLog
            // 
            this._timerRestoreLog.Interval = 3000;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 641);
            this.Controls.Add(this.splitterLog);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.MinimumSize = new System.Drawing.Size(921, 680);
            this.Name = "MainForm";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabPageCommands.ResumeLayout(false);
            this.tabPageViews.ResumeLayout(false);
            this.tabPageXmlSource.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.splitterLog.Panel1.ResumeLayout(false);
            this.splitterLog.Panel2.ResumeLayout(false);
            this.splitterLog.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitterLog)).EndInit();
            this.splitterLog.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuNew;
        private System.Windows.Forms.ToolStripMenuItem menuOpen;
        private System.Windows.Forms.ToolStripSeparator _nN1;
        private System.Windows.Forms.ToolStripMenuItem menuSave;
        private System.Windows.Forms.ToolStripMenuItem menuSaveAs;
        private System.Windows.Forms.ToolStripSeparator _nN3;
        private System.Windows.Forms.ToolStripMenuItem menuSettings;
        private System.Windows.Forms.ToolStripSeparator _nN2;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripMenuItem menuProject;
        private System.Windows.Forms.ToolStripMenuItem menuBuild;
        private System.Windows.Forms.ToolStripMenuItem menuPreview;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuTutorial;
        private System.Windows.Forms.ToolStripMenuItem menuWebSite;
        private System.Windows.Forms.ToolStripMenuItem menuDotnetWebSite;
        private System.Windows.Forms.ToolStripSeparator _nN4;
        private System.Windows.Forms.ToolStripMenuItem menuMSDN;
        private System.Windows.Forms.ToolStripMenuItem autoGenerateIdsForAllResources;
        private System.Windows.Forms.ToolStripMenuItem autoGenerateIdsForAllCommands;
        private System.Windows.Forms.ToolStripMenuItem setresourcename;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolButtonOpen;
        private System.Windows.Forms.ToolStripButton toolButtonSave;
        private System.Windows.Forms.ToolStripSeparator _nN5;
        private System.Windows.Forms.ToolStripButton toolButtonBuild;
        private System.Windows.Forms.ToolStripButton toolButtonPreview;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageViews;
        private System.Windows.Forms.TabPage tabPageXmlSource;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusModified;
        private System.Windows.Forms.ToolStripStatusLabel statusHints;
        private System.Windows.Forms.SplitContainer splitterLog;
        private System.Windows.Forms.TextBox memoMessages;
        private System.Windows.Forms.TabPage tabPageCommands;
        private XmlSourceFrame _xmlSourceFrame;
        private CommandsFrame _commandsFrame;
        private ViewsFrame _viewsFrame;
        private System.Windows.Forms.Timer _timerRestoreLog;
        private System.Windows.Forms.ToolStripComboBox toolPreviewLanguageCombo;
        private System.Windows.Forms.ToolStripTextBox toolVersion;
    }
}

