/* 
  Classes for reading and writing Ribbon Markup XML files.
  We could use XML Data Binding to convert the Ribbon schema UICC.xsd
  to a set of interfaces, but that's not desireable for various reasons:
  -That approach is not very fast and memory efficient.
  -You would still need additional support classes or routines to easily work
   with those object.
  -But most importantly, Ribbon Markup doesn't really lend itself for this
   because the same data can be represented in multiple ways. For example,
   the LabelType of a command can be represented as a LabelTitle attribute
   of the <Command> element, or as a sub-element of the <Command.LabelTitle>
   element (much like XAML). This class model unifies this: it can read both
   ways as a single LabelTitle, and it writes it in a format that is most
   efficient.
   */
// see also files Enums.cs and TRibbonObject.Const.cs

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UIRibbonTools
{
    [Serializable]
    class RibbonMarkupException : Exception
    {
        public RibbonMarkupException()
            : base() { }

        public RibbonMarkupException(string message, Exception innerException)
            : base(message, innerException) { }

        public RibbonMarkupException(string message)
            : base(message) { }

        public RibbonMarkupException(Exception innerException)
            : base(innerException.Message, innerException) { }

    }

    struct TRibbonCommandName
    {
        public string Name { get; set; } // string.Empty when Id is used
        public int Id { get; set; }  // -1 when Name is used
    }

    partial class TRibbonObject
    {
        public static int AttributeAsIntegerDef(XElement sender, string attrName, int @default)
        {
            string tmp = sender.Attribute(attrName)?.Value;
            if (string.IsNullOrEmpty(tmp))
                return @default;
            return XmlConvert.ToInt32(tmp);
        }

        public static bool AttributeAsBooleanDef(XElement sender, string attrName, bool @default)
        {
            string tmp = sender.Attribute(attrName)?.Value;
            if (string.IsNullOrEmpty(tmp))
                return @default;
            return XmlConvert.ToBoolean(tmp);
        }

        public static bool IsValidCommandNameString(string name)
        {
            char ch;
            int i;
            bool result = (name.Length <= 100);
            if ((result) && (!string.IsNullOrEmpty(name)))
            {
                ch = name[0];
                result = ((ch >= 'A') && (ch <= 'Z')) || ((ch >= 'a') && (ch <= 'z')) || (ch == '_');
                if (result)
                {
                    for (i = 1; i < name.Length; i++)
                    {
                        ch = name[i];
                        result = ((ch >= 'A') && (ch <= 'Z')) || ((ch >= 'a') && (ch <= 'z'))
                          || ((ch >= '0') && (ch <= '9')) || (ch == '_');
                        if (!result)
                            return result;
                    }
                }
            }
            return result;
        }

        public static bool IsValidSymbolString(string symbol)
        {
            bool result = IsValidCommandNameString(symbol);
            return result;
        }

        public static bool IsValidCommandValue(int value)
        {
            bool result = (value == 0) || ((value >= 2) && (value <= 59999)); //@ bugfix
            return result;
        }

        public static int StringToCommandValue(string s)
        {
            int result;
            if (string.IsNullOrEmpty(s))
                result = 0;
            else if (!int.TryParse(s, out result))
            {
                if (s.Substring(0, 2).ToUpperInvariant() == "0X")
                    result = Convert.ToInt32(s.Substring(2), 16);
                else
                    result = -1; // Invalid
            }
            return result;
        }

        public static TRibbonCommandName StringToCommandName(string s)
        {
            TRibbonCommandName result = new TRibbonCommandName();
            result.Id = StringToCommandValue(s);
            if (result.Id < 0) //@ bugfix
            {
                result.Id = -1;
                result.Name = s;
            }
            else
                result.Name = string.Empty;
            return result;
        }
    }

    abstract partial class TRibbonObject : IDisposable
    {
        #region Internal Declarations

        private TRibbonDocument _owner;
        private List<TRibbonObject> _notifyList;

        private int GetReferenceCount()
        {
            if (null != _notifyList)
                return _notifyList.Count;
            else
                return 0;
        }

        protected void Error(XElement element, string msg)
        {
            if (null != element)
            {
                IXmlLineInfo info = element;
                int lineNumber = info.LineNumber;
                throw new RibbonMarkupException(string.Format("Line {0:d} {1}", new object[] { lineNumber, msg }));
            }
            else
                throw new RibbonMarkupException(msg);
        }

        protected void Error(XElement element, string msg, params object[] args)
        {
            Error(element, string.Format(msg, args));
        }

        internal void FreeNotification(TRibbonObject listener)
        {
            if (_notifyList == null)
                _notifyList = new List<TRibbonObject>();
            if (_notifyList.IndexOf(listener) < 0)
                _notifyList.Add(listener);
        }

        internal void RemoveFreeNotification(TRibbonObject listener)
        {
            if (null != _notifyList)
                _notifyList.Remove(listener);
        }

        internal virtual void FreeNotify(TRibbonObject obj)
        {
            //No default implementation
        }

        #endregion Internal Declarations

        public TRibbonObject(TRibbonDocument owner)
        {
            _owner = owner;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //destructor
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (null != _notifyList)
                {
                    foreach (TRibbonObject listener in _notifyList)
                        listener.FreeNotify(this);

                    //_notifyList.Dispose();
                }
            }
        }

        public virtual string DisplayName()
        {
            const string TRibbon = "TRibbon"; //@ attention with renaming class names
            string result = GetType().Name;
            if (result.StartsWith(TRibbon))
                result = result.Substring(TRibbon.Length);
            result = "(" + result + ")";
            return result;
        }

        public abstract RibbonObjectType ObjectType();

        public virtual TRibbonObject AddNew(RibbonObjectType objType)
        {
            return null;
        }

        [Obsolete]
        public bool Delete(TRibbonObject obj)
        {
            return Remove(obj);
        }

        public virtual bool Remove(TRibbonObject obj)
        {
            return false;
        }

        public virtual bool CanReorder()
        {
            return false;
        }

        public virtual bool Reorder(TRibbonObject child, int direction)
        {
            return false;
        }

        public TRibbonDocument Owner { get { return _owner; } }
        public int ReferenceCount { get { return GetReferenceCount(); } }
    }

    class TRibbonList<T> : TRibbonObject where T : TRibbonObject
    {
        #region Internal Declarations

        private List<T> _items;
        private bool _ownsItems;

        //private int GetCount()
        //{
        //    return _items.Count;
        //}

        //private T GetItem(int index)
        //{
        //    return _items[index];
        //}

        internal void Add(T item)
        {
            _items.Add(item);
        }

        internal bool Remove(T item)
        {
            int i = _items.IndexOf(item);
            bool result = (i >= 0);
            if (result)
            {
                if (_ownsItems)
                    item.Dispose();
                _items.RemoveAt(i);
            }
            return result;
        }

        private void Clear()
        {
            if (null != _items)
            {
                if (_ownsItems)
                    foreach (T item in _items)
                        item.Dispose();

                _items.Clear();
            }
        }

        #endregion Internal Declarations

        public TRibbonList(TRibbonDocument owner, bool ownsItems) : base(owner)
        {
            _items = new List<T>();
            _ownsItems = ownsItems;
        }

        //destructor
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Clear();
                //_items.Free;
            }
            base.Dispose(disposing);
        }

        public override RibbonObjectType ObjectType()
        {
            return RibbonObjectType.List;
        }

        public override bool Reorder(TRibbonObject child, int direction)
        {
            //@ Test for index + direction ?
            bool result = false;
            T item = (T)(child);
            int index = _items.IndexOf(item);
            if (direction < 0)
                result = (index > 0);
            else
                result = (index >= 0) && (index < (_items.Count - 1));
            if (result)
            {
                T item1, item2;
                item1 = _items[index];
                item2 = _items[index + direction];
                _items.RemoveAt(index);
                _items.Insert(index, item2);
                _items.RemoveAt(index + direction);
                _items.Insert(index + direction, item1);
                //_items.Exchange(Index, Index + direction);
            }
            return result;
        }

        public void Assign(List<T> pItems)
        {
            if (null != _items)
            {
                _items.Clear();
                foreach (T item in pItems)
                    _items.Add(item);
            }
        }

        public List<T>.Enumerator GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public int Count { get { return _items.Count; } }
        public T this[int index] { get { return _items[index]; } }
    }

    class TRibbonDictionary<TKey, TValue> : TRibbonObject
    {
        #region Internal Declarations

        private Dictionary<TKey, TValue> _items;

        private int GetCount()
        {
            return _items.Count;
        }

        private TValue GetItem(TKey key)
        {
            //if (key == null)
            //    return default(TValue);
            return _items[key];
        }

        private Dictionary<TKey, TValue>.KeyCollection GetKeys()
        {
            return _items.Keys;
        }

        private Dictionary<TKey, TValue>.ValueCollection GetValues()
        {
            return _items.Values;
        }


        internal void Add(TKey key, TValue value)
        {
            _items[key] = value;
        }

        internal void Remove(TKey key)
        {
            if (key != null && _items.ContainsKey(key))
                _items.Remove(key);
        }

        #endregion Internal Declarations

        public TRibbonDictionary(TRibbonDocument owner) : base(owner)
        {
            _items = new Dictionary<TKey, TValue>();
        }

        //destructor
        protected override void Dispose(bool disposing)
        {
            //_items.Free;
            base.Dispose(disposing);
        }

        public override RibbonObjectType ObjectType()
        {
            return RibbonObjectType.Dictionary;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            //if (key == null)
            //{
            //    value = default(TValue);
            //    return false;
            //}
            return _items.TryGetValue(key, out value);
        }

        public bool ContainsKey(TKey key)
        {
            //if (key == null)
            //{
            //    return false;
            //}
            return _items.ContainsKey(key);
        }

        public int Count { get { return _items.Count; } }
        public TValue this[TKey key] { get { return GetItem(key); } }
        public Dictionary<TKey, TValue>.Enumerator GetEnumerator() { return _items.GetEnumerator(); }
        public Dictionary<TKey, TValue>.KeyCollection Keys { get { return GetKeys(); } }
        public Dictionary<TKey, TValue>.ValueCollection Values { get { return GetValues(); } }
    }

    class TRibbonString : TRibbonObject
    {
        public TRibbonString(TRibbonDocument owner) : base(owner) { }
        #region Internal Declarations

        private string _content;
        private int _id;
        private string _symbol;

        private void SetId(int value)
        {
            if (value != _id)
            {
                if (!IsValidCommandValue(value))
                    Error(null, RS_INVALID_ID, value);
                _id = value;
            }
        }

        private void SetSymbol(string value)
        {
            if ((value != null) && value != _symbol)
            {
                if (!IsValidSymbolString(value))
                    Error(null, RS_INVALID_SYMBOL, value);
                _symbol = value;
            }
        }

        internal void Initialize(XElement E)
        {
            XElement C;
            _content = E.Value;
            _id = 0;
            _symbol = string.Empty;

            if (E.Elements().Count() == 0)
                return;

            if (E.Elements().Count() > 1)
                Error(E, RS_MULTIPLE_RIBBON_STRINGS);
            else
            {
                C = E.Elements().ElementAt(0);
                if (C.Name.LocalName != EN_STRING)
                    Error(C, RS_ELEMENT_EXPECTED, EN_STRING, C.Name.LocalName);

                _content = C.Attribute(AN_CONTENT)?.Value;
                if (string.IsNullOrEmpty(_content))
                    _content = C.Value;
                SetId(StringToCommandValue(C.Attribute(AN_ID)?.Value));
                SetSymbol(C.Attribute(AN_SYMBOL)?.Value);

                foreach (XElement GC in C.Elements())
                {
                    if (GC.Name.LocalName == EN_STRING_CONTENT)
                        _content = GC.Value;
                    else if (GC.Name.LocalName == EN_STRING_ID)
                        SetId(StringToCommandValue(GC.Value)); //@ bugfix
                    else if (GC.Name.LocalName == EN_STRING_SYMBOL)
                        SetSymbol(GC.Value); //@ bugfix
                    else
                        Error(GC, RS_UNSUPPORTED_CHILD_ELEMENT, GC.Name.LocalName, C.Name.LocalName);
                }
            }
        }

        internal void Save(XmlWriter writer, string elementName)
        {
            if ((_id != 0) || (!string.IsNullOrEmpty(_symbol)))
            {
                writer.WriteStartElement(elementName);

                writer.WriteStartElement(EN_STRING);
                if (_id != 0)
                    writer.WriteAttributeString(AN_ID, XmlConvert.ToString(_id));
                if (!string.IsNullOrEmpty(_symbol))
                    writer.WriteAttributeString(AN_SYMBOL, _symbol);
                if (!string.IsNullOrEmpty(_content))
                    writer.WriteString(_content); //WriteContent
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }

        #endregion Internal Declarations

        public override RibbonObjectType ObjectType() { return RibbonObjectType.String; }

        public bool IsSimpleString()
        {
            return (_id == 0) && (string.IsNullOrEmpty(_symbol));
        }

        public bool HasSimpleString()
        {
            return (_id == 0) && (string.IsNullOrEmpty(_symbol)) && (!string.IsNullOrEmpty(_content));
        }

        public string Content { get { return _content; } set { _content = value; } }
        public int Id { get { return _id; } set { SetId(value); } }
        public string Symbol { get { return _symbol; } set { SetSymbol(value); } }
    }

    class TRibbonImage : TRibbonObject
    {
        #region Internal Declarations

        private string _source;
        private int _minDpi;
        private int _id;
        private string _symbol;

        private void SetId(int value)
        {
            if (value != _id)
            {
                if (!IsValidCommandValue(value))
                    Error(null, RS_INVALID_ID, value);
                _id = value;
            }
        }

        private void SetMinDpi(int value)
        {
            if (value != _minDpi)
            {
                if ((value != 0) && (value < 96))
                    Error(null, RS_INVALID_DPI_VALUE);
                _minDpi = value;
            }
        }

        private void SetSymbol(string value)
        {
            if ((value != null) && value != _symbol)
            {
                if (!IsValidSymbolString(value))
                    Error(null, RS_INVALID_SYMBOL, value);
                _symbol = value;
            }
        }

        public TRibbonImage(TRibbonDocument owner) : base(owner) { }

        public TRibbonImage(TRibbonDocument owner, XElement E) : this(owner)
        {
            if (E.Name.LocalName != EN_IMAGE)
                Error(E, RS_ELEMENT_EXPECTED, EN_IMAGE, E.Name.LocalName);

            _source = E.Attribute(AN_SOURCE)?.Value;
            if (string.IsNullOrEmpty(_source))
                _source = E.Value;
            SetId(StringToCommandValue(E.Attribute(AN_ID)?.Value));
            SetSymbol(E.Attribute(AN_SYMBOL)?.Value);
            string tmp = E.Attribute(AN_MIN_DPI)?.Value;
            SetMinDpi(string.IsNullOrEmpty(tmp) ? 0 : XmlConvert.ToInt32(tmp));

            foreach (XElement C in E.Elements())
            {
                if (C.Name.LocalName == EN_IMAGE_SOURCE)
                    _source = C.Value;
                else
                    Error(C, RS_UNSUPPORTED_CHILD_ELEMENT, C.Name.LocalName, E.Name.LocalName);
            }
        }

        internal void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_IMAGE);
            if (_id != 0)
                writer.WriteAttributeString(AN_ID, XmlConvert.ToString(_id));
            if (!string.IsNullOrEmpty(_symbol))
                writer.WriteAttributeString(AN_SYMBOL, _symbol);
            if (_minDpi != 0)
                writer.WriteAttributeString(AN_MIN_DPI, XmlConvert.ToString(_minDpi));
            if (!string.IsNullOrEmpty(_source))
                writer.WriteString(_source);
            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        public override RibbonObjectType ObjectType() { return RibbonObjectType.Image; }

        public string Source { get { return _source; } set { _source = value; } }
        public int MinDpi { get { return _minDpi; } set { SetMinDpi(value); } }
        public int Id { get { return _id; } set { SetId(value); } }
        public string Symbol { get { return _symbol; } set { SetSymbol(value); } }
    }

    class TRibbonDocument : TRibbonObject
    {
        #region Internal Declarations

        private string _filename;
        private string _directory;
        private TRibbonApplication _application;

        private void SaveResourceName(string fileName)
        {
            Stream stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            try
            {
                stream.Seek(0, SeekOrigin.End);
                byte[] resourceString = Encoding.UTF8.GetBytes(Environment.NewLine
                    + ResourceTagPrefix + _application.ResourceName + ResourceTagSuffix);
                stream.Write(resourceString, 0, (resourceString.Length));
            }
            finally
            {
                stream.Close();
            }
        }

        private string LoadResourceName(string fileName)
        {
            string result = ApplicationDefaultName;
            List<string> strings = new List<string>();
            StreamReader sr = null;
            try
            {
                sr = File.OpenText(fileName);
                while (!sr.EndOfStream)
                {
                    strings.Add(sr.ReadLine());
                }

                string lastLine = strings[strings.Count - 1];
                if (lastLine.StartsWith(ResourceTagPrefix))
                    result = lastLine.Substring((ResourceTagPrefix.Length),
                        (lastLine.Length) - ((ResourceTagSuffix.Length) + (ResourceTagPrefix.Length)));
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
            return result;
        }

        public TRibbonObject CreateObject(RibbonObjectType objType, TRibbonCommandRefObject parent)
        {
            TRibbonObject result;
            switch (objType)
            {
                case RibbonObjectType.Button:
                    result = new TRibbonButton(this, parent);
                    break;
                case RibbonObjectType.SplitButton:
                    result = new TRibbonSplitButton(this, parent);
                    break;
                case RibbonObjectType.ToggleButton:
                    result = new TRibbonToggleButton(this, parent);
                    break;
                case RibbonObjectType.DropDownButton:
                    result = new TRibbonDropDownButton(this, parent);
                    break;
                case RibbonObjectType.CheckBox:
                    result = new TRibbonCheckBox(this, parent);
                    break;
                case RibbonObjectType.ComboBox:
                    result = new TRibbonComboBox(this, parent);
                    break;
                case RibbonObjectType.Spinner:
                    result = new TRibbonSpinner(this, parent);
                    break;
                case RibbonObjectType.DropDownGallery:
                    result = new TRibbonDropDownGallery(this, parent);
                    break;
                case RibbonObjectType.SplitButtonGallery:
                    result = new TRibbonSplitButtonGallery(this, parent);
                    break;
                case RibbonObjectType.InRibbonGallery:
                    result = new TRibbonInRibbonGallery(this, parent);
                    break;
                case RibbonObjectType.DropDownColorPicker:
                    result = new TRibbonDropDownColorPicker(this, parent);
                    break;
                case RibbonObjectType.FontControl:
                    result = new TRibbonFontControl(this, parent);
                    break;
                case RibbonObjectType.FloatieFontControl:
                    result = new TRibbonFloatieFontControl(this, parent);
                    break;
                case RibbonObjectType.ControlGroup:
                    result = new TRibbonControlGroup(this, parent);
                    break;
                case RibbonObjectType.QatButton:
                    result = new TRibbonQatButton(this, parent);
                    break;
                case RibbonObjectType.QatToggleButton:
                    result = new TRibbonQatToggleButton(this, parent);
                    break;
                case RibbonObjectType.QatCheckBox:
                    result = new TRibbonQatCheckBox(this, parent);
                    break;
                case RibbonObjectType.RibbonSizeDefinition:
                    result = new TRibbonRibbonSizeDefinition(this);
                    break;
                default:
                    result = null;
                    Debug.Assert(false);
                    break;
            }
            return result;
        }

        #endregion Internal Declarations

        public TRibbonDocument() : base(null)
        {
            _application = new TRibbonApplication(this);
        }

        //destructor
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _application?.Dispose();
            }
            base.Dispose(disposing);
        }

        public override RibbonObjectType ObjectType() { return RibbonObjectType.Document; }

        public void Clear()
        {
            _filename = string.Empty;
            _directory = string.Empty;
            if (_application != null) //FreeAndNil
            {
                _application.Dispose();
                _application = null;
            }
            _application = new TRibbonApplication(this);
        }

        /* Load Ribbon Markup from a file, stream, XML string or XML document.
         Raises an exception if the markup doesn't conform to the Ribbon schema
         (UICC.xsd).  */
        public void LoadFromFile(string fileName)
        {
            _filename = Path.GetFullPath(fileName);
            _directory = Path.GetDirectoryName(_filename) + Path.DirectorySeparatorChar;
            XDocument doc;
            try
            {
                doc = XDocument.Load(_filename, LoadOptions.SetLineInfo);
                LoadFromXmlDocument(doc);
            }
            finally
            {
                //doc.Free;
            }
            _application.ResourceName = LoadResourceName(fileName);
        }

        public void LoadFromStream(Stream stream)
        {
            _filename = string.Empty;
            XDocument doc;
            try
            {
                doc = XDocument.Load(stream, LoadOptions.SetLineInfo);
                LoadFromXmlDocument(doc);
            }
            finally
            {
                //doc.Free;
            }
        }

        public void LoadFromXml(string xml) //RawByteString
        {
            _filename = string.Empty;
            XDocument doc;
            try
            {
                doc = XDocument.Parse(xml, LoadOptions.SetLineInfo);
                LoadFromXmlDocument(doc);
            }
            finally
            {
                //doc.Free;
            }
        }

        public void LoadFromXmlDocument(XDocument xmlDoc)
        {
            if (_application != null)  //FreeAndNil
            {
                _application.Dispose();
                _application = null;
            }
            _application = new TRibbonApplication(this, true);
            _application.Load(xmlDoc.Root);
        }

        /* Saves the Ribbon Markup to a file, stream or XML string  */
        public void SaveToFile(string fileName)
        {
            byte[] xml;
            SaveToXml(out xml);
            if (xml != null)
            {
                if (File.Exists(fileName))
                    File.Copy(fileName, fileName + ".bak", true);
                FileStream stream = File.Create(fileName);
                try
                {
                    stream.Write(xml, 0, xml.Length);
                }
                finally
                {
                    stream.Close();
                }
            }
            SaveResourceName(fileName);
            _filename = Path.GetFullPath(fileName);
            _directory = Path.GetDirectoryName(_filename) + Path.DirectorySeparatorChar;
        }

        public void SaveToStream(Stream stream)
        {
            byte[] xml;
            SaveToXml(out xml);
            if (xml != null)
                stream.Write(xml, 0, xml.Length);
        }

        public void SaveToXml(out byte[] xml)
        {
            MemoryStream stream = new MemoryStream();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create(stream, settings);
            try
            {
                _application.Save(writer);
                stream.Flush();
            }
            finally
            {
                writer.Close();
                //stream.Close(); stream is closed with writer.Close()
            }
            xml = stream.ToArray();
        }

        /* Finds a command with the given name or Id.
         Returns null when the command does not exist.  */
        public TRibbonCommand FindCommand(string name)
        {
            return _application.FindCommand(name);
        }

        public TRibbonCommand FindCommand(int id)
        {
            return _application.FindCommand(id);
        }

        public TRibbonCommand FindCommand(TRibbonCommandName name)
        {
            return _application.FindCommand(name);
        }

        /* Finds a context menu or mini toolbar with the given name  */
        public TRibbonContextMenu FindContextMenu(string name)
        {
            return _application.FindContextMenu(name);
        }

        public TRibbonMiniToolbar FindMiniToolbar(string name)
        {
            return _application.FindMiniToolbar(name);
        }

        private bool IsRelativePath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return true;
            int length = path.Length;
            return (length > 0) && (path[1] != Path.DirectorySeparatorChar)
                && (length > 1) && (path[2] != ':');
        }

        /* If fileName is an absolute filename, returns the filename.
         Otherwise, returns the fully qualified filename relative to the
         document directory.  */
        public string BuildAbsoluteFilename(string fileName)
        {
            string result;
            if (string.IsNullOrEmpty(fileName))
            {
                result = _directory;
            }
            else if (IsRelativePath(fileName))
                result = Path.Combine(_directory, fileName);
            else
                result = fileName;
            return new Uri(result).LocalPath;
        }

        /* Returns the relative part of the filename, relative to the
         document directory.  */
        public string BuildRelativeFilename(string fileName)
        {
            string result;
            if (fileName.StartsWith(_directory))
            {
                result = fileName.Substring(_directory.Length);
                StringBuilder sb = new StringBuilder(result.Length);
                for (int i = 0; i < result.Length; i++)
                {
                    char onechar = result[i];
                    if (onechar == '\\')
                        sb.Append('/');
                    else sb.Append(onechar);
                }
                return sb.ToString();
            }
            return fileName;
        }

        /* Root<Application> element. Never null.  */
        public TRibbonApplication Application { get { return _application; } }

        /* Fully qualified filename  */
        public string Filename { get { return _filename; } }

        /* Directory containing the ribbon file, including trailing backslash   */
        public string Directory { get { return _directory; } }
    }

    class TRibbonCommand : TRibbonObject
    {
        #region Internal Declarations

        private string _name;
        private string _symbol;
        private int _id;
        private string _comment;
        private TRibbonString _labelTitle;
        private TRibbonString _labelDescription;
        private TRibbonString _tooltipTitle;
        private TRibbonString _tooltipDescription;
        private TRibbonString _keytip;
        private TRibbonList<TRibbonImage> _smallImages;
        private TRibbonList<TRibbonImage> _largeImages;
        private TRibbonList<TRibbonImage> _smallHighContrastImages;
        private TRibbonList<TRibbonImage> _largeHighContrastImages;
        private bool _constructing;

        private void SetName(string value)
        {
            if ((value != null) && value != _name)
            {
                if (!IsValidCommandNameString(value))
                    Error(null, RS_INVALID_COMMAND_NAME, value);
                if (!_constructing)
                    Owner.Application.CommandNameChanged(this, _name, value);
                _name = value;
            }
        }

        private void SetSymbol(string value)
        {
            if ((value != null) && value != _symbol)
            {
                if (!IsValidSymbolString(value))
                    Error(null, RS_INVALID_SYMBOL, value);
                _symbol = value;
            }
        }

        private void SetId(int value)
        {
            if (value != _id)
            {
                if (!IsValidCommandValue(value))
                    Error(null, RS_INVALID_ID, value);
                if (!_constructing)
                    Owner.Application.CommandIdChanged(this, _id, value);
                _id = value;
            }
        }

        private void LoadImages(TRibbonList<TRibbonImage> list, XElement E)
        {
            foreach (XElement C in E.Elements())
                list.Add(new TRibbonImage(Owner, C));
        }

        private void SaveImages(XmlWriter writer,
           TRibbonList<TRibbonImage> list, string elementName)
        {
            if (list.Count > 0)
            {
                writer.WriteStartElement(elementName);
                foreach (TRibbonImage image in list)
                    image.Save(writer);

                writer.WriteEndElement();
            }
        }

        public TRibbonCommand(TRibbonDocument owner) : base(owner)
        {
            _labelTitle = new TRibbonString(owner);
            _labelDescription = new TRibbonString(owner);
            _tooltipTitle = new TRibbonString(owner);
            _tooltipDescription = new TRibbonString(owner);
            _keytip = new TRibbonString(owner);
            _smallImages = new TRibbonList<TRibbonImage>(owner, true);
            _largeImages = new TRibbonList<TRibbonImage>(owner, true);
            _smallHighContrastImages = new TRibbonList<TRibbonImage>(owner, true);
            _largeHighContrastImages = new TRibbonList<TRibbonImage>(owner, true);
        }

        public TRibbonCommand(TRibbonDocument owner, XElement E) : this(owner)
        {
            if (E.Name.LocalName != EN_COMMAND)
                Error(E, RS_ELEMENT_EXPECTED, EN_COMMAND, E.Name.LocalName);

            _constructing = true;
            SetName(E.Attribute(AN_NAME)?.Value);
            SetSymbol(E.Attribute(AN_SYMBOL)?.Value);
            SetId(StringToCommandValue(E.Attribute(AN_ID)?.Value));
            _constructing = false;
            _comment = E.Attribute(AN_COMMENT)?.Value;
            _labelTitle.Content = E.Attribute(AN_LABEL_TITLE)?.Value;
            _labelDescription.Content = E.Attribute(AN_LABEL_DESCRIPTION)?.Value;
            _tooltipTitle.Content = E.Attribute(AN_TOOLTIP_TITLE)?.Value;
            _tooltipDescription.Content = E.Attribute(AN_TOOLTIP_DESCRIPTION)?.Value;
            _keytip.Content = E.Attribute(AN_KEYTIP)?.Value;

            foreach (XElement C in E.Elements())
            {
                if (C.Name.LocalName == EN_COMMAND_NAME)
                    SetName(C.Value);
                else if (C.Name.LocalName == EN_COMMAND_SYMBOL)
                    SetSymbol(C.Value);
                else if (C.Name.LocalName == EN_COMMAND_ID)
                    SetId(StringToCommandValue(C.Value));
                else if (C.Name.LocalName == EN_COMMAND_LABEL_TITLE)
                    _labelTitle.Initialize(C);
                else if (C.Name.LocalName == EN_COMMAND_LABEL_DESCRIPTION)
                    _labelDescription.Initialize(C);
                else if (C.Name.LocalName == EN_COMMAND_KEYTIP)
                    _keytip.Initialize(C);
                else if (C.Name.LocalName == EN_COMMAND_TOOLTIP_TITLE)
                    _tooltipTitle.Initialize(C);
                else if (C.Name.LocalName == EN_COMMAND_TOOLTIP_DESCRIPTION)
                    _tooltipDescription.Initialize(C);
                else if (C.Name.LocalName == EN_COMMAND_SMALL_IMAGES)
                    LoadImages(_smallImages, C);
                else if (C.Name.LocalName == EN_COMMAND_LARGE_IMAGES)
                    LoadImages(_largeImages, C);
                else if (C.Name.LocalName == EN_COMMAND_SMALL_HIGH_CONTRAST_IMAGES)
                    LoadImages(_smallHighContrastImages, C);
                else if (C.Name.LocalName == EN_COMMAND_LARGE_HIGH_CONTRAST_IMAGES)
                    LoadImages(_largeHighContrastImages, C);
                else if (C.Name.LocalName == EN_COMMAND_COMMENT)
                    _comment = C.Value;
                else
                    Error(C, RS_UNSUPPORTED_CHILD_ELEMENT, C.Name.LocalName, E.Name.LocalName);
            }
        }

        internal void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_COMMAND);

            if (!string.IsNullOrEmpty(_name))
                writer.WriteAttributeString(AN_NAME, _name);
            if (!string.IsNullOrEmpty(_comment))
                writer.WriteAttributeString(AN_COMMENT, _comment);
            if (!string.IsNullOrEmpty(_symbol))
                writer.WriteAttributeString(AN_SYMBOL, _symbol);
            if (_id != 0)
                writer.WriteAttributeString(AN_ID, XmlConvert.ToString(_id));
            if (_labelTitle.HasSimpleString())
                writer.WriteAttributeString(AN_LABEL_TITLE, _labelTitle.Content);
            if (_labelDescription.HasSimpleString())
                writer.WriteAttributeString(AN_LABEL_DESCRIPTION, _labelDescription.Content);
            if (_tooltipTitle.HasSimpleString())
                writer.WriteAttributeString(AN_TOOLTIP_TITLE, _tooltipTitle.Content);
            if (_tooltipDescription.HasSimpleString())
                writer.WriteAttributeString(AN_TOOLTIP_DESCRIPTION, _tooltipDescription.Content);
            if (_keytip.HasSimpleString())
                writer.WriteAttributeString(AN_KEYTIP, _keytip.Content);

            _labelTitle.Save(writer, EN_COMMAND_LABEL_TITLE);
            _labelDescription.Save(writer, EN_COMMAND_LABEL_DESCRIPTION);
            _tooltipTitle.Save(writer, EN_COMMAND_TOOLTIP_TITLE);
            _tooltipDescription.Save(writer, EN_COMMAND_TOOLTIP_DESCRIPTION);
            _keytip.Save(writer, EN_COMMAND_KEYTIP);
            SaveImages(writer, _smallImages, EN_COMMAND_SMALL_IMAGES);
            SaveImages(writer, _largeImages, EN_COMMAND_LARGE_IMAGES);
            SaveImages(writer, _smallHighContrastImages, EN_COMMAND_SMALL_HIGH_CONTRAST_IMAGES);
            SaveImages(writer, _largeHighContrastImages, EN_COMMAND_LARGE_HIGH_CONTRAST_IMAGES);

            writer.WriteEndElement();
        }

        internal void SaveRef(XmlWriter writer, string attrName)
        {
            if (!string.IsNullOrEmpty(_name))
                writer.WriteAttributeString(attrName, _name);
            else if (_id != 0)
                writer.WriteAttributeString(attrName, XmlConvert.ToString(_id));
        }

        #endregion Internal Declarations

        //destructor
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _labelTitle?.Dispose();
                _labelDescription?.Dispose();
                _tooltipTitle?.Dispose();
                _tooltipDescription?.Dispose();
                _keytip?.Dispose();
                _smallImages?.Dispose();
                _largeImages?.Dispose();
                _smallHighContrastImages?.Dispose();
                _largeHighContrastImages?.Dispose();
            }
            base.Dispose(disposing);
        }

        public override RibbonObjectType ObjectType() { return RibbonObjectType.Command; }

        public override string DisplayName()
        {
            string result;
            if (string.IsNullOrEmpty(_name))
            {
                if (string.IsNullOrEmpty(_symbol))
                {
                    if (_id == 0)
                        result = string.Empty;
                    else
                        result = _id.ToString();
                }
                else
                    result = _symbol;
            }
            else
                result = _name;

            if (!string.IsNullOrEmpty(_labelTitle.Content))
                result = result + " (" + _labelTitle.Content + ")";
            return result;
        }

        public TRibbonImage AddSmallImage()
        {
            TRibbonImage result = new TRibbonImage(Owner);
            _smallImages.Add(result);
            return result;
        }

        public TRibbonImage AddLargeImage()
        {
            TRibbonImage result = new TRibbonImage(Owner);
            _largeImages.Add(result);
            return result;
        }

        public TRibbonImage AddSmallHighContrastImage()
        {
            TRibbonImage result = new TRibbonImage(Owner);
            _smallHighContrastImages.Add(result);
            return result;
        }

        public TRibbonImage AddLargeHighContrastImage()
        {
            TRibbonImage result = new TRibbonImage(Owner);
            _largeHighContrastImages.Add(result);
            return result;
        }

        [Obsolete]
        public void DeleteImage(TRibbonImage image)
        {
            RemoveImage(image);
        }

        public void RemoveImage(TRibbonImage image)
        {
            _smallImages.Remove(image);
            _largeImages.Remove(image);
            _smallHighContrastImages.Remove(image);
            _largeHighContrastImages.Remove(image);
        }

        public string Name { get { return _name; } set { SetName(value); } }
        public string Symbol { get { return _symbol; } set { SetSymbol(value); } }
        public int Id { get { return _id; } set { SetId(value); } }
        public string Comment { get { return _comment; } set { _comment = value; } }
        public TRibbonString LabelTitle { get { return _labelTitle; } }
        public TRibbonString LabelDescription { get { return _labelDescription; } }
        public TRibbonString TooltipTitle { get { return _tooltipTitle; } }
        public TRibbonString TooltipDescription { get { return _tooltipDescription; } }
        public TRibbonString Keytip { get { return _keytip; } }
        public TRibbonList<TRibbonImage> SmallImages { get { return _smallImages; } }
        public TRibbonList<TRibbonImage> LargeImages { get { return _largeImages; } }
        public TRibbonList<TRibbonImage> SmallHighContrastImages { get { return _smallHighContrastImages; } }
        public TRibbonList<TRibbonImage> LargeHighContrastImages { get { return _largeHighContrastImages; } }
    }

    abstract class TRibbonCommandRefObject : TRibbonObject
    {
        #region Internal Declarations

        private TRibbonCommand _commandRef;
        private void SetCommandRef(TRibbonCommand value)
        {
            if (value != _commandRef)
            {
                if (null != _commandRef)
                    _commandRef.RemoveFreeNotification(this);

                _commandRef = value;

                if (null != _commandRef)
                    _commandRef.FreeNotification(this);
            }
        }

        private TRibbonCommandRefObject _parent;

        protected TRibbonCommandRefObject(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner)
        {
            this._parent = parent;
        }

        protected TRibbonCommandRefObject(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner)
        {
            this._parent = parent;
            TRibbonCommandName name = StringToCommandName(E.Attribute(AN_COMMAND_NAME)?.Value);
            _commandRef = owner.FindCommand(name);
            if (null != _commandRef)
                _commandRef.FreeNotification(this);
        }

        internal virtual void Save(XmlWriter writer)
        {
            if (null != _commandRef)
                _commandRef.SaveRef(writer, AN_COMMAND_NAME);
        }

        internal override void FreeNotify(TRibbonObject obj)
        {
            base.FreeNotify(obj);
            if (obj == _commandRef)
                _commandRef = null;
        }

        #endregion Internal Declarations

        //destructor
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (null != _commandRef)
                    _commandRef.RemoveFreeNotification(this);
            }
            base.Dispose(disposing);
        }

        public override string DisplayName()
        {
            string result;
            if (null != _commandRef)
            {
                result = _commandRef.LabelTitle.Content;
                if ((string.IsNullOrEmpty(result)) && (!string.IsNullOrEmpty(_commandRef.Name)))
                    result = "(" + _commandRef.Name + ")";
            }
            else
                result = string.Empty;

            if (string.IsNullOrEmpty(result))
                result = base.DisplayName();
            return result;
        }

        public TRibbonCommand CommandRef { get { return _commandRef; } set { SetCommandRef(value); } }

        public TRibbonCommandRefObject Parent { get => _parent; }
    }

    abstract class TRibbonView : TRibbonObject
    {
        protected TRibbonView(TRibbonDocument owner) : base(owner)
        {
        }

        #region Internal Declarations

        internal abstract void Save(XmlWriter writer);
        #endregion Internal Declarations
    }

    class TRibbonApplicationMenuRecentItems : TRibbonCommandRefObject
    {
        #region Internal Declarations

        private int _maxCount;
        private bool _enablePinning;

        public TRibbonApplicationMenuRecentItems(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent)
        {
            _maxCount = 10;
            _enablePinning = true;
        }

        public TRibbonApplicationMenuRecentItems(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent)
        {
            //@ changed at caller
            //if ((E.Elements().Count() == 0) || (E[0].Name != EN_RECENT_ITEMS))
            //    Error(E, RS_SINGLE_ELEMENT, E.Name.LocalName, EN_RECENT_ITEMS);
            //inherited Create(Owner, E[0]);
            XElement C = E;
            _maxCount = AttributeAsIntegerDef(C, AN_MAX_COUNT, 10);
            _enablePinning = AttributeAsBooleanDef(C, AN_ENABLE_PINNING, true);
        }

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_APPLICATION_MENU_RECENT_ITEMS);
            writer.WriteStartElement(EN_RECENT_ITEMS);

            base.Save(writer);

            if (_maxCount != 10)
                writer.WriteAttributeString(AN_MAX_COUNT, XmlConvert.ToString(_maxCount));

            if (!_enablePinning)
                writer.WriteAttributeString(AN_ENABLE_PINNING, XmlConvert.ToString(_enablePinning));

            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        public override RibbonObjectType ObjectType() { return RibbonObjectType.RecentItems; }

        public int MaxCount { get { return _maxCount; } set { _maxCount = value; } }
        public bool EnablePinning { get { return _enablePinning; } set { _enablePinning = value; } }
    }

    abstract class TRibbonControl : TRibbonCommandRefObject
    {
        #region Internal Declarations

        private uint _applicationModes;

        protected TRibbonControl(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent)
        {
            _applicationModes = 0xFFFFFFFF;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        protected TRibbonControl(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent)
        {
            _applicationModes = 0xFFFFFFFF;
            if (SupportApplicationModes())
            {
                string s = E.Attribute(AN_APPLICATION_MODES)?.Value;
                if (!string.IsNullOrEmpty(s))
                {
                    string[] modes = s.Split(',');
                    try
                    {
                        if (modes.Length > 0)
                        {
                            _applicationModes = 0;
                            foreach (string s1 in modes)
                            {
                                int result;
                                int i;
                                if (int.TryParse(s1.Trim(), out result))
                                    i = result;
                                else i = -1;
                                if ((i < 0) || (i > 31))
                                    Error(E, RS_INVALID_APPLICATION_MODE);
                                _applicationModes |= (uint)(1 << i);
                            }
                        }
                    }
                    finally
                    {
                        //modes.Free;
                    }
                }
            }
        }

        internal override void Save(XmlWriter writer)
        {
            base.Save(writer);
            if ((SupportApplicationModes()) && (_applicationModes != 0xFFFFFFFF))
            {
                string s = string.Empty;
                for (int i = 0; i < 32; i++)
                {
                    if ((_applicationModes & (1 << i)) != 0)
                    {
                        if (!string.IsNullOrEmpty(s))
                            s = s + ",";
                        s = s + (i.ToString());
                    }
                }
                writer.WriteAttributeString(AN_APPLICATION_MODES, s);
            }
        }

        #endregion Internal Declarations

        public abstract bool SupportApplicationModes();

        public override bool CanReorder()
        {
            return true;
        }

        public uint ApplicationModes { get { return _applicationModes; } set { _applicationModes = value; } }
    }

    class TRibbonAppMenuGroup : TRibbonCommandRefObject
    {
        static readonly string[] CLASSES = { ES_STANDARD_ITEMS, ES_MAJOR_ITEMS };
        #region Internal Declarations

        private RibbonMenuCategoryClass _categoryClass;
        private TRibbonList<TRibbonControl> _controls;

        protected virtual bool AddControl(XElement E)
        {
            bool result = true;
            if (E.Name.LocalName == EN_BUTTON)
                _controls.Add(new TRibbonButton(Owner, E, this));
            else if (E.Name.LocalName == EN_SPLIT_BUTTON)
                _controls.Add(new TRibbonSplitButton(Owner, E, this));
            else if (E.Name.LocalName == EN_DROP_DOWN_BUTTON)
                _controls.Add(new TRibbonDropDownButton(Owner, E, this));
            else if (E.Name.LocalName == EN_DROP_DOWN_GALLERY)
                _controls.Add(new TRibbonDropDownGallery(Owner, E, this));
            else if (E.Name.LocalName == EN_SPLIT_BUTTON_GALLERY)
                _controls.Add(new TRibbonSplitButtonGallery(Owner, E, this));
            else
                result = false;
            return result;
        }

        public TRibbonAppMenuGroup(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent)
        {
            _controls = new TRibbonList<TRibbonControl>(owner, true);
            _categoryClass = RibbonMenuCategoryClass.StandardItems;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TRibbonAppMenuGroup(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent)
        {
            _controls = new TRibbonList<TRibbonControl>(owner, true);
            string s = E.Attribute(AN_CLASS)?.Value;
            if ((string.IsNullOrEmpty(s)) || (s == ES_STANDARD_ITEMS))
                _categoryClass = RibbonMenuCategoryClass.StandardItems;
            else if (s == ES_MAJOR_ITEMS)
                _categoryClass = RibbonMenuCategoryClass.MajorItems;
            else
                Error(E, RS_INVALID_CATEGORY_CLASS);

            foreach (XElement C in E.Elements())
            {
                if (!AddControl(C))
                    Error(C, RS_UNSUPPORTED_CHILD_ELEMENT, C.Name.LocalName, E.Name.LocalName);
            }
        }

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_MENU_GROUP);

            base.Save(writer);

            if (_categoryClass != RibbonMenuCategoryClass.StandardItems)
                writer.WriteAttributeString(AN_CLASS, CLASSES[(int)_categoryClass]);

            foreach (TRibbonControl control in _controls)
                control.Save(writer);

            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        //destructor
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _controls?.Dispose();
            }
            base.Dispose(disposing);
        }

        public override RibbonObjectType ObjectType() { return RibbonObjectType.AppMenuGroup; }

        public override TRibbonObject AddNew(RibbonObjectType objType)
        {
            TRibbonObject result;
            if (objType == RibbonObjectType.Button || objType == RibbonObjectType.SplitButton
                || objType == RibbonObjectType.DropDownButton
                || objType == RibbonObjectType.DropDownGallery
                || objType == RibbonObjectType.SplitButtonGallery)
            {
                result = Owner.CreateObject(objType, this);
                _controls.Add(result as TRibbonControl);
            }
            else
                result = base.AddNew(objType);
            return result;
        }

        public override bool Remove(TRibbonObject obj)
        {
            bool result;
            if (obj is TRibbonControl)
                result = _controls.Remove((TRibbonControl)(obj));
            else
                result = base.Remove(obj);
            return result;
        }

        public override bool CanReorder()
        {
            return true;
        }

        public override bool Reorder(TRibbonObject child, int direction)
        {
            bool result;
            if (child is TRibbonControl)
                result = _controls.Reorder(child, direction);
            else
                result = base.Reorder(child, direction);
            return result;
        }

        /* CategoryClass is ignored when menu group is part of the Application Menu  */
        public RibbonMenuCategoryClass CategoryClass
        {
            get { return _categoryClass; }
            set { _categoryClass = value; }
        }

        public TRibbonList<TRibbonControl> Controls { get { return _controls; } }
    }

    class TRibbonMenuGroup : TRibbonAppMenuGroup
    {
        public TRibbonMenuGroup(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent)
        {
        }

        public TRibbonMenuGroup(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent)
        {
        }

        #region Internal Declarations

        protected override bool AddControl(XElement E)
        {
            bool result = true;
            if (E.Name.LocalName == EN_TOGGLE_BUTTON)
                Controls.Add(new TRibbonToggleButton(Owner, E, this));
            else if (E.Name.LocalName == EN_CHECK_BOX)
                Controls.Add(new TRibbonCheckBox(Owner, E, this));
            else if (E.Name.LocalName == EN_DROP_DOWN_COLOR_PICKER)
                Controls.Add(new TRibbonDropDownColorPicker(Owner, E, this));
            else
                result = base.AddControl(E);
            return result;
        }

        #endregion Internal Declarations

        public override RibbonObjectType ObjectType() { return RibbonObjectType.MenuGroup; }

        public override TRibbonObject AddNew(RibbonObjectType objType)
        {
            TRibbonObject result;
            if (objType == RibbonObjectType.ToggleButton || objType == RibbonObjectType.CheckBox
                || objType == RibbonObjectType.DropDownColorPicker)
            {
                result = Owner.CreateObject(objType, this);
                Controls.Add(result as TRibbonControl);
            }
            else
                result = base.AddNew(objType);
            return result;
        }
    }

    class TRibbonMiniToolbarMenuGroup : TRibbonMenuGroup
    {
        public TRibbonMiniToolbarMenuGroup(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent) { }

        public TRibbonMiniToolbarMenuGroup(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent) { }

        #region Internal Declarations

        protected override bool AddControl(XElement E)
        {
            bool result = true;
            if (E.Name.LocalName == EN_COMBO_BOX)
                Controls.Add(new TRibbonComboBox(Owner, E, this));
            else if (E.Name.LocalName == EN_SPINNER)
                Controls.Add(new TRibbonSpinner(Owner, E, this));
            else if (E.Name.LocalName == EN_FONT_CONTROL)
                Controls.Add(new TRibbonFloatieFontControl(Owner, E, this));
            else
                result = base.AddControl(E);
            return result;
        }

        #endregion Internal Declarations

        public override RibbonObjectType ObjectType() { return RibbonObjectType.MiniToolbarMenuGroup; }

        public override TRibbonObject AddNew(RibbonObjectType objType)
        {
            TRibbonObject result;
            if (objType == RibbonObjectType.ComboBox || objType == RibbonObjectType.Spinner
                || objType == RibbonObjectType.FloatieFontControl)
            {
                result = Owner.CreateObject(objType, this);
                Controls.Add(result as TRibbonControl);
            }
            else
                result = base.AddNew(objType);
            return null;
        }
    }

    class TRibbonButton : TRibbonControl
    {
        public TRibbonButton(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent) { }

        public TRibbonButton(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent) { }

        #region Internal Declarations

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_BUTTON);
            base.Save(writer);
            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        public override RibbonObjectType ObjectType() { return RibbonObjectType.Button; }

        public override bool SupportApplicationModes()
        {
            if (Parent is TRibbonAppMenuGroup && Parent.Parent is TRibbonApplicationMenu)
                return true;
            else
                return false;
        }
    }

    class TRibbonToggleButton : TRibbonControl
    {
        public TRibbonToggleButton(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent) { }
        public TRibbonToggleButton(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent) { }

        #region Internal Declarations

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_TOGGLE_BUTTON);
            base.Save(writer);
            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        public override RibbonObjectType ObjectType() { return RibbonObjectType.ToggleButton; }

        public override bool SupportApplicationModes() { return false; }
    }

    class TRibbonSplitButton : TRibbonControl
    {
        #region Internal Declarations

        TRibbonControl _buttonItem;
        TRibbonList<TRibbonMenuGroup> _menuGroups;
        TRibbonList<TRibbonControl> _controls;

        public TRibbonSplitButton(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent)
        {
            _menuGroups = new TRibbonList<TRibbonMenuGroup>(owner, true);
            _controls = new TRibbonList<TRibbonControl>(owner, true);
        }

        public TRibbonSplitButton(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent)
        {
            _menuGroups = new TRibbonList<TRibbonMenuGroup>(owner, true);
            _controls = new TRibbonList<TRibbonControl>(owner, true);
            bool hasMenuGroups = false;
            foreach (XElement C in E.Elements())
            {
                if (C.Name.LocalName == EN_SPLIT_BUTTON_BUTTON_ITEM)
                {
                    if (C.Elements().Count() > 1)
                        Error(C, RS_MULTIPLE_ELEMENTS, C.Name.LocalName, C.Elements().ElementAt(0).Name.LocalName);
                    if (C.Elements().Count() == 1)
                    {
                        XElement GC = C.Elements().ElementAt(0);
                        if (GC.Name.LocalName == EN_BUTTON)
                            _buttonItem = new TRibbonButton(owner, GC, this);
                        else if (GC.Name.LocalName == EN_TOGGLE_BUTTON)
                            _buttonItem = new TRibbonToggleButton(owner, GC, this);
                        else
                            Error(GC, RS_INVALID_BUTTON_ITEM);
                    }
                }
                else if (C.Name.LocalName == EN_SPLIT_BUTTON_MENU_GROUPS)
                {
                    if (hasMenuGroups)
                        Error(C, RS_SINGLE_ELEMENT, C.Name.LocalName, EN_MENU_GROUP);
                    hasMenuGroups = true;
                    foreach (XElement GC in C.Elements())
                        _menuGroups.Add(new TRibbonMenuGroup(owner, GC, this));
                }
                else if (C.Name.LocalName == EN_TOGGLE_BUTTON)
                    _controls.Add(new TRibbonToggleButton(owner, C, this));
                else if (C.Name.LocalName == EN_CHECK_BOX)
                    _controls.Add(new TRibbonCheckBox(owner, C, this));
                else if (C.Name.LocalName == EN_BUTTON)
                    _controls.Add(new TRibbonButton(owner, C, this));
                else if (C.Name.LocalName == EN_SPLIT_BUTTON)
                    _controls.Add(new TRibbonSplitButton(owner, C, this));
                else if (C.Name.LocalName == EN_DROP_DOWN_BUTTON)
                    _controls.Add(new TRibbonDropDownButton(owner, C, this));
                else if (C.Name.LocalName == EN_DROP_DOWN_GALLERY)
                    _controls.Add(new TRibbonDropDownGallery(owner, C, this));
                else if (C.Name.LocalName == EN_SPLIT_BUTTON_GALLERY)
                    _controls.Add(new TRibbonSplitButtonGallery(owner, C, this));
                else if (C.Name.LocalName == EN_DROP_DOWN_COLOR_PICKER)
                    _controls.Add(new TRibbonDropDownColorPicker(owner, C, this));
                else
                    Error(C, RS_UNSUPPORTED_CHILD_ELEMENT, C.Name.LocalName, E.Name.LocalName);
            }

            // There must be either 1 || more controls, || a menu group
            if ((_controls.Count > 0) && (_menuGroups.Count > 0))
                Error(E, RS_INVALID_SPLITBUTTON);
            //  else if ((_controls.Count == 0) && (_menuGroups.Count == 0))
            //    Error(E, RS_INVALID_SPLITBUTTON);
        }

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_SPLIT_BUTTON);
            base.Save(writer);

            if (null != _buttonItem)
            {
                writer.WriteStartElement(EN_SPLIT_BUTTON_BUTTON_ITEM);
                _buttonItem.Save(writer);
                writer.WriteEndElement();
            }

            if (_menuGroups.Count > 0)
            {
                writer.WriteStartElement(EN_SPLIT_BUTTON_MENU_GROUPS);
                foreach (TRibbonMenuGroup group in _menuGroups)
                    group.Save(writer);

                writer.WriteEndElement();
            }

            foreach (TRibbonControl control in _controls)
                control.Save(writer);

            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        //destructor
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _menuGroups?.Dispose();
                _controls?.Dispose();
                _buttonItem?.Dispose();
            }
            base.Dispose(disposing);
        }

        public override RibbonObjectType ObjectType() { return RibbonObjectType.SplitButton; }

        public override bool SupportApplicationModes()
        {
            if (Parent is TRibbonAppMenuGroup && Parent.Parent is TRibbonApplicationMenu)
                return true;
            else
                return false;
        }

        public override TRibbonObject AddNew(RibbonObjectType objType)
        {
            TRibbonObject result;
            if (objType == RibbonObjectType.MenuGroup)
            {
                if (_controls.Count > 0)
                    Error(null, RS_CANNOT_ADD_MENU_GROUP_TO_SPLIT_BUTTON);
                result = new TRibbonMenuGroup(Owner, this);
                _menuGroups.Add((TRibbonMenuGroup)(result));
            }
            else if (objType == RibbonObjectType.ToggleButton || objType == RibbonObjectType.Button
               || objType == RibbonObjectType.CheckBox || objType == RibbonObjectType.SplitButton
               || objType == RibbonObjectType.DropDownButton || objType == RibbonObjectType.DropDownGallery
               || objType == RibbonObjectType.SplitButtonGallery
               || objType == RibbonObjectType.DropDownColorPicker)
            {
                if (_menuGroups.Count > 0)
                    Error(null, RS_CANNOT_ADD_CONTROL_TO_SPLIT_BUTTON);
                result = Owner.CreateObject(objType, this);
                _controls.Add(result as TRibbonControl);
            }
            else
                result = base.AddNew(objType);
            return result;
        }

        public override bool Remove(TRibbonObject obj)
        {
            bool result;
            if (obj.ObjectType() == RibbonObjectType.MenuGroup)
                result = _menuGroups.Remove(obj as TRibbonMenuGroup);
            else if (obj is TRibbonControl)
                result = _controls.Remove((TRibbonControl)(obj));
            else
                result = base.Remove(obj);
            return result;
        }

        public override bool Reorder(TRibbonObject child, int direction)
        {
            bool result;
            if (child is TRibbonMenuGroup)
                result = _menuGroups.Reorder(child, direction);
            else if (child is TRibbonControl)
                result = _controls.Reorder(child, direction);
            else
                result = base.Reorder(child, direction);
            return result;
        }

        [Obsolete]
        public void DeleteButtonItem()
        {
            RemoveButtonItem();
        }

        public void RemoveButtonItem()
        {
            if (_buttonItem != null)  //FreeAndNil
            {
                _buttonItem.Dispose();
                _buttonItem = null;
            }
        }

        public void CreateButtonItem()
        {
            if (_buttonItem != null)  //FreeAndNil
            {
                _buttonItem.Dispose();
                _buttonItem = null;
            }
            _buttonItem = new TRibbonButton(Owner, this);
        }

        public void CreateToggleButtonItem()
        {
            if (_buttonItem != null) //FreeAndNil
            {
                _buttonItem.Dispose();
                _buttonItem = null;
            }
            _buttonItem = new TRibbonToggleButton(Owner, this);
        }

        /* Either a TRibbonButton or TRibbonToggleButton  */
        public TRibbonControl ButtonItem { get { return _buttonItem; } }

        public TRibbonList<TRibbonMenuGroup> MenuGroups { get { return _menuGroups; } }
        public TRibbonList<TRibbonControl> Controls { get { return _controls; } }
    }

    class TRibbonDropDownButton : TRibbonControl
    {
        #region Internal Declarations

        private TRibbonList<TRibbonControl> _controls;
        private TRibbonList<TRibbonMenuGroup> _menuGroups;

        public TRibbonDropDownButton(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent)
        {
            _controls = new TRibbonList<TRibbonControl>(owner, true);
            _menuGroups = new TRibbonList<TRibbonMenuGroup>(owner, true);
        }

        public TRibbonDropDownButton(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent)
        {
            _controls = new TRibbonList<TRibbonControl>(owner, true);
            _menuGroups = new TRibbonList<TRibbonMenuGroup>(owner, true);
            foreach (XElement C in E.Elements())
            {
                if (C.Name.LocalName == EN_MENU_GROUP)
                    _menuGroups.Add(new TRibbonMenuGroup(owner, C, this));
                else if (C.Name.LocalName == EN_TOGGLE_BUTTON)
                    _controls.Add(new TRibbonToggleButton(owner, C, this));
                else if (C.Name.LocalName == EN_CHECK_BOX)
                    _controls.Add(new TRibbonCheckBox(owner, C, this));
                else if (C.Name.LocalName == EN_BUTTON)
                    _controls.Add(new TRibbonButton(owner, C, this));
                else if (C.Name.LocalName == EN_SPLIT_BUTTON)
                    _controls.Add(new TRibbonSplitButton(owner, C, this));
                else if (C.Name.LocalName == EN_DROP_DOWN_BUTTON)
                    _controls.Add(new TRibbonDropDownButton(owner, C, this));
                else if (C.Name.LocalName == EN_DROP_DOWN_GALLERY)
                    _controls.Add(new TRibbonDropDownGallery(owner, C, this));
                else if (C.Name.LocalName == EN_SPLIT_BUTTON_GALLERY)
                    _controls.Add(new TRibbonSplitButtonGallery(owner, C, this));
                else if (C.Name.LocalName == EN_DROP_DOWN_COLOR_PICKER)
                    _controls.Add(new TRibbonDropDownColorPicker(owner, C, this));
                else
                    Error(C, RS_UNSUPPORTED_CHILD_ELEMENT, C.Name.LocalName, E.Name.LocalName);
            }

            // There must be either 0 || more controls || 1 menu group || more
            if ((_controls.Count > 0) && (_menuGroups.Count > 0))
                Error(E, RS_INVALID_DROPDOWN_BUTTON);
        }

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_DROP_DOWN_BUTTON);
            base.Save(writer);

            foreach (TRibbonMenuGroup group in _menuGroups)
                group.Save(writer);

            foreach (TRibbonControl control in _controls)
                control.Save(writer);

            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        //destructor
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _controls?.Dispose();
                _menuGroups?.Dispose();
            }
            base.Dispose(disposing);
        }

        public override RibbonObjectType ObjectType() { return RibbonObjectType.DropDownButton; }

        public override bool SupportApplicationModes()
        {
            if (Parent is TRibbonAppMenuGroup && Parent.Parent is TRibbonApplicationMenu)
                return true;
            else
                return false;
        }

        public override TRibbonObject AddNew(RibbonObjectType objType)
        {
            TRibbonObject result;
            if (objType == RibbonObjectType.MenuGroup)
            {
                if (_controls.Count > 0)
                    Error(null, RS_CANNOT_ADD_MENU_GROUP_TO_DROP_DOWN_BUTTON);
                result = new TRibbonMenuGroup(Owner, this);
                _menuGroups.Add((TRibbonMenuGroup)(result));
            }
            else if (objType == RibbonObjectType.ToggleButton || objType == RibbonObjectType.CheckBox
               || objType == RibbonObjectType.Button || objType == RibbonObjectType.SplitButton
               || objType == RibbonObjectType.DropDownButton || objType == RibbonObjectType.DropDownGallery
               || objType == RibbonObjectType.SplitButtonGallery
               || objType == RibbonObjectType.DropDownColorPicker)
            {
                if (_menuGroups.Count > 0)
                    Error(null, RS_CANNOT_ADD_CONTROL_TO_DROP_DOWN_BUTTON);
                result = Owner.CreateObject(objType, this);
                _controls.Add(result as TRibbonControl);
            }
            else
                result = base.AddNew(objType);
            return result;
        }

        public override bool Remove(TRibbonObject obj)
        {
            bool result;
            if (obj.ObjectType() == RibbonObjectType.MenuGroup)
                result = _menuGroups.Remove(obj as TRibbonMenuGroup);
            else if (obj is TRibbonControl)
                result = _controls.Remove((TRibbonControl)(obj));
            else
                result = base.Remove(obj);
            return result;
        }

        public override bool Reorder(TRibbonObject child, int direction)
        {
            bool result;
            if (child is TRibbonMenuGroup)
                result = _menuGroups.Reorder(child, direction);
            else if (child is TRibbonControl)
                result = _controls.Reorder(child, direction);
            else
                result = base.Reorder(child, direction);
            return result;
        }

        public TRibbonList<TRibbonControl> Controls { get { return _controls; } }
        public TRibbonList<TRibbonMenuGroup> MenuGroups { get { return _menuGroups; } }
    }

    class TRibbonDropDownColorPicker : TRibbonControl
    {
        static readonly string[] COLOR_TEMPLATES = { ES_THEME_COLORS, ES_STANDARD_COLORS, ES_HIGHLIGHT_COLORS };
        static readonly string[] CHIP_SIZES = { ES_SMALL, ES_MEDIUM, ES_LARGE };

        #region Internal Declarations

        private RibbonColorTemplate _colorTemplate;
        private RibbonChipSize _chipSize;
        private int _columns;
        private int _themeColorGridRows;
        private int _standardColorGridRows;
        private int _recentColorGridRows;
        private bool _isAutomaticColorButtonVisible;
        private bool _isNoColorButtonVisible;

        public TRibbonDropDownColorPicker(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent)
        {
            _colorTemplate = RibbonColorTemplate.ThemeColors;
            _chipSize = RibbonChipSize.Small;
            _isAutomaticColorButtonVisible = true;
            _isNoColorButtonVisible = true;
        }

        public TRibbonDropDownColorPicker(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent)
        {
            string s = E.Attribute(AN_COLOR_TEMPLATE)?.Value;
            if ((string.IsNullOrEmpty(s)) || (s == ES_THEME_COLORS))
                _colorTemplate = RibbonColorTemplate.ThemeColors;
            else if (s == ES_STANDARD_COLORS)
                _colorTemplate = RibbonColorTemplate.StandardColors;
            else if (s == ES_HIGHLIGHT_COLORS)
                _colorTemplate = RibbonColorTemplate.HighlightColors;
            else
                Error(E, RS_INVALID_COLOR_TEMPLATE);

            s = E.Attribute(AN_CHIP_SIZE)?.Value;
            if ((string.IsNullOrEmpty(s)) || (s == ES_SMALL))
                _chipSize = RibbonChipSize.Small;
            else if (s == ES_MEDIUM)
                _chipSize = RibbonChipSize.Medium;
            else if (s == ES_LARGE)
                _chipSize = RibbonChipSize.Large;
            else
                Error(E, RS_INVALID_CHIP_SIZE);

            string tmp = E.Attribute(AN_COLUMNS)?.Value;
            _columns = string.IsNullOrEmpty(tmp) ? 0 : XmlConvert.ToInt32(tmp);
            tmp = E.Attribute(AN_THEME_COLOR_GRID_ROWS)?.Value;
            _themeColorGridRows = string.IsNullOrEmpty(tmp) ? 0 : XmlConvert.ToInt32(tmp);
            tmp = E.Attribute(AN_STANDARD_COLOR_GRID_ROWS)?.Value;
            _standardColorGridRows = string.IsNullOrEmpty(tmp) ? 0 : XmlConvert.ToInt32(tmp);
            tmp = E.Attribute(AN_RECENT_COLOR_GRID_ROWS)?.Value;
            _recentColorGridRows = string.IsNullOrEmpty(tmp) ? 0 : XmlConvert.ToInt32(tmp);
            _isAutomaticColorButtonVisible = AttributeAsBooleanDef(E, AN_IS_AUTOMATIC_COLOR_BUTTON_VISIBLE, true);
            _isNoColorButtonVisible = AttributeAsBooleanDef(E, AN_IS_NO_COLOR_BUTTON_VISIBLE, true);
        }

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_DROP_DOWN_COLOR_PICKER);
            base.Save(writer);
            if (_colorTemplate != RibbonColorTemplate.ThemeColors)
                writer.WriteAttributeString(AN_COLOR_TEMPLATE, COLOR_TEMPLATES[(int)_colorTemplate]);
            if (_chipSize != RibbonChipSize.Small)
                writer.WriteAttributeString(AN_CHIP_SIZE, CHIP_SIZES[(int)_chipSize]);
            if (_columns != 0)
                writer.WriteAttributeString(AN_COLUMNS, XmlConvert.ToString(_columns));
            if (_themeColorGridRows != 0)
                writer.WriteAttributeString(AN_THEME_COLOR_GRID_ROWS, XmlConvert.ToString(_themeColorGridRows));
            if (_standardColorGridRows != 0)
                writer.WriteAttributeString(AN_STANDARD_COLOR_GRID_ROWS, XmlConvert.ToString(_standardColorGridRows));
            if (_recentColorGridRows != 0)
                writer.WriteAttributeString(AN_RECENT_COLOR_GRID_ROWS, XmlConvert.ToString(_recentColorGridRows));
            if (!_isAutomaticColorButtonVisible)
                writer.WriteAttributeString(AN_IS_AUTOMATIC_COLOR_BUTTON_VISIBLE, XmlConvert.ToString(_isAutomaticColorButtonVisible));
            if (!_isNoColorButtonVisible)
                writer.WriteAttributeString(AN_IS_NO_COLOR_BUTTON_VISIBLE, XmlConvert.ToString(_isNoColorButtonVisible));
            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        public override RibbonObjectType ObjectType() { return RibbonObjectType.DropDownColorPicker; }

        public override bool SupportApplicationModes() { return false; }

        public RibbonColorTemplate ColorTemplate { get { return _colorTemplate; } set { _colorTemplate = value; } }
        public RibbonChipSize ChipSize { get { return _chipSize; } set { _chipSize = value; } }
        public int Columns { get { return _columns; } set { _columns = value; } }
        public int ThemeColorGridRows { get { return _themeColorGridRows; } set { _themeColorGridRows = value; } }
        public int StandardColorGridRows { get { return _standardColorGridRows; } set { _standardColorGridRows = value; } }
        public int RecentColorGridRows { get { return _recentColorGridRows; } set { _recentColorGridRows = value; } }
        public bool IsAutomaticColorButtonVisible
        {
            get { return _isAutomaticColorButtonVisible; }
            set { _isAutomaticColorButtonVisible = value; }
        }
        public bool IsNoColorButtonVisible
        {
            get { return _isNoColorButtonVisible; }
            set { _isNoColorButtonVisible = value; }
        }
    }

    class TRibbonSpinner : TRibbonControl
    {
        public TRibbonSpinner(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent)
        {
        }

        public TRibbonSpinner(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent) { }

        #region Internal Declarations

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_SPINNER);
            base.Save(writer);
            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        public override RibbonObjectType ObjectType() { return RibbonObjectType.Spinner; }

        public override bool SupportApplicationModes() { return false; }
    }

    class TRibbonCheckBox : TRibbonControl
    {
        public TRibbonCheckBox(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent) { }

        public TRibbonCheckBox(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent) { }

        #region Internal Declarations

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_CHECK_BOX);
            base.Save(writer);
            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        public override RibbonObjectType ObjectType() { return RibbonObjectType.CheckBox; }

        public override bool SupportApplicationModes() { return false; }
    }

    class TRibbonComboBox : TRibbonControl
    {
        static readonly string[] RESIZE_TYPES = { ES_NO_RESIZE, ES_VERTICAL_RESIZE };
        #region Internal Declarations

        private bool _isEditable;
        private RibbonComboBoxResizeType _resizeType;
        private bool _isAutoCompleteEnabled;

        public TRibbonComboBox(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent)
        {
            _resizeType = RibbonComboBoxResizeType.NoResize;
            _isEditable = true;
            _isAutoCompleteEnabled = true;
        }

        public TRibbonComboBox(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent)
        {
            string s = E.Attribute(AN_RESIZE_TYPE)?.Value;
            if ((string.IsNullOrEmpty(s)) || (s == ES_NO_RESIZE))
                _resizeType = RibbonComboBoxResizeType.NoResize;
            else if (s == ES_VERTICAL_RESIZE)
                _resizeType = RibbonComboBoxResizeType.VerticalResize;
            else
                Error(E, RS_INVALID_RESIZE_TYPE);
            _isEditable = AttributeAsBooleanDef(E, AN_IS_EDITABLE, true);
            _isAutoCompleteEnabled = AttributeAsBooleanDef(E, AN_IS_AUTO_COMPLETE_ENABLED, true);
        }

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_COMBO_BOX);
            base.Save(writer);

            if (_resizeType != RibbonComboBoxResizeType.NoResize)
                writer.WriteAttributeString(AN_RESIZE_TYPE, RESIZE_TYPES[(int)_resizeType]);

            if (!_isEditable)
                writer.WriteAttributeString(AN_IS_EDITABLE, XmlConvert.ToString(_isEditable));

            if (!_isAutoCompleteEnabled)
                writer.WriteAttributeString(AN_IS_AUTO_COMPLETE_ENABLED, XmlConvert.ToString(_isAutoCompleteEnabled));

            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        public override RibbonObjectType ObjectType() { return RibbonObjectType.ComboBox; }

        public override bool SupportApplicationModes() { return false; }

        public bool IsEditable { get { return _isEditable; } set { _isEditable = value; } }
        public RibbonComboBoxResizeType ResizeType { get { return _resizeType; } set { _resizeType = value; } }
        public bool IsAutoCompleteEnabled { get { return _isAutoCompleteEnabled; } set { _isAutoCompleteEnabled = value; } }
    }

    abstract class TRibbonQatControl : TRibbonControl
    {
        #region Internal Declarations

        private bool _isChecked;

        public TRibbonQatControl(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent)
        {
            _isChecked = true;
        }

        public TRibbonQatControl(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent)
        {
            _isChecked = AttributeAsBooleanDef(E, AN_APPLICATION_DEFAULTS_IS_CHECKED, true);
        }

        internal override void Save(XmlWriter writer)
        {
            base.Save(writer);
            writer.WriteAttributeString(AN_APPLICATION_DEFAULTS_IS_CHECKED, XmlConvert.ToString(_isChecked));
        }

        #endregion Internal Declarations

        public override bool SupportApplicationModes() { return false; }

        public bool IsChecked { get { return _isChecked; } set { _isChecked = value; } }
    }

    class TRibbonQatButton : TRibbonQatControl
    {
        public TRibbonQatButton(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent) { }

        public TRibbonQatButton(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent) { }

        #region Internal Declarations

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_BUTTON);
            base.Save(writer);
            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        public override RibbonObjectType ObjectType() { return RibbonObjectType.QatButton; }
    }

    class TRibbonQatToggleButton : TRibbonQatControl
    {
        public TRibbonQatToggleButton(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent) { }

        public TRibbonQatToggleButton(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent) { }

        #region Internal Declarations

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_TOGGLE_BUTTON);
            base.Save(writer);
            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        public override RibbonObjectType ObjectType() { return RibbonObjectType.QatToggleButton; }
    }

    class TRibbonQatCheckBox : TRibbonQatControl
    {
        public TRibbonQatCheckBox(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent) { }

        public TRibbonQatCheckBox(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent) { }

        #region Internal Declarations

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_CHECK_BOX);
            base.Save(writer);
            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        public override RibbonObjectType ObjectType() { return RibbonObjectType.QatCheckBox; }
    }

    class TRibbonHelpButton : TRibbonControl
    {
        public TRibbonHelpButton(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent) { }

        public TRibbonHelpButton(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent) { }

        #region Internal Declarations

        internal override void Save(XmlWriter writer)
        {
            if (CommandRef == null)
                return;

            writer.WriteStartElement(EN_RIBBON_HELP_BUTTON);
            writer.WriteStartElement(EN_HELP_BUTTON);
            base.Save(writer);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        public override RibbonObjectType ObjectType() { return RibbonObjectType.HelpButton; }

        public override bool SupportApplicationModes() { return false; }

        public override bool CanReorder()
        {
            return false;
        }
    }

    class TRibbonFloatieFontControl : TRibbonControl
    {
        #region Internal Declarations

        private bool _showTrueTypeOnly;
        private bool _showVerticalFonts;
        private int _minimumFontSize;
        private int _maximumFontSize;
        private void SetMaximumFontSize(int value)
        {
            _maximumFontSize = Addons.EnsureRange(value, 1, 9999);
        }

        private void SetMinimumFontSize(int value)
        {
            _minimumFontSize = Addons.EnsureRange(value, 1, 9999);
        }

        public TRibbonFloatieFontControl(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent)
        {
            _showVerticalFonts = true;
            _minimumFontSize = 1;
            _maximumFontSize = 9999;
        }

        public TRibbonFloatieFontControl(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent)
        {
            _showTrueTypeOnly = AttributeAsBooleanDef(E, AN_SHOW_TRUE_TYPE_ONLY, false);
            _showVerticalFonts = AttributeAsBooleanDef(E, AN_SHOW_VERTICAL_FONTS, true);
            _minimumFontSize = AttributeAsIntegerDef(E, AN_MINIMUM_FONT_SIZE, 1);
            _maximumFontSize = AttributeAsIntegerDef(E, AN_MAXIMUM_FONT_SIZE, 9999);
            _minimumFontSize = Addons.EnsureRange(_minimumFontSize, 1, 9999);
            _maximumFontSize = Addons.EnsureRange(_maximumFontSize, 1, 9999);
        }

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_FONT_CONTROL);
            base.Save(writer);
            SaveAttributes(writer);
            writer.WriteEndElement();
        }

        protected virtual void SaveAttributes(XmlWriter writer)
        {
            if (_showTrueTypeOnly)
                writer.WriteAttributeString(AN_SHOW_TRUE_TYPE_ONLY, XmlConvert.ToString(_showTrueTypeOnly));
            if (!_showVerticalFonts)
                writer.WriteAttributeString(AN_SHOW_VERTICAL_FONTS, XmlConvert.ToString(_showVerticalFonts));
            if (_minimumFontSize != 1)
                writer.WriteAttributeString(AN_MINIMUM_FONT_SIZE, XmlConvert.ToString(_minimumFontSize));
            if (_maximumFontSize != 9999)
                writer.WriteAttributeString(AN_MAXIMUM_FONT_SIZE, XmlConvert.ToString(_maximumFontSize));
        }

        #endregion Internal Declarations

        public override RibbonObjectType ObjectType() { return RibbonObjectType.FloatieFontControl; }

        public override bool SupportApplicationModes() { return false; }

        public bool ShowTrueTypeOnly { get { return _showTrueTypeOnly; } set { _showTrueTypeOnly = value; } }
        public bool ShowVerticalFonts { get { return _showVerticalFonts; } set { _showVerticalFonts = value; } }
        public int MinimumFontSize { get { return _minimumFontSize; } set { SetMinimumFontSize(value); } }
        public int MaximumFontSize { get { return _maximumFontSize; } set { SetMaximumFontSize(value); } }
    }

    class TRibbonFontControl : TRibbonFloatieFontControl
    {
        static readonly string[] FONT_TYPES = { ES_FONT_ONLY, ES_FONT_WITH_COLOR, ES_RICH_FONT };
        #region Internal Declarations

        private RibbonFontType _fontType;
        private bool _isStrikethroughButtonVisible;
        private bool _isUnderlineButtonVisible;
        private bool _isHighlightButtonVisible;

        public TRibbonFontControl(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent)
        {
            _fontType = RibbonFontType.FontOnly;
            _isStrikethroughButtonVisible = true;
            _isUnderlineButtonVisible = true;
        }

        public TRibbonFontControl(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent)
        {
            string s = E.Attribute(AN_FONT_TYPE)?.Value;
            if ((s == ES_FONT_ONLY) || (string.IsNullOrEmpty(s)))
                _fontType = RibbonFontType.FontOnly;
            else if (s == ES_FONT_WITH_COLOR)
                _fontType = RibbonFontType.FontWithColor;
            else if (s == ES_RICH_FONT)
                _fontType = RibbonFontType.RichFont;
            else
                Error(E, RS_INVALID_FONT_TYPE);

            _isStrikethroughButtonVisible = AttributeAsBooleanDef(E, AN_IS_STRIKETHROUGH_BUTTON_VISIBLE, true);
            _isUnderlineButtonVisible = AttributeAsBooleanDef(E, AN_IS_UNDERLINE_BUTTON_VISIBLE, true);
            _isHighlightButtonVisible = AttributeAsBooleanDef(E, AN_IS_HIGHLIGHT_BUTTON_VISIBLE, (_fontType != RibbonFontType.FontOnly));
        }

        protected override void SaveAttributes(XmlWriter writer)
        {
            if (_fontType != RibbonFontType.FontOnly)
                writer.WriteAttributeString(AN_FONT_TYPE, FONT_TYPES[(int)_fontType]);
            if (_fontType != RibbonFontType.RichFont)
            {
                if (!_isStrikethroughButtonVisible)
                    writer.WriteAttributeString(AN_IS_STRIKETHROUGH_BUTTON_VISIBLE, XmlConvert.ToString(_isStrikethroughButtonVisible));
                if (!_isUnderlineButtonVisible)
                    writer.WriteAttributeString(AN_IS_UNDERLINE_BUTTON_VISIBLE, XmlConvert.ToString(_isUnderlineButtonVisible));
                if ((_fontType != RibbonFontType.FontOnly) && (_isHighlightButtonVisible))
                    writer.WriteAttributeString(AN_IS_HIGHLIGHT_BUTTON_VISIBLE, XmlConvert.ToString(_isHighlightButtonVisible));
            }
            base.SaveAttributes(writer);
        }

        #endregion Internal Declarations

        public override RibbonObjectType ObjectType() { return RibbonObjectType.FontControl; }

        public RibbonFontType FontType { get { return _fontType; } set { _fontType = value; } }
        public bool IsStrikethroughButtonVisible { get { return _isStrikethroughButtonVisible; } set { _isStrikethroughButtonVisible = value; } }
        public bool IsUnderlineButtonVisible { get { return _isUnderlineButtonVisible; } set { _isUnderlineButtonVisible = value; } }
        public bool IsHighlightButtonVisible { get { return _isHighlightButtonVisible; } set { _isHighlightButtonVisible = value; } }
    }

    /* NOTE: This is not an actual control, since it doesn't have a CommandRef
     property. However, we treat it as a control so it can be places alongside
     other controls.  */
    class TRibbonControlGroup : TRibbonControl
    {
        #region Internal Declarations

        private int _sequenceNumber;
        private TRibbonList<TRibbonControl> _controls;

        public TRibbonControlGroup(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent)
        {
            _controls = new TRibbonList<TRibbonControl>(owner, true);
        }

        public TRibbonControlGroup(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent)
        {
            _controls = new TRibbonList<TRibbonControl>(owner, true);
            string tmp = E.Attribute(AN_SEQUENCE_NUMBER)?.Value;
            _sequenceNumber = string.IsNullOrEmpty(tmp) ? 0 : XmlConvert.ToInt32(tmp);
            foreach (XElement C in E.Elements())
            {
                if (C.Name.LocalName == EN_CONTROL_GROUP)
                    _controls.Add(new TRibbonControlGroup(owner, C, null));
                else if (C.Name.LocalName == EN_TOGGLE_BUTTON)
                    _controls.Add(new TRibbonToggleButton(owner, null));
                else if (C.Name.LocalName == EN_CHECK_BOX)
                    _controls.Add(new TRibbonCheckBox(owner, C, null));
                else if (C.Name.LocalName == EN_BUTTON)
                    _controls.Add(new TRibbonButton(owner, C, null));
                else if (C.Name.LocalName == EN_SPLIT_BUTTON)
                    _controls.Add(new TRibbonSplitButton(owner, C, null));
                else if (C.Name.LocalName == EN_DROP_DOWN_BUTTON)
                    _controls.Add(new TRibbonDropDownButton(owner, C, null));
                else if (C.Name.LocalName == EN_DROP_DOWN_GALLERY)
                    _controls.Add(new TRibbonDropDownGallery(owner, C, null));
                else if (C.Name.LocalName == EN_SPLIT_BUTTON_GALLERY)
                    _controls.Add(new TRibbonSplitButtonGallery(owner, C, null));
                else if (C.Name.LocalName == EN_DROP_DOWN_COLOR_PICKER)
                    _controls.Add(new TRibbonDropDownColorPicker(owner, C, null));
                else if (C.Name.LocalName == EN_COMBO_BOX)
                    _controls.Add(new TRibbonComboBox(owner, C, null));
                else if (C.Name.LocalName == EN_SPINNER)
                    _controls.Add(new TRibbonSpinner(owner, C, null));
                else if (C.Name.LocalName == EN_IN_RIBBON_GALLERY)
                    _controls.Add(new TRibbonInRibbonGallery(owner, C, null));
                else if (C.Name.LocalName == EN_FONT_CONTROL)
                    _controls.Add(new TRibbonFontControl(owner, C, null));
                else
                    Error(C, RS_UNSUPPORTED_CHILD_ELEMENT, C.Name.LocalName, E.Name.LocalName);
            }
        }

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_CONTROL_GROUP);
            // NO inherited

            if (_sequenceNumber != 0)
                writer.WriteAttributeString(AN_SEQUENCE_NUMBER, XmlConvert.ToString(_sequenceNumber));

            foreach (TRibbonControl control in _controls)
                control.Save(writer);

            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        public override string DisplayName()
        {
            return RS_CONTROL_GROUP;
        }

        public override RibbonObjectType ObjectType() { return RibbonObjectType.ControlGroup; }

        public override bool SupportApplicationModes() { return false; }

        public override TRibbonObject AddNew(RibbonObjectType objType)
        {
            TRibbonObject result;
            if (objType == RibbonObjectType.ControlGroup || objType == RibbonObjectType.ToggleButton
               || objType == RibbonObjectType.CheckBox || objType == RibbonObjectType.Button
               || objType == RibbonObjectType.SplitButton || objType == RibbonObjectType.DropDownButton
               || objType == RibbonObjectType.DropDownGallery || objType == RibbonObjectType.SplitButtonGallery
               || objType == RibbonObjectType.DropDownColorPicker || objType == RibbonObjectType.ComboBox
               || objType == RibbonObjectType.Spinner || objType == RibbonObjectType.InRibbonGallery
               || objType == RibbonObjectType.FontControl)
            {
                result = Owner.CreateObject(objType, this);
                _controls.Add(result as TRibbonControl);
            }
            else
                result = base.AddNew(objType);
            return result;
        }

        public override bool Remove(TRibbonObject obj)
        {
            bool result;
            if (obj is TRibbonControl)
                result = _controls.Remove((TRibbonControl)(obj));
            else
                result = base.Remove(obj);
            return result;
        }

        public override bool Reorder(TRibbonObject child, int direction)
        {
            bool result;
            if (child is TRibbonControl)
                result = _controls.Reorder(child, direction);
            else
                result = base.Reorder(child, direction);
            return result;
        }

        //destructor
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _controls?.Dispose();
            }
            base.Dispose(disposing);
        }

        public int SequenceNumber { get { return _sequenceNumber; } set { _sequenceNumber = value; } }
        public TRibbonList<TRibbonControl> Controls { get { return _controls; } }
    }

    abstract class TRibbonGalleryMenuLayout : TRibbonObject
    {
        #region Internal Declarations

        private int _rows;

        protected TRibbonGalleryMenuLayout(TRibbonDocument owner) : base(owner)
        {
            _rows = -1;
        }

        protected TRibbonGalleryMenuLayout(TRibbonDocument owner, XElement E) : base(owner)
        {
            _rows = AttributeAsIntegerDef(E, AN_ROWS, -1);
        }


        internal virtual void Save(XmlWriter writer)
        {
            if (_rows != -1)
                writer.WriteAttributeString(AN_ROWS, XmlConvert.ToString(_rows));
        }

        #endregion Internal Declarations

        public int Rows { get { return _rows; } set { _rows = value; } }
    }

    class TRibbonVerticalMenuLayout : TRibbonGalleryMenuLayout
    {
        static readonly string[] GRIPPERS = { ES_NONE, ES_VERTICAL };
        #region Internal Declarations

        private RibbonSingleColumnGripperType _gripper;

        public TRibbonVerticalMenuLayout(TRibbonDocument owner) : base(owner)
        {
            _gripper = RibbonSingleColumnGripperType.Vertical;
        }

        public TRibbonVerticalMenuLayout(TRibbonDocument owner, XElement E) : base(owner, E)
        {
            string s = E.Attribute(AN_GRIPPER)?.Value;
            if ((string.IsNullOrEmpty(s)) || (s == ES_VERTICAL))
                _gripper = RibbonSingleColumnGripperType.Vertical;
            else if (s == ES_NONE)
                _gripper = RibbonSingleColumnGripperType.None;
            else
                Error(E, RS_INVALID_GRIPPER);
        }

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_VERTICAL_MENU_LAYOUT);
            base.Save(writer);

            if (_gripper != RibbonSingleColumnGripperType.Vertical)
                writer.WriteAttributeString(AN_GRIPPER, GRIPPERS[(int)_gripper]);

            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        public override RibbonObjectType ObjectType() { return RibbonObjectType.VerticalMenuLayout; }

        public RibbonSingleColumnGripperType Gripper { get { return _gripper; } set { _gripper = value; } }
    }

    class TRibbonFlowMenuLayout : TRibbonGalleryMenuLayout
    {
        static readonly string[] GRIPPERS = { ES_NONE, ES_VERTICAL, ES_CORNER };
        #region Internal Declarations

        private int _columns;
        private RibbonMultiColumnGripperType _gripper;

        public TRibbonFlowMenuLayout(TRibbonDocument owner) : base(owner)
        {
            _gripper = RibbonMultiColumnGripperType.Corner;
            _columns = 2;
        }

        public TRibbonFlowMenuLayout(TRibbonDocument owner, XElement E) : base(owner, E)
        {
            string s = E.Attribute(AN_GRIPPER)?.Value;
            if ((string.IsNullOrEmpty(s)) || (s == ES_CORNER))
                _gripper = RibbonMultiColumnGripperType.Corner;
            else if (s == ES_VERTICAL)
                _gripper = RibbonMultiColumnGripperType.Vertical;
            else if (s == ES_NONE)
                _gripper = RibbonMultiColumnGripperType.None;
            else
                Error(E, RS_INVALID_GRIPPER);

            _columns = AttributeAsIntegerDef(E, AN_COLUMNS, 2);
        }

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_FLOW_MENU_LAYOUT);
            base.Save(writer);

            if (_gripper != RibbonMultiColumnGripperType.Corner)
                writer.WriteAttributeString(AN_GRIPPER, GRIPPERS[(int)_gripper]);

            if (_columns != 2)
                writer.WriteAttributeString(AN_COLUMNS, XmlConvert.ToString(_columns));

            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        public override RibbonObjectType ObjectType() { return RibbonObjectType.FlowMenuLayout; }

        public int Columns { get { return _columns; } set { _columns = value; } }
        public RibbonMultiColumnGripperType Gripper { get { return _gripper; } set { _gripper = value; } }
    }

    abstract class TRibbonGallery : TRibbonControl
    {
        public static readonly string[] GALLERY_TYPES = { ES_ITEMS, ES_COMMANDS };
        public static readonly string[] TEXT_POSITIONS = { ES_BOTTOM, ES_HIDE, ES_LEFT, ES_OVERLAP, ES_RIGHT, ES_TOP };

        #region Internal Declarations

        private RibbonGalleryType _galleryType;
        private bool _hasLargeItems;
        private int _itemHeight;
        private int _itemWidth;
        private RibbonTextPosition _textPosition;
        private TRibbonGalleryMenuLayout _menuLayout;
        private TRibbonList<TRibbonMenuGroup> _menuGroups;
        private TRibbonList<TRibbonControl> _controls;

        protected TRibbonGallery(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent)
        {
            _menuGroups = new TRibbonList<TRibbonMenuGroup>(owner, true);
            _controls = new TRibbonList<TRibbonControl>(owner, true);
            _galleryType = RibbonGalleryType.Items;
            _hasLargeItems = true;
            _itemHeight = -1;
            _itemWidth = -1;
            _textPosition = RibbonTextPosition.Bottom;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        protected TRibbonGallery(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent)
        {
            _menuGroups = new TRibbonList<TRibbonMenuGroup>(owner, true);
            _controls = new TRibbonList<TRibbonControl>(owner, true);
            string s = E.Attribute(AN_TYPE)?.Value;
            if ((string.IsNullOrEmpty(s)) || (s == ES_ITEMS))
                _galleryType = RibbonGalleryType.Items;
            else if (s == ES_COMMANDS)
                _galleryType = RibbonGalleryType.Commands;
            else
                Error(E, RS_INVALID_TYPE);

            _hasLargeItems = AttributeAsBooleanDef(E, AN_HAS_LARGE_ITEMS, true);
            _itemHeight = AttributeAsIntegerDef(E, AN_ITEM_HEIGHT, -1);
            _itemWidth = AttributeAsIntegerDef(E, AN_ITEM_WIDTH, -1);

            s = E.Attribute(AN_TEXT_POSITION)?.Value;
            if ((string.IsNullOrEmpty(s)) || (s == ES_BOTTOM))
                _textPosition = RibbonTextPosition.Bottom;
            else if (s == ES_HIDE)
                _textPosition = RibbonTextPosition.Hide;
            else if (s == ES_LEFT)
                _textPosition = RibbonTextPosition.Left;
            else if (s == ES_OVERLAP)
                _textPosition = RibbonTextPosition.Overlap;
            else if (s == ES_RIGHT)
                _textPosition = RibbonTextPosition.Right;
            else if (s == ES_TOP)
                _textPosition = RibbonTextPosition.Top;
            else
                Error(E, RS_INVALID_TEXT_POSITION);

            foreach (XElement C in E.Elements())
                if (!HandleElement(C))
                    Error(C, RS_UNSUPPORTED_CHILD_ELEMENT, C.Name.LocalName, E.Name.LocalName);

            // There must be either 1 || more controls, || a menu group
            if ((_controls.Count > 0) && (_menuGroups.Count > 0))
                Error(E, RS_INVALID_GALLERY);
            //  else if (_controls.Count == 0) && (_menuGroups.Count == 0)
            //    Error(E, RS_INVALID_GALLERY);
        }

        internal override void Save(XmlWriter writer)
        {
            base.Save(writer);
            SaveAttributes(writer);
            if (null != _menuLayout)
                SaveMenuLayout(writer);
            if (_menuGroups.Count > 0)
                SaveMenuGroups(writer);
            foreach (TRibbonControl control in _controls)
                control.Save(writer);
        }

        protected virtual void SaveAttributes(XmlWriter writer)
        {
            if (_galleryType != RibbonGalleryType.Items)
                writer.WriteAttributeString(AN_TYPE, GALLERY_TYPES[(int)_galleryType]);

            if (!_hasLargeItems)
                writer.WriteAttributeString(AN_HAS_LARGE_ITEMS, XmlConvert.ToString(_hasLargeItems));

            if (_itemHeight != -1)
                writer.WriteAttributeString(AN_ITEM_HEIGHT, XmlConvert.ToString(_itemHeight));

            if (_itemWidth != -1)
                writer.WriteAttributeString(AN_ITEM_WIDTH, XmlConvert.ToString(_itemWidth));

            if (_textPosition != RibbonTextPosition.Bottom)
                writer.WriteAttributeString(AN_TEXT_POSITION, TEXT_POSITIONS[(int)_textPosition]);
        }

        protected abstract void SaveMenuLayout(XmlWriter writer);

        protected abstract void SaveMenuGroups(XmlWriter writer);

        protected virtual bool HandleElement(XElement E)
        {
            bool result = true;
            if (E.Name.LocalName == EN_TOGGLE_BUTTON)
                _controls.Add(new TRibbonToggleButton(Owner, E, this));
            else if (E.Name.LocalName == EN_CHECK_BOX)
                _controls.Add(new TRibbonCheckBox(Owner, E, this));
            else if (E.Name.LocalName == EN_BUTTON)
                _controls.Add(new TRibbonButton(Owner, E, this));
            else if (E.Name.LocalName == EN_SPLIT_BUTTON)
                _controls.Add(new TRibbonSplitButton(Owner, E, this));
            else if (E.Name.LocalName == EN_DROP_DOWN_BUTTON)
                _controls.Add(new TRibbonDropDownButton(Owner, E, this));
            else if (E.Name.LocalName == EN_DROP_DOWN_GALLERY)
                _controls.Add(new TRibbonDropDownGallery(Owner, E, this));
            else if (E.Name.LocalName == EN_SPLIT_BUTTON_GALLERY)
                _controls.Add(new TRibbonSplitButtonGallery(Owner, E, this));
            else if (E.Name.LocalName == EN_DROP_DOWN_COLOR_PICKER)
                _controls.Add(new TRibbonDropDownColorPicker(Owner, E, this));
            else
                result = false;
            return result;
        }

        protected void LoadMenuLayout(XElement E)
        {
            if (E.Elements().Count() != 1)
                Error(E, RS_SINGLE_ELEMENT, E.Name.LocalName, EN_VERTICAL_MENU_LAYOUT + "/" + EN_FLOW_MENU_LAYOUT);

            XElement C = E.Elements().ElementAt(0);
            if (C.Name.LocalName == EN_VERTICAL_MENU_LAYOUT)
                _menuLayout = new TRibbonVerticalMenuLayout(Owner, C);
            else if (C.Name.LocalName == EN_FLOW_MENU_LAYOUT)
                _menuLayout = new TRibbonFlowMenuLayout(Owner, C);
            else
                Error(C, RS_UNSUPPORTED_CHILD_ELEMENT, C.Name.LocalName, E.Name.LocalName);
        }

        protected void LoadMenuGroups(XElement E)
        {
            if (E.Elements().Count() == 0)
                Error(E, RS_REQUIRED_ELEMENT, E.Name.LocalName, EN_MENU_GROUP);

            foreach (XElement C in E.Elements())
                _menuGroups.Add(new TRibbonMenuGroup(Owner, C, this));
        }

        #endregion Internal Declarations

        //destructor
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _menuGroups?.Dispose();
                _controls?.Dispose();
                _menuLayout?.Dispose();
            }
            base.Dispose(disposing);
        }

        [Obsolete]
        public void DeleteMenuLayout()
        {
            RemoveMenuLayout();
        }

        public void RemoveMenuLayout()
        {
            if (_menuLayout != null)
            {
                _menuLayout.Dispose();
                _menuLayout = null;
            }
        }

        public void CreateVerticalMenuLayout()
        {
            _menuLayout?.Dispose();
            _menuLayout = null;
            _menuLayout = new TRibbonVerticalMenuLayout(Owner);
        }

        public void CreateFlowMenuLayout()
        {
            _menuLayout?.Dispose();
            _menuLayout = null;
            _menuLayout = new TRibbonFlowMenuLayout(Owner);
        }

        public RibbonGalleryType GalleryType { get { return _galleryType; } set { _galleryType = value; } }
        public bool HasLargeItems { get { return _hasLargeItems; } set { _hasLargeItems = value; } }
        public int ItemHeight { get { return _itemHeight; } set { _itemHeight = value; } }
        public int ItemWidth { get { return _itemWidth; } set { _itemWidth = value; } }
        public RibbonTextPosition TextPosition { get { return _textPosition; } set { _textPosition = value; } }

        /* Either a TRibbonVerticalMenuLayout or TRibbonFlowMenuLayout  */
        public TRibbonGalleryMenuLayout MenuLayout { get { return _menuLayout; } }
        public TRibbonList<TRibbonMenuGroup> MenuGroups { get { return _menuGroups; } }
        public TRibbonList<TRibbonControl> Controls { get { return _controls; } }
    }

    class TRibbonDropDownGallery : TRibbonGallery
    {
        public TRibbonDropDownGallery(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent)
        {
        }

        public TRibbonDropDownGallery(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent) { }

        #region Internal Declarations

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_DROP_DOWN_GALLERY);
            base.Save(writer);
            writer.WriteEndElement();
        }

        protected override void SaveMenuLayout(XmlWriter writer)
        {
            writer.WriteStartElement(EN_DROP_DOWN_GALLERY_MENU_LAYOUT);
            MenuLayout.Save(writer);
            writer.WriteEndElement();
        }

        protected override void SaveMenuGroups(XmlWriter writer)
        {
            writer.WriteStartElement(EN_DROP_DOWN_GALLERY_MENU_GROUPS);
            foreach (TRibbonMenuGroup group in MenuGroups)
                group.Save(writer);

            writer.WriteEndElement();
        }

        protected override bool HandleElement(XElement E)
        {
            bool result = true;
            if (E.Name.LocalName == EN_DROP_DOWN_GALLERY_MENU_LAYOUT)
                LoadMenuLayout(E);
            else if (E.Name.LocalName == EN_DROP_DOWN_GALLERY_MENU_GROUPS)
                LoadMenuGroups(E);
            else
                result = base.HandleElement(E);
            return result;
        }

        #endregion Internal Declarations

        public override RibbonObjectType ObjectType() { return RibbonObjectType.DropDownGallery; }

        public override bool SupportApplicationModes()
        {
            if (Parent is TRibbonAppMenuGroup && Parent.Parent is TRibbonApplicationMenu)
                return true;
            else
                return false;
        }
    }

    class TRibbonSplitButtonGallery : TRibbonGallery
    {
        public TRibbonSplitButtonGallery(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent) { }

        public TRibbonSplitButtonGallery(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent) { }

        #region Internal Declarations

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_SPLIT_BUTTON_GALLERY);
            base.Save(writer);
            writer.WriteEndElement();
        }

        protected override void SaveMenuLayout(XmlWriter writer)
        {
            writer.WriteStartElement(EN_SPLIT_BUTTON_GALLERY_MENU_LAYOUT);
            MenuLayout.Save(writer);
            writer.WriteEndElement();
        }

        protected override void SaveMenuGroups(XmlWriter writer)
        {
            writer.WriteStartElement(EN_SPLIT_BUTTON_GALLERY_MENU_GROUPS);
            foreach (TRibbonMenuGroup group in MenuGroups)
                group.Save(writer);

            writer.WriteEndElement();
        }

        protected override bool HandleElement(XElement E)
        {
            bool result = true;
            if (E.Name.LocalName == EN_SPLIT_BUTTON_GALLERY_MENU_LAYOUT)
                LoadMenuLayout(E);
            else if (E.Name.LocalName == EN_SPLIT_BUTTON_GALLERY_MENU_GROUPS)
                LoadMenuGroups(E);
            else
                result = base.HandleElement(E);
            return result;
        }

        #endregion Internal Declarations

        public override RibbonObjectType ObjectType() { return RibbonObjectType.SplitButtonGallery; }

        public override bool SupportApplicationModes()
        {
            if (Parent is TRibbonAppMenuGroup && Parent.Parent is TRibbonApplicationMenu)
                return true;
            else
                return false;
        }
    }

    class TRibbonInRibbonGallery : TRibbonGallery
    {
        #region Internal Declarations

        private int _minColumnsLarge;
        private int _maxColumnsMedium;
        private int _minColumnsMedium;
        private int _maxColumns;
        private int _maxRows;

        public TRibbonInRibbonGallery(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent)
        {
            _minColumnsLarge = -1;
            _maxColumnsMedium = -1;
            _minColumnsMedium = -1;
            _maxColumns = -1;
            _maxRows = -1;
        }

        public TRibbonInRibbonGallery(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent)
        {
            _minColumnsLarge = AttributeAsIntegerDef(E, AN_MIN_COLUMNS_LARGE, -1);
            _maxColumnsMedium = AttributeAsIntegerDef(E, AN_MAX_COLUMNS_MEDIUM, -1);
            _minColumnsMedium = AttributeAsIntegerDef(E, AN_MIN_COLUMNS_MEDIUM, -1);
            _maxColumns = AttributeAsIntegerDef(E, AN_MAX_COLUMNS, -1);
            _maxRows = AttributeAsIntegerDef(E, AN_MAX_ROWS, -1);
        }

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_IN_RIBBON_GALLERY);
            base.Save(writer);
            writer.WriteEndElement();
        }

        protected override void SaveAttributes(XmlWriter writer)
        {
            base.SaveAttributes(writer);
            if (_minColumnsLarge != -1)
                writer.WriteAttributeString(AN_MIN_COLUMNS_LARGE, XmlConvert.ToString(_minColumnsLarge));
            if (_maxColumnsMedium != -1)
                writer.WriteAttributeString(AN_MAX_COLUMNS_MEDIUM, XmlConvert.ToString(_maxColumnsMedium));
            if (_minColumnsMedium != -1)
                writer.WriteAttributeString(AN_MIN_COLUMNS_MEDIUM, XmlConvert.ToString(_minColumnsMedium));
            if (_maxColumns != -1)
                writer.WriteAttributeString(AN_MAX_COLUMNS, XmlConvert.ToString(_maxColumns));
            if (_maxRows != -1)
                writer.WriteAttributeString(AN_MAX_ROWS, XmlConvert.ToString(_maxRows));
        }

        protected override void SaveMenuLayout(XmlWriter writer)
        {
            writer.WriteStartElement(EN_IN_RIBBON_GALLERY_MENU_LAYOUT);
            MenuLayout.Save(writer);
            writer.WriteEndElement();
        }

        protected override void SaveMenuGroups(XmlWriter writer)
        {
            writer.WriteStartElement(EN_IN_RIBBON_GALLERY_MENU_GROUPS);
            foreach (TRibbonMenuGroup group in MenuGroups)
                group.Save(writer);
            writer.WriteEndElement();
        }

        protected override bool HandleElement(XElement E)
        {
            bool result = true;
            if (E.Name.LocalName == EN_IN_RIBBON_GALLERY_MENU_LAYOUT)
                LoadMenuLayout(E);
            else if (E.Name.LocalName == EN_IN_RIBBON_GALLERY_MENU_GROUPS)
                LoadMenuGroups(E);
            else
                result = base.HandleElement(E);
            return result;
        }

        #endregion Internal Declarations

        public override RibbonObjectType ObjectType() { return RibbonObjectType.InRibbonGallery; }

        public override bool SupportApplicationModes()
        {
            return false;
        }

        public int MinColumnsLarge { get { return _minColumnsLarge; } set { _minColumnsLarge = value; } }
        public int MaxColumnsMedium { get { return _maxColumnsMedium; } set { _maxColumnsMedium = value; } }
        public int MinColumnsMedium { get { return _minColumnsMedium; } set { _minColumnsMedium = value; } }
        public int MaxColumns { get { return _maxColumns; } set { _maxColumns = value; } }
        public int MaxRows { get { return _maxRows; } set { _maxRows = value; } }
    }

    class TRibbonApplicationMenu : TRibbonControl
    {
        #region Internal Declarations

        private TRibbonApplicationMenuRecentItems _recentItems;
        private TRibbonList<TRibbonAppMenuGroup> _menuGroups;

        public TRibbonApplicationMenu(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent)
        {
            _menuGroups = new TRibbonList<TRibbonAppMenuGroup>(owner, true);
        }

        public TRibbonApplicationMenu(TRibbonDocument owner, XElement E, XElement E1, TRibbonCommandRefObject parent) : base(owner, E1, parent)
        {
            //@ changed at caller
            _menuGroups = new TRibbonList<TRibbonAppMenuGroup>(owner, true);
            if (E.Elements().Count() > 0)
            {
                XElement C = E.Elements().ElementAt(0);
                if (C.Name.LocalName != EN_APPLICATION_MENU)
                    Error(C, RS_ELEMENT_EXPECTED, EN_APPLICATION_MENU, C.Name.LocalName);

                if (E.Elements().Count() > 1)
                    Error(E, RS_MULTIPLE_ELEMENTS, E.Name.LocalName, C.Name.LocalName);

                foreach (XElement GC in C.Elements())
                {
                    if (GC.Name.LocalName == EN_APPLICATION_MENU_RECENT_ITEMS)
                    {
                        if (null != _recentItems)
                            Error(GC, RS_MULTIPLE_ELEMENTS, C.Name.LocalName, GC.Name.LocalName);
                        //@@ changed
                        if ((GC.Elements().Count() == 0) || (GC.Elements().ElementAt(0).Name.LocalName != EN_RECENT_ITEMS))
                            Error(GC, RS_SINGLE_ELEMENT, GC.Name.LocalName, EN_RECENT_ITEMS);
                        //@@
                        _recentItems = new TRibbonApplicationMenuRecentItems(owner, GC.Elements().ElementAt(0), this);
                    }
                    else if (GC.Name.LocalName == EN_MENU_GROUP)
                        _menuGroups.Add(new TRibbonAppMenuGroup(owner, GC, this));
                    else
                        Error(GC, RS_UNSUPPORTED_CHILD_ELEMENT, GC.Name.LocalName, C.Name.LocalName);
                }
            }
        }

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_RIBBON_APPLICATION_MENU);
            writer.WriteStartElement(EN_APPLICATION_MENU);
            base.Save(writer);

            if (null != _recentItems)
                _recentItems.Save(writer);

            foreach (TRibbonAppMenuGroup group in _menuGroups)
                group.Save(writer);

            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        //destructor
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _menuGroups?.Dispose();
                _recentItems?.Dispose();
            }
            base.Dispose(disposing);
        }

        public override RibbonObjectType ObjectType() { return RibbonObjectType.ApplicationMenu; }

        public override bool SupportApplicationModes() { return false; }

        public void EnableRecentItems(bool Enable)
        {
            if (Enable)
            {
                if (_recentItems == null)
                    _recentItems = new TRibbonApplicationMenuRecentItems(Owner, this);
            }
            else
            {
                if (_recentItems != null)
                {
                    _recentItems.Dispose();
                    _recentItems = null;
                }
            }
        }

        public override TRibbonObject AddNew(RibbonObjectType objType)
        {
            TRibbonObject result;
            if (objType == RibbonObjectType.AppMenuGroup)
            {
                result = new TRibbonAppMenuGroup(Owner, this);
                _menuGroups.Add((TRibbonAppMenuGroup)(result));
            }
            else
                result = base.AddNew(objType);
            return result;
        }

        public override bool Remove(TRibbonObject obj)
        {
            bool result;
            if (obj.ObjectType() == RibbonObjectType.AppMenuGroup)
                result = _menuGroups.Remove((TRibbonAppMenuGroup)(obj));
            else
                result = base.Remove(obj);
            return result;
        }

        public override bool CanReorder()
        {
            return false;
        }

        public override bool Reorder(TRibbonObject child, int direction)
        {
            bool result;
            if (child is TRibbonAppMenuGroup)
                result = _menuGroups.Reorder(child, direction);
            else
                result = base.Reorder(child, direction);
            return result;
        }

        public TRibbonApplicationMenuRecentItems RecentItems { get { return _recentItems; } }
        public TRibbonList<TRibbonAppMenuGroup> MenuGroups { get { return _menuGroups; } }
    }

    class TRibbonQuickAccessToolbar : TRibbonControl
    {
        #region Internal Declarations

        private TRibbonCommand _customizeCommandRef;
        private TRibbonList<TRibbonQatControl> _controls;

        public TRibbonQuickAccessToolbar(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent)
        {
            _controls = new TRibbonList<TRibbonQatControl>(owner, true);
        }

        public TRibbonQuickAccessToolbar(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent)
        {
            _controls = new TRibbonList<TRibbonQatControl>(owner, true);
            TRibbonCommandName name = StringToCommandName(E.Attribute(AN_CUSTOMIZE_COMMAND_NAME)?.Value);
            _customizeCommandRef = owner.FindCommand(name);
            if (null != _customizeCommandRef)
                _customizeCommandRef.FreeNotification(this);
            if (E.Elements().Count() > 1)
                Error(E, RS_MULTIPLE_ELEMENTS, E.Name.LocalName, E.Elements().ElementAt(0).Name.LocalName);
            if (E.Elements().Count() == 1)
            {
                XElement C = E.Elements().ElementAt(0);
                if (C.Name.LocalName != EN_QUICK_ACCESS_TOOLBAR_APPLICATION_DEFAULTS)
                    Error(C, RS_ELEMENT_EXPECTED, EN_QUICK_ACCESS_TOOLBAR_APPLICATION_DEFAULTS, C.Name.LocalName);

                foreach (XElement GC in C.Elements())
                {
                    if (GC.Name.LocalName == EN_BUTTON)
                        _controls.Add(new TRibbonQatButton(owner, GC, this));
                    else if (GC.Name.LocalName == EN_TOGGLE_BUTTON)
                        _controls.Add(new TRibbonQatToggleButton(owner, GC, this));
                    else if (GC.Name.LocalName == EN_CHECK_BOX)
                        _controls.Add(new TRibbonQatCheckBox(owner, GC, this));
                    else
                        Error(GC, RS_UNSUPPORTED_CHILD_ELEMENT, GC.Name.LocalName, C.Name.LocalName);
                }
            }
        }

        private void SetCustomizeCommandRef(TRibbonCommand value)
        {
            if (value != _customizeCommandRef)
            {
                if (null != _customizeCommandRef)
                    _customizeCommandRef.RemoveFreeNotification(this);

                _customizeCommandRef = value;

                if (null != _customizeCommandRef)
                    _customizeCommandRef.FreeNotification(this);
            }
        }

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_RIBBON_QUICK_ACCESS_TOOLBAR);
            writer.WriteStartElement(EN_QUICK_ACCESS_TOOLBAR);
            base.Save(writer);
            if (null != _customizeCommandRef)
                _customizeCommandRef.SaveRef(writer, AN_CUSTOMIZE_COMMAND_NAME);

            if (_controls.Count > 0)
            {
                writer.WriteStartElement(EN_QUICK_ACCESS_TOOLBAR_APPLICATION_DEFAULTS);

                foreach (TRibbonQatControl control in _controls)
                    control.Save(writer);

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        internal override void FreeNotify(TRibbonObject obj)
        {
            base.FreeNotify(obj);
            if (obj == _customizeCommandRef)
                _customizeCommandRef = null;
        }

        #endregion Internal Declarations

        //destructor
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (null != _customizeCommandRef)
                    _customizeCommandRef.RemoveFreeNotification(this);
                _controls?.Dispose();
            }
            base.Dispose(disposing);
        }

        public override RibbonObjectType ObjectType() { return RibbonObjectType.QuickAccessToolbar; }

        public override bool SupportApplicationModes() { return false; }

        public override TRibbonObject AddNew(RibbonObjectType objType)
        {
            TRibbonObject result;
            if (objType == RibbonObjectType.QatButton || objType == RibbonObjectType.QatCheckBox
                || objType == RibbonObjectType.QatToggleButton)
            {
                result = Owner.CreateObject(objType, this);
                _controls.Add(result as TRibbonQatControl);
            }
            else
                result = base.AddNew(objType);
            return result;
        }

        public override bool Remove(TRibbonObject obj)
        {
            bool result;
            if (obj is TRibbonQatControl)
                result = _controls.Remove((TRibbonQatControl)(obj));
            else
                result = base.Remove(obj);
            return result;
        }

        public override bool CanReorder()
        {
            return false;
        }

        public override bool Reorder(TRibbonObject child, int direction)
        {
            bool result;
            if (child is TRibbonQatControl)
                result = _controls.Reorder(child, direction);
            else
                result = base.Reorder(child, direction);
            return result;
        }

        public TRibbonCommand CustomizeCommandRef
        {
            get { return _customizeCommandRef; }
            set { SetCustomizeCommandRef(value); }
        }
        public TRibbonList<TRibbonQatControl> Controls { get { return _controls; } }
    }

    class TRibbonScale : TRibbonObject
    {
        static readonly string[] SIZES = { ES_POPUP, ES_SMALL, ES_MEDIUM, ES_LARGE };
        #region Internal Declarations

        private TRibbonCommand _groupRef;
        private RibbonGroupLayout _size;
        private void SetGroupRef(TRibbonCommand value)
        {
            if (value != _groupRef)
            {
                if (null != _groupRef)
                    _groupRef.RemoveFreeNotification(this);

                _groupRef = value;

                if (null != _groupRef)
                    _groupRef.FreeNotification(this);
            }
        }

        public TRibbonScale(TRibbonDocument owner) : base(owner)
        {
            _size = RibbonGroupLayout.Small;
        }

        public TRibbonScale(TRibbonDocument owner, XElement E) : base(owner)
        {
            TRibbonCommandName name = StringToCommandName(E.Attribute(AN_GROUP)?.Value);
            _groupRef = owner.FindCommand(name);
            if (null != _groupRef)
                _groupRef.FreeNotification(this);
            string s = E.Attribute(AN_SIZE)?.Value;
            if (s == ES_POPUP)
                _size = RibbonGroupLayout.Popup;
            else if ((s == ES_SMALL) || (string.IsNullOrEmpty(s)))
                _size = RibbonGroupLayout.Small;
            else if (s == ES_MEDIUM)
                _size = RibbonGroupLayout.Medium;
            else if (s == ES_LARGE)
                _size = RibbonGroupLayout.Large;
            else
                Error(E, RS_INVALID_SIZE);
        }

        internal void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_SCALE);

            if (null != _groupRef)
                _groupRef.SaveRef(writer, AN_GROUP);

            writer.WriteAttributeString(AN_SIZE, SIZES[(int)_size]);

            writer.WriteEndElement();
        }

        internal override void FreeNotify(TRibbonObject obj)
        {
            base.FreeNotify(obj);
            if (obj == _groupRef)
                _groupRef = null;
        }

        #endregion Internal Declarations

        //destructor
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (null != _groupRef)
                    _groupRef.RemoveFreeNotification(this);
            }
            base.Dispose(disposing);
        }

        public override RibbonObjectType ObjectType() { return RibbonObjectType.Scale; }

        public override string DisplayName()
        {
            string result;
            if (null != _groupRef)
            {
                result = _groupRef.LabelTitle.Content;
                if (string.IsNullOrEmpty(result))
                {
                    result = _groupRef.Name;
                    if (string.IsNullOrEmpty(result))
                    {
                        result = _groupRef.Symbol;
                        if (string.IsNullOrEmpty(result))
                            result = (_groupRef.Id.ToString());
                    }
                }
            }
            else
                result = "(scale)";

            result = result + " (" + SIZES[(int)_size] + ")";
            return result;
        }

        public override bool CanReorder()
        {
            return true;
        }

        public TRibbonCommand GroupRef { get { return _groupRef; } set { SetGroupRef(value); } }
        public RibbonGroupLayout Size { get { return _size; } set { _size = value; } }
    }

    class TRibbonScalingPolicy : TRibbonObject
    {
        #region Internal Declarations

        private TRibbonList<TRibbonScale> _idealSizes;
        private TRibbonList<TRibbonScale> _scales;

        public TRibbonScalingPolicy(TRibbonDocument owner) : base(owner)
        {
            _idealSizes = new TRibbonList<TRibbonScale>(owner, true);
            _scales = new TRibbonList<TRibbonScale>(owner, true);
        }

        public TRibbonScalingPolicy(TRibbonDocument owner, XElement E) : base(owner)
        {
            _idealSizes = new TRibbonList<TRibbonScale>(owner, true);
            _scales = new TRibbonList<TRibbonScale>(owner, true);
            bool hasIdealSizes = false;
            foreach (XElement C in E.Elements())
            {
                if (C.Name.LocalName == EN_SCALING_POLICY_IDEAL_SIZES)
                {
                    if (hasIdealSizes)
                        Error(C, RS_MULTIPLE_ELEMENTS, E.Name.LocalName, C.Name.LocalName);
                    hasIdealSizes = true;
                    foreach (XElement GC in C.Elements())
                    {
                        if (GC.Name.LocalName != EN_SCALE)
                            Error(GC, RS_UNSUPPORTED_CHILD_ELEMENT, GC.Name.LocalName, C.Name.LocalName);
                        _idealSizes.Add(new TRibbonScale(owner, GC));
                    }
                }
                else if (C.Name.LocalName == EN_SCALE)
                    _scales.Add(new TRibbonScale(owner, C));
                else
                    Error(C, RS_UNSUPPORTED_CHILD_ELEMENT, C.Name.LocalName, E.Name.LocalName);
            }
        }

        internal void Save(XmlWriter writer)
        {
            if ((_idealSizes.Count == 0) && (_scales.Count == 0))
                return;

            writer.WriteStartElement(EN_TAB_SCALING_POLICY);
            writer.WriteStartElement(EN_SCALING_POLICY);

            if (_idealSizes.Count > 0)
            {
                writer.WriteStartElement(EN_SCALING_POLICY_IDEAL_SIZES);
                foreach (TRibbonScale scale in _idealSizes)
                    scale.Save(writer);

                writer.WriteEndElement();
            }

            foreach (TRibbonScale scale in _scales)
                scale.Save(writer);

            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        //destructor
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _idealSizes?.Dispose();
                _scales?.Dispose();
            }
            base.Dispose(disposing);
        }

        public override RibbonObjectType ObjectType() { return RibbonObjectType.ScalingPolicy; }

        public override string DisplayName()
        {
            return RS_SCALING_POLICY;
        }

        public override TRibbonObject AddNew(RibbonObjectType objType)
        {
            TRibbonObject result;
            if (objType == RibbonObjectType.Scale)
            {
                result = new TRibbonScale(Owner);
                _scales.Add(result as TRibbonScale);
            }
            else
                result = base.AddNew(objType);
            return result;
        }

        public override bool Remove(TRibbonObject obj)
        {
            bool result;
            if (obj is TRibbonScale)
                result = _scales.Remove((TRibbonScale)(obj)) || _idealSizes.Remove((TRibbonScale)(obj));
            else
                result = base.Remove(obj);
            return result;
        }

        public TRibbonScale AddIdealSize()
        {
            TRibbonScale result = new TRibbonScale(Owner);
            _idealSizes.Add(result);
            return result;
        }

        public override bool Reorder(TRibbonObject child, int direction)
        {
            bool result;
            if (child is TRibbonScale)
                result = _idealSizes.Reorder(child, direction) || _scales.Reorder(child, direction);
            else
                result = base.Reorder(child, direction);
            return result;
        }

        public TRibbonList<TRibbonScale> IdealSizes { get { return _idealSizes; } }
        public TRibbonList<TRibbonScale> Scales { get { return _scales; } }
    }

    class TRibbonControlNameMap : TRibbonObject
    {
        #region Internal Declarations

        private List<string> _controlNameDefinitions;

        public TRibbonControlNameMap(TRibbonDocument owner) : base(owner)
        {
            _controlNameDefinitions = new List<string>();
        }

        public TRibbonControlNameMap(TRibbonDocument owner, XElement E) : base(owner)
        {
            _controlNameDefinitions = new List<string>();
            foreach (XElement C in E.Elements())
            {
                if (C.Name.LocalName == EN_CONTROL_NAME_DEFINITION)
                {
                    string s = C.Attribute(AN_NAME)?.Value;
                    if (IsValidCommandNameString(s))
                        _controlNameDefinitions.Add(s);
                    else
                        Error(C, RS_INVALID_COMMAND_NAME, s);
                }
                else
                    Error(C, RS_UNSUPPORTED_CHILD_ELEMENT, C.Name.LocalName, E.Name.LocalName);
            }
        }

        internal void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_CONTROL_NAME_MAP);

            foreach (string s in _controlNameDefinitions)
            {
                writer.WriteStartElement(EN_CONTROL_NAME_DEFINITION);
                writer.WriteAttributeString(AN_NAME, s);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        //destructor
        protected override void Dispose(bool disposing)
        {
            //_controlNameDefinitions?.Dispose();
            base.Dispose(disposing);
        }

        public override RibbonObjectType ObjectType() { return RibbonObjectType.ControlNameMap; }

        public void Clear()
        {
            _controlNameDefinitions.Clear();
        }

        public void Add(string name)
        {
            _controlNameDefinitions.Add(name);
        }

        public List<string> ControlNameDefinitions { get { return _controlNameDefinitions; } }
    }

    abstract class TRibbonGroupSizeDefinitionElement : TRibbonObject
    {
        #region Internal Declarations

        private TRibbonSizeDefinition _ownerDefinition;

        protected TRibbonGroupSizeDefinitionElement(TRibbonDocument owner,
            TRibbonSizeDefinition ownerDefinition) : base(owner)
        {
            _ownerDefinition = ownerDefinition;
        }

        internal abstract void Save(XmlWriter writer);

        #endregion Internal Declarations

        public override bool CanReorder()
        {
            return true;
        }

        public TRibbonSizeDefinition OwnerDefinition { get { return _ownerDefinition; } }
    }

    class TRibbonControlSizeDefinition : TRibbonGroupSizeDefinitionElement
    {
        static readonly string[] IMAGE_SIZES = { ES_LARGE, ES_SMALL };

        #region Internal Declarations

        private RibbonImageSize _imageSize;
        private bool _isLabelVisible;
        private bool _isImageVisible;
        private bool _isPopup;
        private string _controlName;

        public TRibbonControlSizeDefinition(TRibbonDocument owner,
           TRibbonSizeDefinition ownerDefinition) : base(owner, ownerDefinition)
        {
            _imageSize = RibbonImageSize.Small;
            _isLabelVisible = true;
            _isImageVisible = true;
        }

        public TRibbonControlSizeDefinition(TRibbonDocument owner,
           TRibbonSizeDefinition ownerDefinition, XElement E) : base(owner, ownerDefinition)
        {
            string s = E.Attribute(AN_IMAGE_SIZE)?.Value;
            if ((string.IsNullOrEmpty(s)) || (s == ES_SMALL))
                _imageSize = RibbonImageSize.Small;
            else if (s == ES_LARGE)
                _imageSize = RibbonImageSize.Large;
            else
                Error(E, RS_INVALID_IMAGE_SIZE);

            _isLabelVisible = AttributeAsBooleanDef(E, AN_IS_LABEL_VISIBLE, true);
            _isImageVisible = AttributeAsBooleanDef(E, AN_IS_IMAGE_VISIBLE, true);
            _isPopup = AttributeAsBooleanDef(E, AN_IS_POPUP, false);
            _controlName = E.Attribute(AN_CONTROL_NAME)?.Value;
            if (!IsValidCommandNameString(_controlName))
                Error(E, RS_INVALID_COMMAND_NAME, _controlName);
        }

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_CONTROL_SIZE_DEFINITION);

            if (_imageSize != RibbonImageSize.Small)
                writer.WriteAttributeString(AN_IMAGE_SIZE, IMAGE_SIZES[(int)_imageSize]);

            if (!_isLabelVisible)
                writer.WriteAttributeString(AN_IS_LABEL_VISIBLE, XmlConvert.ToString(_isLabelVisible));

            if (!_isImageVisible)
                writer.WriteAttributeString(AN_IS_IMAGE_VISIBLE, XmlConvert.ToString(_isImageVisible));

            if (_isPopup)
                writer.WriteAttributeString(AN_IS_POPUP, XmlConvert.ToString(_isPopup));

            if (!string.IsNullOrEmpty(_controlName))
                writer.WriteAttributeString(AN_CONTROL_NAME, _controlName);

            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        public override RibbonObjectType ObjectType() { return RibbonObjectType.ControlSizeDefinition; }

        public override string DisplayName()
        {
            string result = RS_CONTROL;
            if (!string.IsNullOrEmpty(_controlName))
                result = result + " (" + _controlName + ")";
            return result;
        }

        public RibbonImageSize ImageSize { get { return _imageSize; } set { _imageSize = value; } }
        public bool IsLabelVisible { get { return _isLabelVisible; } set { _isLabelVisible = value; } }
        public bool IsImageVisible { get { return _isImageVisible; } set { _isImageVisible = value; } }
        public bool IsPopup { get { return _isPopup; } set { _isPopup = value; } }
        public string ControlName { get { return _controlName; } set { _controlName = value; } }
    }

    class TRibbonRow : TRibbonGroupSizeDefinitionElement
    {
        #region Internal Declarations

        TRibbonList<TRibbonGroupSizeDefinitionElement> _elements;

        public TRibbonRow(TRibbonDocument owner,
            TRibbonSizeDefinition ownerDefinition) : base(owner, ownerDefinition)
        {
            _elements = new TRibbonList<TRibbonGroupSizeDefinitionElement>(owner, true);
        }

        public TRibbonRow(TRibbonDocument owner,
           TRibbonSizeDefinition ownerDefinition, XElement E) : base(owner, ownerDefinition)
        {
            _elements = new TRibbonList<TRibbonGroupSizeDefinitionElement>(owner, true);

            foreach (XElement C in E.Elements())
            {
                if (C.Name.LocalName == EN_CONTROL_SIZE_DEFINITION)
                    _elements.Add(new TRibbonControlSizeDefinition(owner, ownerDefinition, C));
                else if (C.Name.LocalName == EN_CONTROL_GROUP)
                    _elements.Add(new TRibbonControlSizeGroup(owner, ownerDefinition, C));
                else
                    Error(C, RS_UNSUPPORTED_CHILD_ELEMENT, C.Name.LocalName, E.Name.LocalName);
            }
        }

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_ROW);

            foreach (TRibbonGroupSizeDefinitionElement element in _elements)
                element.Save(writer);

            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        //destructor
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _elements?.Dispose();
            }
            base.Dispose(disposing);
        }

        public override RibbonObjectType ObjectType() { return RibbonObjectType.Row; }

        public override string DisplayName()
        {
            return RS_ROW;
        }

        public override TRibbonObject AddNew(RibbonObjectType objType)
        {
            TRibbonObject result;
            if (objType == RibbonObjectType.ControlSizeDefinition || objType == RibbonObjectType.ControlSizeGroup)
            {
                switch (objType)
                {
                    case RibbonObjectType.ControlSizeDefinition:
                        result = new TRibbonControlSizeDefinition(Owner, OwnerDefinition);
                        break;
                    case RibbonObjectType.ControlSizeGroup:
                        result = new TRibbonControlSizeGroup(Owner, OwnerDefinition);
                        break;
                    default:
                        {
                            Debug.Assert(false);
                            result = null;
                            return result;
                        }
                }
                _elements.Add((TRibbonGroupSizeDefinitionElement)(result));
            }
            else
                result = base.AddNew(objType);
            return result;
        }

        public override bool Remove(TRibbonObject obj)
        {
            bool result;
            if (obj is TRibbonGroupSizeDefinitionElement)
                result = _elements.Remove((TRibbonGroupSizeDefinitionElement)(obj));
            else
                result = base.Remove(obj);
            return result;
        }

        public override bool Reorder(TRibbonObject child, int direction)
        {
            bool result;
            if (child is TRibbonGroupSizeDefinitionElement)
                result = _elements.Reorder(child, direction);
            else
                result = base.Reorder(child, direction);
            return result;
        }

        /* List of elements of type:
         -TRibbonControlSizeDefinition
         -TRibbonControlSizeGroup  */
        public TRibbonList<TRibbonGroupSizeDefinitionElement> Elements { get { return _elements; } }
    }

    class TRibbonControlSizeGroup : TRibbonGroupSizeDefinitionElement
    {
        #region Internal Declarations

        TRibbonList<TRibbonControlSizeDefinition> _controlSizeDefinitions;

        public TRibbonControlSizeGroup(TRibbonDocument owner,
            TRibbonSizeDefinition ownerDefinition) : base(owner, ownerDefinition)
        {
            _controlSizeDefinitions = new TRibbonList<TRibbonControlSizeDefinition>(owner, true);
        }

        public TRibbonControlSizeGroup(TRibbonDocument owner,
           TRibbonSizeDefinition ownerDefinition, XElement E) : base(owner, ownerDefinition)
        {
            _controlSizeDefinitions = new TRibbonList<TRibbonControlSizeDefinition>(owner, true);

            foreach (XElement C in E.Elements())
            {
                if (C.Name.LocalName == EN_CONTROL_SIZE_DEFINITION)
                    _controlSizeDefinitions.Add(new TRibbonControlSizeDefinition(owner, ownerDefinition, C));
                else
                    Error(C, RS_UNSUPPORTED_CHILD_ELEMENT, C.Name.LocalName, E.Name.LocalName);
            }
        }

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_CONTROL_GROUP);

            foreach (TRibbonControlSizeDefinition sizeDef in _controlSizeDefinitions)
                sizeDef.Save(writer);

            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        //destructor
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _controlSizeDefinitions?.Dispose();
            }
            base.Dispose(disposing);
        }

        public override RibbonObjectType ObjectType() { return RibbonObjectType.ControlSizeGroup; }

        public override string DisplayName()
        {
            return RS_GROUP;
        }

        public override TRibbonObject AddNew(RibbonObjectType objType)
        {
            TRibbonObject result;
            if (objType == RibbonObjectType.ControlSizeDefinition)
            {
                result = new TRibbonControlSizeDefinition(Owner, OwnerDefinition);
                _controlSizeDefinitions.Add((TRibbonControlSizeDefinition)(result));
            }
            else
                result = base.AddNew(objType);
            return result;
        }

        public override bool Remove(TRibbonObject obj)
        {
            bool result;
            if (obj is TRibbonControlSizeDefinition)
                result = _controlSizeDefinitions.Remove((TRibbonControlSizeDefinition)(obj));
            else
                result = base.Remove(obj);
            return result;
        }

        public override bool Reorder(TRibbonObject child, int direction)
        {
            bool result;
            if (child is TRibbonControlSizeDefinition)
                result = _controlSizeDefinitions.Reorder(child, direction);
            else
                result = base.Reorder(child, direction);
            return result;
        }

        public TRibbonList<TRibbonControlSizeDefinition> ControlSizeDefinitions
        {
            get { return _controlSizeDefinitions; }
        }
    }

    class TRibbonColumnBreak : TRibbonGroupSizeDefinitionElement
    {
        #region Internal Declarations

        private bool _showSeparator;

        public TRibbonColumnBreak(TRibbonDocument owner,
            TRibbonSizeDefinition ownerDefinition) : base(owner, ownerDefinition)
        {
            _showSeparator = true;
        }

        public TRibbonColumnBreak(TRibbonDocument owner,
            TRibbonSizeDefinition ownerDefinition, XElement E) : base(owner, ownerDefinition)
        {
            _showSeparator = AttributeAsBooleanDef(E, AN_SHOW_SEPARATOR, true);
        }

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_COLUMN_BREAK);
            if (!_showSeparator)
                writer.WriteAttributeString(AN_SHOW_SEPARATOR, XmlConvert.ToString(_showSeparator));
            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        public override RibbonObjectType ObjectType() { return RibbonObjectType.ColumnBreak; }

        public override string DisplayName()
        {
            return RS_COLUMN_BREAK;
        }

        public bool ShowSeparator { get { return _showSeparator; } set { _showSeparator = value; } }
    }

    class TRibbonGroupSizeDefinition : TRibbonObject
    {
        static readonly string[] SIZES = { ES_LARGE, ES_MEDIUM, ES_SMALL };

        #region Internal Declarations

        private TRibbonSizeDefinition _ownerDefinition;
        private RibbonGroupSizeType _size;
        private TRibbonList<TRibbonGroupSizeDefinitionElement> _elements;

        public TRibbonGroupSizeDefinition(TRibbonDocument owner,
           TRibbonSizeDefinition ownerDefinition) : base(owner)
        {
            _ownerDefinition = ownerDefinition;
            _elements = new TRibbonList<TRibbonGroupSizeDefinitionElement>(owner, true);
            _size = RibbonGroupSizeType.Large;
        }

        public TRibbonGroupSizeDefinition(TRibbonDocument owner,
            TRibbonSizeDefinition ownerDefinition, XElement E) : base(owner)
        {
            _ownerDefinition = ownerDefinition;
            _elements = new TRibbonList<TRibbonGroupSizeDefinitionElement>(owner, true);
            string s = E.Attribute(AN_SIZE)?.Value;
            if ((string.IsNullOrEmpty(s)) || (s == ES_LARGE))
                _size = RibbonGroupSizeType.Large;
            else if (s == ES_MEDIUM)
                _size = RibbonGroupSizeType.Medium;
            else if (s == ES_SMALL)
                _size = RibbonGroupSizeType.Small;
            else
                Error(E, RS_INVALID_SIZE);

            foreach (XElement C in E.Elements())
            {
                if (C.Name.LocalName == EN_CONTROL_SIZE_DEFINITION)
                    _elements.Add(new TRibbonControlSizeDefinition(owner, ownerDefinition, C));
                else if (C.Name.LocalName == EN_CONTROL_GROUP)
                    _elements.Add(new TRibbonControlSizeGroup(owner, ownerDefinition, C));
                else if (C.Name.LocalName == EN_COLUMN_BREAK)
                    _elements.Add(new TRibbonColumnBreak(owner, ownerDefinition, C));
                else if (C.Name.LocalName == EN_ROW)
                    _elements.Add(new TRibbonRow(owner, ownerDefinition, C));
                else
                    Error(C, RS_UNSUPPORTED_CHILD_ELEMENT, C.Name.LocalName, E.Name.LocalName);
            }
        }

        internal void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_GROUP_SIZE_DEFINITION);

            writer.WriteAttributeString(AN_SIZE, SIZES[(int)_size]);

            foreach (TRibbonGroupSizeDefinitionElement element in _elements)
                element.Save(writer);

            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        //destructor
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _elements?.Dispose();
            }
            base.Dispose(disposing);
        }

        public override RibbonObjectType ObjectType() { return RibbonObjectType.GroupSizeDefinition; }

        public override string DisplayName()
        {
            return RS_GROUP + " (" + SIZES[(int)_size] + ")";
        }

        public override TRibbonObject AddNew(RibbonObjectType objType)
        {
            TRibbonObject result = null;
            if (objType == RibbonObjectType.ControlSizeDefinition || objType == RibbonObjectType.ControlSizeGroup
                || objType == RibbonObjectType.ColumnBreak || objType == RibbonObjectType.Row)
            {
                switch (objType)
                {
                    case RibbonObjectType.ControlSizeDefinition:
                        result = new TRibbonControlSizeDefinition(Owner, _ownerDefinition);
                        break;
                    case RibbonObjectType.ControlSizeGroup:
                        result = new TRibbonControlSizeGroup(Owner, _ownerDefinition);
                        break;
                    case RibbonObjectType.ColumnBreak:
                        result = new TRibbonColumnBreak(Owner, _ownerDefinition);
                        break;
                    case RibbonObjectType.Row:
                        result = new TRibbonRow(Owner, _ownerDefinition);
                        break;
                    default:
                        {
                            Debug.Assert(false);
                            result = null;
                            return result;
                        }
                }
                _elements.Add((TRibbonGroupSizeDefinitionElement)(result));
            }
            else
                result = base.AddNew(objType);
            return result;
        }

        public override bool Remove(TRibbonObject obj)
        {
            bool result;
            if (obj is TRibbonGroupSizeDefinitionElement)
                result = _elements.Remove((TRibbonGroupSizeDefinitionElement)(obj));
            else
                result = base.Remove(obj);
            return result;
        }

        public override bool CanReorder()
        {
            return true;
        }

        public override bool Reorder(TRibbonObject child, int direction)
        {
            bool result;
            if (child is TRibbonGroupSizeDefinitionElement)
                result = _elements.Reorder(child, direction);
            else
                result = base.Reorder(child, direction);
            return result;
        }

        public RibbonGroupSizeType Size { get { return _size; } set { _size = value; } }

        /* List of elements of type:
         -TRibbonControlSizeDefinition
         -TRibbonControlSizeGroup
         -TRibbonColumnBreak
         -TRibbonRow  */
        public TRibbonList<TRibbonGroupSizeDefinitionElement> Elements { get { return _elements; } }
    }

    class TRibbonSizeDefinition : TRibbonObject
    {
        #region Internal Declarations

        private TRibbonControlNameMap _controlNameMap;
        private TRibbonList<TRibbonGroupSizeDefinition> _groupSizeDefinitions;

        public TRibbonSizeDefinition(TRibbonDocument owner) : base(owner)
        {
            _groupSizeDefinitions = new TRibbonList<TRibbonGroupSizeDefinition>(owner, true);
            _groupSizeDefinitions.Add(new TRibbonGroupSizeDefinition(owner, this));
            _controlNameMap = new TRibbonControlNameMap(owner);
        }

        public TRibbonSizeDefinition(TRibbonDocument owner, XElement E) : base(owner)
        {
            _groupSizeDefinitions = new TRibbonList<TRibbonGroupSizeDefinition>(owner, true);
            foreach (XElement C in E.Elements())
            {
                if (C.Name.LocalName == EN_CONTROL_NAME_MAP)
                {
                    if (null != _controlNameMap)
                        Error(C, RS_MULTIPLE_ELEMENTS, E.Name.LocalName, C.Name.LocalName);
                    _controlNameMap = new TRibbonControlNameMap(owner, C);
                }
                else if (C.Name.LocalName == EN_GROUP_SIZE_DEFINITION)
                    _groupSizeDefinitions.Add(new TRibbonGroupSizeDefinition(owner, this, C));
                else
                    Error(C, RS_UNSUPPORTED_CHILD_ELEMENT, C.Name.LocalName, E.Name.LocalName);
            }

            if ((_groupSizeDefinitions.Count < 1) || (_groupSizeDefinitions.Count > 3))
                Error(E, RS_INVALID_GROUP_SIZE_DEFINITIONS);

            if (_controlNameMap == null)
                _controlNameMap = new TRibbonControlNameMap(owner);
        }

        internal void Save(XmlWriter writer)
        {
            writer.WriteStartElement(AN_SIZE_DEFINITION);

            SaveAttributes(writer);

            if (null != _controlNameMap)
                _controlNameMap.Save(writer);

            foreach (TRibbonGroupSizeDefinition group in _groupSizeDefinitions)
                group.Save(writer);

            writer.WriteEndElement();
        }

        protected virtual void SaveAttributes(XmlWriter writer)
        {
            // No default implementation
        }

        #endregion Internal Declarations

        //destructor
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _groupSizeDefinitions?.Dispose();
                _controlNameMap?.Dispose();
            }
            base.Dispose(disposing);
        }

        public override RibbonObjectType ObjectType() { return RibbonObjectType.SizeDefinition; }

        public override string DisplayName()
        {
            return RS_SIZE_DEF;
        }

        public override TRibbonObject AddNew(RibbonObjectType objType)
        {
            TRibbonObject result;
            if (objType == RibbonObjectType.GroupSizeDefinition)
            {
                if (_groupSizeDefinitions.Count >= 3)
                    Error(null, RS_MAX_GROUP_SIZE_DEF);
                result = new TRibbonGroupSizeDefinition(Owner, this);
                _groupSizeDefinitions.Add((TRibbonGroupSizeDefinition)(result));
            }
            else
                result = base.AddNew(objType);
            return result;
        }

        public override bool Remove(TRibbonObject obj)
        {
            bool result;
            if (obj is TRibbonGroupSizeDefinition)
            {
                if (_groupSizeDefinitions.Count <= 1)
                    Error(null, RS_MIN_GROUP_SIZE_DEF);
                result = _groupSizeDefinitions.Remove((TRibbonGroupSizeDefinition)(obj));
            }
            else
                result = base.Remove(obj);
            return result;
        }

        public override bool CanReorder()
        {
            return true;
        }

        public override bool Reorder(TRibbonObject child, int direction)
        {
            bool result;
            if (child is TRibbonGroupSizeDefinition)
                result = _groupSizeDefinitions.Reorder(child, direction);
            else
                result = base.Reorder(child, direction);
            return result;
        }

        public TRibbonControlNameMap ControlNameMap { get { return _controlNameMap; } }
        public TRibbonList<TRibbonGroupSizeDefinition> GroupSizeDefinitions { get { return _groupSizeDefinitions; } }
    }

    class TRibbonRibbonSizeDefinition : TRibbonSizeDefinition
    {
        #region Internal Declarations

        private string _name;

        public TRibbonRibbonSizeDefinition(TRibbonDocument owner) : base(owner) { }

        public TRibbonRibbonSizeDefinition(TRibbonDocument owner, XElement E) : base(owner, E)
        {
            _name = E.Attribute(AN_NAME)?.Value;
            if (!IsValidCommandNameString(_name))
                Error(E, RS_INVALID_COMMAND_NAME, _name);
        }

        protected override void SaveAttributes(XmlWriter writer)
        {
            base.SaveAttributes(writer);
            writer.WriteAttributeString(AN_NAME, _name);
        }

        #endregion Internal Declarations

        public override RibbonObjectType ObjectType() { return RibbonObjectType.RibbonSizeDefinition; }

        public override string DisplayName()
        {
            string result = _name;
            if (string.IsNullOrEmpty(result))
                result = base.DisplayName();
            return result;
        }

        public string Name { get { return _name; } set { _name = value; } }
    }

    class TRibbonGroup : TRibbonControl
    {
        #region Internal Declarations

        private RibbonBasicSizeDefinition _basicSizeDefinition;
        private string _customSizeDefinition;
        private TRibbonSizeDefinition _sizeDefinition;
        private TRibbonList<TRibbonControl> _controls;
        private void SetBasicSizeDefinition(RibbonBasicSizeDefinition value)
        {
            if (value != _basicSizeDefinition)
            {
                _basicSizeDefinition = value;
                if (value != RibbonBasicSizeDefinition.Custom)
                    _customSizeDefinition = string.Empty;
            }
        }

        private void SetCustomSizeDefinition(string value)
        {
            if (value != _customSizeDefinition)
            {
                _customSizeDefinition = value;
                _basicSizeDefinition = RibbonBasicSizeDefinition.Custom;
            }
        }

        public TRibbonGroup(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent)
        {
            _controls = new TRibbonList<TRibbonControl>(owner, true);
        }

        public TRibbonGroup(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent)
        {
            _controls = new TRibbonList<TRibbonControl>(owner, true);
            string s = E.Attribute(AN_SIZE_DEFINITION)?.Value;
            Array sizeDefValues = Enum.GetValues(typeof(RibbonBasicSizeDefinition)); //@ ?
            foreach (RibbonBasicSizeDefinition sizeDef in sizeDefValues)
                if (s == ES_SIZE_DEFINITION[(int)sizeDef])
                {
                    _basicSizeDefinition = sizeDef;
                    break;
                }
            if (_basicSizeDefinition == RibbonBasicSizeDefinition.Custom)
                _customSizeDefinition = s;

            foreach (XElement C in E.Elements())
            {
                if (C.Name.LocalName == EN_SIZE_DEFINITION)
                {
                    if (null != _sizeDefinition)
                        Error(C, RS_MULTIPLE_ELEMENTS, E.Name.LocalName, C.Name.LocalName);
                    _sizeDefinition = new TRibbonSizeDefinition(owner, C);
                    _basicSizeDefinition = RibbonBasicSizeDefinition.Advanced;
                }
                else if (C.Name.LocalName == EN_CONTROL_GROUP)
                    _controls.Add(new TRibbonControlGroup(owner, C, this));
                else if (C.Name.LocalName == EN_TOGGLE_BUTTON)
                    _controls.Add(new TRibbonToggleButton(owner, C, this));
                else if (C.Name.LocalName == EN_CHECK_BOX)
                    _controls.Add(new TRibbonCheckBox(owner, C, this));
                else if (C.Name.LocalName == EN_BUTTON)
                    _controls.Add(new TRibbonButton(owner, C, this));
                else if (C.Name.LocalName == EN_SPLIT_BUTTON)
                    _controls.Add(new TRibbonSplitButton(owner, C, this));
                else if (C.Name.LocalName == EN_DROP_DOWN_BUTTON)
                    _controls.Add(new TRibbonDropDownButton(owner, C, this));
                else if (C.Name.LocalName == EN_DROP_DOWN_GALLERY)
                    _controls.Add(new TRibbonDropDownGallery(owner, C, this));
                else if (C.Name.LocalName == EN_SPLIT_BUTTON_GALLERY)
                    _controls.Add(new TRibbonSplitButtonGallery(owner, C, this));
                else if (C.Name.LocalName == EN_DROP_DOWN_COLOR_PICKER)
                    _controls.Add(new TRibbonDropDownColorPicker(owner, C, this));
                else if (C.Name.LocalName == EN_COMBO_BOX)
                    _controls.Add(new TRibbonComboBox(owner, C, this));
                else if (C.Name.LocalName == EN_SPINNER)
                    _controls.Add(new TRibbonSpinner(owner, C, this));
                else if (C.Name.LocalName == EN_IN_RIBBON_GALLERY)
                    _controls.Add(new TRibbonInRibbonGallery(owner, C, this));
                else if (C.Name.LocalName == EN_FONT_CONTROL)
                    _controls.Add(new TRibbonFontControl(owner, C, this));
                else
                    Error(C, RS_UNSUPPORTED_CHILD_ELEMENT, C.Name.LocalName, E.Name.LocalName);
            }
        }

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_GROUP);
            base.Save(writer);

            if (_basicSizeDefinition > RibbonBasicSizeDefinition.Advanced)
                writer.WriteAttributeString(AN_SIZE_DEFINITION, ES_SIZE_DEFINITION[(int)_basicSizeDefinition]);
            else if ((_basicSizeDefinition == RibbonBasicSizeDefinition.Custom) && (!string.IsNullOrEmpty(_customSizeDefinition)))
                writer.WriteAttributeString(AN_SIZE_DEFINITION, _customSizeDefinition);

            if (null != _sizeDefinition)
                _sizeDefinition.Save(writer);

            foreach (TRibbonControl control in _controls)
                control.Save(writer);

            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        //destructor
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _controls?.Dispose();
                _sizeDefinition?.Dispose();
            }
            base.Dispose(disposing);
        }

        public override RibbonObjectType ObjectType() { return RibbonObjectType.Group; }

        public override bool SupportApplicationModes() { return true; }

        public override TRibbonObject AddNew(RibbonObjectType objType)
        {
            TRibbonObject result;
            if (objType == RibbonObjectType.ControlGroup || objType == RibbonObjectType.ToggleButton
                || objType == RibbonObjectType.CheckBox || objType == RibbonObjectType.Button
                || objType == RibbonObjectType.SplitButton || objType == RibbonObjectType.DropDownButton
                || objType == RibbonObjectType.DropDownGallery || objType == RibbonObjectType.SplitButtonGallery
                || objType == RibbonObjectType.DropDownColorPicker || objType == RibbonObjectType.ComboBox
                || objType == RibbonObjectType.Spinner || objType == RibbonObjectType.InRibbonGallery
                || objType == RibbonObjectType.FontControl)
            {
                result = Owner.CreateObject(objType, this);
                _controls.Add(result as TRibbonControl);
            }
            else
                result = base.AddNew(objType);
            return result;
        }

        public override bool Remove(TRibbonObject obj)
        {
            bool result;
            if (obj is TRibbonControl)
                result = _controls.Remove((TRibbonControl)(obj));
            else
                result = base.Remove(obj);
            return result;
        }

        public override bool Reorder(TRibbonObject child, int direction)
        {
            bool result;
            if (child is TRibbonControl)
                result = _controls.Reorder(child, direction);
            else
                result = base.Reorder(child, direction);
            return result;
        }

        public void CreateAdvancedSizeDefinition()
        {
            if (_sizeDefinition != null)
            {
                _sizeDefinition.Dispose();
                _sizeDefinition = null;
            }
            _sizeDefinition = new TRibbonSizeDefinition(Owner);
        }

        [Obsolete]
        public void DeleteAdvancedSizeDefinition()
        {
            RemoveAdvancedSizeDefinition();
        }

        public void RemoveAdvancedSizeDefinition()
        {
            if (_sizeDefinition != null)
            {
                _sizeDefinition.Dispose();
                _sizeDefinition = null;
            }
        }

        public RibbonBasicSizeDefinition BasicSizeDefinition
        {
            get { return _basicSizeDefinition; }
            set { SetBasicSizeDefinition(value); }
        }
        public string CustomSizeDefinition
        {
            get { return _customSizeDefinition; }
            set { SetCustomSizeDefinition(value); }
        }
        public TRibbonSizeDefinition SizeDefinition { get { return _sizeDefinition; } }
        public TRibbonList<TRibbonControl> Controls { get { return _controls; } }
    }

    class TRibbonTab : TRibbonControl
    {
        #region Internal Declarations

        private TRibbonScalingPolicy _scalingPolicy;
        private TRibbonList<TRibbonGroup> _groups;

        public TRibbonTab(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent)
        {
            _groups = new TRibbonList<TRibbonGroup>(owner, true);
            _scalingPolicy = new TRibbonScalingPolicy(owner);
        }

        public TRibbonTab(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent)
        {
            _groups = new TRibbonList<TRibbonGroup>(owner, true);
            if (E.Name.LocalName != EN_TAB)
                Error(E, RS_ELEMENT_EXPECTED, EN_TAB, E.Name.LocalName);
            foreach (XElement C in E.Elements())
            {
                if (C.Name.LocalName == EN_TAB_SCALING_POLICY)
                {
                    if (C.Elements().Count() != 1)
                        Error(C, RS_SINGLE_ELEMENT, C.Name.LocalName, EN_SCALING_POLICY);
                    XElement GC = C.Elements().ElementAt(0);
                    if (GC.Name.LocalName != EN_SCALING_POLICY)
                        Error(GC, RS_ELEMENT_EXPECTED, EN_SCALING_POLICY, GC.Name.LocalName);
                    _scalingPolicy = new TRibbonScalingPolicy(owner, GC);
                }
                else if (C.Name.LocalName == EN_GROUP)
                    _groups.Add(new TRibbonGroup(owner, C, this));
                else
                    Error(C, RS_UNSUPPORTED_CHILD_ELEMENT, C.Name.LocalName, E.Name.LocalName);
            }
            if (_scalingPolicy == null)
                _scalingPolicy = new TRibbonScalingPolicy(owner);
        }

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_TAB);
            base.Save(writer);

            if (null != _scalingPolicy)
                _scalingPolicy.Save(writer);

            foreach (TRibbonGroup group in _groups)
                group.Save(writer);

            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        //destructor
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _groups?.Dispose();
                _scalingPolicy?.Dispose();
            }
            base.Dispose(disposing);
        }

        public override RibbonObjectType ObjectType() { return RibbonObjectType.Tab; }

        public override bool SupportApplicationModes() { return true; }

        public override TRibbonObject AddNew(RibbonObjectType objType)
        {
            TRibbonObject result;
            if (objType == RibbonObjectType.Group)
            {
                result = new TRibbonGroup(Owner, this);
                _groups.Add(result as TRibbonGroup);
            }
            else
                result = base.AddNew(objType);
            return result;
        }

        public override bool Remove(TRibbonObject obj)
        {
            bool result;
            if (obj is TRibbonGroup)
                result = _groups.Remove((TRibbonGroup)(obj));
            else
                result = base.Remove(obj);
            return result;
        }

        public override bool Reorder(TRibbonObject child, int direction)
        {
            bool result;
            if (child is TRibbonGroup)
                result = _groups.Reorder(child, direction);
            else
                result = base.Reorder(child, direction);
            return result;
        }

        public TRibbonScalingPolicy ScalingPolicy { get { return _scalingPolicy; } }
        public TRibbonList<TRibbonGroup> Groups { get { return _groups; } }
    }

    class TRibbonTabGroup : TRibbonControl
    {
        #region Internal Declarations

        private TRibbonList<TRibbonTab> _tabs;

        public TRibbonTabGroup(TRibbonDocument owner, TRibbonCommandRefObject parent) : base(owner, parent)
        {
            _tabs = new TRibbonList<TRibbonTab>(owner, true);
        }

        public TRibbonTabGroup(TRibbonDocument owner, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent)
        {
            _tabs = new TRibbonList<TRibbonTab>(owner, true);
            foreach (XElement C in E.Elements())
            {
                if (C.Name.LocalName != EN_TAB)
                    Error(C, RS_ELEMENT_EXPECTED, EN_TAB, C.Name.LocalName);
                _tabs.Add(new TRibbonTab(owner, C, this));
            }
        }

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_TAB_GROUP);
            base.Save(writer);
            foreach (TRibbonTab tab in _tabs)
                tab.Save(writer);

            writer.WriteEndElement();
        }

        #endregion Internal Declarations

        //destructor
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _tabs?.Dispose();
            }
            base.Dispose(disposing);
        }

        public override RibbonObjectType ObjectType() { return RibbonObjectType.TabGroup; }

        public override bool SupportApplicationModes() { return false; }

        public override TRibbonObject AddNew(RibbonObjectType objType)
        {
            TRibbonObject result;
            if (objType == RibbonObjectType.Tab)
            {
                result = new TRibbonTab(Owner, this);
                _tabs.Add((TRibbonTab)(result));
            }
            else
                result = base.AddNew(objType);
            return result;
        }

        public override bool Remove(TRibbonObject obj)
        {
            bool result;
            if (obj is TRibbonTab)
                result = _tabs.Remove((TRibbonTab)(obj));
            else
                result = base.Remove(obj);
            return result;
        }

        public override bool Reorder(TRibbonObject child, int direction)
        {
            bool result;
            if (child is TRibbonTab)
                result = _tabs.Reorder(child, direction);
            else
                result = base.Reorder(child, direction);
            return result;
        }

        public TRibbonList<TRibbonTab> Tabs { get { return _tabs; } }
    }

    class TRibbonViewRibbon : TRibbonView
    {
        #region Internal Declarations

        private string _name;
        private RibbonGroupSpacing _groupSpacing;
        private TRibbonApplicationMenu _applicationMenu;
        private TRibbonHelpButton _helpButton;
        private TRibbonQuickAccessToolbar _quickAccessToolbar;
        private TRibbonList<TRibbonTab> _tabs;
        private TRibbonList<TRibbonTabGroup> _contextualTabs;
        private TRibbonList<TRibbonRibbonSizeDefinition> _sizeDefinitions;

        internal override void Save(XmlWriter writer)
        {
            string[] GROUP_SPACINGS = { ES_SMALL, ES_MEDIUM, ES_LARGE };

            writer.WriteStartElement(EN_RIBBON);

            if (!string.IsNullOrEmpty(_name))
                writer.WriteAttributeString(AN_NAME, _name);
            if (_groupSpacing != RibbonGroupSpacing.Small)
                writer.WriteAttributeString(AN_GROUP_SPACING, GROUP_SPACINGS[(int)_groupSpacing]);

            if (_sizeDefinitions.Count > 0)
            {
                writer.WriteStartElement(EN_RIBBON_SIZE_DEFINITIONS);
                foreach (TRibbonRibbonSizeDefinition sizeDef in _sizeDefinitions)
                    sizeDef.Save(writer);

                writer.WriteEndElement();
            }

            if (null != _applicationMenu)
                _applicationMenu.Save(writer);

            if (null != _helpButton)
                _helpButton.Save(writer);

            if (null != _quickAccessToolbar)
                _quickAccessToolbar.Save(writer);

            if (_contextualTabs.Count > 0)
            {
                writer.WriteStartElement(EN_RIBBON_CONTEXTUAL_TABS);
                foreach (TRibbonTabGroup tabGroup in _contextualTabs)
                    tabGroup.Save(writer);

                writer.WriteEndElement();
            }

            if (_tabs.Count > 0)
            {
                writer.WriteStartElement(EN_RIBBON_TABS);
                foreach (TRibbonTab tab in _tabs)
                    tab.Save(writer);

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        public TRibbonViewRibbon(TRibbonDocument owner) : base(owner)
        {
            _tabs = new TRibbonList<TRibbonTab>(owner, true);
            _contextualTabs = new TRibbonList<TRibbonTabGroup>(owner, true);
            _sizeDefinitions = new TRibbonList<TRibbonRibbonSizeDefinition>(owner, true);
            _applicationMenu = new TRibbonApplicationMenu(owner, null);
            _quickAccessToolbar = new TRibbonQuickAccessToolbar(owner, null);
            _helpButton = new TRibbonHelpButton(owner, null);
            _groupSpacing = RibbonGroupSpacing.Small;
        }

        public TRibbonViewRibbon(TRibbonDocument owner, XElement E) : base(owner)
        {
            _tabs = new TRibbonList<TRibbonTab>(owner, true);
            _contextualTabs = new TRibbonList<TRibbonTabGroup>(owner, true);
            _sizeDefinitions = new TRibbonList<TRibbonRibbonSizeDefinition>(owner, true);
            _name = E.Attribute(AN_NAME)?.Value;
            string s = E.Attribute(AN_GROUP_SPACING)?.Value;
            if ((s == ES_SMALL) || (string.IsNullOrEmpty(s)))
                _groupSpacing = RibbonGroupSpacing.Small;
            else if (s == ES_MEDIUM)
                _groupSpacing = RibbonGroupSpacing.Medium;
            else if (s == ES_LARGE)
                _groupSpacing = RibbonGroupSpacing.Large;
            else
                Error(E, RS_INVALID_GROUP_SPACING);

            foreach (XElement C in E.Elements())
            {
                if (C.Name.LocalName == EN_RIBBON_SIZE_DEFINITIONS)
                {
                    foreach (XElement GC in C.Elements())
                    {
                        if (GC.Name.LocalName != EN_SIZE_DEFINITION)
                            Error(GC, RS_ELEMENT_EXPECTED, EN_SIZE_DEFINITION, GC.Name.LocalName);
                        _sizeDefinitions.Add(new TRibbonRibbonSizeDefinition(owner, GC));
                    }
                }
                else if (C.Name.LocalName == EN_RIBBON_APPLICATION_MENU)
                {
                    if ((null != _applicationMenu))
                        Error(C, RS_MULTIPLE_ELEMENTS, E.Name.LocalName, C.Name.LocalName);
                    //@@ changed
                    XElement C1;
                    if (C.Elements().Count() > 0)
                        C1 = (XElement)C.Elements().ElementAt(0);
                    else
                        C1 = C;
                    _applicationMenu = new TRibbonApplicationMenu(owner, C, C1, null);
                    //@@ changed
                }
                else if (C.Name.LocalName == EN_RIBBON_HELP_BUTTON)
                {
                    if (C.Elements().Count() > 1)
                        Error(C, RS_MULTIPLE_ELEMENTS, E.Name.LocalName, C.Name.LocalName);
                    if (C.Elements().Count() == 1)
                    {
                        XElement GC = C.Elements().ElementAt(0);
                        if (GC.Name.LocalName != EN_HELP_BUTTON)
                            Error(C, RS_ELEMENT_EXPECTED, EN_HELP_BUTTON, GC.Name.LocalName);
                        _helpButton = new TRibbonHelpButton(owner, GC, null);
                    }
                }
                else if (C.Name.LocalName == EN_RIBBON_TABS)
                {
                    if (_tabs.Count > 0)
                        Error(C, RS_MULTIPLE_ELEMENTS, E.Name.LocalName, C.Name.LocalName);
                    foreach (XElement GC in C.Elements())
                        _tabs.Add(new TRibbonTab(owner, GC, null));
                }
                else if (C.Name.LocalName == EN_RIBBON_CONTEXTUAL_TABS)
                {
                    if (C.Elements().Count() == 0)
                        Error(C, RS_REQUIRED_ELEMENT, C.Name.LocalName, EN_TAB_GROUP);
                    foreach (XElement GC in C.Elements())
                    {
                        if (GC.Name.LocalName != EN_TAB_GROUP)
                            Error(GC, RS_ELEMENT_EXPECTED, EN_TAB_GROUP, GC.Name.LocalName);
                        _contextualTabs.Add(new TRibbonTabGroup(owner, GC, null));
                    }
                }
                else if (C.Name.LocalName == EN_RIBBON_QUICK_ACCESS_TOOLBAR)
                {
                    if (null != _quickAccessToolbar)
                        Error(C, RS_MULTIPLE_ELEMENTS, E.Name.LocalName, C.Name.LocalName);
                    if (C.Elements().Count() != 1)
                        Error(C, RS_SINGLE_ELEMENT, C.Name.LocalName, EN_QUICK_ACCESS_TOOLBAR);
                    XElement GC = C.Elements().ElementAt(0);
                    if (GC.Name.LocalName != EN_QUICK_ACCESS_TOOLBAR)
                        Error(GC, RS_ELEMENT_EXPECTED, EN_QUICK_ACCESS_TOOLBAR, GC.Name.LocalName);
                    _quickAccessToolbar = new TRibbonQuickAccessToolbar(owner, GC, null);
                }
                else
                    Error(C, RS_UNSUPPORTED_CHILD_ELEMENT, C.Name.LocalName, E.Name.LocalName);
            }

            if (_applicationMenu == null)
                _applicationMenu = new TRibbonApplicationMenu(owner, null);
            if (_quickAccessToolbar == null)
                _quickAccessToolbar = new TRibbonQuickAccessToolbar(owner, null);
            if (_helpButton == null)
                _helpButton = new TRibbonHelpButton(owner, null);
        }

        #endregion Internal Declarations

        //destructor
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _tabs?.Dispose();
                _contextualTabs?.Dispose();
                _sizeDefinitions?.Dispose();
                _applicationMenu?.Dispose();
                _helpButton?.Dispose();
                _quickAccessToolbar?.Dispose();
            }
            base.Dispose(disposing);
        }

        public override RibbonObjectType ObjectType() { return RibbonObjectType.ViewRibbon; }

        public override string DisplayName()
        {
            string result;
            if (!string.IsNullOrEmpty(_name))
                result = _name;
            else
                result = base.DisplayName();
            return result;
        }

        public override TRibbonObject AddNew(RibbonObjectType objType)
        {
            TRibbonObject result;
            if (objType == RibbonObjectType.RibbonSizeDefinition)
            {
                result = Owner.CreateObject(objType, null);
                _sizeDefinitions.Add(result as TRibbonRibbonSizeDefinition);
            }
            else if (objType == RibbonObjectType.Tab)
            {
                result = new TRibbonTab(Owner, null);
                _tabs.Add((TRibbonTab)(result));
            }
            else if (objType == RibbonObjectType.TabGroup)
            {
                result = new TRibbonTabGroup(Owner, null);
                _contextualTabs.Add((TRibbonTabGroup)(result));
            }
            else
                result = base.AddNew(objType);
            return result;
        }

        public override bool Remove(TRibbonObject obj)
        {
            bool result;
            if (obj is TRibbonRibbonSizeDefinition)
                result = _sizeDefinitions.Remove((TRibbonRibbonSizeDefinition)(obj));
            else if (obj is TRibbonTab)
                result = _tabs.Remove((TRibbonTab)(obj));
            else if (obj is TRibbonTabGroup)
                result = _contextualTabs.Remove((TRibbonTabGroup)(obj));
            else
                result = base.Remove(obj);
            return result;
        }

        public override bool Reorder(TRibbonObject child, int direction)
        {
            bool result;
            if (child is TRibbonRibbonSizeDefinition)
                result = _sizeDefinitions.Reorder(child, direction);
            else if (child is TRibbonTabGroup)
                result = _contextualTabs.Reorder(child, direction);
            else if (child is TRibbonTab)
                result = _tabs.Reorder(child, direction);
            else
                result = base.Reorder(child, direction);
            return result;
        }

        public string Name { get { return _name; } set { _name = value; } }
        public RibbonGroupSpacing GroupSpacing { get { return _groupSpacing; } set { _groupSpacing = value; } }
        public TRibbonApplicationMenu ApplicationMenu { get { return _applicationMenu; } }
        public TRibbonHelpButton HelpButton { get { return _helpButton; } }
        public TRibbonQuickAccessToolbar QuickAccessToolbar { get { return _quickAccessToolbar; } }
        public TRibbonList<TRibbonTab> Tabs { get { return _tabs; } }
        public TRibbonList<TRibbonTabGroup> ContextualTabs { get { return _contextualTabs; } }
        public TRibbonList<TRibbonRibbonSizeDefinition> SizeDefinitions
        {
            get { return _sizeDefinitions; }
            set { _sizeDefinitions = value; }
        }
    }

    class TRibbonMiniToolbar : TRibbonObject
    {
        #region Internal Declarations

        private string _name;
        private TRibbonList<TRibbonMiniToolbarMenuGroup> _menuGroups;

        public TRibbonMiniToolbar(TRibbonDocument owner) : base(owner)
        {
            _menuGroups = new TRibbonList<TRibbonMiniToolbarMenuGroup>(owner, true);
            _menuGroups.Add(new TRibbonMiniToolbarMenuGroup(owner, null));
        }

        public TRibbonMiniToolbar(TRibbonDocument owner, XElement E) : base(owner)
        {
            _menuGroups = new TRibbonList<TRibbonMiniToolbarMenuGroup>(owner, true);
            _name = E.Attribute(AN_NAME)?.Value;
            if (E.Elements().Count() == 0)
                Error(E, RS_REQUIRED_ELEMENT, E.Name.LocalName, EN_MENU_GROUP);
            foreach (XElement C in E.Elements())
            {
                if (C.Name.LocalName != EN_MENU_GROUP)
                    Error(C, RS_ELEMENT_EXPECTED, EN_MENU_GROUP, C.Name.LocalName);
                _menuGroups.Add(new TRibbonMiniToolbarMenuGroup(owner, C, null));
            }
        }

        internal void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_MINI_TOOLBAR);

            if (!string.IsNullOrEmpty(_name))
                writer.WriteAttributeString(AN_NAME, _name);

            foreach (TRibbonMiniToolbarMenuGroup group in _menuGroups)
                group.Save(writer);

            writer.WriteEndElement();
        }

        internal void SaveRef(XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(_name))
                writer.WriteAttributeString(AN_MINI_TOOLBAR, _name);
        }

        #endregion Internal Declarations

        //destructor
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _menuGroups?.Dispose();
            }
            base.Dispose(disposing);
        }

        public override string DisplayName()
        {
            string result = _name;
            if (string.IsNullOrEmpty(result))
                result = RS_MINI_TOOLBAR;
            return result;
        }

        public override RibbonObjectType ObjectType() { return RibbonObjectType.MiniToolbar; }

        public override TRibbonObject AddNew(RibbonObjectType objType)
        {
            TRibbonObject result;
            if (objType == RibbonObjectType.MiniToolbarMenuGroup)
            {
                result = new TRibbonMiniToolbarMenuGroup(Owner, null);
                _menuGroups.Add((TRibbonMiniToolbarMenuGroup)(result));
            }
            else
                result = base.AddNew(objType);
            return result;
        }

        public override bool Remove(TRibbonObject obj)
        {
            bool result;
            if (obj is TRibbonMiniToolbarMenuGroup)
            {
                if (_menuGroups.Count <= 1)
                    Error(null, RS_MIN_MINI_TOOLBAR);
                result = _menuGroups.Remove((TRibbonMiniToolbarMenuGroup)(obj));
            }
            else
                result = base.Remove(obj);
            return result;
        }

        public override bool CanReorder()
        {
            return true;
        }

        public override bool Reorder(TRibbonObject child, int direction)
        {
            bool result;
            if (child is TRibbonMiniToolbarMenuGroup)
                result = _menuGroups.Reorder(child, direction);
            else
                result = base.Reorder(child, direction);
            return result;
        }

        public string Name { get { return _name; } set { _name = value; } }
        public TRibbonList<TRibbonMiniToolbarMenuGroup> MenuGroups { get { return _menuGroups; } }
    }

    class TRibbonContextMenu : TRibbonObject
    {
        #region Internal Declarations

        private string _name;
        private TRibbonList<TRibbonMenuGroup> _menuGroups;

        public TRibbonContextMenu(TRibbonDocument owner) : base(owner)
        {
            _menuGroups = new TRibbonList<TRibbonMenuGroup>(owner, true);
            _menuGroups.Add(new TRibbonMenuGroup(owner, null));
        }

        public TRibbonContextMenu(TRibbonDocument owner, XElement E) : base(owner)
        {
            _menuGroups = new TRibbonList<TRibbonMenuGroup>(owner, true);
            _name = E.Attribute(AN_NAME)?.Value;
            if (E.Elements().Count() == 0)
                Error(E, RS_REQUIRED_ELEMENT, E.Name.LocalName, EN_MENU_GROUP);
            foreach (XElement C in E.Elements())
            {
                if (C.Name.LocalName != EN_MENU_GROUP)
                    Error(C, RS_ELEMENT_EXPECTED, EN_MENU_GROUP, C.Name.LocalName);
                _menuGroups.Add(new TRibbonMenuGroup(owner, C, null));
            }
        }

        internal void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_CONTEXT_MENU);

            if (!string.IsNullOrEmpty(_name))
                writer.WriteAttributeString(AN_NAME, _name);

            foreach (TRibbonMenuGroup group in _menuGroups)
                group.Save(writer);

            writer.WriteEndElement();
        }

        internal void SaveRef(XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(_name))
                writer.WriteAttributeString(AN_CONTEXT_MENU, _name);
        }

        #endregion Internal Declarations

        //destructor
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _menuGroups?.Dispose();
            }
            base.Dispose(disposing);
        }

        public override string DisplayName()
        {
            string result = _name;
            if (string.IsNullOrEmpty(result))
                result = RS_CONTEXT_MENU;
            return result;
        }

        public override RibbonObjectType ObjectType() { return RibbonObjectType.ContextMenu; }

        public override TRibbonObject AddNew(RibbonObjectType objType)
        {
            TRibbonObject result;
            if (objType == RibbonObjectType.MenuGroup)
            {
                result = new TRibbonMenuGroup(Owner, null);
                _menuGroups.Add((TRibbonMenuGroup)(result));
            }
            else
                result = base.AddNew(objType);
            return result;
        }

        public override bool Remove(TRibbonObject obj)
        {
            bool result;
            if (obj is TRibbonMenuGroup)
            {
                if (_menuGroups.Count <= 1)
                    Error(null, RS_MIN_CONTEXT_MENU);
                result = _menuGroups.Remove((TRibbonMenuGroup)(obj));
            }
            else
                result = base.Remove(obj);
            return result;
        }

        public override bool CanReorder()
        {
            return true;
        }

        public override bool Reorder(TRibbonObject child, int direction)
        {
            bool result;
            if (child is TRibbonMenuGroup)
                result = _menuGroups.Reorder(child, direction);
            else
                result = base.Reorder(child, direction);
            return result;
        }

        public string Name { get { return _name; } set { _name = value; } }
        public TRibbonList<TRibbonMenuGroup> MenuGroups { get { return _menuGroups; } }
    }

    class TRibbonContextMap : TRibbonCommandRefObject
    {
        #region Internal Declarations

        private TRibbonViewContextPopup _contextPopup;
        private TRibbonContextMenu _contextMenuRef;
        private TRibbonMiniToolbar _miniToolbarRef;
        private string _contextMenuName;
        private string _miniToolbarName;

        public void SetContextMenuRef(TRibbonContextMenu value)
        {
            if (value != _contextMenuRef)
            {
                if (null != _contextMenuRef)
                    _contextMenuRef.RemoveFreeNotification(this);

                _contextMenuRef = value;

                if (null != _contextMenuRef)
                {
                    _contextMenuRef.FreeNotification(this);
                    _contextMenuName = _contextMenuRef.Name;
                }
                else
                    _contextMenuName = string.Empty;
            }
        }

        public void SetMiniToolbarRef(TRibbonMiniToolbar value)
        {
            if (value != _miniToolbarRef)
            {
                if (null != _miniToolbarRef)
                    _miniToolbarRef.RemoveFreeNotification(this);

                _miniToolbarRef = value;

                if (null != _miniToolbarRef)
                {
                    _miniToolbarRef.FreeNotification(this);
                    _miniToolbarName = _miniToolbarRef.Name;
                }
                else
                    _miniToolbarName = string.Empty;
            }
        }

        public TRibbonContextMap(TRibbonDocument owner,
            TRibbonViewContextPopup popup, TRibbonCommandRefObject parent) : base(owner, parent)
        {
            _contextPopup = popup;
        }

        public TRibbonContextMap(TRibbonDocument owner,
           TRibbonViewContextPopup popup, XElement E, TRibbonCommandRefObject parent) : base(owner, E, parent)
        {
            _contextPopup = popup;
            _contextMenuName = E.Attribute(AN_CONTEXT_MENU)?.Value;
            _miniToolbarName = E.Attribute(AN_MINI_TOOLBAR)?.Value;
        }

        public void FixupReferences()
        {
            _contextMenuRef = Owner.FindContextMenu(_contextMenuName);
            if (null != _contextMenuRef)
                _contextMenuRef.FreeNotification(this);
            _miniToolbarRef = Owner.FindMiniToolbar(_miniToolbarName);
            if (null != _miniToolbarRef)
                _miniToolbarRef.FreeNotification(this);
        }

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_CONTEXT_MAP);
            base.Save(writer);
            if (null != _contextMenuRef)
                _contextMenuRef.SaveRef(writer);
            if (null != _miniToolbarRef)
                _miniToolbarRef.SaveRef(writer);
            writer.WriteEndElement();
        }

        internal override void FreeNotify(TRibbonObject obj)
        {
            base.FreeNotify(obj);
            if (obj == _contextMenuRef)
                _contextMenuRef = null;
            else if (obj == _miniToolbarRef)
                _miniToolbarRef = null;
        }

        #endregion Internal Declarations

        //destructor
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (null != _contextMenuRef)
                    _contextMenuRef.RemoveFreeNotification(this);
                if (null != _miniToolbarRef)
                    _miniToolbarRef.RemoveFreeNotification(this);
            }
            base.Dispose(disposing);

        }

        public override string DisplayName()
        {
            string result;
            string Menu, Toolbar;
            if (null != _contextMenuRef)
                Menu = _contextMenuRef.DisplayName();
            else
                Menu = _contextMenuName;

            if (null != _miniToolbarRef)
                Toolbar = _miniToolbarRef.DisplayName();
            else
                Toolbar = _miniToolbarName;

            if (!string.IsNullOrEmpty(Menu))
            {
                if (!string.IsNullOrEmpty(Toolbar))
                    result = Menu + "<->" + Toolbar;
                else
                    result = Menu;
            }
            else
            {
                if (!string.IsNullOrEmpty(Toolbar))
                    result = Toolbar;
                else
                    result = RS_CONTEXT_MAP;
            }
            return result;
        }

        public override RibbonObjectType ObjectType() { return RibbonObjectType.ContextMap; }

        public override bool CanReorder()
        {
            return true;
        }

        public TRibbonViewContextPopup ContextPopup { get { return _contextPopup; } }
        public TRibbonContextMenu ContextMenuRef
        {
            get { return _contextMenuRef; }
            set { SetContextMenuRef(value); }
        }
        public TRibbonMiniToolbar MiniToolbarRef
        {
            get { return _miniToolbarRef; }
            set { SetMiniToolbarRef(value); }
        }
    }

    class TRibbonViewContextPopup : TRibbonView
    {
        #region Internal Declarations

        private TRibbonList<TRibbonMiniToolbar> _miniToolbars;
        private TRibbonList<TRibbonContextMenu> _contextMenus;
        private TRibbonList<TRibbonContextMap> _contextMaps;

        internal override void Save(XmlWriter writer)
        {
            writer.WriteStartElement(EN_CONTEXT_POPUP);

            if (_miniToolbars.Count > 0)
            {
                writer.WriteStartElement(EN_CONTEXT_POPUP_MINI_TOOLBARS);
                foreach (TRibbonMiniToolbar miniToolbar in _miniToolbars)
                    miniToolbar.Save(writer);

                writer.WriteEndElement();
            }

            if (_contextMenus.Count > 0)
            {
                writer.WriteStartElement(EN_CONTEXT_POPUP_CONTEXT_MENUS);
                foreach (TRibbonContextMenu contextMenu in _contextMenus)
                    contextMenu.Save(writer);

                writer.WriteEndElement();
            }

            if (_contextMaps.Count > 0)
            {
                writer.WriteStartElement(EN_CONTEXT_POPUP_CONTEXT_MAPS);
                foreach (TRibbonContextMap contextMap in _contextMaps)
                    contextMap.Save(writer);

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        public TRibbonViewContextPopup(TRibbonDocument owner) : base(owner)
        {
            _miniToolbars = new TRibbonList<TRibbonMiniToolbar>(owner, true);
            _contextMenus = new TRibbonList<TRibbonContextMenu>(owner, true);
            _contextMaps = new TRibbonList<TRibbonContextMap>(owner, true);
        }

        public TRibbonViewContextPopup(TRibbonDocument owner, XElement E) : this(owner)
        {
            _miniToolbars = new TRibbonList<TRibbonMiniToolbar>(owner, true);
            _contextMenus = new TRibbonList<TRibbonContextMenu>(owner, true);
            _contextMaps = new TRibbonList<TRibbonContextMap>(owner, true);
            foreach (XElement C in E.Elements())
            {
                if (C.Name.LocalName == EN_CONTEXT_POPUP_MINI_TOOLBARS)
                {
                    if (_miniToolbars.Count > 0)
                        Error(C, RS_MULTIPLE_ELEMENTS, E.Name.LocalName, C.Name.LocalName);
                    foreach (XElement GC in C.Elements())
                    {
                        if (GC.Name.LocalName != EN_MINI_TOOLBAR)
                            Error(GC, RS_ELEMENT_EXPECTED, EN_MINI_TOOLBAR, GC.Name.LocalName);
                        _miniToolbars.Add(new TRibbonMiniToolbar(owner, GC));
                    }
                }
                else if (C.Name.LocalName == EN_CONTEXT_POPUP_CONTEXT_MENUS)
                {
                    if (_contextMenus.Count > 0)
                        Error(C, RS_MULTIPLE_ELEMENTS, E.Name.LocalName, C.Name.LocalName);
                    foreach (XElement GC in C.Elements())
                    {
                        if (GC.Name.LocalName != EN_CONTEXT_MENU)
                            Error(GC, RS_ELEMENT_EXPECTED, EN_CONTEXT_MENU, GC.Name.LocalName);
                        _contextMenus.Add(new TRibbonContextMenu(owner, GC));
                    }
                }
                else if (C.Name.LocalName == EN_CONTEXT_POPUP_CONTEXT_MAPS)
                {
                    if (_contextMaps.Count > 0)
                        Error(C, RS_MULTIPLE_ELEMENTS, E.Name.LocalName, C.Name.LocalName);
                    foreach (XElement GC in C.Elements())
                    {
                        if (GC.Name.LocalName != EN_CONTEXT_MAP)
                            Error(GC, RS_ELEMENT_EXPECTED, EN_CONTEXT_MAP, GC.Name.LocalName);
                        _contextMaps.Add(new TRibbonContextMap(owner, this, GC, null));
                    }
                }
                else
                    Error(C, RS_UNSUPPORTED_CHILD_ELEMENT, C.Name.LocalName, E.Name.LocalName);
            }
        }

        public void FixupContextMaps()
        {
            foreach (TRibbonContextMap map in _contextMaps)
                map.FixupReferences();
        }

        #endregion Internal Declarations

        //destructor
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _miniToolbars?.Dispose();
                _contextMenus?.Dispose();
                _contextMaps?.Dispose();
            }
            base.Dispose(disposing);
        }

        public override RibbonObjectType ObjectType() { return RibbonObjectType.ViewContextPopup; }

        public override TRibbonObject AddNew(RibbonObjectType objType)
        {
            TRibbonObject result;
            if (objType == RibbonObjectType.MiniToolbar)
            {
                result = new TRibbonMiniToolbar(Owner);
                _miniToolbars.Add((TRibbonMiniToolbar)(result));
            }
            else if (objType == RibbonObjectType.ContextMenu)
            {
                result = new TRibbonContextMenu(Owner);
                _contextMenus.Add((TRibbonContextMenu)(result));
            }
            else if (objType == RibbonObjectType.ContextMap)
            {
                result = new TRibbonContextMap(Owner, this, null);
                _contextMaps.Add((TRibbonContextMap)(result));
            }
            else
                result = base.AddNew(objType);
            return result;
        }

        public override bool Remove(TRibbonObject obj)
        {
            bool result;
            if (obj is TRibbonMiniToolbar)
                result = _miniToolbars.Remove((TRibbonMiniToolbar)(obj));
            else if (obj is TRibbonContextMenu)
                result = _contextMenus.Remove((TRibbonContextMenu)(obj));
            else if (obj is TRibbonContextMap)
                result = _contextMaps.Remove((TRibbonContextMap)(obj));
            else
                result = base.Remove(obj);
            return result;
        }

        public override bool Reorder(TRibbonObject child, int direction)
        {
            bool result;
            if (child is TRibbonMiniToolbar)
                result = _miniToolbars.Reorder(child, direction);
            else if (child is TRibbonContextMenu)
                result = _contextMenus.Reorder(child, direction);
            else if (child is TRibbonContextMap)
                result = _contextMaps.Reorder(child, direction);
            else
                result = base.Reorder(child, direction);
            return result;
        }

        public TRibbonList<TRibbonMiniToolbar> MiniToolbars { get { return _miniToolbars; } }
        public TRibbonList<TRibbonContextMenu> ContextMenus { get { return _contextMenus; } }
        public TRibbonList<TRibbonContextMap> ContextMaps { get { return _contextMaps; } }
    }

    class TRibbonApplication : TRibbonObject
    {
        #region Internal Declarations

        private TRibbonList<TRibbonCommand> _commands;
        private TRibbonDictionary<string, TRibbonCommand> _commandsByName;
        private TRibbonDictionary<int, TRibbonCommand> _commandsById;
        private TRibbonList<TRibbonView> _views;
        private string _resourceName;

        private TRibbonViewRibbon GetRibbon()
        {
            foreach (TRibbonViewRibbon view in _views)
                if (view is TRibbonViewRibbon)
                    return (TRibbonViewRibbon)(view);

            return null;
        }

        public TRibbonApplication(TRibbonDocument owner) : base(owner)
        {
            _resourceName = ApplicationDefaultName;
            _commands = new TRibbonList<TRibbonCommand>(owner, true);
            _commandsByName = new TRibbonDictionary<string, TRibbonCommand>(owner);
            _commandsById = new TRibbonDictionary<Int32, TRibbonCommand>(owner);
            _views = new TRibbonList<TRibbonView>(owner, true);
            _views.Add(new TRibbonViewRibbon(owner));
            _views.Add(new TRibbonViewContextPopup(owner));
        }

        public TRibbonApplication(TRibbonDocument owner, bool empty) : base(owner) { } //CreateEmpty

        public void Load(XElement E)
        {
            if ((E == null) || (E.Name.LocalName != EN_APPLICATION))
                Error(E, RS_NO_APPLICATION);

            bool hasRibbon = false;

            // Load commands first
            foreach (XElement C in E.Elements())
            {
                if (C.Name.LocalName == EN_APPLICATION_COMMANDS)
                {
                    if (null != _commands)
                        Error(C, RS_INVALID_COMMANDS);
                    _commands = new TRibbonList<TRibbonCommand>(Owner, true);
                    _commandsByName = new TRibbonDictionary<string, TRibbonCommand>(Owner);
                    _commandsById = new TRibbonDictionary<Int32, TRibbonCommand>(Owner);
                    foreach (XElement GC in C.Elements())
                    {
                        //XElement GC = GCNode as XElement;
                        //if (GC == null)
                        //    continue;
                        TRibbonCommand command = new TRibbonCommand(Owner, GC);
                        _commands.Add(command);
                        if (!string.IsNullOrEmpty(command.Name))
                            _commandsByName.Add(command.Name, command);
                        if (command.Id != 0)
                            _commandsById.Add(command.Id, command);
                    }
                }
                else if (C.Name.LocalName != EN_APPLICATION_VIEWS)
                    Error(C, RS_UNSUPPORTED_CHILD_ELEMENT, C.Name.LocalName, E.Name.LocalName);
            }

            if (_commands == null)
            {
                _commands = new TRibbonList<TRibbonCommand>(Owner, true);
                _commandsByName = new TRibbonDictionary<string, TRibbonCommand>(Owner);
                _commandsById = new TRibbonDictionary<Int32, TRibbonCommand>(Owner);
            }

            // Load views next.These depends
            // on the commands.
            foreach (XElement C in E.Elements())
            {
                if (C.Name.LocalName == EN_APPLICATION_VIEWS)
                {
                    if (null != _views)
                        Error(C, RS_INVALID_VIEWS);
                    _views = new TRibbonList<TRibbonView>(Owner, true);
                    foreach (XElement GC in C.Elements())
                    {
                        if (GC.Name.LocalName == EN_RIBBON)
                        {
                            if (hasRibbon)
                                Error(GC, RS_MULTIPLE_ELEMENTS, C.Name.LocalName, GC.Name.LocalName);
                            hasRibbon = true;
                            _views.Add(new TRibbonViewRibbon(Owner, GC));
                        }
                        else if (GC.Name.LocalName == EN_CONTEXT_POPUP)
                            _views.Add(new TRibbonViewContextPopup(Owner, GC));
                        else
                            Error(GC, RS_UNSUPPORTED_CHILD_ELEMENT, GC.Name.LocalName, C.Name.LocalName);
                    }
                }
                else if (C.Name.LocalName != EN_APPLICATION_COMMANDS)
                    Error(C, RS_UNSUPPORTED_CHILD_ELEMENT, C.Name.LocalName, E.Name.LocalName);
            }

            if (!hasRibbon)
                Error(E, RS_SINGLE_ELEMENT, EN_APPLICATION_VIEWS, EN_RIBBON);

            if (_views == null)
                Error(E, RS_INVALID_VIEWS);

            // Fixup the<ContextPopup.ContextMaps> references
            FixupContextMaps();
        }

        public void Save(XmlWriter writer)
        {
            writer.WriteStartDocument();
            writer.WriteStartElement(EN_APPLICATION, RIBBON_NAMESPACE);
            //writer.WriteAttributeString(AN_XMLNS, RIBBON_NAMESPACE);

            if (_commands.Count > 0)
            {
                writer.WriteStartElement(EN_APPLICATION_COMMANDS);

                foreach (TRibbonCommand command in _commands)
                    command.Save(writer);

                writer.WriteEndElement();
            }

            writer.WriteStartElement(EN_APPLICATION_VIEWS);
            foreach (TRibbonView view in _views)
                view.Save(writer);

            writer.WriteEndElement();

            writer.WriteEndElement();
            writer.WriteEndDocument();
        }

        public void FixupContextMaps()
        {
            TRibbonViewContextPopup popup;
            foreach (TRibbonView view in _views)
            {
                popup = view as TRibbonViewContextPopup;
                if (popup != null)
                    popup.FixupContextMaps();
            }
        }

        public void CommandNameChanged(TRibbonCommand command,
            string oldName, string newName)
        {
            if (newName != oldName)
            {
                _commandsByName.Remove(oldName);
                _commandsByName.Add(newName, command);
            }
        }

        public void CommandIdChanged(TRibbonCommand command,
            int oldId, int newId)
        {
            if (newId != oldId)
            {
                _commandsById.Remove(oldId);
                _commandsById.Add(newId, command);
            }
        }

        #endregion Internal Declarations

        //destructor
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _commands?.Dispose();
                _commandsByName?.Dispose();
                _commandsById?.Dispose();
                _views?.Dispose();
            }
            base.Dispose(disposing);
        }

        public override RibbonObjectType ObjectType() { return RibbonObjectType.Application; }

        public override TRibbonObject AddNew(RibbonObjectType objType)
        {
            TRibbonObject result;
            if (objType == RibbonObjectType.ViewContextPopup)
            {
                result = new TRibbonViewContextPopup(Owner);
                _views.Add((TRibbonViewContextPopup)(result));
            }
            else
                result = base.AddNew(objType);
            return result;
        }

        public override bool Remove(TRibbonObject obj)
        {
            bool result;
            if (obj is TRibbonViewContextPopup)
                result = _views.Remove((TRibbonViewContextPopup)(obj));
            else
                result = base.Remove(obj);
            return result;
        }

        public override bool Reorder(TRibbonObject child, int direction)
        {
            bool result;
            if (child is TRibbonCommand)
                result = _commands.Reorder(child, direction);
            else
                result = base.Reorder(child, direction);
            return result;
        }

        /* <Application.Commands> element.  */
        public TRibbonList<TRibbonCommand> Commands { get { return _commands; } }

        public TRibbonCommand AddCommand(string name)
        {
            TRibbonCommand result = new TRibbonCommand(Owner);
            result.Name = name;
            _commands.Add(result);
            return result;
        }

        [Obsolete]
        public void DeleteCommand(TRibbonCommand command)
        {
            RemoveCommand(command);
        }

        public void RemoveCommand(TRibbonCommand command)
        {
            _commands.Remove(command);
        }

        /* Finds a command with the given name or Id.
         Returns null when the command does not exist.  */
        public TRibbonCommand FindCommand(string name)
        {
            TRibbonCommand result;
            _commandsByName.TryGetValue(name, out result);
            return result;
        }

        public TRibbonCommand FindCommand(int id)
        {
            TRibbonCommand result;
            _commandsById.TryGetValue(id, out result);
            return result;
        }

        public TRibbonCommand FindCommand(TRibbonCommandName name)
        {
            TRibbonCommand result;
            if (name.Id > 0)
                _commandsById.TryGetValue(name.Id, out result);
            else
                _commandsByName.TryGetValue(name.Name, out result);
            return result;
        }

        /* Finds a context menu or mini toolbar with the given name  */
        public TRibbonContextMenu FindContextMenu(string name)
        {
            TRibbonViewContextPopup popup;
            if (!string.IsNullOrEmpty(name))
            {
                foreach (TRibbonView view in _views)
                {
                    popup = view as TRibbonViewContextPopup;
                    if (popup != null)
                    {
                        foreach (TRibbonContextMenu result in popup.ContextMenus)
                            if (result.Name == name)
                                return result;
                    }
                }
            }
            return null;
        }

        public TRibbonMiniToolbar FindMiniToolbar(string name)
        {
            TRibbonViewContextPopup popup;
            if (!string.IsNullOrEmpty(name))
            {
                foreach (TRibbonView view in _views)
                {
                    popup = view as TRibbonViewContextPopup;
                    if (popup != null)
                    {
                        foreach (TRibbonMiniToolbar result in popup.MiniToolbars)
                            if (result.Name == name)
                                return result;
                    }
                }
            }
            return null;
        }

        /* <Application.Views> element.  */
        public TRibbonList<TRibbonView> Views { get { return _views; } }

        public TRibbonViewRibbon Ribbon { get { return GetRibbon(); } }
        public string ResourceName { get { return _resourceName; } set { _resourceName = value; } }
    }
}
