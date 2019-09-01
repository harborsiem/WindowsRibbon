using System;
using System.Windows.Forms;

using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Interop;
using RibbonLib.Controls.Events;
using System.IO;

namespace _17_QuickAccessToolbar
{
    public enum RibbonMarkupCommands : uint
    {
        cmdButtonNew = 1001,
        cmdButtonOpen = 1002,
        cmdButtonSave = 1003,
        cmdTabMain = 1004,
        cmdGroupFileActions = 1005,
        cmdQAT = 1006,
        cmdCustomizeQAT = 1007,
    }

    public partial class Form1 : Form
    {
        private RibbonButton _buttonNew;
        private RibbonButton _buttonOpen;
        private RibbonButton _buttonSave;
        private RibbonTab _tabMain;
        private RibbonGroup _groupFileActions;
        private RibbonQuickAccessToolbar _ribbonQuickAccessToolbar;

        private Stream _stream;

        public Form1()
        {
            InitializeComponent();

            _buttonNew = new RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonNew);
            _buttonOpen = new RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonOpen);
            _buttonSave = new RibbonButton(_ribbon, (uint)RibbonMarkupCommands.cmdButtonSave);
            _tabMain = new RibbonTab(_ribbon, (uint)RibbonMarkupCommands.cmdTabMain);
            _groupFileActions = new RibbonGroup(_ribbon, (uint)RibbonMarkupCommands.cmdGroupFileActions);
            _ribbonQuickAccessToolbar = new RibbonQuickAccessToolbar(_ribbon,
                                                                     (uint)RibbonMarkupCommands.cmdQAT,
                                                                     (uint)RibbonMarkupCommands.cmdCustomizeQAT);

            _buttonNew.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonNew_ExecuteEvent);
            _buttonSave.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonSave_ExecuteEvent);
            _buttonOpen.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonOpen_ExecuteEvent);
            
            // register to the QAT customize button
            _ribbonQuickAccessToolbar.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_ribbonQuickAccessToolbar_ExecuteEvent);
        }

        void _buttonNew_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            // changing QAT commands list 
            IUICollection itemsSource = _ribbonQuickAccessToolbar.ItemsSource;
            itemsSource.Clear();
            itemsSource.Add(new GalleryCommandPropertySet() { CommandID = (uint)RibbonMarkupCommands.cmdButtonNew });
            itemsSource.Add(new GalleryCommandPropertySet() { CommandID = (uint)RibbonMarkupCommands.cmdButtonOpen });
            itemsSource.Add(new GalleryCommandPropertySet() { CommandID = (uint)RibbonMarkupCommands.cmdButtonSave });
        }

        void _buttonSave_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            // save ribbon QAT settings 
            _stream = new MemoryStream();
            _ribbon.SaveSettingsToStream(_stream);
        }

        void _buttonOpen_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            if (_stream == null)
            {
                return;
            }

            // load ribbon QAT settings 
            _stream.Position = 0;
            _ribbon.LoadSettingsFromStream(_stream);
        }

        void _ribbonQuickAccessToolbar_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            MessageBox.Show("Open customize commands dialog..");
        }
    }
}
