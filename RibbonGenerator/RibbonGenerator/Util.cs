using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Microsoft.Win32;

namespace RibbonGenerator
{
    /// <summary>
    /// Contains helper methods
    /// </summary>
    public static class Util
    {
        static Util()
        {
            // delete log file if file is bigger than...
            try
            {
                FileInfo info = new FileInfo(LogFile);
                if (info.Length > 1024 * 512)
                    File.Delete(LogFile);
            }
            catch { }
        }

        /// <summary>
        /// Writes a message to the log file
        /// </summary>
        /// <param name="message">the message</param>
        public static void LogMessage(string message)
        {
            string content = string.Format("{0} - {1}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message);
            File.AppendAllText(LogFile, content);
        }

        /// <summary>
        /// Writes a message to the log file
        /// </summary>
        /// <param name="message">the message</param>
        /// <param name="args">the args(look at string format for more info)</param>
        public static void LogMessage(string message, params object[] args)
        {
            LogMessage(string.Format(message, args));
        }

        /// <summary>
        /// Writes an exception to the log file
        /// </summary>
        /// <param name="ex"></param>
        public static void LogError(Exception ex)
        {
            string value = string.Format("{0}\r\n{1}", ex.Message, ex.ToString());
            LogMessage(value);
        }

        /// <summary>
        /// Contains the log filename
        /// </summary>
        static string _logFile;

        /// <summary>
        /// Gets the log filename. For example: C:\Users\<user>\AppData\Local\RibbonGenerator\RibbonGenerator.log
        /// </summary>
        public static string LogFile
        {
            get
            {
                if (_logFile == null)
                    _logFile = Path.Combine(GeneratorLocalAppData, "RibbonGenerator.log");
                return _logFile;
            }
        }

        /// <summary>
        /// Gets the local app data path for ribbongenerator. For example: C:\Users\<user>\AppData\Local\RibbonGenerator\
        /// </summary>
        public static string GeneratorLocalAppData
        {
            get
            {
                string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string path = Path.Combine(localAppData, "RibbonGenerator");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
        }

        /// <summary>
        /// Contains the template bat filename. For example: C:\Users\<user>\AppData\Local\RibbonGenerator\Template.bat
        /// </summary>
        static string _templateBatFilename;

        /// <summary>
        /// Gets the template bat filename. For example: C:\Users\<user>\AppData\Local\RibbonGenerator\Template.bat.
        /// </summary>
        public static string TemplateBatFilename
        {
            get
            {
                if (_templateBatFilename == null)
                {
                    string template = Path.Combine(GeneratorLocalAppData, "Template.bat");
                    var uri = new Uri(template);
                    _templateBatFilename = uri.LocalPath;
                }

                return _templateBatFilename;
            }
        }

        public static string DetectAppropriateWindowsSdkPath()
        {
            return GetLatestSdkToolsPath();
            //RegistryKey hklm = Environment.Is64BitOperatingSystem
            //    ? RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32)
            //    : Registry.LocalMachine;

            //var winSdkKey = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SDKs\Windows");

            //// if no windows sdk is installed return default path
            //if (winSdkKey == null)
            //    return DEFAULTWINDOWS7SDKPATH;

            //var versions = winSdkKey.GetSubKeyNames();
            //var desc = from s in versions
            //           orderby s descending
            //           select s;

            //// search folder that contains uicc.exe to verify windows 7 sdk path
            //string sdkToolsPath = null;
            //foreach (var version in desc)
            //{
            //    var versionKeyName = string.Format(@"SOFTWARE\Microsoft\Microsoft SDKs\Windows\{0}", version);
            //    var sdkToolsKey = hklm.OpenSubKey(versionKeyName);
            //    if (sdkToolsKey == null)
            //        continue;
            //    var installationFolder = sdkToolsKey.GetValue("InstallationFolder");
            //    if (installationFolder == null)
            //        continue;
            //    var uiccexePath = Path.Combine((string)installationFolder, "bin", "UICC.exe");
            //    if (File.Exists(uiccexePath))
            //    {
            //        sdkToolsPath = Path.Combine((string)installationFolder, "bin");
            //        break;
            //    }
            //}

            //// if no path found return default path
            //if (sdkToolsPath == null)
            //    sdkToolsPath = DEFAULTWINDOWS7SDKPATH;

            //if (!sdkToolsPath.EndsWith("\\"))
            //    sdkToolsPath += "\\";
            //return sdkToolsPath;
        }

        /// <summary>
        /// Gets the most recent sdk tools path.
        /// </summary>
        private static string GetLatestSdkToolsPath()
        {
            RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            RegistryKey winSdkKey = hklm.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft SDKs\\Windows");
            if (winSdkKey != null)
            {
                string[] versions = winSdkKey.GetSubKeyNames();

                SDKVersion[] sdkVersions = GetSortedSDKVersions(versions);

                for (int i = 0; i < sdkVersions.Length; i++)
                {
                    RegistryKey sdkToolsKey = winSdkKey.OpenSubKey(sdkVersions[i].sdkVersion);
                    if (sdkToolsKey == null)
                        continue;

                    string installFolder = sdkToolsKey.GetValue("InstallationFolder") as string;
                    string productVersion = sdkToolsKey.GetValue("ProductVersion") as string;
                    if (!string.IsNullOrEmpty(installFolder))
                    {
                        string sdkToolsPath = FindUiccFolder(installFolder, productVersion);

                        if (!string.IsNullOrEmpty(sdkToolsPath))
                        {
                            if (!sdkToolsPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                            {
                                sdkToolsPath += Path.DirectorySeparatorChar; 
                            }
                            return sdkToolsPath;
                        }
                    }
                }
            }
            return DEFAULTWINDOWS7SDKPATH;
        }

        /// <summary>
        /// return array is sorted with most recent sdk version at index 0.
        /// </summary>
        private static SDKVersion[] GetSortedSDKVersions(string[] versions)
        {
            SDKVersion[] sdkVersions = new SDKVersion[versions.Length];
            for (int i = 0; i < versions.Length; i++)
            {
                SDKVersion v = new SDKVersion();
                v.sdkVersion = versions[i];
                string sdk = versions[i];

                if (sdk.StartsWith("v"))
                {
                    sdk = sdk.Substring(1);
                }
                string[] sdkSplit = sdk.Split('.');
                if (sdkSplit.Length >= 2)
                {
                    Int32.TryParse(sdkSplit[0], out v.major);
                    v.minor = sdkSplit[1];
                }
                sdkVersions[i] = v;
            }
            Array.Sort<SDKVersion>(sdkVersions);
            return sdkVersions;
        }

        /// <summary>
        /// return folder that contains uicc.exe.
        /// </summary>
        private static string FindUiccFolder(string installFolder, string productVersion)
        {
            const string uicc = "uicc.exe";
            string binFolder = Path.Combine(installFolder, "bin");
            if (File.Exists(Path.Combine(binFolder, uicc)))
            {
                return binFolder;
            }
            string toolsFolder = Path.Combine(binFolder, "x86");
            if (File.Exists(Path.Combine(toolsFolder, uicc)))
            {
                return toolsFolder;
            }
            if (!string.IsNullOrEmpty(productVersion))
            {
                string[] versionComponents = productVersion.Split('.');
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < 4; i++)
                {
                    if (i < versionComponents.Length)
                    {
                        builder.Append(versionComponents[i] + ".");
                    } else
                    {
                        builder.Append("0.");
                    }
                }
                builder.Length = builder.Length - 1;
                string productVersionFull = builder.ToString();

                toolsFolder = Path.Combine(binFolder, productVersionFull, "x86");
                if (File.Exists(Path.Combine(toolsFolder, uicc)))
                {
                    return toolsFolder;
                }
            }
            return string.Empty;
        }

        public static string DEFAULTWINDOWS7SDKPATH = string.Format(@"%PROGRAMFILES{0}%\Microsoft SDKs\Windows\v7.1\Bin\", Environment.Is64BitOperatingSystem ? "(x86)": "");

        class SDKVersion : IComparable<SDKVersion>
        {
            public string sdkVersion;
            public int major;
            public string minor;
            //public string build;

            public int CompareTo(SDKVersion other)
            {
                if (major > other.major)
                {
                    return -1;
                }
                if (major < other.major)
                {
                    return 1;
                }
                if (major == other.major)
                {
                    return -minor.CompareTo(other.minor);
                }
                return 0;
            }
        }
    }
}
