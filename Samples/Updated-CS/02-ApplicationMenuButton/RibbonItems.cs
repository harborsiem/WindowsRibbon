using System;
using System.Windows.Forms;
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
            ApplicationMenu.TooltipTitle = "Menu";
            ApplicationMenu.TooltipDescription = "Application main menu";

            ButtonNew.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonNew_ExecuteEvent);
        }

        void _buttonNew_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            MessageBox.Show("new button pressed");
        }

        public void Load()
        {
        }

    }
}
