using System;
using System.Windows.Forms;

using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Interop;
using RibbonLib.Controls.Events;
using System.IO;

namespace _17_QuickAccessToolbar
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
