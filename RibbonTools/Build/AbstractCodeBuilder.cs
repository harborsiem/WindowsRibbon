//#define OldCode
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Xml;

namespace UIRibbonTools
{
    public abstract class AbstractCodeBuilder
    {
        private const string IndentString = "    ";
        private const string RibbonItems = "RibbonItems";

#if OldCode
        //lists build from .xml file
        //in pair1List are only keys and values they are not in pair3List
        protected IList<KeyValuePair<string, string>> pair1List; //Command Name, Command Id
        protected IList<KeyValuePair<string, string>> pair2List; //Command Name, RibbonControl class name

        //list build from .h file
        protected IList<KeyValuePair<string, string>> pair3List; //Command Name, Command Id
#endif
        protected IList<RibbonItem> popupRibbonItems; //

        protected IList<RibbonItem> ribbonItems;

        protected string ribbonItemsClass;
        protected KeyValuePair<string, uint>? _qatCustomizeCommand;
        protected bool hasHFile;

        protected StreamWriter sw;

        protected AbstractCodeBuilder() { }

        /// <summary>
        /// Method builds a C# file RibbonItems.Designer.cs
        /// </summary>
        /// <param name="path">RibbonMarkup.xml with path</param>
        public void Execute(string path, RibbonParser parser, bool advWrapperClassFile)
        {
            //string @namespace = System.Reflection.Assembly.GetEntryAssembly().EntryPoint.DeclaringType.Namespace;
            if (File.Exists(path))
            {
                string directory = Path.GetDirectoryName(path);

                if (advWrapperClassFile)
                {
                    ribbonItemsClass = Path.GetFileNameWithoutExtension(path);
                }
                else
                {
                    string xmlFileName = Path.GetFileNameWithoutExtension(path);
                    char last = xmlFileName[xmlFileName.Length - 1];
                    if (Char.IsNumber(last))
                    {
                        ribbonItemsClass = RibbonItems + last.ToString();
                    }
                    else
                    {
                        ribbonItemsClass = RibbonItems;
                    }
                }

                RibbonParser.ParseResult results = parser.Results;
#if OldCode
                pair1List = (results.Pair1List);
                pair2List = (results.Pair2List);
                pair3List = (results.Pair3List);
                popupCommandNames = (results.PopupCommandNames);
#else
                popupRibbonItems = new List<RibbonItem>();
#endif
                _qatCustomizeCommand = results.QatCustomizeCommand;
                ribbonItems = results.RibbonItems;
                hasHFile = results.HasHFile;

                SetStreamWriter(directory);
                WriteCodeFile();
            }
        }

        protected String Indent(int count)
        {
            StringBuilder result = new StringBuilder(string.Empty);
            for (int i = 0; i < count; i++)
            {
                result.Append(IndentString);
            }
            return result.ToString();
        }

        protected abstract void SetStreamWriter(string path);

        private void WriteCodeFile()
        {
            WriteHeader();
            WriteConst();
            WritePopupConst();
            WriteProperties();
            WriteConstructor();
            CloseCodeFile();
        }

        protected abstract void WriteHeader();

        protected abstract void WriteConst();

        protected abstract void WritePopupConst();

        protected abstract void WriteProperties();

        protected abstract void WriteConstructor();

        protected abstract void CloseCodeFile();

        protected string GetPropertyName(string commandName)
        {
            string result;
            if (commandName.StartsWith("cmd", StringComparison.OrdinalIgnoreCase))
            {
                result = commandName.Substring(3);
            }
            else
            {
                result = commandName;
            }
            return result;
        }
    }
}
