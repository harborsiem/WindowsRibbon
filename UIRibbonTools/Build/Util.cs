using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Diagnostics;
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
            return null;
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
                    }
                    else
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

        /// <summary>
        /// Build the Linker command
        /// </summary>
        /// <returns></returns>
        public static string GetLinkerCommand()
        {
            const string linkerAddString = " /VERBOSE /NOENTRY /DLL /MACHINE:X86 /OUT:\"{DllFilename}\" \"{ResFilename}\"";
            string linkerCommand = null;
            LinkPaths linkPaths = new LinkPaths();
            bool result = DetectLatestVSVersion(linkPaths);
            if (result)
            {
                if (File.Exists(linkPaths.VcVars32Bat))
                    result = true;
                else result = false;
                if (result && File.Exists(linkPaths.VcLinkExe))
                    result = true;
                else result = false;
            }
            if (!result)
            {
                linkerCommand = null;
                //linkerCommand = "(\"{VcVars32Bat}\") && (\"{VcLinkExe}\"" + linkerAddString + ")";
            }
            else
            {
                if (string.IsNullOrEmpty(linkPaths.EnvironmentVcLinkExe))
                    linkerCommand = "(\"" + linkPaths.VcVars32Bat + "\") && (\"" + linkPaths.VcLinkExe + "\"" + linkerAddString + ")";
                else
                    linkerCommand = "(\"" + linkPaths.EnvironmentVcVars32Bat + "\") && (\"" + linkPaths.EnvironmentVcLinkExe + "\"" + linkerAddString + ")";
            }
            return linkerCommand;
        }

        private static string GetMSVCToolsVersion(string visualStudioInstallRoot)
        {
            string filePath = Path.Combine(visualStudioInstallRoot, "VC", "Auxiliary", "Build", "Microsoft.VCToolsVersion.default.txt");
            return File.ReadAllText(filePath).Trim();
        }

        public static string[] DetectMSVCVersion()
        {
            string[] lines = GetVsWhereInfo("-latest");
            if (lines != null)
            {
                string installRoot = null;
                foreach (string line in lines)
                {
                    if (line.StartsWith("installationPath:"))
                    {
                        installRoot = line.Substring("installationPath:".Length).TrimStart();
                        string msvcVersion = GetMSVCToolsVersion(installRoot);
                        return new string[] { msvcVersion , installRoot };
                    }
                }
            }
            return new string[0];
        }

        private static bool DetectLatestVSVersion(LinkPaths linkPaths)
        {
            bool result = false;
            string vcvars32Bat = string.Empty;
            string vcLinkExe = string.Empty;

            string[] lines = GetVsWhereInfo("-latest");
            if (lines != null)
            {
                string installRoot = null;
                foreach (string line in lines)
                {
                    if (line.StartsWith("installationPath:"))
                    {
                        installRoot = line.Substring("installationPath:".Length).TrimStart();
                        vcvars32Bat = Path.Combine(installRoot, "VC", "Auxiliary", "Build", "vcvars32.bat");
                        string msvcVersion = GetMSVCToolsVersion(installRoot);
                        vcLinkExe = Path.Combine(installRoot, "VC", "Tools", "MSVC", msvcVersion, "bin", "Hostx86", "x86", "link.exe");
                        linkPaths.VcVars32Bat = vcvars32Bat;
                        linkPaths.VcLinkExe = vcLinkExe;
                        linkPaths.EnvironmentVcVars32Bat = string.Empty;
                        linkPaths.EnvironmentVcLinkExe = string.Empty;
                        result = true;
                        break;
                    }
                }
            }
            else
                result = GetLatestOlderVSVersion(linkPaths);

            return result;
        }

        private static string[] GetVsWhereInfo(string option)
        {
            string[] lines = null;
            string programFiles = Environment.ExpandEnvironmentVariables(string.Format(@"%PROGRAMFILES{0}%", Environment.Is64BitOperatingSystem ? "(x86)" : ""));
            string vswhereExe = Path.Combine(programFiles, "Microsoft Visual Studio", "Installer", "vswhere.exe");
            if (File.Exists(vswhereExe))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(vswhereExe);
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                startInfo.UseShellExecute = false;
                startInfo.Arguments = option;

                //if (RequiredWorkloads != null && RequiredWorkloads.Length > 0)
                //{
                //    startInfo.Arguments += " -requires " + string.Join(" ", RequiredWorkloads);
                //}

                Process proc = Process.Start(startInfo);
                proc.WaitForExit();
                if (proc.ExitCode != 0) throw new InvalidOperationException($"vswhere.exe exited with code {proc.ExitCode}");

                string allOutput = proc.StandardOutput.ReadToEnd();
                lines = allOutput.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            }
            return lines;
        }

        private static bool GetLatestOlderVSVersion(LinkPaths linkPaths)
        {
            if (GetPaths("%VS140COMNTOOLS%", linkPaths))
            {
                return true;
            }
            if (GetPaths("%VS120COMNTOOLS%", linkPaths))
            {
                return true;
            }
            if (GetPaths("%VS110COMNTOOLS%", linkPaths))
            {
                return true;
            }
            if (GetPaths("%VS100COMNTOOLS%", linkPaths))
            {
                return true;
            }
            return false;
        }

        private static bool GetPaths(string envVsVers, LinkPaths linkPaths)
        {
            bool result = false;
            string vsVers = Environment.ExpandEnvironmentVariables(envVsVers);
            string vcvars32Bat = string.Empty;
            string vcLinkExe = string.Empty;
            if (!vsVers.StartsWith("%VS"))
            {
                vcvars32Bat = Path.GetFullPath(Path.Combine(vsVers, @"..\..\VC\bin\vcvars32.bat"));
                vcLinkExe = Path.GetFullPath(Path.Combine(vsVers, @"..\..\VC\bin\link.exe"));
                result = true;
            }
            linkPaths.VcVars32Bat = vcvars32Bat;
            linkPaths.VcLinkExe = vcLinkExe;
            linkPaths.EnvironmentVcVars32Bat = envVsVers + @"..\..\VC\bin\vcvars32.bat";
            linkPaths.EnvironmentVcLinkExe = envVsVers + @"..\..\VC\bin\link.exe";
            return result;
        }

        private static List<VSVersion> GetInstalledVsVersions()
        {
            List<VSVersion> versions = new List<VSVersion>();
            string[] lines = GetVsWhereInfo("-sort");
            int i = 0;
            VSVersion version;
            while (i < lines.Length)
            {
                if (lines[i++].StartsWith("instanceId"))
                {
                    version = new VSVersion();
                    for (; i < lines.Length; i++)
                    {
                        string line = lines[i];
                        if (line.StartsWith("installationPath:"))
                        {
                            version.InstallationPath = line.Substring("installationPath:".Length).TrimStart();
                            int editionStart = version.InstallationPath.LastIndexOf('\\') + 1;
                            if (editionStart > 0)
                            {
                                version.Edition = version.InstallationPath.Substring(editionStart);
                                version.ShortInstallationPath = version.InstallationPath.Substring(0, editionStart - 1);
                            }
                            int versionStart = version.ShortInstallationPath.LastIndexOf('\\') + 1;
                            if (versionStart > 0)
                            {
                                version.Version = version.ShortInstallationPath.Substring(versionStart);
                            }
                            versions.Add(version);
                            break;
                        }
                    }
                }
            }
            return versions;
        }

        public static string DEFAULTWINDOWS7SDKPATH = string.Format(@"%PROGRAMFILES{0}%\Microsoft SDKs\Windows\v7.1A\Bin\", Environment.Is64BitOperatingSystem ? "(x86)" : "");

        class LinkPaths
        {
            public string VcLinkExe { get; set; }
            public string VcVars32Bat { get; set; }
            public string EnvironmentVcLinkExe { get; set; }
            public string EnvironmentVcVars32Bat { get; set; }
        }

        class VSVersion
        {
            public string Version { get; set; }
            public string ShortInstallationPath { get; set; }
            public string InstallationPath { get; set; }
            public string Edition { get; set; }
        }

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
