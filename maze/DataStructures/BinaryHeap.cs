using Common.DataStructures.Interfaces;
using System.Collections.Generic;

namespace Common.DataStructures
{
    /// <summary>
    /// A Generic Binary Min Heap Class.
    /// </summary>
    /// <typeparam name="T">A <see cref="T"/>, the type used for keys and values.</typeparam>
    public abstract class BinaryHeap
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryHeap{T}"/> class.
        /// </summary>
        public BinaryHeap()
        {
            PriorityList = new List<int>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Inserts the given node into the heap.
        /// </summary>
        /// <param name="node">A <see cref="INode"/>, the node to be added to the heap.</param>
        public int Insert(int priority)
        {
            // Append to end of heap
            PriorityList.Add(priority);
            // Percolate new value up
            return PercolateUp();
        }

        /// <summary>
        /// Extracts the <see cref="INode"/> with the lowest Value.
        /// </summary>
        /// <returns>A <see cref="INode"/>, the node with the lowest value.</returns>
        public int ExtractRoot()
        {
            int minValue = -1;
            // Check for empty heap
            if (PriorityList.Count > 0)
            {
                // Store min value before removing
                minValue = PriorityList[0];
                // Replace the root with last item in heap
                Replace(0);
                // Percolate new root downward to satisfy heap property
                PercolateDown(0);
            }
            return minValue;
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Percolates the last item in the heap upward until the heap property is satisfied.
        /// NOTE: make single percolate up so that can override in priority queue and determine index changes
        /// </summary>
        protected int PercolateUp()
        {
            // Grab last item in heap
            int currentIndex = PriorityList.Count - 1;
            int parentIndex = IndexOfParentFor(currentIndex);
            // While current value is less than its parent value
            // OR
            // current value == parent value AND current H greater than parent H
            while (parentIndex > -1 &&
                  (PriorityList[currentIndex] < PriorityList[parentIndex])) //||
                  //(NodeList[currentIndex].Value == NodeList[parentIndex].Value &&
                   //NodeList[currentIndex].H < NodeList[parentIndex].H)))
            {
                Swap(currentIndex, parentIndex);
                // Update indexes
                currentIndex = parentIndex;
                parentIndex = IndexOfParentFor(currentIndex);
            }
            return currentIndex;
        }

        protected virtual void Swap(int index1, int index2)
        {
            // Swap priority values of two items
            int currentItemPriority = PriorityList[index1];
            PriorityList[index1] = PriorityList[index2];
            PriorityList[index2] = currentItemPriority;
        }

        /// <summary>
        /// Percolates the node at the given index in the heap downward until the heap property is satisfied.
        /// </summary>
        protected int PercolateDown(int index)
        {
            // Set current index as root
            int currentIndex = index;
            int leftChildIndex = IndexOfLeftChildFor(currentIndex);
            int rightChildIndex = IndexOfRightChildFor(currentIndex);
            // While current value is greater than any of its child values
            while ( (leftChildIndex > 0 &&
                    (PriorityList[currentIndex] > PriorityList[leftChildIndex] //||
                    //(NodeList[currentIndex].Value == NodeList[leftChildIndex].Value &&
                    // NodeList[currentIndex].H > NodeList[leftChildIndex].Value)))
                    ))
                     ||
                    (rightChildIndex > 0 &&
                    (PriorityList[currentIndex] > PriorityList[rightChildIndex] //||
                    //(NodeList[currentIndex].Value == NodeList[rightChildIndex].Value &&
                    // NodeList[currentIndex].H > NodeList[rightChildIndex].H))))
                    )))
            {
                // Swap current with its lowest valued child
                if (rightChildIndex <= 0 || PriorityList[leftChildIndex] < PriorityList[rightChildIndex])
                {
                    Swap(currentIndex, leftChildIndex);
                    // Update current index
                    currentIndex = leftChildIndex;
                }
                else
                {
                    Swap(currentIndex, rightChildIndex);
                    // Update current index
                    currentIndex = rightChildIndex;
                }
                // Update child indexes
                leftChildIndex = IndexOfLeftChildFor(currentIndex);
                rightChildIndex = IndexOfRightChildFor(currentIndex);
            }
            return currentIndex;
        }

        /// <summary>
        /// Replaces the node at the given index of the heap with the last item in the heap.
        /// </summary>
        protected virtual void Replace(int index)
        {
            // Get last item
            int lastItem = PriorityList[PriorityList.Count - 1];
            // Write last item to given index
            PriorityList[index] = lastItem;
            // Remove last item
            PriorityList.RemoveAt(PriorityList.Count - 1);
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
            if (index >= PriorityList.Count)
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
            if (leftChildIndex < 1 || leftChildIndex + 1 >= PriorityList.Count)
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
                index = ((childIndex - 2) + (childIndex % 2)) / 2;
            return index;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Returns the <see cref="INode"/> with the lowest value but does not remove it from the heap.
        /// </summary>
        //public virtual int Peek
        //{
        //    get
        //    {
        //        // TODO: Fix this, user could potentially update node's values
        //        if (PriorityList.Count > 0)
        //            return PriorityList[0];
        //        return -1;
        //    }
        //}

        #endregion

        #region Internal Properties

        

        protected List<int> PriorityList { get; set; }

        #endregion
    }
}
