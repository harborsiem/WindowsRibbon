using System;
using System.Windows.Forms;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Controls.Events;
using RibbonLib.Interop;

namespace _10_CheckBox
{
    public enum RibbonMarkupCommands : uint 
    {
         cmdButton = 1001,
         cmdToggleButton = 1002,
         cmdCheckBox = 1003,
         cmdDropDown = 1004,
         cmdTabMain = 1011,
         cmdGroupButtons = 1013,
         cmdGroupCheckBox = 1014,
    }

    public partial class Form1 : Form
    {
        private RibbonButton _button;
        private RibbonToggleButton _toggleButton;
        private RibbonCheckBox _checkBox;

        public Form1()
        {
            InitializeComponent();

            _button = new RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButton);
            _toggleButton = new RibbonToggleButton(_ribbon, (uint)RibbonMarkupCommands.cmdToggleButton);
            _checkBox = new RibbonLib.Controls.RibbonCheckBox(_ribbon, (uint)RibbonMarkupCommands.cmdCheckBox);

            _button.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_button_ExecuteEvent);
        }

        void _button_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            MessageBox.Show("checkbox check status is: " +  _checkBox.BooleanValue.ToString());
        }
    }
}
