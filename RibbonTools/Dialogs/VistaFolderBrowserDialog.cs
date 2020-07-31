using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;

namespace System.Windows.Forms
{
    /// <summary>
    /// Stellt einen Auswahldialog für Ordner und Systemelemente ab Windows Vista bereit.
    /// </summary>
    public sealed class VistaFolderBrowserDialog : CommonDialog
    {
        private Environment.SpecialFolder _rootFolder;
        private string _descriptionText;
        private string _selectedPath;

        /// <summary>
        ///  Initializes a new instance of the <see cref='FolderBrowserDialog'/> class.
        /// </summary>
        public VistaFolderBrowserDialog()
        {
            Reset();
        }

        #region Properties

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler HelpRequest
        {
            add => base.HelpRequest += value;
            remove => base.HelpRequest -= value;
        }

        [Browsable(true)]
        [DefaultValue(true)]
        public bool ShowNewFolderButton { get; set; }

        /// <summary>
        /// Ruft den ausgewählten Ordnerpfad ab bzw. legt diesen fest.
        /// </summary>
        [Browsable(true)]
        [DefaultValue("")]
        public string SelectedPath
        {
            get { return _selectedPath; }
            set { _selectedPath = value ?? string.Empty; }
        }

        [Browsable(true)]
        [DefaultValue(Environment.SpecialFolder.Desktop)]
        public Environment.SpecialFolder RootFolder
        {
            get => _rootFolder;
            set
            {
                if (!Enum.IsDefined(typeof(Environment.SpecialFolder), value))
                {
                    throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(Environment.SpecialFolder));
                }

                _rootFolder = value;
            }
        }

        /// <summary>
        /// Ruft den Dialog Titel ab bzw. legt diesen fest.
        /// </summary>
        public string Description
        {
            get { return _descriptionText; }
            set { _descriptionText = value ?? string.Empty; }
        }

        [Browsable(true)]
        [DefaultValue(false)]
        public bool UseDescriptionForTitle { get; set; }

        /// <summary>
        /// Ruft einen Wert ab der angibt ob auch Elemente ausgewählt werden können, die keine Ordner sind oder legt diesen fest.
        /// </summary>
        //public bool AllowNonStoragePlaces { get; set; }

        #endregion

        #region Public Methods

        public override void Reset()
        {
            _rootFolder = Environment.SpecialFolder.Desktop;
            _descriptionText = string.Empty;
            _selectedPath = string.Empty;
            ShowNewFolderButton = true;
        }

        #endregion

        /// <summary>
        /// Zeigt den Auswahldialog an.
        /// </summary>
        /// <param name="hWndOwner">Der Besitzer des Fensters</param>
        /// <returns><c>true</c> wenn der Benutzer die Ordnerauswahl bestätigte; andernfalls <c>false</c></returns>
        protected override bool RunDialog(IntPtr hWndOwner)
        {
            if (Environment.OSVersion.Version.Major < 6)
            {
                throw new InvalidOperationException("The dialog need at least Windows Vista to work.");
            }

            if (TryRunDialogVista(hWndOwner, out bool returnValue))
                return returnValue;

            return false;
        }

        private bool TryRunDialogVista(IntPtr owner, out bool returnValue)
        {
            FileDialogNative.NativeFileOpenDialog dialog;
            try
            {
                // Creating the Vista dialog can fail on Windows Server Core, even if the
                // Server Core App Compatibility FOD is installed.
                dialog = new FileDialogNative.NativeFileOpenDialog();
            }
            catch (COMException)
            {
                returnValue = false;
                return false;
            }

            try
            {
                SetDialogProperties(dialog);
                int result = dialog.Show(owner);
                if (result < 0)
                {
                    if ((HRESULT)result == HRESULT.ERROR_CANCELLED)
                    {
                        returnValue = false;
                        return true;
                    }
                    else
                    {
                        throw Marshal.GetExceptionForHR(result);
                    }
                }

                GetResult(dialog);
                returnValue = true;
                return true;
            }
            finally
            {
                if (dialog != null)
                {
                    Marshal.FinalReleaseComObject(dialog);
                }
            }
        }

        private void SetDialogProperties(FileDialogNative.IFileDialog dialog)
        {
            // Description
            if (!string.IsNullOrEmpty(_descriptionText))
            {
                if (UseDescriptionForTitle)
                {
                    dialog.SetTitle(_descriptionText);
                }
                else
                {
                    FileDialogNative.IFileDialogCustomize customize = (FileDialogNative.IFileDialogCustomize)dialog;
                    customize.AddText(0, _descriptionText);
                }
            }

            dialog.SetOptions(FOS.PICKFOLDERS | FOS.FORCEFILESYSTEM | FOS.FILEMUSTEXIST);

            string parent;
            string folder = string.Empty;

            if (!string.IsNullOrEmpty(_selectedPath))
            {
                parent = Path.GetDirectoryName(_selectedPath);
                if (parent == null || !Directory.Exists(parent))
                {
                    if (Directory.Exists(_selectedPath))
                    {
                        folder = Path.GetFileName(_selectedPath);
                        parent = _selectedPath;
                    }
                    else
                        parent = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer); //not existing drive name   
                }
                else
                {
                    folder = Path.GetFileName(_selectedPath);
                }
            }
            else
            {
                parent = Environment.GetFolderPath(RootFolder);
            }
            dialog.SetFolder(FileDialogNative.CreateItemFromParsingName(parent));
            dialog.SetFileName(folder);

            //if (!string.IsNullOrEmpty(_selectedPath))
            //{
            //    string parent = Path.GetDirectoryName(_selectedPath);
            //    if (parent == null || !Directory.Exists(parent))
            //    {
            //        dialog.SetFileName(_selectedPath);
            //    }
            //    else
            //    {
            //        string folder = Path.GetFileName(_selectedPath);
            //        dialog.SetFolder(FileDialogNative.CreateItemFromParsingName(parent));
            //        dialog.SetFileName(folder);
            //    }
            //}
        }

        private void GetResult(FileDialogNative.IFileDialog dialog)
        {
            dialog.GetResult(out FileDialogNative.IShellItem item);
            item.GetDisplayName(SIGDN.FILESYSPATH, out _selectedPath);
        }
    }
}
