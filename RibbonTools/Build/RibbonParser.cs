using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Xml;
using System.IO;

namespace UIRibbonTools
{
    public class RibbonParser
    {
        private const string CommandNameAttribute = "CommandName";
        private const string CustomizeCommandNameAttribute = "CustomizeCommandName";
        private const string NameAttribute = "Name";
        private const string IdAttribute = "Id";
        private const string SymbolAttribute = "Symbol";
        private const string ApplicationModesAttribute = "ApplicationModes";
        private const string RibbonString = "Ribbon";

        private XmlNode applicationCommands;
        private XmlNode applicationViewsRibbon;
        private XmlNode applicationViewsContextPopup;

        //lists build from .xml file
        //in pair1List are only keys and values they are not in pair3List
        private List<KeyValuePair<string, string>> pair1List = new List<KeyValuePair<string, string>>(); //Command Name, Command Id
        private List<KeyValuePair<string, string>> pair2List = new List<KeyValuePair<string, string>>(); //Command Name, RibbonControl class name

        //list build from .h file
        private List<KeyValuePair<string, string>> pair3List = new List<KeyValuePair<string, string>>(); //Command Name, Command Id
        private Dictionary<string, string> _commentPairs = new Dictionary<string, string>(); //Key: Command Name, Value: Comment

        private Dictionary<string, uint> commandIdPairs;

        private List<string> popupCommandNames = new List<string>(); //
        private KeyValuePair<string, uint>? _qatCustomizeCommand;

        private uint allApplicationModes;
        private bool hasHFile;

        private List<RibbonItem> ribbonItems;
        public ParseResult Results { get; private set; }

        public RibbonParser(string path)
        {
            _qatCustomizeCommand = null;
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
                        if (viewsNode.Name == RibbonString)
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
            _commentPairs.Clear();
            hasHFile = false;
            if (!File.Exists(path))
                return;
            StreamReader sr = File.OpenText(path);
            try
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (line.StartsWith("#define"))
                    {
                        if (!line.Contains("_RESID "))
                        {
                            line = line.Remove(0, "#define ".Length).TrimEnd();
                            string[] lineSplit = line.Split(' ');
                            if (lineSplit.Length < 2)
                            {
                                throw new ArgumentException("Error in .h file");
                            }
                            pair3List.Add(new KeyValuePair<string, string>(lineSplit[0], lineSplit[1]));
                            if (lineSplit.Length > 2)
                            {
                                string comment = line.Substring(line.IndexOf("/*")).Remove(0, 3);
                                comment = comment.Substring(0, comment.LastIndexOf(" */"));
                                _commentPairs.Add(lineSplit[0], comment);
                            }
                        }
                    }
                }
            }
            finally
            {
                sr.Close();
            }
            hasHFile = true;
        }

        private void Parse()
        {
            if (applicationCommands != null)
                ParseApplicationCommands(applicationCommands);

            commandIdPairs = GetCommandsAndIds();

            ParseRibbon(applicationViewsRibbon);
            ParseContextPopup(applicationViewsContextPopup);
            uint id;
            RibbonItem item;
            string comment;
            for (int i = 0; i < pair2List.Count; i++)
            {
                string commandName = pair2List[i].Key;
                string ribbonClassName = pair2List[i].Value;
                if (!commandIdPairs.TryGetValue(commandName, out id))
                    if (!uint.TryParse(commandName, out id))
                        throw new ArgumentException("Unresolved CommandName");
                    else commandName = "cmd" + ribbonClassName.Substring(RibbonString.Length) + commandName;
                item = new RibbonItem(commandName, ribbonClassName, id);
                if (_commentPairs.ContainsKey(commandName))
                {
                    if (_commentPairs.TryGetValue(commandName, out comment))
                        item.Comment = comment;
                }

                ribbonItems.Add(item);
            }
            for (int i = 0; i < popupCommandNames.Count; i++)
            {
                string commandName;
                commandName = popupCommandNames[i];
                if (!commandIdPairs.TryGetValue(commandName, out id))
                    if (!uint.TryParse(commandName, out id))
                        throw new ArgumentException("Unresolved CommandName");
                    else commandName = "_" + commandName;
                item = new RibbonItem(commandName, "ContextPopup", id);
                if (_commentPairs.ContainsKey(commandName))
                {
                    if (_commentPairs.TryGetValue(commandName, out comment))
                        item.Comment = comment;
                }

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
            String symbol = null;
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
                XmlNode symbolAttr = parms.GetNamedItem(SymbolAttribute);
                if (symbolAttr != null)
                {
                    symbol = symbolAttr.Value;
                }
                if (idOrSymbol == null)
                {
                    idOrSymbol = symbol;
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
                uint applicationModes = GetApplicationModes(child);
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
                    name = GetCommandName(attr.Value);
                    if ((ContainsKey(pair1List, name) >= 0 || ContainsKey(pair3List, name) >= 0 || Char.IsNumber(name[0])) && (ContainsKey(pair2List, name) == -1))
                    {
                        pair2List.Add(new KeyValuePair<string, string>(name, RibbonString + nodeName));
                    }
                }
                if (child.Name.Equals("QuickAccessToolbar", StringComparison.Ordinal))
                {
                    attr = parms.GetNamedItem(CustomizeCommandNameAttribute);
                    if (attr != null)
                    {
                        string customizeCommandName = GetCommandName(attr.Value);
                        uint id = GetCommandId(pair3List, customizeCommandName);
                        _qatCustomizeCommand = new KeyValuePair<string, uint>(customizeCommandName, id);
                    }
                }

            }
            return name;
        }

        private uint GetApplicationModes(XmlNode child)
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
                                string name = GetCommandName(attr.Value);
                                popupCommandNames.Add(name);
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
            if (key == null)
                return -1;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Key.Equals(key))
                    return i;
            }
            return -1;
        }

        private Dictionary<string, uint> GetCommandsAndIds()
        {
            //Merge the 2 lists
            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>(pair3List);
            pairs.AddRange(pair1List);

            Dictionary<string, uint> result = new Dictionary<string, uint>();
            for (int i = 0; i < pairs.Count; i++)
            {
                uint id = GetCommandId(pairs, pairs[i].Key);
                result[pairs[i].Key] = id;
            }
            return result;
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

        private string GetCommandName(string value)
        {
            uint id;
            bool tryCheck;
            if (!string.IsNullOrEmpty(value))
            {
                //check if name is Id
                if (value.ToUpperInvariant().StartsWith("0X"))
                    tryCheck = uint.TryParse(value.Substring(2), NumberStyles.AllowHexSpecifier, null, out id);
                else
                    tryCheck = uint.TryParse(value, out id);
                if (tryCheck)
                {
                    value = id.ToString();
                }
            }
            return value;
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
                QatCustomizeCommand = parser._qatCustomizeCommand;
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
            public KeyValuePair<string, uint>? QatCustomizeCommand { get; private set; }
        }
    }
}
