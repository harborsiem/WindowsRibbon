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
    partial class ImageEditForm : Form
    {
        private TRibbonImage _image;
        private Bitmap _bitmap;
        private string _filename;
        private ImageFlags _flags;
        private bool _initialized;

        public ImageEditForm()
        {
#if Core
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
#endif
            InitializeComponent();
#if SegoeFont
            this.Font = SystemFonts.MessageBoxFont;
#endif
            Label1.Font = new Font(Label1.Font, FontStyle.Bold);
            //MemoHelp.Size = new Size(MemoHelp.Width, (MemoHelp.Font.Height) * (MemoHelp.Lines.Length + 1));
            if (components == null)
                components = new Container();
            RightButton.ImageList = ImageManager.ImageList_Edit(components);
            RightButton.ImageIndex = 0;
            RightButton.MouseEnter += RightButton_MouseEnter;
            RightButton.MouseLeave += RightButton_MouseLeave;
            InitializeEvents();
        }

        private void RightButton_MouseLeave(object sender, EventArgs e)
        {
            RightButton.ImageIndex = 0;
        }

        private void RightButton_MouseEnter(object sender, EventArgs e)
        {
            RightButton.ImageIndex = 1;
        }

        private void InitializeEvents()
        {
            this.Shown += TFormEditImage_Shown;
            this.FormClosed += FormClose;
            RightButton.Click += EditImageFileRightButtonClick;
            EditImageFile.TextChanged += EditImageFileChange;
            PaintBox.Paint += PaintBoxPaint;
        }

        private void TFormEditImage_Shown(object sender, EventArgs e)
        {
            if (!_initialized)
            {
                _initialized = true;
                if (string.IsNullOrEmpty(_image.Source))
                    EditImageFileRightButtonClick(null, EventArgs.Empty);
            }
        }

        private void ClearBitmap(Bitmap bitmap)
        {
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);
            g.Dispose();
        }

        public ImageEditForm(TRibbonImage image,
           ImageFlags flags) : this()
        {
            Bitmap uIImage;

            _image = image;
            _flags = flags;
            _bitmap = new Bitmap(64, 64, PixelFormat.Format32bppArgb);
            ClearBitmap(_bitmap);
            _filename = image.Owner.BuildAbsoluteFilename(image.Source);
            if (File.Exists(_filename))
            {
                uIImage = AlphaBitmap.TryAlphaBitmapFromFile(_filename);
                try
                {
                    Graphics canvas = Graphics.FromImage(_bitmap);
                    canvas.DrawImage(uIImage, new Point((64 - uIImage.Width) / 2, (64 - uIImage.Height) / 2));
                    canvas.Dispose();
                }
                finally
                {
                    uIImage.Dispose();
                }
            }

            EditImageFile.Text = _image.Source;
            if (_image.MinDpi == 0)
                ComboBoxMinDpi.SelectedIndex = 0;
            else if (_image.MinDpi < 108)
                ComboBoxMinDpi.SelectedIndex = 1;
            else if (_image.MinDpi < 132)
                ComboBoxMinDpi.SelectedIndex = 2;
            else if (_image.MinDpi < 156)
                ComboBoxMinDpi.SelectedIndex = 3;
            else
                ComboBoxMinDpi.SelectedIndex = 4;

            EditResourceId.Value = _image.Id;
            EditSymbol.Text = _image.Symbol;
            UpdateControls();
        }

        private void Destroy() //called in Dispose(bool disposing)
        {
            if (_bitmap != null)
                _bitmap.Dispose();
        }

        private void EditImageFileChange(object sender, EventArgs e)
        {
            _filename = _image.Owner.BuildAbsoluteFilename(EditImageFile.Text);
            UpdateControls();
        }

        private void EditImageFileRightButtonClick(object sender, EventArgs e)
        {
            string newFilename;
            bool usePngFile;
            Bitmap uIImage;
            Bitmap bitmap;
            int size;

            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "BMP and PNG files|*.bmp;*.png|BMP files|*.bmp|PNG files|*.png";
            openDialog.CheckFileExists = true;
            openDialog.ReadOnlyChecked = false;
            openDialog.Title = "Open Image File";
            openDialog.InitialDirectory = Path.GetDirectoryName(_filename);
            openDialog.FileName = Path.GetFileName(_filename);
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                bool saveToBmp = false;
                newFilename = openDialog.FileName;

                usePngFile = Settings.Instance.AllowPngImages && Path.GetExtension(openDialog.FileName).Equals(".png", StringComparison.OrdinalIgnoreCase);

                //UIImage will automatically convert to 32 - bit alpha image

                bitmap = null;
                uIImage = AlphaBitmap.TryAlphaBitmapFromFile(newFilename, (_flags & ImageFlags.HighContrast) != 0);

                try
                {
                    bitmap = uIImage.Clone(new Rectangle(new Point(), uIImage.Size), uIImage.PixelFormat);
                    //bitmap = Bitmap.FromHbitmap(uIImage.GetHbitmap()); // this code do not work, because it will not produce a ARGB bitmap

                    if (!Addons.StartsText(_image.Owner.Directory, newFilename))
                    {
                        newFilename = Path.Combine(_image.Owner.Directory, "Res");
                        Addons.ForceDirectories(newFilename);
                        newFilename = Path.Combine(newFilename, Path.GetFileName(openDialog.FileName));

                        if (usePngFile)
                            File.Copy(openDialog.FileName, newFilename, true);
                        else
                            saveToBmp = true;

                        //newFilename = Path.ChangeExtension(newFilename, ".bmp");
                        //bitmap.Save(newFilename, ImageFormat.Bmp); //@ changed, don't override the same file
                        //It seems to be a better solution when we copy the file when it is a .bmp with BM Header than bitmap.Save
                        //If the file from openDialog is the same as newFilename we can delete the openDialog file first
                        //or jump around the bitmap.Save
                        //we have to do some checks if the choosen file has a BM Header
                        //or is it possible for the WindowsRibbon, that we can use png files ?
                    }
                    if (!usePngFile && !Path.GetExtension(newFilename).Equals(".bmp", StringComparison.OrdinalIgnoreCase))
                    {
                        saveToBmp = true;
                        newFilename = Path.ChangeExtension(newFilename, ".bmp");
                    }
                    if (saveToBmp)
                    {
                        AlphaBitmap.SetTransparentRGB(bitmap, Color.LightGray.ToArgb() & 0xffffff);
                        bitmap.Save(newFilename, ImageFormat.Bmp); //@ changed, don't override the same file
                    }

                    EditImageFile.Text = _image.Owner.BuildRelativeFilename(newFilename);
                    _image.Source = EditImageFile.Text;

                    ClearBitmap(_bitmap);
                    Graphics canvas = Graphics.FromImage(_bitmap);
                    if (uIImage.PixelFormat != PixelFormat.Format32bppArgb)
                        uIImage.MakeTransparent();
                    canvas.DrawImage(uIImage, new Point((64 - uIImage.Width) / 2, (64 - uIImage.Height) / 2));
                    canvas.Dispose();
                    PaintBox.Invalidate();

                    size = Math.Max(bitmap.Width, bitmap.Height);
                    if ((_flags & ImageFlags.Large) != 0)
                        size = size / 2;
                    if (size <= 16)
                        ComboBoxMinDpi.SelectedIndex = 0;
                    else if (size <= 20)
                        ComboBoxMinDpi.SelectedIndex = 2;
                    else if (size <= 24)
                        ComboBoxMinDpi.SelectedIndex = 3;
                    else
                        ComboBoxMinDpi.SelectedIndex = 4;
                }
                finally
                {
                    uIImage.Dispose();
                    bitmap.Dispose();
                }
            }
        }

        private void FormClose(object sender, FormClosedEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                _image.Source = EditImageFile.Text;
                switch (ComboBoxMinDpi.SelectedIndex)
                {
                    case 0:
                        _image.MinDpi = 0;
                        break;
                    case 1:
                        _image.MinDpi = 96;
                        break;
                    case 2:
                        _image.MinDpi = 120;
                        break;
                    case 3:
                        _image.MinDpi = 144;
                        break;
                    case 4:
                        _image.MinDpi = 192;
                        break;
                }
                _image.Id = (int)EditResourceId.Value;
                _image.Symbol = EditSymbol.Text;
                MainForm.FormMain.Modified();
            }
        }

        private void PaintBoxPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(_bitmap, new Point(0, 0));
        }

        private void UpdateControls()
        {
            buttonOk.Enabled = File.Exists(_filename);
        }
    }
}
