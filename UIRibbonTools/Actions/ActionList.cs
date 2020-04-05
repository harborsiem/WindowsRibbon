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
using System.Drawing;
using System.Windows.Forms;

namespace WinForms.Actions
{
    /// <summary>
    /// An ActionList is a list of Actions.
    /// </summary>
    [
        ToolboxBitmap(typeof(TActionList)),
        DefaultProperty("Actions"),
        ProvideProperty("Action", typeof(Component))
    ]
    public partial class TActionList : System.ComponentModel.Component, IExtenderProvider
    {
        #region member variables
        private ActionCollection _actions;
        private ImageList _imageList;
        private object _tag;
        private bool _showTextOnToolBar = false;
        private bool _init = false;
        // As there is no way to ensure that the designer calls the SetAction method after the call to ActionList.AddRange method, 
        // we need to duplicate the list of assocations between actions and component already contained in the ActionCollection member. 
        // It's not really a problem, because it speeds up all operations based on iteration on the components.
        private Hashtable _components = new Hashtable();
        #endregion
        #region public interface
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="container"></param>
        public TActionList(System.ComponentModel.IContainer container)
        {
            /// <summary>
            /// Required for Windows.Forms Class Composition Designer support
            /// </summary>
            container.Add(this);
            InitializeComponent();
            //
            Init();
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public TActionList()
        {
            /// <summary>
            /// Required for Windows.Forms Class Composition Designer support
            /// </summary>
            InitializeComponent();
            //
            Init();
        }
        /// <summary>
        /// The number of Actions in this ActionList
        /// </summary>
        [Browsable(false)]
        public int Count
        {
            get
            {
                return _actions.Count;
            }
        }
        /// <summary>
        /// The collection of Actions that makes up this ActionList
        /// </summary>
        [
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            Category("Behavior"),
            Description("The collection of Actions that makes up this ActionList")
        ]
        public ActionCollection Actions
        {
            get
            {
                return _actions;
            }
            set
            {
                _actions = value;
            }
        }
        /// <summary>
        /// The ImageList from which this ActionList will get all of the action images.
        /// </summary>
        [
        Category("Behavior"),
        Description("The ImageList from which this ActionList will get all of the action images.")
        ]
        public ImageList ImageList
        {
            get
            {
                return _imageList;
            }
            set
            {
                _imageList = value;
                foreach (TAction a in Actions)
                {
                    a.ImageList = _imageList;
                }
            }
        }
        /// <summary>
        /// User defined data associated with this ActionList.
        /// </summary>
        [
        Category("Data"),
        Description("User defined data associated with this ActionList.")
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
        /// User defined data associated with this ActionList.
        /// </summary>
        [
        Category("Behavior"),
        Description("User defined data associated with this ActionList.")
        ]
        public bool ShowTextOnToolBar
        {
            get
            {
                return _showTextOnToolBar;
            }
            set
            {
                _showTextOnToolBar = value;
                foreach (TAction a in Actions)
                {
                    a.ShowTextOnToolBar = value;
                }
            }
        }
        /// <summary>
        /// Indicates whether or not labels of ToolBar Buttons are displayed or not.
        /// </summary>
        [
        ExtenderProvidedProperty(),
        TypeConverter(typeof(ActionConverter)),
        Description("Indicates whether or not labels of ToolBar Buttons are displayed or not."),
        Category("Behavior")
        ]
        public TAction GetAction(Component comp)
        {
            Debug.Assert(comp != null);
            TAction res = (TAction)_components[comp];
            if (res == null)
            {
                return Actions.Null;
            }
            return res;
        }
        /// <summary>
        /// The set method of the extended property Action
        /// </summary>
        [ExtenderProvidedProperty()]
        public void SetAction(Component comp, TAction value)
        {
            Debug.Assert(comp != null && value != null);
            TAction res = (TAction)_components[comp];

            if (res != null)
            {
                if (value == res)
                {
                    return;
                }
                res.SetComponent(comp, false);
                _components.Remove(comp);
            }

            if (value != Actions.Null)
            {
                value._owner = this;
                value.SetComponent(comp, true);
                _components.Add(comp, value);
            }
        }
        /// <summary>
        /// We need only to serialize Components wich have associated to an Action
        /// </summary>
        public bool ShouldSerializeAction(object o)
        {
            Debug.Assert(o != null);
            foreach (TAction a in Actions)
            {
                if (a.HandleComponent((Component)o))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Specifies whether this object can provide its extender properties to the specified object.
        /// </summary>
        /// <param name="target">The Object to receive the extender properties</param>
        public bool CanExtend(Object target)
        {
            return (target is Component) && !(target is TActionList) && !(target is TAction);
        }
        #endregion
        #region  internal
        /// <summary>
        /// Common code for initialising the instance
        /// </summary>
        private void Init()
        {
            _actions = new ActionCollection(this);
            if (!DesignMode)
            {
                Application.Idle += new EventHandler(OnIdle);
            }
        }
        /// <summary>
        /// Occurs when the application is idle so that the action list can update the actions in the list.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">An EventArgs that contains the event data</param>
        private void OnIdle(Object sender, EventArgs e)
        {
            // the designer don't put necesseraly the initialisation code in the right order
            // Especially, toolbarbuttons can be assigned to their corresponding toolbar after their corresponding action has been assigned
            // So in this case, we are not able to add an event handler to toolbarbutton.Parent.click

            // so if it was not done, we do the job here
            if (!_init)
            {
                foreach (TAction a in Actions)
                {
                    a.FinishInit();
                }
                _init = true;
            }

            // the real work begins here
            foreach (TAction a in Actions)
            {
                a.OnUpdate(this, e);
            }
        }
        /// <summary>
        /// Cleans the action list when an associated component is destroyed
        /// </summary>
        internal void OnComponentDisposed(Object sender, EventArgs e)
        {
            Component comp = (Component)sender;
            Debug.Assert(comp != null);
            TAction a = (TAction)_components[comp];

            if (a != null)
            {
                a.SetComponent(comp, false);
                _components.Remove(comp);
            }
        }
        #endregion
    }
}
