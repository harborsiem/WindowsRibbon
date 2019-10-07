using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace RibbonGenerator
{
    class CodeBehindGenerator
    {
        private const string CommandNameAttribute = "CommandName";
        private const string NameAttribute = "Name";
        private const string IdAttribute = "Id";
        private const string SymbolAttribute = "Symbol";
        private const string Ident = "    ";
        private const string RibbonItems = "RibbonItems";

        //lists build from .xml file
        //in pair1List are only keys and values they are not in pair3List
        List<KeyValuePair<string, string>> pair1List = new List<KeyValuePair<string, string>>(); //Command Name, Command Id
        List<KeyValuePair<string, string>> pair2List = new List<KeyValuePair<string, string>>(); //Command Name, RibbonControl class name

        //list build from .h file
        List<KeyValuePair<string, string>> pair3List = new List<KeyValuePair<string, string>>(); //Command Name, Command Id
        string ribbonItemsClass;

        public void Execute(string fileName) //RibbonMarkup.xml
        {
            //string @namespace = System.Reflection.Assembly.GetEntryAssembly().EntryPoint.DeclaringType.Namespace;
            if (File.Exists(fileName))
            {
                string path = Path.GetDirectoryName(fileName);

                string xmlFileName = Path.GetFileNameWithoutExtension(fileName);
                char last = xmlFileName[xmlFileName.Length - 1];
                if (Char.IsNumber(last))
                {
                    ribbonItemsClass = RibbonItems + last.ToString();
                }
                else
                {
                    ribbonItemsClass = RibbonItems;
                }

                ParseHFile(Path.ChangeExtension(fileName, "h"));
                ParseXmlFile(fileName);
                WriteCsFile(path);
            }
        }

        private void ParseHFile(string fileName)
        {
            pair3List.Clear();
            if (!File.Exists(fileName))
                return;
            StreamReader sr = File.OpenText(fileName);
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
        }

        private void ParseXmlFile(string fileName)
        {
            pair1List.Clear();
            pair2List.Clear();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);
            XmlNode node = xmlDocument.DocumentElement;
            XmlNodeList list = node.ChildNodes;
            XmlNode xmlNode;
            for (int i = 0; i < list.Count; i++)
            {
                xmlNode = list.Item(i);
                if (xmlNode.Name == "Application.Commands")
                {
                    XmlNodeList list1 = xmlNode.ChildNodes;
                    for (int j = 0; j < list1.Count; j++)
                    {
                        ParseCommands(list1[j]);
                    }
                }
                if (xmlNode.Name == "Application.Views")
                {
                    XmlNodeList ribbonList = xmlNode.ChildNodes;
                    if (ribbonList.Count == 1 && ribbonList.Item(0).Name == "Ribbon")
                    {
                        ParseViews(ribbonList.Item(0));
                    }
                    break;
                }
            }
        }

        private void ParseCommands(XmlNode node)
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
                    if (!ContainsKey(pair3List, name))
                    {
                        pair1List.Add(new KeyValuePair<string, string>(name, idOrSymbol));
                    }
                }
            }
        }

        private void ParseViews(XmlNode node)
        {
            XmlNodeList nodeList = node.ChildNodes;
            if (nodeList.Count == 0)
                return;

            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlNode child = nodeList.Item(i);
                String nodeName = child.Name;
                String name = null;
                XmlAttributeCollection parms = child.Attributes;
                if (parms != null && parms.Count > 0)
                {
                    XmlNode attr = parms.GetNamedItem(CommandNameAttribute);
                    if (attr != null)
                    {
                        name = attr.Value;
                        if ((ContainsKey(pair1List, name) || ContainsKey(pair3List, name)) && !(ContainsKey(pair2List, name)))
                        {
                            pair2List.Add(new KeyValuePair<string, string>(name, "Ribbon" + nodeName));
                        }
                    }
                }
                ParseViews(child);
            }
        }

        private bool ContainsKey(List<KeyValuePair<string, string>> list, string key)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Key.Equals(key))
                    return true;
            }
            return false;
        }

        private void WriteCsFile(string path)
        {
            StreamWriter sw = CreateCsFile(Path.Combine(path, ribbonItemsClass + ".Designer.cs"));
            WriteConst(sw);
            WriteProperties(sw);
            WriteConstructor(sw);
            CloseCsFile(sw);
        }

        private StreamWriter CreateCsFile(string path)
        {
            StreamWriter sw = File.CreateText(path);
            sw.WriteLine("//------------------------------------------------------------------------------");
            sw.WriteLine("// <auto-generated>");
            sw.WriteLine("//     This code was generated by a tool.");
            sw.WriteLine("//     Runtime Version:");
            sw.WriteLine("//");
            sw.WriteLine("//     Changes to this file may cause incorrect behavior and will be lost if");
            sw.WriteLine("//     the code is regenerated.");
            sw.WriteLine("// </auto-generated>");
            sw.WriteLine("//------------------------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine("using System;");
            sw.WriteLine("using RibbonLib;");
            sw.WriteLine("using RibbonLib.Controls;");
            sw.WriteLine("using RibbonLib.Interop;");
            sw.WriteLine();
            sw.WriteLine("namespace RibbonLib.Controls");
            sw.WriteLine("{");
            sw.WriteLine(Ident + "partial class " + ribbonItemsClass);
            sw.WriteLine(Ident + "{");
            return sw;
        }

        private void WriteConst(StreamWriter sw)
        {
            sw.WriteLine(Ident + Ident + "private static class Cmd");
            sw.WriteLine(Ident + Ident + "{");
            for (int i = 0; i < pair3List.Count; i++)
            {
                sw.WriteLine(Ident + Ident + Ident + "public const uint " + pair3List[i].Key + " = " + pair3List[i].Value + ";");
            }
            for (int i = 0; i < pair1List.Count; i++)
            {
                sw.WriteLine(Ident + Ident + Ident + "public const uint " + pair1List[i].Key + " = " + pair1List[i].Value + ";");
            }
            sw.WriteLine(Ident + Ident + "}");
            sw.WriteLine();
        }

        private void WriteProperties(StreamWriter sw)
        {
            sw.WriteLine(Ident + Ident + "private static bool initialized;");
            sw.WriteLine();
            sw.WriteLine(Ident + Ident + "public " + "Ribbon" + " " + "Ribbon" + " { get; private set; }");
            for (int i = 0; i < pair2List.Count; i++)
            {
                string name = GetPropertyName(pair2List[i].Key);
                sw.WriteLine(Ident + Ident + "public " + pair2List[i].Value + " " + name + " { get; private set; }");
            }
            sw.WriteLine();
        }

        private void WriteConstructor(StreamWriter sw)
        {
            sw.WriteLine(Ident + Ident + "public " + ribbonItemsClass + "(Ribbon ribbon)");
            sw.WriteLine(Ident + Ident + "{");
            sw.WriteLine(Ident + Ident + Ident + "if (ribbon == null)");
            sw.WriteLine(Ident + Ident + Ident + Ident + "throw new ArgumentNullException(nameof(ribbon), \"Parameter is null\");");
            sw.WriteLine(Ident + Ident + Ident + "if (initialized)");
            sw.WriteLine(Ident + Ident + Ident + Ident + "return;");
            sw.WriteLine(Ident + Ident + Ident + "this.Ribbon = ribbon;");
            for (int i = 0; i < pair2List.Count; i++)
            {
                string name = GetPropertyName(pair2List[i].Key);
                sw.WriteLine(Ident + Ident + Ident + name + " = new " + pair2List[i].Value + "(ribbon, " + "Cmd." + pair2List[i].Key + ");");
            }

            sw.WriteLine(Ident + Ident + Ident + "initialized = true;");
            sw.WriteLine(Ident + Ident + "}");
            sw.WriteLine();
        }

        private void CloseCsFile(StreamWriter sw)
        {
            sw.WriteLine(Ident + "}");
            sw.WriteLine("}");
            sw.Close();
        }

        private string GetPropertyName(string commandName)
        {
            string result;
            if (commandName.StartsWith("cmd", StringComparison.InvariantCultureIgnoreCase))
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
