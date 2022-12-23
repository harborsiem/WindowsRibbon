//*****************************************************************************
//
//  File:       RecentItemsPropertiesProvider.cs
//
//  Contents:   Definition for recent items properties provider 
//
//*****************************************************************************

using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using RibbonLib.Interop;

namespace RibbonLib.Controls.Properties
{
    /// <summary>
    /// Definition for recent items properties provider interface
    /// </summary>
    public interface IRecentItemsPropertiesProvider
    {
        /// <summary>
        /// Recent items property
        /// </summary>
        IList<RecentItemsPropertySet> RecentItems { get; set; }

        /// <summary>
        /// Maximal count of RecentItems
        /// </summary>
        int MaxCount { get; }
    }

    /// <summary>
    /// Implementation of IRecentItemsPropertiesProvider
    /// </summary>
    public class RecentItemsPropertiesProvider : BasePropertiesProvider, IRecentItemsPropertiesProvider
    {
        /// <summary>
        /// RecentItemsPropertiesProvider ctor
        /// </summary>
        /// <param name="ribbon">parent ribbon</param>
        /// <param name="commandId">ribbon control command id</param>
        public RecentItemsPropertiesProvider(Ribbon ribbon, uint commandId)
            : base(ribbon, commandId)
        {
            // add supported properties
            _supportedProperties.Add(RibbonProperties.RecentItems);
        }

        private IList<RecentItemsPropertySet> _recentItems = new List<RecentItemsPropertySet>();
        private bool _maxCountFound;

        /// <summary>
        /// Handles IUICommandHandler.UpdateProperty function for the supported properties
        /// </summary>
        /// <param name="key">The Property Key to update</param>
        /// <param name="currentValue">A pointer to the current value for key. This parameter can be null</param>
        /// <param name="newValue">When this method returns, contains a pointer to the new value for key</param>
        /// <returns>Returns S_OK if successful, or an error value otherwise</returns>
        public override HRESULT UpdateProperty(ref PropertyKey key, PropVariantRef currentValue, ref PropVariant newValue)
        {
            if (key == RibbonProperties.RecentItems)
            {
                if (!_maxCountFound && currentValue != null)
                {
                    if (currentValue.PropVariant.VarType == (VarEnum.VT_ARRAY | VarEnum.VT_UNKNOWN))
                    {
                        object[] oa = currentValue.PropVariant.Value as object[];
                        MaxCount = oa.Length;
                        _maxCountFound = true;
                    }
                }
                if (_recentItems != null && _recentItems.Count > 0)
                {
                    newValue.SetSafeArray(_recentItems.ToArray());
                }
            }

            return HRESULT.S_OK;
        }

        #region IRecentItemsPropertiesProvider Members

        /// <summary>
        /// Recent items property
        /// </summary>
        public IList<RecentItemsPropertySet> RecentItems
        {
            get
            {
                return _recentItems;
            }
            set
            {
                _recentItems = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int MaxCount
        {
            get;
            private set;
        }

        #endregion
    }
}
