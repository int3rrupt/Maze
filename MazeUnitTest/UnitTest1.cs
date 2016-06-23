using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Maze.Tests
{
    [TestClass()]
    public class HeapTests
    {
        #region Test Methods

        [TestMethod]
        public void Insert_SingleIntoEmptyHeap()
        {
            BinaryHeap<int, int> heap = new BinaryHeap<int, int>();
            // Insert single item into empty heap
            heap.Insert(0, 0);
            // Verify what was inserted
            Assert.IsTrue(heap.Extract().Key == 0);
        }

        [TestMethod]
        public void Insert_SmallInOrderIntoHeap()
        {
            int[] inputData = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15};
            BinaryHeap<int, int> heap = new BinaryHeap<int, int>();
            // Verify input against expected result
            Assert.IsTrue(HeapInsertExtractCheck(inputData, inputData));
        }

        [TestMethod]
        public void Insert_SmallReverseOrderIntoHeap()
        {
            int[] inputData = { 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
            int[] sortedInput = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            BinaryHeap<int, int> heap = new BinaryHeap<int, int>();
            // Verify input against expected result
            Assert.IsTrue(HeapInsertExtractCheck(inputData, sortedInput));
        }

        [TestMethod()]
        public void Insert_SmallOutOfOrderIntoHeap()
        {
            int[] inputData = { 1343, 1929, 15, 89, 2345, 7, 1234, 2, 531, 86, 1, 846, 23, 6, 2, 1 };
            int[] sortedInput = { 1, 1, 2, 2, 6, 7, 15, 23, 86, 89, 531, 846, 1234, 1343, 1929, 2345 };
            // Verify input against expected result
            Assert.IsTrue(HeapInsertExtractCheck(inputData, sortedInput));
        }

        [TestMethod()]
        public void Insert_MediumOutOfOrderIntoHeap()
        {
            int[] inputData = { 391, 413, 3423, 2332, 14, 756, 34, 2, 5, 1, 65632, 1, 4535, 231, 34134, 31, 131, 13, 413, 76, 234, 84, 134, 87123, 5463, 4867, 234, 1, 5, 7, 2, 0, 1, 12, 532 };
            int[] sortedInput = { 0, 1, 1, 1, 1, 2, 2, 5, 5, 7, 12, 13, 14, 31, 34, 76, 84, 131, 134, 231, 234, 234, 391, 413, 413, 532, 756, 2332, 3423, 4535, 4867, 5463, 34134, 65632, 87123 };
            // Verify input against expected result
            Assert.IsTrue(HeapInsertExtractCheck(inputData, sortedInput));
        }

        [TestMethod]
        public void Extract_FromEmptyHeap()
        {
            Assert.Fail();
        }

        #endregion

        #region Helper Methods

        public void HeapInsert(BinaryHeap<int, int> heap, int[] inputData)
        {
            // Insert all items into heap
            for (int i = 0; i < inputData.Length; i++)
            {
                heap.Insert(inputData[i], i);
            }
        }

        public bool HeapExtractVerify(BinaryHeap<int, int> heap, int[] expectedOutput)
        {
            // Verify what is extracted against expected result
            for (int i = 0; i < expectedOutput.Length; i++)
            {
                if (heap.Extract().Key != expectedOutput[i])
                    return false;
            }
            return true;
        }

        public bool HeapInsertExtractCheck(int[] inputData, int[] expectedOutput)
        {
            BinaryHeap<int, int> heap = new BinaryHeap<int, int>();
            // Insert all items into empty heap
            HeapInsert(heap, inputData);
            // Verify what against expected result
            return (HeapExtractVerify(heap, expectedOutput));
        }

        #endregion
    }
}

namespace Maze.Imaging.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        //S[TestMethod()]
        public void BitmapToBitmapArrayTest()
        {
            Assert.Fail();
        }
    }
}