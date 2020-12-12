/*
usage
MainForm (_mainForm):
        public ShortCutKeysHandler ShortCutKeysHandler { get; private set; }

        Ctor of MainForm
        InitializeComponent();
        ...
        ShortCutKeysHandler = new ShortCutKeysHandler(base.ProcessCmdKey);


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return ShortCutKeysHandler.ProcessCmdKey(ref msg, keyData);
        }

class (mainly UserControl) with TAction and ShortcutKeys:

        //after init of TActionList (_actionList)

        //Activate the ShortcutKeys
        _mainForm.ShortCutKeysHandler.Add(_actionList);
        //Deactivate the ShortcutKeys
        _mainForm.ShortCutKeysHandler.Remove(_actionList);
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WinForms.Actions
{
    /// <summary>
    /// A delegate for handling ShortcutKeys
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="keys"></param>
    /// <returns></returns>
    public delegate bool ShortCutKeysDelegate(ref Message msg, Keys keys);

    /// <summary>
    /// Set a class instance to the mainForm
    /// </summary>
    public class ShortCutKeysHandler
    {
        //Windows message
        private const UInt32 WM_KEYDOWN = 0x0100;

        private ShortCutKeysDelegate _baseProcessCmdKey;
        private List<Handler> _handlerList = new List<Handler>();

        /// <summary>
        /// Only a Form class can instantiate this class
        /// </summary>
        /// <param name="baseProcessCmdKey">Method base.ProcessCmdKey</param>
        public ShortCutKeysHandler(ShortCutKeysDelegate baseProcessCmdKey)
        {
            if (baseProcessCmdKey == null)
                throw new ArgumentNullException(nameof(baseProcessCmdKey));
            _baseProcessCmdKey = baseProcessCmdKey;
        }

        /// <summary>
        /// Add a class (Control) with an TActionList and Shortcuts in TAction
        /// </summary>
        /// <param name="actionList"></param>
        public void Add(TActionList actionList)
        {
            if (actionList == null)
                throw new ArgumentNullException(nameof(actionList));
            for (int i = 0; i < _handlerList.Count; i++)
            {
                if (_handlerList[i].ActionList == actionList)
                    return; //don't add the same actionList
            }
            Handler handler = new Handler(actionList);
            _handlerList.Add(handler);
        }

        /// <summary>
        /// Add a method that handles ShortcutKeys
        /// </summary>
        /// <param name="method"></param>
        public void Add(ShortCutKeysDelegate method)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));
            for (int i = 0; i < _handlerList.Count; i++)
            {
                if (_handlerList[i].ShortCutKeys == method)
                    return; //don't add the same method
            }
            Handler handler = new Handler(method);
            _handlerList.Add(handler);
        }

        /// <summary>
        /// Remove the TActionList
        /// </summary>
        /// <param name="actionList"></param>
        public void Remove(TActionList actionList)
        {
            if (actionList == null)
                throw new ArgumentNullException(nameof(actionList));
            for (int i = 0; i < _handlerList.Count; i++)
            {
                if (_handlerList[i].ActionList == actionList)
                {
                    _handlerList.Remove(_handlerList[i]);
                    break;
                }
            }
        }

        /// <summary>
        /// Remove the handler for ShortcutKeys
        /// </summary>
        /// <param name="method"></param>
        public void Remove(ShortCutKeysDelegate method)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));
            for (int i = 0; i < _handlerList.Count; i++)
            {
                if (_handlerList[i].ShortCutKeys == method)
                {
                    _handlerList.Remove(_handlerList[i]);
                    break;
                }
            }
        }

        /// <summary>
        /// Call this Method in your Form class in the overridden ProcessCmdKey
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        public bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.Msg == WM_KEYDOWN)
            {
                bool result = false;
                for (int i = 0; i < _handlerList.Count; i++)
                {
                    result = _handlerList[i].ShortCutKeys(ref msg, keyData);
                    if (result)
                        return true;
                }
            }
            return _baseProcessCmdKey(ref msg, keyData);
        }

        class Handler
        {
            public TActionList ActionList { get; private set; }
            public ShortCutKeysDelegate ShortCutKeys { get; private set; }
            private List<TAction> _actionListWithShortCuts;

            public Handler(TActionList actionList)
            {
                ActionList = actionList;
                _actionListWithShortCuts = new List<TAction>();
                ActionCollection col = actionList.Actions;
                for (int i = 0; i < col.Count; i++)
                {
                    TAction action = col[i];
                    if (action.ShortcutKeys != Keys.None)
                    {
                        _actionListWithShortCuts.Add(action);
                    }
                }
                ShortCutKeys = ShortCutKeysMethod;
            }

            public Handler(ShortCutKeysDelegate shortCutKeys)
            {
                if (shortCutKeys == null)
                    throw new ArgumentNullException(nameof(shortCutKeys));
                ShortCutKeys = shortCutKeys;
            }

            private bool ShortCutKeysMethod(ref Message msg, Keys keys)
            {
                bool result = false;
                if (msg.Msg == WM_KEYDOWN)
                {
                    for (int i = 0; i < _actionListWithShortCuts.Count; i++)
                    {
                        TAction action = _actionListWithShortCuts[i];
                        if (action.ShortcutKeys == keys)
                        {
                            if (action.Enabled)
                            {
                                action.PerformClick();
                                result = true;
                                break;
                            }
                        }
                    }
                }
                return result;
            }
        }
    }
}
