//*****************************************************************************
//
//  File:       PreviewEventsProvider.cs
//
//  Contents:   definition for preview events provider 
//
//*****************************************************************************

using RibbonLib.Interop;
using System;
using System.Threading;

namespace RibbonLib.Controls.Events
{
    /// <summary>
    /// Definition for Execute events provider
    /// </summary>
    public interface IPreviewEventsProvider
    {
        /// <summary>
        /// Preview event
        /// </summary>
        event EventHandler<ExecuteEventArgs> PreviewEvent;

        /// <summary>
        /// Cancel Preview event
        /// </summary>
        event EventHandler<ExecuteEventArgs> CancelPreviewEvent;
    }

    /// <summary>
    /// Implementation of IPreviewEventsProvider
    /// </summary>
    class PreviewEventsProvider : BaseEventsProvider, IPreviewEventsProvider
    {
        private object _sender;

        public PreviewEventsProvider(object sender)
        {
            _sender = sender;
            SupportedEvents.Add(ExecutionVerb.Preview);
            SupportedEvents.Add(ExecutionVerb.CancelPreview);
        }

        /// <summary>
        /// Handles IUICommandHandler.Execute function for supported events
        /// </summary>
        /// <param name="verb">the mode of execution</param>
        /// <param name="key">the property that has changed</param>
        /// <param name="currentValue">the new value of the property that has changed</param>
        /// <param name="commandExecutionProperties">additional data for this execution</param>
        /// <returns>Returns S_OK if successful, or an error value otherwise</returns>
        public override HRESULT Execute(ExecutionVerb verb, PropertyKeyRef key, PropVariantRef currentValue, IUISimplePropertySet commandExecutionProperties)
        {
            switch (verb)
            {
                case ExecutionVerb.Preview:
                    if (PreviewEvent != null)
                    {
                        try
                        {
                            PreviewEvent(_sender, new ExecuteEventArgs(key, currentValue, commandExecutionProperties));
                        }
                        catch (Exception ex) {
                            BaseRibbonControl ctrl = _sender as BaseRibbonControl;
                            if (ctrl != null)
                            {
                                ThreadExceptionEventArgs e = new ThreadExceptionEventArgs(ex);
                                if (ctrl._ribbon.OnRibbonEventException(_sender, e))
                                    return HRESULT.E_FAIL;
                            }
                            Environment.FailFast(ex.StackTrace);
                            return HRESULT.E_ABORT;
                        }
                    }
                    break;

                case ExecutionVerb.CancelPreview:
                    if (CancelPreviewEvent != null)
                    {
                        try
                        {
                            CancelPreviewEvent(_sender, new ExecuteEventArgs(key, currentValue, commandExecutionProperties));
                        }
                        catch (Exception ex) {
                            BaseRibbonControl ctrl = _sender as BaseRibbonControl;
                            if (ctrl != null)
                            {
                                ThreadExceptionEventArgs e = new ThreadExceptionEventArgs(ex);
                                if (ctrl._ribbon.OnRibbonEventException(_sender, e))
                                    return HRESULT.E_FAIL;
                            }
                            Environment.FailFast(ex.StackTrace);
                            return HRESULT.E_ABORT;
                        }
                    }
                    break;
            }
            return HRESULT.S_OK;
        }

        #region IPreviewEventsProvider Members

        /// <summary>
        /// Preview event
        /// </summary>
        public event EventHandler<ExecuteEventArgs> PreviewEvent;

        /// <summary>
        /// Cancel Preview event
        /// </summary>
        public event EventHandler<ExecuteEventArgs> CancelPreviewEvent;

        #endregion
    }
}
