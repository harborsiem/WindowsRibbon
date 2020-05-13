using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIRibbonTools
{
    partial class ViewsSampleForm : Form
    {
        public ViewsSampleForm()
        {
#if Core
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
#endif
            InitializeComponent();
#if SegoeFont
            this.Font = SystemFonts.MessageBoxFont;
#endif
        }
    }
}
