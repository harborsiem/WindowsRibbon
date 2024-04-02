# Windows Ribbon Framework for .NET

*Windows Ribbon Framework* for WinForms

Windows Ribbon for WinForms is a .NET wrapper for *Microsoft Windows Ribbon Framework* control UIRibbon.dll. It will allow WinForms developers to use *Microsoft Windows Ribbon Framework* control in their WinForms applications. Supported Windows versions are Windows 7 and later versions (Windows 8, Windows 10, Windows 11).

*Windows Ribbon* is sometimes called UIRibbon. The former development title is *Scenic Ribbon*.

See also [Microsoft documentation](https://learn.microsoft.com/en-us/windows/win32/windowsribbon/-uiplat-windowsribbon-entry)

## **Project Description**

 Windows Ribbon for WinForms is a .NET wrapper for *Microsoft Windows Ribbon Framework* control.
 It will allow WinForms developers to use *Microsoft Windows Ribbon Framework* control in their WinForms applications.

The project includes the library RibbonLib, which adds support for *Microsoft Windows Ribbon Framework* to WinForms application and sample applications, written both in C# and VB.NET, that demonstrates the use of the library and the different Ribbon features available.

Read the Wiki Pages for more details on how to use the *Microsoft Windows Ribbon Framework*.

Note: You must have the Windows 7 SDK (or any later SDK) installed in order to compile (build) the project.

For easier designing, building and previewing the Windows Ribbon Framework there is gui and console based tool called **RibbonTools**

Following is the list of sample application and their description: 

- **01 - AddingRibbonSupport** - Empty WinForms application with basic Ribbon support.
- **02 - ApplicationMenuButton** - WinForms application with Ribbon that contains an application menu with some buttons.
- **03 - MenuDropDown** - WinForms application with DropDownButton and SplitButton inside an application menu.
- **04 - TabGroupHelp** - WinForms application that uses Tabs, Groups and HelpButton.
- **05 - Spinner** - WinForms application that demonstrates the use of a Spinner control in the ribbon.
- **06 - ComboBox** - WinForms application that demonstrates the use of a CombBox control in the ribbon.
- **07 - RibbonColor** - WinForms application that shows how to change the ribbon global colors.
- **08 - Images** - WinForms application that shows how to work set images dynamically in the ribbon.
- **09 - Galleries** - WinForms application thats uses DropDownGallery, SplitButtonGallery and InRibbonGallery.
- **10 - CheckBox** - WinForms application that uses CheckBox and ToggleButton control in the ribbon.
- **11 - DropDownColorPicker** - WinForms application that demonstrates the use of a DropDownColorPicker control in the ribbon.
- **12 - FontControl** - WinForms application that demonstrates the use of a FontControl control in the ribbon.
- **13 - ApplicationModes** - WinForms application that demonstrates the use of ApplicationModes.
- **14 - ContextualTabs** - WinForms application that demonstrates the use of ContextualTabs.
- **15 - ContextPopup** - WinForms application that demonstrates the use of ribbon context popups.
- **16 - RecentItems** - WinForms application that demonstrates the use of ribbon recent items.
- **17 - QuickAccessToolbar** WinForms application that demonstrates the use of quick access toolbar.
- **18 - SizeDefinition** WinForms application that demonstrates the use of custom layout templates.
- **19 - Localization** WinForms application that demonstrates how to localize your ribbon.
- **NewFunctions** WinForms application that demonstrates new functions with Ribbon version 2.14 and above

see also:
[Documentation archive](https://www.codeproject.com/Articles/55599/Windows-Ribbon-for-WinForms-Part-Table-of-Conten)

[Current Documentation](https://github.com/harborsiem/WindowsRibbon/wiki)

Thanks to former developers Arik Poznanski and Bernhard Elbl for the great work.


Now the WindowsRibbon project is running also in the latest Visual Studio Versions 2017, 2019, 2022. Have a look to the **WIKI** and the file "HowToUse.md" to see how to use the WindowsRibbon in a .NET WinForms application. In the Releases page of this Github repository there are also zipped *.msi setup files for the components. The msi files are build with the WIX Toolset.

---

WindowsRibbon is also available on NuGet, with Package Id [WindowsRibbon](https://www.nuget.org/packages/WindowsRibbon). Via this package you can get a compiled Ribbon Library also for .NET Framework 4.0, .NET 6 , .NET 7 , .NET 8

---

The C# samples are added with a more complex Mainform application. Here you can see the Ribbon of Mainform in the RibbonTools app.


![PrintPreview](./Images/PrintPreview.png)


An other C# Application with the Ribbon Control one can find at the following page:

[ElemntViewer](https://github.com/harborsiem/ELEMNTViewer)

## Installation:

Requirement for installation is the Microsoft .NET Framework 4.6.2 or any higher version. If it is not installed on your computer then you can download it from a Microsoft page. Install this first.
