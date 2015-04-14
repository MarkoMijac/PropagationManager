using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropagationManager
{
    public class DependencyGraph<T>
    {
        #region Private Fields

        private List<GraphNode<T>> _nodes;
        private bool[,] _dependencies;

        #endregion

        #region Properties

        /// <summary>
        /// List containing all the nodes from dependency graph (graph Vertices).
        /// </summary>
        public List<GraphNode<T>> Nodes
        {
            get { return _nodes; }
            private set { _nodes = value; }
        }

        /// <summary>
        /// Adjacency matrix showing dependencies between nodes in dependency graph (Graph edges).
        /// </summary>
        public bool[,] Dependencies
        {
            get { return _dependencies; }
            private set { _dependencies = value; }
        }
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor method.
        /// </summary>
        public DependencyGraph()
        {
            Nodes = new List<GraphNode<T>>();
            Dependencies = new bool[100, 100];      //TEMP
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets up a dependency between two nodes in dependency graph.
        /// </summary>
        /// <param name="notifierIndex">Index of the Notifier node that notifies about changes.</param>
        /// <param name="dependentIndex">Index of the Dependent node that depends on changes of the Notifier node.</param>
        public void AddDependency(int notifierIndex, int dependentIndex)
        {
            Dependencies.SetValue(true, notifierIndex, dependentIndex);
        }

        /// <summary>
        /// Sets up a dependency between two nodes (Notifier and Dependent) in dependency graph.
        /// </summary>
        /// <param name="notifierNode">Notifier node that notifies about changes.</param>
        /// <param name="dependentNode">Dependent node that depends on changes of the Notifier node.</param>
        public void AddDependency(GraphNode<T> notifierNode, GraphNode<T> dependentNode)
        {
            AddDependency(notifierNode.Index, dependentNode.Index);
        }

        /// <summary>
        /// Removes a dependency between two nodes (Notifier and Dependent) in dependency graph.
        /// </summary>
        /// <param name="index1">Index of the Notifier node that notifies about changes.</param>
        /// <param name="index2">Index of the Dependent node that depends on changes of the Notifier node.</param>
        public void RemoveDependency(int index1, int index2)
        {
            Dependencies.SetValue(false, index1, index2);
        }

        /// <summary>
        /// Removes a dependency between two nodes (Notifier and Dependent) in dependency graph.
        /// </summary>
        /// <param name="index1">Index of the Notifier node that notifies about changes.</param>
        /// <param name="index2">Index of the Dependent node that depends on changes of the Notifier node.</param>
        public void RemoveDependency(GraphNode<T> notifierNode, GraphNode<T> dependentNode)
        {
            RemoveDependency(notifierNode.Index, dependentNode.Index);
        }

        /// <summary>
        /// Adds new node in dependency graph.
        /// </summary>
        /// <param name="obj">A reference to object containing tracked property.</param>
        /// <param name="propertyName">The name of the tracked property.</param>
        /// <param name="updateMethod">Delegate to method for updating tracked property.</param>
        /// <returns>New node in dependency graph.</returns>
        public GraphNode<T> AddNode(T obj, string propertyName, Action updateMethod)
        {
            GraphNode<T> node = new GraphNode<T>
            {
                Obj = obj,
                PropertyName = propertyName,
                UpdateMethod = updateMethod,
                Index = Nodes.Count
            };
            Nodes.Add(node);

            return node;
        }

        /// <summary>
        /// Adds new node in dependency graph.
        /// </summary>
        /// <param name="obj">A reference to object containing tracked property.</param>
        /// <param name="propertyName">The name of the tracked property.</param>
        /// <returns>New node in dependency graph.</returns>
        public GraphNode<T> AddNode(T obj, string propertyName)
        {
            return AddNode(obj, propertyName, null);
        }

        /// <summary>
        /// Gets existing node from dependency graph.
        /// </summary>
        /// <param name="obj">Object contained in graph node.</param>
        /// <param name="propertyName">The name of the tracked property.</param>
        /// <returns>Existing node from dependency graph.</returns>
        public GraphNode<T> GetNode(T obj, string propertyName)
        {
            GraphNode<T> vertex = null;

            vertex = Nodes.Find(v => v.Obj.GetHashCode() == obj.GetHashCode() && v.PropertyName == propertyName);

            return vertex;
        }

        /// <summary>
        /// Gets the list of all dependent nodes of specified Notifier node.
        /// </summary>
        /// <param name="notifierNode">Notifier node whose dependent nodes we want.</param>
        /// <returns>The list of all dependent nodes of Notifier node.</returns>
        public List<GraphNode<T>> GetDependents(GraphNode<T> notifierNode)
        {
            List<GraphNode<T>> dependents = new List<GraphNode<T>>();

            for (int i = 0; i < Dependencies.GetLength(1); i++)
            {
                if (Dependencies[notifierNode.Index, i] == true)
                {
                    dependents.Add(Nodes[i]);
                }
            }

            return dependents;
        }

        /// <summary>
        /// Gets the list of all Notifier nodes that notify specified Dependent node.
        /// </summary>
        /// <param name="dependentNode">Dependent node whose notifier nodes we want.</param>
        /// <returns>List of all Notifier nodes that notify specified Dependent node.</returns>
        public List<GraphNode<T>> GetNotifiers(GraphNode<T> dependentNode)
        {
            List<GraphNode<T>> notifiers = new List<GraphNode<T>>();

            for (int i = 0; i < Dependencies.GetLength(0); i++)
            {
                if (Dependencies[i, dependentNode.Index] == true)
                {
                    notifiers.Add(Nodes[i]);
                }
            }

            return notifiers;
        }

        #endregion
    }
}
