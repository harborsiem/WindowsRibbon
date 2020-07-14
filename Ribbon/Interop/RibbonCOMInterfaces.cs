//****************************************************************************
//
//  File:       RibbonCOMInterfaces.cs
//
//  Contents:   Interfaces of the Windows Ribbon Framework, based on 
//              UIRibbon.idl from windows 7 SDK
//
//****************************************************************************

using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace RibbonLib.Interop
{
    // Windows Ribbon interfaces implemented by the framework

    /// <summary>
    /// Specifies values that identify the availability
    /// of a contextual tab.
    /// </summary>
    public enum ContextAvailability
    {
        /// <summary>
        /// A contextual tab is not available for the selected object.
        /// </summary>
        NotAvailable = 0,
        /// <summary>
        /// A contextual tab is available for the selected object.
        /// The tab is not the active tab.
        /// </summary>
        Available = 1,
        /// <summary>
        /// A contextual tab is available for the selected object.
        /// The tab is the active tab.
        /// </summary>
        Active = 2,
    }

    /// <summary>
    /// Specifies values that identify the font property state
    /// of a FontControl, such as Strikethrough.
    /// </summary>
    public enum FontProperties
    {
        /// <summary>
        /// The property is not available.
        /// </summary>
        NotAvailable = 0,
        /// <summary>
        /// The property is not set.
        /// </summary>
        NotSet = 1,
        /// <summary>
        /// The property is set.
        /// </summary>
        Set = 2,
    }

    /// <summary>
    /// Specifies values that identify the
    /// vertical-alignment state of a FontControl.
    /// </summary>
    public enum FontVerticalPosition
    {
        /// <summary>
        /// Vertical positioning is not enabled.
        /// </summary>
        NotAvailable = 0,
        /// <summary>
        /// Vertical positioning is enabled but not toggled.
        /// </summary>
        NotSet = 1,
        /// <summary>
        /// Vertical positioning is enabled and toggled for superscript.
        /// </summary>
        SuperScript = 2,
        /// <summary>
        /// Vertical positioning is enabled and toggled for subscript.
        /// </summary>
        SubScript = 3,
    }

    /// <summary>
    /// Specifies values that identify the
    /// underline state of a FontControl.
    /// </summary>
    public enum FontUnderline
    {
        /// <summary>
        /// Underlining is not enabled.
        /// </summary>
        NotAvailable = 0,
        /// <summary>
        /// Underlining is off.
        /// </summary>
        NotSet = 1,
        /// <summary>
        /// Underlining is on.
        /// </summary>
        Set = 2,
    }

    /// <summary>
    /// Specifies values that identify whether
    /// the font size of a highlighted text run
    /// should be incremented or decremented.
    /// </summary>
    public enum FontDeltaSize
    {
        /// <summary>
        /// Increment the font size.
        /// </summary>
        Grow = 0,
        /// <summary>
        /// Decrement the font size.
        /// </summary>
        Shrink = 1,
    }

    /// <summary>
    /// Specifies values that identify the dock state
    /// of the Quick Access Toolbar (QAT).
    /// </summary>
    public enum ControlDock
    {
        /// <summary>
        /// The QAT is docked in the nonclient area of the Ribbon host application.
        /// </summary>
        Top = 1,
        /// <summary>
        /// The QAT is docked as a visually integral band below the Ribbon,
        /// </summary>
        Bottom = 3,
    }


    // Types for the color picker

    /// <summary>
    /// Specifies the values that identify how a color swatch
    /// in a DropDownColorPicker or a FontControl color picker
    /// (Text color or Text highlight) is filled.
    /// </summary>
    public enum SwatchColorType
    {
        /// <summary>
        /// The swatch is transparent.
        /// </summary>
        NoColor = 0,
        /// <summary>
        /// The swatch is filled with a solid RGB color
        /// bound to GetSysColor(COLOR_WINDOWTEXT).
        /// </summary>
        Automatic = 1,
        /// <summary>
        /// The swatch is filled with a solid RGB color.
        /// </summary>
        RGB = 2,
    }

    /// <summary>
    /// Specifies whether a swatch has normal or monochrome mode.
    /// </summary>
    public enum SwatchColorMode
    {
        /// <summary>
        /// The swatch is normal mode.
        /// </summary>
        Normal = 0,
        /// <summary>
        /// The swatch is monochrome. The swatch's RGB color
        /// value will be interpreted as a 1 bit-per-pixel pattern.
        /// </summary>
        Monochrome = 1,
    }


    /// <summary>
    /// Simple property bag
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid(RibbonIIDGuid.IUISimplePropertySet)]
    public interface IUISimplePropertySet
    {
        /// <summary>
        /// Retrieves the stored value of a given property
        /// </summary>
        /// <param name="key">The Property Key of interest.</param>
        /// <param name="value">When this method returns, contains a pointer to the value for key.</param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT GetValue([In] ref PropertyKey key, [Out] out PropVariant value);
    }


    /// <summary>
    /// Ribbon view interface
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid(RibbonIIDGuid.IUIRibbon)]
    public interface IUIRibbon
    {
        /// <summary>
        /// Returns the Ribbon height
        /// </summary>
        /// <param name="cy"></param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT GetHeight([Out] out UInt32 cy);

        /// <summary>
        /// Load Ribbon parameters (e.g. QuickAccessToolbar) from a stream
        /// </summary>
        /// <param name="pStream"></param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT LoadSettingsFromStream([In, MarshalAs(UnmanagedType.Interface)] IStream pStream);

        /// <summary>
        /// Save Ribbon parameters (e.g. QuickAccessToolbar) to a stream
        /// </summary>
        /// <param name="pStream"></param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT SaveSettingsToStream([In, MarshalAs(UnmanagedType.Interface)] IStream pStream);
    }

    /// <summary>
    /// Specifies values that identify the aspect
    /// of a Command to invalidate.
    /// </summary>
    [Flags]
    public enum Invalidations
    {
        /// <summary>
        /// A state property, such as UI_PKEY_Enabled.
        /// </summary>
        State = 0X00000001,
        /// <summary>
        /// The value property of a Command.
        /// </summary>
        Value = 0X00000002,
        /// <summary>
        /// Any property.
        /// </summary>
        Property = 0X00000004,
        /// <summary>
        /// All properties.
        /// </summary>
        AllProperties = 0X00000008,
    }


    /// <summary>
    /// Windows Ribbon Application interface
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid(RibbonIIDGuid.IUIFramework)]
    public interface IUIFramework
    {
        /// <summary>
        /// Connects the framework and the application
        /// </summary>
        /// <param name="frameWnd"></param>
        /// <param name="application"></param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT Initialize(IntPtr frameWnd, IUIApplication application);

        /// <summary>
        /// Releases all framework objects
        /// </summary>
        /// <returns></returns>
        [PreserveSig]
        HRESULT Destroy();

        /// <summary>
        /// Loads and instantiates the views and commands specified in markup
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT LoadUI(IntPtr instance, [MarshalAs(UnmanagedType.LPWStr)] string resourceName);

        /// <summary>
        /// Retrieves a pointer to a view object
        /// </summary>
        /// <param name="viewID"></param>
        /// <param name="riid"></param>
        /// <param name="ppv"></param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT GetView(UInt32 viewID, [In] ref Guid riid, [Out, MarshalAs(UnmanagedType.Interface, IidParameterIndex = 1)] out object ppv);

        /// <summary>
        /// Retrieves the current value of a property
        /// </summary>
        /// <param name="commandID"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT GetUICommandProperty(UInt32 commandID, [In] ref PropertyKey key, [Out] out PropVariant value);

        /// <summary>
        /// Immediately sets the value of a property
        /// </summary>
        /// <param name="commandID"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT SetUICommandProperty(UInt32 commandID, [In] ref PropertyKey key, [In] ref PropVariant value);

        /// <summary>
        /// Asks the framework to retrieve the new value of a property at the next update cycle
        /// </summary>
        /// <param name="commandID"></param>
        /// <param name="flags"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT InvalidateUICommand(UInt32 commandID, Invalidations flags, [In, Optional] PropertyKeyRef key);

        /// <summary>
        /// Flush all the pending UI command updates
        /// </summary>
        /// <returns></returns>
        [PreserveSig]
        HRESULT FlushPendingInvalidations();

        /// <summary>
        /// Asks the framework to switch to the list of modes specified and adjust visibility of controls accordingly
        /// </summary>
        /// <param name="iModes"></param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT SetModes(Int32 iModes);
    }


    /// <summary>
    /// Windows Ribbon ContextualUI interface
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid(RibbonIIDGuid.IUIContextualUI)]
    public interface IUIContextualUI
    {
        /// <summary>
        /// Sets the desired anchor point where ContextualUI should be displayed.
        /// Typically this is the mouse location at the time of right click.
        /// x and y are in virtual screen coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT ShowAtLocation(Int32 x, Int32 y);
    }


    /// <summary>
    /// Windows Ribbon Collection interface
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid(RibbonIIDGuid.IUICollection)]
    public interface IUICollection
    {
        /// <summary>
        /// Retrieves the count of the collection
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT GetCount([Out] out UInt32 count);

        /// <summary>
        /// Retrieves an item
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT GetItem(UInt32 index, [Out, MarshalAs(UnmanagedType.IUnknown)] out object item);

        /// <summary>
        /// Adds an item to the end
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT Add([In, MarshalAs(UnmanagedType.IUnknown)] object item);

        /// <summary>
        /// Inserts an item
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT Insert(UInt32 index, [In, MarshalAs(UnmanagedType.IUnknown)] object item);

        /// <summary>
        /// Removes an item at the specified position
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT RemoveAt(UInt32 index);

        /// <summary>
        /// Replaces an item at the specified position
        /// </summary>
        /// <param name="indexReplaced"></param>
        /// <param name="itemReplaceWith"></param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT Replace(UInt32 indexReplaced, [In, MarshalAs(UnmanagedType.IUnknown)] object itemReplaceWith);

        /// <summary>
        /// Clear the collection
        /// </summary>
        /// <returns></returns>
        [PreserveSig]
        HRESULT Clear();
    }


    /// <summary>
    /// Specifies values that identify the types
    /// of changes that can be made to a collection.
    /// </summary>
    public enum CollectionChange
    {
        /// <summary>
        /// Insert an item into the collection.
        /// </summary>
        Insert = 0,
        /// <summary>
        /// Delete an item from the collection.
        /// </summary>
        Remove = 1,
        /// <summary>
        /// Replace an item in the collection.
        /// </summary>
        Replace = 2,
        /// <summary>
        /// Delete all items from the collection.
        /// </summary>
        Reset = 3,
    }

    /// <summary>
    /// Constants
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Invalid index in UI_Collection
        /// </summary>
        public const UInt32 UI_Collection_InvalidIndex = 0xffffffff;
        /// <summary>
        /// All commands value for IUIFramework
        /// </summary>
        public const UInt32 UI_All_Commands = 0;
    }


    /// <summary>
    /// Connection Sink for listening to collection changes
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid(RibbonIIDGuid.IUICollectionChangedEvent)]
    public interface IUICollectionChangedEvent
    {
        /// <summary>
        /// A collection has changed
        /// </summary>
        /// <param name="action"></param>
        /// <param name="oldIndex"></param>
        /// <param name="oldItem"></param>
        /// <param name="newIndex"></param>
        /// <param name="newItem"></param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT OnChanged(CollectionChange action,
                          UInt32 oldIndex,
                          [In, Optional, MarshalAs(UnmanagedType.IUnknown)] object oldItem,
                          UInt32 newIndex,
                          [In, Optional, MarshalAs(UnmanagedType.IUnknown)] object newItem);
    }


    // Windows Ribbon interfaces implemented by the application

    /// <summary>
    /// Specifies values that identify the execution IDs
    /// that map to actions a user can initiate on a Command.
    /// </summary>
    public enum ExecutionVerb
    {
        /// <summary>
        /// Execute
        /// </summary>
        Execute = 0,
        /// <summary>
        /// Preview
        /// </summary>
        Preview = 1,
        /// <summary>
        /// Cancel preview
        /// </summary>
        CancelPreview = 2,
    }


    /// <summary>
    /// Command handler interface
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid(RibbonIIDGuid.IUICommandHandler)]
    public interface IUICommandHandler
    {
        /// <summary>
        /// User action callback, with transient execution parameters
        /// </summary>
        /// <param name="commandID">the command that has been executed</param>
        /// <param name="verb">the mode of execution</param>
        /// <param name="key">the property that has changed</param>
        /// <param name="currentValue">the new value of the property that has changed</param>
        /// <param name="commandExecutionProperties">additional data for this execution</param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT Execute(UInt32 commandID,
                        ExecutionVerb verb,
                        [In, Optional] PropertyKeyRef key,
                        [In, Optional] PropVariantRef currentValue,
                        [In, Optional] IUISimplePropertySet commandExecutionProperties);

        /// <summary>
        /// Informs of the current value of a property, and queries for the new one
        /// </summary>
        /// <param name="commandID">The ID for the Command, which is specified in the Markup resource file</param>
        /// <param name="key">The Property Key to update</param>
        /// <param name="currentValue">A pointer to the current value for key. This parameter can be null</param>
        /// <param name="newValue">When this method returns, contains a pointer to the new value for key</param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT UpdateProperty(UInt32 commandID,
                               [In] ref PropertyKey key,
                               [In, Optional] PropVariantRef currentValue,
                               [In, Out] ref PropVariant newValue);
    }


    /// <summary>
    /// Specifies values that identify the type
    /// of Command associated with a Ribbon control.
    /// </summary>
    public enum CommandType
    {
        /// <summary>
        /// The type of command is not known.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Group
        /// </summary>
        Group = 1,
        /// <summary>
        /// Action (Button, HelpButton)
        /// </summary>
        Action = 2,
        /// <summary>
        /// Anchor (ApplicationMenu, DropDownButton,
        /// SplitButton, Tab)
        /// </summary>
        Anchor = 3,
        /// <summary>
        /// Context (TabGroup)
        /// </summary>
        Context = 4,
        /// <summary>
        /// Collection (ComboBox, DropDownGallery,
        /// InRibbonGallery, SplitButtonGallery)
        /// </summary>
        Collection = 5,
        /// <summary>
        /// Command collection (DropDownGallery, InRibbonGallery,
        /// QuickAccessToolbar, SplitButtonGallery)
        /// </summary>
        Commandcollection = 6,
        /// <summary>
        /// Decimal (Spinner)
        /// </summary>
        Decimal = 7,
        /// <summary>
        /// Boolean (ToggleButton, CheckBox)
        /// </summary>
        Boolean = 8,
        /// <summary>
        /// Font (FontControl)
        /// </summary>
        Font = 9,
        /// <summary>
        /// RecentItems
        /// </summary>
        RecentItems = 10,
        /// <summary>
        /// ColorAnchor (DropDownColorPicker)
        /// </summary>
        ColorAnchor = 11,
        /// <summary>
        /// ColorCollection.
        /// This Command type is not supported by any framework controls.
        /// </summary>
        ColorCollection = 12,
    }


    /// <summary>
    /// Specifies values that identify the Ribbon framework View.
    /// </summary>
    public enum ViewType
    {
        /// <summary>
        /// A Ribbon View.
        /// </summary>
        Ribbon = 1,
    }


    /// <summary>
    /// Specifies values that identify the type of action
    /// to complete on a Ribbon framework View.
    /// </summary>
    public enum ViewVerb
    {
        /// <summary>
        /// Create a View.
        /// </summary>
        Create = 0,
        /// <summary>
        /// Destroy a View.
        /// </summary>
        Destroy = 1,
        /// <summary>
        /// Resize a View.
        /// </summary>
        Size = 2,
        /// <summary>
        /// Unable to complete the action.
        /// </summary>
        Error = 3,
    }


    /// <summary>
    /// Application callback interface
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid(RibbonIIDGuid.IUIApplication)]
    public interface IUIApplication
    {
        /// <summary>
        /// A view has changed
        /// </summary>
        /// <param name="viewID"></param>
        /// <param name="typeID"></param>
        /// <param name="view"></param>
        /// <param name="verb"></param>
        /// <param name="uReasonCode"></param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT OnViewChanged(UInt32 viewID,
                              ViewType typeID,
                              [In, MarshalAs(UnmanagedType.IUnknown)] object view,
                              ViewVerb verb,
                              Int32 uReasonCode);

        /// <summary>
        /// Command creation callback
        /// </summary>
        /// <param name="commandID"></param>
        /// <param name="typeID"></param>
        /// <param name="commandHandler"></param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT OnCreateUICommand(UInt32 commandID,
                                  CommandType typeID,
                                  [Out] out IUICommandHandler commandHandler);

        /// <summary>
        /// Command destroy callback
        /// </summary>
        /// <param name="commandID"></param>
        /// <param name="typeID"></param>
        /// <param name="commandHandler"></param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT OnDestroyUICommand(UInt32 commandID,
                                   CommandType typeID,
                                   [In, Optional] IUICommandHandler commandHandler);
    }


    /// <summary>
    /// Container for a bitmap image
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid(RibbonIIDGuid.IUIImage)]
    public interface IUIImage
    {
        /// <summary>
        /// Retrieves a bitmap to display as an icon in the ribbon and context popup UI of the Windows Ribbon (Ribbon) framework.
        /// </summary>
        /// <param name="bitmap">Bitmap handle</param>
        /// <returns>HRESULT</returns>
        [PreserveSig]
        HRESULT GetBitmap([Out] out IntPtr bitmap);
    }

    /// <summary>
    /// Ownership of HBITMAP
    /// </summary>
    public enum Ownership
    {
        /// <summary>
        /// The handle to the bitmap (HBITMAP) is owned by the
        /// Ribbon framework through the IUIImage object.
        /// </summary>
        Transfer = 0,
        /// <summary>
        /// A copy of the HBITMAP is created by the 
        /// Ribbon framework through the IUIImage object.
        /// The host application still owns the HBITMAP.
        /// </summary>
        Copy = 1,
    }

    /// <summary>
    /// Produces containers for bitmap images
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid(RibbonIIDGuid.IUIImageFromBitmap)]
    public interface IUIImageFromBitmap
    {
        /// <summary>
        /// Creates an IUIImage object from a bitmap image.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="options"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT CreateImage(IntPtr bitmap, Ownership options, [Out, MarshalAs(UnmanagedType.Interface)] out IUIImage image);
    }

    //following types and interfaces are in UIRibbon since Windows 8, not used yet
    /// <summary>
    /// Identifies the types of events associated with a Ribbon.
    /// UI_EVENTTYPE enum
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// The ApplicationMenu opened
        /// </summary>
        ApplicationMenuOpened = 0,
        /// <summary>
        /// The Ribbon minimized
        /// </summary>
        RibbonMinimized = 1,
        /// <summary>
        /// The Ribbon expanded
        /// </summary>
        RibbonExpanded = 2,
        /// <summary>
        /// The application mode changed
        /// </summary>
        ApplicationModeSwitched = 3,
        /// <summary>
        /// A Tab activated
        /// </summary>
        TabActivated = 4,
        /// <summary>
        /// A menu opened
        /// </summary>
        MenuOpened = 5,
        /// <summary>
        /// A Command executed
        /// </summary>
        CommandExecuted = 6,
        /// <summary>
        /// A Command tooltip displayed.
        /// </summary>
        TooltipShown = 7
    }

    /// <summary>
    /// Identifies the locations where events associated
    /// with a Ribbon control can originate.
    /// UI_EVENTLOCATION enum
    /// </summary>
    public enum EventLocation
    {
        /// <summary>
        /// The Ribbon
        /// </summary>
        Ribbon = 0,
        /// <summary>
        /// The QuickAccessToolbar
        /// </summary>
        QAT = 1,
        /// <summary>
        /// The ApplicationMenu
        /// </summary>
        ApplicationMenu = 2,
        /// <summary>
        /// The ContextPopup
        /// </summary>
        ContextPopup = 3
    }

    /// <summary>
    /// Contains information about a Command associated with a event.
    /// Marshalling of strings can only be done in the wrapper class of interface IUIEventLogger
    /// UI_EVENTPARAMS_COMMAND struct
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct EventParametersCommand
    {
        /// <summary>
        /// The command Id
        /// </summary>
        public uint CommandID;
        /// <summary>
        /// The command name (not Marshaled)
        /// </summary>
        public IntPtr CommandName; //PCWStr
        /// <summary>
        /// The parent command Id
        /// </summary>
        public uint ParentCommandID;
        /// <summary>
        /// The parent command name (not Marshaled)
        /// </summary>
        public IntPtr ParentCommandName; //PCWStr
        /// <summary>
        /// 
        /// </summary>
        public uint SelectionIndex;
        /// <summary>
        /// The event location
        /// </summary>
        public EventLocation Location;
    }

    /// <summary>
    /// Contains information about a Ribbon event.
    /// UI_EVENTPARAMS struct
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct EventParameters
    {
        /// <summary>
        /// The event type
        /// </summary>
        [FieldOffset(0)]
        public EventType EventType;
        /// <summary>
        /// Application modes
        /// </summary>
        [FieldOffset(4)]
        public Int32 Modes;
        /// <summary>
        /// Event command parameters
        /// </summary>
        [FieldOffset(4)]
        public EventParametersCommand Params;
    }

    /// <summary>
    /// The IUIEventLogger interface is implemented by the application
    /// and defines the ribbon events callback method.
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid(RibbonIIDGuid.IUIEventLogger)]
    public interface IUIEventLogger
    {
        /// <summary>
        /// Receives notifications that a ribbon event has occurred.
        /// </summary>
        /// <param name="pEventParams">The parameters associated with the event. This value varies according to the event type.</param>
        [PreserveSig]
        void OnUIEvent([In] ref EventParameters pEventParams);
    }

    /// <summary>
    /// The IUIEventingManager interface is implemented by
    /// the Ribbon framework and provides the notification
    /// functionality for applications that register for ribbon events.
    /// Getting the interface is possible with the as operator
    /// from IUIFramework variable (Ribbon.Framework) after
    /// Framework.LoadUI(...)
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid(RibbonIIDGuid.IUIEventingManager)]
    public interface IUIEventingManager
    {
        /// <summary>
        /// Sets the event logger for ribbon events.
        /// </summary>
        /// <param name="eventLogger">The event logger. If null, disables event logging.</param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT SetEventLogger([In] IUIEventLogger eventLogger);
    }
}



