# Changelog
All notable changes to this project will be documented in this file.
This project adheres to [Semantic Versioning](http://semver.org/).

### RibbonTools V1.7.1

#### Changed (RibbonTools)

- Bugfix for Localize.

### Ribbon V2.15.0 RibbonTools V1.7.0

#### Changed (Ribbon)

- Destroy EventLogger.
- Hide Interface methods Execute, UpdateProperty, GetValue
- Update Ribbon Size for Designer 
- New Name property (RibbonButton, RibbonComboBox, ...) for easier debugging. One can set the Name after instantiation the RibbonButton, ...
- Create method in GalleryItemEventArgs, ColorPickerEventArgs, FontControlEventArgs add a sender parameter for checking
- UIImage class new designed. This class can help for handling between Bitmap class and IUIImage interface

#### Changed (RibbonTools)

- Bitmap Alpha channel detection like Microsoft in the Icon class.
- Changes for Issue #12

#### Changed (Samples)

- Update Size for the Ribbon Control.
- C# Sample "NewFunctions" updated with new sender parameter.

### Ribbon V2.14.0, RibbonTools V1.6.0

#### Changed (Ribbon)

- Additional classes: AbstractPropertySet, QatCommandPropertySet, UICollection<T>, SelectedItem<T>, GalleryCommandProperties.
- RecentItemsEventArgs, GalleryItemEventArgs, ColorPickerEventArgs, FontControlEventArgs, HRESULT, PInvoke
- With usage of the additional classes the user don't need to handle Com interface IUICollection and the structs PropVariant and PropertyKey
- Gallery controls like RibbonComboBox, RibbonDropDownGallery, RibbonInRibbonGallery, RibbonSplitButtonGallery have easy to use new properties.
GalleryCategories, GalleryItemItemsSource (for Item controls), GalleryCommandItemsSource (for Command controls)
These new properties should be used instead of ItemsSource, Categories
- RibbonQuickAccessToolbar gets a new property: QatItemsSource. This should be used instead of ItemsSource
- DarkMode only for the Ribbon for newer Windows 10, Windows 11 versions (DarkModeRibbon property in the Ribbon class). But wait till Microsoft delivers DarkMode for WinForms

```
        // Example code for the (Ribbon) Form to change DarkMode in TitleBar. 
		
		public unsafe void SetDarkModeTitleBar(bool enabled)
        {
            HRESULT hr;
            int trueValue = 0x01, falseValue = 0x00;
            HWND hwnd = new HWND(this.Handle);
            if (enabled)
            {
                hr = PInvoke.DwmSetWindowAttribute(hwnd, DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, &trueValue, (uint)Marshal.SizeOf(typeof(int)));
                PInvoke.SendMessage(hwnd, PInvoke.WM_NCACTIVATE, new WPARAM(0), new LPARAM());
                PInvoke.UpdateWindow(hwnd);
            }
            else
            {
                hr = PInvoke.DwmSetWindowAttribute(hwnd, DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, &falseValue, (uint)Marshal.SizeOf(typeof(int)));
                PInvoke.SendMessage(hwnd, PInvoke.WM_NCACTIVATE, new WPARAM(1), new LPARAM());
                PInvoke.UpdateWindow(hwnd);
            }
        }
```
For PInvoke functions above see [Microsoft.CsWin32](https://www.nuget.org/packages/Microsoft.Windows.CsWin32) or [Github page](https://github.com/Microsoft/CsWin32)

- Bugfix in FontPropertyStore (DeltaSize is changed to a Nullable enum)
- In events for Spinner, ToggleButton, CheckBox one don't need to use ExecuteEventsArgs class.
 1. One can use for RibbonSpinner the property DecimalValue from this RibbonControl
 2. For RibbonToggleButton, RibbonCheckBox one can use property BooleanValue
- new C# Sample "NewFunctions" that shows the usage of new functions.
- Supported .NET Framework versions: 3.5 and 4.x. Supported .NET (Core) versions: 6.0, 7.0

#### Changed (RibbonTools)

- Tooltips for Qat CustomizeCommand ComboBox
- ResourceIdentifier savings
- Preview colorization design issue

### Ribbon V2.13.0, RibbonTools V1.5.0

#### Changed (Ribbon)

- Bugfix RibbonDropDownColorPicker.StandardColors.
- Free unmanaged memory as soon as possible.
- Support for .NET7

#### Changed (RibbonTools)

- Correct usage of TActions
- delete unnecessary namespace in CodeBuilder for RibbonItems


### Ribbon V2.12.0, RibbonTools V1.4.0

#### Changed (Ribbon)

- AssemblyVersion is now 1.0.0.0 for .NET Framework 3.5. This version is installed to the GAC. For all other (.NET Framework 4.0 and higher, .NET Core) the AssemblyVersion is 4.0.0.0. These changes are done for easier handling with different .Net versions, mainly for .NET Framework 4.x.

#### Changed (RibbonTools)

- Added functions (and small changes) to AlphaBitmap, NativeMethods
- Project files changes to the Ribbon with Assembly version 4.0.0.0
- So we don't need to activate the .NET Framework 3.5 in the Windows System

#### Changed Setup files


### Ribbon V2.11.1, RibbonTools V1.3.7

#### Changed (RibbonTools)

- Bugfixes: NullReferenceException with Empty Commands
- Command Images are now sorted when using Add, Add Range
- In SizeDefinitions ControlName is now optional.
- Now CommandName in Views shows only the CommandName. In the ComboBox list you get a Tooltip with the LabelTitle for the CommandName
- Some refactoring with ConvertImage
- Newline \n in TooltipDescription and LabelDescription allowed. In the markup xml file the \n is replaced with xml newline characters.

### Ribbon V2.11.1, RibbonTools V1.3.6

#### Changed (Ribbon)

- DesignMode issue for .NET5, .NET6 (other naming for the Visual Studio Designer)

#### Changed (RibbonTools)

- Bugfixes: GalleryControls in AppMenuGroup and in other Groups
- Default TextPosition in GalleryControls
- RibbonToolsCore.sln only supports .NET6 (VS2022 required)
### Ribbon V2.11.0, RibbonTools V1.3.5

#### Changed (Ribbon)

- Property Enabled of all Ribbon items returns true after application startup

#### Changed (RibbonTools)

- New: Image Converters with Bitmap V5 Header support
- Bugfix: Saving Bitmap files
- Methods added to class AlphaBitmap
- Big Views with better performance

### Ribbon V2.10.0, RibbonTools V1.3.4

#### Changed (Ribbon)
- new Ribbon event ViewDestroy (Example: you can use it for saving the settings)
- new Properties (Tag, CommandType) for all Ribbon Controls (RibbonButton, ...)
- Interface IRibbonControl is now public
- some bugfixes

#### Changed (RibbonTools)
- Some Setting dialog text and tooltips modified.
- Bugfix: MiniToolbar add of controls not possible
- Bugfix: Issue with the WordPad template
- refactoring and new class AlphaBitmap

### Ribbon V2.9.2, RibbonTools V1.3.3

#### Changed (Ribbon)

- Bugfix: RibbonSpinner with DecimalValue set by application
#### Changed (RibbonTools)
- Generating RibbonItems.Designer file: initialized removed.

### Ribbon V2.9.1, RibbonTools V1.3.2

#### Changed (Ribbon)

- Bugfix: reading RibbonTabGroup.ContextAvailable throws Exception
- Update sample 14-ContextualTabs

### Ribbon V2.9.0, RibbonTools V1.3.2

#### Changed (Ribbon)

- new Ribbon event: RibbonHeightChanged

#### Changed (RibbonTools)

- Help Tutorial is linked to the wiki
- using Ribbon.ResourceIdentifier for preview

### Ribbon V2.8.8, RibbonTools V1.3.1

#### Changed (Ribbon)

- bugfix spinner DecimalValue
- Ribbon: comments for designtime properties
- new Ribbon property: ResourceIdentifier  (normally leave it empty) Only neccessary if you use a not default (APPLICATION) ***name*** parameter with uicc.exe.

### Ribbon V2.8.7, RibbonTools V1.3.1

#### Changed (RibbonTools)

- .net core 3.1 project files
- Intellisense for ribbonitems.designer.cs(vb) props
- fix for comment in .h file
- fix color for log
- BugFix for QAT CustomizeCommandName
- Command in View lost when LabelTitle changed
- Issue with Add in View page
- other fixes and cleanups

### Ribbon V2.8.7, RibbonTools V1.3.0

#### Changed

- RibbonTools: Integration of Ribbon features they are available since Windows 8.
  More Qat Controls like ComboBox
  FontControl: IsGrowShrinkButtonGroupVisible
  DropDownGallery, SplitButtonGallery: IsMultipleHighlightingEnabled

#### Todo (=> done in the wiki)

- Documentation for RibbonTools, EventLogger

### Ribbon V2.8.7, RibbonTools V1.2.0

#### Changed

- Ribbon: more IntelliSense comments for the code behind.
- Ribbon: new EventLogger classes (since Windows 8)
  usage: from the Ribbon class you can get an instance of the EventLogger class (Property EventLogger). If you want use the EventLogger, you have to call EventLogger.Attach() and connect to the EventLogger.LogEvent. If you don't want the Logging anymore, then you have to call EventLogger.Detach().
- RibbonTools: Issues with xxGallery, DropDownButton, SplitButton, MenuGroups and Controls and other small fixes.

### Ribbon V2.8.6, RibbonTools V1.1.0

#### Changed
- RibbonTools: Settings added, faster Build
### Ribbon V2.8.6, RibbonGenerator V1.3.5, RibbonPreview V1.2.1, RibbonTools V1.0.0

#### Changed

- new application: RibbonTools. This is a tool for design, build and preview the Ribbon. This tool is 

  similar to the Delphi RibbonDesigner. One can call this tool also from commandline with option help (/h). RibbonGenerator and RibbonPreview functions are integrated in the new RibbonTools and therefore they are deprecated.

### Ribbon V2.8.6, RibbonGenerator V1.3.5, RibbonPreview V1.2.1

#### Changed

- Ribbon.dll: Calculation of Ribbon colors to / from HSB values as W3C describe. Calculation is not lossless. In Windows 10 Background and Highlight setting is a bit of strange.
- Ribbon.dll: new methods in Ribbon Control. SetBackgroundColor, SetHighlightColor, SetTextColor.
- RibbonPreview.exe: Single Color settings possible.

### Ribbon V2.8.4, RibbonGenerator V1.3.5, RibbonPreview V1.2

#### Changed

- CommandName can also have a Id which is not defined in the Commands.
- Update function for the Template.bat (Linker path)
- VBCodeBuilder as alpha version (not in RibbonGenerator included)
- Setup Icon for Ribbon Components (from ennerperez)
- RibbonPreview: Language selection for preview
- Build files for .Net Core 3.1

## [2.8.4] - [2019-11-25]

### Ribbon V2.8.4, RibbonGenerator V1.3.4, RibbonPreview V1.1

#### Changed
- Detection of latest Visual Studio Linker in RibbonGenerator for the Template.bat in LocalAppData folder RibbonGenerator.
- In Ribbon events with unhandled Exceptions the application exits now with Environment.FailFast(Stacktrace). Environment.FailFast write the information to the Windows application event logger.  You can prevent this hard exit by using the new Ribbon event "RibbonEventException".

## [2.8.3] - [2019-11-08]

### Ribbon V2.8.3, RibbonGenerator V1.3.3, RibbonPreview V1.1

#### Changed
- Refactoring CodeBuilder, Parser, ... (RibbonGenerator, RibbonPreview)
- some Bugfixes
- ItemsSourceReady and CategoriesReady events now fires only once (Ribbon.dll)
- Template.bat is set to a new path for VS2019

## [2.8.2] - [2019-11-03]

### Ribbon V2.8.1, RibbonGenerator V1.3.2, RibbonPreview V1.0

#### Changed
- new RibbonPreview (Build the Ribbon and Preview).
- some refactoring and bugfixes
- In the file msi.zip are the Setups for Ribbon.msi, RibbonGenerator.msi, RibbonPreview.msi

## [2.8.1] - [2019-11-01]

### Ribbon V2.8.1 and RibbonGenerator V1.3.1

#### Changed
- Uninstall older RibbonGenerator first !
- some Bugfixes
- Refactoring the CodeGenerator in RibbonGenerator.
- The Template.bat is now set to Visual Studio 2019 Community Edition
- In the file msi.zip are the Setups for Ribbon.msi and RibbonGenerator.msi

## [2.8.0] - [2019-10-07]

### Ribbon V2.8 and RibbonGenerator V1.3

#### Changed
- see HowToUse.md
- file based RibbonMarkup.ribbon including localization
- C# Wrapper Code generated (RibbonItems.Designer.cs)
- new Sample

## [2.7.0] - [2019-09-08]

### Ribbon V2.7 and RibbonGenerator V1.2

#### Changed
- Ribbon and RibbonGenerator work also in newer Visual Studio versions.
- We got a Setup for the components.
- HowToUse.md
- Project cleaning

## [2.6.0] - [Obsolete]