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
            //#if Core
            // do not uncomment, because the Font of this Form is set to the default Font of .Net Core and later versions
            // default Font is Segoe UI, 9f, Regular
            //            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
            //#endif
            InitializeComponent();
        }

        public static string Show(string caption, string label, string text)
        {
            InputBox dialog = new InputBox();
            dialog.label.Text = label;
            dialog.Text = caption;
            dialog.textBox.Text = text;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.textBox.Text;
            }
            else
                return string.Empty;
        }

        public static string Show(Form owner, string caption, string label, string text)
        {
            InputBox dialog = new InputBox();
            dialog.label.Text = label;
            dialog.Text = caption;
            dialog.textBox.Text = text;
            dialog.Owner = owner;

            if (dialog.ShowDialog(owner) == DialogResult.OK)
            {
                return dialog.textBox.Text;
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
