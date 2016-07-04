using System.Collections.Generic;

namespace Common.DataTypes
{
    /// <summary>
    /// A Binary Min Heap abstract class
    /// </summary>
    public abstract class BinaryHeap
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance of the <see cref="BinaryHeap"/> class.
        /// </summary>
        public BinaryHeap()
        {
            ItemList = new List<int>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Inserts the given item into the heap.
        /// </summary>
        /// <param name="item">An <see cref="int"/>, the item to be added to the heap.</param>
        public int Insert(int item)
        {
            // Append to end of heap
            ItemList.Add(item);
            // Percolate new item up
            return PercolateUp();
        }

        /// <summary>
        /// Extracts the <see cref="int"/> with the lowest value.
        /// </summary>
        /// <returns>An <see cref="int"/>, the item with the lowest value.</returns>
        public int ExtractRoot()
        {
            int minValue = -1;
            // Check for empty heap
            if (ItemList.Count > 0)
            {
                // Store min value before removing
                minValue = ItemList[0];
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
        /// <returns>An <see cref="int"/>, the new index of the current item.</returns>
        protected int PercolateUp()
        {
            // Grab last item in heap
            int currentIndex = ItemList.Count - 1;
            int parentIndex = IndexOfParentFor(currentIndex);
            // While current value is less than its parent value
            while (parentIndex > -1 && (ItemList[currentIndex] < ItemList[parentIndex]))
            {
                // Swap the two values
                Swap(currentIndex, parentIndex);
                // Update indexes
                currentIndex = parentIndex;
                parentIndex = IndexOfParentFor(currentIndex);
            }
            return currentIndex;
        }

        /// <summary>
        /// Swaps the values at the two indices with each other.
        /// </summary>
        /// <param name="index1">An <see cref="int"/>, the index of the fist item.</param>
        /// <param name="index2">An <see cref="int"/>, the index of the second item.</param>
        protected virtual void Swap(int index1, int index2)
        {
            // Swap the values of two items
            int currentItemPriority = ItemList[index1];
            ItemList[index1] = ItemList[index2];
            ItemList[index2] = currentItemPriority;
        }

        /// <summary>
        /// Percolates the item at the given index in the heap downward until the heap property is satisfied.
        /// </summary>
        /// <returns>An <see cref="int"/>, the new index of the current item.</returns>
        protected int PercolateDown(int index)
        {
            // Set current index as root
            int currentIndex = index;
            int leftChildIndex = IndexOfLeftChildFor(currentIndex);
            int rightChildIndex = IndexOfRightChildFor(currentIndex);
            // While current value is greater than any of its child values
            while ( (leftChildIndex > 0 && (ItemList[currentIndex] > ItemList[leftChildIndex])) ||
                    (rightChildIndex > 0 && (ItemList[currentIndex] > ItemList[rightChildIndex])))
            {
                // Swap current with its lowest valued child
                if (rightChildIndex <= 0 || ItemList[leftChildIndex] < ItemList[rightChildIndex])
                {
                    // Swap the two values
                    Swap(currentIndex, leftChildIndex);
                    // Update current index
                    currentIndex = leftChildIndex;
                }
                else
                {
                    // Swap the two values
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
        /// Replaces the item at the given index of the heap with the last item in the heap.
        /// </summary>
        /// <param name="index">An <see cref="int"/>, the index of the item to be replaced.</param>
        protected virtual void Replace(int index)
        {
            // Get last item
            int lastItem = ItemList[ItemList.Count - 1];
            // Write last item to given index
            ItemList[index] = lastItem;
            // Remove last item
            ItemList.RemoveAt(ItemList.Count - 1);
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
            if (index >= ItemList.Count)
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
            if (leftChildIndex < 1 || leftChildIndex + 1 >= ItemList.Count)
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

        #region Internal Properties

        /// <summary>
        /// The collection of items in the heap.
        /// </summary>
        protected List<int> ItemList { get; set; }

        #endregion
    }
}
