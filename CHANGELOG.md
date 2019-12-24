# Changelog
All notable changes to this project will be documented in this file.
This project adheres to [Semantic Versioning](http://semver.org/).

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