using System;
using System.Windows.Forms;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Controls.Events;
using RibbonLib.Interop;

namespace _02_ApplicationMenuButton
{
    public enum RibbonMarkupCommands : uint 
    {
         cmdApplicationMenu = 1000,
         cmdButtonNew = 1001,
         cmdButtonOpen = 1002,
         cmdButtonSave = 1003,
         cmdButtonExit = 1004,
    }
    
    public partial class Form1 : Form
    {
        private RibbonApplicationMenu _applicationMenu;
        private RibbonButton _buttonNew;
        private RibbonButton _buttonOpen;
        private RibbonButton _buttonSave;
        private RibbonButton _buttonExit;

        public Form1()
        {
            InitializeComponent();

            _applicationMenu = new RibbonApplicationMenu(_ribbon, (uint)RibbonMarkupCommands.cmdApplicationMenu);
            _buttonNew = new RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonNew);
            _buttonOpen = new RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonOpen);
            _buttonSave = new RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonSave);
            _buttonExit = new RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonExit);

            _applicationMenu.TooltipTitle = "Menu";
            _applicationMenu.TooltipDescription = "Application main menu";

            _buttonNew.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonNew_ExecuteEvent);
        }

        void _buttonNew_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            MessageBox.Show("new button pressed");
        }
    }
}
