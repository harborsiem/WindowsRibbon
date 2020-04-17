using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Xml.Schema;
using System.Xml.Linq;
using RibbonGenerator;

namespace UIRibbonTools
{
    class BuildPreviewHelper
    {
        public const string Neutral = "Neutral";

        //private Action<bool> _buildActionEnabled;
        private Action<bool> _previewActionEnabled;
        //private Action<string> _setText;
        private Action<IList<string>> _setLanguages;
        private string _uiccXsdPath = string.Empty;
        private string _selectedCulture = Neutral;
        private Action<MessageKind, string> _log;

        public bool HasValidParser
        {
            get { return (Parser != null); }
            set
            {
                if (!value)
                    Parser = null;
                else if (Parser == null)
                    Parser = new RibbonParser(XmlRibbonFile);
            }
        }

        public static BuildPreviewHelper Instance = new BuildPreviewHelper();

        private BuildPreviewHelper()
        {
            //string sdkPath = null;
            //sdkPath = Util.DetectAppropriateWindowsSdkPath();
            //uiccXsdPath = Path.Combine(sdkPath, "UICC.xsd");
        }

        public void ShowDialog(Form form)
        {
            if (Parser == null)
                Parser = new RibbonParser(XmlRibbonFile);
            SetPreviewUiCulture();
            PreviewForm dialog = new PreviewForm();
            dialog.ShowDialog(form);
            ResetPreviewUiCulture();
        }

        private void SetPreviewUiCulture()
        {
            if (_selectedCulture.ToUpperInvariant().Equals(Neutral.ToUpperInvariant()))
                Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
            else
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(_selectedCulture);
        }

        private void ResetPreviewUiCulture()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InstalledUICulture;
        }

        public string UICulture
        {
            get { return _selectedCulture; }
            set { _selectedCulture = value; }
        }

        public RibbonParser Parser { get; private set; }

        public void SetActions(Action<bool> buildActionEnabled, Action<bool> previewActionEnabled, Action<MessageKind, string> log
            , Action<IList<string>> setLanguages)
        {
            //this._buildActionEnabled = buildActionEnabled;
            this._previewActionEnabled = previewActionEnabled;
            this._log = log;
            this._setLanguages = setLanguages;
            //buildActionEnabled(false);
            //previewActionEnabled(false);
        }

        public void SetRibbonXmlFile(string path)
        {
            string validateMsg = null;
            //bool buildEnabled = false;
            bool previewEnabled = false;
            if (File.Exists(path))
            {
                //validateMsg = Validation(path);
                bool validate = (string.IsNullOrEmpty(validateMsg) ? true : false);
                if (validate)
                {
                    XmlRibbonFile = path;
                    //buildEnabled = true;
                    _setLanguages(FindLanguages(path));
                    _selectedCulture = BuildPreviewHelper.Neutral;
                    previewEnabled = CheckRibbonResource(path);
                }
                else
                {
                    _log(MessageKind.Pipe, validateMsg);
                    MessageBox.Show("Xml is not valid", "XML Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //buildActionEnabled(buildEnabled);
            _previewActionEnabled(previewEnabled);
        }

        private IList<string> FindLanguages(string path)
        {
            string directory = Path.GetDirectoryName(path);
            string markup = Path.GetFileNameWithoutExtension(path);
            string[] files = Directory.GetFiles(directory, markup + ".*.resx", SearchOption.TopDirectoryOnly);
            List<string> languages = new List<string>();
            for (int i = 0; i < files.Length; i++)
            {
                string languageMarkup = Path.GetFileNameWithoutExtension(files[i]);
                int index = languageMarkup.LastIndexOf('.');
                if (index >= 0)
                {
                    languages.Add(languageMarkup.Substring(index + 1));
                }
            }
            return languages;
        }

        private bool CheckRibbonResource(string path)
        {
            bool previewEnabled = false;
            string ribbonResourceFileName = Path.ChangeExtension(path, "ribbon");
            if (File.Exists(ribbonResourceFileName))
            {
                SetRibbonResourceName(ribbonResourceFileName);
                previewEnabled = true;
                if (Parser == null)
                    Parser = new RibbonParser(path);
            }
            return previewEnabled;
        }

        private void SetRibbonResourceName(string path)
        {
            RibbonResourceName = "file://" + path;
        }

        /// <summary>
        /// MainForm sets the name, PreviewForm uses it for Ribbon.ResourceName
        /// ResourceName starts with "file://". It's a file based resource
        /// </summary>
        public string RibbonResourceName { get; private set; }

        public string XmlRibbonFile { get; private set; }

        private string Validation(string path)
        {
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add("http://schemas.microsoft.com/windows/2009/Ribbon", _uiccXsdPath);

            XDocument doc = XDocument.Load(path);
            string msg = string.Empty;
            doc.Validate(schemas, (sender, e) =>
            {
                msg += e.Message + Environment.NewLine;
            });
            return msg;
            //Console.WriteLine(msg == "" ? "Document is valid" : "Document invalid: " + msg);
        }

        public void BuildRibbonFile()
        {
            MessageOutput message = null;
            try
            {
                string path = XmlRibbonFile;
                string content = File.ReadAllText(path);
                Manager manager = new Manager(message = new MessageOutput(), path, content);

                var targets = manager.Targets;
                foreach (var target in targets)
                {
                    var buffer = manager.CreateRibbon(target);
                    File.WriteAllBytes(target.RibbonFilename, buffer);
                }
                bool previewEnabled = CheckRibbonResource(path);
                _previewActionEnabled(previewEnabled);

                // create the C# file RibbonItems.Designer.cs
                if (previewEnabled)
                    new CSharpCodeBuilder().Execute(path, Parser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (message != null)
                {
                    string allMsg = message.GetString();
                    _log(MessageKind.Pipe, allMsg);
                    BuildLogFile(allMsg);
                    message.Close();
                }
            }
        }

        private void BuildLogFile(string logging)
        {
            string fileName = XmlRibbonFile;
            StringReader sr = new StringReader(logging);
            StreamWriter sw = File.CreateText(Path.ChangeExtension(fileName, "log"));
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                sw.WriteLine(line);
            }
            sw.Close();
            sr.Close();
        }
    }
}
