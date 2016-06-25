using Common.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresUnitTests
{
    /// <summary>
    /// Contains unit tests for the BinaryHeap class
    /// </summary>
    [TestClass]
    public class BinaryHeapTests
    {
        #region Test Methods

        /// <summary>
        /// Tests insertion of a single node into an empty heap.
        /// </summary>
        [TestMethod]
        public void Insert_SingleIntoEmptyHeap()
        {
            BinaryHeap<AStarNode> heap = new BinaryHeap<AStarNode>();
            // Insert single item into empty heap
            heap.Insert(new AStarNode(0, 0, 0, 0, null));
            // Verify what was inserted
            Assert.IsTrue(heap.ExtractRoot().Value == 0);
        }

        /// <summary>
        /// Tests insertion of small collection of ordered nodes into a heap.
        /// </summary>
        [TestMethod]
        public void Insert_SmallInOrderIntoHeap()
        {
            int[] inputData = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            // Verify input against expected result
            Assert.IsTrue(HeapInsertExtractCheck(inputData, inputData));
        }

        /// <summary>
        /// Tests insertion of small collection of reverse ordered nodes into a heap.
        /// </summary>
        [TestMethod]
        public void Insert_SmallReverseOrderIntoHeap()
        {
            int[] inputData = { 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
            int[] sortedInput = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            // Verify input against expected result
            Assert.IsTrue(HeapInsertExtractCheck(inputData, sortedInput));
        }

        /// <summary>
        /// Tests insertion of small collection of unordered nodes into a heap.
        /// </summary>
        [TestMethod()]
        public void Insert_SmallOutOfOrderIntoHeap()
        {
            int[] inputData = { 1343, 1929, 15, 89, 2345, 7, 1234, 2, 531, 86, 1, 846, 23, 6, 2, 1 };
            int[] sortedInput = { 1, 1, 2, 2, 6, 7, 15, 23, 86, 89, 531, 846, 1234, 1343, 1929, 2345 };
            // Verify input against expected result
            Assert.IsTrue(HeapInsertExtractCheck(inputData, sortedInput));
        }

        /// <summary>
        /// Tests insertion of medium collection of unordered nodes into a heap.
        /// </summary>
        [TestMethod()]
        public void Insert_MediumOutOfOrderIntoHeap()
        {
            int[] inputData = { 391, 413, 3423, 2332, 14, 756, 34, 2, 5, 1, 65632, 1, 4535, 231, 34134, 31, 131, 13, 413, 76, 234, 84, 134, 87123, 5463, 4867, 234, 1, 5, 7, 2, 0, 1, 12, 532 };
            int[] sortedInput = { 0, 1, 1, 1, 1, 2, 2, 5, 5, 7, 12, 13, 14, 31, 34, 76, 84, 131, 134, 231, 234, 234, 391, 413, 413, 532, 756, 2332, 3423, 4535, 4867, 5463, 34134, 65632, 87123 };
            // Verify input against expected result
            Assert.IsTrue(HeapInsertExtractCheck(inputData, sortedInput));
        }

        /// <summary>
        /// Tests extraction from an empty heap.
        /// </summary>
        [TestMethod]
        public void Extract_FromEmptyHeap()
        {
            BinaryHeap<AStarNode> heap = new BinaryHeap<AStarNode>();
            Assert.IsNull(heap.ExtractRoot());
        }

        /// <summary>
        /// Tests peek on non empty heap.
        /// </summary>
        [TestMethod()]
        public void Peek_PopulatedHeap()
        {
            int[] inputData = { 391, 413, 3423, 2332, 14, 756, 34, 2, 5, 1, 65632, 1, 4535, 231, 34134, 31, 131, 13, 413, 76, 234, 84, 134, 87123, 5463, 4867, 234, 1, 5, 7, 2, 0, 1, 12, 532 };
            int[] sortedInput = { 0, 1, 1, 1, 1, 2, 2, 5, 5, 7, 12, 13, 14, 31, 34, 76, 84, 131, 134, 231, 234, 234, 391, 413, 413, 532, 756, 2332, 3423, 4535, 4867, 5463, 34134, 65632, 87123 };
            // Populate heap
            BinaryHeap<AStarNode> heap = CreateHeap(inputData);
            // Peek
            AStarNode node = (AStarNode)heap.Peek;
            // Verify not null
            Assert.IsNotNull(node);
            // Verify input against expected result
            Assert.IsTrue(node.Value == sortedInput[0]);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Inserts the given data into the given binary heap.
        /// </summary>
        /// <param name="heap">A <see cref="BinaryHeap{T}"/>, the heap to insert the data into.</param>
        /// <param name="inputData">An <see cref="int[]"/>, the input data to insert into the heap.</param>
        public void HeapInsert(BinaryHeap<AStarNode> heap, int[] inputData)
        {
            // Insert all items into heap
            for (int i = 0; i < inputData.Length; i++)
            {
                heap.Insert(new AStarNode(i, inputData[i], 0, 0, null));
            }
        }

        /// <summary>
        /// Extracts from the heap and verifies the extraction value against the given expected ordered data.
        /// </summary>
        /// <param name="heap">A <see cref="BinaryHeap{T}"/>, the heap to extract from.</param>
        /// <param name="expectedOutput">An <see cref="int[]"/>, the ordered expected output.</param>
        /// <returns>A <see cref="bool"/>, true when all extractions from the heap match the data given, false otherwise.</returns>
        public bool HeapExtractVerify(BinaryHeap<AStarNode> heap, int[] expectedOutput)
        {
            // Verify what is extracted against expected result
            for (int i = 0; i < expectedOutput.Length; i++)
            {
                if (heap.ExtractRoot().Value != expectedOutput[i])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Inserts the given input data into a new heap, extracts from the heap and checks against the given ordered expected data.
        /// </summary>
        /// <param name="inputData">An <see cref="int[]"/>, the input data to insert into the heap.</param>
        /// <param name="expectedOutput">An <see cref="int[]"/>, the ordered expected output.</param>
        /// <returns>A <see cref="bool"/>, true when all extractions from the heap match the data given, false otherwise.</returns>
        public bool HeapInsertExtractCheck(int[] inputData, int[] expectedOutput)
        {
            BinaryHeap<AStarNode> heap = new BinaryHeap<AStarNode>();
            // Insert all items into empty heap
            HeapInsert(heap, inputData);
            // Verify what against expected result
            return (HeapExtractVerify(heap, expectedOutput));
        }

        /// <summary>
        /// Creates a new BinaryHeap and populates it with the given input data.
        /// </summary>
        /// <param name="inputData">An <see cref="int[]"/>, the input data to insert into the heap.</param>
        /// <returns>A <see cref="BinaryHeap{TNode}"/>, the newly created heap containing the given data.</returns>
        private BinaryHeap<AStarNode> CreateHeap(int[] inputData)
        {
            BinaryHeap<AStarNode> queue = new BinaryHeap<AStarNode>();
            // Insert all items into empty heap
            HeapInsert(queue, inputData);
            return queue;
        }

        #endregion
    }
}
