using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropagationManager
{
    public static class PropagationManager
    {
        #region Private Fields

        private static DependencyGraph<object> _dependencyGraph;

        #endregion

        #region Properties

        /// <summary>
        /// Dependency graph instance.
        /// </summary>
        public static DependencyGraph<object> DependencyGraph
        {
            get { return _dependencyGraph; }
            private set { _dependencyGraph = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Notifies depenendents of the notifier node about changes.
        /// </summary>
        /// <param name="obj">Object containing changed property.</param>
        /// <param name="propertyName">The name of changed property.</param>
        public static void NotifyDependents(object obj, string propertyName)
        {
            GraphNode<object> notifierNode = DependencyGraph.GetNode(obj, propertyName);
            List<GraphNode<object>> dependentNodes = DependencyGraph.GetDependents(notifierNode);
            foreach (var dependent in dependentNodes)
            {
                dependent.UpdateMethod();
            }
        }

        #endregion
    }
}
