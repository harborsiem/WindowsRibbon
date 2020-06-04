using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Drawing;

namespace UIRibbonTools
{
    class Settings
    {
        //resourcestring
        public const string RS_CANNOT_SAVE_SETTINGS = "Unable to save settings.";
        public const string RS_TOOLS_HEADER = "Cannot find compilation tools";
        public const string RS_TOOLS_MESSAGE = "One or more ribbon compilation tools (uicc.exe, rc.exe, link.exe) could not be found.";
        public const string RS_TOOLS_SETUP = "Do you want to open the settings dialog box to specify the locations of these tools now?";

        private const string SettingsFile = "Settings.xml";

        public static Settings Instance = new Settings();

        private string _settingsPath;
        private bool _modified;
        private ToolsPathFinder _pathFinder;
        private bool? _toolsAvailableCache;

        private Settings()
        {
            _settingsPath = Util.RibbonToolsLocalAppData;
            _toolsAvailableCache = null;
        }

        public string RibbonCompilerPath { get; set; } = string.Empty;
        public string ResourceCompilerPath { get; set; } = string.Empty;
        public string LinkPath { get; set; } = string.Empty;
        public string Vcvars32BatPath { get; private set; } = string.Empty; //?
        public bool AutoUpdateToolsPath { get; set; } = true;
        public bool DeleteTempFiles { get; set; } = false; // or just delete?
        public bool DeleteResFile { get; set; } = true;
        public bool BuildCSharpWrapper { get; set; } = true;
        public bool BuildVBWrapper { get; set; }
        //public bool BuildCppWrapper { get; set; }
        //public bool BuildDelphiWrapper { get; set; }
        public bool AllowPngImages { get; set; }
        public bool AllowChangingResourceName { get; set; } = false;
        public int Width { get; set; }
        public int Height { get; set; }

        public bool Modified { get => _modified; set => _modified = value; }

        private void MinimumSize(Size size)
        {
            if (size.Width > Width)
            {
                Width = size.Width;
                _modified = true;
            }
            if (size.Height > Height)
            {
                Height = size.Height;
                _modified = true;
            }
        }

        private void AutoDetectTools()
        {
            if (string.IsNullOrEmpty(RibbonCompilerPath))
            {
                if (_pathFinder == null)
                    _pathFinder = new ToolsPathFinder(true, true);
                RibbonCompilerPath = _pathFinder.GetUiccExe();
                _modified = true;
            }
            if (string.IsNullOrEmpty(ResourceCompilerPath))
            {
                if (_pathFinder == null)
                    _pathFinder = new ToolsPathFinder(true, true);
                ResourceCompilerPath = _pathFinder.GetRcExe();
                _modified = true;
            }
            if (string.IsNullOrEmpty(LinkPath))
            {
                if (_pathFinder == null)
                    _pathFinder = new ToolsPathFinder(false, true);
                LinkPath = _pathFinder.GetLinkExe();
                _modified = true;
            }
            else
            {
                if (!File.Exists(LinkPath) && AutoUpdateToolsPath)
                {
                    if (_pathFinder == null)
                        _pathFinder = new ToolsPathFinder(false, false);
                    string tmp = _pathFinder.TryUpdateMSVCVersion(LinkPath);
                    if (tmp != null)
                    {
                        LinkPath = tmp;
                        _modified = true;
                    }
                }
            }
            if (string.IsNullOrEmpty(Vcvars32BatPath))
            {
                if (_pathFinder == null)
                    _pathFinder = new ToolsPathFinder(false, true);
                Vcvars32BatPath = _pathFinder.GetVcvars32Bat();
                _modified = true;
            }
        }

        public void Read(Size minimumSize)
        {
            bool modified = true;
            string settingsFile = Path.Combine(_settingsPath, SettingsFile);
            if (File.Exists(settingsFile))
            {
                XDocument xdoc = XDocument.Load(settingsFile);
                XElement root = xdoc.Root;
                if (root.Name.LocalName == Attributes.Settings)
                {
                    foreach (XElement ele in root.Elements())
                    {
                        switch (ele.Name.LocalName)
                        {
                            case Attributes.RibbonCompilerPath:
                                RibbonCompilerPath = ele.Value;
                                break;
                            case Attributes.ResourceCompilerPath:
                                ResourceCompilerPath = ele.Value;
                                break;
                            case Attributes.LinkPath:
                                LinkPath = ele.Value;
                                break;
                            case Attributes.Vcvars32BatPath:
                                Vcvars32BatPath = ele.Value;
                                break;
                            case Attributes.AutoUpdateToolsPath:
                                AutoUpdateToolsPath = XmlConvert.ToBoolean(ele.Value);
                                break;
                            case Attributes.DeleteTempFiles:
                                DeleteTempFiles = XmlConvert.ToBoolean(ele.Value);
                                break;
                            case Attributes.DeleteResFile:
                                DeleteResFile = XmlConvert.ToBoolean(ele.Value);
                                break;
                            case Attributes.BuildCSharpWrapper:
                                BuildCSharpWrapper = XmlConvert.ToBoolean(ele.Value);
                                break;
                            case Attributes.BuildVBWrapper:
                                BuildVBWrapper = XmlConvert.ToBoolean(ele.Value);
                                break;
                            //case Attributes.BuildCppWrapper:
                            //    BuildCppWrapper = XmlConvert.ToBoolean(ele.Value);
                            //    break;
                            //case Attributes.BuildDelphiWrapper:
                            //    BuildDelphiWrapper = XmlConvert.ToBoolean(ele.Value);
                            //    break;
                            case Attributes.AllowPngImages:
                                AllowPngImages = XmlConvert.ToBoolean(ele.Value);
                                break;
                            case Attributes.AllowChangingResourceName:
                                AllowChangingResourceName = XmlConvert.ToBoolean(ele.Value);
                                break;
                            case Attributes.Width:
                                Width = XmlConvert.ToInt32(ele.Value);
                                break;
                            case Attributes.Height:
                                Height = XmlConvert.ToInt32(ele.Value);
                                break;
                        }
                    }
                    modified = false;
                }
            }
            _modified = modified;
            AutoDetectTools();
            MinimumSize(minimumSize);
        }

        public void Write()
        {
            if (_modified)
            {
                string settingsFile = Path.Combine(_settingsPath, SettingsFile);
                XmlWriterSettings writerSettings = new XmlWriterSettings();
                writerSettings.Indent = true;
                XmlWriter writer = XmlWriter.Create(settingsFile, writerSettings);
                try
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement(Attributes.Settings);

                    writer.WriteStartElement(Attributes.RibbonCompilerPath);
                    writer.WriteString(RibbonCompilerPath);
                    writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.ResourceCompilerPath);
                    writer.WriteString(ResourceCompilerPath);
                    writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.LinkPath);
                    writer.WriteString(LinkPath);
                    writer.WriteEndElement();

                    //writer.WriteStartElement(Attributes.Vcvars32BatPath);
                    //writer.WriteString(Vcvars32BatPath);
                    //writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.AutoUpdateToolsPath);
                    writer.WriteString(XmlConvert.ToString(AutoUpdateToolsPath));
                    writer.WriteEndElement();

                    //writer.WriteStartElement(Attributes.DeleteTempFiles);
                    //writer.WriteString(XmlConvert.ToString(DeleteTempFiles));
                    //writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.DeleteResFile);
                    writer.WriteString(XmlConvert.ToString(DeleteResFile));
                    writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.BuildCSharpWrapper);
                    writer.WriteString(XmlConvert.ToString(BuildCSharpWrapper));
                    writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.BuildVBWrapper);
                    writer.WriteString(XmlConvert.ToString(BuildVBWrapper));
                    writer.WriteEndElement();

                    //writer.WriteStartElement(Attributes.BuildCppWrapper);
                    //writer.WriteString(XmlConvert.ToString(BuildCppWrapper));
                    //writer.WriteEndElement();

                    //writer.WriteStartElement(Attributes.BuildDelphiWrapper);
                    //writer.WriteString(XmlConvert.ToString(BuildDelphiWrapper));
                    //writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.AllowPngImages);
                    writer.WriteString(XmlConvert.ToString(AllowPngImages));
                    writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.AllowChangingResourceName);
                    writer.WriteString(XmlConvert.ToString(AllowChangingResourceName));
                    writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.Width);
                    writer.WriteString(XmlConvert.ToString(Width));
                    writer.WriteEndElement();

                    writer.WriteStartElement(Attributes.Height);
                    writer.WriteString(XmlConvert.ToString(Height));
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
                finally
                {
                    writer.Close();
                }
            }
        }

        public Size ApplicationSize
        {
            get => new Size(Width, Height);
        }

        public bool ToolsAvailable(bool noCacheValue = false)
        {
            if (_toolsAvailableCache == null || noCacheValue)
            {
                bool result = true;
                if (string.IsNullOrEmpty(RibbonCompilerPath) || !File.Exists(RibbonCompilerPath))
                    result = false;
                if (string.IsNullOrEmpty(ResourceCompilerPath) || !File.Exists(ResourceCompilerPath))
                    result = false;
                if (string.IsNullOrEmpty(LinkPath) || !File.Exists(LinkPath))
                    result = false;
                _toolsAvailableCache = result;
                return result;
            }
            return (bool)_toolsAvailableCache;
        }

        private static class Attributes
        {
            public const string Settings = nameof(Settings);
            public const string RibbonCompilerPath = nameof(RibbonCompilerPath);
            public const string ResourceCompilerPath = nameof(ResourceCompilerPath);
            public const string LinkPath = nameof(LinkPath);
            public const string Vcvars32BatPath = nameof(Vcvars32BatPath);
            public const string AutoUpdateToolsPath = nameof(AutoUpdateToolsPath);
            public const string DeleteTempFiles = nameof(DeleteTempFiles);
            public const string DeleteResFile = nameof(DeleteResFile);
            public const string BuildCSharpWrapper = nameof(BuildCSharpWrapper);
            public const string BuildVBWrapper = nameof(BuildVBWrapper);
            public const string BuildCppWrapper = nameof(BuildCppWrapper);
            public const string BuildDelphiWrapper = nameof(BuildDelphiWrapper);
            public const string AllowPngImages = nameof(AllowPngImages);
            public const string AllowChangingResourceName = nameof(AllowChangingResourceName);
            public const string Width = nameof(Width);
            public const string Height = nameof(Height);
        }
    }
}
