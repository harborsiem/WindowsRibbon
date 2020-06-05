using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.Reflection;
using WinForms.Actions;

namespace UIRibbonTools
{
    partial class MainForm : Form
    {
        private const string RS_CANNOT_LOAD_DLL = "Unable to load Ribbon Resource DLL";
        private const string RS_MODIFIED = "Modified";
        private const string RS_RIBBON_TOOLS = "Ribbon Tools";
        private const string RS_UNTITLED = "(untitled document)";
        private const string RS_CHANGED_HEADER = "Document has changed";
        private const string RS_CHANGED_MESSAGE = "The document has changed. Do you want to save the changes?";
        private const string RS_DIFFERENT_DIR_HEADER = "Directory changed";
        private static readonly string RS_DIFFERENT_DIR_MESSAGE = "You are about to save to document to a different directory." + Environment.NewLine +
            "Any images associated with this document will NOT be copied to this new directory." + Environment.NewLine +
            "If you want to keep these images, you will need to copy them to the new directory yourself." + Environment.NewLine +
            "Do you want to continue to save this document?";

        private TActionList _actionList;
        private TAction _actionPreview;
        private TAction _actionOpen;
        private TAction _actionNew;
        private TAction _actionGenerateCommandIDs;
        internal TAction _actionSaveAs;
        private TAction _actionSave;
        private TAction _actionSettings;
        private TAction _actionExit;
        //private TAction _actionNewBlank;
        private TAction _actionBuild;
        private TAction _actionTutorial;
        private TAction _actionWebSite;
        private TAction _actionDotnetWebSite;
        private TAction _actionMSDN;
        private TAction _actionSetResourceName;
        private TAction _actionGenerateResourceIDs;

        private bool _initialized;
        private TRibbonDocument _document;
        //private CommandsFrame _commandsFrame;
        //private ViewsFrame  _viewsFrame;
        //private XmlSourceFrame _xmlSourceFrame;
        private bool _modified;
        //private PreviewForm  _previewForm;
        private BuildPreviewHelper _buildPreviewHelper;
        private ImageList _imageListMain;
        public static MainForm FormMain;
        public ShortCutKeysDelegate ShortCutKeys;

        public MainForm()
        {
#if Core
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
#endif
            InitializeComponent();
            _commandsFrame.SetBoldFonts();
            _viewsFrame.SetBoldFonts();

#if SegoeFont
            this.Font = SystemFonts.MessageBoxFont;
#endif
#if Core && SegoeFont
            //@ maybe this is a bug in Core because we have to set the Font to the UserControls
            _commandsFrame.SetFonts(this.Font);
            _viewsFrame.SetFonts(this.Font);
            _xmlSourceFrame.SetFonts(this.Font);
#endif
            if (components == null)
                components = new Container();
            this.Text = RS_RIBBON_TOOLS;
            this.Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
            FormMain = this;
            CreateMainBitmaps();

            Settings.Instance.Read(this.MinimumSize);
            this.Size = Settings.Instance.ApplicationSize;

            _buildPreviewHelper = BuildPreviewHelper.Instance;
            _buildPreviewHelper.SetActions(null, SetPreviewEnabled, Log, SetLanguages);
            toolPreviewLanguageCombo.Enabled = false;
            toolPreviewLanguageCombo.SelectedIndex = 0;
            InitEvents();
            InitActions();
            toolVersion.Text = "Version: " + Application.ProductVersion;


            //    constructor TFormMain.Create(AOwner: TComponent)
            //{
            //  inherited;

            _document = new TRibbonDocument();

            //FCompiler = new TRibbonCompiler();
            //FCompiler.OnMessage = RibbonCompilerMessage;

            //// Handle command line options
            //if ((ParamCount > 0) && File.Exists(ParamStr(1)))  // File passed at the command line?
            //    OpenFile(ParamStr(1));
            //if (FindCmdLineSwitch("BUILD"))
            //{
            //    ActionBuild.Execute();
            //    Application.ShowMainForm = false;
            //    Application.Terminate();
            //}// if /BUILD
            //else
            //{
            //    NewFile(true);
            //}//else

            //UpdateControls(); //@ added why?
            _commandsFrame.RefreshSelection(); //@ added
        }

        private void InitActions()
        {
            _actionList = new TActionList(components);

            _actionPreview = new TAction(components);
            _actionOpen = new TAction(components);
            _actionNew = new TAction(components);
            _actionGenerateCommandIDs = new TAction(components);
            _actionSaveAs = new TAction(components);
            _actionSave = new TAction(components);
            _actionSettings = new TAction(components);
            _actionExit = new TAction(components);
            //_actionNewBlank = new TAction(components);
            _actionBuild = new TAction(components);
            _actionTutorial = new TAction(components);
            _actionWebSite = new TAction(components);
            _actionDotnetWebSite = new TAction(components);
            _actionMSDN = new TAction(components);
            _actionSetResourceName = new TAction(components);
            _actionGenerateResourceIDs = new TAction(components);

            //_actionList.SetAction(MenuAddCommand, ActionAddCommand);
            _actionList.Actions.AddRange(new TAction[] {
                _actionPreview,
                _actionOpen,
                _actionNew,
                _actionGenerateCommandIDs,
                _actionSaveAs,
                _actionSave,
                _actionSettings,
                _actionExit,
                //_actionNewBlank,
                _actionBuild,
                _actionTutorial,
                _actionWebSite,
                _actionDotnetWebSite,
                _actionMSDN,
                _actionSetResourceName,
                _actionGenerateResourceIDs
            });

            _actionPreview.Execute += ActionPreviewExecute;
            _actionPreview.Enabled = false; //@ added
            _actionPreview.Hint = "Preview the ribbon (F9)"; //@ "Build & Preview the ribbon (F9)"
            _actionPreview.ImageIndex = 3;
            _actionPreview.Text = "Preview";
            _actionPreview.ShortcutKeys = Keys.F9;
            _actionPreview.SetComponent(toolButtonPreview, true);
            _actionPreview.SetComponent(menuPreview, true);

            _actionOpen.Execute += ActionOpenExecute;
            _actionOpen.Hint = "Open an existing Ribbon document (Ctrl+O)";
            _actionOpen.ImageIndex = 1;
            _actionOpen.Text = "Open";
            _actionOpen.ShortcutKeys = (Keys)(Keys.Control | Keys.O);
            _actionOpen.SetComponent(toolButtonOpen, true);
            _actionOpen.SetComponent(menuOpen, true);
            //_actionAddCommand.ShowTextOnToolBar = false;

            _actionNew.Execute += ActionNewExecute;
            _actionNew.Hint = "Create a new Ribbon document (Ctrl+N)";
            _actionNew.ImageIndex = 0;
            _actionNew.Text = "New";
            _actionNew.ShortcutKeys = (Keys)(Keys.Control | Keys.N);
            _actionNew.SetComponent(menuNew, true);

            _actionGenerateCommandIDs.Execute += ActionGenerateCommandIDsExecute;
            _actionGenerateCommandIDs.Hint = "Generates and sets IDs for all commands in this markup.";
            _actionGenerateCommandIDs.Text = "Auto generate IDs for all commands";
            _actionGenerateCommandIDs.SetComponent(autoGenerateIdsForAllCommands, true);

            _actionSaveAs.Execute += ActionSaveAsExecute;
            _actionSaveAs.Enabled = false; //@ added
            _actionSaveAs.Hint = "Saves the Ribbon document under a new name (Shift+Ctrl+S)";
            _actionSaveAs.Text = "Save As";
            _actionSaveAs.ShortcutKeys = (Keys)(Keys.Shift | Keys.Control | Keys.S);
            _actionSaveAs.SetComponent(menuSaveAs, true);

            _actionSave.Execute += ActionSaveExecute;
            _actionSave.Enabled = false; //@ added
            _actionSave.Hint = "Saves the Ribbon document (Ctrl+S)";
            _actionSave.ImageIndex = 2;
            _actionSave.Text = "Save";
            _actionSave.ShortcutKeys = (Keys)(Keys.Control | Keys.S);
            _actionSave.SetComponent(toolButtonSave, true);
            _actionSave.SetComponent(menuSave, true);

            _actionSettings.Execute += ActionSettingsExecute;
            _actionSettings.ImageIndex = 4;
            _actionSettings.Text = "Settings";
            _actionSettings.SetComponent(menuSettings, true);

            _actionExit.Execute += ActionExitExecute;
            _actionExit.Hint = "Exits the " + RS_RIBBON_TOOLS;
            _actionExit.Text = "Exit";
            _actionExit.SetComponent(menuExit, true);

            //_actionNewBlank.Hint = "Create a new blank Ribbon Document";
            //_actionNewBlank.Text = "Empty Ribbon Document";
            ////_actionNewBlank.Shortcut = (Shortcut)16462;
            //_actionNewBlank.SetComponent(menuNew, true);

            _actionBuild.Execute += ActionBuildExecute;
            _actionBuild.Enabled = false; //@ added
            _actionBuild.Hint = "Build the ribbon (Ctrl+F9)";
            _actionBuild.ImageIndex = 5;
            _actionBuild.Text = "Build";
            _actionBuild.ShortcutKeys = (Keys)(Keys.Control | Keys.F9);
            _actionBuild.SetComponent(toolButtonBuild, true);
            _actionBuild.SetComponent(menuBuild, true);

            _actionTutorial.Execute += ActionTutorialExecute;
            _actionTutorial.ImageIndex = 7;
            _actionTutorial.Text = "Tutorial";
            _actionTutorial.SetComponent(menuTutorial, true);

            _actionWebSite.Execute += ActionWebSiteExecute;
            _actionWebSite.ImageIndex = 7;
            _actionWebSite.Text = "Ribbon Framework for Delphi website";
            _actionWebSite.SetComponent(menuWebSite, true);

            _actionDotnetWebSite.Execute += ActionDotnetWebSiteExecute;
            _actionDotnetWebSite.Hint = "C#, VB Ribbon Framework";
            _actionDotnetWebSite.ImageIndex = 7;
            _actionDotnetWebSite.Text = "Website for .NET WindowsRibbon";
            _actionDotnetWebSite.SetComponent(menuDotnetWebSite, true);

            _actionMSDN.Execute += ActionMSDNExecute;
            _actionMSDN.ImageIndex = 6;
            _actionMSDN.Text = "MSDN WindowsRibbon";
            _actionMSDN.SetComponent(menuMSDN, true);

            _actionSetResourceName.Visible = Settings.Instance.AllowChangingResourceName; // false; //@ not supported in .NET Ribbon
            _actionSetResourceName.Execute += ActionSetResourceNameExecute;
            _actionSetResourceName.Hint =
                "Set a resource name for the markup. This is necessary " + Environment.NewLine +
                "if multiple markups are used in one application." + Environment.NewLine +
                "The default is APPLICATION" + Environment.NewLine +
                Environment.NewLine + "Changing of default is not supported in .NET Ribbon";
            _actionSetResourceName.Text = "Set ribbon resource name";
            _actionSetResourceName.SetComponent(setresourcename, true);

            _actionGenerateResourceIDs.Execute += ActionGenerateResourceIDsExecute;
            _actionGenerateResourceIDs.Hint =
                "Generates and sets IDs for all resources in this markup." + Environment.NewLine +
                "(For applications that use multiple different ribbons," + Environment.NewLine +
                "it may be necessary to set IDs explicitly," + Environment.NewLine +
                "so that there are no conflicting resource IDs)";
            _actionGenerateResourceIDs.Text = "Auto generate IDs for all resources";
            _actionGenerateResourceIDs.ShortcutKeys = (Keys)(Keys.Control | Keys.G);
            _actionGenerateResourceIDs.SetComponent(autoGenerateIdsForAllResources, true);

            _actionList.ImageList = _imageListMain;
        }

        public void MsgToDo() //@ todo
        {
            MessageBox.Show("Todo", "Todo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void InitEvents()
        {
            this.ResizeBegin += MainForm_ResizeBegin;
            this.ResizeEnd += MainForm_ResizeEnd;
            Load += FormLoad;
            Shown += CMShowingChanged;
            this.Closing += FormCloseQuery;
            this.FormClosed += FormClose;
            Application.ThreadException += ApplicationEventsException;
            _timerRestoreLog.Tick += TimerRestoreLogTimer;
            tabControl.SelectedIndexChanged += PageControlChange;
            toolPreviewLanguageCombo.SelectedIndexChanged += LanguageComboBox_SelectedIndexChanged;
        }

        private void MainForm_ResizeBegin(object sender, EventArgs e)
        {
            this.SuspendLayout();
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            NativeMethods.SuspendPainting(this.Handle);
            this.ResumeLayout(true);
            NativeMethods.ResumePainting(this.Handle);
            this.Refresh();
        }

        private void CreateMainBitmaps()
        {
            _imageListMain = ImageManager.Images_Main(components);
            toolStrip.ImageList = _imageListMain;
            //mainMenuStrip.ImageList = _imageListMain;
            menuFile.DropDown.ImageList = _imageListMain;
            menuProject.DropDown.ImageList = _imageListMain;
            menuHelp.DropDown.ImageList = _imageListMain;
        }

        private void ActionBuildExecute(object sender, EventArgs e)
        {
            BuildAndPreview(false);
        }

        private void ActionExitExecute(object sender, EventArgs e)
        {
            Close();
        }

        private void ActionGenerateCommandIDsExecute(object sender, EventArgs e)
        {
            TRibbonCommand command;
            for (int i = 0; i < _commandsFrame.ListViewCommands.Items.Count; i++)
            {
                command = (TRibbonCommand)_commandsFrame.ListViewCommands.Items[i].Tag;
                if (command.Id == 0)
                    // Try to mimic the auto ID generation of the ribbon compiler.
                    command.Id = _commandsFrame.FindSmallestUnusedID(i + 2);
            }

            _commandsFrame.RefreshSelection();
        }

        private void ActionGenerateResourceIDsExecute(object sender, EventArgs e)
        {
            int autoID;

            void SetID(TRibbonString rs)
            {
                if (!string.IsNullOrEmpty(rs.Content))
                {
                    rs.Id = autoID;
                    autoID++;
                }
            }

            void SetImageID(TRibbonList<TRibbonImage> rl)
            {
                for (int i1 = 0; i1 < rl.Count; i1++)
                {
                    rl[i1].Id = autoID;
                    autoID++;
                }
            }

            TRibbonCommand command;
            int i, maxID;
            string s;

            //
            // First work out the maximum no of command ids that will be required
            maxID = _commandsFrame.ListViewCommands.Items.Count; //@ bugfix
            for (i = 0; i < _commandsFrame.ListViewCommands.Items.Count; i++)
            {
                command = (TRibbonCommand)_commandsFrame.ListViewCommands.Items[i].Tag;
                {
                    if (!string.IsNullOrEmpty(command.LabelTitle.Content))
                        maxID++;
                    if (!string.IsNullOrEmpty(command.LabelDescription.Content))
                        maxID++;
                    if (!string.IsNullOrEmpty(command.TooltipTitle.Content))
                        maxID++;
                    if (!string.IsNullOrEmpty(command.TooltipDescription.Content))
                        maxID++;
                    if (!string.IsNullOrEmpty(command.Keytip.Content))
                        maxID++;
                    maxID += command.SmallImages.Count + command.LargeImages.Count
                        + command.SmallHighContrastImages.Count + command.LargeHighContrastImages.Count;
                }
            }

            if (InputQuery.Show(this, "ID Number", "Enter the starting ID number between 2 & " + (59999 - maxID).ToString(), out s) == DialogResult.OK)
            {
                if (!int.TryParse(s, out autoID))
                {
                    throw new ArgumentException("Invalid integer value");
                }
            }
            else
                return;

            if ((autoID < 2) || (autoID + maxID > 59999))
            {
                throw new ArgumentException(autoID.ToString() + "is an invalid starting ID. "
                + "Must be a number between 2 && < " + (59999 - maxID).ToString(), nameof(autoID));
            }

            for (i = 0; i < _commandsFrame.ListViewCommands.Items.Count; i++)
            {
                command = (TRibbonCommand)_commandsFrame.ListViewCommands.Items[i].Tag;

                {
                    SetID(command.LabelTitle);
                    SetID(command.LabelDescription);
                    SetID(command.TooltipTitle);
                    SetID(command.TooltipDescription);
                    SetID(command.Keytip);

                    SetImageID(command.SmallImages);
                    SetImageID(command.LargeImages);
                    SetImageID(command.SmallHighContrastImages);
                    SetImageID(command.LargeHighContrastImages);
                }
            }
            _commandsFrame.RefreshSelection();
        }

        private void ActionMSDNExecute(object sender, EventArgs e)
        {
            OpenWebsite("https://docs.microsoft.com/en-us/windows/win32/windowsribbon/-uiplat-windowsribbon-entry");
        }

        private void ActionNewExecute(object sender, EventArgs e)
        {
            NewFile(false);
        }

        private void ActionOpenExecute(object sender, EventArgs e)
        {
            if (!CheckSave())
                return;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ReadOnlyChecked = false;
            dialog.CheckFileExists = true;
            dialog.DefaultExt = "xml";
            dialog.Filter = "Ribbon XML Files| *.xml";
            dialog.Title = "Open Ribbon File";
            if (dialog.ShowDialog() == DialogResult.OK)
                OpenFile(dialog.FileName);
        }

        private void ActionPreviewExecute(object sender, EventArgs e)
        {
            BuildAndPreview(true);
        }

        private void ActionSaveAsExecute(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.OverwritePrompt = true;
            dialog.DefaultExt = "xml";
            dialog.Filter = "Ribbon XML Files| *.xml";
            dialog.Title = "Save Ribbon File";

            string originalDirectory, newDirectory;
            originalDirectory = Path.GetDirectoryName(_document.Filename);
            dialog.FileName = Path.GetFileName(_document.Filename);
            dialog.InitialDirectory = originalDirectory;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                newDirectory = Path.GetDirectoryName(dialog.FileName);
                if ((!string.IsNullOrEmpty(originalDirectory)) && (!string.Equals(originalDirectory, newDirectory, StringComparison.OrdinalIgnoreCase)))
                {
                    if (MessageBox.Show(RS_DIFFERENT_DIR_MESSAGE, RS_DIFFERENT_DIR_HEADER, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                        return;
                }
                _document.SaveToFile(dialog.FileName);
                UpdateCaption();
                UpdateControls();
                ClearModified();
                ShowFilename(dialog.FileName);
                _buildPreviewHelper.SetRibbonXmlFile(dialog.FileName);
            }
        }

        private void ActionSaveExecute(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_document.Filename))
                ActionSaveAsExecute(sender, e);
            else
            {
                _document.SaveToFile(_document.Filename);
                ClearModified();
            }
        }

        private void ActionSetResourceNameExecute(object sender, EventArgs e)
        {
            string userInput;
            userInput = InputBox.Show(this, "Enter resource name", "Please enter a resource name that is used for this ribbon markup", _document.Application.ResourceName);
            if (!string.IsNullOrEmpty(userInput))
                _document.Application.ResourceName = userInput;
        }

        private void ActionSettingsExecute(object sender, EventArgs e)
        {
            ShowSettingsDialog();
        }

        private void ActionTutorialExecute(object sender, EventArgs e)
        {
            OpenWebsite("https://www.bilsen.com/windowsribbon/tutorial.shtml");
        }

        private void ActionWebSiteExecute(object sender, EventArgs e)
        {
            OpenWebsite("https://www.bilsen.com/windowsribbon/index.shtml");
        }

        private void ActionDotnetWebSiteExecute(object sender, EventArgs e)
        {
            OpenWebsite("https://github.com/harborsiem/WindowsRibbon");
        }

        private void ApplicationEventsException(object sender, ThreadExceptionEventArgs e)
        {
            memoMessages.ForeColor = Color.Red;
            Log(MessageKind.Error, e.Exception.Message);
            _timerRestoreLog.Enabled = true;
        }

        //@ => we have Tooltips
        //private void ApplicationEventsHint(object sender, EventArgs e)
        //{
        //    statusHints.Text = Application.Hint;
        //}

        private void BuildAndPreview(bool preview)
        {
            ClearLog();
            if (_modified)
                ActionSaveExecute(this, EventArgs.Empty);
            if (preview)
            {
                _buildPreviewHelper.ShowPreviewDialog(this);
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    _buildPreviewHelper.BuildRibbonFile(_document.Application.ResourceName);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }
        //private void BuildAndPreview(bool preview)
        //{
        //    THandle DllInstance;
        //    TRibbonCompileResult result;
        //    ClearLog();
        //    if (_modified)
        //        ActionSaveExecute(this, EventArgs.Empty);
        //    FreeAndNil(_previewForm);
        //    // Create DLL only if a preview is requested
        //    result = FCompiler.Compile(_document, _document.Application.ResourceName, preview);

        //    if (result == crOk)
        //    {
        //        if (preview)
        //        {
        //            DllInstance = LoadLibraryEx(PChar(FCompiler.OutputDllPath), 0, LOAD_LIBRARY_AS_DATAFILE);
        //            if (DllInstance == 0)
        //            {
        //                Log(MessageKind.Error, RS_CANNOT_LOAD_DLL);
        //                return;
        //            }

        //            try
        //            {
        //                _previewForm = new PreviewForm(DllInstance, _document, _document.Application.ResourceName);
        //                _previewForm.ShowDialog();
        //            }
        //            catch (Exception)
        //            {
        //                FreeLibrary(DllInstance);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        memoMessages.ForeColor = Color.Red;
        //        //    memoMessages.Update();
        //        _timerRestoreLog.Enabled = true;
        //        //    if (result == crRibbonCompilerError) 
        //        //    {
        //        //      _xmlSourceFrame.Activate_();
        //        //      tabControl.SelectedTab = tabPageXmlSource;
        //        //    }
        //    }
        //}

        private bool CheckSave()
        {
            bool result = true;
            if (_modified)
            {
                switch (MessageBox.Show(RS_CHANGED_MESSAGE, RS_CHANGED_HEADER, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    case DialogResult.Yes:
                        {
                            if (_actionSave.Enabled)
                                ActionSaveExecute(this, EventArgs.Empty);
                            else
                                ActionSaveAsExecute(this, EventArgs.Empty);
                            break;
                        }

                    case DialogResult.No:
                        break;

                    case DialogResult.Cancel:
                        result = false;
                        break;
                }
            }
            return result;
        }

        private void ClearDocument()
        {
            _commandsFrame.ClearDocument();
            _viewsFrame.ClearDocument();
            _xmlSourceFrame.ClearDocument();
            _document.Clear();
        }

        private void ClearLog()
        {
            memoMessages.Clear();
        }

        private void ClearModified()
        {
            _modified = false;
            statusModified.Text = string.Empty;
        }

        //@ todo
        private void CMShowingChanged(object sender, EventArgs e)
        {
            if (!_initialized)
            {
                _initialized = true;
                if (!Settings.Instance.ToolsAvailable())
                    if (MessageBox.Show(Settings.RS_TOOLS_MESSAGE + Environment.NewLine + Settings.RS_TOOLS_SETUP, Settings.RS_TOOLS_HEADER, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)

                        ShowSettingsDialog();
            }
        }

        private void Destroy()
        {
            _document.Dispose();
        }

        private void FormLoad(object sender, EventArgs e)
        {
            memoMessages.SelectionLength = 0;
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1 && !string.IsNullOrEmpty(args[1]) && File.Exists(args[1]))
            {
                OpenFile(Addons.GetExactFilenameWithPath(args[1]));
            }
        }

        private void FormClose(object sender, FormClosedEventArgs e)
        {
            Settings.Instance.Write();
            Application.Exit();
        }

        private void FormCloseQuery(object sender, CancelEventArgs e)
        {
            if (!CheckSave())
                e.Cancel = true;
        }

        static readonly string[] MSG_TYPES = { string.Empty, "WARNING: ", "ERROR: ", string.Empty };

        private void Log(MessageKind msgType,
            string msg)
        {
            List<string> lines = new List<string>(memoMessages.Lines);
            lines.Add(MSG_TYPES[(int)msgType] + msg);
            memoMessages.Lines = lines.ToArray();
        }

        public void Modified()
        {
            if (!_modified)
            {
                _modified = true;
                statusModified.Text = RS_MODIFIED;
                _buildPreviewHelper.HasValidParser = false;
            }
        }

        private void ShowFilename(string value)
        {
            statusHints.Text = value;
        }

        private void NewFile(bool emptyFile)
        {
            RibbonTemplate template;
            string fileName, filePath;
            Stream zipStream;

            if (!CheckSave())
                return;

            if (emptyFile)
            {
                template = RibbonTemplate.None;
                fileName = string.Empty;
            }
            else if (!NewFileForm.NewFileDialog(out template, out fileName))
                return;

            ClearDocument();
            if (template == RibbonTemplate.None)
            {
                if (!string.IsNullOrEmpty(fileName))
                    _document.SaveToFile(fileName);
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    filePath = Path.GetDirectoryName(fileName);
                    zipStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("UIRibbonTools.Wordpad.zip");
                    string tmpFile = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
                    FileStream fs = File.Create(tmpFile);
                    zipStream.CopyTo(fs);
                    fs.Close();
                    try
                    {
                        if (Directory.Exists(@".\Res"))
                        {
                            Directory.Delete(@".\Res", true);
                            File.Delete(@".\RibbonMarkup.xml");
                        }
                        ZipFile.ExtractToDirectory(tmpFile, @".\");
                    }
                    finally
                    {
                        zipStream.Close();
                        File.Delete(tmpFile);
                    }
                    File.Move(Path.Combine(filePath, "RibbonMarkup.xml"), fileName);
                    _document.LoadFromFile(fileName);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }

            tabControl.SelectedTab = tabPageCommands;
            ActiveControl = _commandsFrame.ListViewCommands;
            ShowDocument();
            UpdateCaption();
            UpdateControls();
            ClearModified();
            ShowFilename(fileName);
            _buildPreviewHelper.SetRibbonXmlFile(fileName);
        }

        private void OpenFile(string fileName)
        {
            ClearDocument();
            _document.LoadFromFile(fileName);
            tabControl.SelectedTab = tabPageCommands;
            ActiveControl = _commandsFrame.ListViewCommands;
            ShowDocument();
            UpdateCaption();
            UpdateControls();
            ClearModified();
            ShowFilename(fileName);
            _buildPreviewHelper.SetRibbonXmlFile(fileName);
        }

        private void OpenWebsite(string url)
        {
            ProcessStartInfo psi = new ProcessStartInfo() { UseShellExecute = true, FileName = url };
            Process.Start(psi);
        }

        private void PageControlChange(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabPageViews)
            {
                _commandsFrame.Deactivate_();
                _xmlSourceFrame.Deactivate_();
                _viewsFrame.Activate_();
            }
            else if (tabControl.SelectedTab == tabPageCommands)
            {
                _viewsFrame.Deactivate_();
                _xmlSourceFrame.Deactivate_();
                _commandsFrame.Activate_();
            }
            else if (tabControl.SelectedTab == tabPageXmlSource)
            {
                if (_modified)
                    ActionSaveExecute(sender, e);
                _viewsFrame.Deactivate_();
                _commandsFrame.Deactivate_();
                _xmlSourceFrame.Activate_();
            }
        }

        //@ todo
        //private void RibbonCompilerMessage(TRibbonCompiler Compiler,
        //    MessageKind MsgType, string Msg)
        //{
        //    if (MsgType == mkPipe)
        //        memoMessages.Text = memoMessages.Text + Msg;
        //    else
        //        Log(MsgType, Msg);
        //}

        private void ShowDocument()
        {
            _xmlSourceFrame.Deactivate_();
            _viewsFrame.Deactivate_();
            _commandsFrame.Activate_();
            _commandsFrame.ShowDocument(_document);
            _viewsFrame.ShowDocument(_document);
            _xmlSourceFrame.ShowDocument(_document);
        }

        private void ShowSettingsDialog()
        {
            SettingsForm dialog;

            dialog = new SettingsForm();
            try
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _actionSetResourceName.Visible = Settings.Instance.AllowChangingResourceName;
                }

            }
            finally
            {
                //dialog.Close();
            }
        }

        private void TimerRestoreLogTimer(object sender, EventArgs e)
        {
            _timerRestoreLog.Enabled = false;
            memoMessages.ForeColor = SystemColors.Window;
        }

        private void UpdateCaption()
        {
            if (string.IsNullOrEmpty(_document.Filename))
                this.Text = RS_UNTITLED + " - " + RS_RIBBON_TOOLS;
            else
                this.Text = Path.GetFileName(_document.Filename) + " - " + RS_RIBBON_TOOLS;
        }

        private void UpdateControls()
        {
            bool enabled = File.Exists(_document.Filename);
            _actionPreview.Enabled = enabled;
            _actionBuild.Enabled = enabled;
            _actionSave.Enabled = enabled; //@ added
            _actionSaveAs.Enabled = enabled; //@ added
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) //@ added
        {
            bool result = false;
            if (ShortCutKeys != null)
                result = ShortCutKeys(ref msg, keyData);
            if (result)
                return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void LanguageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolStripComboBox combo = sender as ToolStripComboBox;
            if (combo != null)
            {
                _buildPreviewHelper.UICulture = (string)combo.SelectedItem;
            }
        }

        private void SetLanguages(IList<string> languages)
        {
            toolPreviewLanguageCombo.SelectedIndexChanged -= LanguageComboBox_SelectedIndexChanged;
            toolPreviewLanguageCombo.BeginUpdate();
            toolPreviewLanguageCombo.Items.Clear();
            toolPreviewLanguageCombo.Enabled = true;
            toolPreviewLanguageCombo.Items.Add(BuildPreviewHelper.Neutral);
            if (languages != null)
            {
                for (int i = 0; i < languages.Count; i++)
                {
                    toolPreviewLanguageCombo.Items.Add(languages[i]);
                }
            }
            toolPreviewLanguageCombo.EndUpdate();
            toolPreviewLanguageCombo.SelectedIndex = 0;
            toolPreviewLanguageCombo.SelectedIndexChanged += LanguageComboBox_SelectedIndexChanged;
        }

        private void SetPreviewEnabled(bool enabled)
        {
            _actionPreview.Enabled = enabled;
        }
    }

    public delegate bool ShortCutKeysDelegate(ref Message msg, Keys keys); //@ added, Should we put it to the Actions ?
}
