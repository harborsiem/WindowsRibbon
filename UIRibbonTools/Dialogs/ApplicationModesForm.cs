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
    partial class ApplicationModesForm : Form
    {
        private uint _appModes;

        public ApplicationModesForm()
        {
#if Core
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
#endif
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;
            ButtonCheckAll.Click += ButtonCheckAllClick;
            ButtonClearAll.Click += ButtonClearAllClick;
            this.Load += ApplicationModesForm_Load;
            this.FormClosing += FormClose;
        }

        private void ApplicationModesForm_Load(object sender, EventArgs e)
        {
        }

        public ApplicationModesForm(uint appModes) : this()
        {
            _appModes = appModes;
            for (int i = 0; i < 32; i++)
                CheckListBoxModes.SetItemChecked(i, (_appModes & (1 << i)) != 0);
        }

        public uint AppModes
        {
            get { return _appModes; }
            set
            {
                _appModes = value;
                for (int i = 0; i < 32; i++)
                    CheckListBoxModes.SetItemChecked(i, (_appModes & (1 << i)) != 0);
            }
        }

        private void ButtonCheckAllClick(object sender, EventArgs e)
        {
            for (int i = 0; i < 32; i++)
                CheckListBoxModes.SetItemChecked(i, true);
        }

        private void ButtonClearAllClick(object sender, EventArgs e)
        {
            for (int i = 0; i < 32; i++)
                CheckListBoxModes.SetItemChecked(i, false);
        }

        private void FormClose(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                _appModes = 0;
                for (int i = 0; i < 32; i++)
                    if (CheckListBoxModes.GetItemChecked(i))
                        _appModes = _appModes | (uint)(1 << i);
                if (_appModes == 0)
                    _appModes = 0xFFFFFFFF;
            }
        }
    }
}
