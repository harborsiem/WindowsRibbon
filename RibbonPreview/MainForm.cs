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

namespace RibbonPreview
{
    public partial class MainForm : Form
    {
        private PreviewRibbonItems ribbonItems;

        public MainForm()
        {
            ribbonItems = PreviewRibbonItems.Instance;
            InitializeComponent();
            openToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            openToolStrip.Click += OpenToolStripMenuItem_Click;
            buildRibbonToolStripMenuItem.Click += BuildRibbonToolStripMenuItem_Click;
            buildToolStrip.Click += BuildRibbonToolStripMenuItem_Click;
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            previewRibbonToolStripMenuItem.Click += PreviewRibbonToolStripMenuItem_Click;
            previewToolStrip.Click += PreviewRibbonToolStripMenuItem_Click;
            openPreviewToolStripMenuItem.Click += OpenPreviewToolStripMenuItem_Click;
            openPreviewToolStrip.Click += OpenPreviewToolStripMenuItem_Click;
            ribbonItems.SetActions(SetBuildEnabled, SetPreviewEnabled, SetText);
       }

        private void OpenPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenToolStripMenuItem_Click(sender, e);
            if (previewToolStrip.Enabled)
                PreviewRibbonToolStripMenuItem_Click(sender, e);
        }

        private void SetBuildEnabled(bool enabled)
        {
            buildRibbonToolStripMenuItem.Enabled = enabled;
            buildToolStrip.Enabled = enabled;
        }

        private void SetPreviewEnabled(bool enabled)
        {
            previewRibbonToolStripMenuItem.Enabled = enabled;
            previewToolStrip.Enabled = enabled;
        }

        private void SetText(string text)
        {
            //StringReader sr = new StringReader(text);
            //List<string> vs = new List<string>();
            //string line;
            //while ((line = sr.ReadLine()) != null)
            //{
            //    vs.Add(line);
            //}
            //textBox1.Lines = vs.ToArray();
            textBox1.Text = text;
        }

        private void PreviewRibbonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetText(string.Empty);
            PreviewForm dialog = new PreviewForm();
            if (dialog.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BuildRibbonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetText(string.Empty);
            ribbonItems.BuildRibbonFile();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.DefaultExt = "xml";
            dialog.Filter = " (*.xml)|*.xml";
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                ribbonItems.SetRibbonXmlFile(dialog.FileName);
            }
        }
    }
}
