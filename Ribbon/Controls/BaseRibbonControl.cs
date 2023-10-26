//*****************************************************************************
//
//  File:       BaseRibbonControl.cs
//
//  Contents:   Base class for all ribbon control helper classes, provides 
//              common functionality for ribbon controls.
//
//*****************************************************************************

using System.Collections.Generic;
using System.Diagnostics;
using RibbonLib.Controls.Events;
using RibbonLib.Controls.Properties;
using RibbonLib.Interop;

namespace RibbonLib.Controls
{
    /// <summary>
    /// Base class for all ribbon control helper classes, provides 
    /// common functionality for ribbon controls.
    /// </summary>
    public class BaseRibbonControl : IRibbonControl
    {
        private string _name;

        /// <summary>
        /// reference for parent ribbon class
        /// </summary>
        internal protected Ribbon _ribbon;

        /// <summary>
        /// ribbon control command id
        /// </summary>
        protected uint _commandID;

        /// <summary>
        /// map of registered properties for this ribbon control
        /// </summary>
        protected Dictionary<PropertyKey, IPropertiesProvider> _mapProperties = new Dictionary<PropertyKey, IPropertiesProvider>();

        /// <summary>
        /// map of registered events for this ribbon control
        /// </summary>
        protected Dictionary<ExecutionVerb, IEventsProvider> _mapEvents = new Dictionary<ExecutionVerb, IEventsProvider>();

        /// <summary>
        /// BaseRibbonControl ctor
        /// </summary>
        /// <param name="ribbon">parent ribbon</param>
        /// <param name="commandID">command id attached to this control</param>
        protected BaseRibbonControl(Ribbon ribbon, uint commandID)
        {
            _ribbon = ribbon;
            _commandID = commandID;

            ribbon.AddRibbonControl(this);
        }

        /// <summary>
        /// Register a properties provider with this ribbon control
        /// </summary>
        /// <param name="propertiesProvider">properties provider</param>
        protected void AddPropertiesProvider(IPropertiesProvider propertiesProvider)
        {
            foreach (PropertyKey propertyKey in propertiesProvider.SupportedProperties)
            {
                _mapProperties[propertyKey] = propertiesProvider;
            }
        }

        /// <summary>
        /// Register an events provider with this ribbon control
        /// </summary>
        /// <param name="eventsProvider">events provider</param>
        protected void AddEventsProvider(IEventsProvider eventsProvider)
        {
            foreach (ExecutionVerb verb in eventsProvider.SupportedEvents)
            {
                _mapEvents[verb] = eventsProvider;
            }
        }

        /// <summary>
        /// The name of RibbonStripItem
        /// </summary>
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                    return string.Empty;
                return _name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    _name = null;
                else
                    _name = value;
            }
        }

        #region IRibbonControl Members

        /// <summary>
        /// The Command Id, member of IRibbonControl
        /// </summary>
        public uint CommandID
        {
            get
            {
                return _commandID;
            }
        }

        /// <summary>
        /// Handles IUICommandHandler.Execute function for this ribbon control
        /// </summary>
        /// <param name="verb">the mode of execution</param>
        /// <param name="key">the property that has changed</param>
        /// <param name="currentValue">the new value of the property that has changed</param>
        /// <param name="commandExecutionProperties">additional data for this execution</param>
        /// <returns>Returns S_OK if successful, or an error value otherwise</returns>
        protected virtual HRESULT ExecuteImpl(ExecutionVerb verb, PropertyKeyRef key, PropVariantRef currentValue, IUISimplePropertySet commandExecutionProperties)
        {
            // check if verb is registered with this ribbon control
            if (_mapEvents.ContainsKey(verb))
            {
                // find events provider
                IEventsProvider eventsProvider = _mapEvents[verb];

                // delegates execution to events provider
                return eventsProvider.Execute(verb, key, currentValue, commandExecutionProperties);
            }

            Debug.WriteLine(string.Format("Class {0} does not support verb: {1}.", GetType(), verb));
            return HRESULT.E_NOTIMPL;
        }

        /// <summary>
        /// Handles IUICommandHandler.Execute function for this ribbon control
        /// </summary>
        /// <param name="verb">the mode of execution</param>
        /// <param name="key">the property that has changed</param>
        /// <param name="currentValue">the new value of the property that has changed</param>
        /// <param name="commandExecutionProperties">additional data for this execution</param>
        /// <returns>Returns S_OK if successful, or an error value otherwise</returns>
        HRESULT IRibbonControl.Execute(ExecutionVerb verb, PropertyKeyRef key, PropVariantRef currentValue, IUISimplePropertySet commandExecutionProperties)
        {
            return ExecuteImpl(verb, key, currentValue, commandExecutionProperties);
        }

        /// <summary>
        /// Handles IUICommandHandler.UpdateProperty function for this ribbon control
        /// </summary>
        /// <param name="key">The Property Key to update</param>
        /// <param name="currentValue">A pointer to the current value for key. This parameter can be null</param>
        /// <param name="newValue">When this method returns, contains a pointer to the new value for key</param>
        /// <returns>Returns S_OK if successful, or an error value otherwise</returns>
        protected virtual HRESULT UpdatePropertyImpl(ref PropertyKey key, PropVariantRef currentValue, ref PropVariant newValue)
        {
            // check if property is registered with this ribbon control
            if (_mapProperties.ContainsKey(key))
            {
                // find property provider
                IPropertiesProvider propertiesProvider = _mapProperties[key];

                // delegates execution to property provider
                return propertiesProvider.UpdateProperty(ref key, currentValue, ref newValue);
            }

            Debug.WriteLine(string.Format("Class {0} does not support property: {1}.", GetType(), RibbonProperties.GetPropertyKeyName(ref key)));
            return HRESULT.S_OK;
        }

        /// <summary>
        /// Handles IUICommandHandler.UpdateProperty function for this ribbon control
        /// </summary>
        /// <param name="key">The Property Key to update</param>
        /// <param name="currentValue">A pointer to the current value for key. This parameter can be null</param>
        /// <param name="newValue">When this method returns, contains a pointer to the new value for key</param>
        /// <returns>Returns S_OK if successful, or an error value otherwise</returns>
        HRESULT IRibbonControl.UpdateProperty(ref PropertyKey key, PropVariantRef currentValue, ref PropVariant newValue)
        {
            return UpdatePropertyImpl(ref key, currentValue, ref newValue);
        }

        /// <summary>
        /// Gets or sets the object that contains data about the control
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// The CommandType of the Control
        /// If the CommandType is CommandType.Unknown then the Control is not initialized by the Framework
        /// </summary>
        public CommandType CommandType { get; internal set; }

        #endregion
    }
}
