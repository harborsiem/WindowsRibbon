using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RibbonLib.Controls;
using RibbonLib;

namespace UIRibbonTools
{
    class RibbonClassBuilder
    {
        private Ribbon _ribbon;

        public KeyValuePair<string, RibbonQuickAccessToolbar> Qat;
        public KeyValuePair<string, RibbonApplicationMenu> ApplicationMenu;
        public Dictionary<string, RibbonButton> Buttons;
        public Dictionary<string, RibbonCheckBox> CheckBoxes;
        public Dictionary<string, RibbonComboBox> ComboBoxes;
        public Dictionary<string, RibbonDropDownButton> DropDownButtons;
        public Dictionary<string, RibbonDropDownColorPicker> DropDownColorPickers;
        public Dictionary<string, RibbonDropDownGallery> DropDownGalleries;
        public Dictionary<string, RibbonFontControl> FontControls;
        public Dictionary<string, RibbonGroup> Groups;
        public KeyValuePair<string, RibbonHelpButton> HelpButton;
        public Dictionary<string, RibbonInRibbonGallery> InRibbonGalleries;
        public Dictionary<string, RibbonMenuGroup> MenuGroups;
        public Dictionary<string, RibbonRecentItems> RecentItems;
        public Dictionary<string, RibbonSpinner> Spinners;
        public Dictionary<string, RibbonSplitButton> SplitButtons;
        public Dictionary<string, RibbonSplitButtonGallery> SplitButtonGalleries;
        public Dictionary<string, RibbonTab> Tabs;
        public Dictionary<string, RibbonTabGroup> TabGroups;
        public Dictionary<string, RibbonToggleButton> ToggleButtons;
        //Dictionary's and KeyValuePairs with getter, private setter ?

        public RibbonClassBuilder(Ribbon ribbon)
        {
            this._ribbon = ribbon;
        }

        /// <summary>
        /// Clear all Dictionaries and KeyValuePairs of Ribbon items
        /// </summary>
        public void Clear()
        {
            Qat = new KeyValuePair<string, RibbonQuickAccessToolbar>();
            ApplicationMenu = new KeyValuePair<string, RibbonApplicationMenu>();
            if (Buttons != null)
                Buttons.Clear();
            if (CheckBoxes != null)
                CheckBoxes.Clear();
            if (ComboBoxes != null)
                ComboBoxes.Clear();
            if (DropDownButtons != null)
                DropDownButtons.Clear();
            if (DropDownColorPickers != null)
                DropDownColorPickers.Clear();
            if (DropDownGalleries != null)
                DropDownGalleries.Clear();
            if (FontControls != null)
                FontControls.Clear();
            if (Groups != null)
                Groups.Clear();
            HelpButton = new KeyValuePair<string, RibbonHelpButton>();
            if (InRibbonGalleries != null)
                InRibbonGalleries.Clear();
            if (MenuGroups != null)
                MenuGroups.Clear();
            if (RecentItems != null)
                RecentItems.Clear();
            if (Spinners != null)
                Spinners.Clear();
            if (SplitButtons != null)
                SplitButtons.Clear();
            if (SplitButtonGalleries != null)
                SplitButtonGalleries.Clear();
            if (Tabs != null)
                Tabs.Clear();
            if (TabGroups != null)
                TabGroups.Clear();
            if (ToggleButtons != null)
                ToggleButtons.Clear();
        }

        /// <summary>
        /// Build Ribbon class via Reflection and store them in Dictionaries and KeyValuePairs.
        /// commandName is Key for the class
        /// </summary>
        /// <param name="className">class name of Ribbon item</param>
        /// <param name="commandName">The Command Name from RibbonMarkup.xml</param>
        /// <param name="cmd">This is the Command Id from RibbonMarkup.xml or RibbonMarkup.h</param>
        /// <returns>The RibbonClass</returns>
        public object BuildRibbonClass(string className, string commandName, uint cmd)
        {
            object control = null;
            switch (className)
            {
                case "RibbonQuickAccessToolbar":
                    control = Activator.CreateInstance(typeof(RibbonQuickAccessToolbar), new object[] { _ribbon, cmd });
                    Qat = new KeyValuePair<string, RibbonQuickAccessToolbar>(commandName, (RibbonQuickAccessToolbar)control);
                    break;
                case "RibbonApplicationMenu":
                    control = Activator.CreateInstance(typeof(RibbonApplicationMenu), new object[] { _ribbon, cmd });
                    ApplicationMenu = new KeyValuePair<string, RibbonApplicationMenu>(commandName, (RibbonApplicationMenu)control);
                    break;
                case "RibbonButton":
                    if (Buttons == null)
                        Buttons = new Dictionary<string, RibbonButton>();
                    control = Activator.CreateInstance(typeof(RibbonButton), new object[] { _ribbon, cmd });
                    Buttons.Add(commandName, (RibbonButton)control);
                    break;
                case "RibbonCheckBox":
                    if (CheckBoxes == null)
                        CheckBoxes = new Dictionary<string, RibbonCheckBox>();
                    control = Activator.CreateInstance(typeof(RibbonCheckBox), new object[] { _ribbon, cmd });
                    CheckBoxes.Add(commandName, (RibbonCheckBox)control);
                    break;
                case "RibbonComboBox":
                    if (ComboBoxes == null)
                        ComboBoxes = new Dictionary<string, RibbonComboBox>();
                    control = Activator.CreateInstance(typeof(RibbonComboBox), new object[] { _ribbon, cmd });
                    ComboBoxes.Add(commandName, (RibbonComboBox)control);
                    break;
                case "RibbonDropDownButton":
                    if (DropDownButtons == null)
                        DropDownButtons = new Dictionary<string, RibbonDropDownButton>();
                    control = Activator.CreateInstance(typeof(RibbonDropDownButton), new object[] { _ribbon, cmd });
                    DropDownButtons.Add(commandName, (RibbonDropDownButton)control);
                    break;
                case "RibbonDropDownColorPicker":
                    if (DropDownColorPickers == null)
                        DropDownColorPickers = new Dictionary<string, RibbonDropDownColorPicker>();
                    control = Activator.CreateInstance(typeof(RibbonDropDownColorPicker), new object[] { _ribbon, cmd });
                    DropDownColorPickers.Add(commandName, (RibbonDropDownColorPicker)control);
                    break;
                case "RibbonDropDownGallery":
                    if (DropDownGalleries == null)
                        DropDownGalleries = new Dictionary<string, RibbonDropDownGallery>();
                    control = Activator.CreateInstance(typeof(RibbonDropDownGallery), new object[] { _ribbon, cmd });
                    DropDownGalleries.Add(commandName, (RibbonDropDownGallery)control);
                    break;
                case "RibbonFontControl":
                    if (FontControls == null)
                        FontControls = new Dictionary<string, RibbonFontControl>();
                    control = Activator.CreateInstance(typeof(RibbonFontControl), new object[] { _ribbon, cmd });
                    FontControls.Add(commandName, (RibbonFontControl)control);
                    break;
                case "RibbonGroup":
                    if (Groups == null)
                        Groups = new Dictionary<string, RibbonGroup>();
                    control = Activator.CreateInstance(typeof(RibbonGroup), new object[] { _ribbon, cmd });
                    Groups.Add(commandName, (RibbonGroup)control);
                    break;
                case "RibbonHelpButton":
                    control = Activator.CreateInstance(typeof(RibbonHelpButton), new object[] { _ribbon, cmd });
                    HelpButton = new KeyValuePair<string, RibbonHelpButton>(commandName, (RibbonHelpButton)control);
                    break;
                case "RibbonInRibbonGallery":
                    if (InRibbonGalleries == null)
                        InRibbonGalleries = new Dictionary<string, RibbonInRibbonGallery>();
                    control = Activator.CreateInstance(typeof(RibbonInRibbonGallery), new object[] { _ribbon, cmd });
                    InRibbonGalleries.Add(commandName, (RibbonInRibbonGallery)control);
                    break;
                case "RibbonMenuGroup":
                    if (MenuGroups == null)
                        MenuGroups = new Dictionary<string, RibbonMenuGroup>();
                    control = Activator.CreateInstance(typeof(RibbonMenuGroup), new object[] { _ribbon, cmd });
                    MenuGroups.Add(commandName, (RibbonMenuGroup)control);
                    break;
                case "RibbonRecentItems":
                    if (RecentItems == null)
                        RecentItems = new Dictionary<string, RibbonRecentItems>();
                    control = Activator.CreateInstance(typeof(RibbonRecentItems), new object[] { _ribbon, cmd });
                    RecentItems.Add(commandName, (RibbonRecentItems)control);
                    break;
                case "RibbonSpinner":
                    if (Spinners == null)
                        Spinners = new Dictionary<string, RibbonSpinner>();
                    control = Activator.CreateInstance(typeof(RibbonSpinner), new object[] { _ribbon, cmd });
                    Spinners.Add(commandName, (RibbonSpinner)control);
                    break;
                case "RibbonSplitButton":
                    if (SplitButtons == null)
                        SplitButtons = new Dictionary<string, RibbonSplitButton>();
                    control = Activator.CreateInstance(typeof(RibbonSplitButton), new object[] { _ribbon, cmd });
                    SplitButtons.Add(commandName, (RibbonSplitButton)control);
                    break;
                case "RibbonSplitButtonGallery":
                    if (SplitButtonGalleries == null)
                        SplitButtonGalleries = new Dictionary<string, RibbonSplitButtonGallery>();
                    control = Activator.CreateInstance(typeof(RibbonSplitButtonGallery), new object[] { _ribbon, cmd });
                    SplitButtonGalleries.Add(commandName, (RibbonSplitButtonGallery)control);
                    break;
                case "RibbonTab":
                    if (Tabs == null)
                        Tabs = new Dictionary<string, RibbonTab>();
                    control = Activator.CreateInstance(typeof(RibbonTab), new object[] { _ribbon, cmd });
                    Tabs.Add(commandName, (RibbonTab)control);
                    break;
                case "RibbonTabGroup":
                    if (TabGroups == null)
                        TabGroups = new Dictionary<string, RibbonTabGroup>();
                    control = Activator.CreateInstance(typeof(RibbonTabGroup), new object[] { _ribbon, cmd });
                    TabGroups.Add(commandName, (RibbonTabGroup)control);
                    break;
                case "RibbonToggleButton":
                    if (ToggleButtons == null)
                        ToggleButtons = new Dictionary<string, RibbonToggleButton>();
                    control = Activator.CreateInstance(typeof(RibbonToggleButton), new object[] { _ribbon, cmd });
                    ToggleButtons.Add(commandName, (RibbonToggleButton)control);
                    break;
                default:
                    break;
            }
            return control;
        }
    }
}
