//*****************************************************************************
//
//  File:       StringValuePropertiesProvider.cs
//
//  Contents:   Definition for string value Properties provider 
//
//*****************************************************************************

using RibbonLib.Interop;

namespace RibbonLib.Controls.Properties
{
    /// <summary>
    /// Definition for string value properties provider interface
    /// </summary>
    public interface IStringValuePropertiesProvider
    {
        /// <summary>
        /// String value property
        /// </summary>
        string StringValue { get; set; }
    }

    /// <summary>
    /// Implementation of IStringValuePropertiesProvider
    /// </summary>
    public class StringValuePropertiesProvider : BasePropertiesProvider, IStringValuePropertiesProvider
    {
        /// <summary>
        /// StringValuePropertiesProvider ctor
        /// </summary>
        /// <param name="ribbon">parent ribbon</param>
        /// <param name="commandId">ribbon control command id</param>
        public StringValuePropertiesProvider(Ribbon ribbon, uint commandId)
            : base(ribbon, commandId)
        { 
            // add supported properties
            _supportedProperties.Add(RibbonProperties.StringValue);
        }

        private string _stringValue;

        /// <summary>
        /// Handles IUICommandHandler.UpdateProperty function for the supported properties
        /// </summary>
        /// <param name="key">The Property Key to update</param>
        /// <param name="currentValue">A pointer to the current value for key. This parameter can be null</param>
        /// <param name="newValue">When this method returns, contains a pointer to the new value for key</param>
        /// <returns>Returns S_OK if successful, or an error value otherwise</returns>
        public override HRESULT UpdateProperty(ref PropertyKey key, PropVariantRef currentValue, ref PropVariant newValue)
        {
            if (key == RibbonProperties.StringValue)
            {
                if (_stringValue != null)
                {
                    newValue.SetString(_stringValue);
                }
            }
            
            return HRESULT.S_OK;
        }

        #region IStringValuePropertiesProvider Members

        /// <summary>
        /// String value property
        /// </summary>
        public string StringValue
        {
            get
            {
                if (_ribbon.Initialized)
                {
                    PropVariant stringValue;
                    HRESULT hr = _ribbon.Framework.GetUICommandProperty(_commandID, ref RibbonProperties.StringValue, out stringValue);
                    if (hr.Succeeded)
                    {
                        string result = (string)stringValue.Value;
                        PropVariant.Clear(ref stringValue);
                        return result;
                    }
                }

                return _stringValue;
            }
            set
            {
                _stringValue = value;

                if (_ribbon.Initialized)
                {
                    PropVariant stringValue;
                    if ((_stringValue == null) || (_stringValue.Trim() == string.Empty))
                    {
                        stringValue = PropVariant.Empty;
                    }
                    else
                    {
                        stringValue = PropVariant.FromObject(_stringValue);
                    } 
                    HRESULT hr = _ribbon.Framework.SetUICommandProperty(_commandID, ref RibbonProperties.StringValue, ref stringValue);
                    PropVariant.Clear(ref stringValue);
                }
            }
        }

        #endregion
    }
}
