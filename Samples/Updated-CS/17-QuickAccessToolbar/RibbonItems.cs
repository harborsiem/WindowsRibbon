//#define Old
using System;
using System.IO;
using System.Windows.Forms;
using RibbonLib;
using RibbonLib.Controls;
using RibbonLib.Interop;
using RibbonLib.Controls.Events;


namespace RibbonLib.Controls
{
    partial class RibbonItems
    {
        private Stream _stream;

        public void Init()
        {
            ButtonNew.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonNew_ExecuteEvent);
            ButtonSave.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonSave_ExecuteEvent);
            ButtonOpen.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_buttonOpen_ExecuteEvent);

            // register to the QAT customize button
            QAT.ExecuteEvent += new EventHandler<ExecuteEventArgs>(_ribbonQuickAccessToolbar_ExecuteEvent);
        }

        void _buttonNew_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            // changing QAT commands list 
#if Old
            IUICollection itemsSource = QAT.ItemsSource;
#else
            UICollection<QatCommandPropertySet> itemsSource = QAT.QatItemsSource;
#endif
            itemsSource.Clear();
            itemsSource.Add(new QatCommandPropertySet() { CommandID = (uint)Cmd.cmdButtonNew });
            itemsSource.Add(new QatCommandPropertySet() { CommandID = (uint)Cmd.cmdButtonOpen });
            itemsSource.Add(new QatCommandPropertySet() { CommandID = (uint)Cmd.cmdButtonSave });
        }

        void _buttonSave_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            // save ribbon QAT settings 
            _stream = new MemoryStream();
            Ribbon.SaveSettingsToStream(_stream);
        }

        void _buttonOpen_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            if (_stream == null)
            {
                return;
            }

            // load ribbon QAT settings 
            _stream.Position = 0;
            Ribbon.LoadSettingsFromStream(_stream);
        }

        void _ribbonQuickAccessToolbar_ExecuteEvent(object sender, ExecuteEventArgs e)
        {
            MessageBox.Show("Open customize commands dialog..");
        }

        public void Load()
        {
        }

    }
}
