//*****************************************************************************
//
//  File:       GalleryCommandPropertySet.cs
//
//  Contents:   Helper class that wraps a gallery command simple property set.
//
//*****************************************************************************

using System.Diagnostics;
using RibbonLib.Interop;

namespace RibbonLib
{
    /// <summary>
    /// Helper class that wraps a gallery command simple property set.
    /// </summary>
    public sealed class GalleryCommandPropertySet : AbstractPropertySet //, IUISimplePropertySet
    {
        private uint? _commandID;
        private CommandType? _commandType;
        private uint? _categoryID;

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

        /// <summary>
        /// Get or set the Command Type
        /// </summary>
        public CommandType CommandType
        {
            get
            {
                return _commandType.GetValueOrDefault();
            }
            set
            {
                _commandType = value;
            }
        }

        /// <summary>
        /// Get or set the Category ID
        /// </summary>
        public uint CategoryID
        {
            get
            {
                return _categoryID.GetValueOrDefault(Constants.UI_Collection_InvalidIndex);
            }
            set
            {
                _categoryID = value;
            }
        }

        /// <summary>
        /// Gets or sets the object that contains to this PropertySet
        /// Additional object for the user
        /// </summary>
        public object Tag { get; set; }

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
            
            if (key == RibbonProperties.CommandType)
            {
                if (_commandType.HasValue)
                {
                    value = PropVariant.FromObject((uint)_commandType.Value);
                }
                else
                {
                    value = PropVariant.Empty;
                }
                return HRESULT.S_OK;
            }
            
            if (key == RibbonProperties.CategoryID)
            {
                if (_categoryID.HasValue)
                {
                    value = PropVariant.FromObject(_categoryID.Value);
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
