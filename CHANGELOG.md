# Changelog
All notable changes to this project will be documented in this file.
This project adheres to [Semantic Versioning](http://semver.org/).

### Ribbon V2.8.7, RibbonTools V1.3.0

### Changed

- RibbonTools: Integration of Ribbon features they are available since Windows 8.
  More Qat Controls like ComboBox
  FontControl: IsGrowShrinkButtonGroupVisible
  DropDownGallery, SplitButtonGallery: IsMultipleHighlightingEnabled

### Todo 

- Documentation for RibbonTools, EventLogger

### Ribbon V2.8.7, RibbonTools V1.2.0

### Changed

- Ribbon: more IntelliSence comments for the codebehind.
- Ribbon: new EventLogger classes (Windows 8 and later)
  usage: from the Ribbon class you can get an instance of the EventLogger class (Property EventLogger). If you want use the EventLogger, you have to call EventLogger.Attach() and connect to the EventLogger.LogEvent. If you don't want the Logging anymore, then you have to call EventLogger.Detach().
- RibbonTools: Issues with xxGallery, DropDownButton, SplitButton, MenuGroups and Controls and other small fixes.

### Ribbon V2.8.6, RibbonTools V1.1.0

### Changed
- RibbonTools: Settings added, faster Build
### Ribbon V2.8.6, RibbonGenerator V1.3.5, RibbonPreview V1.2.1, RibbonTools V1.0.0

### Changed

- new application: RibbonTools. This is a tool for design, build and preview the Ribbon. This tool is 

  similar to the Delphi RibbonDesigner. One can call this tool also from commandline with option help (/h). RibbonGenerator and RibbonPreview functions are integrated in the new RibbonTools and therefore they are deprecated.

### Ribbon V2.8.6, RibbonGenerator V1.3.5, RibbonPreview V1.2.1

### Changed

- Ribbon.dll: Calculation of Ribbon colors to / from HSB values as W3C describe. Calculation is not lossless. In Windows 10 Background and Highlight setting is a bit of strange.
- Ribbon.dll: new methods in Ribbon Control. SetBackgroundColor, SetHighlightColor, SetTextColor.
- RibbonPreview.exe: Single Color settings possible.

### Ribbon V2.8.4, RibbonGenerator V1.3.5, RibbonPreview V1.2

### Changed

- CommandName can also have a Id which is not defined in the Commands.
- Update function for the Template.bat (Linker path)
- VBCodeBuilder as alpha version (not in RibbonGenerator included)
- Setup Icon for Ribbon Components (from ennerperez)
- RibbonPreview: Language selection for preview
- Build files for .Net Core 3.1

## [2.8.4] - [2019-11-25]

### Ribbon V2.8.4, RibbonGenerator V1.3.4, RibbonPreview V1.1

### Changed
- Detection of latest Visual Studio Linker in RibbonGenerator for the Template.bat in LocalAppData folder RibbonGenerator.
- In Ribbon events with unhandled Exceptions the application exits now with Environment.FailFast(Stacktrace). Environment.FailFast write the informations to the Windows application event logger.  You can prevent this hard exit by using the new Ribbon event "RibbonEventException".

## [2.8.3] - [2019-11-08]

### Ribbon V2.8.3, RibbonGenerator V1.3.3, RibbonPreview V1.1

### Changed
- Refactoring CodeBuilder, Parser, ... (RibbonGenerator, RibbonPreview)
- some Bugfixes
- ItemsSourceReady and CategoriesReady events now fires only once (Ribbon.dll)
- Template.bat is set to a new path for VS2019

## [2.8.2] - [2019-11-03]

### Ribbon V2.8.1, RibbonGenerator V1.3.2, RibbonPreview V1.0

### Changed
- new RibbonPreview (Build the Ribbon and Preview).
- some refactoring and bugfixes
- In the file msi.zip are the Setups for Ribbon.msi, RibbonGenerator.msi, RibbonPreview.msi

## [2.8.1] - [2019-11-01]

### Ribbon V2.8.1 and RibbonGenerator V1.3.1

### Changed
- Deinstall older RibbonGenerator first !
- some Bugfixes
- Refactoring the CodeGenerator in RibbonGenerator.
- The Template.bat is now set to Visual Studio 2019 Community Edition
- In the file msi.zip are the Setups for Ribbon.msi and RibbonGenerator.msi

## [2.8.0] - [2019-10-07]

### Ribbon V2.8 and RibbonGenerator V1.3

### Changed
- see HowToUse.md
- file based RibbonMarkup.ribbon including localization
- C# Wrapper Code generated (RibbonItems.Designer.cs)
- new Sample

## [2.7.0] - [2019-09-08]

### Ribbon V2.7 and RibbonGenerator V1.2

### Changed
- Ribbon and RibbonGenerator work also in newer Visual Studio versions.
- We got a Setup for the components.
- HowToUse.md
- Project cleaning

## [2.6.0] - [Obsolete]