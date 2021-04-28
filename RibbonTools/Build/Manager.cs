using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace UIRibbonTools
{
    public class Manager
    {
        public const string RESXEXTENSION = ".resx";
        public const string RIBBONEXTENSION = ".ribbon";

        private IMessageOutput _output;

        public Manager(IMessageOutput output, string ribbonXmlFilename, string ribbonXmlContent)
        {
            _output = output;

            _ribbonXmlFilename = ribbonXmlFilename;
            _ribbonXmlContent = ribbonXmlContent;

            Initialize();
        }

        private List<string> _cleanupFiles = new List<string>();

        public List<string> CleanupFiles
        {
            get { return _cleanupFiles; }
            private set { _cleanupFiles = value; }
        }

        private string _ribbonXmlFilename;

        public string RibbonXmlFilename
        {
            get
            {
                return _ribbonXmlFilename;
            }
        }

        private string _ribbonXmlContent;

        public string RibbonXmlContent
        {
            get
            {
                return _ribbonXmlContent;
            }
        }

        private List<Target> _targets;

        public List<Target> Targets
        {
            get
            {
                return _targets;
            }
        }

        /// <summary>
        /// Returns ".ribbon" for empty culturename and ".de.ribbon" for cultureName 'de'.
        /// </summary>
        /// <param name="cultureName">the name of the culture</param>
        /// <returns>the localized ribbon file extension</returns>
        private string GetRibbonExtension(string cultureName)
        {
            if (string.IsNullOrEmpty(cultureName))
                return RIBBONEXTENSION;

            var result = string.Format(".{0}{1}", cultureName, RIBBONEXTENSION);
            return result;
        }

        private void Initialize()
        {
            try
            {
                string path = Path.GetDirectoryName(RibbonXmlFilename);
                if (string.IsNullOrEmpty(path))
                {
                    path = @".\";
                }
                string filenameWithoutExtension = Path.GetFileNameWithoutExtension(RibbonXmlFilename);

                string fullFilenameWithoutExtension = Path.Combine(path, filenameWithoutExtension);
                string resourceFullFilename = AddFileExtension(fullFilenameWithoutExtension, RESXEXTENSION);

                List<Target> targets = new List<Target>();

                var localize = File.Exists(resourceFullFilename);

                // create the default target = .ribbon file
                var target = new Target()
                {
                    Localize = localize,
                    ResourceFilename = localize ? resourceFullFilename : null,
                    RibbonFilename = AddFileExtension(fullFilenameWithoutExtension, GetRibbonExtension(null))
                };

                targets.Add(target);

                // search for localized ResX files
                string searchPattern = string.Format("{0}.*{1}", Path.GetFileName(fullFilenameWithoutExtension), RESXEXTENSION);

                var localizedFiles = Directory.GetFiles(path, searchPattern);
                foreach (var file in localizedFiles)
                {
                    // create localized targets = for example: .de.ribbon file
                    string cultureName = GetCultureName(file);
                    target = new Target()
                    {
                        Localize = true,
                        CultureName = cultureName,
                        RibbonFilename = AddFileExtension(fullFilenameWithoutExtension, GetRibbonExtension(cultureName)),
                        ResourceFilename = file
                    };
                    targets.Add(target);
                }

                _targets = targets;

                // if there are ResX files for the ribbons create a ResXReader
                if (target.Localize)
                    _resXReader = new ResXReader(targets);

                Util.LogMessage("Manager.Initialize returns {0} targets and localize set to {1}", targets.Count, _resXReader != null);
            }
            catch (Exception ex)
            {
                Util.LogError(ex);
                throw;
            }
        }

        private string GetCultureName(string file)
        {
            var firstExtensionExcluded = Path.GetFileNameWithoutExtension(file);
            var cultureNameExtension = Path.GetExtension(firstExtensionExcluded);
            if (string.IsNullOrEmpty(cultureNameExtension) || !cultureNameExtension.StartsWith("."))
                return null;
            var cultureName = cultureNameExtension.Substring(1);
            return cultureName;
        }

        private string AddFileExtension(string fullFilenameWithoutExtension, string extension)
        {
            return string.Format("{0}{1}", fullFilenameWithoutExtension, extension);
        }

        private ResXReader _resXReader;

        private ResXReader ResXReader
        {
            get
            {
                return _resXReader;
            }
        }

        public byte[] CreateRibbon(Target element, string resourceIdentifier)
        {
            string localizedRibbonXmlFilename = null;
            CleanupFiles.Clear();

            RibbonCompiler compiler = new RibbonCompiler(this);
            try
            {
                // use the following format to specify localization info {Resource:key}
                localizedRibbonXmlFilename = LocalizeRibbon(element);
                string ribbonDll = null;
                byte[] result;

                RibbonCompileResult compileResult = compiler.Compile(localizedRibbonXmlFilename, resourceIdentifier);
                if (compileResult == RibbonCompileResult.Ok)
                {
                    ribbonDll = compiler.RibbonDll;
                    result = File.ReadAllBytes(ribbonDll);
                }
                else
                {
                    Util.LogMessage("Error: " + compileResult.ToString());
                    result = new byte[0];
                }
                for (int i = 0; i < compiler.Messages.Count; i++)
                {
                    if (!string.IsNullOrEmpty(compiler.Messages[i]))
                        _output.WriteLine(compiler.Messages[i]);
                }
                Util.LogMessage("Manager.CreateRibbon returns {0} bytes for file '{1}'", result.Length, ribbonDll);
                return result;
            }
            catch (Exception ex)
            {
                for (int i = 0; i < compiler.Messages.Count; i++)
                {
                    if (!string.IsNullOrEmpty(compiler.Messages[i]))
                        _output.WriteLine(compiler.Messages[i]);
                }
                Util.LogError(ex);
                throw;
            }
            finally
            {
                Cleanup();
                if (localizedRibbonXmlFilename != null)
                {
                    string firstExtensionExcluded = Path.GetFileNameWithoutExtension(localizedRibbonXmlFilename);
                    string dotCulture = Path.GetExtension(firstExtensionExcluded);
                    if (!string.IsNullOrEmpty(dotCulture) && dotCulture.Equals(".default", StringComparison.OrdinalIgnoreCase))
                    {
                        string defaultH = Path.ChangeExtension(localizedRibbonXmlFilename, ".h");
                        if (File.Exists(defaultH))
                        {
                            string neutralHFile = Path.Combine(Path.GetDirectoryName(localizedRibbonXmlFilename), Path.ChangeExtension(firstExtensionExcluded, ".h"));
                            File.Delete(neutralHFile);
                            File.Move(defaultH, neutralHFile); // rename *.default.h to *.h
                        }
                        string defaultRes = Path.ChangeExtension(localizedRibbonXmlFilename, ".res");
                        if (File.Exists(defaultRes))
                        {
                            string neutralResFile = Path.Combine(Path.GetDirectoryName(localizedRibbonXmlFilename), Path.ChangeExtension(firstExtensionExcluded, ".res"));
                            File.Delete(neutralResFile);
                            File.Move(defaultRes, neutralResFile); // rename *.default.res to *.res
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Delete all temporary files created during CreateRibbon
        /// </summary>
        private void Cleanup()
        {
            //if (!Settings.Instance.DeleteTempFiles) return;

            foreach (string cleanupFile in this.CleanupFiles)
            {
                try
                {
                    if (File.Exists(cleanupFile))
                        File.Delete(cleanupFile);
                }
                catch (Exception ex)
                {
                    Util.LogError(new ArgumentException(string.Format("Cleanup fails for file '{0}'", cleanupFile), ex));
                }
            }
        }

        private string LocalizeRibbon(Target element)
        {
            if (!element.Localize)
                return this.RibbonXmlFilename;

            var localizedContent = this.RibbonXmlContent;
            StringBuilder sb = new StringBuilder();

            this.ResXReader.SetCulture(element.CultureName);

            int pos = 0;
            const string LOCALIZEBEGINTOKEN = "{Resource:";
            const string LOCALIZEENDTOKEN = "}";
            while (true)
            {
                int nextTokenBegin = localizedContent.IndexOf(LOCALIZEBEGINTOKEN, pos);
                if (nextTokenBegin < 0)
                    break;

                int nextTokenEnd = localizedContent.IndexOf(LOCALIZEENDTOKEN, nextTokenBegin);
                if (nextTokenEnd < 0)
                    break;

                int tokenLength = nextTokenEnd - nextTokenBegin + 1;
                string token = localizedContent.Substring(nextTokenBegin, tokenLength);

                //if (token.Contains("Home_"))
                //    System.Diagnostics.Debugger.Break();

                int resourceKeyBegin = nextTokenBegin + LOCALIZEBEGINTOKEN.Length;
                int resourceKeyLength = nextTokenEnd - resourceKeyBegin;

                string resourceKey = localizedContent.Substring(resourceKeyBegin, resourceKeyLength);

                string localizedString = this.ResXReader.GetString(resourceKey);
                localizedContent = localizedContent.Replace(token, localizedString);

                pos++;
            }

            string cultureName = !string.IsNullOrEmpty(element.CultureName) ? element.CultureName : "default";
            string localizedFilename = Path.ChangeExtension(this.RibbonXmlFilename, string.Format(".{0}.xml", cultureName));
            File.WriteAllText(localizedFilename, localizedContent);
            if (!cultureName.Equals("default", StringComparison.OrdinalIgnoreCase))
                this.CleanupFiles.Add(Path.ChangeExtension(localizedFilename, ".h"));
            this.CleanupFiles.Add(localizedFilename);
            return localizedFilename;
        }
    }
}
