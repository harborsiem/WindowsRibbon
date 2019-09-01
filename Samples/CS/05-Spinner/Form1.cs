using System;
using System.Windows.Forms;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Controls.Events;
using RibbonLib.Interop;

namespace _05_Spinner
{
    public enum RibbonMarkupCommands : uint 
    {
         cmdButtonDropA = 1008,
         cmdButtonDropB = 1009,
         cmdButtonDropC = 1010,
         cmdTabDrop = 1012,
         cmdGroupDrop = 1015,
         cmdGroupMore = 1017,
         cmdSpinner = 1018,
    }

    public partial class Form1 : Form
    {
        private RibbonButton _buttonDropA;
        private RibbonSpinner _spinner;
                        
        public Form1()
        {
            InitializeComponent();

            _buttonDropA = new RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonDropA);
            _spinner = new RibbonSpinner(_ribbon, (uint)RibbonMarkupCommands.cmdSpinner);

            _buttonDropA.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonDropA_ExecuteEvent);
        }

        void _buttonDropA_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            InitSpinner();
        }

        private void InitSpinner()
        {
            _spinner.DecimalPlaces = 2;
            _spinner.DecimalValue = 1.8M;
            _spinner.TooltipTitle = "Height";
            _spinner.TooltipDescription = "Enter height in meters.";
            _spinner.MaxValue = 2.5M;
            _spinner.MinValue = 0;
            _spinner.Increment = 0.01M;
            _spinner.FormatString = " m";
            _spinner.RepresentativeString = "2.50 m";
            _spinner.Label = "Height:";
        }
    }
}
