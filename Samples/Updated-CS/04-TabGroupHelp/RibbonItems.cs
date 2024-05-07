using System;
using System.Windows.Forms;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Controls.Events;

namespace RibbonLib.Controls
{
    partial class RibbonItems
    {
        private Form _form;

        public void Init(Form form)
        {
            _form = form;
            ButtonExit.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_exitButton_ExecuteEvent);
            HelpButton.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_helpButton_ExecuteEvent);
        }

        void _exitButton_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            // Close form asynchronously since we are in a ribbon event 
            // handler, so the ribbon is still in use, and calling Close 
            // will eventually call _ribbon.DestroyFramework(), which is 
            // a big no-no, if you still use the ribbon.
            _form.BeginInvoke(new MethodInvoker(_form.Close));
        }

        void _helpButton_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            MessageBox.Show("Help button pressed");
        }

        public void Load()
        {
        }

    }
}
