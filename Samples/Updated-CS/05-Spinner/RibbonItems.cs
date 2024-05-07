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

            ButtonDropA.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonDropA_ExecuteEvent);
            Spinner.RepresentativeString = "2.50 m";
        }

        void _buttonDropA_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            InitSpinner();
        }

        private void InitSpinner()
        {
            Spinner.DecimalPlaces = 2;
            Spinner.DecimalValue = 1.8M;
            Spinner.TooltipTitle = "Height";
            Spinner.TooltipDescription = "Enter height in meters.";
            Spinner.MaxValue = 2.5M;
            Spinner.MinValue = 0;
            Spinner.Increment = 0.01M;
            Spinner.FormatString = " m";
            Spinner.Label = "Height:";
        }

        public void Load()
        {
        }

    }
}
