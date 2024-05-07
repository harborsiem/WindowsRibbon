using System;
using System.Drawing;
using System.Windows.Forms;
using RibbonLib;
using RibbonLib.Controls;

namespace _07_RibbonColor
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
