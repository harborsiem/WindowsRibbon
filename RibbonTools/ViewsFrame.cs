using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
//using UIRibbonTools.Properties;
using WinForms.Actions;

namespace UIRibbonTools
{
    partial class ViewsFrame : UserControl
    {
        const string RS_RIBBON = "Ribbon";
        const string RS_APPLICATION_MENU = "Application Menu";
        const string RS_MENU_GROUP = "Menu Group";
        const string RS_HELP_BUTTON = "Help Button";
        const string RS_QAT = "Quick Access Toolbar";
        const string RS_TABS = "Tabs";
        const string RS_CONTEXTUAL_TABS = "Contextual Tabs";
        const string RS_SIZE_DEFS = "Size Definitions";
        const string RS_NONE = "(none)";
        const string RS_BUTTON_ITEM = "Button (Top) Item";
        const string RS_ITEMS = "Items";
        const string RS_SCALING_POLICY = "Scaling Policy";
        const string RS_IDEAL_SIZES = "Ideal Sizes";
        const string RS_CONTROL_NAME_MAP = "Control Name Map";
        const string RS_CONTEXT_POPUP = "Context Popup";
        const string RS_MINI_TOOLBARS = "Mini Toolbars";
        const string RS_CONTEXT_MENUS = "Context Menus";
        const string RS_CONTEXT_MAPS = "Context Maps";
        const string RS_REMOVE_ITEM_HEADER = "Remove Item?";
        const string RS_REMOVE_ITEM_MESSAGE = "Do you want to remove the selected item AND all subitems (this cannot be undone)?";
        const string RS_SIZE_DEF = "Size Definition"; //@ added

        // Image Index
        const int II_HELP_BUTTON = 11;
        const int II_APPLICATION_MENU = 18;
        const int II_QAT = 19;
        const int II_GROUP = 20;
        const int II_TAB = 21;
        const int II_CONTEXTUAL_TAB = 22;
        const int II_RIBBON = 23;
        const int II_MENU_GROUP = 24;
        public const int II_SIZE_DEF = 25;
        const int II_BUTTON_ITEM = 26;
        const int II_LIST = 27;
        const int II_SCALING = 28;
        const int II_SCALE = 29;
        const int II_GROUP_SIZE_DEF = 30;
        const int II_CONTROL_SIZE_DEF = 31;
        const int II_CONTEXT_POPUP = 32;
        const int II_MINI_TOOLBAR = 33;
        const int II_CONTEXT_MENU = 34;
        const int II_CONTEXT_MAP = 35;

        private static readonly int[] ConvertQatImageIndex = { (int)RibbonObjectType.ComboBox, (int)RibbonObjectType.DropDownGallery,
            (int)RibbonObjectType.SplitButtonGallery, (int)RibbonObjectType.InRibbonGallery };

        private TRibbonDocument _document;
        private TreeNode _currentNode;
        private BaseFrame _currentFrame;
        private TRibbonObject _currentObject;
        private List<RibbonCommandItem> _commands;
        private Dictionary<ViewClasses, BaseFrame> _viewClasses; //cache of ViewClasses
        private bool _selectAddedNode;

        public ViewsFrame()
        {
#if Core
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
#endif
            InitializeComponent();
            if (components == null)
                components = new Container();

            bool runtime = (LicenseManager.UsageMode == LicenseUsageMode.Runtime);
            if (runtime)
                InitializeAddon();
            _commands = new List<RibbonCommandItem>();
            _viewClasses = new Dictionary<ViewClasses, BaseFrame>(36);
        }

        public void SetBoldFonts()
        {
            //nothing
        }

        public void SetFonts(Font font)
        {
            this.Font = font;
        }

        public List<RibbonCommandItem> Commands { get { return _commands; } }

        private void AddApplicationMenu(TreeNode parent,
            TRibbonApplicationMenu appMenu)
        {
            TreeNode node;

            node = AddNode(parent, RS_APPLICATION_MENU, II_APPLICATION_MENU, appMenu);
            foreach (TRibbonAppMenuGroup group in appMenu.MenuGroups)
                AddAppMenuGroup(node, group);
        }

        private void AddAppMenuGroup(TreeNode parent,
            TRibbonAppMenuGroup group)
        {
            TreeNode node;

            node = AddNode(parent, group.DisplayName(), II_MENU_GROUP, group);
            foreach (TRibbonControl control in group.Controls)
                AddControl(node, control);
        }

        private void AddContextMap(TreeNode parent,
            TRibbonContextMap map)
        {
            AddNode(parent, map.DisplayName(), II_CONTEXT_MAP, map);
        }

        private void AddContextMaps(TreeNode parent,
            TRibbonList<TRibbonContextMap> maps)
        {
            TreeNode node;

            node = AddNode(parent, RS_CONTEXT_MAPS, II_CONTEXT_MAP, maps);
            foreach (TRibbonContextMap map in maps)
                AddContextMap(node, map);
        }

        private void AddContextMenu(TreeNode parent,
            TRibbonContextMenu menu)
        {
            TreeNode node;
            bool originalSelectedAddedNode;

            node = AddNode(parent, menu.DisplayName(), II_CONTEXT_MENU, menu);
            originalSelectedAddedNode = _selectAddedNode;
            _selectAddedNode = false;
            try
            {
                foreach (TRibbonMenuGroup group in menu.MenuGroups)
                    AddMenuGroup(node, group);
            }
            finally
            {
                _selectAddedNode = originalSelectedAddedNode;
            }
        }

        private void AddContextMenus(TreeNode parent,
            TRibbonList<TRibbonContextMenu> menus)
        {
            TreeNode node;
            node = AddNode(parent, RS_CONTEXT_MENUS, II_CONTEXT_MENU, menus);
            foreach (TRibbonContextMenu menu in menus)
                AddContextMenu(node, menu);
        }

        private void AddContextPopup(TRibbonViewContextPopup popup)
        {
            TreeNode root;
            bool originalSelectedAddedNode;

            root = AddNode(null, RS_CONTEXT_POPUP, II_CONTEXT_POPUP, popup);
            originalSelectedAddedNode = _selectAddedNode;
            _selectAddedNode = false;
            try
            {
                AddMiniToolbars(root, popup.MiniToolbars);
                AddContextMenus(root, popup.ContextMenus);
                AddContextMaps(root, popup.ContextMaps);
                root.Expand();
            }
            finally
            {
                _selectAddedNode = originalSelectedAddedNode;
            }
        }

        private void AddContextualTabs(TreeNode parent,
            TRibbonList<TRibbonTabGroup> tabs)
        {
            TreeNode node;

            node = AddNode(parent, RS_CONTEXTUAL_TABS, II_CONTEXTUAL_TAB, tabs);
            foreach (TRibbonTabGroup tabGroup in tabs)
                AddTabGroup(node, tabGroup);
        }

        public void AddControl(TreeNode parent,
            TRibbonControl control)
        {
            TreeNode node, child;
            bool originalSelectedAddedNode;

            int imageIndex = (int)control.ObjectType();
            if (imageIndex >= (int)RibbonObjectType.QatComboBox) //hack
                imageIndex = ConvertQatImageIndex[imageIndex - (int)RibbonObjectType.QatComboBox];

            node = AddNode(parent, control.DisplayName(), imageIndex, control);
            originalSelectedAddedNode = _selectAddedNode;
            _selectAddedNode = false;
            try
            {
                switch (control.ObjectType())
                {
                    case RibbonObjectType.SplitButton:
                        {
                            TRibbonSplitButton splitButton = control as TRibbonSplitButton;
                            child = AddNode(node, RS_BUTTON_ITEM, II_BUTTON_ITEM, null);
                            if ((splitButton.ButtonItem) != null)
                                AddControl(child, splitButton.ButtonItem);

                            child = AddNode(node, RS_ITEMS, II_LIST, splitButton.Controls);
                            foreach (TRibbonControl subControl in splitButton.Controls)
                                AddControl(child, subControl);
                            foreach (TRibbonMenuGroup menuGroup in splitButton.MenuGroups)
                                AddMenuGroup(child, menuGroup);
                            break;
                        }

                    case RibbonObjectType.DropDownButton:
                        {
                            TRibbonDropDownButton dropDownButton = control as TRibbonDropDownButton;
                            foreach (TRibbonControl subControl in dropDownButton.Controls)
                                AddControl(node, subControl);
                            foreach (TRibbonMenuGroup menuGroup in dropDownButton.MenuGroups)
                                AddMenuGroup(node, menuGroup);
                            break;
                        }

                    case RibbonObjectType.DropDownGallery:
                    case RibbonObjectType.SplitButtonGallery:
                    case RibbonObjectType.InRibbonGallery:
                        {
                            TRibbonGallery gallery = control as TRibbonGallery;
                            foreach (TRibbonControl subControl in gallery.Controls)
                                AddControl(node, subControl);
                            foreach (TRibbonMenuGroup menuGroup in gallery.MenuGroups)
                                AddMenuGroup(node, menuGroup);
                            break;
                        }

                    case RibbonObjectType.ControlGroup:
                        TRibbonControlGroup controlGroup = control as TRibbonControlGroup;
                        foreach (TRibbonControl subControl in controlGroup.Controls)
                            AddControl(node, subControl);
                        break;
                }
            }
            finally
            {
                _selectAddedNode = originalSelectedAddedNode;
            }
        }

        private void AddGroup(TreeNode parent,
            TRibbonGroup group)
        {
            TreeNode node;

            node = AddNode(parent, group.DisplayName(), II_GROUP, group);
            if ((group.BasicSizeDefinition == RibbonBasicSizeDefinition.Advanced) && (group.SizeDefinition != null))
                AddSizeDefinition(node, group.SizeDefinition);
            foreach (TRibbonControl control in group.Controls)
                AddControl(node, control);
        }

        private void AddGroupSizeDefinition(TreeNode parent,
            TRibbonGroupSizeDefinition sizeDef)
        {
            TreeNode node;

            node = AddNode(parent, sizeDef.DisplayName(), II_GROUP_SIZE_DEF, sizeDef);
            foreach (TRibbonGroupSizeDefinitionElement element in sizeDef.Elements)
                AddSizeDefElement(node, element);
        }

        private void AddMenuGroup(TreeNode parent,
            TRibbonMenuGroup group)
        {
            TreeNode node;

            node = AddNode(parent, group.DisplayName(), II_MENU_GROUP, group);
            foreach (TRibbonControl control in group.Controls)
                AddControl(node, control);
        }

        private void AddMiniToolbar(TreeNode parent,
            TRibbonMiniToolbar toolbar)
        {
            TreeNode node;
            bool originalSelectedAddedNode;

            node = AddNode(parent, toolbar.DisplayName(), II_MINI_TOOLBAR, toolbar);
            originalSelectedAddedNode = _selectAddedNode;
            _selectAddedNode = false;
            try
            {
                foreach (TRibbonMiniToolbarMenuGroup group in toolbar.MenuGroups)
                    AddMenuGroup(node, group);
            }
            finally
            {
                _selectAddedNode = originalSelectedAddedNode;
            }
        }

        private void AddMiniToolbars(TreeNode parent,
            TRibbonList<TRibbonMiniToolbar> toolbars)
        {
            TreeNode node;

            node = AddNode(parent, RS_MINI_TOOLBARS, II_MINI_TOOLBAR, toolbars);
            foreach (TRibbonMiniToolbar toolbar in toolbars)
                AddMiniToolbar(node, toolbar);
        }

        private TRibbonObject AddNewObject(RibbonObjectType objType)
        {
            TreeNode node;
            TRibbonObject obj;

            TRibbonObject result = null;
            node = treeViewRibbon.SelectedNode;
            if ((node == null) || (node.Tag == null))
                return result;

            obj = (TRibbonObject)node.Tag;
            if (obj.ObjectType() == RibbonObjectType.List)
            {
                if ((node.Parent == null) || (node.Parent.Tag == null))
                    return result;
                obj = (TRibbonObject)node.Parent.Tag;
            }

            result = obj.AddNew(objType);
            if (result == null)
                return result;

            if (result.ObjectType() == RibbonObjectType.Tab)
                AddTab(node, (TRibbonTab)(result));
            else if (result is TRibbonControl)
                AddControl(node, (TRibbonControl)(result));
            else
            {
                switch (result.ObjectType())
                {
                    case RibbonObjectType.AppMenuGroup:
                        AddAppMenuGroup(node, result as TRibbonAppMenuGroup);
                        break;
                    case RibbonObjectType.MenuGroup:
                        AddMenuGroup(node, result as TRibbonMenuGroup);
                        break;
                    case RibbonObjectType.MiniToolbarMenuGroup:
                        AddMenuGroup(node, result as TRibbonMiniToolbarMenuGroup);
                        break;
                    case RibbonObjectType.RibbonSizeDefinition:
                        AddRibbonSizeDefinition(node, result as TRibbonRibbonSizeDefinition);
                        break;
                    case RibbonObjectType.GroupSizeDefinition:
                        AddGroupSizeDefinition(node, result as TRibbonGroupSizeDefinition);
                        break;
                    case RibbonObjectType.ControlSizeDefinition:
                    case RibbonObjectType.ControlSizeGroup:
                    case RibbonObjectType.ColumnBreak:
                    case RibbonObjectType.Row:
                        AddSizeDefElement(node, result as TRibbonGroupSizeDefinitionElement);
                        break;
                    case RibbonObjectType.Scale:
                        AddScale(node, result as TRibbonScale);
                        break;
                    case RibbonObjectType.MiniToolbar:
                        AddMiniToolbar(node, result as TRibbonMiniToolbar);
                        break;
                    case RibbonObjectType.ContextMenu:
                        AddContextMenu(node, result as TRibbonContextMenu);
                        break;
                    case RibbonObjectType.ContextMap:
                        AddContextMap(node, result as TRibbonContextMap);
                        break;
                    default:
                        Debug.Assert(false);
                        break;
                }
            }

            Modified();
            return result;
        }

        private TreeNode AddNode(TreeNode parent, string caption,
            int imageIndex, Object data, bool first = false)
        {
            TreeNode result;
            if (first)
            {
                if (parent == null)
                {
                    result = new TreeNode(caption);
                    treeViewRibbon.Nodes.Add(result);
                }
                else
                    result = parent.Nodes.Insert(0, caption);
            }
            else
            {
                if (parent == null)
                {
                    result = new TreeNode(caption);
                    treeViewRibbon.Nodes.Add(result);
                }
                else
                    result = parent.Nodes.Add(caption);
            }
            result.ImageIndex = imageIndex;
            result.SelectedImageIndex = imageIndex;
            result.Tag = data;

            if (_selectAddedNode)
            {
                treeViewRibbon.SelectedNode = result;
                result.EnsureVisible();
            }
            return result;
        }

        private void AddQuickAccessToolbar(TreeNode parent,
            TRibbonQuickAccessToolbar qat)
        {
            TreeNode node;

            node = AddNode(parent, RS_QAT, II_QAT, qat);
            foreach (TRibbonControl control in qat.Controls)
                AddControl(node, control);
        }

        private void AddRibbonSizeDefinition(TreeNode parent,
            TRibbonRibbonSizeDefinition sizeDef)
        {
            TreeNode node;
            bool originalSelectedAddedNode;

            node = AddNode(parent, sizeDef.DisplayName(), II_SIZE_DEF, sizeDef);
            originalSelectedAddedNode = _selectAddedNode;
            _selectAddedNode = false;
            try
            {
                foreach (TRibbonGroupSizeDefinition groupSizeDef in sizeDef.GroupSizeDefinitions)
                    AddGroupSizeDefinition(node, groupSizeDef);
            }
            finally
            {
                _selectAddedNode = originalSelectedAddedNode;
            }
        }

        private void AddScale(TreeNode parent,
            TRibbonScale scale)
        {
            AddNode(parent, scale.DisplayName(), II_SCALE, scale);
        }

        private void AddScalingPolicy(TreeNode parent,
            TRibbonScalingPolicy scaling)
        {
            TreeNode node, child;

            node = AddNode(parent, RS_SCALING_POLICY, II_SCALING, scaling);

            child = AddNode(node, RS_IDEAL_SIZES, II_SCALING, scaling.IdealSizes);
            foreach (TRibbonScale scale in scaling.IdealSizes)
                AddNode(child, scale.DisplayName(), II_SCALE, scale);

            foreach (TRibbonScale scale in scaling.Scales)
                AddScale(node, scale);
        }

        private void AddSizeDefElement(TreeNode parent,
            TRibbonGroupSizeDefinitionElement element)
        {
            TreeNode node;
            TRibbonRow row = element as TRibbonRow;
            TRibbonControlSizeGroup group = element as TRibbonControlSizeGroup;

            node = AddNode(parent, element.DisplayName(), II_CONTROL_SIZE_DEF, element);

            if (element is TRibbonRow)
                foreach (TRibbonGroupSizeDefinitionElement subElement in row.Elements)
                    AddSizeDefElement(node, subElement);

            if (element is TRibbonControlSizeGroup)
                foreach (TRibbonControlSizeDefinition sizeDef in group.ControlSizeDefinitions)
                    AddSizeDefElement(node, sizeDef);
        }

        public void AddSizeDefinition(TreeNode parent,
            TRibbonSizeDefinition sizeDef)
        {
            TreeNode node;

            node = AddNode(parent, RS_SIZE_DEF, II_SIZE_DEF, sizeDef, true);
            foreach (TRibbonGroupSizeDefinition groupSizeDef in sizeDef.GroupSizeDefinitions)
                AddGroupSizeDefinition(node, groupSizeDef);
        }

        private void AddSizeDefinitions(TreeNode parent,
            TRibbonList<TRibbonRibbonSizeDefinition> sizeDefs)
        {
            TreeNode node;
            node = AddNode(parent, RS_SIZE_DEFS, II_SIZE_DEF, sizeDefs);
            foreach (TRibbonRibbonSizeDefinition sizeDef in sizeDefs)
                AddRibbonSizeDefinition(node, sizeDef);
        }

        private void AddTab(TreeNode parent, TRibbonTab tab)
        {
            TreeNode node;
            bool originalSelectedAddedNode;
            node = AddNode(parent, tab.DisplayName(), II_TAB, tab);
            originalSelectedAddedNode = _selectAddedNode;
            _selectAddedNode = false;
            try
            {
                AddScalingPolicy(node, tab.ScalingPolicy);
                foreach (TRibbonGroup group in tab.Groups)
                    AddGroup(node, group);
            }
            finally
            {
                _selectAddedNode = originalSelectedAddedNode;
            }
        }

        private void AddTabGroup(TreeNode parent,
            TRibbonTabGroup tabGroup)
        {
            TreeNode node;

            node = AddNode(parent, tabGroup.DisplayName(), II_CONTEXTUAL_TAB, tabGroup);
            foreach (TRibbonTab tab in tabGroup.Tabs)
                AddTab(node, tab);
        }

        private void AddTabs(TreeNode parent,
            TRibbonList<TRibbonTab> tabs)
        {
            foreach (TRibbonTab tab in tabs)
                AddTab(parent, tab);
        }

        public void ClearDocument()
        {
            treeViewRibbon.Nodes.Clear();
        }

        public void DeactivateFrame()
        {
            Program.ApplicationForm.ShortCutKeysHandler.Remove(_actionList);
            //@ different
            //_actionRemove.Shortcut = 0;
            //_actionMoveUp.Shortcut = 0;
            //_actionMoveDown.Shortcut = 0;
        }

        //private void Destroy()
        //        {
        //          _commands.Free;
        //}

        private bool GetObject(TreeNode node, out TRibbonObject obj)
        {
            object o;

            obj = null;
            bool result = (node != null) && (node.Tag != null);
            if (result)
            {
                o = node.Tag;
                result = (o is TRibbonObject);
                if (result)
                    obj = (TRibbonObject)(o);
            }
            return result;
        }

        public void Modified()
        {
            Program.ApplicationForm.Modified();
        }

        private void MoveNode(int direction)
        {
            TreeNode node, parentNode;
            //TreeNode sibling;
            TRibbonObject obj, parentObj;

            node = treeViewRibbon.SelectedNode;
            if ((node == null) || (node.Tag == null))
                return;
            obj = (TRibbonObject)node.Tag;

            parentNode = node.Parent;
            if ((parentNode == null) || (parentNode.Tag == null))
                return;
            parentObj = (TRibbonObject)parentNode.Tag;

            if (parentObj.ObjectType() == RibbonObjectType.List)
            {
                parentNode = parentNode.Parent;
                if ((parentNode == null) || (parentNode.Tag == null))
                    return;
                parentObj = (TRibbonObject)parentNode.Tag;
            }

            if (parentObj.Reorder(obj, direction))
            {
                if (direction < 0)
                    node.MoveUp();
                //sibling = node.PrevNode;
                else
                {
                    node.MoveDown();
                    //sibling = node.NextNode;
                    //if (sibling != null)
                    //    sibling = sibling.NextNode;
                }

                //@ different
                //if (sibling != null)
                //    node.MoveTo(sibling, NodeAction.Insert);
                //else
                //    node.MoveTo(node.NextNode, NodeAction.Add);

                _actionMoveUp.Enabled = (node.PrevNode != null);
                _actionMoveDown.Enabled = (node.NextNode != null);
                Modified();
            }
        }

        private void PopupMenuPopup(object sender, CancelEventArgs e)
        {
            TreeNode node;
            TRibbonObject obj;
            RibbonObjectType objType;
            RibbonObjectType parentObjType = RibbonObjectType.ViewRibbon;
            bool hasControls = false;
            bool hasMenuGroups = false;

            node = treeViewRibbon.SelectedNode;
            if ((node == null) || (node.Tag == null))
                objType = RibbonObjectType.Application;
            else
            {
                obj = (TRibbonObject)node.Tag;
                objType = obj.ObjectType();
                TRibbonCommandRefObject refObj = obj as TRibbonCommandRefObject;
                if (refObj != null && refObj.Parent is TRibbonAppMenuGroup)
                    parentObjType = RibbonObjectType.AppMenuGroup;
                TRibbonGallery gallery = refObj as TRibbonGallery;
                if (gallery != null)
                {
                    hasControls = gallery.Controls.Count > 0;
                    hasMenuGroups = gallery.MenuGroups.Count > 0;
                }
                TRibbonDropDownButton dropDown = refObj as TRibbonDropDownButton;
                if (dropDown != null)
                {
                    hasControls = dropDown.Controls.Count > 0;
                    hasMenuGroups = dropDown.MenuGroups.Count > 0;
                }
            }

            if (objType == RibbonObjectType.List)
            {
                if ((node == null) || (node.Parent == null) || (node.Parent.Tag == null))
                    objType = RibbonObjectType.Application;
                else
                {
                    obj = (TRibbonObject)node.Parent.Tag;
                    objType = obj.ObjectType();
                    TRibbonCommandRefObject refObj = obj as TRibbonCommandRefObject;
                    if (refObj != null && refObj.Parent is TRibbonAppMenuGroup)
                        parentObjType = RibbonObjectType.AppMenuGroup;
                    TRibbonSplitButton splitButton = refObj as TRibbonSplitButton;
                    if (splitButton != null)
                    {
                        hasControls = splitButton.Controls.Count > 0;
                        hasMenuGroups = splitButton.MenuGroups.Count > 0;
                    }
                }

                switch (objType)
                {
                    case RibbonObjectType.SplitButton:
                        objType = RibbonObjectType.SplitButton_Items;
                        break;
                    case RibbonObjectType.ViewRibbon:
                        if (node.ImageIndex == II_SIZE_DEF)
                            objType = RibbonObjectType.RibbonSizeDefinitions;
                        else if (node.ImageIndex == II_CONTEXTUAL_TAB)
                            objType = RibbonObjectType.ContextualTabs;
                        else
                            objType = RibbonObjectType.List;
                        break;
                    case RibbonObjectType.ViewContextPopup:
                        if (node.ImageIndex == II_MINI_TOOLBAR)
                            objType = RibbonObjectType.MiniToolbars;
                        else if (node.ImageIndex == II_CONTEXT_MENU)
                            objType = RibbonObjectType.ContextMenus;
                        else if (node.ImageIndex == II_CONTEXT_MAP)
                            objType = RibbonObjectType.ContextMaps;
                        else
                            objType = RibbonObjectType.List;
                        break;
                    case RibbonObjectType.ScalingPolicy:
                        objType = RibbonObjectType.ScalingPolicy_IdealSizes;
                        break;
                    default:
                        {
                            //@ todo: Abort
                            //Abort();
                            return;
                        }
                }
            }

            // Make sure Selected item stays selected when popup menu pops up

            //treeViewRibbon.SelectedNode = treeViewRibbon.SelectedNode;

            _actionAddButton.Visible = (objType == RibbonObjectType.MenuGroup || objType == RibbonObjectType.AppMenuGroup || objType == RibbonObjectType.MiniToolbarMenuGroup
                || objType == RibbonObjectType.Group || objType == RibbonObjectType.ControlGroup || ((objType == RibbonObjectType.SplitButton_Items || objType == RibbonObjectType.DropDownButton
                || ((objType == RibbonObjectType.DropDownGallery || objType == RibbonObjectType.SplitButtonGallery) && parentObjType != RibbonObjectType.AppMenuGroup) || objType == RibbonObjectType.InRibbonGallery) && !hasMenuGroups)); //@ added

            _actionAddToggleButton.Visible = (objType == RibbonObjectType.MenuGroup || objType == RibbonObjectType.MiniToolbarMenuGroup || objType == RibbonObjectType.Group
                || objType == RibbonObjectType.ControlGroup || ((objType == RibbonObjectType.SplitButton_Items || objType == RibbonObjectType.DropDownButton
                || ((objType == RibbonObjectType.DropDownGallery || objType == RibbonObjectType.SplitButtonGallery) && parentObjType != RibbonObjectType.AppMenuGroup) || objType == RibbonObjectType.InRibbonGallery) && !hasMenuGroups)); //@ added

            _actionAddDropDownButton.Visible = (objType == RibbonObjectType.MenuGroup || objType == RibbonObjectType.AppMenuGroup || objType == RibbonObjectType.MiniToolbarMenuGroup
                || objType == RibbonObjectType.Group || objType == RibbonObjectType.ControlGroup || ((objType == RibbonObjectType.SplitButton_Items || objType == RibbonObjectType.DropDownButton) && !hasMenuGroups));

            _actionAddSplitButton.Visible = (objType == RibbonObjectType.MenuGroup || objType == RibbonObjectType.AppMenuGroup || objType == RibbonObjectType.MiniToolbarMenuGroup
                || objType == RibbonObjectType.Group || objType == RibbonObjectType.ControlGroup || ((objType == RibbonObjectType.SplitButton_Items || objType == RibbonObjectType.DropDownButton
                || ((objType == RibbonObjectType.DropDownGallery || objType == RibbonObjectType.SplitButtonGallery) && parentObjType != RibbonObjectType.AppMenuGroup) || objType == RibbonObjectType.InRibbonGallery) && !hasMenuGroups)); //@ added

            _actionAddCheckBox.Visible = (objType == RibbonObjectType.MenuGroup || objType == RibbonObjectType.MiniToolbarMenuGroup || objType == RibbonObjectType.Group
                || objType == RibbonObjectType.ControlGroup || ((objType == RibbonObjectType.SplitButton_Items || objType == RibbonObjectType.DropDownButton
                || ((objType == RibbonObjectType.DropDownGallery || objType == RibbonObjectType.SplitButtonGallery) && parentObjType != RibbonObjectType.AppMenuGroup) || objType == RibbonObjectType.InRibbonGallery) && !hasMenuGroups)); //@ added

            _actionAddDropDownGallery.Visible = (objType == RibbonObjectType.MenuGroup || objType == RibbonObjectType.AppMenuGroup || objType == RibbonObjectType.MiniToolbarMenuGroup
                || objType == RibbonObjectType.Group || objType == RibbonObjectType.ControlGroup
                || ((objType == RibbonObjectType.SplitButton_Items || objType == RibbonObjectType.DropDownButton) && parentObjType != RibbonObjectType.AppMenuGroup && !hasMenuGroups));

            _actionAddSplitButtonGallery.Visible = (objType == RibbonObjectType.MenuGroup || objType == RibbonObjectType.AppMenuGroup || objType == RibbonObjectType.MiniToolbarMenuGroup
                || objType == RibbonObjectType.Group || objType == RibbonObjectType.ControlGroup
                || ((objType == RibbonObjectType.SplitButton_Items || objType == RibbonObjectType.DropDownButton) && parentObjType != RibbonObjectType.AppMenuGroup && !hasMenuGroups));

            _actionAddDropDownColorPicker.Visible = (objType == RibbonObjectType.MenuGroup || objType == RibbonObjectType.MiniToolbarMenuGroup || objType == RibbonObjectType.Group
                || objType == RibbonObjectType.ControlGroup || ((objType == RibbonObjectType.SplitButton_Items || objType == RibbonObjectType.DropDownButton) && !hasMenuGroups));

            _actionAddMenuGroup.Visible = (objType == RibbonObjectType.ApplicationMenu || objType == RibbonObjectType.ContextMenu || (objType == RibbonObjectType.SplitButton_Items || objType == RibbonObjectType.DropDownButton
                || ((objType == RibbonObjectType.DropDownGallery || objType == RibbonObjectType.SplitButtonGallery) && parentObjType != RibbonObjectType.AppMenuGroup) || objType == RibbonObjectType.InRibbonGallery) && !hasControls); //@ added

            _actionAddQatButton.Visible = (objType == RibbonObjectType.QuickAccessToolbar);
            _actionAddQatToggleButton.Visible = (objType == RibbonObjectType.QuickAccessToolbar);
            _actionAddQatCheckBox.Visible = (objType == RibbonObjectType.QuickAccessToolbar);
            _actionAddQatComboBox.Visible = (objType == RibbonObjectType.QuickAccessToolbar);
            _actionAddQatDropDownGallery.Visible = (objType == RibbonObjectType.QuickAccessToolbar);
            _actionAddQatSplitButtonGallery.Visible = (objType == RibbonObjectType.QuickAccessToolbar);
            _actionAddQatInRibbonGallery.Visible = (objType == RibbonObjectType.QuickAccessToolbar);

            _actionAddRibbonSizeDefinition.Visible = (objType == RibbonObjectType.RibbonSizeDefinitions);
            _actionAddGroupSizeDefinition.Visible = (objType == RibbonObjectType.RibbonSizeDefinition || objType == RibbonObjectType.SizeDefinition);
            _actionAddControlSizeDefinition.Visible = (objType == RibbonObjectType.GroupSizeDefinition || objType == RibbonObjectType.Row || objType == RibbonObjectType.ControlSizeGroup);
            _actionAddControlSizeGroup.Visible = (objType == RibbonObjectType.GroupSizeDefinition || objType == RibbonObjectType.Row);
            _actionAddColumnBreak.Visible = (objType == RibbonObjectType.GroupSizeDefinition);
            _actionAddRow.Visible = (objType == RibbonObjectType.GroupSizeDefinition);
            _actionAddGroup.Visible = (objType == RibbonObjectType.Tab);
            _actionAddScale.Visible = (objType == RibbonObjectType.ScalingPolicy || objType == RibbonObjectType.ScalingPolicy_IdealSizes);
            _actionAddControlGroup.Visible = (objType == RibbonObjectType.Group || objType == RibbonObjectType.ControlGroup);
            _actionAddComboBox.Visible = (objType == RibbonObjectType.MiniToolbarMenuGroup || objType == RibbonObjectType.Group || objType == RibbonObjectType.ControlGroup);
            _actionAddSpinner.Visible = (objType == RibbonObjectType.MiniToolbarMenuGroup || objType == RibbonObjectType.Group || objType == RibbonObjectType.ControlGroup);

            _actionAddInRibbonGallery.Visible = (objType == RibbonObjectType.Group || objType == RibbonObjectType.ControlGroup);
            _actionAddFontControl.Visible = (objType == RibbonObjectType.Group || objType == RibbonObjectType.ControlGroup);
            _actionAddFloatieFontControl.Visible = (objType == RibbonObjectType.MiniToolbarMenuGroup);
            _actionAddTab.Visible = (objType == RibbonObjectType.ViewRibbon || objType == RibbonObjectType.TabGroup);
            _actionAddTabGroup.Visible = (objType == RibbonObjectType.ContextualTabs);
            //  ActionAddContextPopup.Visible = (node.Level == 0);
            _actionAddMiniToolbar.Visible = (objType == RibbonObjectType.MiniToolbars);
            _actionAddContextMenu.Visible = (objType == RibbonObjectType.ContextMenus);
            _actionAddContextMap.Visible = (objType == RibbonObjectType.ContextMaps);
            _actionAddMiniToolbarMenuGroup.Visible = (objType == RibbonObjectType.MiniToolbar);

            //I think there is a bug in .NET when we have no visible items. After selecting an other node we have to click Add twice
        }

        public void ShowDocument(TRibbonDocument document)
        {
            TreeNode root;

            _document = document;
            if (_currentFrame != null)
                _currentFrame.Visible = false;
            _currentFrame = null;
            _currentNode = null;
            _currentObject = null;
            _selectAddedNode = false;
            treeViewRibbon.BeginUpdate();
            try
            {
                treeViewRibbon.Nodes.Clear();

                foreach (TRibbonView view in _document.Application.Views)
                {
                    if (view is TRibbonViewRibbon)
                    {
                        TRibbonViewRibbon ribbon = view as TRibbonViewRibbon;
                        root = AddNode(null, RS_RIBBON, II_RIBBON, ribbon);
                        AddApplicationMenu(root, ribbon.ApplicationMenu);
                        AddQuickAccessToolbar(root, ribbon.QuickAccessToolbar);
                        AddSizeDefinitions(root, ribbon.SizeDefinitions);
                        AddNode(root, RS_HELP_BUTTON, II_HELP_BUTTON, ribbon.HelpButton);
                        AddContextualTabs(root, ribbon.ContextualTabs);
                        AddTabs(root, ribbon.Tabs);
                        root.Expand();
                    }
                    else if (view is TRibbonViewContextPopup)
                        AddContextPopup((TRibbonViewContextPopup)(view));
                }
            }
            finally
            {
                treeViewRibbon.EndUpdate();
                _selectAddedNode = true;
            }
        }

        private void ChangeCurrentFrame(BaseFrame obj)
        {
            if (splitterRibbon.Panel2.Controls.Count > 0)
            {
                Control ctrl = splitterRibbon.Panel2.Controls[0];
                if (ctrl.Equals(obj))
                    return;
                splitterRibbon.Panel2.Controls.Remove(ctrl);
            }
            splitterRibbon.Panel2.Controls.Add(obj);
        }

        private BaseFrame AddCurrentFrame<T>() where T : BaseFrame, new()
        {
            const string namespaceString = "UIRibbonTools"; //@ hard coded
            const int dotLength = 1;
            int startOfClassName = namespaceString.Length + dotLength;
            string className = typeof(T).ToString().Substring(startOfClassName);
            ViewClasses viewClazz = (ViewClasses)Enum.Parse(typeof(ViewClasses), className);
            BaseFrame obj;
            if (_viewClasses.ContainsKey(viewClazz))
            {
                obj = _viewClasses[viewClazz];
                ChangeCurrentFrame(obj);
            }
            else
            {
                T obj1 = new T();
#if Core && SegoeFont
                obj1.Font = this.Font;
#endif
                _viewClasses[viewClazz] = obj1;
                obj = obj1;

                obj.Dock = DockStyle.Fill;
                obj.SetOwner(this);
                ChangeCurrentFrame(obj);

                IActivate frame = obj as IActivate;
                if (frame != null)
                    frame.ActivateFrame();
            }
            return obj;
        }

        private void TreeViewRibbonChange(object sender, TreeViewEventArgs e)
        {
            _actionRemove.Enabled = false;
            _actionMoveUp.Enabled = false;
            _actionMoveDown.Enabled = false;
            if ((e.Node == null) || (e.Node == _currentNode) || (!e.Node.IsSelected))
                return;

            if (_currentFrame != null)
            {
                _currentFrame.Visible = false;
                _currentFrame = null;
            }

            _currentNode = e.Node;
            _currentObject = (TRibbonObject)e.Node.Tag;
            if (_currentObject == null)
                return;

            _actionRemove.Enabled = true;

            _actionMoveUp.Enabled = _currentObject.CanReorder() && (e.Node.PrevNode != null);
            _actionMoveDown.Enabled = _currentObject.CanReorder() && (e.Node.NextNode != null);

            switch (_currentObject.ObjectType())
            {
                case RibbonObjectType.ApplicationMenu:
                    {
                        _currentFrame = AddCurrentFrame<TFrameApplicationMenu>();
                        _actionRemove.Enabled = false;
                        break;
                    }

                case RibbonObjectType.Button:
                    _currentFrame = AddCurrentFrame<TFrameButton>();
                    break;

                case RibbonObjectType.ToggleButton:
                    _currentFrame = AddCurrentFrame<TFrameToggleButton>();
                    break;
                case RibbonObjectType.SplitButton:
                    _currentFrame = AddCurrentFrame<TFrameSplitButton>();
                    break;
                case RibbonObjectType.DropDownButton:
                    _currentFrame = AddCurrentFrame<TFrameDropDownButton>();
                    break;
                case RibbonObjectType.QuickAccessToolbar:
                    {
                        _currentFrame = AddCurrentFrame<TFrameQuickAccessToolbar>();
                        _actionRemove.Enabled = false;
                        break;
                    }

                case RibbonObjectType.HelpButton:
                    {
                        _currentFrame = AddCurrentFrame<TFrameHelpButton>();
                        _actionRemove.Enabled = false;
                        break;
                    }

                case RibbonObjectType.Tab:
                    _currentFrame = AddCurrentFrame<TFrameTab>();
                    break;
                case RibbonObjectType.TabGroup:
                    _currentFrame = AddCurrentFrame<TFrameTabGroup>();
                    break;
                case RibbonObjectType.Group:
                    _currentFrame = AddCurrentFrame<TFrameGroup>();
                    break;
                case RibbonObjectType.QatButton:
                case RibbonObjectType.QatToggleButton:
                case RibbonObjectType.QatCheckBox:
                case RibbonObjectType.QatComboBox:
                case RibbonObjectType.QatDropDownGallery:
                case RibbonObjectType.QatSplitButtonGallery:
                case RibbonObjectType.QatInRibbonGallery:
                    _currentFrame = AddCurrentFrame<TFrameQatControl>();
                    break;
                case RibbonObjectType.DropDownGallery:
                    _currentFrame = AddCurrentFrame<TFrameDropDownGallery>();
                    break;
                case RibbonObjectType.SplitButtonGallery:
                    _currentFrame = AddCurrentFrame<TFrameSplitButtonGallery>();
                    break;
                case RibbonObjectType.InRibbonGallery:
                    _currentFrame = AddCurrentFrame<TFrameInRibbonGallery>();
                    break;
                case RibbonObjectType.MenuGroup:
                case RibbonObjectType.MiniToolbarMenuGroup:
                    _currentFrame = AddCurrentFrame<TFrameMenuGroup>();
                    break;
                case RibbonObjectType.AppMenuGroup:
                    _currentFrame = AddCurrentFrame<TFrameAppMenuGroup>();
                    break;
                case RibbonObjectType.Scale:
                    _currentFrame = AddCurrentFrame<TFrameScale>();
                    break;
                case RibbonObjectType.ViewRibbon:
                    {
                        _currentFrame = AddCurrentFrame<TFrameViewRibbon>();
                        _actionRemove.Enabled = false;
                        break;
                    }

                case RibbonObjectType.SizeDefinition:
                case RibbonObjectType.RibbonSizeDefinition:
                    _currentFrame = AddCurrentFrame<TFrameSizeDefinition>();
                    break;
                case RibbonObjectType.GroupSizeDefinition:
                    _currentFrame = AddCurrentFrame<TFrameGroupSizeDefinition>();
                    break;
                case RibbonObjectType.ControlSizeDefinition:
                    _currentFrame = AddCurrentFrame<TFrameControlSizeDefinition>();
                    break;
                case RibbonObjectType.ColumnBreak:
                    _currentFrame = AddCurrentFrame<TFrameColumnBreak>();
                    break;
                case RibbonObjectType.FloatieFontControl:
                    _currentFrame = AddCurrentFrame<TFrameFloatieFontControl>();
                    break;
                case RibbonObjectType.FontControl:
                    _currentFrame = AddCurrentFrame<TFrameFontControl>();
                    break;
                case RibbonObjectType.ControlGroup:
                    _currentFrame = AddCurrentFrame<TFrameControlGroup>();
                    break;
                case RibbonObjectType.ComboBox:
                    _currentFrame = AddCurrentFrame<TFrameComboBox>();
                    break;
                case RibbonObjectType.CheckBox:
                    _currentFrame = AddCurrentFrame<TFrameCheckBox>();
                    break;
                case RibbonObjectType.DropDownColorPicker:
                    _currentFrame = AddCurrentFrame<TFrameDropDownColorPicker>();
                    break;
                case RibbonObjectType.Spinner:
                    _currentFrame = AddCurrentFrame<TFrameSpinner>();
                    break;
                case RibbonObjectType.MiniToolbar:
                    _currentFrame = AddCurrentFrame<TFrameMiniToolbar>();
                    break;
                case RibbonObjectType.ContextMenu:
                    _currentFrame = AddCurrentFrame<TFrameContextMenu>();
                    break;
                case RibbonObjectType.ContextMap:
                    _currentFrame = AddCurrentFrame<TFrameContextMap>();
                    break;
                case RibbonObjectType.List:
                case RibbonObjectType.ViewContextPopup:
                    {
                        _actionRemove.Enabled = false;
                        // No properties
                        return;
                    }

                case RibbonObjectType.Row:
                case RibbonObjectType.ControlSizeGroup:
                case RibbonObjectType.ScalingPolicy:
                    // No properties
                    return;
            }

            if (_currentFrame != null)
            {
                _currentFrame.ShowProperties(_currentObject, _currentNode);
                _currentFrame.Visible = true;
            }
            else
                Debug.Assert(false);
        }

        public void UpdateCurrentNode()
        {
            if (_currentNode != null)
                UpdateTreeNodeCaption(_currentNode, false);
        }

        private void UpdateTreeNodeCaption(TreeNode node,
            bool recursive)
        {
            TRibbonObject obj;

            obj = (TRibbonObject)node.Tag;
            if (obj != null)
            {
                if (obj is TRibbonControl)
                {
                    TRibbonControl control = obj as TRibbonControl;
                    RibbonObjectType type = control.ObjectType();
                    if (!(type == RibbonObjectType.QuickAccessToolbar || type == RibbonObjectType.HelpButton || type == RibbonObjectType.ApplicationMenu))
                        node.Text = control.DisplayName();
                }
                else if (obj.ObjectType() != RibbonObjectType.List)
                    node.Text = obj.DisplayName();
            }

            if (recursive)
                for (int i = 0; i < node.Nodes.Count; i++)
                    UpdateTreeNodeCaption(node.Nodes[i], true);
        }

        private void ActionAddButtonExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.Button);
        }

        private void ActionAddCheckBoxExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.CheckBox);
        }

        private void ActionAddColumnBreakExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.ColumnBreak);
        }

        private void ActionAddComboBoxExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.ComboBox);
        }

        private void ActionAddContextMapExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.ContextMap);
        }

        private void ActionAddContextMenuExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.ContextMenu);
        }

        private void ActionAddContextPopupExecute(object sender, EventArgs e)
        {
            // TRibbonViewContextPopup popup;
            // popup = _document.Application.AddNew(RibbonObjectType.ViewContextPopup) as TRibbonViewContextPopup;
            // AddContextPopup(popup);
            // Modified();
        }

        private void ActionAddControlGroupExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.ControlGroup);
        }

        private void ActionAddControlSizeDefinitionExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.ControlSizeDefinition);
        }

        private void ActionAddControlSizeGroupExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.ControlSizeGroup);
        }

        private void ActionAddDropDownButtonExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.DropDownButton);
        }

        private void ActionAddDropDownColorPickerExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.DropDownColorPicker);
        }

        private void ActionAddDropDownGalleryExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.DropDownGallery);
        }

        private void ActionAddFloatieFontControlExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.FloatieFontControl);
        }

        private void ActionAddFontControlExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.FontControl);
        }

        private void ActionAddGroupExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.Group);
        }

        private void ActionAddGroupSizeDefinitionExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.GroupSizeDefinition);
        }

        private void ActionAddInRibbonGalleryExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.InRibbonGallery);
        }

        private void ActionAddMenuGroupExecute(object sender, EventArgs e)
        {
            TreeNode node;
            TRibbonObject obj;

            node = treeViewRibbon.SelectedNode;
            if ((node == null) || (node.Tag == null))
                return;

            obj = (TRibbonObject)node.Tag;
            switch (obj.ObjectType())
            {
                case RibbonObjectType.ApplicationMenu:
                    AddNewObject(RibbonObjectType.AppMenuGroup);
                    break;
                default:
                    AddNewObject(RibbonObjectType.MenuGroup);
                    break;
            }
        }

        private void ActionAddMiniToolbarExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.MiniToolbar);
        }

        private void ActionAddMiniToolbarMenuGroupExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.MiniToolbarMenuGroup);
        }

        private void ActionAddQatButtonExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.QatButton);
        }

        private void ActionAddQatCheckBoxExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.QatCheckBox);
        }

        private void ActionAddQatComboBoxExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.QatComboBox);
        }

        private void ActionAddQatDropDownGalleryExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.QatDropDownGallery);
        }

        private void ActionAddQatSplitButtonGalleryExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.QatSplitButtonGallery);
        }

        private void ActionAddQatInRibbonGalleryExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.QatInRibbonGallery);
        }

        private void ActionAddQatToggleButtonExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.QatToggleButton);
        }

        private void ActionAddRibbonSizeDefinitionExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.RibbonSizeDefinition);
        }

        private void ActionAddRowExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.Row);
        }

        private void ActionAddScaleExecute(object sender, EventArgs e)
        {
            TreeNode node;
            TRibbonObject obj;
            TRibbonScalingPolicy scalingPolicy;
            TRibbonScale scale;

            node = treeViewRibbon.SelectedNode;
            if (GetObject(node, out obj))
            {
                if (obj.ObjectType() == RibbonObjectType.List)
                {
                    if (GetObject(node.Parent, out obj) && (obj is TRibbonScalingPolicy))
                    {
                        scalingPolicy = obj as TRibbonScalingPolicy;
                        scale = scalingPolicy.AddIdealSize();
                        AddScale(node, scale);
                        Modified();
                        return;
                    }
                }
            }
            AddNewObject(RibbonObjectType.Scale);
        }

        private void ActionAddSpinnerExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.Spinner);
        }

        private void ActionAddSplitButtonExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.SplitButton);
        }

        private void ActionAddSplitButtonGalleryExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.SplitButtonGallery);
        }

        private void ActionAddTabExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.Tab);
        }

        private void ActionAddTabGroupExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.TabGroup);
        }

        private void ActionAddToggleButtonExecute(object sender, EventArgs e)
        {
            AddNewObject(RibbonObjectType.ToggleButton);
        }

        private void ActionRemoveExecute(object sender, EventArgs e)
        {
            TreeNode node;
            TRibbonObject obj, parentObj;

            node = treeViewRibbon.SelectedNode;
            if (!GetObject(node, out obj))
                return;

            if (node.Nodes.Count > 0)
            {
                if (MessageBox.Show(RS_REMOVE_ITEM_MESSAGE, RS_REMOVE_ITEM_HEADER, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)

                    return;
            }

            if (!GetObject(node.Parent, out parentObj))
                return;

            if (parentObj.ObjectType() == RibbonObjectType.List)
            {
                if (!GetObject(node.Parent.Parent, out parentObj))
                    return;
            }

            if (parentObj.Remove(obj))
            {
                node.Remove();
                Modified();
            }
        }

        private void ActionMoveDownExecute(object sender, EventArgs e)
        {
            MoveNode(1);
        }

        private void ActionMoveUpExecute(object sender, EventArgs e)
        {
            MoveNode(-1);
        }

        private void TreeActionUpdate(object sender, EventArgs e)
        {
            (sender as TAction).Enabled = (treeViewRibbon.SelectedNode != null);
        }

        public void ActivateFrame()
        {
            if (treeViewRibbon.Nodes.Count == 0)
                return; // Nothing to do here

            Program.ApplicationForm.ShortCutKeysHandler.Add(_actionList);
            //@ changed
            //_actionRemove.ShortCut = ShortCut(VK_DELETE, [ssCtrl]);
            //_actionMoveUp.ShortCut = ShortCut(VK_UP, [ssCtrl]);
            //_actionMoveDown.ShortCut = ShortCut(VK_DOWN, [ssCtrl]); ;

            treeViewRibbon.BeginUpdate();
            UpdateTreeNodeCaption(treeViewRibbon.Nodes[0], true);
            treeViewRibbon.EndUpdate();

            if (_currentNode == null) //@ added begin
                treeViewRibbon.SelectedNode = treeViewRibbon.Nodes[0];
            else
                treeViewRibbon.SelectedNode = _currentNode;
            treeViewRibbon.Select(); //@ added end

            _commands.Clear();
            _commands.Add(new RibbonCommandItem(RS_NONE, null));
            foreach (TRibbonCommand command in _document.Application.Commands)
                _commands.Add(new RibbonCommandItem(command.DisplayName(), command));

            _commands.Sort();

            //@ different implementation
            foreach (var clazz in _viewClasses.Values) //caching of ViewFrameClasses
            {
                IActivate frame = clazz as IActivate;
                if (frame != null)
                    frame.ActivateFrame();
            }
        }
    }
}
