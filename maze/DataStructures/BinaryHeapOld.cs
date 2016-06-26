using Common.DataStructures.Interfaces;
using System.Collections.Generic;

namespace Common.DataStructures
{
    /// <summary>
    /// A Generic Binary Min Heap Class.
    /// </summary>
    /// <typeparam name="T">A <see cref="T"/>, the type used for keys and values.</typeparam>
    public class BinaryHeapOld<TNode> where TNode: INode
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryHeap{T}"/> class.
        /// </summary>
        public BinaryHeapOld()
        {
            NodeList = new List<INode>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Inserts the given node into the heap.
        /// </summary>
        /// <param name="node">A <see cref="INode"/>, the node to be added to the heap.</param>
        public void Insert(INode node)
        {
            // Append to end of heap
            NodeList.Add(node);
            // Percolate new value up
            PercolateUp();
        }

        /// <summary>
        /// Extracts the <see cref="INode"/> with the lowest Value.
        /// </summary>
        /// <returns>A <see cref="INode"/>, the node with the lowest value.</returns>
        public INode ExtractRoot()
        {
            INode minValue = null;
            // Check for empty heap
            if (NodeList.Count > 0)
            {
                // Store min value before removing
                minValue = NodeList[0];
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
        /// </summary>
        internal void PercolateUp()
        {
            // Grab last item in heap
            int currentIndex = NodeList.Count - 1;
            int parentIndex = IndexOfParentFor(currentIndex);
            // While current value is less than its parent value
            while (parentIndex > -1 && NodeList[currentIndex].Value < NodeList[parentIndex].Value)
            {
                // Swap current with its parent
                INode currentItem = NodeList[currentIndex];
                NodeList[currentIndex] = NodeList[parentIndex];
                NodeList[parentIndex] = currentItem;
                // Update indexes
                currentIndex = parentIndex;
                parentIndex = IndexOfParentFor(currentIndex);
            }
        }

        /// <summary>
        /// Percolates the node at the given index in the heap downward until the heap property is satisfied.
        /// </summary>
        internal void PercolateDown(int index)
        {
            // Set current index as root
            int currentIndex = index;
            int leftChildIndex = IndexOfLeftChildFor(currentIndex);
            int rightChildIndex = IndexOfRightChildFor(currentIndex);
            // While current value is greater than any of its child values
            while ((leftChildIndex > 0 && NodeList[currentIndex].Value > NodeList[leftChildIndex].Value) ||
                   (rightChildIndex > 0 && NodeList[currentIndex].Value > NodeList[rightChildIndex].Value))
            {
                INode currentItem = NodeList[currentIndex];
                // Swap current with its lowest valued child
                if (rightChildIndex < 1 || NodeList[leftChildIndex].Value < NodeList[rightChildIndex].Value)
                {
                    NodeList[currentIndex] = NodeList[leftChildIndex];
                    NodeList[leftChildIndex] = currentItem;
                    // Update current index
                    currentIndex = leftChildIndex;
                }
                else
                {
                    NodeList[currentIndex] = NodeList[rightChildIndex];
                    NodeList[rightChildIndex] = currentItem;
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
        internal void Replace(int index)
        {
            // Get last item
            INode lastItem = NodeList[NodeList.Count - 1];
            // Write last item to given index
            NodeList[index] = lastItem;
            // Remove last item
            NodeList.RemoveAt(NodeList.Count - 1);
        }

        /// <summary>
        /// Determines the index of the left child for the parent at the given index.
        /// </summary>
        /// <param name="parentIndex">An <see cref="int"/>, the parent's index used to find the index of its left child.</param>
        /// <returns>An <see cref="int"/>, the index of the given parent's left child. Returns -1 when no left child exists for the given parent index.</returns>
        internal int IndexOfLeftChildFor(int parentIndex)
        {
            // Find potential left child index
            int index = (2 * parentIndex) + 1;
            // Verify index not greater than size of heap
            if (index >= NodeList.Count)
                index = -1;
            return index;
        }

        /// <summary>
        /// Determines the index of the right child for the parent at the given index.
        /// </summary>
        /// <param name="parentIndex">An <see cref="int"/>, the parent's index used to find the index of its right child.</param>
        /// <returns>An <see cref="int"/>, the index of the given parent's right child. Returns -1 when no right child exists for the given parent index.</returns>
        internal int IndexOfRightChildFor(int parentIndex)
        {
            int index;
            // Find left child index
            int leftChildIndex = IndexOfLeftChildFor(parentIndex);
            // Verify left child exists and that right child index not greater than size of heap
            if (leftChildIndex < 1 || leftChildIndex + 1 >= NodeList.Count)
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
        internal int IndexOfParentFor(int childIndex)
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

        #region Public Properties

        /// <summary>
        /// Returns the <see cref="INode"/> with the lowest value but does not remove it from the heap.
        /// </summary>
        public INode Peek
        {
            get
            {
                // TODO: Fix this, user could potentially update node's values
                if (NodeList.Count > 0)
                    return NodeList[0];
                return null;
            }
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// A <see cref="List{T}"/> of <see cref="INode{T}"/> used to represent the heap.
        /// </summary>
        internal List<INode> NodeList { get; set; }

        #endregion
    }
}
