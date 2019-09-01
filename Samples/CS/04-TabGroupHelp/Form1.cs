using System;
using System.Windows.Forms;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Controls.Events;
using RibbonLib.Interop;

namespace _04_TabGroupHelp
{
    public enum RibbonMarkupCommands : uint 
    {
         cmdButtonNew = 1001,
         cmdButtonOpen = 1002,
         cmdButtonSave = 1003,
         cmdButtonExit = 1004,
         cmdButtonDropA = 1008,
         cmdButtonDropB = 1009,
         cmdButtonDropC = 1010,
         cmdTabMain = 1011,
         cmdTabDrop = 1012,
         cmdGroupFileActions = 1013,
         cmdGroupExit = 1014,
         cmdGroupDrop = 1015,
         cmdHelpButton = 1016,
    }

    public partial class Form1 : Form
    {
        private RibbonButton _exitButton;
        private RibbonHelpButton _helpButton;

        public Form1()
        {
            InitializeComponent();

            _exitButton = new RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonExit);
            _helpButton = new RibbonHelpButton(_ribbon, (uint)RibbonMarkupCommands.cmdHelpButton);

            _exitButton.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_exitButton_ExecuteEvent);
            _helpButton.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_helpButton_ExecuteEvent);
        }

        void _exitButton_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            // Close form asynchronously since we are in a ribbon event 
            // handler, so the ribbon is still in use, and calling Close 
            // will eventually call _ribbon.DestroyFramework(), which is 
            // a big no-no, if you still use the ribbon.
            this.BeginInvoke(new MethodInvoker(this.Close));
        }

        void _helpButton_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            MessageBox.Show("Help button pressed");
        }
    }
}
