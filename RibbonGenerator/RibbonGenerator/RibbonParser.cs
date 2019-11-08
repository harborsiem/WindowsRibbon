using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Xml;
using System.IO;

namespace RibbonGenerator
{
    public class RibbonParser
    {
        private const string CommandNameAttribute = "CommandName";
        private const string NameAttribute = "Name";
        private const string IdAttribute = "Id";
        private const string SymbolAttribute = "Symbol";
        private const string ApplicationModesAttribute = "ApplicationModes";

        private XmlNode applicationCommands;
        private XmlNode applicationViewsRibbon;
        private XmlNode applicationViewsContextPopup;

        //lists build from .xml file
        //in pair1List are only keys and values they are not in pair3List
        private List<KeyValuePair<string, string>> pair1List = new List<KeyValuePair<string, string>>(); //Command Name, Command Id
        private List<KeyValuePair<string, string>> pair2List = new List<KeyValuePair<string, string>>(); //Command Name, RibbonControl class name

        //list build from .h file
        private List<KeyValuePair<string, string>> pair3List = new List<KeyValuePair<string, string>>(); //Command Name, Command Id

        private List<string> popupCommandNames = new List<string>(); //
        private uint allApplicationModes;
        private bool hasHFile;

        private List<RibbonItem> ribbonItems;
        public ParseResult Results { get; private set; }

        public RibbonParser(string path)
        {
            ribbonItems = new List<RibbonItem>();
            ParseHFile(Path.ChangeExtension(path, "h"));
            PreParseXmlFile(path);
            Parse();
            Results = new ParseResult(this);
        }

        private void PreParseXmlFile(string path)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);
            XmlNode node = xmlDocument.DocumentElement;
            XmlNodeList applicationList = node.ChildNodes;
            XmlNode applicationNode;
            for (int i = 0; i < applicationList.Count; i++)
            {
                applicationNode = applicationList.Item(i);
                if (applicationNode.Name == "Application.Commands")
                {
                    applicationCommands = applicationNode;
                }
                if (applicationNode.Name == "Application.Views")
                {
                    XmlNodeList viewsList = applicationNode.ChildNodes;
                    XmlNode viewsNode;
                    for (int j = 0; j < viewsList.Count; j++)
                    {
                        viewsNode = viewsList.Item(j);
                        if (viewsNode.Name == "Ribbon")
                        {
                            applicationViewsRibbon = viewsNode;
                        }
                        else if (viewsNode.Name == "ContextPopup")
                        {
                            applicationViewsContextPopup = viewsNode;
                        }
                    }
                    break;
                }
            }
        }

        private void ParseHFile(string path)
        {
            pair3List.Clear();
            hasHFile = false;
            if (!File.Exists(path))
                return;
            StreamReader sr = File.OpenText(path);
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (line.StartsWith("#define"))
                {
                    if (!line.Contains("_RESID "))
                    {
                        line = line.Remove(0, "#define ".Length).TrimEnd();
                        string[] lineSplit = line.Split(' ');
                        if (lineSplit.Length != 2)
                        {
                            throw new ArgumentException("Error in .h file");
                        }
                        pair3List.Add(new KeyValuePair<string, string>(lineSplit[0], lineSplit[1]));
                    }
                }
            }
            sr.Close();
            hasHFile = true;
        }

        private void Parse()
        {
            ParseApplicationCommands(applicationCommands);
            ParseRibbon(applicationViewsRibbon);
            ParseContextPopup(applicationViewsContextPopup);

            IList<KeyValuePair<string, string>> pairs = MergeCommandIdLists();
            uint id;
            RibbonItem item;
            for (int i = 0; i < pair2List.Count; i++)
            {
                string commandName = pair2List[i].Key;
                string ribbonClassName = pair2List[i].Value;
                int index = ContainsKey(pairs, commandName);
                if (index == -1)
                    throw new ArgumentException("unresolved CommandName");
                string commandId = pairs[index].Value;
                id = GetCommandId(pairs, commandName);
                item = new RibbonItem(commandName, ribbonClassName, id);
                ribbonItems.Add(item);
            }
            for (int i = 0; i < popupCommandNames.Count; i++)
            {
                id = GetCommandId(pairs, popupCommandNames[i]);
                item = new RibbonItem(popupCommandNames[i], "ContextPopup", id);
                ribbonItems.Add(item);
            }
        }

        private void ParseApplicationCommands(XmlNode node)
        {
            XmlNodeList list = node.ChildNodes;
            for (int i = 0; i < list.Count; i++)
            {
                ParseCommand(list[i]);
            }
        }

        private void ParseCommand(XmlNode node)
        {
            String nodeName = node.Name;
            String name = null;
            String idOrSymbol = null;
            XmlAttributeCollection parms = node.Attributes;
            if (parms != null && parms.Count > 0)
            {
                XmlNode nameAttr = parms.GetNamedItem(NameAttribute);
                if (nameAttr != null)
                {
                    name = nameAttr.Value;
                }
                XmlNode idAttr = parms.GetNamedItem(IdAttribute);
                if (idAttr != null)
                {
                    idOrSymbol = idAttr.Value;
                }
                else
                {
                    XmlNode symbolAttr = parms.GetNamedItem(SymbolAttribute);
                    if (symbolAttr != null)
                    {
                        idOrSymbol = symbolAttr.Value;
                    }
                }
                if (!string.IsNullOrEmpty(idOrSymbol))
                {
                    if (ContainsKey(pair3List, name) == -1)
                    {
                        pair1List.Add(new KeyValuePair<string, string>(name, idOrSymbol));
                    }
                }
            }
        }

        private void ParseRibbon(XmlNode node)
        {
            XmlNodeList nodeList = node.ChildNodes;
            if (nodeList.Count == 0)
                return;

            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlNode child = nodeList.Item(i);
                string commandName = GetCommandName(child);
                uint applicationModes = GetApplicationModes(child, commandName);
                allApplicationModes |= applicationModes;
                ParseRibbon(child);
            }
        }

        private string GetCommandName(XmlNode child)
        {
            String name = string.Empty;
            String nodeName = child.Name;
            XmlAttributeCollection parms = child.Attributes;
            if (parms != null && parms.Count > 0)
            {
                XmlNode attr = parms.GetNamedItem(CommandNameAttribute);
                if (attr != null)
                {
                    name = attr.Value;
                    if ((ContainsKey(pair1List, name) >= 0 || ContainsKey(pair3List, name) >= 0) && (ContainsKey(pair2List, name) == -1))
                    {
                        pair2List.Add(new KeyValuePair<string, string>(name, "Ribbon" + nodeName));
                    }
                }
            }
            return name;
        }

        private uint GetApplicationModes(XmlNode child, string commandName)
        {
            uint applicationModes = 0;
            String value = null;
            XmlAttributeCollection parms = child.Attributes;
            if (parms != null && parms.Count > 0)
            {
                XmlNode attr = parms.GetNamedItem(ApplicationModesAttribute);
                if (attr != null)
                {
                    value = attr.Value;
                    string[] modes = value.Split(',');
                    for (int i = 0; i < modes.Length; i++)
                    {
                        int modeValue;
                        if (int.TryParse(modes[i].Trim(), out modeValue))
                        {
                            if (modeValue >= 0 && modeValue <= 31)
                            {
                                applicationModes |= (uint)(1 << modeValue);
                            }
                        }
                    }
                }
            }
            return applicationModes;
        }

        private void ParseContextPopup(XmlNode node)
        {
            if (node == null)
                return;

            XmlNodeList nodeList = node.ChildNodes;
            if (nodeList.Count == 0)
                return;

            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlNode child = nodeList.Item(i);
                if (child.Name == "ContextPopup.ContextMaps")
                {
                    XmlNodeList childList = child.ChildNodes;
                    for (int j = 0; j < childList.Count; j++)
                    {
                        XmlNode cChild = childList.Item(j);
                        String nodeName = cChild.Name;
                        XmlAttributeCollection parms = cChild.Attributes;
                        if (parms != null && parms.Count > 0)
                        {
                            XmlNode attr = parms.GetNamedItem(CommandNameAttribute);
                            if (attr != null)
                            {
                                popupCommandNames.Add(attr.Value);
                            }
                        }
                    }
                }
                else
                {
                    ParseRibbon(child);
                }
            }
        }

        private int ContainsKey(IList<KeyValuePair<string, string>> list, string key)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Key.Equals(key))
                    return i;
            }
            return -1;
        }

        private IList<KeyValuePair<string, string>> MergeCommandIdLists()
        {
            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>(pair3List);
            pairs.AddRange(pair1List);
            return pairs;
        }

        private uint GetCommandId(IList<KeyValuePair<string, string>> pairs, string commandName)
        {
            int index;
            string value = string.Empty;
            index = ContainsKey(pairs, commandName);
            if (index >= 0)
            {
                value = pairs[index].Value;
            }
            index = ContainsKey(pairs, value);
            if (index >= 0)
            {
                value = pairs[index].Value;
            }
            if (!string.IsNullOrEmpty(value))
            {
                if (value.ToUpperInvariant().StartsWith("0X"))
                    return uint.Parse(value.Substring(2), NumberStyles.AllowHexSpecifier);
                return uint.Parse(value);
            }
            return 0;
        }

        public class ParseResult
        {
            public ParseResult(RibbonParser parser)
            {
                Pair1List = parser.pair1List.AsReadOnly();
                Pair2List = parser.pair2List.AsReadOnly();
                Pair3List = parser.pair3List.AsReadOnly();
                PopupCommandNames = parser.popupCommandNames.AsReadOnly();
                HasHFile = parser.hasHFile;
                AllApplicationModes = parser.allApplicationModes;
                RibbonItems = parser.ribbonItems.AsReadOnly();
            }

            //lists build from .xml file
            //in pair1List are only keys and values they are not in pair3List
            public IList<KeyValuePair<string, string>> Pair1List; //Command Name, Command Id
            public IList<KeyValuePair<string, string>> Pair2List; //Command Name, RibbonControl class name

            //list build from .h file
            public IList<KeyValuePair<string, string>> Pair3List; //Command Name, Command Id

            public IList<string> PopupCommandNames; //
            public IList<RibbonItem> RibbonItems { get; private set; }

            public bool HasHFile { get; private set; }
            public uint AllApplicationModes { get; private set; }
        }
    }
}
