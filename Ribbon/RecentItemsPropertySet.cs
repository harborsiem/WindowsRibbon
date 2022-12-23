//*****************************************************************************
//
//  File:       RecentItemsPropertySet.cs
//
//  Contents:   Helper class that wraps a recent items simple property set.
//
//*****************************************************************************

using System.Diagnostics;
using RibbonLib.Interop;

namespace RibbonLib
{
    /// <summary>
    /// Helper class that wraps a recent items simple property set.
    /// </summary>
    public sealed class RecentItemsPropertySet : IUISimplePropertySet
    {
        private string _label;
        private string _labelDescription;
        private bool? _pinned;

        /// <summary>
        /// This is the label as it will appear on the ribbon.
        /// </summary>
        public string Label
        {
            get
            {
                return _label;
            }
            set
            {
                _label = value;
            }
        }

        /// <summary>
        /// A longer description. This description is used right side of the application menu
        /// </summary>
        public string LabelDescription
        {
            get
            {
                return _labelDescription;
            }
            set
            {
                _labelDescription = value;
            }
        }

        /// <summary>
        /// The pinned status
        /// </summary>
        public bool Pinned
        {
            get
            {
                return _pinned.GetValueOrDefault();
            }
            set
            {
                _pinned = value;
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
        public HRESULT GetValue(ref PropertyKey key, out PropVariant value)
        {
            if (key == RibbonProperties.Label)
            {
                if ((_label == null) || (_label.Trim() == string.Empty))
                {
                    value = PropVariant.Empty;
                }
                else
                {
                    value = PropVariant.FromObject(_label);
                }
                return HRESULT.S_OK;
            }

            if (key == RibbonProperties.LabelDescription)
            {
                if ((_labelDescription == null) || (_labelDescription.Trim() == string.Empty))
                {
                    value = PropVariant.Empty;
                }
                else
                {
                    value = PropVariant.FromObject(_labelDescription);
                }
                return HRESULT.S_OK;
            }
            
            if (key == RibbonProperties.Pinned)
            {
                if (_pinned.HasValue)
                {
                    value = PropVariant.FromObject(_pinned.Value);
                }
                else
                {
                    value = PropVariant.Empty;
                }
                return HRESULT.S_OK;
            }

            Debug.WriteLine(string.Format("Class {0} does not support property: {1}.", GetType().ToString(), RibbonProperties.GetPropertyKeyName(ref key)));

            value = PropVariant.Empty;
            return HRESULT.E_NOTIMPL;
        }

        #endregion

        /// <summary>
        /// Clone
        /// </summary>
        /// <returns></returns>
        public RecentItemsPropertySet Clone()
        {
            RecentItemsPropertySet result = new RecentItemsPropertySet()
            {
                Label = this.Label,
                LabelDescription = this.LabelDescription,
                Pinned = this.Pinned,
                Tag = this.Tag
            };
            return result;
        }
    }
}
