using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using WinForms.Actions;

namespace UIRibbonTools
{
    partial class ViewsFrame
    {
        private System.Windows.Forms.ToolStripMenuItem menuAddTab;
        private System.Windows.Forms.ToolStripMenuItem menuAddTabGroup;
        private System.Windows.Forms.ToolStripMenuItem menuAddButton;
        private System.Windows.Forms.ToolStripMenuItem menuAddToggleButton;
        private System.Windows.Forms.ToolStripMenuItem menuAddDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem menuAddSplitButton;
        private System.Windows.Forms.ToolStripMenuItem menuAddCheckBox;
        private System.Windows.Forms.ToolStripMenuItem menuAddComboBox;
        private System.Windows.Forms.ToolStripMenuItem menuAddSpinner;
        private System.Windows.Forms.ToolStripMenuItem menuAddDropDownGallery;
        private System.Windows.Forms.ToolStripMenuItem menuAddSplitButtonGallery;
        private System.Windows.Forms.ToolStripMenuItem menuAddInRibbonGallery;
        private System.Windows.Forms.ToolStripMenuItem menuAddDropDownColorPicker;
        private System.Windows.Forms.ToolStripMenuItem menuAddFontControl;
        private System.Windows.Forms.ToolStripMenuItem menuAddFloatieFontControl;
        private System.Windows.Forms.ToolStripMenuItem menuAddMenuGroup;
        private System.Windows.Forms.ToolStripMenuItem menuAddQatButton;
        private System.Windows.Forms.ToolStripMenuItem menuAddQatToggleButton;
        private System.Windows.Forms.ToolStripMenuItem menuAddQatCheckBox;
        private System.Windows.Forms.ToolStripMenuItem menuAddQatComboBox;
        private System.Windows.Forms.ToolStripMenuItem menuAddQatDropDownGallery;
        private System.Windows.Forms.ToolStripMenuItem menuAddQatSplitButtonGallery;
        private System.Windows.Forms.ToolStripMenuItem menuAddQatInRibbonGallery;
        private System.Windows.Forms.ToolStripMenuItem menuAddRibbonSizeDefinition;
        private System.Windows.Forms.ToolStripMenuItem menuAddGroupSizeDefinition;
        private System.Windows.Forms.ToolStripMenuItem menuAddControlSizeDefinition;
        private System.Windows.Forms.ToolStripMenuItem menuAddControlSizeGroup;
        private System.Windows.Forms.ToolStripMenuItem menuAddColumnBreak;
        private System.Windows.Forms.ToolStripMenuItem menuAddRow;
        private System.Windows.Forms.ToolStripMenuItem menuAddGroup;
        private System.Windows.Forms.ToolStripMenuItem menuAddScale;
        private System.Windows.Forms.ToolStripMenuItem menuAddControlGroup;
        private System.Windows.Forms.ToolStripMenuItem menuAddContextPopup;
        private System.Windows.Forms.ToolStripMenuItem menuAddMiniToolbar;
        private System.Windows.Forms.ToolStripMenuItem menuAddContextMenu;
        private System.Windows.Forms.ToolStripMenuItem menuAddContextMap;
        private System.Windows.Forms.ToolStripMenuItem menuAddMiniToolbarMenuGroup;
        private ImageList _imageListTreeView;

        private TActionList _actionList;
        private TAction _actionAdd;
        private TAction _actionRemove;
        private TAction _actionMoveUp;
        private TAction _actionMoveDown;
        private TAction _actionAddButton;
        private TAction _actionAddToggleButton;
        private TAction _actionAddDropDownButton;
        private TAction _actionAddSplitButton;
        private TAction _actionAddCheckBox;
        private TAction _actionAddDropDownGallery;
        private TAction _actionAddSplitButtonGallery;
        private TAction _actionAddDropDownColorPicker;
        private TAction _actionAddMenuGroup;
        private TAction _actionAddQatButton;
        private TAction _actionAddQatToggleButton;
        private TAction _actionAddQatCheckBox;
        private TAction _actionAddQatComboBox;
        private TAction _actionAddQatDropDownGallery;
        private TAction _actionAddQatSplitButtonGallery;
        private TAction _actionAddQatInRibbonGallery;
        private TAction _actionAddRibbonSizeDefinition;
        private TAction _actionAddGroupSizeDefinition;
        private TAction _actionAddControlSizeDefinition;
        private TAction _actionAddControlSizeGroup;
        private TAction _actionAddColumnBreak;
        private TAction _actionAddRow;
        private TAction _actionAddGroup;
        private TAction _actionAddScale;
        private TAction _actionAddControlGroup;
        private TAction _actionAddComboBox;
        private TAction _actionAddSpinner;
        private TAction _actionAddInRibbonGallery;
        private TAction _actionAddFontControl;
        private TAction _actionAddFloatieFontControl;
        private TAction _actionAddTab;
        private TAction _actionAddTabGroup;
        private TAction _actionAddContextPopup;
        private TAction _actionAddMiniToolbar;
        private TAction _actionAddContextMenu;
        private TAction _actionAddContextMap;
        private TAction _actionAddMiniToolbarMenuGroup;

        private void InitActions(IContainer components)
        {
            _actionList = new TActionList(components);

            _actionAdd = new TAction(components);
            _actionRemove = new TAction(components);
            _actionMoveUp = new TAction(components);
            _actionMoveDown = new TAction(components);
            _actionAddButton = new TAction(components);
            _actionAddToggleButton = new TAction(components);
            _actionAddDropDownButton = new TAction(components);
            _actionAddSplitButton = new TAction(components);
            _actionAddCheckBox = new TAction(components);
            _actionAddDropDownGallery = new TAction(components);
            _actionAddSplitButtonGallery = new TAction(components);
            _actionAddDropDownColorPicker = new TAction(components);
            _actionAddMenuGroup = new TAction(components);
            _actionAddQatButton = new TAction(components);
            _actionAddQatToggleButton = new TAction(components);
            _actionAddQatCheckBox = new TAction(components);
            _actionAddQatComboBox = new TAction(components);
            _actionAddQatDropDownGallery = new TAction(components);
            _actionAddQatSplitButtonGallery = new TAction(components);
            _actionAddQatInRibbonGallery = new TAction(components);
            _actionAddRibbonSizeDefinition = new TAction(components);
            _actionAddGroupSizeDefinition = new TAction(components);
            _actionAddControlSizeDefinition = new TAction(components);
            _actionAddControlSizeGroup = new TAction(components);
            _actionAddColumnBreak = new TAction(components);
            _actionAddRow = new TAction(components);
            _actionAddGroup = new TAction(components);
            _actionAddScale = new TAction(components);
            _actionAddControlGroup = new TAction(components);
            _actionAddComboBox = new TAction(components);
            _actionAddSpinner = new TAction(components);
            _actionAddInRibbonGallery = new TAction(components);
            _actionAddFontControl = new TAction(components);
            _actionAddFloatieFontControl = new TAction(components);
            _actionAddTab = new TAction(components);
            _actionAddTabGroup = new TAction(components);
            _actionAddContextPopup = new TAction(components);
            _actionAddMiniToolbar = new TAction(components);
            _actionAddContextMenu = new TAction(components);
            _actionAddContextMap = new TAction(components);
            _actionAddMiniToolbarMenuGroup = new TAction(components);

            _actionList.Actions.AddRange(new TAction[] {
                _actionAdd,
                _actionRemove,
                _actionMoveUp,
                _actionMoveDown,
                _actionAddButton,
                _actionAddToggleButton,
                _actionAddDropDownButton,
                _actionAddSplitButton,
                _actionAddCheckBox,
                _actionAddDropDownGallery,
                _actionAddSplitButtonGallery,
                _actionAddDropDownColorPicker,
                _actionAddMenuGroup,
                _actionAddQatButton,
                _actionAddQatToggleButton,
                _actionAddQatCheckBox,
                _actionAddQatComboBox,
                _actionAddQatDropDownGallery,
                _actionAddQatSplitButtonGallery,
                _actionAddQatInRibbonGallery,
                _actionAddRibbonSizeDefinition,
                _actionAddGroupSizeDefinition,
                _actionAddControlSizeDefinition,
                _actionAddControlSizeGroup,
                _actionAddColumnBreak,
                _actionAddRow,
                _actionAddGroup,
                _actionAddScale,
                _actionAddControlGroup,
                _actionAddComboBox,
                _actionAddSpinner,
                _actionAddInRibbonGallery,
                _actionAddFontControl,
                _actionAddFloatieFontControl,
                _actionAddTab,
                _actionAddTabGroup,
                _actionAddContextPopup,
                _actionAddMiniToolbar,
                _actionAddContextMenu,
                _actionAddContextMap,
                _actionAddMiniToolbarMenuGroup,
            });

            _actionAdd.Update += TreeActionUpdate;
            _actionAdd.Hint = "Adds a subitem to the selected node";
            _actionAdd.ImageIndex = 36;
            _actionAdd.Text = "Add";
            _actionList.SetAction(toolButtonAdd, _actionAdd);

            _actionRemove.Execute += ActionRemoveExecute;
            _actionRemove.Update += TreeActionUpdate;
            _actionRemove.Hint = "Removes the selected item";
            _actionRemove.ImageIndex = 37;
            _actionRemove.ShortcutKeys = Keys.Control | Keys.Delete;
            _actionRemove.Text = "Remove";
            _actionList.SetAction(toolButtonRemove, _actionRemove);
            _actionList.SetAction(popupRemove, _actionRemove);

            _actionMoveUp.Execute += ActionMoveUpExecute;
            _actionMoveUp.Update += TreeActionUpdate;
            _actionMoveUp.ImageIndex = 38;
            _actionMoveUp.ShortcutKeys = Keys.Control | Keys.Up;
            _actionMoveUp.Text = "Up";
            _actionList.SetAction(toolButtonMoveUp, _actionMoveUp);
            _actionList.SetAction(popupMoveUp, _actionMoveUp);

            _actionMoveDown.Execute += ActionMoveDownExecute;
            _actionMoveDown.Update += TreeActionUpdate;
            _actionMoveDown.ImageIndex = 39;
            _actionMoveDown.ShortcutKeys = Keys.Control | Keys.Down;
            _actionMoveDown.Text = "Down";
            _actionList.SetAction(toolButtonMoveDown, _actionMoveDown);
            _actionList.SetAction(popupMoveDown, _actionMoveDown);

            _actionAddButton.Execute += ActionAddButtonExecute;
            _actionAddButton.ImageIndex = 0;
            _actionAddButton.Text = "Add Button";
            _actionList.SetAction(popupAddButton, _actionAddButton);
            _actionList.SetAction(menuAddButton, _actionAddButton);

            _actionAddToggleButton.Execute += ActionAddToggleButtonExecute;
            _actionAddToggleButton.ImageIndex = 1;
            _actionAddToggleButton.Text = "Add ToggleButton";
            _actionList.SetAction(popupAddToggleButton, _actionAddToggleButton);
            _actionList.SetAction(menuAddToggleButton, _actionAddToggleButton);

            _actionAddDropDownButton.Execute += ActionAddDropDownButtonExecute;
            _actionAddDropDownButton.ImageIndex = 3;
            _actionAddDropDownButton.Text = "Add DropDownButton";
            _actionList.SetAction(popupAddDropDownButton, _actionAddDropDownButton);
            _actionList.SetAction(menuAddDropDownButton, _actionAddDropDownButton);

            _actionAddSplitButton.Execute += ActionAddSplitButtonExecute;
            _actionAddSplitButton.ImageIndex = 2;
            _actionAddSplitButton.Text = "Add SplitButton";
            _actionList.SetAction(popupAddSplitButton, _actionAddSplitButton);
            _actionList.SetAction(menuAddSplitButton, _actionAddSplitButton);

            _actionAddCheckBox.Execute += ActionAddCheckBoxExecute;
            _actionAddCheckBox.ImageIndex = 10;
            _actionAddCheckBox.Text = "Add CheckBox";
            _actionList.SetAction(popupAddCheckBox, _actionAddCheckBox);
            _actionList.SetAction(menuAddCheckBox, _actionAddCheckBox);

            _actionAddDropDownGallery.Execute += ActionAddDropDownGalleryExecute;
            _actionAddDropDownGallery.ImageIndex = 15;
            _actionAddDropDownGallery.Text = "Add DropDownGallery";
            _actionList.SetAction(popupAddDropDownGallery, _actionAddDropDownGallery);
            _actionList.SetAction(menuAddDropDownGallery, _actionAddDropDownGallery);

            _actionAddSplitButtonGallery.Execute += ActionAddSplitButtonGalleryExecute;
            _actionAddSplitButtonGallery.ImageIndex = 16;
            _actionAddSplitButtonGallery.Text = "Add SplitButtonGallery";
            _actionList.SetAction(popupAddSplitButtonGallery, _actionAddSplitButtonGallery);
            _actionList.SetAction(menuAddSplitButtonGallery, _actionAddSplitButtonGallery);

            _actionAddDropDownColorPicker.Execute += ActionAddDropDownColorPickerExecute;
            _actionAddDropDownColorPicker.ImageIndex = 4;
            _actionAddDropDownColorPicker.Text = "Add DropDownColorPicker";
            _actionList.SetAction(popupAddDropDownColorPicker, _actionAddDropDownColorPicker);
            _actionList.SetAction(menuAddDropDownColorPicker, _actionAddDropDownColorPicker);

            _actionAddMenuGroup.Execute += ActionAddMenuGroupExecute;
            _actionAddMenuGroup.ImageIndex = 24;
            _actionAddMenuGroup.Text = "Add MenuGroup";
            _actionList.SetAction(popupAddMenuGroup, _actionAddMenuGroup);
            _actionList.SetAction(menuAddMenuGroup, _actionAddMenuGroup);

            _actionAddQatButton.Execute += ActionAddQatButtonExecute;
            _actionAddQatButton.ImageIndex = 0;
            _actionAddQatButton.Text = "Add Qat Button";
            _actionList.SetAction(popupAddQatButton, _actionAddQatButton);
            _actionList.SetAction(menuAddQatButton, _actionAddQatButton);

            _actionAddQatToggleButton.Execute += ActionAddQatToggleButtonExecute;
            _actionAddQatToggleButton.ImageIndex = 1;
            _actionAddQatToggleButton.Text = "Add Qat ToggleButton";
            _actionList.SetAction(popupAddQatToggleButton, _actionAddQatToggleButton);
            _actionList.SetAction(menuAddQatToggleButton, _actionAddQatToggleButton);

            _actionAddQatCheckBox.Execute += ActionAddQatCheckBoxExecute;
            _actionAddQatCheckBox.ImageIndex = 10;
            _actionAddQatCheckBox.Text = "Add Qat CheckBox";
            _actionList.SetAction(popupAddQatCheckBox, _actionAddQatCheckBox);
            _actionList.SetAction(menuAddQatCheckBox, _actionAddQatCheckBox);

            _actionAddQatComboBox.Execute += ActionAddQatComboBoxExecute;
            _actionAddQatComboBox.Hint = "Since Windows 8";
            _actionAddQatComboBox.ImageIndex = 7;
            _actionAddQatComboBox.Text = "Add Qat ComboBox";
            _actionList.SetAction(popupAddQatComboBox, _actionAddQatComboBox);
            _actionList.SetAction(menuAddQatComboBox, _actionAddQatComboBox);

            _actionAddQatDropDownGallery.Execute += ActionAddQatDropDownGalleryExecute;
            _actionAddQatDropDownGallery.Hint = "Since Windows 8";
            _actionAddQatDropDownGallery.ImageIndex = 15;
            _actionAddQatDropDownGallery.Text = "Add Qat DropDownGallery";
            _actionList.SetAction(popupAddQatDropDownGallery, _actionAddQatDropDownGallery);
            _actionList.SetAction(menuAddQatDropDownGallery, _actionAddQatDropDownGallery);

            _actionAddQatSplitButtonGallery.Execute += ActionAddQatSplitButtonGalleryExecute;
            _actionAddQatSplitButtonGallery.Hint = "Since Windows 8";
            _actionAddQatSplitButtonGallery.ImageIndex = 16;
            _actionAddQatSplitButtonGallery.Text = "Add Qat SplitButtonGallery";
            _actionList.SetAction(popupAddQatSplitButtonGallery, _actionAddQatSplitButtonGallery);
            _actionList.SetAction(menuAddQatSplitButtonGallery, _actionAddQatSplitButtonGallery);

            _actionAddQatInRibbonGallery.Execute += ActionAddQatInRibbonGalleryExecute;
            _actionAddQatInRibbonGallery.Hint = "Since Windows 8";
            _actionAddQatInRibbonGallery.ImageIndex = 17;
            _actionAddQatInRibbonGallery.Text = "Add Qat InRibbonGallery";
            _actionList.SetAction(popupAddQatInRibbonGallery, _actionAddQatInRibbonGallery);
            _actionList.SetAction(menuAddQatInRibbonGallery, _actionAddQatInRibbonGallery);

            _actionAddRibbonSizeDefinition.Execute += ActionAddRibbonSizeDefinitionExecute;
            _actionAddRibbonSizeDefinition.ImageIndex = 25;
            _actionAddRibbonSizeDefinition.Text = "Add Ribbon SizeDefinition";
            _actionList.SetAction(popupAddRibbonSizeDefinition, _actionAddRibbonSizeDefinition);
            _actionList.SetAction(menuAddRibbonSizeDefinition, _actionAddRibbonSizeDefinition);

            _actionAddGroupSizeDefinition.Execute += ActionAddGroupSizeDefinitionExecute;
            _actionAddGroupSizeDefinition.ImageIndex = 30;
            _actionAddGroupSizeDefinition.Text = "Add GroupSizeDefinition";
            _actionList.SetAction(popupAddGroupSizeDefinition, _actionAddGroupSizeDefinition);
            _actionList.SetAction(menuAddGroupSizeDefinition, _actionAddGroupSizeDefinition);

            _actionAddControlSizeDefinition.Execute += ActionAddControlSizeDefinitionExecute;
            _actionAddControlSizeDefinition.ImageIndex = 31;
            _actionAddControlSizeDefinition.Text = "Add ControlSizeDefinition";
            _actionList.SetAction(popupAddControlSizeDefinition, _actionAddControlSizeDefinition);
            _actionList.SetAction(menuAddControlSizeDefinition, _actionAddControlSizeDefinition);

            _actionAddControlSizeGroup.Execute += ActionAddControlSizeGroupExecute;
            _actionAddControlSizeGroup.ImageIndex = 31;
            _actionAddControlSizeGroup.Text = "Add ControlGroup";
            _actionList.SetAction(popupAddControlSizeGroup, _actionAddControlSizeGroup);
            _actionList.SetAction(menuAddControlSizeGroup, _actionAddControlSizeGroup);

            _actionAddColumnBreak.Execute += ActionAddColumnBreakExecute;
            _actionAddColumnBreak.ImageIndex = 31;
            _actionAddColumnBreak.Text = "Add ColumnBreak";
            _actionList.SetAction(popupAddColumnBreak, _actionAddColumnBreak);
            _actionList.SetAction(menuAddColumnBreak, _actionAddColumnBreak);

            _actionAddRow.Execute += ActionAddRowExecute;
            _actionAddRow.ImageIndex = 31;
            _actionAddRow.Text = "Add Row";
            _actionList.SetAction(popupAddRow, _actionAddRow);
            _actionList.SetAction(menuAddRow, _actionAddRow);

            _actionAddGroup.Execute += ActionAddGroupExecute;
            _actionAddGroup.ImageIndex = 20;
            _actionAddGroup.Text = "Add Group";
            _actionList.SetAction(popupAddGroup, _actionAddGroup);
            _actionList.SetAction(menuAddGroup, _actionAddGroup);

            _actionAddScale.Execute += ActionAddScaleExecute;
            _actionAddScale.ImageIndex = 29;
            _actionAddScale.Text = "Add Scale";
            _actionList.SetAction(popupAddScale, _actionAddScale);
            _actionList.SetAction(menuAddScale, _actionAddScale);

            _actionAddControlGroup.Execute += ActionAddControlGroupExecute;
            _actionAddControlGroup.ImageIndex = 14;
            _actionAddControlGroup.Text = "Add ControlGroup";
            _actionList.SetAction(popupAddControlGroup, _actionAddControlGroup);
            _actionList.SetAction(menuAddControlGroup, _actionAddControlGroup);

            _actionAddComboBox.Execute += ActionAddComboBoxExecute;
            _actionAddComboBox.ImageIndex = 7;
            _actionAddComboBox.Text = "Add ComboBox";
            _actionList.SetAction(popupAddComboBox, _actionAddComboBox);
            _actionList.SetAction(menuAddComboBox, _actionAddComboBox);

            _actionAddSpinner.Execute += ActionAddSpinnerExecute;
            _actionAddSpinner.ImageIndex = 5;
            _actionAddSpinner.Text = "Add Spinner";
            _actionList.SetAction(popupAddSpinner, _actionAddSpinner);
            _actionList.SetAction(menuAddSpinner, _actionAddSpinner);

            _actionAddInRibbonGallery.Execute += ActionAddInRibbonGalleryExecute;
            _actionAddInRibbonGallery.ImageIndex = 17;
            _actionAddInRibbonGallery.Text = "Add InRibbonGallery";
            _actionList.SetAction(popupAddInRibbonGallery, _actionAddInRibbonGallery);
            _actionList.SetAction(menuAddInRibbonGallery, _actionAddInRibbonGallery);

            _actionAddFontControl.Execute += ActionAddFontControlExecute;
            _actionAddFontControl.ImageIndex = 13;
            _actionAddFontControl.Text = "Add FontControl";
            _actionList.SetAction(popupAddFontControl, _actionAddFontControl);
            _actionList.SetAction(menuAddFontControl, _actionAddFontControl);

            _actionAddFloatieFontControl.Execute += ActionAddFloatieFontControlExecute;
            _actionAddFloatieFontControl.ImageIndex = 12;
            _actionAddFloatieFontControl.Text = "Add FloatieFontControl";
            _actionList.SetAction(popupAddFloatieFontControl, _actionAddFloatieFontControl);
            _actionList.SetAction(menuAddFloatieFontControl, _actionAddFloatieFontControl);

            _actionAddTab.Execute += ActionAddTabExecute;
            _actionAddTab.ImageIndex = 21;
            _actionAddTab.Text = "Add Tab";
            _actionList.SetAction(popupAddTab, _actionAddTab);
            _actionList.SetAction(menuAddTab, _actionAddTab);

            _actionAddTabGroup.Execute += ActionAddTabGroupExecute;
            _actionAddTabGroup.ImageIndex = 22;
            _actionAddTabGroup.Text = "Add TabGroup";
            _actionList.SetAction(popupAddTabGroup, _actionAddTabGroup);
            _actionList.SetAction(menuAddTabGroup, _actionAddTabGroup);

            //_actionAddContextPopup.Execute += ActionAddContextPopupExecute;
            //_actionAddContextPopup.ImageIndex = 32;
            //_actionAddContextPopup.Text = "Add ContextPopup";
            //_actionAddContextPopup.Visible = false;
			//_actionList.SetAction(popupAddContextPopup, _actionAddContextPopup);
			//_actionList.SetAction(menuAddContextPopup, _actionAddContextPopup);

            _actionAddMiniToolbar.Execute += ActionAddMiniToolbarExecute;
            _actionAddMiniToolbar.ImageIndex = 33;
            _actionAddMiniToolbar.Text = "Add MiniToolbar";
            _actionList.SetAction(popupAddMiniToolbar, _actionAddMiniToolbar);
            _actionList.SetAction(menuAddMiniToolbar, _actionAddMiniToolbar);

            _actionAddContextMenu.Execute += ActionAddContextMenuExecute;
            _actionAddContextMenu.ImageIndex = 34;
            _actionAddContextMenu.Text = "Add ContextMenu";
            _actionList.SetAction(popupAddContextMenu, _actionAddContextMenu);
            _actionList.SetAction(menuAddContextMenu, _actionAddContextMenu);

            _actionAddContextMap.Execute += ActionAddContextMapExecute;
            _actionAddContextMap.ImageIndex = 35;
            _actionAddContextMap.Text = "Add ContextMap";
            _actionList.SetAction(popupAddContextMap, _actionAddContextMap);
            _actionList.SetAction(menuAddContextMap, _actionAddContextMap);

            _actionAddMiniToolbarMenuGroup.Execute += ActionAddMiniToolbarMenuGroupExecute;
            _actionAddMiniToolbarMenuGroup.ImageIndex = 24;
            _actionAddMiniToolbarMenuGroup.Text = "Add MiniToolbar MenuGroup";
            _actionList.SetAction(popupAddMiniToolbarMenuGroup, _actionAddMiniToolbarMenuGroup);
            _actionList.SetAction(menuAddMiniToolbarMenuGroup, _actionAddMiniToolbarMenuGroup);

            _actionList.ImageList = _imageListTreeView;
        }

        private void InitMenu()
        {
            this.menuAddTab = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddTabGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddButton = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddToggleButton = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddDropDownButton = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddSplitButton = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddCheckBox = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddComboBox = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddSpinner = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddDropDownGallery = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddSplitButtonGallery = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddInRibbonGallery = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddDropDownColorPicker = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddFontControl = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddFloatieFontControl = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddMenuGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddQatButton = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddQatToggleButton = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddQatCheckBox = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddQatComboBox = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddQatDropDownGallery = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddQatSplitButtonGallery = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddQatInRibbonGallery = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddRibbonSizeDefinition = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddGroupSizeDefinition = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddControlSizeDefinition = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddControlSizeGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddColumnBreak = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddRow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddScale = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddControlGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddContextPopup = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddMiniToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddContextMap = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddMiniToolbarMenuGroup = new System.Windows.Forms.ToolStripMenuItem();
            MenuProperties();
        }

        private void InitializeAddon()
        {
            _imageListTreeView = ImageManager.ImageListTreeView_Views(components);

            popupMenuTree.ImageList = _imageListTreeView;
            InitMenu();
            toolBarViews.ImageList = _imageListTreeView;
            toolButtonAdd.DropDown = new ToolStripDropDown();
            toolButtonAdd.DropDown.ImageList = _imageListTreeView;
            toolButtonAdd.DropDown.Items.AddRange(new ToolStripItem[] {
            this.menuAddTab,
            this.menuAddTabGroup,
            this.menuAddButton,
            this.menuAddToggleButton,
            this.menuAddDropDownButton,
            this.menuAddSplitButton,
            this.menuAddCheckBox,
            this.menuAddComboBox,
            this.menuAddSpinner,
            this.menuAddDropDownGallery,
            this.menuAddSplitButtonGallery,
            this.menuAddInRibbonGallery,
            this.menuAddDropDownColorPicker,
            this.menuAddFontControl,
            this.menuAddFloatieFontControl,
            this.menuAddMenuGroup,
            this.menuAddQatButton,
            this.menuAddQatToggleButton,
            this.menuAddQatCheckBox,
            this.menuAddQatComboBox,
            this.menuAddQatDropDownGallery,
            this.menuAddQatSplitButtonGallery,
            this.menuAddQatInRibbonGallery,
            this.menuAddRibbonSizeDefinition,
            this.menuAddGroupSizeDefinition,
            this.menuAddControlSizeDefinition,
            this.menuAddControlSizeGroup,
            this.menuAddColumnBreak,
            this.menuAddRow,
            this.menuAddGroup,
            this.menuAddScale,
            this.menuAddControlGroup,
            this.menuAddContextPopup,
            this.menuAddMiniToolbar,
            this.menuAddContextMenu,
            this.menuAddContextMap,
            this.menuAddMiniToolbarMenuGroup
            });

            treeViewRibbon.ImageList = _imageListTreeView;
            treeViewRibbon.ContextMenuStrip = popupMenuTree;
            treeViewRibbon.ContextMenuStrip.Opening += PopupMenuPopup;
            treeViewRibbon.AfterSelect += TreeViewRibbonChange;
            toolButtonAdd.DropDown.Opening += PopupMenuPopup;

            InitActions(components);
        }

        private void MenuProperties()
        {
            // 
            // menuAddTab
            // 
            this.menuAddTab.Name = "menuAddTab";
            this.menuAddTab.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddTabGroup
            // 
            this.menuAddTabGroup.Name = "menuAddTabGroup";
            this.menuAddTabGroup.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddButton
            // 
            this.menuAddButton.Name = "menuAddButton";
            this.menuAddButton.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddToggleButton
            // 
            this.menuAddToggleButton.Name = "menuAddToggleButton";
            this.menuAddToggleButton.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddDropDownButton
            // 
            this.menuAddDropDownButton.Name = "menuAddDropDownButton";
            this.menuAddDropDownButton.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddSplitButton
            // 
            this.menuAddSplitButton.Name = "menuAddSplitButton";
            this.menuAddSplitButton.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddCheckBox
            // 
            this.menuAddCheckBox.Name = "menuAddCheckBox";
            this.menuAddCheckBox.Size = new System.Drawing.Size(180, 22);
           // 
            // menuAddComboBox
            // 
            this.menuAddComboBox.Name = "menuAddComboBox";
            this.menuAddComboBox.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddSpinner
            // 
            this.menuAddSpinner.Name = "menuAddSpinner";
            this.menuAddSpinner.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddDropDownGallery
            // 
            this.menuAddDropDownGallery.Name = "menuAddDropDownGallery";
            this.menuAddDropDownGallery.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddSplitButtonGallery
            // 
            this.menuAddSplitButtonGallery.Name = "menuAddSplitButtonGallery";
            this.menuAddSplitButtonGallery.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddInRibbonGallery
            // 
            this.menuAddInRibbonGallery.Name = "menuAddInRibbonGallery";
            this.menuAddInRibbonGallery.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddDropDownColorPicker
            // 
            this.menuAddDropDownColorPicker.Name = "menuAddDropDownColorPicker";
            this.menuAddDropDownColorPicker.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddFontControl
            // 
            this.menuAddFontControl.Name = "menuAddFontControl";
            this.menuAddFontControl.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddFloatieFontControl
            // 
            this.menuAddFloatieFontControl.Name = "menuAddFloatieFontControl";
            this.menuAddFloatieFontControl.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddMenuGroup
            // 
            this.menuAddMenuGroup.Name = "menuAddMenuGroup";
            this.menuAddMenuGroup.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddQatButton
            // 
            this.menuAddQatButton.Name = "menuAddQatButton";
            this.menuAddQatButton.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddQatToggleButton
            // 
            this.menuAddQatToggleButton.Name = "menuAddQatToggleButton";
            this.menuAddQatToggleButton.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddQatCheckBox
            // 
            this.menuAddQatCheckBox.Name = "menuAddQatCheckBox";
            this.menuAddQatCheckBox.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddQatComboBox
            // 
            this.menuAddQatComboBox.Name = "menuAddQatComboBox";
            this.menuAddQatComboBox.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddQatDropDownGallery
            // 
            this.menuAddQatDropDownGallery.Name = "menuAddQatDropDownGallery";
            this.menuAddQatDropDownGallery.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddQatSplitButtonGallery
            // 
            this.menuAddQatSplitButtonGallery.Name = "menuAddQatSplitButtonGallery";
            this.menuAddQatSplitButtonGallery.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddQatInRibbonGallery
            // 
            this.menuAddQatInRibbonGallery.Name = "menuAddQatInRibbonGallery";
            this.menuAddQatInRibbonGallery.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddRibbonSizeDefinition
            // 
            this.menuAddRibbonSizeDefinition.Name = "menuAddRibbonSizeDefinition";
            this.menuAddRibbonSizeDefinition.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddGroupSizeDefinition
            // 
            this.menuAddGroupSizeDefinition.Name = "menuAddGroupSizeDefinition";
            this.menuAddGroupSizeDefinition.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddControlSizeDefinition
            // 
            this.menuAddControlSizeDefinition.Name = "menuAddControlSizeDefinition";
            this.menuAddControlSizeDefinition.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddControlSizeGroup
            // 
            this.menuAddControlSizeGroup.Name = "menuAddControlSizeGroup";
            this.menuAddControlSizeGroup.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddColumnBreak
            // 
            this.menuAddColumnBreak.Name = "menuAddColumnBreak";
            this.menuAddColumnBreak.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddRow
            // 
            this.menuAddRow.Name = "menuAddRow";
            this.menuAddRow.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddGroup
            // 
            this.menuAddGroup.Name = "menuAddGroup";
            this.menuAddGroup.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddScale
            // 
            this.menuAddScale.Name = "menuAddScale";
            this.menuAddScale.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddControlGroup
            // 
            this.menuAddControlGroup.Name = "menuAddControlGroup";
            this.menuAddControlGroup.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddContextPopup
            // 
            this.menuAddContextPopup.Name = "menuAddContextPopup";
            this.menuAddContextPopup.Size = new System.Drawing.Size(180, 22);
            this.menuAddContextPopup.Visible = false;
            // 
            // menuAddMiniToolbar
            // 
            this.menuAddMiniToolbar.Name = "menuAddMiniToolbar";
            this.menuAddMiniToolbar.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddContextMenu
            // 
            this.menuAddContextMenu.Name = "menuAddContextMenu";
            this.menuAddContextMenu.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddContextMap
            // 
            this.menuAddContextMap.Name = "menuAddContextMap";
            this.menuAddContextMap.Size = new System.Drawing.Size(180, 22);
            // 
            // menuAddMiniToolbarMenuGroup
            // 
            this.menuAddMiniToolbarMenuGroup.Name = "menuAddMiniToolbarMenuGroup";
            this.menuAddMiniToolbarMenuGroup.Size = new System.Drawing.Size(180, 22);
        }
    }
}
