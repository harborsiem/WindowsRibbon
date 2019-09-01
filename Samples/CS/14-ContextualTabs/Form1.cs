using System;
using System.Windows.Forms;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Controls.Events;
using RibbonLib.Interop;

namespace _14_ContextualTabs
{
    public enum RibbonMarkupCommands : uint
    {
        cmdTabMain = 1001,
        cmdGroupMain = 1002,
        cmdTabGroupTableTools = 1003,
        cmdTabDesign = 1004,
        cmdTabLayout = 1005,
        cmdGroupDesign = 1006,
        cmdGroupLayout = 1007,
        cmdButtonSelect = 1008,
        cmdButtonUnselect = 1009,
        cmdButtonDesign1 = 1010,
        cmdButtonDesign2 = 1011,
        cmdButtonDesign3 = 1012,
        cmdButtonLayout1 = 1013,
        cmdButtonLayout2 = 1014,
    }

    public partial class Form1 : Form
    {
        private RibbonTab _tabMain;
        private RibbonGroup _groupMain;
        private RibbonTabGroup _tabGroupTableTools;
        private RibbonTab _tabDesign;
        private RibbonTab _tabLayout;
        private RibbonGroup _groupDesign;
        private RibbonGroup _groupLayout;
        private RibbonButton _buttonSelect;
        private RibbonButton _buttonUnselect;
        private RibbonButton _buttonDesign1;
        private RibbonButton _buttonDesign2;
        private RibbonButton _buttonDesign3;
        private RibbonButton _buttonLayout1;
        private RibbonButton _buttonLayout2;

        public Form1()
        {
            InitializeComponent();

            _tabMain = new RibbonTab(_ribbon, (uint)RibbonMarkupCommands.cmdTabMain);
            _groupMain = new RibbonGroup(_ribbon, (uint)RibbonMarkupCommands.cmdGroupMain);
            _tabGroupTableTools = new RibbonTabGroup(_ribbon, (uint)RibbonMarkupCommands.cmdTabGroupTableTools);
            _tabDesign = new RibbonTab(_ribbon, (uint)RibbonMarkupCommands.cmdTabDesign);
            _tabLayout = new RibbonTab(_ribbon, (uint)RibbonMarkupCommands.cmdTabLayout);
            _groupDesign = new RibbonGroup(_ribbon, (uint)RibbonMarkupCommands.cmdGroupDesign);
            _groupLayout = new RibbonGroup(_ribbon, (uint)RibbonMarkupCommands.cmdGroupLayout);
            _buttonSelect = new RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonSelect);
            _buttonUnselect = new RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonUnselect);
            _buttonDesign1 = new RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonDesign1);
            _buttonDesign2 = new RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonDesign2);
            _buttonDesign3 = new RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonDesign3);
            _buttonLayout1 = new RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonLayout1);
            _buttonLayout2 = new RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonLayout2);

            _buttonSelect.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonSelect_ExecuteEvent);
            _buttonUnselect.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonUnselect_ExecuteEvent);
        }

        void _buttonSelect_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            _tabGroupTableTools.ContextAvailable = ContextAvailability.Active;
        }

        void _buttonUnselect_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            _tabGroupTableTools.ContextAvailable = ContextAvailability.NotAvailable;
        }
    }
}
