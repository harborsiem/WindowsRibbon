using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Resources;
using System.Reflection;
using System.Xml.Serialization;

namespace RibbonLib
{
    static class Util
    {
        /// <summary>
        /// Contains true, if we are in design mode of Visual Studio
        /// </summary>
        private static bool _designMode;

        /// <summary>
        /// Initializes an instance of Util class
        /// </summary>
        static Util()
        {
            // design mode is true if host process is: Visual Studio, Visual Studio Express Versions (C#, VB, C++) or SharpDevelop
            var designerHosts = new List<string>() { "designtoolsserver", "devenv", "vcsexpress", "vbexpress", "vcexpress", "sharpdevelop" };
            var processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower();
            _designMode = designerHosts.Contains(processName);
        }

        /// <summary>
        /// Gets true, if we are in design mode of Visual Studio
        /// </summary>
        /// <remarks>
        /// In Visual Studio 2008 SP1 the designer is crashing sometimes on windows forms. 
        /// The DesignMode property of Control class is buggy and cannot be used, so use our own implementation instead.
        /// </remarks>
        public static bool DesignMode
        {
            get
            {
                return _designMode;
            }
        }

        public static byte[] GetEmbeddedResource(string resourceName, Assembly assembly)
        {
            string[] resNames = assembly.GetManifestResourceNames();
            if (!resNames.Contains(resourceName))
            {
                return null;
            }
            Stream stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                return null;
            }
            try
            {
                var buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
            finally
            {
                stream.Close();
            }
        }

        public static T DeserializeEmbeddedResource<T>(string resourceName, Assembly assembly) where T : class
        {
            string[] resNames = assembly.GetManifestResourceNames();
            if (!resNames.Contains(resourceName))
            {
                return null;
            }
            Stream stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                return null;
            }
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                var result = (T)serializer.Deserialize(stream);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                stream.Close();
            }
        }
    }
}
