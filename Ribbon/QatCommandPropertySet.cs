//*****************************************************************************
//
//  File:       QatCommandPropertySet.cs
//
//  Contents:   Helper class that wraps a qat command IUISimplePropertySet.
//
//*****************************************************************************

using System.Diagnostics;
using RibbonLib.Interop;
using RibbonLib.Controls;

namespace RibbonLib
{
    /// <summary>
    /// Helper class that wraps a qat command IUISimplePropertySet.
    /// </summary>
    public sealed class QatCommandPropertySet : AbstractPropertySet //, IUISimplePropertySet
    {
        private uint? _commandID;

        /// <summary>
        /// The Ribbon Control like RibbonButton, ... which belongs to the CommandID
        /// </summary>
        public IRibbonControl RibbonCtrl { get; set; }

        /// <summary>
        /// Get or set the Command ID
        /// </summary>
        public uint CommandID
        {
            get
            {
                return _commandID.GetValueOrDefault();
            }
            set
            {
                _commandID = value;
            }
        }

        #region IUISimplePropertySet Members

        /// <summary>
        /// Retrieves the stored value of a given property
        /// </summary>
        /// <param name="key">The Property Key of interest.</param>
        /// <param name="value">When this method returns, contains a pointer to the value for key.</param>
        /// <returns></returns>
        public override HRESULT GetValueImpl(ref PropertyKey key, out PropVariant value)
        {
            if (key == RibbonProperties.CommandID)
            {
                if (_commandID.HasValue)
                {
                    value = PropVariant.FromObject(_commandID.Value);
                }
                else
                {
                    value = PropVariant.Empty;
                }
                return HRESULT.S_OK;
            }

            Debug.WriteLine(string.Format("Class {0} does not support property: {1}.", GetType(), RibbonProperties.GetPropertyKeyName(ref key)));

            value = PropVariant.Empty;
            return HRESULT.E_NOTIMPL;
        }

        #endregion
    }
}
