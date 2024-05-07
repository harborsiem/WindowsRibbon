using System;
using System.Windows.Forms;

using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Interop;
using RibbonLib.Controls.Events;
using System.Collections.Generic;

namespace _16_RecentItems
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

        private void Form1_Load(object sender, EventArgs e)
        {
            _ribbonItems.Load();
        }
    }
}
