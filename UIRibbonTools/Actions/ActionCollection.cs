// --------------------------------------------------------------------------
// Description : CDiese Toolkit library
// Author	   : Serge Weinstock
//
//	You are free to use, distribute or modify this code
//	as long as this header is not removed or modified.
// --------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Reflection;

namespace WinForms.Actions
{
    /// <summary>
    /// A collection that stores Action Actions.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    [Editor(typeof(ActionCollectionEditor), typeof(UITypeEditor))]
    public class ActionCollection : CollectionBase
    {
        private TActionList _owner;
        private TAction _null = new TAction();

        /// <summary>
        ///  Initializes a new instance of ActionCollection.
        /// </summary>
        public ActionCollection(TActionList owner)
        {
            Debug.Assert(owner != null);
            _owner = owner;
            _null._owner = _owner;
        }
        /// <summary>
        /// Initialises a new instance of ActionCollection based on another ActionCollection.
        /// </summary>
        /// <param name='value'>An ActionCollection from which the contents are copied</param>
        public ActionCollection(ActionCollection value)
        {
            this.AddRange(value);
        }

        /// <summary>
        /// Initialises a new instance of ActionCollection containing any array of Actions.
        /// </summary>
        /// <param name='value'>An array of Actions with which to intialize the collection</param>
        public ActionCollection(TAction[] value)
        {
            this.AddRange(value);
        }

        /// <summary>
        /// Returns the ActionList which owns this ActionCollection
        /// </summary>
        public TActionList Parent
        {
            get
            {
                return _owner;
            }
        }

        /// <summary>
        /// Returns a reference to the "null" action of this collection (needed in design mode)
        /// </summary>
        internal TAction Null
        {
            get
            {
                return _null;
            }
        }

        /// <summary>
        /// Represents the entry at the specified index.
        /// </summary>
        /// <param name='index'>The zero-based index of the entry to locate in the collection.</param>
        /// <returns>The entry at the specified index of the collection.</returns>
        public TAction this[int index]
        {
            get
            {
                return ((TAction)(List[index]));
            }
            set
            {
                List[index] = value;
            }
        }

        /// <summary>
        /// Adds a Action with the specified value to the ActionCollection.
        /// </summary>
        /// <param name='value'>The Action to add.</param>
        /// <returns>The index at which the new element was inserted.</returns>
        public int Add(TAction value)
        {
            return List.Add(value);
        }

        /// <summary>
        /// Copies the elements of an array to the end of the ActionCollection.
        /// </summary>
        /// <param name='value'> An array of Actions containing the objects to add to the collection.</param>
        public void AddRange(TAction[] value)
        {
            foreach (TAction a in value)
            {
                this.Add(a);
            }
        }

        /// <summary>
        /// Adds the contents of another ActionCollection to the end of the collection.
        /// </summary>
        /// <param name='value'>An ActionCollection containing the objects to add to the collection.</param>
        public void AddRange(ActionCollection value)
        {
            foreach (TAction a in value)
            {
                this.Add(a);
            }
        }

        /// <summary>
        /// Returns true if this ActionCollection contains the specified Action.
        /// </summary>
        /// <param name='value'>The Action to locate.</param>
        public bool Contains(TAction value)
        {
            return List.Contains(value);
        }

        /// <summary>
        /// Copies the ActionCollection values to a one-dimensional Array instance at the  specified index.
        /// </summary>
        /// <param name='array'>The one-dimensional Array that is the destination of the values copied from ActionCollection .</param>
        /// <param name='index'>The index in <paramref name='array'/> where copying begins.</param>
        public void CopyTo(TAction[] array, int index)
        {
            List.CopyTo(array, index);
        }

        /// <summary>Returns the index of an Action in the ActionCollection.</summary>
        /// <param name='value'>The Action to locate.</param>
        /// <returns>The index of the Action of <paramref name='value'/> in the ActionCollection, if found; otherwise, -1.</returns>
        public int IndexOf(TAction value)
        {
            return List.IndexOf(value);
        }

        /// <summary>Inserts a Action into the ActionCollection at the specified index.</summary>
        /// <param name='index'>The zero-based index where <paramref name='value'/> should be inserted.</param>
        /// <param name=' value'>The Action to insert.</param>
        public void Insert(int index, TAction value)
        {
            List.Insert(index, value);
        }

        /// <summary>Returns an enumerator that can iterate through  the ActionCollection.</summary>
        public new ActionEnumerator GetEnumerator()
        {
            return new ActionEnumerator(this);
        }

        /// <summary>
        /// Removes a specific Action from the ActionCollection.
        /// </summary>
        /// <param name='value'>The Action to remove from the ActionCollection .</param>
        public void Remove(TAction value)
        {
            List.Remove(value);
        }

        protected override void OnSet(int index, object oldValue, object newValue)
        {
            if (oldValue != null) ((TAction)oldValue)._owner = null;
            if (newValue != null) ((TAction)newValue)._owner = _owner;
        }

        protected override void OnInsert(int index, object value)
        {
            if (value != null) ((TAction)value)._owner = _owner;
        }

        protected override void OnClear()
        {
        }

        protected override void OnRemove(int index, object value)
        {
        }

        protected override void OnValidate(object value)
        {
        }

        /// <summary>
        /// An enumerator for an ActionCollection
        /// </summary>
        public class ActionEnumerator : object, IEnumerator
        {

            private IEnumerator _baseEnumerator;
            private IEnumerable _temp;

            public ActionEnumerator(ActionCollection mappings)
            {
                this._temp = ((IEnumerable)(mappings));
                this._baseEnumerator = _temp.GetEnumerator();
            }

            public TAction Current
            {
                get
                {
                    return ((TAction)(_baseEnumerator.Current));
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return _baseEnumerator.Current;
                }
            }

            public bool MoveNext()
            {
                return _baseEnumerator.MoveNext();
            }

            bool IEnumerator.MoveNext()
            {
                return _baseEnumerator.MoveNext();
            }

            public void Reset()
            {
                _baseEnumerator.Reset();
            }

            void IEnumerator.Reset()
            {
                _baseEnumerator.Reset();
            }
        }
    }
}
