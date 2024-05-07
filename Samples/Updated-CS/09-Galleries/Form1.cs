using System;
using System.Drawing;
using System.Windows.Forms;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Controls.Events;
using RibbonLib.Interop;
using System.Diagnostics;

namespace _09_Galleries
{
    public partial class Form1 : Form
    {
        private RibbonItems _ribbonItems;
        public ImageList ImageListLines => imageListLines;
        public ImageList ImageListBrushes => imageListBrushes;
        public ImageList ImageListShapes => imageListShapes;

        public Form1()
        {
            InitializeComponent();
            _ribbonItems = new RibbonItems(_ribbon);
            _ribbonItems.Init(this);

        }
    }
}
