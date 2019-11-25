//*****************************************************************************
//
//  File:       ExecuteEventsProvider.cs
//
//  Contents:   Definition for execute events provider 
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
    public interface IExecuteEventsProvider
    {
        /// <summary>
        /// Execute event
        /// </summary>
        event EventHandler<ExecuteEventArgs> ExecuteEvent;
    }

    /// <summary>
    /// Implementation of IExecuteEventsProvider
    /// </summary>
    public class ExecuteEventsProvider : BaseEventsProvider, IExecuteEventsProvider
    {
        private object _sender;

        public ExecuteEventsProvider(object sender)
        {
            _sender = sender;
            SupportedEvents.Add(ExecutionVerb.Execute);
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
            if (verb == ExecutionVerb.Execute)
            {
                if (ExecuteEvent != null)
                {
                    try
                    {
                        ExecuteEvent(_sender, new ExecuteEventArgs(key, currentValue, commandExecutionProperties));
                    }
                    catch (Exception ex)
                    {
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
            }

            return HRESULT.S_OK;
        }

        #region IExecuteEventsProvider Members

        /// <summary>
        /// Execute event
        /// </summary>
        public event EventHandler<ExecuteEventArgs> ExecuteEvent;

        #endregion
    }
}
