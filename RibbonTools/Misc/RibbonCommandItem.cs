using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIRibbonTools
{
    class RibbonCommandItem : IComparable
    {
        public string Name { get; private set; }
        public TRibbonCommand Value { get; private set; }

        public RibbonCommandItem(string name, TRibbonCommand value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            // Generates the text shown in the combo box
            return Name;
        }

        public string Description
        {
            get {
                string result = Value?.LabelTitle.Content;
                if (result == null)
                    result = string.Empty;
                return result;
            }
        }

        public int CompareTo(object obj)
        {
            if (obj == null || !(obj is RibbonCommandItem))
                return 0;
            RibbonCommandItem rci = (RibbonCommandItem)obj;
            return this.Name.CompareTo(rci.Name);
        }

        public static int IndexOf(ComboBox combo, string command)
        {
            string c;
            int j = command.IndexOf('(');
            if (j > 0)
                c = command.Substring(0, j - 1);
            else
                c = command;
            List<string> list = new List<string>();
            ComboBox.ObjectCollection items = combo.Items;
            for (int i = 0; i < items.Count; i++)
            {
                list.Add(OnlyCommandName(((RibbonCommandItem)items[i]).Name));
            }
            return list.IndexOf(OnlyCommandName(command));
        }

        private static string OnlyCommandName(string command) //@ added, View reference lost if labelTitle changed
        {
            string result;
            int j = command.IndexOf('(');
            if (j > 0)
                result = command.Substring(0, j - 1);
            else
                result = command;
            return result;
        }

        public static int IndexOf(ComboBox combo, TRibbonCommand command)
        {
            List<TRibbonCommand> list = new List<TRibbonCommand>();
            ComboBox.ObjectCollection items = combo.Items;
            for (int i = 0; i < items.Count; i++)
            {
                list.Add(((RibbonCommandItem)(items[i])).Value);
            }
            return list.IndexOf(command);
        }

        public static TRibbonCommand Selected(ComboBox combo)
        {
            RibbonCommandItem item = combo.Items[combo.SelectedIndex] as RibbonCommandItem;
            return item.Value;
        }
    }
}
