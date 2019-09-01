using System;
using System.Windows.Forms;

using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Interop;

namespace _15_ContextPopup
{
    public enum RibbonMarkupCommands : uint
    {
        cmdButtonNew = 1001,
        cmdButtonOpen = 1002,
        cmdButtonSave = 1003,
        cmdButtonExit = 1004,
        cmdContextMap = 1005,
        cmdFontControl = 1006,
        cmdDropDownColorPicker = 1007,
    }
    
    public partial class Form1 : Form
    {
        private RibbonContextMenuStrip _ribbonContextMenuStrip;

        public Form1()
        {
            InitializeComponent();

            _ribbonContextMenuStrip = new RibbonContextMenuStrip(_ribbon, (uint)RibbonMarkupCommands.cmdContextMap);

            // recommended way
            panel2.MouseClick += new MouseEventHandler(panel2_MouseClick);

            // convenient way
            panel1.ContextMenuStrip = _ribbonContextMenuStrip;
        }

        void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                System.Drawing.Point p = panel2.PointToScreen(e.Location);
                _ribbon.ShowContextPopup((uint)RibbonMarkupCommands.cmdContextMap, p.X, p.Y);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _ribbon.Viewable = false;
        }
    }
}
