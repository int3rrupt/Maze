using DataStructures;
using System;

namespace Maze
{
    public class PriorityQueue<T> : BinaryHeap<T> where T: IComparable
    {
        public PriorityQueue()
            : base()
        {
        }

        #region Public Methods

        #region Standard Operations

        /// <summary>
        /// Enqueues the given node based on the given priority.
        /// </summary>
        /// <param name="node">An <see cref="AStarNode{T}"/>, the item to enqueue.</param>
        public void Enqueue(AStarNode<T> node)
        {
            Insert(node);
        }

        /// <summary>
        /// Dequeues the node with the highest priority (lowest value).
        /// </summary>
        /// <returns>An <see cref="AStarNode{T}"/>, the node with the highest priority.</returns>
        public AStarNode<T> DequeueHighestPriority()
        {
            return (AStarNode<T>)ExtractRoot();
        }

        #endregion

        #region Additional Operations

        /// <summary>
        /// Dequeues the node with the given key.
        /// </summary>
        /// <param name="key">A <see cref="T"/>, the item to dequeue.</param>
        /// <returns>A <see cref="T"/>, the item requested.</returns>
        public AStarNode<T> Dequeue(T key)
        {
            int index = heap.FindIndex(n => n.Key.CompareTo(key) == 0);
            AStarNode<T> node = (AStarNode<T>)heap[index];
            Replace(index);
            PercolateDown(index);
            // Determine whether value exists
            //AStarNode<T> node = Exists(key);
            // Remove from heap
            // heap.Remove(node);
            return node;
        }

        #endregion

        #endregion

        #region Private Methods

        private AStarNode<T> Exists(T key)
        {
            return (AStarNode<T>)heap.Find(n => n.Key.CompareTo(key) == 0);
        }
        
        #endregion
    }
}
