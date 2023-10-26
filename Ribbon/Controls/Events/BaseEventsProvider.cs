//*****************************************************************************
//
//  File:       BaseEventsProvider.cs
//
//  Contents:   Base class for all events provider classes.
//              provides common members like: SupportedEvents.
//
//*****************************************************************************

using System.Collections.Generic;
using RibbonLib.Interop;

namespace RibbonLib.Controls.Events
{
    /// <summary>
    /// Base class for all events provider classes.
    /// provides common members like: SupportedEvents.
    /// </summary>
    public abstract class BaseEventsProvider : IEventsProvider
    {
        /// <summary>
        /// list of supported events
        /// </summary>
        private List<ExecutionVerb> _supportedEvents = new List<ExecutionVerb>();

        /// <summary>
        /// Initializes a new instance of the BaseEventsProvider
        /// </summary>
        protected BaseEventsProvider()
        {
        }

        #region IEventsProvider Members

        /// <summary>
        /// Get supported "execution verbs", or events
        /// </summary>
        public IList<ExecutionVerb> SupportedEvents
        {
            get
            {
                return _supportedEvents;
            }
        }

        /// <summary>
        /// Handles IUICommandHandler.Execute function for supported events
        /// </summary>
        /// <param name="verb">the mode of execution</param>
        /// <param name="key">the property that has changed</param>
        /// <param name="currentValue">the new value of the property that has changed</param>
        /// <param name="commandExecutionProperties">additional data for this execution</param>
        /// <returns>Returns S_OK if successful, or an error value otherwise</returns>
        protected abstract HRESULT ExecuteImpl(ExecutionVerb verb, PropertyKeyRef key, PropVariantRef currentValue, IUISimplePropertySet commandExecutionProperties);

        /// <summary>
        /// Handles IUICommandHandler.Execute function for supported events
        /// </summary>
        /// <param name="verb">the mode of execution</param>
        /// <param name="key">the property that has changed</param>
        /// <param name="currentValue">the new value of the property that has changed</param>
        /// <param name="commandExecutionProperties">additional data for this execution</param>
        /// <returns>Returns S_OK if successful, or an error value otherwise</returns>
        HRESULT IEventsProvider.Execute(ExecutionVerb verb, PropertyKeyRef key, PropVariantRef currentValue, IUISimplePropertySet commandExecutionProperties)
        {
            return ExecuteImpl(verb, key, currentValue, commandExecutionProperties);
        }

        #endregion
    }
}
