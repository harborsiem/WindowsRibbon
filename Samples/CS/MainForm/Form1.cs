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
using RibbonLib.Interop;

namespace MainForm
{
    public partial class Form1 : Form
    {
        RibbonItems r;

        public Form1()
        {
            if (!DesignMode)
            {
                Font = SystemFonts.MessageBoxFont;
            }
            InitializeComponent();
            Load += Form1_Load;
            Shown += Form1_Shown;
            r = new RibbonItems(ribbon1);
            r.BelowRibbon = tableLayoutPanel1;
            r.AfterInit();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            r.OnShown();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            r.OnLoad();
        }
    }
}
