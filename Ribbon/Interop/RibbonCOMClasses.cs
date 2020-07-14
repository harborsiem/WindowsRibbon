//****************************************************************************
//
//  File:       RibbonCOMClasses.cs
//
//  Contents:   Classes of the Windows Ribbon Framework, based on 
//              UIRibbon.idl from windows 7 SDK
//
//****************************************************************************

using System.Runtime.InteropServices;

namespace RibbonLib.Interop
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    // UIRibbonFramework class
    [ComImport]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid(RibbonCLSIDGuid.UIRibbonFramework)]
    public class UIRibbonFramework
    {
        // implements IUIFramework
    }


    // UIRibbonImageFromBitmapFactory class
    [ComImport]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid(RibbonCLSIDGuid.UIRibbonImageFromBitmapFactory)]
    public class UIRibbonImageFromBitmapFactory
    {
        // implements IUIImageFromBitmap
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
