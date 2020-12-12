using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using WinForms.Actions;

namespace UIRibbonTools
{
    partial class ImageListFrame : UserControl
    {
        //resourcestring
        private const string RS_NEED_SAVE_HEADER = "Document must be saved";
        private static readonly string RS_NEED_SAVE_MESSAGE = "The document needs to be saved before you can add images." + Environment.NewLine +
            "Do you want to save the document now?";
        //  const string RS_REMOVE_IMAGE_HEADER == "Remove Image?";
        //  const string RS_REMOVE_IMAGE_MESSAGE == "Do you want to remove this image (this cannot be undone)?";

        private TRibbonCommand _command;
        private TRibbonList<TRibbonImage> _images;
        private ImageFlags _flags;
        private ImageList _imageListToolbars;
        private TActionList _actionList;
        private TAction _actionAddImage;
        private TAction _actionRemoveImage;
        private TAction _actionRemoveAllImages;
        private TAction _actionEditImage;
        private TAction _actionAddMultiple;

        public ImageListFrame()
        {
#if Core
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
#endif
            InitializeComponent();
            if (components == null)
                components = new Container();
            _imageList.TransparentColor = Color.Transparent;
            listView.SmallImageList = _imageList;
            _imageListToolbars = ImageManager.ImageListToolbars_ImageList(components);
            toolButtonAddImage.DropDown.ImageList = _imageListToolbars;
            toolBarImages.ImageList = _imageListToolbars;
            InitEvents();
            InitActions(components);
        }

        private void InitEvents()
        {
            listView.DoubleClick += ListViewDblClick;
            listView.ItemSelectionChanged += ListViewSelectItem;
        }

        private void InitActions(IContainer components)
        {
            _actionList = new TActionList(components);

            _actionAddImage = new TAction(components);
            _actionRemoveImage = new TAction(components);
            _actionRemoveAllImages = new TAction(components);
            _actionEditImage = new TAction(components);
            _actionAddMultiple = new TAction(components);

            _actionList.Actions.AddRange(new TAction[] {
                _actionAddImage,
                _actionRemoveImage,
                _actionRemoveAllImages,
                _actionEditImage,
                _actionAddMultiple
            });

            _actionAddImage.Execute += ActionAddImageExecute;
            _actionAddImage.Enabled = false;
            _actionAddImage.Hint = "Adds a new image";
            _actionAddImage.ImageIndex = 0;
            _actionAddImage.Text = "Add";
            _actionAddImage.SetComponent(toolButtonAddImage, true);
            _actionAddImage.SetComponent(popupAdd, true);
            //_actionAddCommand.ShowTextOnToolBar = false;

            _actionRemoveImage.Execute += ActionRemoveImageExecute;
            _actionRemoveImage.Enabled = false;
            _actionRemoveImage.Hint = "Remove the selected image";
            _actionRemoveImage.ImageIndex = 1;
            _actionRemoveImage.Text = "Remove";
            _actionRemoveImage.SetComponent(toolButtonRemoveImage, true);

            _actionRemoveAllImages.Execute += ActionRemoveAllImagesExecute;
            //_actionRemoveAllImages.ImageIndex = 1;
            _actionRemoveAllImages.Text = "Remove All";
            _actionRemoveAllImages.SetComponent(toolButtonRemoveAllImages, true);

            _actionEditImage.Execute += ActionEditImageExecute;
            _actionEditImage.Enabled = false;
            _actionEditImage.Hint = "Edit the selected image";
            _actionEditImage.ImageIndex = 2;
            _actionEditImage.Text = "Edit";
            _actionEditImage.SetComponent(toolButtonEditImage, true);

            _actionAddMultiple.Execute += ActionAddMultipleExecute;
            _actionAddMultiple.Hint = "Add multiple images with different resolutions";
            //_actionAddMultiple.ImageIndex = 2;
            _actionAddMultiple.Text = "Add Multiple Images";
            _actionAddMultiple.SetComponent(popupAddMultiple, true);

            _actionList.ImageList = _imageListToolbars;
        }

        private void ActionAddImageExecute(object sender, EventArgs e)
        {
            TRibbonImage image;
            ImageEditForm dialog;
            ListViewItem item;

            if (string.IsNullOrEmpty(_command.Owner.Filename))
            {
                if (MessageBox.Show(RS_NEED_SAVE_MESSAGE, RS_NEED_SAVE_HEADER, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    == DialogResult.No)
                    return;

                MainForm.FormMain._actionSaveAs.PerformClick();
                if (string.IsNullOrEmpty(_command.Owner.Filename))
                    return;
            }

            if ((ImageFlags.Large & _flags) != 0)
                if ((ImageFlags.HighContrast & _flags) != 0)
                    image = _command.AddLargeHighContrastImage();
                else
                    image = _command.AddLargeImage();
            else if ((ImageFlags.HighContrast & _flags) != 0)
                image = _command.AddSmallHighContrastImage();
            else
                image = _command.AddSmallImage();

            dialog = new ImageEditForm(image, _flags);
            try
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    item = listView.Items.Add(new ListViewItem());
                    SetImageItem(item, image);
                    listView.Items[item.Index].Selected = true;
                    item.Focused = true;
                }
                else
                    _command.RemoveImage(image);
            }
            finally
            {
                dialog.Close();
            }
        }

        private void ActionAddMultipleExecute(object sender, EventArgs e)
        {
            string filename;
            bool usePngFile;
            Bitmap bitmap;
            Bitmap uIImage;
            TRibbonImage image;
            int size;
            ListViewItem item;

            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "BMP and PNG files|*.bmp;*.png|BMP files|*.bmp|PNG files|*.png";
            openDialog.Title = "Open Image File";
            openDialog.CheckFileExists = true;
            openDialog.ReadOnlyChecked = false;
            openDialog.Multiselect = true;
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string s in openDialog.FileNames)
                {
                    bool saveToBmp = false;
                    filename = s;

                    if ((ImageFlags.Large & _flags) != 0)
                        if ((ImageFlags.HighContrast & _flags) != 0)
                            image = _command.AddLargeHighContrastImage();
                        else
                            image = _command.AddLargeImage();
                    else if ((ImageFlags.HighContrast & _flags) != 0)
                        image = _command.AddSmallHighContrastImage();
                    else
                        image = _command.AddSmallImage();

                    usePngFile = Settings.Instance.AllowPngImages && Path.GetExtension(s).Equals(".png", StringComparison.OrdinalIgnoreCase);

                    // UIImage will automatically convert to 32 - bit alpha image
                    bitmap = null;
                    uIImage = AlphaBitmap.BitmapFromFile(filename, (ImageFlags.HighContrast & _flags) != 0);
                    try
                    {
                        bitmap = uIImage.Clone(new Rectangle(new Point(), uIImage.Size), uIImage.PixelFormat);

                        if (!Addons.StartsText(image.Owner.Directory, filename))
                        {
                            filename = Path.Combine(image.Owner.Directory, "Res");
                            Addons.ForceDirectories(filename);
                            filename = Path.Combine(filename, Path.GetFileName(s));

                            if (usePngFile)
                                File.Copy(s, filename, true);
                            else
                                saveToBmp = true;
                        }
                        if (!usePngFile && !Path.GetExtension(filename).Equals("bmp", StringComparison.OrdinalIgnoreCase)) {
                            saveToBmp = true;
                            filename = Path.ChangeExtension(filename, ".bmp");
                        }
                        if (saveToBmp)
                        {
                            bitmap.Save(filename, ImageFormat.Bmp); //@ changed, don't override the same file
                        }
                        image.Source = image.Owner.BuildRelativeFilename(filename);

                        size = Math.Max(bitmap.Width, bitmap.Height);
                        if ((ImageFlags.Large & _flags) != 0)
                            size = size / 2;
                        if (size <= 16)
                            image.MinDpi = 0;
                        else if (size <= 20)
                            image.MinDpi = 120;
                        else if (size <= 24)
                            image.MinDpi = 144;
                        else
                            image.MinDpi = 192;
                    }
                    finally
                    {
                        uIImage.Dispose();
                        bitmap.Dispose();
                    }

                    item = listView.Items.Add(new ListViewItem());
                    SetImageItem(item, image);
                    listView.Items[item.Index].Selected = true;
                    item.Focused = true;
                    MainForm.FormMain.Modified();
                }
            }
        }

        private void ActionRemoveImageExecute(object sender, EventArgs e)
        {
            TRibbonImage image;

            //if (listView.SelectedItems.Count > 0 && MessageBox.Show(RS_REMOVE_IMAGE_MESSAGE, RS_REMOVE_IMAGE_HEADER, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            //    == DialogResult.Yes)
            //  if Assigned(ListView.Selected) && (TaskMessageDlg(RS_REMOVE_IMAGE_HEADER,
            //    RS_REMOVE_IMAGE_MESSAGE, mtConfirmation, [mbYes, mbNo], 0, mbYes) == mrYes)
            //  
            {
                if (listView.SelectedItems.Count > 0)
                {
                    image = (TRibbonImage)listView.SelectedItems[0].Tag;
                    _command.RemoveImage(image);
                    listView.SelectedItems[0].Remove();
                    MainForm.FormMain.Modified();
                }
            }
        }

        private void ActionRemoveAllImagesExecute(object sender, EventArgs e)
        {
            TRibbonImage image;

            if (listView.Items.Count == 0)
                return;

            for (int i = listView.Items.Count - 1; i >= 0; i--)
            {
                image = (TRibbonImage)listView.Items[i].Tag;
                _command.RemoveImage(image);
            }

            listView.Items.Clear();
            MainForm.FormMain.Modified();
        }

        private void ActionEditImageExecute(object sender, EventArgs e)
        {
            TRibbonImage image;
            ImageEditForm dialog;

            if (listView.SelectedItems.Count > 0 && listView.SelectedItems[0] != null)
                image = (TRibbonImage)listView.SelectedItems[0].Tag;
            else
                return;

            if (image == null)
                return;

            dialog = new ImageEditForm(image, _flags);
            try
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                    SetImageItem(listView.SelectedItems[0], image);
            }
            finally
            {
                dialog.Close();
            }
        }

        private void ListViewDblClick(object sender, EventArgs e)
        {
            if (_actionEditImage.Enabled)
                _actionEditImage.PerformClick();
        }

        private void ListViewSelectItem(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            _actionRemoveImage.Enabled = (e.Item != null) && e.IsSelected;
            _actionEditImage.Enabled = (e.Item != null) && e.IsSelected;
        }

        private void SetImageItem(ListViewItem item,
            TRibbonImage image)
        {
            string filename;
            Bitmap uIImage;
            int imageIndex;
            Bitmap bitmap;

            filename = image.Owner.BuildAbsoluteFilename(image.Source);
            if (File.Exists(filename))
            {
                uIImage = AlphaBitmap.BitmapFromFile(filename);
                try
                {
                    if ((uIImage.Width == _imageList.ImageSize.Width) && (uIImage.Height == _imageList.ImageSize.Height))
                    {
                        bitmap = new Bitmap(_imageList.ImageSize.Width, _imageList.ImageSize.Height, PixelFormat.Format32bppArgb);
                        Graphics canvas = Graphics.FromImage(bitmap);
                        canvas.DrawImage(uIImage, 0, 0, _imageList.ImageSize.Width, _imageList.ImageSize.Height);
                        canvas.Dispose();
                        imageIndex = _imageList.Images.Add(bitmap, Color.Transparent);
                    }
                    else
                    {
                        bitmap = new Bitmap(_imageList.ImageSize.Width, _imageList.ImageSize.Height, PixelFormat.Format32bppArgb);
                        try
                        {
                            if ((uIImage.Width <= _imageList.ImageSize.Width) && (uIImage.Height <= _imageList.ImageSize.Height))
                            {
                                Graphics canvas = Graphics.FromImage(bitmap);
                                canvas.DrawImage(uIImage, (_imageList.ImageSize.Width - uIImage.Width) / 2,
                                (_imageList.ImageSize.Height - uIImage.Height) / 2);
                                canvas.Dispose();
                            }
                            else
                            {
                                Graphics canvas = Graphics.FromImage(bitmap);
                                canvas.DrawImage(uIImage, 0, 0, _imageList.ImageSize.Width, _imageList.ImageSize.Height);
                                canvas.Dispose();
                            }
                            imageIndex = _imageList.Images.Add(bitmap, Color.Transparent);
                        }
                        finally
                        {
                            bitmap.Dispose();
                        }
                    }
                }
                finally
                {
                    uIImage.Dispose();
                }
            }
            else
                imageIndex = -1;

            item.ImageIndex = imageIndex;
            item.SubItems.Clear();
            item.SubItems.Add(image.MinDpi.ToString());
            item.SubItems.Add(image.Id.ToString());
            item.SubItems.Add(image.Symbol);
            item.SubItems.Add(image.Source);
            item.Tag = image;
        }

        public void ShowImages(TRibbonCommand command,
            ImageFlags flags)
        {
            ListViewItem item;

            _images = null;
            _command = command;
            _flags = flags;
            if (_command != null)
            {
                if ((ImageFlags.Large & flags) != 0)
                    if ((ImageFlags.HighContrast & flags) != 0)
                        _images = _command.LargeHighContrastImages;
                    else
                        _images = _command.LargeImages;
                else if ((ImageFlags.HighContrast & flags) != 0)
                    _images = _command.SmallHighContrastImages;
                else
                    _images = _command.SmallImages;
            }
            listView.BeginUpdate();
            try
            {
                listView.Items.Clear();
                _imageList.Images.Clear();

                _actionAddImage.Enabled = (_images != null);
                _actionRemoveImage.Enabled = false;
                _actionEditImage.Enabled = false;

                if (_images == null)
                    return;

                foreach (TRibbonImage image in _images)
                {
                    item = listView.Items.Add(new ListViewItem());
                    SetImageItem(item, image);
                }

                if (listView.Items.Count > 0)
                    listView.Items[0].Selected = true;
            }
            finally
            {
                listView.EndUpdate();
            }
        }
    }
}
