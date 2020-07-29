using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIRibbonTools
{
    partial class TRibbonObject
    {
        public const string RS_NO_APPLICATION = "Ribbon XML must have an <Application> root element.";
        public const string RS_INVALID_COMMANDS = "There can be at most 1 <Application.Commands> element.";
        public const string RS_INVALID_VIEWS = "There must be exactly 1 <Application.Views> element.";
        public const string RS_INVALID_COMMAND_NAME = "Invalid command name {0}. Name must start with a letter or underscore and contain only letters, digits or underscores after that.";
        public const string RS_INVALID_SYMBOL = "Invalid symbol {0}. Symbol must start with a letter or underscore and contain only letters, digits or underscores after that.";
        public const string RS_ELEMENT_EXPECTED = "Element <{0}> expected, but found <{1}>.";
        public const string RS_INVALID_ID = "Id ({0:D}) must have a value between 2 and 59999.";
        public const string RS_MULTIPLE_RIBBON_STRINGS = "Element can have at most 1 <String> child element.";
        public const string RS_UNSUPPORTED_CHILD_ELEMENT = "Child element <{0}> is not allowed within element <{1}>.";
        public const string RS_INVALID_DPI_VALUE = "MinDPI value must be at least 96.";
        public const string RS_MULTIPLE_ELEMENTS = "Element <{0}> can have at most 1 <{1}> child element.";
        public const string RS_SINGLE_ELEMENT = "Element <{0}> must contain exactly 1 <{1}> child element.";
        public const string RS_REQUIRED_ELEMENT = "Element <{0}> must have at least 1 <{1}> child element.";
        public const string RS_INVALID_GROUP_SPACING = "Invalid GroupSpacing attribute value.";
        public const string RS_INVALID_CATEGORY_CLASS = "Invalid Class attribute value.";
        public const string RS_INVALID_APPLICATION_MODE = "Invalid ApplicationModes attribute value.";
        public const string RS_INVALID_BUTTON_ITEM = "<SplitButton.ButtonItem> must reference a Button or ToggleButton.";
        public const string RS_INVALID_SPLITBUTTON = "A <SplitButton> must either have a <SplitButton.MenuGroups> element or one or more control elements.";
        public const string RS_INVALID_SIZE = "Invalid Size attribute value.";
        public const string RS_INVALID_FONT_TYPE = "Invalid FontType attribute value.";
        public const string RS_INVALID_DROPDOWN_BUTTON = "A <DropDownButton> must either have one or more <MenuGroup> elements or control elements.";
        public const string RS_INVALID_COLOR_TEMPLATE = "Invalid ColorTemplate attribute value.";
        public const string RS_INVALID_CHIP_SIZE = "Invalid ChipSize attribute value.";
        public const string RS_INVALID_TYPE = "Invalid Type attribute value.";
        public const string RS_INVALID_TEXT_POSITION = "Invalid TextPosition attribute value.";
        public const string RS_INVALID_GRIPPER = "Invalid Gripper attribute value.";
        public const string RS_INVALID_GROUP_SIZE_DEFINITIONS = "<SizeDefinition> element must contain between 1 and 3 <GroupSizeDefinition> child elements.";
        public const string RS_INVALID_IMAGE_SIZE = "Invalid ImageSize attribute value.";
        public const string RS_INVALID_RESIZE_TYPE = "Invalid ResizeType attribute value.";
        public const string RS_INVALID_GALLERY = "A Gallery must either have a <MenuGroups> element or one or more control elements.";
        public const string RS_CANNOT_ADD_MENU_GROUP_TO_SPLIT_BUTTON = "Cannot add a menu group to a split button that already contains controls. A split button must either contain menu groups or controls.";
        public const string RS_CANNOT_ADD_MENU_GROUP_TO_DROP_DOWN_BUTTON = "Cannot add a menu group to a drop-down button that already contains controls. A drop-down button must either contain menu groups or controls.";
        public const string RS_CANNOT_ADD_MENU_GROUP_TO_GALLERY = "Cannot add a menu group to a gallery that already contains controls. A gallery must either contain menu groups or controls."; //@ added
        public const string RS_CANNOT_ADD_CONTROL_TO_SPLIT_BUTTON = "Cannot add a control to a split button that already contains menu groups. A split button must either contain menu groups or controls.";
        public const string RS_CANNOT_ADD_CONTROL_TO_DROP_DOWN_BUTTON = "Cannot add a control to a drop-down button that already contains menu groups. A drop-down button must either contain menu groups or controls.";
        public const string RS_CANNOT_ADD_CONTROL_TO_GALLERY = "Cannot add a control to a gallery that already contains menu groups. A gallery must either contain menu groups or controls."; //@ added
        public const string RS_MAX_GROUP_SIZE_DEF = "You can add at most 3 groups to a size definition.";
        public const string RS_MIN_GROUP_SIZE_DEF = "There must be at least 1 group in a size definition.";
        public const string RS_MIN_MINI_TOOLBAR = "There must be at least 1 menu group in a mini toolbar.";
        public const string RS_MIN_CONTEXT_MENU = "There must be at least 1 menu group in a context menu.";
        public const string RS_SIZE_DEF = "Size Definition";
        public const string RS_GROUP = "Group";
        public const string RS_CONTROL = "Control";
        public const string RS_ROW = "Row";
        public const string RS_COLUMN_BREAK = "Column Break";
        public const string RS_CONTROL_GROUP = "Control Group";
        public const string RS_CONTEXT_MENU = "Context Menu";
        public const string RS_MINI_TOOLBAR = "Mini Toolbar";
        public const string RS_CONTEXT_MAP = "Context Map";
        public const string RS_SCALING_POLICY = "Scaling Policy";

        
        public const string RIBBON_NAMESPACE = "http://schemas.microsoft.com/windows/2009/Ribbon";

        // Element Names
        public const string EN_APPLICATION = "Application";
        public const string EN_APPLICATION_COMMANDS = "Application.Commands";
        public const string EN_APPLICATION_VIEWS = "Application.Views";
        public const string EN_STRING = "String";
        public const string EN_STRING_CONTENT = "String.Content";
        public const string EN_STRING_ID = "String.Id";
        public const string EN_STRING_SYMBOL = "String.Symbol";
        public const string EN_COMMAND = "Command";
        public const string EN_COMMAND_NAME = "Command.Name";
        public const string EN_COMMAND_SYMBOL = "Command.Symbol";
        public const string EN_COMMAND_ID = "Command.Id";
        public const string EN_COMMAND_LABEL_TITLE = "Command.LabelTitle";
        public const string EN_COMMAND_LABEL_DESCRIPTION = "Command.LabelDescription";
        public const string EN_COMMAND_KEYTIP = "Command.Keytip";
        public const string EN_COMMAND_TOOLTIP_TITLE = "Command.TooltipTitle";
        public const string EN_COMMAND_TOOLTIP_DESCRIPTION = "Command.TooltipDescription";
        public const string EN_COMMAND_SMALL_IMAGES = "Command.SmallImages";
        public const string EN_COMMAND_LARGE_IMAGES = "Command.LargeImages";
        public const string EN_COMMAND_SMALL_HIGH_CONTRAST_IMAGES = "Command.SmallHighContrastImages";
        public const string EN_COMMAND_LARGE_HIGH_CONTRAST_IMAGES = "Command.LargeHighContrastImages";
        public const string EN_COMMAND_COMMENT = "Command.Comment";
        public const string EN_IMAGE = "Image";
        public const string EN_IMAGE_SOURCE = "Image.Source";
        public const string EN_RIBBON = "Ribbon";
        public const string EN_RIBBON_SIZE_DEFINITIONS = "Ribbon.SizeDefinitions";
        public const string EN_RIBBON_APPLICATION_MENU = "Ribbon.ApplicationMenu";
        public const string EN_RIBBON_HELP_BUTTON = "Ribbon.HelpButton";
        public const string EN_RIBBON_TABS = "Ribbon.Tabs";
        public const string EN_RIBBON_CONTEXTUAL_TABS = "Ribbon.ContextualTabs";
        public const string EN_RIBBON_QUICK_ACCESS_TOOLBAR = "Ribbon.QuickAccessToolbar";
        public const string EN_CONTEXT_POPUP = "ContextPopup";
        public const string EN_CONTEXT_POPUP_MINI_TOOLBARS = "ContextPopup.MiniToolbars";
        public const string EN_CONTEXT_POPUP_CONTEXT_MENUS = "ContextPopup.ContextMenus";
        public const string EN_CONTEXT_POPUP_CONTEXT_MAPS = "ContextPopup.ContextMaps";
        public const string EN_MINI_TOOLBAR = "MiniToolbar";
        public const string EN_CONTEXT_MENU = "ContextMenu";
        public const string EN_CONTEXT_MAP = "ContextMap";
        public const string EN_APPLICATION_MENU = "ApplicationMenu";
        public const string EN_APPLICATION_MENU_RECENT_ITEMS = "ApplicationMenu.RecentItems";
        public const string EN_RECENT_ITEMS = "RecentItems";
        public const string EN_MENU_GROUP = "MenuGroup";
        public const string EN_BUTTON = "Button";
        public const string EN_SPLIT_BUTTON = "SplitButton";
        public const string EN_SPLIT_BUTTON_BUTTON_ITEM = "SplitButton.ButtonItem";
        public const string EN_SPLIT_BUTTON_MENU_GROUPS = "SplitButton.MenuGroups";
        public const string EN_DROP_DOWN_BUTTON = "DropDownButton";
        public const string EN_DROP_DOWN_GALLERY = "DropDownGallery";
        public const string EN_DROP_DOWN_GALLERY_MENU_LAYOUT = "DropDownGallery.MenuLayout";
        public const string EN_DROP_DOWN_GALLERY_MENU_GROUPS = "DropDownGallery.MenuGroups";
        public const string EN_SPLIT_BUTTON_GALLERY = "SplitButtonGallery";
        public const string EN_SPLIT_BUTTON_GALLERY_MENU_LAYOUT = "SplitButtonGallery.MenuLayout";
        public const string EN_SPLIT_BUTTON_GALLERY_MENU_GROUPS = "SplitButtonGallery.MenuGroups";
        public const string EN_TOGGLE_BUTTON = "ToggleButton";
        public const string EN_CHECK_BOX = "CheckBox";
        public const string EN_DROP_DOWN_COLOR_PICKER = "DropDownColorPicker";
        public const string EN_HELP_BUTTON = "HelpButton";
        public const string EN_QUICK_ACCESS_TOOLBAR = "QuickAccessToolbar";
        public const string EN_QUICK_ACCESS_TOOLBAR_APPLICATION_DEFAULTS = "QuickAccessToolbar.ApplicationDefaults";
        public const string EN_TAB = "Tab";
        public const string EN_TAB_SCALING_POLICY = "Tab.ScalingPolicy";
        public const string EN_GROUP = "Group";
        public const string EN_SCALING_POLICY = "ScalingPolicy";
        public const string EN_SCALING_POLICY_IDEAL_SIZES = "ScalingPolicy.IdealSizes";
        public const string EN_SCALE = "Scale";
        public const string EN_SIZE_DEFINITION = "SizeDefinition";
        public const string EN_CONTROL_GROUP = "ControlGroup";
        public const string EN_COMBO_BOX = "ComboBox";
        public const string EN_SPINNER = "Spinner";
        public const string EN_IN_RIBBON_GALLERY = "InRibbonGallery";
        public const string EN_IN_RIBBON_GALLERY_MENU_LAYOUT = "InRibbonGallery.MenuLayout";
        public const string EN_IN_RIBBON_GALLERY_MENU_GROUPS = "InRibbonGallery.MenuGroups";
        public const string EN_FONT_CONTROL = "FontControl";
        public const string EN_VERTICAL_MENU_LAYOUT = "VerticalMenuLayout";
        public const string EN_FLOW_MENU_LAYOUT = "FlowMenuLayout";
        public const string EN_CONTROL_NAME_MAP = "ControlNameMap";
        public const string EN_CONTROL_NAME_DEFINITION = "ControlNameDefinition";
        public const string EN_GROUP_SIZE_DEFINITION = "GroupSizeDefinition";
        public const string EN_CONTROL_SIZE_DEFINITION = "ControlSizeDefinition";
        public const string EN_COLUMN_BREAK = "ColumnBreak";
        public const string EN_ROW = "Row";
        public const string EN_TAB_GROUP = "TabGroup";

        // Attribute Names
        public const string AN_XMLNS = "xmlns";
        public const string AN_NAME = "Name";
        public const string AN_SYMBOL = "Symbol";
        public const string AN_ID = "Id";
        public const string AN_CONTENT = "Content";
        public const string AN_COMMENT = "Comment";
        public const string AN_LABEL_TITLE = "LabelTitle";
        public const string AN_LABEL_DESCRIPTION = "LabelDescription";
        public const string AN_TOOLTIP_TITLE = "TooltipTitle";
        public const string AN_TOOLTIP_DESCRIPTION = "TooltipDescription";
        public const string AN_KEYTIP = "Keytip";
        public const string AN_SOURCE = "Source";
        public const string AN_MIN_DPI = "MinDPI";
        public const string AN_GROUP_SPACING = "GroupSpacing";
        public const string AN_COMMAND_NAME = "CommandName";
        public const string AN_MAX_COUNT = "MaxCount";
        public const string AN_ENABLE_PINNING = "EnablePinning";
        public const string AN_CLASS = "Class";
        public const string AN_APPLICATION_MODES = "ApplicationModes";
        public const string AN_CUSTOMIZE_COMMAND_NAME = "CustomizeCommandName";
        public const string AN_APPLICATION_DEFAULTS_IS_CHECKED = "ApplicationDefaults.IsChecked";
        public const string AN_GROUP = "Group";
        public const string AN_SIZE = "Size";
        public const string AN_SIZE_DEFINITION = "SizeDefinition";
        public const string AN_FONT_TYPE = "FontType";
        public const string AN_IS_STRIKETHROUGH_BUTTON_VISIBLE = "IsStrikethroughButtonVisible";
        public const string AN_IS_UNDERLINE_BUTTON_VISIBLE = "IsUnderlineButtonVisible";
        public const string AN_IS_HIGHLIGHT_BUTTON_VISIBLE = "IsHighlightButtonVisible";
        public const string AN_SHOW_TRUE_TYPE_ONLY = "ShowTrueTypeOnly";
        public const string AN_SHOW_VERTICAL_FONTS = "ShowVerticalFonts";
        public const string AN_IsGrowShrinkButtonGroupVisible = "IsGrowShrinkButtonGroupVisible";
        public const string AN_MINIMUM_FONT_SIZE = "MinimumFontSize";
        public const string AN_MAXIMUM_FONT_SIZE = "MaximumFontSize";
        public const string AN_SEQUENCE_NUMBER = "SequenceNumber";
        public const string AN_MINI_TOOLBAR = "MiniToolbar";
        public const string AN_CONTEXT_MENU = "ContextMenu";
        public const string AN_COLOR_TEMPLATE = "ColorTemplate";
        public const string AN_CHIP_SIZE = "ChipSize";
        public const string AN_COLUMNS = "Columns";
        public const string AN_THEME_COLOR_GRID_ROWS = "ThemeColorGridRows";
        public const string AN_STANDARD_COLOR_GRID_ROWS = "StandardColorGridRows";
        public const string AN_RECENT_COLOR_GRID_ROWS = "RecentColorGridRows";
        public const string AN_IS_AUTOMATIC_COLOR_BUTTON_VISIBLE = "IsAutomaticColorButtonVisible";
        public const string AN_IS_NO_COLOR_BUTTON_VISIBLE = "IsNoColorButtonVisible";
        public const string AN_TYPE = "Type";
        public const string AN_HAS_LARGE_ITEMS = "HasLargeItems";
        public const string AN_ITEM_HEIGHT = "ItemHeight";
        public const string AN_ITEM_WIDTH = "ItemWidth";
        public const string AN_TEXT_POSITION = "TextPosition";
        public const string AN_MIN_COLUMNS_LARGE = "MinColumnsLarge";
        public const string AN_MAX_COLUMNS_MEDIUM = "MaxColumnsMedium";
        public const string AN_MIN_COLUMNS_MEDIUM = "MinColumnsMedium";
        public const string AN_MAX_COLUMNS = "MaxColumns";
        public const string AN_MAX_ROWS = "MaxRows";
        public const string AN_ROWS = "Rows";
        public const string AN_GRIPPER = "Gripper";
        public const string AN_IsMultipleHighlightingEnabled = "IsMultipleHighlightingEnabled";
        public const string AN_IMAGE_SIZE = "ImageSize";
        public const string AN_IS_LABEL_VISIBLE = "IsLabelVisible";
        public const string AN_IS_IMAGE_VISIBLE = "IsImageVisible";
        public const string AN_IS_POPUP = "IsPopup";
        public const string AN_CONTROL_NAME = "ControlName";
        public const string AN_IS_EDITABLE = "IsEditable";
        public const string AN_RESIZE_TYPE = "ResizeType";
        public const string AN_IS_AUTO_COMPLETE_ENABLED = "IsAutoCompleteEnabled";
        public const string AN_SHOW_SEPARATOR = "ShowSeparator";

        // Enum Strings
        // TRibbonGroupSpacing/TRibbonGroupLayout
        public const string ES_SMALL = "Small";
        public const string ES_MEDIUM = "Medium";
        public const string ES_LARGE = "Large";

        // TRibbonMenuCategoryClass
        public const string ES_STANDARD_ITEMS = "StandardItems";
        public const string ES_MAJOR_ITEMS = "MajorItems";

        // TRibbonGroupLayout
        public const string ES_POPUP = "Popup";

        // TRibbonBasicSizeDefinition
        public static readonly string[] ES_SIZE_DEFINITION = {string.Empty, string.Empty,
            "OneButton", "TwoButtons", "ThreeButtons", "ThreeButtons-OneBigAndTwoSmall",
            "ThreeButtonsAndOneCheckBox", "FourButtons", "FiveButtons",
            "FiveOrSixButtons", "SixButtons", "SixButtons-TwoColumns", "SevenButtons",
            "EightButtons", "EightButtons-LastThreeSmall", "NineButtons", "TenButtons",
            "ElevenButtons", "OneFontControl", "OneInRibbonGallery",
            "InRibbonGalleryAndBigButton", "InRibbonGalleryAndButtons-GalleryScalesFirst",
            "ButtonGroups", "ButtonGroupsAndInputs", "BigButtonsAndSmallButtonsOrInputs" };

        // TRibbonFontType
        public const string ES_FONT_ONLY = "FontOnly";
        public const string ES_FONT_WITH_COLOR = "FontWithColor";
        public const string ES_RICH_FONT = "RichFont";

        // TRibbonColorTemplate
        public const string ES_THEME_COLORS = "ThemeColors";
        public const string ES_STANDARD_COLORS = "StandardColors";
        public const string ES_HIGHLIGHT_COLORS = "HighlightColors";

        // TRibbonGalleryType
        public const string ES_ITEMS = "Items";
        public const string ES_COMMANDS = "Commands";

        // TRibbonTextPosition
        public const string ES_BOTTOM = "Bottom";
        public const string ES_HIDE = "Hide";
        public const string ES_LEFT = "Left";
        public const string ES_OVERLAP = "Overlap";
        public const string ES_RIGHT = "Right";
        public const string ES_TOP = "Top";

        // TRibbonSingleColumnGripperType/TRibbonMultiColumnGripperType
        public const string ES_NONE = "None";
        public const string ES_VERTICAL = "Vertical";
        public const string ES_CORNER = "Corner";

        // TRibbonComboBoxResizeType
        public const string ES_NO_RESIZE = "NoResize";
        public const string ES_VERTICAL_RESIZE = "VerticalResize";


        public const string ApplicationDefaultName = "APPLICATION";
        public const string ResourceTagPrefix = "<!--ResourceName Value=\"";
        public const string ResourceTagSuffix = "\"-->";
    }
}
