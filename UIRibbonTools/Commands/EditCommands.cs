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
    partial class EditCommands : Form
    {
        public EditCommands()
        {
            if (!DesignMode)
                Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
        }
    }
}
