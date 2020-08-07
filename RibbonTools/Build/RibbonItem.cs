using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIRibbonTools
{
    public class RibbonItem
    {
        public string CommandName { get; set; }
        public string RibbonClassName { get; set; }
        public uint CommandId { get; set; }
        public bool IsContextPopup { get { return (RibbonClassName == "ContextPopup"); } }
        //public uint ApplicationMode { get; set; }
        public string Comment { get; set; }

        public RibbonItem(string commandName, string ribbonClassName, uint commandId)
        {
            this.CommandName = commandName;
            this.RibbonClassName = ribbonClassName;
            this.CommandId = commandId;
        }

        public override string ToString()
        {
            return CommandName + ", " + RibbonClassName + ", " + CommandId.ToString();
        }
    }
}
