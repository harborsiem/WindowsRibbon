using System;
using System.Drawing;
using System.Windows.Forms;
using RibbonLib;

namespace _07_RibbonColor
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // set ribbon colors
            _ribbon.SetColors(Color.Wheat, Color.IndianRed, Color.BlueViolet);
        }
    }
}
