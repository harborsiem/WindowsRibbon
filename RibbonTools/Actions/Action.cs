// --------------------------------------------------------------------------
// Description : CDiese Toolkit library
// Author	   : Serge Weinstock
//
//	You are free to use, distribute or modify this code
//	as long as this header is not removed or modified.
// --------------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;

namespace WinForms.Actions
{
    /// <summary>
    /// Action.
    /// </summary>
    [
        DesignTimeVisible(false),
        ToolboxItem(false),
        DefaultEvent("Execute"),
        DefaultProperty("Text")
    ]
    public partial class TAction : System.ComponentModel.Component
    {
        #region member variables
        private object _tag;
        private string _text;
        private int _imageIndex = -1;
        private Hashtable _components = new Hashtable();
        internal TActionList _owner = null;
        private bool _enabled = true;
        private bool _checked = false;
        private bool _visible = true;
        private Shortcut _shortcut = Shortcut.None;
        private Keys _shortcutKeys = Keys.None;
        private string _hint;
        #endregion
        #region public interface
        public TAction(System.ComponentModel.IContainer container)
        {
            /// <summary>
            /// Required for Windows.Forms Class Composition Designer support
            /// </summary>
            container.Add(this);
            InitializeComponent();
        }

        public TAction()
        {
            /// <summary>
            /// Required for Windows.Forms Class Composition Designer support
            /// </summary>
            InitializeComponent();
        }
        /// <summary>
        /// The text used in controls associated to this Action.
        /// </summary>
        [
        Category("Misc"),
        Localizable(true),
        Description("The text used in controls associated to this Action.")
        ]
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                IDictionaryEnumerator i = _components.GetEnumerator();
                while (i.MoveNext())
                {
                    ((ActionData)i.Value).Text = _text;
                }
            }
        }
        /// <summary>
        /// Indicates whether the associated components are enabled.
        /// </summary>
        [
        Category("Behavior"),
        Description("Indicates whether the associated components are enabled.")
        ]
        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
                IDictionaryEnumerator i = _components.GetEnumerator();
                while (i.MoveNext())
                {
                    ((ActionData)i.Value).Enabled = _enabled;
                }
            }
        }
        /// <summary>
        /// Indicates whether the associated components are checked.
        /// </summary>
        [
        Category("Behavior"),
        Description("Indicates whether the associated components are checked.")
        ]
        public bool Checked
        {
            get
            {
                return _checked;
            }
            set
            {
                _checked = value;
                IDictionaryEnumerator i = _components.GetEnumerator();
                while (i.MoveNext())
                {
                    ((ActionData)i.Value).Checked = _checked;
                }
            }
        }
        /// <summary>
        /// Indicates the shortcut for this Action.
        /// used for MenuItem and 
        /// </summary>
        [
        Category("Misc"),
        Description("Indicates the shortcut for this Action.")
        ]
        public Shortcut Shortcut
        {
            get
            {
                return _shortcut;
            }
            set
            {
                _shortcut = value;
                IDictionaryEnumerator i = _components.GetEnumerator();
                while (i.MoveNext())
                {
                    ((ActionData)i.Value).Shortcut = _shortcut;
                }
            }
        }
        /// <summary>
        /// Indicates the shortcutKeys for this Action.
        /// used for ToolStripMenuItem
        /// </summary>
        [
        Category("Misc"),
        Description("Indicates the shortcutKeys for this Action.")
        ]
        public Keys ShortcutKeys
        {
            get
            {
                return _shortcutKeys;
            }
            set
            {
                _shortcutKeys = value;
                IDictionaryEnumerator i = _components.GetEnumerator();
                while (i.MoveNext())
                {
                    ((ActionData)i.Value).ShortcutKeys = _shortcutKeys;
                }
            }
        }
        /// <summary>
        /// Indicates whether the associated components are visibled or hidden.
        /// </summary>
        [
        Category("Behavior"),
        Description("Indicates whether the associated components are visibled or hidden.")
        ]
        public bool Visible
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;
                IDictionaryEnumerator i = _components.GetEnumerator();
                while (i.MoveNext())
                {
                    ((ActionData)i.Value).Visible = _visible;
                }
            }
        }
        /// <summary>
        /// User defined data associated with this Action.
        /// </summary>
        [
        Category("Data"),
        Description("User defined data associated with this Action.")
        ]
        public object Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
            }
        }
        /// <summary>
        /// Indicates the index of the image in the parent ActionList's ImageList this Action will use to obtains its image.
        /// </summary>
        [
        Category("Misc"),
        Localizable(true),
        Description("Indicates the index of the image in the parent ActionList's ImageList this Action will use to obtains its image."),
        TypeConverter(typeof(System.Windows.Forms.ImageIndexConverter)),
        Editor(typeof(Design.ImageIndexEditor), typeof(UITypeEditor)),
        DefaultValue(-1)
        ]
        public int ImageIndex
        {
            get
            {
                return _imageIndex;
            }
            set
            {
                _imageIndex = value;
                IDictionaryEnumerator i = _components.GetEnumerator();
                while (i.MoveNext())
                {
                    ((ActionData)i.Value).ImageIndex = _imageIndex;
                }
            }
        }
        /// <summary>
        /// Indicates the text that appears as a ToolTip for a control.
        /// </summary>
        [
        Category("Misc"),
        Localizable(true),
        Description("Indicates the text that appears as a ToolTip for a control."),
        ]
        public string Hint
        {
            get
            {
                return _hint;
            }
            set
            {
                _hint = value;
                IDictionaryEnumerator i = _components.GetEnumerator();
                while (i.MoveNext())
                {
                    ((ActionData)i.Value).Hint = _hint;
                }
            }
        }
        /// <summary>
        /// The ActionList to which this action belongs
        /// </summary>
        [Browsable(false)]
        public TActionList Parent
        {
            get
            {
                return _owner;
            }
        }
        /// <summary>
        /// This event is triggered when the path changes
        /// </summary>
        [Description("Triggered when the action is executed")]
        public event EventHandler Execute;
        /// <summary>
        /// This event is triggered when the path changes
        /// </summary>
        [Description("Triggered when the application is idle or when the action list updates.")]
        public event EventHandler Update;
        #endregion
        #region implementation
        [Browsable(false)]
        internal ImageList ImageList
        {
            set
            {
                IDictionaryEnumerator i = _components.GetEnumerator();
                while (i.MoveNext())
                {
                    ((ActionData)i.Value).ImageList = value;
                }
            }
        }
        public void DoExecute()
        {
            OnExecute(this, EventArgs.Empty);
        }
        internal void OnExecute(Object sender, EventArgs e)
        {
            if (Execute != null)
            {
                Execute(this, e);
            }
        }
        internal void OnUpdate(Object sender, EventArgs e)
        {
            if (Update != null)
            {
                Update(this, e);
            }
        }
        internal void SetComponent(Component comp, bool add)
        {
            ActionData ad = (ActionData)_components[comp];

            if (add)
            {
                if (ad == null)
                {
                    ad = new ActionData();
                    ad.Attach(this, comp, DesignMode);
                    _components[comp] = ad;
                }
            }
            else if (ad != null)
            {
                ad.Detach();
                _components.Remove(comp);
            }
        }
        internal bool HandleComponent(Component comp)
        {
            return (_components[comp] != null);
        }
        /// <summary>
        /// The ActionList to which this action belongs
        /// </summary>
        [Browsable(false)]
        internal bool ShowTextOnToolBar
        {
            set
            {
                string dtext = (value ? Text : null);

                IDictionaryEnumerator i = _components.GetEnumerator();
                while (i.MoveNext())
                {
                    ((ActionData)i.Value).ShowTextOnToolBar = dtext;
                }
            }
        }
        #endregion

        internal void FinishInit()
        {
#if !Core
            IDictionaryEnumerator i = _components.GetEnumerator();
            while (i.MoveNext())
            {
                ((ActionData)i.Value).FinishInit();
            }
#endif
        }
    }

    /// <summary>
    /// Internal data about a control used by an Action
    /// </summary>
    internal class ActionData : IDisposable
    {
#region member variables
        private PropertyInfo _text;
        private PropertyInfo _enabled;
        private PropertyInfo _checked;
        private PropertyInfo _visible;
        private PropertyInfo _shortcut;
        private PropertyInfo _shortcutKeys;
        private PropertyInfo _imageIndex;
        private PropertyInfo _imageList;
        private bool _click = false;
        private Component _component;
        private TAction _owner;
#endregion
#region "public" interface
        internal void Attach(TAction a, Component o, bool designMode)
        {
            Debug.Assert(o != null && a != null);
            _component = o;
            _owner = a;
            Debug.Assert(_owner.Parent != null);
            // Text
            _text = o.GetType().GetProperty("Text");
            if (_text != null && (!_text.CanRead || !_text.CanWrite) && (_text.PropertyType == typeof(string)))
            {
                // we must be able to read and write a boolean property
                _text = null;
            }
            Text = _owner.Text;
            // Enabled
            _enabled = o.GetType().GetProperty("Enabled");
            if (_enabled != null && (!_enabled.CanRead || !_enabled.CanWrite) && (_enabled.PropertyType == typeof(bool)))
            {
                // we must be able to read and write a boolean property
                _enabled = null;
            }
            Enabled = _owner.Enabled;
            // Checked
#if !Core
            // special case of a toolbarButton
            if (_component is ToolBarButton)
            {
                _checked = o.GetType().GetProperty("Pushed");
                Debug.Assert(_checked != null && _checked.CanRead && _checked.CanWrite && (_checked.PropertyType == typeof(bool)));
            }
            else
#endif
            {
                _checked = o.GetType().GetProperty("Checked");
                if (_checked != null && (!_checked.CanRead || !_checked.CanWrite) && (_checked.PropertyType == typeof(bool)))
                {
                    // we must be able to read and write a boolean property
                    _checked = null;
                }
            }
            Checked = _owner.Checked;
            // Visible
            _visible = o.GetType().GetProperty("Visible");
            if (_visible != null && (!_visible.CanRead || !_visible.CanWrite) && (_visible.PropertyType == typeof(bool)))
            {
                // we must be able to read and write a boolean property
                _visible = null;
            }
            Visible = _owner.Visible;
            // Shortcut
            _shortcut = o.GetType().GetProperty("Shortcut");
            if (_shortcut != null && (!_shortcut.CanRead || !_shortcut.CanWrite) && (_shortcut.PropertyType == typeof(Shortcut)))
            {
                // we must be able to read and write a shortcut property
                _shortcut = null;
            }
            Shortcut = _owner.Shortcut;
            // ShortcutKeys
            _shortcutKeys = o.GetType().GetProperty("ShortcutKeys");
            if (_shortcutKeys != null && (!_shortcutKeys.CanRead || !_shortcutKeys.CanWrite) && (_shortcutKeys.PropertyType == typeof(Keys)))
            {
                // we must be able to read and write a shortcutKey property
                _shortcutKeys = null;
            }
            ShortcutKeys = _owner.ShortcutKeys;
            // ImageList
            // don't handle toolbarButtons here
#if !Core
            if (!(_component is ToolBarButton))
#endif
            {
                _imageList = o.GetType().GetProperty("ImageList");
                if (_imageList != null && (!_imageList.CanRead || !_imageList.CanWrite) && (_imageList.PropertyType == typeof(ImageList)))
                {
                    // we must be able to read and write an ImageList property
                    _imageList = null;
                }
            }
            ImageList = _owner.Parent.ImageList;
            // ImageIndex
            _imageIndex = o.GetType().GetProperty("ImageIndex");
            if (_imageIndex != null && (!_imageIndex.CanRead || !_imageIndex.CanWrite) && (_imageIndex.PropertyType == typeof(int)))
            {
                // we must be able to read and write an integer property
                _imageIndex = null;
            }
            ImageIndex = _owner.ImageIndex;
            // Hint
            Hint = _owner.Hint;
            // click
            if (!designMode)
            {
#if !Core
                // special case of a toolbarButton
                if (_component is ToolBarButton)
                {
                    ToolBar tb = ((ToolBarButton)_component).Parent;
                    if (tb != null)
                    {
                        tb.ButtonClick += new ToolBarButtonClickEventHandler(OnToolbarClick);
                        _click = true;
                    }
                }
                else
#endif
                {
                    EventInfo e = o.GetType().GetEvent("Click");
                    if (e != null && e.EventHandlerType == typeof(EventHandler))
                    {
                        e.AddEventHandler(_component, new EventHandler(_owner.OnExecute));
                        _click = true;
                    }
                }
            }
            // Dispose
            Debug.Assert(_owner.Parent != null);
            _component.Disposed += new EventHandler(_owner.Parent.OnComponentDisposed);
        }
        internal void Detach()
        {
            _text = null;
            _enabled = null;
            _checked = null;
            _shortcut = null;
            if (_component != null && _click)
            {
#if !Core
                if (_component is ToolBarButton)
                {
                    ToolBar tb = ((ToolBarButton)_component).Parent;
                    if (tb != null)
                    {
                        tb.ButtonClick -= new ToolBarButtonClickEventHandler(OnToolbarClick);
                    }
                }
                else
#endif
                {
                    EventInfo e = _component.GetType().GetEvent("Click");
                    e.RemoveEventHandler(_component, new EventHandler(_owner.OnExecute));
                }
            }
            Debug.Assert(_owner.Parent != null);
            _component.Disposed -= new EventHandler(_owner.Parent.OnComponentDisposed);
        }
        internal string Text
        {
            set
            {
                if (_text != null)
                {
#if !Core
                    if ((_component is ToolBarButton) && !_owner.Parent.ShowTextOnToolBar)
                    {
                        _text.SetValue(_component, null, null);
                    }
                    else
#endif
                    if ((string)_text.GetValue(_component, null) != value)
                    {
                        _text.SetValue(_component, value, null);
                    }
                }
            }
        }
        internal bool Enabled
        {
            set
            {
                if (_enabled != null && ((bool)_enabled.GetValue(_component, null) != value))
                {
                    _enabled.SetValue(_component, value, null);
                }
            }
        }
        internal bool Checked
        {
            set
            {
                if (_checked != null && ((bool)_checked.GetValue(_component, null) != value))
                {
                    _checked.SetValue(_component, value, null);
                }
            }
        }
        internal bool Visible
        {
            set
            {
                if (_visible != null && (value == false || ((bool)_visible.GetValue(_component, null) != true))) // just set the value if Visible == false
                {
                    _visible.SetValue(_component, value, null);
                }
            }
        }
        internal Shortcut Shortcut
        {
            set
            {
                if (_shortcut != null && ((Shortcut)_shortcut.GetValue(_component, null) != value))
                {
                    _shortcut.SetValue(_component, value, null);
                }
            }
        }
        internal Keys ShortcutKeys
        {
            set
            {
                if (_shortcutKeys != null && ((Keys)_shortcutKeys.GetValue(_component, null) != value))
                {
                    _shortcutKeys.SetValue(_component, value, null);
                }
            }
        }
        internal ImageList ImageList
        {
            set
            {
#if !Core
                if (_component is ToolBarButton)
                {
                    ToolBarButton tb = (ToolBarButton)_component;

                    if (tb.Parent != null && tb.Parent.ImageList != value)
                    {
                        tb.Parent.ImageList = value;
                    }
                    return;
                }
#endif
                if (_imageList != null && ((ImageList)_imageList.GetValue(_component, null) != value))
                {
                    _imageList.SetValue(_component, value, null);
                }
            }
        }
        internal int ImageIndex
        {
            set
            {
                if (_imageIndex != null && ((int)_imageIndex.GetValue(_component, null) != value))
                {
                    _imageIndex.SetValue(_component, value, null);
                }
            }
        }
#if !Core
        private void OnToolbarClick(Object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button == _component)
            {
                _owner.OnExecute(sender, e);
            }
        }
#endif
        public void Dispose()
        {
            Detach();
        }
#if !Core
        internal void FinishInit()
        {
            if (_component is ToolBarButton && !_click)
            {
                ToolBar tb = ((ToolBarButton)_component).Parent;
                if (tb != null)
                {
                    tb.ButtonClick += new ToolBarButtonClickEventHandler(OnToolbarClick);
                    _click = true;
                }
            }
        }
#endif
        internal string ShowTextOnToolBar
        {
            set
            {
                if (_component is ToolStripItem)
                {
                    Text = value;
                }
#if !Core
                else if (_component is ToolBarButton)
                {
                    Text = value;
                }
#endif
            }
        }
        internal string Hint
        {
            set
            {
                if (_component is ToolStripItem)
                {
                    ToolStripItem item = (ToolStripItem)_component;
                    if (item.ToolTipText != value)
                    {
                        item.ToolTipText = value;
                    }
                }
#if !Core
                else if (_component is ToolBarButton)
                {
                    ToolBarButton button = (ToolBarButton)_component;
                    if (button.ToolTipText != value)
                    {
                        button.ToolTipText = value;
                    }
                }
#endif
                else if (_component is Control)
                {
                    Debug.Assert(_owner != null && _owner.Parent != null && _owner._owner._toolTip != null);
                    Control c = (Control)_component;
                    ToolTip t = _owner._owner._toolTip;
                    if (t.GetToolTip(c) != value)
                    {
                        t.SetToolTip(c, value);
                    }
                }
            }
        }
#endregion
    }
}
