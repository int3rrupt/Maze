using DataStructures.Interfaces;
using System;

namespace Maze
{
    public class AStarNode<T> : INode<T> where T : IComparable
    {
        #region Declarations

        private T key;
        private T value;
        private INode<T> parent;
        
        #endregion

        #region Constructor

        public AStarNode(T key, T value)
        {
            this.key = key;
            this.value = value;
        }

        #endregion

        #region Public Properties

        T INode<T>.Key { get { return this.key; } set { this.key = value; } }

        T INode<T>.Value { get { return this.value; } set { this.value = value; } }

        INode<T> INode<T>.Parent { get { return this.parent; } set { this.parent = value; } }

        T Heuristic { get; set; }

        #endregion
    }
}
