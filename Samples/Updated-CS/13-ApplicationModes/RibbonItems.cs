using System;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Controls.Events;

namespace RibbonLib.Controls
{
    partial class RibbonItems
    {
        public void Init()
        {
            ButtonSwitchToAdvanced.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonSwitchToAdvanced_ExecuteEvent);
            ButtonSwitchToSimple.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonSwitchToSimple_ExecuteEvent);
        }

        void _buttonSwitchToAdvanced_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            Ribbon.SetModes(1);
        }

        void _buttonSwitchToSimple_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            Ribbon.SetModes(0);
        }

        public void Load()
        {
        }

    }
}
