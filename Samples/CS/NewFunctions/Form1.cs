using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RibbonLib;
using RibbonLib.Controls;

namespace NewFunctions
{
    public partial class Form1 : Form
    {
        private RibbonItems _ribbonItems;

        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
            _ribbonItems = new RibbonItems(ribbon1);
            _ribbonItems.Init();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _ribbonItems.Load();
        }
    }
}
