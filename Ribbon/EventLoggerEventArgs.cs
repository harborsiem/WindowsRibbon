using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using RibbonLib.Interop;

namespace RibbonLib
{
    /// <summary>
    /// The EventArgs of EventLogger
    /// </summary>
    public sealed class EventLoggerEventArgs : EventArgs
    {
        /// <summary>
        /// Identifies the types of events associated with a Ribbon.
        /// </summary>
        public EventType EventType { get; private set; }
        /// <summary>
        /// The application modes. Only used when a EventType ApplicationModeSwitched has been fired.
        /// In all other cases it is set to 0.
        /// </summary>
        public Int32 Modes { get; private set; }
        /// <summary>
        /// The ID of the Command directly related to the event, which is specified in the markup resource file.
        /// </summary>
        public uint CommandID { get; private set; }
        /// <summary>
        /// The Command name that is associated with CommandId.
        /// </summary>
        public String CommandName { get; private set; }
        /// <summary>
        /// The ID for the parent of the Command, which is specified in the markup resource file.
        /// </summary>
        public uint ParentCommandID { get; private set; }
        /// <summary>
        /// The Command name of the parent that is associated with CommandId.
        /// </summary>
        public String ParentCommandName { get; private set; }
        /// <summary>
        /// SelectionIndex is used only when a EventType CommandExecuted has been fired in response to the user selecting an item within a ComboBox or item gallery.
        /// In those cases, SelectionIndex contains the index of the selected item. In all other cases, it is set to 0.
        /// </summary>
        public uint SelectionIndex { get; private set; }
        /// <summary>
        /// Identifies the locations where events associated with a Ribbon control can originate.
        /// </summary>
        public EventLocation Location { get; private set; }

        internal EventLoggerEventArgs(ref EventParameters pEventParams)
        {
            EventType = pEventParams.EventType;
            switch (pEventParams.EventType)
            {
                case EventType.ApplicationModeSwitched:
                    Modes = (pEventParams.Modes);
                    break;
                case EventType.CommandExecuted:
                case EventType.TooltipShown:
                case EventType.TabActivated:
                case EventType.MenuOpened:
                    CopyAndMarshal(ref pEventParams);
                    break;
                case EventType.ApplicationMenuOpened:
                case EventType.RibbonExpanded:
                case EventType.RibbonMinimized:
                default:
                    break;
            }
        }

        private void CopyAndMarshal(ref EventParameters pEventParams)
        {
            CommandID = pEventParams.Params.CommandID;
            CommandName = Marshal.PtrToStringUni(pEventParams.Params.CommandName); //PCWStr
            ParentCommandID = pEventParams.Params.ParentCommandID;
            ParentCommandName = Marshal.PtrToStringUni(pEventParams.Params.ParentCommandName); //PCWStr
            SelectionIndex = pEventParams.Params.SelectionIndex;
            Location = pEventParams.Params.Location;
        }
    }
}
