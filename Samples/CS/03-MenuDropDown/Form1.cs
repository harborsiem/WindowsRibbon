using System;
using System.Windows.Forms;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Controls.Events;
using RibbonLib.Interop;

namespace _03_MenuDropDown
{
    public enum RibbonMarkupCommands : uint 
    {
         cmdButtonNew = 1001,
         cmdButtonOpen = 1002,
         cmdButtonSave = 1003,
         cmdButtonExit = 1004,
         cmdMenuGroupFile = 1005,
         cmdMenuGroupExit = 1006,
         cmdDropDownButton = 1007,
         cmdButtonDropA = 1008,
         cmdButtonDropB = 1009,
         cmdButtonDropC = 1010,
    }

    public partial class Form1 : Form
    {
        private RibbonButton _buttonDropB;
        
        public Form1()
        {
            InitializeComponent();

            _buttonDropB = new RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonDropB);

            _buttonDropB.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonDropB_ExecuteEvent);
        }

        void _buttonDropB_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            MessageBox.Show("drop B button pressed");
        }
    }
}
