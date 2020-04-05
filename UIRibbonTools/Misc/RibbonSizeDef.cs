using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIRibbonTools
{
    class RibbonSizeDef : IComparable
    {
        public string Name { get; private set; }
        public TRibbonSizeDefinition Value { get; private set; }

        public RibbonSizeDef(string name, TRibbonSizeDefinition value)
        {
            Name = name;
            Value = value;
        }
        public override string ToString()
        {
            // Generates the text shown in the combo box
            return Name;
        }

        public int CompareTo(object obj)
        {
            if (obj == null || !(obj is RibbonSizeDef))
                return 0;
            RibbonSizeDef rsd = (RibbonSizeDef)obj;
            return this.Name.CompareTo(rsd.Name);
        }

        public static int IndexOf(ComboBox combo, string customSizeDef)
        {
            List<string> list = new List<string>();
            ComboBox.ObjectCollection items = combo.Items;
            for (int i = 0; i < items.Count; i++)
            {
                list.Add(((RibbonSizeDef)items[i]).Name);
            }
            return list.IndexOf(customSizeDef);
        }
    }
}
