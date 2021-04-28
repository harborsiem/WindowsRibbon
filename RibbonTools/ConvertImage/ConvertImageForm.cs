using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace UIRibbonTools
{
    partial class ConvertImageForm : Form
    {
        InputSelector _inputSelected;
        OutputSelector _outputSelected;
        string[] _inFileNames;

        public ConvertImageForm()
        {
#if Core
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
#endif
            InitializeComponent();
#if SegoeFont
            this.Font = SystemFonts.MessageBoxFont;
#endif
            InitEvents();
            inSelectorCombo.SelectedIndex = (int)InputSelector.Bitmap;
            outCombo.SelectedIndex = (int)OutputSelector.BitmapV5;
            convertButton.Enabled = false;
        }

        private void InitEvents()
        {
            outSelectButton.Click += OutSelectButton_Click;
            inSelectButton.Click += InSelectButton_Click;
            inSelectorCombo.SelectedIndexChanged += InSelectorCombo_SelectedIndexChanged;
            outCombo.SelectedIndexChanged += OutCombo_SelectedIndexChanged;
            convertButton.Click += ConvertButton_Click;
        }

        private void ConvertButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                switch (_inputSelected)
                {
                    case InputSelector.Bitmap:
                        BitmapConverter bmpConverter = new BitmapConverter(outPathTextBox.Text, _outputSelected, inIgnoreAlphaCheck.Checked);
                        bmpConverter.Convert(_inFileNames);
                        break;
                    case InputSelector.Icon:
                        IconConverter icoConverter = new IconConverter(outPathTextBox.Text, _outputSelected);
                        icoConverter.Convert(_inFileNames);
                        break;
                    case InputSelector.IconsGif:
                        SpecialGifConverter gifConverter = new SpecialGifConverter(outPathTextBox.Text, _outputSelected);
                        gifConverter.Convert(_inFileNames);
                        break;
                    case InputSelector.Xaml:
                        XamlConverter xamlConverter = new XamlConverter(outPathTextBox.Text, _outputSelected);
                        xamlConverter.Convert(_inFileNames);
                        break;
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void OutCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            _outputSelected = (OutputSelector)outCombo.SelectedIndex;
            outIgnoreAlphaCheck.Enabled = false;
        }

        private void InSelectorCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            _inputSelected = (InputSelector)inSelectorCombo.SelectedIndex;
            inPathTextBox.Text = string.Empty;
            switch (_inputSelected)
            {
                case InputSelector.Bitmap:
                    inIgnoreAlphaCheck.Enabled = true;
                    break;
                case InputSelector.Icon:
                    inIgnoreAlphaCheck.Enabled = false;
                    break;
                case InputSelector.IconsGif:
                    inIgnoreAlphaCheck.Enabled = false;
                    break;
                case InputSelector.Xaml:
                    inIgnoreAlphaCheck.Enabled = false;
                    break;
                case InputSelector.ShowInfos:
                    inIgnoreAlphaCheck.Enabled = false;
                    break;
            }
        }

        private void InSelectButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.Multiselect = true;
            switch (_inputSelected)
            {
                case InputSelector.Bitmap:
                    dialog.Filter = "Bitmap (*.bmp;*.png)|*.bmp;*.png|Other Bitmaps (*.gif;*.tiff;*.jpeg;*.jpg)|*.gif;*.tiff;*.jpeg;*.jpg";
                    break;
                case InputSelector.Icon:
                    dialog.Filter = "Icons (*.ico)|*.ico";
                    break;
                case InputSelector.IconsGif:
                    dialog.Filter = "Standard-Icons (*.gif)|*.gif";
                    break;
                case InputSelector.Xaml:
                    dialog.Filter = "XAML (*.xaml)|*.xaml";
                    break;
                case InputSelector.ShowInfos:
                    dialog.Multiselect = false;
                    dialog.Filter = "Bitmap (*.bmp;*.png;*.ico)|*.bmp;*.png;*.ico";
                    break;
            }

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                if (_inputSelected == InputSelector.ShowInfos)
                {
                    ShowInfos(dialog.FileName);
                    return;
                }
                inPathTextBox.Text = Path.GetDirectoryName(dialog.FileName);
                _inFileNames = dialog.FileNames;
                if (!string.IsNullOrEmpty(inPathTextBox.Text) && !string.IsNullOrEmpty(outPathTextBox.Text))
                    convertButton.Enabled = true;
            }
        }

        private void OutSelectButton_Click(object sender, EventArgs e)
        {
            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
            dialog.RootFolder = Environment.SpecialFolder.CommonDocuments;
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                outPathTextBox.Text = dialog.SelectedPath;
                if (!string.IsNullOrEmpty(inPathTextBox.Text) && !string.IsNullOrEmpty(outPathTextBox.Text))
                    convertButton.Enabled = true;
            }
        }

        private void ShowInfos(string fileName)
        {
            Bitmap bmp = null;
            try
            {
                string boxText;
                string bmpText = string.Empty;
                if (Path.GetExtension(fileName) == ".ico")
                {
                    Icon icon = new Icon(fileName);
                    bmp = icon.ToBitmap();
                    icon.Dispose();
                }
                else
                {
                    bmp = new Bitmap(fileName);
                    if (bmp.RawFormat.Guid == ImageFormat.Bmp.Guid)
                    {
                        //Analyze the Headers
                        BITMAPFILEHEADER fileHeader = new BITMAPFILEHEADER();
                        BITMAPINFOHEADER infoHeader = new BITMAPINFOHEADER();
                        FileStream stream = File.OpenRead(fileName);
                        BinaryReader reader = new BinaryReader(stream);
                        fileHeader.bfType = reader.ReadUInt16();
                        fileHeader.bfSize = reader.ReadUInt32();
                        fileHeader.bfReserved1 = reader.ReadUInt16();
                        fileHeader.bfReserved2 = reader.ReadUInt16();
                        fileHeader.bfOffBits = reader.ReadUInt32();
                        infoHeader.biSize = reader.ReadUInt32();
                        infoHeader.biWidth = reader.ReadInt32();
                        infoHeader.biHeight = reader.ReadInt32();
                        infoHeader.biPlanes = reader.ReadUInt16();
                        infoHeader.biBitCount = reader.ReadUInt16();
                        infoHeader.biCompression = (BitmapCompressionMode)reader.ReadUInt32();
                        infoHeader.biSizeImage = reader.ReadUInt32();
                        infoHeader.biXPelsPerMeter = reader.ReadInt32();
                        infoHeader.biYPelsPerMeter = reader.ReadInt32();
                        infoHeader.biClrUsed = reader.ReadUInt32();
                        infoHeader.biClrImportant = reader.ReadUInt32();
                        reader.Close();
                        if (infoHeader.biSize == 40)
                            bmpText = "Bitmap V1 Header";
                        else if (infoHeader.biSize == 124)
                            bmpText = "Bitmap V5 Header";
                        else if (infoHeader.biSize == 108)
                            bmpText = "Bitmap V4 Header";
                        else if (infoHeader.biSize == 56)
                            bmpText = "Bitmap V3 Header";
                        else if (infoHeader.biSize == 52)
                            bmpText = "Bitmap V2 Header";

                        bmpText += Environment.NewLine + "Compression: " + infoHeader.biCompression.ToString();
                    }
                }
                int stride;
                BitmapData data = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), ImageLockMode.ReadOnly, bmp.PixelFormat);
                stride = data.Stride;
                bmp.UnlockBits(data);
                boxText = string.Format("Width: {0}, Height: {1}" + Environment.NewLine + "PixelFormat: {2}" + Environment.NewLine + "Stride: {3}", bmp.Width, bmp.Height, bmp.PixelFormat, stride);
                MessageBox.Show(boxText + Environment.NewLine + bmpText, Path.GetFileName(fileName), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                bmp?.Dispose();
            }
        }
    }

    public enum InputSelector
    {
        ShowInfos,
        Bitmap,
        Icon,
        IconsGif,
        Xaml,
    }

    public enum OutputSelector
    {
        BitmapV5,
        Bitmap,
        Png
    }
}
