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
            ButtonDropB.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonDropB_ExecuteEvent);
        }

        void _buttonDropB_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            MessageBox.Show("drop B button pressed");
        }

        public void Load()
        {
        }

    }
}
