using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIRibbonTools
{
    [Flags]
    enum ImageFlags
    {
        None = 0,
        Large = 1,
        HighContrast = 2
    };

    enum MessageKind
    {
        Info, Warning, Error, Pipe
    };

    enum RibbonTemplate
    {
        None, WordPad
    };

    public enum RibbonGroupSpacing
    {
        Small, Medium, Large
    };

    public enum RibbonGroupLayout
    {
        Popup, Small, Medium, Large
    };

    public enum RibbonMenuCategoryClass
    {
        StandardItems, MajorItems
    };

    public enum RibbonBasicSizeDefinition
    {
        Custom, Advanced, OneButton, TwoButtons,
        ThreeButtons, ThreeButtonsOneBigAndTwoSmall, ThreeButtonsAndOneCheckBox,
        FourButtons, FiveButtons, FiveOrSixButtons, SixButtons,
        SixButtonsTwoColumns, SevenButtons, EightButtons,
        EightButtonsLastThreeSmall, NineButtons, TenButtons, ElevenButtons,
        OneFontControl, IntFontOnly, IntRichFont, IntFontWithColor,
        OneInRibbonGallery, BigButtonsAndSmallButtonsOrInputs,
        InRibbonGalleryAndBigButton, InRibbonGalleryAndButtonsGalleryScalesFirst,
        InRibbonGalleryAndThreeButtons, ButtonGroupsAndInputs, ButtonGroups
    };

    public enum RibbonFontType
    {
        FontOnly, FontWithColor, RichFont
    };

    public enum RibbonColorTemplate
    {
        ThemeColors, StandardColors, HighlightColors
    };

    public enum RibbonChipSize
    {
        Small, Medium, Large
    };

    public enum RibbonGalleryType
    {
        Items, Commands
    };

    public enum RibbonTextPosition
    {
        Bottom, Hide, Left, Overlap, Right, Top
    };

    public enum RibbonSingleColumnGripperType
    {
        None, Vertical
    };

    public enum RibbonMultiColumnGripperType
    {
        None, Vertical, Corner
    };

    public enum RibbonGroupSizeType
    {
        Large, Medium, Small
    };

    public enum RibbonImageSize
    {
        Large, Small
    };

    public enum RibbonComboBoxResizeType
    {
        NoResize, VerticalResize
    };

    public enum RibbonObjectType
    {
        Button, ToggleButton, SplitButton, DropDownButton,
        DropDownColorPicker, Spinner, CheckBox, ComboBox, QatButton,
        QatToggleButton, QatCheckBox, HelpButton, FloatieFontControl,
        FontControl, ControlGroup, DropDownGallery, SplitButtonGallery,
        InRibbonGallery, ApplicationMenu, QuickAccessToolbar, Group, Tab,
        TabGroup, List, Dictionary, String, Image, Document, Command,
        RecentItems, AppMenuGroup, MenuGroup, MiniToolbarMenuGroup,
        VerticalMenuLayout, FlowMenuLayout, Scale, ScalingPolicy,
        ControlNameMap, ControlSizeDefinition, Row, ControlSizeGroup,
        ColumnBreak, GroupSizeDefinition, SizeDefinition,
        RibbonSizeDefinition, ViewRibbon, MiniToolbar, ContextMenu,
        ContextMap, ViewContextPopup, Application, SplitButton_Items,
        RibbonSizeDefinitions, ScalingPolicy_IdealSizes, ContextualTabs,
        MiniToolbars, ContextMenus, ContextMaps
    };

    //@ The names in this enum must be equal to the class names !
    //see AddCurrentFrame in TFrameViews
    public enum ViewClasses
    {
        None,
        TFrameViewRibbon,
        TFrameApplicationMenu,
        TFrameMenuGroup,
        TFrameAppMenuGroup,
        TFrameButton,
        TFrameToggleButton,
        TFrameSplitButton,
        TFrameDropDownButton,
        TFrameQuickAccessToolbar,
        TFrameQatControl,
        TFrameHelpButton,
        TFrameTab,
        TFrameTabGroup,
        TFrameGroup,
        TFrameScale,
        TFrameDropDownGallery,
        TFrameSplitButtonGallery,
        TFrameInRibbonGallery,
        TFrameSizeDefinition,
        TFrameGroupSizeDefinition,
        TFrameControlSizeDefinition,
        TFrameColumnBreak,
        TFrameFloatieFontControl,
        TFrameFontControl,
        TFrameControlGroup,
        TFrameComboBox,
        TFrameCheckBox,
        TFrameDropDownColorPicker,
        TFrameSpinner,
        TFrameMiniToolbar,
        TFrameContextMenu,
        TFrameContextMap
    }

    //The renamed enum
    public enum ViewClassesNew
    {
        None,
        ViewRibbonFrame,
        ApplicationMenuFrame,
        MenuGroupFrame,
        AppMenuGroupFrame,
        ButtonFrame,
        ToggleButtonFrame,
        SplitButtonFrame,
        DropDownButtonFrame,
        QuickAccessToolbarFrame,
        QatControlFrame,
        HelpButtonFrame,
        TabFrame,
        TabGroupFrame,
        GroupFrame,
        ScaleFrame,
        DropDownGalleryFrame,
        SplitButtonGalleryFrame,
        InRibbonGalleryFrame,
        SizeDefinitionFrame,
        GroupSizeDefinitionFrame,
        ControlSizeDefinitionFrame,
        ColumnBreakFrame,
        FloatieFontControlFrame,
        FontControlFrame,
        ControlGroupFrame,
        ComboBoxFrame,
        CheckBoxFrame,
        DropDownColorPickerFrame,
        SpinnerFrame,
        MiniToolbarFrame,
        ContextMenuFrame,
        ContextMapFrame
    }

}
