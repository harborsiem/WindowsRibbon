using System;
using System.Windows.Forms;
using RibbonLib;
using RibbonLib.Controls;

namespace _01_AddingRibbonSupport
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
