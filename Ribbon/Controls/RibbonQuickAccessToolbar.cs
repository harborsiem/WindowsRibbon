//*****************************************************************************
//
//  File:       RibbonQuickAccessToolbar.cs
//
//  Contents:   Helper class that wraps the ribbon quick access toolbar.
//
//*****************************************************************************

using RibbonLib.Controls.Events;
using RibbonLib.Interop;
using System;

namespace RibbonLib.Controls
{
    /// <summary>
    /// Helper class that wraps the ribbon quick access toolbar.
    /// </summary>
    public class RibbonQuickAccessToolbar : BaseRibbonControl, 
        IExecuteEventsProvider
    {
        ///// <summary>
        ///// reference for parent ribbon class
        ///// </summary>
        //protected Ribbon _ribbon;

        ///// <summary>
        ///// QAT command id
        ///// </summary>
        //protected uint _commandID;

        /// <summary>
        /// handler for the customize button
        /// </summary>
        protected RibbonButton _customizeButton;

        private IUICollection _itemsSource = new UICollection();

        /// <summary>
        /// Initializes a new instance of the Ribbon QuickAccessToolbar (QAT)
        /// </summary>
        /// <param name="ribbon">Parent Ribbon control</param>
        /// <param name="commandId">Command id attached to this control</param>
        public RibbonQuickAccessToolbar(Ribbon ribbon, uint commandId) :base(ribbon, commandId)
        {
            _ribbon = ribbon;
            _commandID = commandId;
        }

        /// <summary>
        /// Initializes a new instance of the Ribbon QuickAccessToolbar (QAT)
        /// </summary>
        /// <param name="ribbon">Parent Ribbon control</param>
        /// <param name="commandId">Command id attached to this control</param>
        /// <param name="customizeCommandId">Customize Command id attached to this control</param>
        public RibbonQuickAccessToolbar(Ribbon ribbon, uint commandId, uint customizeCommandId) : this(ribbon, commandId)
        {
            _customizeButton = new RibbonButton(_ribbon, customizeCommandId);
        }

        #region IRibbonControl Members

        ///// <summary>
        ///// The command Id.
        ///// </summary>
        //public uint CommandID
        //{
        //    get 
        //    {
        //        return _commandID;
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        public override HRESULT Execute(ExecutionVerb verb, PropertyKeyRef key, PropVariantRef currentValue, IUISimplePropertySet commandExecutionProperties)
        {
            return HRESULT.S_OK;
        }

        /// <summary>
        /// 
        /// </summary>
        public override HRESULT UpdateProperty(ref PropertyKey key, PropVariantRef currentValue, ref PropVariant newValue)
        {
            //if (key == RibbonProperties.ItemsSource)
            //{
            //    if (_itemsSource != null)
            //    {
            //        IUICollection itemsSource = (IUICollection)currentValue.PropVariant.Value;

            //        itemsSource.Clear();
            //        uint count;
            //        _itemsSource.GetCount(out count);
            //        for (uint i = 0; i < count; ++i)
            //        {
            //            object item;
            //            _itemsSource.GetItem(i, out item);
            //            itemsSource.Add(item);
            //        }
            //    }
            //}
            return HRESULT.S_OK;
        }

        /// <summary>
        /// Items source property
        /// </summary>
        public IUICollection ItemsSource
        {
            get
            {
                if (_ribbon.Initialized)
                {
                    PropVariant unknownValue;
                    HRESULT hr = _ribbon.Framework.GetUICommandProperty(_commandID, ref RibbonProperties.ItemsSource, out unknownValue);
                    if (NativeMethods.Succeeded(hr))
                    {
                        return (IUICollection)unknownValue.Value;
                    }
                }

                return _itemsSource;
            }
        }

        #endregion

        #region IExecuteEventsProvider Members

        /// <summary>
        /// The customizeButton Click event
        /// </summary>
        public event EventHandler<ExecuteEventArgs> ExecuteEvent
        {
            add
            { 
                if (_customizeButton != null)
                {
                    _customizeButton.ExecuteEvent += value;
                }
            }
            remove
            {
                if (_customizeButton != null)
                {
                    _customizeButton.ExecuteEvent -= value;
                }
            }
        }

        #endregion
    }
}
