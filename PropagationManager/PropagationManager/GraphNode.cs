using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropagationManager
{
    /// <summary>
    /// Node in dependency graph which encapsulates value that needs to be tracked.
    /// </summary>
    /// <typeparam name="T">Type of tracked object.</typeparam>
    public class GraphNode<T>
    {
        #region Private Fields

        private string _propertyName;
        private T _obj;
        private int _index;
        private Action _updateMethod;

        #endregion

        #region Properties

        /// <summary>
        /// Name of property which contains value that needs to be tracked.
        /// </summary>
        public string PropertyName
        {
            get { return _propertyName; }
            set { _propertyName = value; }
        }

        /// <summary>
        /// Object which contains tracked property. 
        /// </summary>
        public T Obj
        {
            get { return _obj; }
            set { _obj = value; }
        }

        /// <summary>
        /// Node index in matrix.
        /// </summary>
        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        /// <summary>
        /// Delegate to method which updates value in tracked property.
        /// </summary>
        public Action UpdateMethod
        {
            get { return _updateMethod; }
            set { _updateMethod = value; }
        }

        #endregion
    }
}
