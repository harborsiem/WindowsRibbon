# How to use WindowsRibbon

## [Limitation]
- You can use WindowsRibbon only with Windows 7 or newer Windows versions (Windows 8, Windows 10)
- There is no Designer for the Ribbon Elements

## [Workflow]
1. For building an application with Ribbon one have to create a Windows Forms App.
2. In Solution Explorer References: choose -> Add Reference -> Assemblies -> Extensions -> Ribbon.dll
3. Visual Studio  Menu: Tool -> Choose Toolbox Items: Select Ribbon with Namespace RibbonLib
4. Place a Ribbon to the Form.
5. Now create a RibbonMarkup.xml file and insert Application Commands and Application Views as you can see in the examples and the documentation in Arik's blogs (see the links below).
6. Because CustomTools, like Bernhard Elbl says, do not work this way in newer Visual Studio versions like VS 2017 you have to
open a Console Window in the folder of RibbonMarkup.xml (maybe we have a solution for a CustomTool later on).
8. Call in the Console Window: rgc RibbonMarkup.xml
9. At the first time you make the call, this may not work.
10. Then you have to modify the Template.bat in your User Path\AppData\Local\RibbonGenerator (mainly C:\Users\\<user>\AppData\Local\RibbonGenerator\Template.bat)
11. Set the correctly path (depended to the Visual Studio version) of vcvars32.bat and link.exe and call again "rgc RibbonMarkup.xml"
12. Now you get some generated files, eg. RibbonMarkup.ribbon. Add this file to your project.
13. Set the Property Build Action of RibbonMarkup.ribbon to Embedded Resource
14. In Windows Form Designer select the Ribbon and set the Property ResourceName of the Ribbon Control to [Assembly name].RibbonMarkup.ribbon
14. Add
     using RibbonLib;
     using RibbonLib.Controls;
     to the Form1.cs. Define your Application Command ID's as Int32 const to Form1.cs and call the Wrapper Classes of the Application Views in Ctor of the Form.
15. now compile your application and run


## [Installation]
Installation of Visual Studio (any version since Visual Studio 2010 up to the newer ones) is required.
Minimum installation components in Visual Studio are .NET Desktop Development and C++ Desktop Development.

Installation of the Windows SDK which is suitable for your Windows version
eg. If you have Windows 7 as Operating System (OS), than you have to install Windows 7 SDK
If you have Windows 10 as OS, than you have to install Windows 10 SDK.

Than you have to install RibbonGenerator.msi and Ribbon.msi. You get them in the Releases Page.
RibbonGenerator installs the Commandline Tool rgc.exe and RibbonGenerator.dll. This Toolchain will build a .ribbon file.

Ribbon.msi installs Ribbon.dll to the Global Assembly Cache (GAC). The Ribbon.dll contains a Ribbon Control and mainly the Wrapper Classes for the Ribbon Elements
RibbonGenerator.msi installs the components to the 32 Bit ProgramFiles folder RibbonLib and adds the Environment Path to this folder.

## Useful Links
[Microsoft Windows Ribbon Framework](https://docs.microsoft.com/en-us/windows/win32/windowsribbon/-uiplat-windowsribbon-entry)

[Codeplex archive](https://archive.codeplex.com/?p=windowsribbon)

[Table of Content for Documentation](https://www.codeproject.com/Articles/55599/Windows-Ribbon-for-WinForms-Part-Table-of-Conten)