using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using RibbonLib.Interop;

namespace RibbonLib
{
    /// <summary>
    /// Implementation of Com Interface IUIEventLogger
    /// </summary>
    [ComVisible(true)]
    [Guid("0406D872-D9E9-4D6D-A053-B12ECFC22C35")]
    public sealed class EventLogger : IUIEventLogger
    {
        private IUIEventingManager _eventingManager;
        private bool attached;

        /// <summary>
        /// Log Event
        /// </summary>
        public event EventHandler<EventLoggerEventArgs> LogEvent;

        internal EventLogger(IUIEventingManager eventingManager)
        {
            _eventingManager = eventingManager;
        }

        /// <summary>
        /// Attach to an IUIEventLogger and IUIEventingManager objects events
        /// </summary>
        public void Attach()
        {
            if (!attached)
            {
                _eventingManager.SetEventLogger(this);
                attached = true;
            }
        }

        /// <summary>
        /// Detach the log events
        /// </summary>
        public void Detach()
        {
            _eventingManager.SetEventLogger(null);
            attached = false;
        }

        /// <summary>
        /// Don't call it from user code
        /// </summary>
        /// <param name="pEventParams"></param>
        void IUIEventLogger.OnUIEvent([In] ref EventParameters pEventParams)
        {
            EventHandler<EventLoggerEventArgs> handler = LogEvent;
            if (handler != null)
            {
                handler(this, new EventLoggerEventArgs(ref pEventParams));
            }
        }
    }
}
