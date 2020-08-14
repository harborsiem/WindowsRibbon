using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms.Actions;

namespace UIRibbonTools
{
    partial class CommandsFrame : UserControl
    {
        const string DefaultCommandNameAtBeginning = "cmd_"; //"Command"; //@ added

        private const string RS_REMOVE_COMMAND_HEADER = "Remove command?";
        private static readonly string RS_REMOVE_COMMAND_MESSAGE = "There are {0:d} control(s) that reference this command." + Environment.NewLine +
          "If you remove this command, those controls may become unusable. " + Environment.NewLine +
          "Do you want to remove this command (this cannot be undone)?";

        private TRibbonDocument _document;
        private TRibbonCommand _command;
        private bool _updating;
        //private ImageListFrame _smallImagesFrame;
        //private ImageListFrame _largeImagesFrame;
        //private ImageListFrame _smallHCImagesFrame;
        //private ImageListFrame _largeHCImagesFrame;
        private int _newCommandIndex;
        private TActionList _actionList;
        private TAction _actionAddCommand;
        private TAction _actionRemoveCommand;
        private TAction _actionMoveUp;
        private TAction _actionMoveDown;
        private TAction _actionSearchCommand;
        private ImageList _imageListToolbars;
        private ListViewColumnSorter _listViewColumnSorter;
        private Timer _listViewTimer; //@ added

        public CommandsFrame()
        {
#if Core
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
#endif
            InitializeComponent();
            if (components == null)
                components = new Container();

            _listViewColumnSorter = new ListViewColumnSorter();
            _listViewTimer = new Timer(); //@ added, because we don't want unEnabled the right site after each selection change
            _listViewTimer.Interval = 100;

            bool runtime = (LicenseManager.UsageMode == LicenseUsageMode.Runtime);
            if (runtime)
                InitAddon();
        }

        public void SetBoldFonts()
        {
            labelProperty.Font = new Font(labelProperty.Font, FontStyle.Bold);
            labelValue.Font = new Font(labelValue.Font, FontStyle.Bold);
            labelID.Font = new Font(labelID.Font, FontStyle.Bold);
            labelSymbol.Font = new Font(labelSymbol.Font, FontStyle.Bold);
            labelSmallImages.Font = new Font(labelSmallImages.Font, FontStyle.Bold);
            labelLargeImages.Font = new Font(labelLargeImages.Font, FontStyle.Bold);
            labelSmallHCImages.Font = new Font(labelSmallHCImages.Font, FontStyle.Bold);
            labelLargeHCImages.Font = new Font(labelLargeHCImages.Font, FontStyle.Bold);
            LabelHeader.Font = new Font(LabelHeader.Font, FontStyle.Bold);
        }

        public void SetFonts(Font font)
        {
            this.Font = font;
            _smallImagesFrame.Font = font;
            _largeImagesFrame.Font = font;
            _smallHCImagesFrame.Font = font;
            _largeHCImagesFrame.Font = font;
        }

        private void InitActions()
        {
            _actionList = new TActionList(components);

            _actionAddCommand = new TAction(components);
            _actionRemoveCommand = new TAction(components);
            _actionMoveUp = new TAction(components);
            _actionMoveDown = new TAction(components);
            _actionSearchCommand = new TAction(components);

            _actionList.Actions.AddRange(new TAction[] {
                _actionAddCommand,
                _actionRemoveCommand,
                _actionMoveUp,
                _actionMoveDown,
                _actionSearchCommand
            });

            _actionAddCommand.Execute += ActionAddCommandExecute;
            _actionAddCommand.Update += ActionAddCommandUpdate;
            _actionAddCommand.Hint = "Adds a new command";
            _actionAddCommand.ImageIndex = 0;
            _actionAddCommand.ShortcutKeys = Keys.Shift | Keys.Control | Keys.Insert;
            _actionAddCommand.Text = "Add";
            _actionAddCommand.SetComponent(toolButtonAddCommand, true);
            _actionAddCommand.SetComponent(menuAddCommand, true);
            //_actionAddCommand.ShowTextOnToolBar = false;

            _actionRemoveCommand.Execute += ActionRemoveCommandExecute;
            _actionRemoveCommand.Update += ActionUpdate;
            _actionRemoveCommand.Hint = "Removes the selected command";
            _actionRemoveCommand.ImageIndex = 1;
            _actionRemoveCommand.Text = "Remove";
            _actionRemoveCommand.SetComponent(toolButtonRemoveCommand, true);
            _actionRemoveCommand.SetComponent(menuRemoveCommand, true);

            _actionMoveUp.Execute += ActionMoveUpExecute;
            _actionMoveUp.Update += ActionUpdateUp;
            _actionMoveUp.Hint = "Moves the selected command up in the list";
            _actionMoveUp.ImageIndex = 2;
            _actionMoveUp.Text = "Up";
            _actionMoveUp.SetComponent(toolButtonMoveUp, true);
            _actionMoveUp.SetComponent(menuMoveUp, true);

            _actionMoveDown.Execute += ActionMoveDownExecute;
            _actionMoveDown.Update += ActionUpdateDown;
            _actionMoveDown.Hint = "Moves the selected command down in the list";
            _actionMoveDown.ImageIndex = 3;
            _actionMoveDown.Text = "Down";
            _actionMoveDown.SetComponent(toolButtonMoveDown, true);
            _actionMoveDown.SetComponent(menuMoveDown, true);

            _actionSearchCommand.Execute += ActionSearchCommandExecute;
            _actionSearchCommand.Update += ActionUpdate;
            _actionSearchCommand.Hint = string.Empty;
            _actionSearchCommand.ImageIndex = 4;
            _actionSearchCommand.ShortcutKeys = Keys.Shift | Keys.F;
            _actionSearchCommand.Text = "Search";
            _actionSearchCommand.SetComponent(toolButtonSearchCommand, true);

            _actionList.ImageList = _imageListToolbars;
        }

        private void InitAddon()
        {
            _imageListToolbars = ImageManager.ImageListToolbars_Commands(components);
            toolBarCommands.ImageList = _imageListToolbars;
            popupMenuList.ImageList = _imageListToolbars;

            InitActions();
            InitEvents();
            InitToolTips();
        }

        private void InitEvents()
        {
            ListViewCommands.ColumnClick += ListViewCommandsColumnClick;
            ListViewCommands.ItemSelectionChanged += ListViewCommandsSelectItem;
            _listViewTimer.Tick += ListViewTimer_Tick;
            EditName.TextChanged += EditNameChange;
            EditName.KeyPress += EditNameKeyPress;
            EditId.TextChanged += EditIdChange;
            EditId.ValueChanged += UpDownChanging;
            EditSymbol.TextChanged += EditSymbolChange;
            EditSymbol.KeyPress += EditNameKeyPress;

            EditCaption.TextChanged += EditCaptionChange;
            EditCaptionId.TextChanged += EditCaptionIdChange;
            EditCaptionId.ValueChanged += UpDownChanging;
            EditCaptionSymbol.TextChanged += EditCaptionSymbolChange;

            EditDescription.TextChanged += EditDescriptionChange;
            EditDescriptionId.TextChanged += EditDescriptionIdChange;
            EditDescriptionId.ValueChanged += UpDownChanging;
            EditDescriptionSymbol.TextChanged += EditDescriptionSymbolChange;

            EditTooltipTitle.TextChanged += EditTooltipTitleChange;
            EditTooltipTitleId.TextChanged += EditTooltipTitleIdChange;
            EditTooltipTitleId.ValueChanged += UpDownChanging;
            EditTooltipTitleSymbol.TextChanged += EditTooltipTitleSymbolChange;

            EditTooltipDescription.TextChanged += EditTooltipDescriptionChange;
            EditTooltipDescriptionId.TextChanged += EditTooltipDescriptionIdChange;
            EditTooltipDescriptionId.ValueChanged += UpDownChanging;
            EditTooltipDescriptionSymbol.TextChanged += EditTooltipDescriptionSymbolChange;

            EditKeytip.TextChanged += EditKeytipChange;
            EditKeytipId.TextChanged += EditKeytipIdChange;
            EditKeytipId.ValueChanged += UpDownChanging;
            EditKeytipSymbol.TextChanged += EditKeyTipSymbolChange;

            EditComment.TextChanged += EditCommentChange;
        }

        private void InitToolTips()
        {
            ToolTip commandsTip = new ToolTip(components);
            commandsTip.SetToolTip(EditName,
                "The command name is used to connect commands with controls." + Environment.NewLine +
                "Unless you specify a Symbol name, this name is also used" + Environment.NewLine +
                "as the name of the constant for this command.");
            commandsTip.IsBalloon = false;
            //commandsTip.Popup += CommandsTip_Popup;
            commandsTip.SetToolTip(EditId,
                "A unique numeric identifier for the command (the value of" + Environment.NewLine +
                "the Symbol constant). Use 0 for auto-generated identifiers.");
            commandsTip.SetToolTip(EditSymbol,
                "This is the name of the constant that will be generated to access" + Environment.NewLine +
                "this command. If not specified, the command Name is used.");
            commandsTip.SetToolTip(EditCaption, "The caption/label title for the command.");
            commandsTip.SetToolTip(EditCaptionId,
                "Numeric resource string identifier for the caption." + Environment.NewLine +
                "Use 0 for auto-generated identifiers.");
            commandsTip.SetToolTip(EditCaptionSymbol,
                "Constant name for the resource identifier." + Environment.NewLine +
                "If not specified, it is automatically generated.");
            commandsTip.SetToolTip(EditDescription,
                "The label description for the command." + Environment.NewLine +
                "Is used when the command is displayed in the application menu.");
            commandsTip.SetToolTip(EditDescriptionId,
                "Numeric resource string identifier for the description." + Environment.NewLine +
                "Use 0 for auto-generated identifiers.");
            commandsTip.SetToolTip(EditDescriptionSymbol,
                "Constant name for the resource identifier." + Environment.NewLine +
                "If not specified, it is automatically generated.");
            commandsTip.SetToolTip(EditTooltipTitle,
                "The tooltip title for the command." + Environment.NewLine +
                "(This is the bold caption of the tooltip)");
            commandsTip.SetToolTip(EditTooltipTitleId,
                "Numeric resource string identifier for the tooltip title." + Environment.NewLine +
                "Use 0 for auto-generated identifiers.");
            commandsTip.SetToolTip(EditTooltipTitleSymbol,
                "Constant name for the resource identifier." + Environment.NewLine +
                "If not specified, it is automatically generated.");
            commandsTip.SetToolTip(EditTooltipDescription,
                "The tooltip description for the command." + Environment.NewLine +
                "(Is displayed below the tooltip title in the tooltip popup)");
            commandsTip.SetToolTip(EditTooltipDescriptionId,
                "Numeric resource string identifier for the tooltip description." + Environment.NewLine +
                "Use 0 for auto-generated identifiers.");
            commandsTip.SetToolTip(EditTooltipDescriptionSymbol,
                "Constant name for the resource identifier." + Environment.NewLine +
                "If not specified, it is automatically generated.");
            commandsTip.SetToolTip(EditKeytip,
                "The keytip for the command. This is key sequence that is shown" + Environment.NewLine +
                "when the user pressed the Alt key to access ribbon controls.");
            commandsTip.SetToolTip(EditKeytipId,
                "Numeric resource string identifier for the keytip." + Environment.NewLine +
                "Use 0 for auto-generated identifiers.");
            commandsTip.SetToolTip(EditKeytipSymbol,
                "Constant name for the resource identifier." + Environment.NewLine +
                "If not specified, it is automatically generated.");
            commandsTip.SetToolTip(EditComment,
                "This text is placed as a comment in the *.h file" + Environment.NewLine +
                "containing the constant for this command.");
        }

        private void CommandsTip_Popup(object sender, PopupEventArgs e)
        {
            ToolTip tip = sender as ToolTip;
            string text;
            if (tip != null)
            {
                text = tip.GetToolTip(e.AssociatedControl);
            }
        }

        private void ActionAddCommandExecute(object sender, EventArgs e)
        {
            TRibbonCommand command;

            _newCommandIndex++;
            command = _document.Application.AddCommand(DefaultCommandNameAtBeginning + (_newCommandIndex.ToString())); //@ changed
            ListViewItem item = AddCommand(command);
            ListViewCommands.Items[item.Index].Selected = true;
            ListViewCommands.Items[item.Index].Focused = true;
            ListViewCommands.Items[item.Index].EnsureVisible();
            EditName.Select();
            BtnGenerateIDClick(sender, EventArgs.Empty);
            Modified();
        }

        private void ActionUpdateUp(object sender, EventArgs e) //@ added, bugfix
        {
            TAction action = sender as TAction;
            if (action != null)
            {
                action.Enabled = (ListViewCommands.SelectedItems.Count > 0 && (ListViewCommands.SelectedItems[0]) != null
                    && ListViewCommands.SelectedItems[0].Index != 0);
            }
        }

        private void ActionUpdateDown(object sender, EventArgs e) //@ added, bugfix
        {
            TAction action = sender as TAction;
            if (action != null)
            {
                action.Enabled = (ListViewCommands.SelectedItems.Count > 0 && (ListViewCommands.SelectedItems[0]) != null
                    && ListViewCommands.SelectedItems[0].Index != ListViewCommands.Items.Count - 1);
            }
        }

        private void ActionUpdate(object sender, EventArgs e)
        {
            (sender as TAction).Enabled = (ListViewCommands.SelectedItems.Count > 0 && (ListViewCommands.SelectedItems[0]) != null);
        }

        private void ActionAddCommandUpdate(object sender, EventArgs e)
        {
            (sender as TAction).Enabled = (_document != null);
        }

        private void ActionRemoveCommandExecute(object sender, EventArgs e)
        {
            if (_command != null && ((_command.ReferenceCount == 0) ||
              (MessageBox.Show(string.Format(RS_REMOVE_COMMAND_MESSAGE, _command.ReferenceCount), RS_REMOVE_COMMAND_HEADER,
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)))
            {
                if (ListViewCommands.SelectedItems.Count > 0)
                {
                    _document.Application.RemoveCommand(_command);
                    ListViewCommands.SelectedItems[0].Remove();
                    ShowSelection();
                    Modified();
                }
            }
        }

        private void ActionMoveDownExecute(object sender, EventArgs e)
        {
            MoveCommand(1);
        }

        private void ActionMoveUpExecute(object sender, EventArgs e)
        {
            MoveCommand(-1);
        }

        private void ActionSearchCommandExecute(object sender, EventArgs e)
        {
            CommandSearchForm commandSearchForm;

            commandSearchForm = new CommandSearchForm(this, ListViewCommands);
            try
            {
                if (commandSearchForm.ShowDialog(FindForm()) == DialogResult.OK)
                {
                    if (commandSearchForm.ListViewCommands.SelectedItems.Count == 0 || commandSearchForm.ListViewCommands.SelectedItems[0] == null)
                        return;
                    foreach (ListViewItem item in this.ListViewCommands.Items)
                    {
                        if (item.Tag == commandSearchForm.ListViewCommands.SelectedItems[0].Tag)
                            this.ListViewCommands.Items[item.Index].Selected = true;
                        this.ListViewCommands.SelectedItems[0].EnsureVisible();
                    }
                }
            }
            finally
            {
                commandSearchForm.Close();
            }
        }

        private bool ShortCutKeys(ref Message msg, Keys keyData) //@ added
        {
            bool result = false;
            if (msg.Msg == NativeMethods.WM_KEYDOWN)
            {
                switch (keyData)
                {
                    //ShortcutKeys for ToolStripButton
                    case (Keys.Shift | Keys.Control | Keys.Insert):
                        if (_actionAddCommand.Enabled)
                        {
                            _actionAddCommand.DoExecute();
                            result = true;
                        }
                        break;
                    case (Keys.Control | Keys.F):
                        if (_actionSearchCommand.Enabled)
                        {
                            _actionSearchCommand.DoExecute();
                            result = true;
                        }
                        break;
                    //Remove
                    case (Keys.Control | Keys.Delete):
                        if (_actionRemoveCommand.Enabled)
                        {
                            _actionRemoveCommand.DoExecute();
                            result = true;
                        }
                        break;

                    //Arrow Up
                    case (Keys.Control | Keys.Up):
                        if (_actionMoveUp.Enabled)
                        {
                            _actionMoveUp.DoExecute();
                            result = true;
                        }
                        break;

                    //Arrow Down
                    case (Keys.Control | Keys.Down):
                        if (_actionMoveDown.Enabled)
                        {
                            _actionMoveDown.DoExecute();
                            result = true;
                        }
                        break;
                }
            }
            return result;
        }

        public void Activate_()
        {
            ((MainForm)FindForm()).ShortCutKeys = ShortCutKeys;
            //@ changed
            //_actionRemoveCommand.Shortcut = Shortcut.CtrlDel;
            //_actionMoveUp.Shortcut = Shortcut.AltUpArrow;
            //_actionMoveDown.Shortcut = Shortcut.AltDownArrow;
        }

        private ListViewItem AddCommand(TRibbonCommand command)
        {
            ListViewItem result = ListViewCommands.Items.Add(new ListViewItem(command.Name));
            //result.Text = command.Name;
            result.SubItems.Add(command.LabelTitle.Content);
            result.Tag = command;
            return result;
        }

        public void ClearDocument()
        {
            ListViewCommands.Items.Clear();
        }

        public int FindSmallestUnusedID(int minId = 2)
        {
            Dictionary<int, TRibbonCommand> iDs;
            int i;
            const int MaxValidID = 59999;

            iDs = new Dictionary<int, TRibbonCommand>();

            try
            {
                // Gather all IDs that are already taken in a dictionary
                foreach (TRibbonCommand command in _document.Application.Commands)
                    if (command.Id > 0)
                        iDs.Add(command.Id, command);

                // Iterate all allowed IDs, starting with the smallest. Return the first one that hasn't been used yet
                for (i = minId; i < MaxValidID; i++)
                    if (!iDs.ContainsKey(i))
                        return (i);

                throw new ArgumentOutOfRangeException("No valid, unused ID could be found within the range between " + minId.ToString() + " && " + MaxValidID.ToString());
            }
            finally
            {
                //iDs.Free;
            }
        }

        private void BtnGenerateIDClick(object sender, EventArgs e)
        {
            int highID;
            int minID;

            highID = 1;
            foreach (TRibbonCommand command in _document.Application.Commands)
                if (command.Id > highID)
                    highID = command.Id;


            minID = 0;

            if (ListViewCommands.SelectedItems.Count > 0 && ListViewCommands.SelectedItems[0] != null)
                minID = ListViewCommands.SelectedItems[0].Index;

            minID = minID + 2;

            // By using at least the index of the item, we mimic the behavior of the ribbon compiler's ID auto generation as closely as possible.
            EditId.Text = FindSmallestUnusedID(minID).ToString();
        }

        public void Deactivate_()
        {
            ((MainForm)FindForm()).ShortCutKeys = null;
            //@ changed
            //_actionRemoveCommand.Shortcut = 0;
            //_actionMoveUp.Shortcut = 0;
            //_actionMoveDown.Shortcut = 0;
        }

        private void EditCaptionChange(object sender, EventArgs e)
        {
            if (_command != null && (_command.LabelTitle.Content != EditCaption.Text))
            {
                _command.LabelTitle.Content = EditCaption.Text;
                ListViewCommands.SelectedItems[0].SubItems[1].Text = EditCaption.Text;
                Modified();
            }
        }

        private void EditCaptionIdChange(object sender, EventArgs e)
        {
            if (_command != null && (EditCaptionId.Value != 1) && (_command.LabelTitle.Id != EditCaptionId.Value))
            {
                _command.LabelTitle.Id = (int)EditCaptionId.Value;
                Modified();
            }
        }

        private void EditCaptionSymbolChange(object sender, EventArgs e)
        {
            if (_command != null && (_command.LabelTitle.Symbol != EditCaptionSymbol.Text))
            {
                _command.LabelTitle.Symbol = EditCaptionSymbol.Text;
                Modified();
            }
        }

        private void EditCommentChange(object sender, EventArgs e)
        {
            if (_command != null && (_command.Comment != EditComment.Text))
            {
                _command.Comment = EditComment.Text;
                Modified();
            }
        }

        private void EditDescriptionChange(object sender, EventArgs e)
        {
            if (_command != null && (_command.LabelDescription.Content != EditDescription.Text))
            {
                _command.LabelDescription.Content = EditDescription.Text;
                Modified();
            }
        }

        private void EditDescriptionIdChange(object sender, EventArgs e)
        {
            if (_command != null && (EditDescriptionId.Value != 1) && (_command.LabelDescription.Id != EditDescriptionId.Value))
            {
                _command.LabelDescription.Id = (int)EditDescriptionId.Value;
                Modified();
            }
        }

        private void EditDescriptionSymbolChange(object sender, EventArgs e)
        {
            if (_command != null && (_command.LabelDescription.Symbol != EditDescriptionSymbol.Text))
            {
                _command.LabelDescription.Symbol = EditDescriptionSymbol.Text;
                Modified();
            }
        }

        private void EditIdChange(object sender, EventArgs e)
        {
            if (_command != null && (EditId.Value != 1) && (_command.Id != EditId.Value))
            {
                _command.Id = (int)EditId.Value;
                Modified();
            }
        }

        private void EditKeytipChange(object sender, EventArgs e)
        {
            if (_command != null && (_command.Keytip.Content != EditKeytip.Text))
            {
                _command.Keytip.Content = EditKeytip.Text;
                Modified();
            }
        }

        private void EditKeytipIdChange(object sender, EventArgs e)
        {
            if (_command != null && (EditKeytipId.Value != 1) && (_command.Keytip.Id != EditKeytipId.Value))
            {
                _command.Keytip.Id = (int)EditKeytipId.Value;
                Modified();
            }
        }

        private void EditKeyTipSymbolChange(object sender, EventArgs e)
        {
            if (_command != null && (_command.Keytip.Symbol != EditKeytipSymbol.Text))
            {
                _command.Keytip.Symbol = EditKeytipSymbol.Text;
                Modified();
            }
        }

        private void EditNameChange(object sender, EventArgs e)
        {
            if (_command != null && (_command.Name != EditName.Text))
            {
                _command.Name = EditName.Text;
                ListViewCommands.SelectedItems[0].Text = EditName.Text;
                Modified();
            }
        }

        private void EditNameKeyPress(object sender, KeyPressEventArgs e)
        {
            bool allowed = false;
            // Only allow valid Name/Symbol characters
            TextBox edit = sender as TextBox;
            switch (e.KeyChar)
            {
                case (char)3: // Ctrl-C
                case (char)0x16: // Ctrl-V
                case (char)0x18: // Ctrl-X
                case (char)8: // backspace
                case '_':
                    allowed = true;
                    break;
            }
            if (!allowed)
            {
                allowed = char.IsLetter(e.KeyChar);
            }
            if (allowed)
            {
                e.Handled = false;
                return;
            }
            if (char.IsDigit(e.KeyChar))
            {
                if (edit.SelectionStart == 0)
                    e.KeyChar = (char)0;
            }
            else
                e.KeyChar = (char)0;
            if (e.KeyChar == (char)0)
                e.Handled = true;
        }

        private void EditSymbolChange(object sender, EventArgs e)
        {
            if (_command != null && (_command.Symbol != EditSymbol.Text))
            {
                _command.Symbol = EditSymbol.Text;
                Modified();
            }
        }

        private void EditTooltipDescriptionChange(object sender, EventArgs e)
        {
            if (_command != null && (_command.TooltipDescription.Content != EditTooltipDescription.Text))
            {
                _command.TooltipDescription.Content = EditTooltipDescription.Text;
                Modified();
            }
        }

        private void EditTooltipDescriptionIdChange(object sender, EventArgs e)
        {
            if (_command != null && (EditTooltipDescriptionId.Value != 1) && (_command.TooltipDescription.Id != EditTooltipDescriptionId.Value))
            {
                _command.TooltipDescription.Id = (int)EditTooltipDescriptionId.Value;
                Modified();
            }
        }

        private void EditTooltipDescriptionSymbolChange(object sender, EventArgs e)
        {
            if (_command != null && (_command.TooltipDescription.Symbol != EditTooltipDescriptionSymbol.Text))
            {
                _command.TooltipDescription.Symbol = EditTooltipDescriptionSymbol.Text;
                Modified();
            }
        }

        private void EditTooltipTitleChange(object sender, EventArgs e)
        {
            if (_command != null && (_command.TooltipTitle.Content != EditTooltipTitle.Text))
            {
                _command.TooltipTitle.Content = EditTooltipTitle.Text;
                Modified();
            }
        }

        private void EditTooltipTitleIdChange(object sender, EventArgs e)
        {
            if (_command != null && (EditTooltipTitleId.Value != 1) && (_command.TooltipTitle.Id != EditTooltipTitleId.Value))
            {
                _command.TooltipTitle.Id = (int)EditTooltipTitleId.Value;
                Modified();
            }
        }

        private void EditTooltipTitleSymbolChange(object sender, EventArgs e)
        {
            if (_command != null && (_command.TooltipTitle.Symbol != EditTooltipTitleSymbol.Text))
            {
                _command.TooltipTitle.Symbol = EditTooltipTitleSymbol.Text;
                Modified();
            }
        }

        private void EnableControls(bool enable)
        {
            for (int i = 0; i < _panel2Layout.Controls.Count; i++)
                _panel2Layout.Controls[i].Enabled = enable;

            toolButtonRemoveCommand.Enabled = enable;
        }

        private void ListViewCommandsColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == _listViewColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (_listViewColumnSorter.Order == SortOrder.Ascending)
                {
                    _listViewColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    _listViewColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                _listViewColumnSorter.SortColumn = e.Column;
                _listViewColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.ListViewCommands.Sort();


            List<TRibbonCommand> commands;

            commands = new List<TRibbonCommand>();
            try
            {
                foreach (ListViewItem item in ((ListView)sender).Items)
                    commands.Add((TRibbonCommand)item.Tag);

                _document.Application.Commands.Assign(commands);
            }
            finally
            {
                //commands.Free;
            }
        }

        //ListViewCommandsCompare: Implementation in class ListViewColumnSorter

        private void ListViewTimer_Tick(object sender, EventArgs e) //@ added
        {
            ShowSelection();
            _listViewTimer.Stop();
        }

        private void ListViewCommandsSelectItem(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!e.IsSelected) //@ some code added
            {
                _listViewTimer.Start();
                return;
            }
            _listViewTimer.Stop();
            ShowSelection();
        }

        private void Modified()
        {
            if (!_updating)
                ((MainForm)FindForm()).Modified();
        }

        private void MoveCommand(int direction)
        {
            ListViewItem item, newItem;
            TRibbonCommand command;
            ListViewCommands.Sorting = SortOrder.None;

            item = ListViewCommands.SelectedItems[0];
            if ((item == null) || (item.Tag == null))
                return;
            command = (TRibbonCommand)item.Tag;

            if (_document.Application.Reorder(command, direction))
            {
                int itemIndex = item.Index;
                ListViewCommands.SelectedItems[0].Remove();
                if (direction < 0)
                    newItem = ListViewCommands.Items.Insert(itemIndex - 1, item);
                else
                    newItem = ListViewCommands.Items.Insert(itemIndex + 1, item);
                newItem = (item);
                //item.Free;
                ListViewCommands.Items[newItem.Index].Selected = true;
                newItem.Focused = true;
                newItem.EnsureVisible();
                Modified();
            }
        }

        private void PanelCommandPropertiesResize(object sender, EventArgs e)
        {
            //@ different implementation, function not needed
            //PanelImages.Height = (PanelCommandProperties.Height - PanelProps.Height - LabelHeader.Height) / 2;
        }

        private void PanelHighContrastImagesResize(object sender, EventArgs e)
        {
            //@ different implementation, function not needed
            //PanelSmallHCImages.Width = PanelHighContrastImages.Width / 2;
        }

        private void PanelImagesResize(object sender, EventArgs e)
        {
            //@ different implementation, function not needed
            //PanelSmallImages.Width = PanelImages.Width / 2;
        }

        public void ShowDocument(TRibbonDocument document)
        {
            int index;
            _document = document;
            _newCommandIndex = 0;
            ListViewCommands.ListViewItemSorter = null;
            ListViewCommands.Sorting = SortOrder.None; //@ added
            _listViewColumnSorter.Order = SortOrder.None;
            ListViewCommands.Items.Clear();
            ListViewCommands.BeginUpdate();
            try
            {
                foreach (TRibbonCommand command in _document.Application.Commands)
                {
                    AddCommand(command);
                    int commandNameMinLength = DefaultCommandNameAtBeginning.Length;
                    if (command.Name.Length >= commandNameMinLength && command.Name.Substring(0, commandNameMinLength).Equals(DefaultCommandNameAtBeginning))
                    //@ changed
                    //if (SameText(string.Copy(Command.Name, 1, 7), "Command"))
                    {
                        if (!int.TryParse(command.Name.Substring(commandNameMinLength), out index))
                            index = -1;
                        if (index > _newCommandIndex)
                            _newCommandIndex = index;
                    }
                }
                if (ListViewCommands.Items.Count > 0)
                {
                    ListViewCommands.Items[0].Selected = true;
                }
                else
                    ShowSelection();
            }
            finally
            {
                ListViewCommands.EndUpdate();
                ListViewCommands.ListViewItemSorter = _listViewColumnSorter;
            }
        }

        public void RefreshSelection()
        {
            ShowSelection();
        }

        private void ShowSelection()
        {
            ListViewItem item = null;
            if (ListViewCommands.SelectedItems.Count > 0)
                item = ListViewCommands.SelectedItems[0];
            if ((item != null))
            {
                _command = (TRibbonCommand)item.Tag;
                toolButtonMoveUp.Enabled = (item.Index > 0);
                toolButtonMoveDown.Enabled = (item.Index < (ListViewCommands.Items.Count - 1));
            }
            else
            {
                _command = null;
                toolButtonMoveUp.Enabled = false;
                toolButtonMoveDown.Enabled = false;
            }

            _updating = true;
            try
            {
                if (_command != null)
                {
                    EnableControls(true);
                    EditName.Text = _command.Name;
                    EditSymbol.Text = _command.Symbol;
                    EditId.Value = _command.Id;
                    EditComment.Text = _command.Comment;

                    EditCaption.Text = _command.LabelTitle.Content;
                    EditCaptionId.Value = _command.LabelTitle.Id;
                    EditCaptionSymbol.Text = _command.LabelTitle.Symbol;

                    EditDescription.Text = _command.LabelDescription.Content;
                    EditDescriptionId.Value = _command.LabelDescription.Id;
                    EditDescriptionSymbol.Text = _command.LabelDescription.Symbol;

                    EditTooltipTitle.Text = _command.TooltipTitle.Content;
                    EditTooltipTitleId.Value = _command.TooltipTitle.Id;
                    EditTooltipTitleSymbol.Text = _command.TooltipTitle.Symbol;

                    EditTooltipDescription.Text = _command.TooltipDescription.Content;
                    EditTooltipDescriptionId.Value = _command.TooltipDescription.Id;
                    EditTooltipDescriptionSymbol.Text = _command.TooltipDescription.Symbol;

                    EditKeytip.Text = _command.Keytip.Content;
                    EditKeytipId.Value = _command.Keytip.Id;
                    EditKeytipSymbol.Text = _command.Keytip.Symbol;
                }
                else
                {
                    EnableControls(false);
                    EditName.Text = string.Empty;
                    EditSymbol.Text = string.Empty;
                    EditId.Value = 0;
                    EditComment.Text = string.Empty;

                    EditCaption.Text = string.Empty;
                    EditCaptionId.Value = 0;
                    EditCaptionSymbol.Text = string.Empty;

                    EditDescription.Text = string.Empty;
                    EditDescriptionId.Value = 0;
                    EditDescriptionSymbol.Text = string.Empty;

                    EditTooltipTitle.Text = string.Empty;
                    EditTooltipTitleId.Value = 0;
                    EditTooltipTitleSymbol.Text = string.Empty;

                    EditTooltipDescription.Text = string.Empty;
                    EditTooltipDescriptionId.Value = 0;
                    EditTooltipDescriptionSymbol.Text = string.Empty;

                    EditKeytip.Text = string.Empty;
                    EditKeytipId.Value = 0;
                    EditKeytipSymbol.Text = string.Empty;
                }
                _smallImagesFrame.ShowImages(_command, ImageFlags.None);
                _largeImagesFrame.ShowImages(_command, ImageFlags.Large);
                _smallHCImagesFrame.ShowImages(_command, ImageFlags.HighContrast);
                _largeHCImagesFrame.ShowImages(_command, ImageFlags.Large | ImageFlags.HighContrast);
            }
            finally
            {
                _updating = false;
            }
        }

        private void UpDownChanging(object sender, EventArgs e)
        {
            NumericUpDown upDown = sender as NumericUpDown;
            // Skip value 1
            bool allowChange = upDown.Value != 1;
            if (!allowChange)
            {
                if (upDown.Text == "0" || upDown.Text == "1")
                    upDown.Value = 2;
                else
                    upDown.Value = 0;
            }
        }
    }
}
