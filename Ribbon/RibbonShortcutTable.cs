using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace RibbonLib
{
    /// <summary>
    /// Ribbon Shortcut class (can't used by a user,
    /// only used with the embedded xml resource file for shortcuts) 
    /// </summary>
    public class RibbonShortcut
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public uint CommandId { get; set; }

        string _shortcut;

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("Shortcut")]
        public string Shortcut
        {
            get 
            { 
                return _shortcut; 
            }
            set
            {
                _shortcut = value;
                _shortcutKeys = ConvertToKeys(value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Keys ConvertToKeys(string value)
        {
            if (string.IsNullOrEmpty(value))
                return Keys.None;

            Keys result = Keys.None;

            string[] keys = value.Split('+');
            foreach (string key in keys)
            {
                string formattedKey;
                if (key == "Ctrl")
                    formattedKey = "Control";
                else
                    formattedKey = key;

                try
                {
                    Keys k = (Keys)Enum.Parse(typeof(Keys), formattedKey, true);
                    result |= k;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(string.Format("The ShortcutKey '{0}' is invalid. The token '{1}' is unknown", value, key), ex);
                }
            }

            return result;
        }

        Keys _shortcutKeys;
        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public Keys ShortcutKeys
        {
            get { return _shortcutKeys; }
        }
    }

    /// <summary>
    /// Ribbon ShortcutTable class (can't used by a user,
    /// only used with the embedded xml resource file for shortcuts) 
    /// </summary>
    public class RibbonShortcutTable
    {
        /// <summary>
        /// 
        /// </summary>
        public RibbonShortcut[] RibbonShortcutArray
        {
            get
            {
                if (_ribbonShortcuts == null)
                    return null;
                return _ribbonShortcuts.ToArray();
            }
            set
            {
                if (value == null)
                    _ribbonShortcuts = new List<RibbonShortcut>();
                else
                    _ribbonShortcuts = new List<RibbonShortcut>(value);
            }
        }

        List<RibbonShortcut> _ribbonShortcuts = new List<RibbonShortcut>();

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore()]
        public List<RibbonShortcut> RibbonShortcuts
        {
            get { return _ribbonShortcuts; }
        }

        /// <summary>
        /// Tests if the shortcut has an underlying command id
        /// </summary>
        /// <param name="shortcutKeys">the shortcut keys</param>
        /// <returns>the command name</returns>
        public uint HitTest(Keys shortcutKeys)
        {
            var ribbonShortcut = this.RibbonShortcuts.FirstOrDefault(s => s.ShortcutKeys == shortcutKeys);
            if(ribbonShortcut == null)
                return 0;
            return ribbonShortcut.CommandId;
        }
    }
}
