//*****************************************************************************
//
//  File:       ExecuteEventsArgs.cs
//
//  Contents:   Definition for execute events arguments 
//
//*****************************************************************************

using System;
using RibbonLib.Interop;

namespace RibbonLib.Controls.Events
{
    /// <summary>
    /// Definition for execute events arguments
    /// </summary>
    public class ExecuteEventArgs : EventArgs
    {
        private PropertyKeyRef _key;
        private PropVariantRef _currentValue;
        private IUISimplePropertySet _commandExecutionProperties;

        /// <summary>
        /// Initializes a new instance of the ExecuteEventArgs
        /// </summary>
        /// <param name="key"></param>
        /// <param name="currentValue"></param>
        /// <param name="commandExecutionProperties"></param>
        public ExecuteEventArgs(PropertyKeyRef key, PropVariantRef currentValue, IUISimplePropertySet commandExecutionProperties)
        {
            _key = key;
            _currentValue = currentValue;
            _commandExecutionProperties = commandExecutionProperties;
        }

        /// <summary>
        /// Get the key
        /// </summary>
        public PropertyKeyRef Key
        {
            get
            {
                return _key;
            }
        }

        /// <summary>
        /// Get the current value
        /// </summary>
        public PropVariantRef CurrentValue
        {
            get
            {
                return _currentValue;
            }
        }

        /// <summary>
        /// Get the Command Execution Properties
        /// </summary>
        public IUISimplePropertySet CommandExecutionProperties
        {
            get
            {
                return _commandExecutionProperties;
            }                
        }
    }
}
