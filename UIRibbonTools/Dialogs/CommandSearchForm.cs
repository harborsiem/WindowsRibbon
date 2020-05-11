using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;

namespace UIRibbonTools
{
    partial class CommandSearchForm : Form
    {
        private ListView _source;

        private CommandSearchForm()
        {
#if Core
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
#endif
            InitializeComponent();
#if SegoeFont
            this.Font = SystemFonts.MessageBoxFont;
#endif
            this.Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
            buttonCancel.Click += ButtonCancelClick;
            buttonOK.Click += ButtonOKClick;
            LabeledEditSearchInput.KeyDown += LabeledEditSearchInputKeyDown;
            LabeledEditSearchInput.TextChanged += LabeledEditSearchInputChange;
            ListViewCommands.DoubleClick += ListViewCommandsDblClick;
            //ListViewCommands.ListViewItemSorter
            this.Shown += FormShow;
        }

        public CommandSearchForm(UserControl caller, ListView source) : this()
        {
            //Owner = caller.FindForm();
            _source = source;
            UpdateListView();
        }

        //public new Form Owner
        //{
        //    set
        //    {
        //        this.Icon = (value == null ? null : value.Icon);
        //        base.Owner = value;
        //    }

        //    get
        //    {
        //        return base.Owner;
        //    }
        //}

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void ButtonOKClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }


        private void FormShow(object sender, EventArgs e)
        {
            LabeledEditSearchInput.Focus();
        }

        private void LabeledEditSearchInputChange(object sender, EventArgs e)
        {
            UpdateListView();
        }

        private void LabeledEditSearchInputKeyDown(object sender, KeyEventArgs e)
        {
            //Forward some keys to the list view to allow easier navigation
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up || e.KeyCode == Keys.Prior ||
                  e.KeyCode == Keys.Next || e.KeyCode == Keys.End || e.KeyCode == Keys.Home)
                NativeMethods.PostMessage(ListViewCommands.Handle, NativeMethods.WM_KEYDOWN, (IntPtr)e.KeyCode, IntPtr.Zero);
        }

        private void ListViewCommandsDblClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void UpdateListView()
        {
            ListViewItem newItem;
            string searchText;

            this.ListViewCommands.Items.Clear();
            searchText = LabeledEditSearchInput.Text;

            foreach (ListViewItem item in _source.Items)
            {
                if ((searchText.Length == 0) || (item.Text.ToUpper().Contains(searchText.ToUpper())))
                {
                    newItem = ListViewCommands.Items.Insert(ListViewCommands.Items.Count, (ListViewItem)item.Clone()); //@ Count - 1 ?
                }
            }

            if (ListViewCommands.Items.Count > 0)
                ListViewCommands.Items[0].Selected = true;
        }
    }
}

