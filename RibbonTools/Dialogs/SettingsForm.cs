using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;

namespace UIRibbonTools
{
    partial class SettingsForm : Form
    {
        const string cUiccDownloadUrl7 = "https://www.microsoft.com/en-us/download/confirmation.aspx?id=8279"; //Windows 7 SDK
        const string cUiccDownloadUrl8 = "http://go.microsoft.com/fwlink/p/?LinkId=323507"; // Windows 8 SDK download permalink

        private ImageList _imageList;
        private OpenFileDialog _openDialog;
        private Settings _settings;
        private bool _modified;
        private Size _size;
        private ToolTip _settingsTip;

        public SettingsForm()
        {
#if Core
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
#endif
            InitializeComponent();
#if SegoeFont
            this.Font = SystemFonts.MessageBoxFont;
#endif
            this.Icon = Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if (components == null)
                components = new Container();
            _imageList = ImageManager.ImageList_NewFile(components);
            compilerButton.ImageList = _imageList;
            compilerButton.ImageIndex = 0;
            resourceButton.ImageList = _imageList;
            resourceButton.ImageIndex = 0;
            linkerButton.ImageList = _imageList;
            linkerButton.ImageIndex = 0;

            _settings = Settings.Instance;
            _openDialog = new OpenFileDialog();
            _openDialog.CheckFileExists = true;
            _openDialog.CheckPathExists = true;
            _openDialog.Multiselect = false;
            UpdateControls();
            _settingsTip = new ToolTip(components);
            _settingsTip.SetToolTip(allowChangingResourceName, "Changing of default ResourceName is not recommended in .NET WindowsRibbon");
            _settingsTip.SetToolTip(allowPngImages, "*.png images are only allowed in Windows 8 and later versions");
            _settingsTip.SetToolTip(advancedWrapperClassFile, "For unlimited Ribbons in an application");
            InitEvents();
        }

        private void InitEvents()
        {
            compilerButton.Click += RibbonCompilerRightButtonClick;
            compilerButton.MouseEnter += OpenDialogButton_MouseEnter;
            compilerButton.MouseLeave += OpenDialogButton_MouseLeave;
            resourceButton.Click += ResourceCompilerRightButtonClick;
            resourceButton.MouseEnter += OpenDialogButton_MouseEnter;
            resourceButton.MouseLeave += OpenDialogButton_MouseLeave;
            linkerButton.Click += LinkerRightButtonClick;
            linkerButton.MouseEnter += OpenDialogButton_MouseEnter;
            linkerButton.MouseLeave += OpenDialogButton_MouseLeave;
            cSharpCheck.Click += CheckBox_Click;
            vbCheck.Click += CheckBox_Click;
            advancedWrapperClassFile.Click += CheckBox_Click;
            autoUpdateToolsPath.Click += CheckBox_Click;
            allowChangingResourceName.Click += CheckBox_Click;
            deleteResFile.Click += CheckBox_Click;
            allowPngImages.Click += CheckBox_Click;
            sizeButton.Click += SizeButton_Click;
            buttonOK.Click += ButtonOK_Click;
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            if (_modified)
            {
                _settings.AllowChangingResourceName = allowChangingResourceName.Checked;
                _settings.AllowPngImages = allowPngImages.Checked;
                _settings.AutoUpdateToolsPath = autoUpdateToolsPath.Checked;
                _settings.DeleteResFile = deleteResFile.Checked;
                _settings.AdvancedWrapperClassFile = advancedWrapperClassFile.Checked;
                _settings.BuildCSharpWrapper = cSharpCheck.Checked;
                _settings.BuildVBWrapper = vbCheck.Checked;
                _settings.RibbonCompilerPath = ribbonCompilerText.Text;
                _settings.ResourceCompilerPath = resourceCompilerText.Text;
                _settings.LinkPath = linkerText.Text;
                _settings.Height = _size.Height;
                _settings.Width = _size.Width;
                _settings.Modified = true;
            }
        }

        private void SizeButton_Click(object sender, EventArgs e)
        {
            if (Program.ApplicationForm.WindowState == FormWindowState.Maximized)
                MessageBox.Show("Can't set default size when application is maximized", "Set default size", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                _size = Program.ApplicationForm.Size;
                _modified = true;
            }
        }

        private void CheckBox_Click(object sender, EventArgs e)
        {
            _modified = true;
        }

        private void DownloadButtonClick(object sender, EventArgs e)
        {
            string downloadUrl;

            if (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor >= 2)
                downloadUrl = cUiccDownloadUrl8;  // Windows 8 && higher
            else
                downloadUrl = cUiccDownloadUrl7;   // Windows 7 && below
            ProcessStartInfo psi = new ProcessStartInfo() { UseShellExecute = true, FileName = downloadUrl };
            Process.Start(psi);
        }

        //private void DelphiCompilerRightButtonClick(object sender, EventArgs e)
        //{
        //    _openDialog.Filter = "DCC32.exe|DCC32.exe";
        //    _openDialog.InitialDirectory = Path.GetDirectoryName(delphiCompiler.Text);
        //    _openDialog.FileName = Path.GetFileName(delphiCompiler.Text);
        //    if (_openDialog.ShowDialog() == DialogResult.OK)
        //        delphiCompiler.Text = _openDialog.FileName;
        //}

        private void OpenDialogButton_MouseLeave(object sender, EventArgs e)
        {
            Button openDialog = sender as Button;
            openDialog.ImageIndex = 0;
        }

        private void OpenDialogButton_MouseEnter(object sender, EventArgs e)
        {
            Button openDialog = sender as Button;
            openDialog.ImageIndex = 1;
        }

        private void RibbonCompilerRightButtonClick(object sender, EventArgs e)
        {
            _openDialog.Filter = "uicc.exe|uicc.exe";
            if (!string.IsNullOrEmpty(ribbonCompilerText.Text))
            {
                _openDialog.InitialDirectory = Path.GetDirectoryName(ribbonCompilerText.Text);
                _openDialog.FileName = Path.GetFileName(ribbonCompilerText.Text);
            }
            if (_openDialog.ShowDialog() == DialogResult.OK)
            {
                ribbonCompilerText.Text = _openDialog.FileName;
                _modified = true;
            }
        }

        private void ResourceCompilerRightButtonClick(object sender, EventArgs e)
        {
            _openDialog.Filter = "rc.exe|rc.exe";
            if (!string.IsNullOrEmpty(resourceCompilerText.Text))
            {
                _openDialog.InitialDirectory = Path.GetDirectoryName(resourceCompilerText.Text);
                _openDialog.FileName = Path.GetFileName(resourceCompilerText.Text);
            }
            if (_openDialog.ShowDialog() == DialogResult.OK)
            {
                resourceCompilerText.Text = _openDialog.FileName;
                _modified = true;
            }
        }

        private void LinkerRightButtonClick(object sender, EventArgs e)
        {
            _openDialog.Filter = "link.exe|link.exe";
            if (!string.IsNullOrEmpty(linkerText.Text))
            {
                _openDialog.InitialDirectory = Path.GetDirectoryName(linkerText.Text);
                _openDialog.FileName = Path.GetFileName(linkerText.Text);
            }
            if (_openDialog.ShowDialog() == DialogResult.OK)
            {
                linkerText.Text = _openDialog.FileName;
                _modified = true;
            }
        }

        private void UpdateControls()
        {
            if (string.IsNullOrEmpty(ribbonCompilerText.Text))
                ribbonCompilerText.Text = _settings.RibbonCompilerPath;
            if (string.IsNullOrEmpty(resourceCompilerText.Text))
                resourceCompilerText.Text = _settings.ResourceCompilerPath;
            if (string.IsNullOrEmpty(linkerText.Text))
                linkerText.Text = _settings.LinkPath;
            advancedWrapperClassFile.Checked = _settings.AdvancedWrapperClassFile;
            cSharpCheck.Checked = _settings.BuildCSharpWrapper;
            vbCheck.Checked = _settings.BuildVBWrapper;
            autoUpdateToolsPath.Checked = _settings.AutoUpdateToolsPath;
            allowChangingResourceName.Checked = _settings.AllowChangingResourceName;
            allowPngImages.Checked = _settings.AllowPngImages;
            deleteResFile.Checked = _settings.DeleteResFile;
            _size = new Size(_settings.Width, _settings.Height);
            //buttonOK.Enabled = _settings.ToolsAvailable(true);
        }
    }

    //class Settings
    //{
    //    //resourcestring
    //    const string RS_CANNOT_SAVE_SETTINGS = "Unable to save settings.";
    //    const string RS_TOOLS_HEADER = "Cannot find compilation tools";
    //    const string RS_TOOLS_MESSAGE = "One or more ribbon compilation tools (UICC.exe && DCC32.exe) could not be found.";
    //    const string RS_TOOLS_SETUP = "Do you want to open the settings dialog box to specify the locations of these tools now?";

    //    // Element Names
    //    const string EN_SETTINGS = "Settings";
    //    const string EN_SETTING = "Setting";

    //    // Attribute Names
    //    const string AN_NAME = "name";
    //    const string AN_VALUE = "value";

    //    // Setting Names
    //    const string SN_RIBBON_COMPILER = "RibbonCompiler";
    //    const string SN_RESOURCE_COMPILER = "ResourceCompiler";
    //    const string SN_DELPHI_COMPILER = "DelphiCompiler";

    //    private static Settings _instance = new Settings();
    //    private string _settingsFilename;
    //    private string _ribbonCompilerPath;
    //    private string _resourceCompilerPath;
    //    private string _delphiCompilerPath;
    //    public static Settings Instance { get { return _instance; } }

    //    public string RibbonCompilerPath
    //    {
    //        get { return _ribbonCompilerPath; }
    //        set { _ribbonCompilerPath = value; }
    //    }
    //    public string ResourceCompilerPath
    //    {
    //        get { return _resourceCompilerPath; }
    //        set { _resourceCompilerPath = value; }
    //    }
    //    /// The path of the directory in which the ribbon compiler UICC.exe can be found
    //    public string RibbonCompilerDir
    //    {
    //        get { return GetRibbonCompilerPath(); }
    //    }
    //    public string DelphiCompilerPath
    //    {
    //        get { return _delphiCompilerPath; }
    //        set { _delphiCompilerPath = value; }
    //    }

    //    private Settings()
    //    {
    //        string path; //: array[0..MAX_PATH] of Char;

    //        if (Succeeded(SHGetFolderPath(0, CSIDL_LOCAL_APPDATA, 0, 0, path)))
    //        {
    //            _settingsFilename = path;
    //            _settingsFilename = Path.Combine(_settingsFilename, "Ribbon Designer");
    //            Addons.ForceDirectories(_settingsFilename);
    //            _settingsFilename = Path.Combine(_settingsFilename, "Settings.xml");
    //            Load();
    //        }
    //    }

    //    public void DetectTools()
    //    {
    //        RegistryKey reg;
    //        int bdsVersion;
    //        string bdsKey, bdsPath;

    //        _ribbonCompilerPath = FindTool("UICC.exe");
    //        _resourceCompilerPath = FindTool("rc.exe");

    //        // Find delphi compiler
    //        reg = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default);
    //        try
    //        {
    //            for (bdsVersion = 30; bdsVersion >= 10; bdsVersion--)
    //            {
    //                bdsKey = @"Software\Embarcadero\BDS\" + bdsVersion.ToString() + ".0";
    //                RegistryKey bds = reg.OpenSubKey(bdsKey, false);
    //                if (bds != null)
    //                {
    //                    bdsPath = (string)reg.GetValue("RootDir");
    //                    if (!string.IsNullOrEmpty(bdsPath))
    //                    {
    //                        bdsPath = Path.Combine(bdsPath, "bin");
    //                        if (string.IsNullOrEmpty(_delphiCompilerPath))
    //                        {
    //                            _delphiCompilerPath = Path.Combine(bdsPath, "DCC32.exe");
    //                            if (!File.Exists(_delphiCompilerPath))
    //                                _delphiCompilerPath = string.Empty;
    //                        }
    //                    }
    //                }
    //                if (!string.IsNullOrEmpty(_delphiCompilerPath))
    //                    break;
    //            }
    //        }
    //        finally
    //        {
    //            reg.Dispose();
    //        }
    //    }

    //    private string GetSearchPaths()
    //    {
    //        string result;
    //        List<string> list;
    //        list = new List<string>();
    //        try
    //        {
    //            list.Delimiter = Path.DirectorySeparatorChar;
    //            list.Add(ExtractFileDir(ParamStr(0))); // Check current directory of the RibbonCMDCompiler.exe first to find ribbon compiler UICC.exe
    //            list.Add(Environment.GetEnvironmentVariable("PATH"));
    //            list.Add(Environment.GetEnvironmentVariable("ProgramFiles") + @"\Windows Kits\10\bin\x86\");


    //            list.Add(Environment.GetEnvironmentVariable("ProgramFiles(x86)") + @"\Windows Kits\10\bin\x86\");


    //            list.Add(Environment.GetEnvironmentVariable("ProgramFiles(x86)") + @"\Windows Kits\10\bin\x86\");


    //            list.Add(Environment.GetEnvironmentVariable("ProgramFiles") + @"\Windows Kits\8.1\bin\x86\");


    //            list.Add(Environment.GetEnvironmentVariable("ProgramFiles(x86)") + @"\Windows Kits\8.1\bin\x86\");


    //            list.Add(Environment.GetEnvironmentVariable("ProgramFiles") + @"\Microsoft SDKs\Windows\v7.1\Bin\");


    //            list.Add(Environment.GetEnvironmentVariable("ProgramFiles(x86)") + @"\Microsoft SDKs\Windows\v7.1\Bin\");


    //            result = list.DelimitedText.Replace('"', "");
    //        }
    //        finally
    //        {
    //            //lList.Free();
    //        }
    //        return result;
    //    }

    //    private string FindTool(string pExeName)
    //    {
    //        string result = FileSearch(pExeName, GetSearchPaths);
    //        return result;
    //    }

    //    private string GetRibbonCompilerPath()
    //    {
    //        return (Path.GetDirectoryName(_ribbonCompilerPath));
    //    }

    //    public void Load()
    //    {
    //        XDocument doc = null;
    //        //XElement E;
    //        string name, value;

    //        if (!File.Exists(_settingsFilename))
    //            return;

    //        try
    //        {
    //            doc = XDocument.Load(_settingsFilename);
    //            if ((doc.Root == null) || (doc.Root.Name != EN_SETTINGS))
    //                return;

    //            foreach (XElement E in doc.Root)
    //            {
    //                if (E.Name == EN_SETTING)
    //                {
    //                    name = E.Attribute(AN_NAME)?.Value;
    //                    value = E.Attribute(AN_VALUE)?.Value;
    //                    if (!string.IsNullOrEmpty(value))
    //                    {
    //                        if ((name == SN_RIBBON_COMPILER) && File.Exists(value))
    //                            _ribbonCompilerPath = value;
    //                        else if ((name == SN_DELPHI_COMPILER) && File.Exists(value))
    //                            _delphiCompilerPath = value;
    //                    }
    //                }
    //            }
    //        }
    //        finally
    //        {
    //            //doc.Dispose();
    //        }
    //    }

    //    public void Save()
    //    {
    //        XmlWriter writer;
    //        FileStream stream;
    //        byte[] xml;

    //        void SaveSetting(string name, string value)
    //        {
    //            writer.WriteStartElement(EN_SETTING);
    //            writer.WriteAttributeString(AN_NAME, name);
    //            writer.WriteAttributeString(AN_VALUE, value);
    //            writer.WriteEndElement();
    //        }

    //        if (string.IsNullOrEmpty(_settingsFilename))
    //            throw new Exception(RS_CANNOT_SAVE_SETTINGS);

    //        MemoryStream mStream = new MemoryStream();
    //        XmlWriterSettings settings = new XmlWriterSettings();
    //        settings.Indent = true;
    //        writer = XmlWriter.Create(mStream, settings);
    //        try
    //        {
    //            writer.WriteStartElement(EN_SETTINGS);
    //            SaveSetting(SN_RIBBON_COMPILER, _ribbonCompilerPath);
    //            SaveSetting(SN_RESOURCE_COMPILER, _resourceCompilerPath);
    //            SaveSetting(SN_DELPHI_COMPILER, _delphiCompilerPath);
    //            writer.WriteEndElement();
    //            writer.WriteEndDocument(); //@ added
    //            xml = mStream.ToArray();
    //        }
    //        finally
    //        {
    //            writer.Close();
    //        }

    //        stream = new FileStream(_settingsFilename, FileMode.Create);
    //        try
    //        {
    //            if (xml.Length != 0)
    //                stream.Write(xml, 0, xml.Length);
    //        }
    //        finally
    //        {
    //            stream.Close();
    //        }
    //    }

    //    public bool ToolsAvailable()
    //    {
    //        DetectTools();
    //        return ((!string.IsNullOrEmpty(_ribbonCompilerPath)) && (!string.IsNullOrEmpty(_delphiCompilerPath)));
    //    }
    //}
}
