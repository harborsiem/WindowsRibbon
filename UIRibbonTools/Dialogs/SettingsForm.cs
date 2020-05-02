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
using Microsoft.Win32;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;

namespace UIRibbonTools
{
    partial class SettingsForm : Form
    {
        const string cUiccDownloadUrl7 = "https://www.microsoft.com/en-us/download/confirmation.aspx?id=8279"; //Windows 7 SDK
        const string cUiccDownloadUrl8 = "http://go.microsoft.com/fwlink/p/?LinkId=323507"; // Windows 8 SDK download permalink

        ImageList _imageList;
        OpenFileDialog _openDialog;
        private Settings _settings;

        public SettingsForm()
        {
#if Core
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
#endif
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;
            _openDialog = new OpenFileDialog();
            this.Closing += FormClose;
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

        public SettingsForm(Settings settings) : this()
        {
            _settings = settings;
            Shown += this.EditPathChange; // Update controls when the form is activated
            UpdateControls();
        }

        private void Destroy()
        {
            Shown -= this.EditPathChange;
        }

        private void EditDelphiCompilerRightButtonClick(object sender, EventArgs e)
        {
            _openDialog.Filter = "DCC32.exe|DCC32.exe";
            _openDialog.InitialDirectory = Path.GetDirectoryName(EditDelphiCompiler.Text);
            _openDialog.FileName = Path.GetFileName(EditDelphiCompiler.Text);
            if (_openDialog.ShowDialog() == DialogResult.OK)
                EditDelphiCompiler.Text = _openDialog.FileName;
        }

        private void EditPathChange(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void EditRibbonCompilerRightButtonClick(object sender, EventArgs e)
        {
            _openDialog.Filter = "UICC.exe|UICC.exe";
            _openDialog.InitialDirectory = Path.GetDirectoryName(EditRibbonCompiler.Text);
            _openDialog.FileName = Path.GetFileName(EditRibbonCompiler.Text);
            if (_openDialog.ShowDialog() == DialogResult.OK)
                EditRibbonCompiler.Text = _openDialog.FileName;
        }

        private void FormClose(object sender, CancelEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                _settings.RibbonCompilerPath = EditRibbonCompiler.Text;
                _settings.DelphiCompilerPath = EditDelphiCompiler.Text;
                _settings.Save();
            }
        }

        private void UpdateControls()
        {
            _settings.DetectTools();
            if (EditRibbonCompiler.Text == string.Empty)
                EditRibbonCompiler.Text = _settings.RibbonCompilerPath;
            if (EditDelphiCompiler.Text == string.Empty)
                EditDelphiCompiler.Text = _settings.DelphiCompilerPath;
            ButtonOk.Enabled =
              File.Exists(EditRibbonCompiler.Text) &&
              File.Exists(EditDelphiCompiler.Text);
        }
    }

    class Settings
    {
        //resourcestring
        const string RS_CANNOT_SAVE_SETTINGS = "Unable to save settings.";
        const string RS_TOOLS_HEADER = "Cannot find compilation tools";
        const string RS_TOOLS_MESSAGE = "One or more ribbon compilation tools (UICC.exe && DCC32.exe) could not be found.";
        const string RS_TOOLS_SETUP = "Do you want to open the settings dialog box to specify the locations of these tools now?";

        // Element Names
        const string EN_SETTINGS = "Settings";
        const string EN_SETTING = "Setting";

        // Attribute Names
        const string AN_NAME = "name";
        const string AN_VALUE = "value";

        // Setting Names
        const string SN_RIBBON_COMPILER = "RibbonCompiler";
        const string SN_RESOURCE_COMPILER = "ResourceCompiler";
        const string SN_DELPHI_COMPILER = "DelphiCompiler";

        private static Settings _instance = new Settings();
        private string _settingsFilename;
        private string _ribbonCompilerPath;
        private string _resourceCompilerPath;
        private string _delphiCompilerPath;
        public static Settings Instance { get { return _instance; } }

        public string RibbonCompilerPath
        {
            get { return _ribbonCompilerPath; }
            set { _ribbonCompilerPath = value; }
        }
        public string ResourceCompilerPath
        {
            get { return _resourceCompilerPath; }
            set { _resourceCompilerPath = value; }
        }
        /// The path of the directory in which the ribbon compiler UICC.exe can be found
        public string RibbonCompilerDir
        {
            get { return GetRibbonCompilerPath(); }
        }
        public string DelphiCompilerPath
        {
            get { return _delphiCompilerPath; }
            set { _delphiCompilerPath = value; }
        }

        private Settings()
        {
            string path; //: array[0..MAX_PATH] of Char;

            if (Succeeded(SHGetFolderPath(0, CSIDL_LOCAL_APPDATA, 0, 0, path)))
            {
                _settingsFilename = path;
                _settingsFilename = Path.Combine(_settingsFilename, "Ribbon Designer");
                Addons.ForceDirectories(_settingsFilename);
                _settingsFilename = Path.Combine(_settingsFilename, "Settings.xml");
                Load();
            }
        }

        public void DetectTools()
        {
            RegistryKey reg;
            int bdsVersion;
            string bdsKey, bdsPath;

            _ribbonCompilerPath = FindTool("UICC.exe");
            _resourceCompilerPath = FindTool("rc.exe");

            // Find delphi compiler
            reg = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default);
            try
            {
                for (bdsVersion = 30; bdsVersion >= 10; bdsVersion--)
                {
                    bdsKey = @"Software\Embarcadero\BDS\" + bdsVersion.ToString() + ".0";
                    RegistryKey bds = reg.OpenSubKey(bdsKey, false);
                    if (bds != null)
                    {
                        bdsPath = (string)reg.GetValue("RootDir");
                        if (!string.IsNullOrEmpty(bdsPath))
                        {
                            bdsPath = Path.Combine(bdsPath, "bin");
                            if (string.IsNullOrEmpty(_delphiCompilerPath))
                            {
                                _delphiCompilerPath = Path.Combine(bdsPath, "DCC32.exe");
                                if (!File.Exists(_delphiCompilerPath))
                                    _delphiCompilerPath = string.Empty;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(_delphiCompilerPath))
                        break;
                }
            }
            finally
            {
                reg.Dispose();
            }
        }

        private string GetSearchPaths()
        {
            string result;
            List<string> list;
            list = new List<string>();
            try
            {
                list.Delimiter = Path.DirectorySeparatorChar;
                list.Add(ExtractFileDir(ParamStr(0))); // Check current directory of the RibbonCMDCompiler.exe first to find ribbon compiler UICC.exe
                list.Add(Environment.GetEnvironmentVariable("PATH"));
                list.Add(Environment.GetEnvironmentVariable("ProgramFiles") + @"\Windows Kits\10\bin\x86\");


                list.Add(Environment.GetEnvironmentVariable("ProgramFiles(x86)") + @"\Windows Kits\10\bin\x86\");


                list.Add(Environment.GetEnvironmentVariable("ProgramFiles(x86)") + @"\Windows Kits\10\bin\x86\");


                list.Add(Environment.GetEnvironmentVariable("ProgramFiles") + @"\Windows Kits\8.1\bin\x86\");


                list.Add(Environment.GetEnvironmentVariable("ProgramFiles(x86)") + @"\Windows Kits\8.1\bin\x86\");


                list.Add(Environment.GetEnvironmentVariable("ProgramFiles") + @"\Microsoft SDKs\Windows\v7.1\Bin\");


                list.Add(Environment.GetEnvironmentVariable("ProgramFiles(x86)") + @"\Microsoft SDKs\Windows\v7.1\Bin\");


                result = list.DelimitedText.Replace('"', "");
            }
            finally
            {
                //lList.Free();
            }
            return result;
        }

        private string FindTool(string pExeName)
        {
            string result = FileSearch(pExeName, GetSearchPaths);
            return result;
        }

        private string GetRibbonCompilerPath()
        {
            return (Path.GetDirectoryName(_ribbonCompilerPath));
        }

        public void Load()
        {
            XDocument doc = null;
            //XElement E;
            string name, value;

            if (!File.Exists(_settingsFilename))
                return;

            try
            {
                doc = XDocument.Load(_settingsFilename);
                if ((doc.Root == null) || (doc.Root.Name != EN_SETTINGS))
                    return;

                foreach (XElement E in doc.Root)
                {
                    if (E.Name == EN_SETTING)
                    {
                        name = E.Attribute(AN_NAME)?.Value;
                        value = E.Attribute(AN_VALUE)?.Value;
                        if (!string.IsNullOrEmpty(value))
                        {
                            if ((name == SN_RIBBON_COMPILER) && File.Exists(value))
                                _ribbonCompilerPath = value;
                            else if ((name == SN_DELPHI_COMPILER) && File.Exists(value))
                                _delphiCompilerPath = value;
                        }
                    }
                }
            }
            finally
            {
                //doc.Dispose();
            }
        }

        public void Save()
        {
            XmlWriter writer;
            FileStream stream;
            byte[] xml;

            void SaveSetting(string name, string value)
            {
                writer.WriteStartElement(EN_SETTING);
                writer.WriteAttributeString(AN_NAME, name);
                writer.WriteAttributeString(AN_VALUE, value);
                writer.WriteEndElement();
            }

            if (string.IsNullOrEmpty(_settingsFilename))
                throw new Exception(RS_CANNOT_SAVE_SETTINGS);

            MemoryStream mStream = new MemoryStream();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            writer = XmlWriter.Create(mStream, settings);
            try
            {
                writer.WriteStartElement(EN_SETTINGS);
                SaveSetting(SN_RIBBON_COMPILER, _ribbonCompilerPath);
                SaveSetting(SN_RESOURCE_COMPILER, _resourceCompilerPath);
                SaveSetting(SN_DELPHI_COMPILER, _delphiCompilerPath);
                writer.WriteEndElement();
                writer.WriteEndDocument(); //@ added
                xml = mStream.ToArray();
            }
            finally
            {
                writer.Close();
            }

            stream = new FileStream(_settingsFilename, FileMode.Create);
            try
            {
                if (xml.Length != 0)
                    stream.Write(xml, 0, xml.Length);
            }
            finally
            {
                stream.Close();
            }
        }

        public bool ToolsAvailable()
        {
            DetectTools();
            return ((!string.IsNullOrEmpty(_ribbonCompilerPath)) && (!string.IsNullOrEmpty(_delphiCompilerPath)));
        }
    }
}
