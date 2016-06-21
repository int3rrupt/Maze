using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Maze.Tests
{
    [TestClass()]
    public class HeapTests
    {
        [TestMethod()]
        public void InsertTest()
        {
            int[] testInput = { 1343, 1929, 15, 89, 2345, 7, 25, 252, 3245, 11, 2345, 245, 2462, 29, 124, 6348, 528, 925, 534, 3, 53452, 345329, 1249, 8371, 4, 23423, 52352, 6546, 98725, 2342, 3, 63462, 3, 16, 34, 82, 12, 54, 12, 2, 3, 23, 53, 1, 89, 42, 11, 2, 523, 5, 7, 8, 4, 26, 18, 43, 0, 1, 63, 9 };
            int[] sortedInput = { 0, 1, 1, 2, 2, 3, 3, 3, 3, 4, 4, 5, 7, 7, 8, 9, 11, 11, 12, 12, 15, 16, 18, 23, 25, 26, 29, 34, 42, 43, 53, 54, 63, 82, 89, 89, 124, 245, 252, 523, 528, 534, 925, 1249, 1343, 1929, 2342, 2345, 2345, 2462, 3245, 6348, 6546, 8371, 23423, 52352, 53452, 63462, 98725, 345329 };
            BinaryHeap bHeap = new BinaryHeap();

            // Populate heap
            for (int i = 0; i < testInput.Length; i++)
            {
                bHeap.Insert(i, testInput[i]);
            }
            // Check heap
            for (int i = 0; i < sortedInput.Length; i++)
            {
                if (bHeap.Extract().Priority != sortedInput[i])
                    Assert.Fail();
            }
        }
    }
}

namespace Maze.Imaging.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        [TestMethod()]
        public void BitmapToBitmapArrayTest()
        {
            Assert.Fail();
        }
    }
}