using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace UIRibbonTools
{
    partial class NewFileForm : Form
    {
        const string RS_SELECT_DIR_CAPTION = "Select or create directory for Ribbon Document";

        public NewFileForm()
        {
            InitializeComponent();
            if (components == null)
                components = new Container();
            button1.ImageList = ImageManager.ImageList_NewFile(components);
            button1.ImageIndex = 0;
            button1.MouseEnter += Button1_MouseEnter;
            button1.MouseLeave += Button1_MouseLeave;
            EditDirectory.Text = Directory.GetCurrentDirectory();
            button1.Click += EditDirectoryRightButtonClick;
            EditFilename.TextChanged += EditFilenameChange;
            EditDirectory.TextChanged += EditDirectoryChange;
        }

        private void Button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ImageIndex = 0;
        }

        private void Button1_MouseEnter(object sender, EventArgs e)
        {
            button1.ImageIndex = 1;
        }

        public static bool NewFileDialog(out RibbonTemplate template, out string fileName)
        {
            NewFileForm form;
            bool result;
            template = RibbonTemplate.None;
            fileName = string.Empty;
            form = new NewFileForm();
            try
            {
                result = (form.ShowDialog() == DialogResult.OK);
                if (result)
                {
                    int itemIndex = 0;
                    if (form.radioButton2.Checked)
                        itemIndex = 1;
                    template = (RibbonTemplate)(itemIndex);
                    fileName = Path.Combine(form.EditDirectory.Text, form.EditFilename.Text);
                }
            }
            finally
            {
                form.Close();
            }
            return result;
        }

        private void EditDirectoryChange(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void EditDirectoryRightButtonClick(object sender, EventArgs e)
        {
            string directory;

            directory = EditDirectory.Text;
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = RS_SELECT_DIR_CAPTION;
            dialog.RootFolder = Environment.SpecialFolder.MyComputer;
            if (!string.IsNullOrEmpty(directory) && Directory.Exists(directory))
                dialog.SelectedPath = directory;
            dialog.ShowNewFolderButton = true;

            //if (SelectDirectory(RS_SELECT_DIR_CAPTION, Path.GetDirectoryName(Directory), Directory, [sdNewFolder, sdNewUI]))
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                directory = dialog.SelectedPath;
                EditDirectory.Text = directory;
                UpdateControls();
            }
        }

        private void EditFilenameChange(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void UpdateControls()
        {
            ButtonOk.Enabled = Directory.Exists(EditDirectory.Text) && (!string.IsNullOrEmpty(EditFilename.Text));
        }
    }
}
