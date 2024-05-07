using System;
using System.Windows.Forms;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Controls.Events;
using RibbonLib.Interop;

namespace _14_ContextualTabs
{
    public partial class Form1 : Form
    {
        private RibbonItems _ribbonItems;

        public Form1()
        {
            InitializeComponent();
            _ribbonItems = new RibbonItems(_ribbon);
            _ribbonItems.Init();

        }
    }
}
