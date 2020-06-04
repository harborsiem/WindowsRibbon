using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using Microsoft.Win32;

namespace UIRibbonTools
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
                    //File.Delete(LogFile);
                    info.Delete();
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
        /// Gets the log filename. For example: C:\Users\<user>\AppData\Local\RibbonTools\RibbonTools.log
        /// </summary>
        public static string LogFile
        {
            get
            {
                if (_logFile == null)
                    _logFile = Path.Combine(RibbonToolsLocalAppData, "RibbonTools.log");
                return _logFile;
            }
        }

        /// <summary>
        /// Gets the local app data path for ribbontools. For example: C:\Users\<user>\AppData\Local\RibbonTools\
        /// </summary>
        public static string RibbonToolsLocalAppData
        {
            get
            {
                string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string path = Path.Combine(localAppData, "RibbonTools");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
        }
    }
}
