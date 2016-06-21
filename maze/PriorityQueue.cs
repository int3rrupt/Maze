using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    public class PriorityQueue<T>
    {
        #region Constructors

        public PriorityQueue() { }

        #endregion

        /// <summary>
        /// Enqueues the given item based on the given priority.
        /// </summary>
        /// <param name="item">A <see cref="T"/>, the item to enqueue.</param>
        /// <param name="priority">An <see cref="int"/>, the priority to assign the given value.</param>
        public void Enqueue(T item, int priority)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Dequeues the given item.
        /// </summary>
        /// <param name="value">A <see cref="T"/>, the item to dequeue.</param>
        /// <returns>A <see cref="T"/>, the item requested.</returns>
        public T Dequeue(T value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Dequeues the 
        /// </summary>
        /// <returns></returns>
        public T DequeueLowestPriority()
        {
            throw new NotImplementedException();
        }
    }
}
