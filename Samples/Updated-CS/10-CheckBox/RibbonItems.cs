using System;
using System.Windows.Forms;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Controls.Events;

namespace RibbonLib.Controls
{
    partial class RibbonItems
    {
        public void Init()
        {
            Button.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_button_ExecuteEvent);
        }

        void _button_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            MessageBox.Show("checkbox check status is: " + CheckBox.BooleanValue.ToString());
        }

        public void Load()
        {
        }

    }
}
