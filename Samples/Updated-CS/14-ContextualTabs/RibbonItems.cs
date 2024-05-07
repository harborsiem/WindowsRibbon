using System;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Controls.Events;
using RibbonLib.Interop;

namespace RibbonLib.Controls
{
    partial class RibbonItems
    {
        public void Init()
        {
            ButtonSelect.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonSelect_ExecuteEvent);
            ButtonUnselect.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonUnselect_ExecuteEvent);
        }

        void _buttonSelect_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            if (TabGroupTableTools.ContextAvailable != ContextAvailability.Active)
                TabGroupTableTools.ContextAvailable = ContextAvailability.Active;
        }

        void _buttonUnselect_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            if (TabGroupTableTools.ContextAvailable != ContextAvailability.NotAvailable)
                TabGroupTableTools.ContextAvailable = ContextAvailability.NotAvailable;
        }

        public void Load()
        {
        }

    }
}
