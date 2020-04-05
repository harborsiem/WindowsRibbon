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
    public partial class InputBox : Form
    {
        protected InputBox()
        {
            if (!DesignMode)
                Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            if (!DesignMode)
            {
                Graphics g = CreateGraphics();
                int width = 640 * (int)g.DpiX / 96;
                g.Dispose();
                this.Size = new Size(width, this.Height);
            }
        }

        public static string Show(string caption, string label, string text)
        {
            InputBox dialog = new InputBox();
            dialog.label1.Text = label;
            dialog.Text = caption;
            dialog.textBox1.Text = text;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.textBox1.Text;
            }
            else
                return string.Empty;
        }

        public static string Show(Form owner, string caption, string label, string text)
        {
            InputBox dialog = new InputBox();
            dialog.label1.Text = label;
            dialog.Text = caption;
            dialog.textBox1.Text = text;
            dialog.Owner = owner;

            if (dialog.ShowDialog(owner) == DialogResult.OK)
            {
                return dialog.textBox1.Text;
            }
            else
                return string.Empty;
        }

        public new Form Owner
        {
            set
            {
                this.Icon = (value == null ? null : value.Icon);
                base.Owner = value;
            }

            get
            {
                return base.Owner;
            }
        }
    }
}
