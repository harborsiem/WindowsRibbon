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
    public partial class InputQuery : Form
    {
        protected InputQuery()
        {
            //#if Core
            // do not uncomment, because the Font of this Form is set to the default Font of .Net Core and later versions
            // default Font is Segoe UI, 9f, Regular
            //            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
            //#endif
            InitializeComponent();
        }

        public static DialogResult Show(string caption, string label, out string text)
        {
            InputQuery dialog = new InputQuery();
            dialog.label.Text = label;
            dialog.Text = caption;
            DialogResult result;

            if ((result = dialog.ShowDialog()) == DialogResult.OK)
            {
                text = dialog.textBox.Text;
            }
            else
                text = string.Empty;
            return result;
        }

        public static DialogResult Show(Form owner, string caption, string label, out string text)
        {
            InputQuery dialog = new InputQuery();
            dialog.label.Text = label;
            dialog.Text = caption;
            DialogResult result;
            dialog.Owner = owner;

            if ((result = dialog.ShowDialog(owner)) == DialogResult.OK)
            {
                text = dialog.textBox.Text;
            }
            else
                text = string.Empty;
            return result;
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
