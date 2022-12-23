//*****************************************************************************
//
//  File:       RibbonRecentItems.cs
//
//  Contents:   Helper class that wraps a ribbon recent items.
//
//*****************************************************************************

using System.Collections.Generic;
using RibbonLib.Controls.Events;
using RibbonLib.Controls.Properties;
using System;

namespace RibbonLib.Controls
{
    /// <summary>
    /// Helper class that wraps a ribbon recent items.
    /// </summary>
    public class RibbonRecentItems : BaseRibbonControl, 
        IRecentItemsPropertiesProvider,
        IKeytipPropertiesProvider,
        IExecuteEventsProvider
    {
        private KeytipPropertiesProvider _keytipPropertiesProvider;
        private RecentItemsPropertiesProvider _recentItemsPropertiesProvider;
        private ExecuteEventsProvider _executeEventsProvider;

        /// <summary>
        /// Initializes a new instance of the Ribbon RecentItems
        /// </summary>
        /// <param name="ribbon">Parent Ribbon control</param>
        /// <param name="commandId">Command id attached to this control</param>
        public RibbonRecentItems(Ribbon ribbon, uint commandId)
            : base(ribbon, commandId)
        {
            AddPropertiesProvider(_recentItemsPropertiesProvider= new RecentItemsPropertiesProvider(ribbon, commandId));
            AddPropertiesProvider(_keytipPropertiesProvider = new KeytipPropertiesProvider(ribbon, commandId));

            AddEventsProvider(_executeEventsProvider = new ExecuteEventsProvider(this));
        }

        #region IRecentItemsPropertiesProvider Members

        /// <summary>
        /// This property contains the list of the recent items.
        /// </summary>
        public IList<RecentItemsPropertySet> RecentItems
        {
            get
            {
                return _recentItemsPropertiesProvider.RecentItems;
            }
            set
            {
                _recentItemsPropertiesProvider.RecentItems = value;
            }
        }

        /// <summary>
        /// This property contains the maximum count of recent items.
        /// This is configured in the RibbonMarkup file
        /// The value is available after first showing the file menu
        /// </summary>
        public int MaxCount
        {
            get
            {
                return _recentItemsPropertiesProvider.MaxCount;
            }
        }

        #endregion

        #region IKeytipPropertiesProvider Members

        /// <summary>
        /// The keytip or key sequence that is used to access the command using the Alt key.
        /// This keytip appears when the user presses the Alt key to navigate the ribbon.
        /// The Ribbon Framework will automatically apply keytips to every command.
        /// However, if you want more control over the keytips used, you can specify them yourself.
        /// A keytip is not limited to a single character.
        /// </summary>
        public string Keytip
        {
            get
            {
                return _keytipPropertiesProvider.Keytip;
            }
            set
            {
                _keytipPropertiesProvider.Keytip = value;
            }
        }

        #endregion

        #region IExecuteEventsProvider Members

        /// <summary>
        /// Event provider similar to a Click event.
        /// </summary>
        public event EventHandler<ExecuteEventArgs> ExecuteEvent
        {
            add
            {
                _executeEventsProvider.ExecuteEvent += value;
            }
            remove
            {
                _executeEventsProvider.ExecuteEvent -= value;
            }
        }

        #endregion
    }
}
