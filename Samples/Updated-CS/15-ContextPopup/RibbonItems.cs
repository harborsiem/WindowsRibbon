using System;
using System.Windows.Forms;
using RibbonLib;
using RibbonLib.Controls;
using _15_ContextPopup;

namespace RibbonLib.Controls
{
    partial class RibbonItems
    {
        private Form1 _form;
        private RibbonContextMenuStrip _ribbonContextMenuStrip;

        public void Init(Form1 form)
        {
            _form = form;
            _ribbonContextMenuStrip = new RibbonContextMenuStrip(Ribbon, cmdContextMap);
            // recommended way
            _form.Panel2.MouseClick += new MouseEventHandler(panel2_MouseClick);

            // convenient way
            _form.Panel1.ContextMenuStrip = _ribbonContextMenuStrip;
        }

        internal void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                System.Drawing.Point p = _form.Panel2.PointToScreen(e.Location);
                Ribbon.ShowContextPopup(cmdContextMap, p.X, p.Y);
            }
        }

        public void Load()
        {
            Ribbon.Viewable = false;
        }

    }
}
