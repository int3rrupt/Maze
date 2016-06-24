using DataStructures.Interfaces;
using System;
using System.Collections.Generic;

namespace DataStructures
{
    /// <summary>
    /// A Generic Binary Min Heap Class.
    /// </summary>
    /// <typeparam name="T">A <see cref="T"/>, the type used for keys and values.</typeparam>
    public class BinaryHeap<T> where T: IComparable
    {
        #region Declarations

        /// <summary>
        /// The <see cref="List{T}"/> used to represent the heap.
        /// </summary>
        protected List<INode<T>> heap;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryHeap{T}"/> class.
        /// </summary>
        public BinaryHeap()
        {
            heap = new List<INode<T>>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Inserts the given node into the heap.
        /// </summary>
        /// <param name="node">A <see cref="INode{T}"/>, the node to be added to the heap.</param>
        public void Insert(INode<T> node)
        {
            // Append to end of heap
            heap.Add(node);
            // Percolate new value up
            PercolateUp();
        }

        /// <summary>
        /// Extracts the <see cref="INode{T}"/> with the lowest Value.
        /// </summary>
        /// <returns>A <see cref="INode{T}"/>, the node with the lowest value.</returns>
        public INode<T> ExtractRoot()
        {
            INode<T> minValue = null;
            // Check for empty heap
            if (heap.Count > 0)
            {
                // Store min value before removing
                minValue = heap[0];
                // Replace the root with last item in heap
                Replace(0);
                // Percolate new root downward to satisfy heap property
                PercolateDown(0);
            }
            return minValue;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Percolates the last item in the heap upward until the heap property is satisfied.
        /// </summary>
        protected void PercolateUp()
        {
            // Grab last item in heap
            int currentIndex = heap.Count - 1;
            int parentIndex = IndexOfParentFor(currentIndex);
            // While current value is less than its parent value
            while (parentIndex > -1 && heap[currentIndex].Value.CompareTo(heap[parentIndex].Value) < 0)
            {
                // Swap current with its parent
                INode<T> currentItem = heap[currentIndex];
                heap[currentIndex] = heap[parentIndex];
                heap[parentIndex] = currentItem;
                // Update indexes
                currentIndex = parentIndex;
                parentIndex = IndexOfParentFor(currentIndex);
            }
        }

        /// <summary>
        /// Percolates the node at the given index in the heap downward until the heap property is satisfied.
        /// </summary>
        protected void PercolateDown(int index)
        {
            // Set current index as root
            int currentIndex = index;
            int leftChildIndex = IndexOfLeftChildFor(currentIndex);
            int rightChildIndex = IndexOfRightChildFor(currentIndex);
            // While current value is greater than any of its child values
            while ((leftChildIndex > 0 && heap[currentIndex].Value.CompareTo(heap[leftChildIndex].Value) > 0) ||
                   (rightChildIndex > 0 && heap[currentIndex].Value.CompareTo(heap[rightChildIndex].Value) > 0))
            {
                INode<T> currentItem = heap[currentIndex];
                // Swap current with its lowest valued child
                if (rightChildIndex < 1 || heap[leftChildIndex].Value.CompareTo(heap[rightChildIndex].Value) < 0)
                {
                    heap[currentIndex] = heap[leftChildIndex];
                    heap[leftChildIndex] = currentItem;
                    // Update current index
                    currentIndex = leftChildIndex;
                }
                else
                {
                    heap[currentIndex] = heap[rightChildIndex];
                    heap[rightChildIndex] = currentItem;
                    // Update current index
                    currentIndex = rightChildIndex;
                }
                // Update child indexes
                leftChildIndex = IndexOfLeftChildFor(currentIndex);
                rightChildIndex = IndexOfRightChildFor(currentIndex);
            }
        }

        /// <summary>
        /// Replaces the node at the given index of the heap with the last item in the heap.
        /// </summary>
        protected void Replace(int index)
        {
            // Get last item
            INode<T> lastItem = heap[heap.Count - 1];
            // Write last item to given index
            heap[index] = lastItem;
            // Remove last item
            heap.RemoveAt(heap.Count - 1);
        }

        /// <summary>
        /// Determines the index of the left child for the parent at the given index.
        /// </summary>
        /// <param name="parentIndex">An <see cref="int"/>, the parent's index used to find the index of its left child.</param>
        /// <returns>An <see cref="int"/>, the index of the given parent's left child. Returns -1 when no left child exists for the given parent index.</returns>
        protected int IndexOfLeftChildFor(int parentIndex)
        {
            // Find potential left child index
            int index = (2 * parentIndex) + 1;
            // Verify index not greater than size of heap
            if (index >= heap.Count)
                index = -1;
            return index;
        }

        /// <summary>
        /// Determines the index of the right child for the parent at the given index.
        /// </summary>
        /// <param name="parentIndex">An <see cref="int"/>, the parent's index used to find the index of its right child.</param>
        /// <returns>An <see cref="int"/>, the index of the given parent's right child. Returns -1 when no right child exists for the given parent index.</returns>
        protected int IndexOfRightChildFor(int parentIndex)
        {
            int index;
            // Find left child index
            int leftChildIndex = IndexOfLeftChildFor(parentIndex);
            // Verify left child exists and that right child index not greater than size of heap
            if (leftChildIndex < 1 || leftChildIndex + 1 >= heap.Count)
                index = -1;
            else
                index = leftChildIndex + 1;
            return index;
        }

        /// <summary>
        /// Determines the index of the parent for the child at the given index.
        /// </summary>
        /// <param name="childIndex">An <see cref="int"/>, the index of the child who's parent is to be found.</param>
        /// <returns>An <see cref="int"/>, the index of the parent to the child at the given index. Returns -1 when no parent exists for the given child index.</returns>
        protected int IndexOfParentFor(int childIndex)
        {
            int index;
            // Verify given child index isn't root of heap
            if (childIndex == 0)
                index = -1;
            else
                // Need to get left child, subtract 1, and divide by 2 to obtain parent.
                // childIndex % 2 determines odd/even. When odd index is left child, when even is right child.
                // % 2 equals 1 when odd and so reduces the -2 below to -1, when even -2 applies to account for further distance
                // from parent
                index  = ((childIndex - 2) + (childIndex % 2)) / 2;
            return index;
        }

        #endregion
    }
}
