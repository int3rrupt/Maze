using Common.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace MazeUnitTest
{
    /// <summary>
    /// Contains unit tests for the PriorityQueue class
    /// </summary>
    [TestClass]
    public class PriorityQueueTests
    {
        #region Declarations
        
        // In Order
        private static int[] small_InOrder_InputData = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        // Reverse Order
        private static int[] small_ReverseOrder_InputData = { 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
        private static int[] small_ReverseOrder_ExpectedOutput = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        // Out of Order
        private static int[] small_OutOfOrder_InputData = { 1343, 1929, 15, 89, 2345, 7, 1234, 2, 531, 86, 1, 846, 23, 6, 2, 1 };
        private static int[] small_OutOfOrder_ExpectedOutput = { 1, 1, 2, 2, 6, 7, 15, 23, 86, 89, 531, 846, 1234, 1343, 1929, 2345 };
        private static int[] medium_OutOfOrder_InputData = { 391, 413, 3423, 2332, 14, 756, 34, 2, 5, 1, 65632, 1, 4535, 231, 34134, 31, 131, 13, 413, 76, 234, 84, 134, 87123, 5463, 4867, 234, 1, 5, 7, 2, 0, 1, 12, 532 };
        private static int[] medium_OutOfOrder_ExpectedOutput = { 0, 1, 1, 1, 1, 2, 2, 5, 5, 7, 12, 13, 14, 31, 34, 76, 84, 131, 134, 231, 234, 234, 391, 413, 413, 532, 756, 2332, 3423, 4535, 4867, 5463, 34134, 65632, 87123 };

        #endregion


        #region Test Methods

        /// <summary>
        /// Tests enqueuing and dequeuing of a single node into an empty queue.
        /// </summary>
        [TestMethod]
        public void EnqueueDequeue_SingleIntoEmptyQueue()
        {
            PriorityQueue<AStarNode> queue = new PriorityQueue<AStarNode>();
            // Insert single item into empty heap
            queue.Enqueue(new AStarNode(0, 0, 0, 0, null));
            // Verify what was inserted
            Assert.IsTrue(queue.DequeueHighestPriority().Value == 0);
        }

        /// <summary>
        /// Tests enqueuing and dequeuing of small collection of ordered nodes into a queue.
        /// </summary>
        [TestMethod]
        public void EnqueueDequeue_SmallInOrderIntoQueue()
        {
            // Verify input against expected result
            Assert.IsTrue(QueueEnqueueDequeueHighestCheck(small_InOrder_InputData, small_InOrder_InputData));
        }

        /// <summary>
        /// Tests enqueuing and dequeuing of small collection of reverse ordered nodes into a queue.
        /// </summary>
        [TestMethod]
        public void EnqueueDequeue_SmallReverseOrderIntoQueue()
        {
            // Verify input against expected result
            Assert.IsTrue(QueueEnqueueDequeueHighestCheck(small_ReverseOrder_InputData, small_ReverseOrder_ExpectedOutput));
        }

        /// <summary>
        /// Tests enqueuing and dequeuing of small collection of unordered nodes into a queue.
        /// </summary>
        [TestMethod()]
        public void EnqueueDequeue_SmallOutOfOrderIntoQueue()
        {
            // Verify input against expected result
            Assert.IsTrue(QueueEnqueueDequeueHighestCheck(small_OutOfOrder_InputData, small_OutOfOrder_ExpectedOutput));
        }

        /// <summary>
        /// Tests enqueuing and dequeuing of medium collection of unordered nodes into a queue.
        /// </summary>
        [TestMethod()]
        public void EnqueueDequeue_MediumOutOfOrderIntoQueue()
        {
            // Verify input against expected result
            Assert.IsTrue(QueueEnqueueDequeueHighestCheck(medium_OutOfOrder_InputData, medium_OutOfOrder_ExpectedOutput));
        }

        /// <summary>
        /// Tests dequeuing from an empty queue.
        /// </summary>
        [TestMethod]
        public void Dequeue_FromEmptyQueue()
        {
            PriorityQueue<AStarNode> queue = new PriorityQueue<AStarNode>();
            Assert.IsNull(queue.DequeueHighestPriority());
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
            PriorityQueue<AStarNode> heap = CreateQueue(inputData);
            // Peek
            AStarNode node = (AStarNode)heap.Peek;
            // Verify not null
            Assert.IsNotNull(node);
            // Verify input against expected result
            Assert.IsTrue(node.Value == sortedInput[0]);
        }

        /// <summary>
        /// Tests dequeuing from the queue where the node's ID equals the given value.
        /// </summary>
        [TestMethod]
        public void Dequeue_KeyFromQueue()
        {
            // Create new queue using medium out of order data
            PriorityQueue<AStarNode> queue = CreateQueue(medium_OutOfOrder_InputData);
            // Randomly choose a node ID, where node ID is the index of the array
            int nodeId = new Random().Next(0, medium_OutOfOrder_InputData.Length);
            // Get corresponding value
            int nodePriority = medium_OutOfOrder_InputData[nodeId];
            // Attempt to dequeue node where key equals the randomly selected node ID
            AStarNode node = queue.Dequeue(nodeId);
            // Verify node was found
            Assert.IsNotNull(node);
            // Verify node ID and value (priority)
            Assert.IsTrue(node.Key.CompareTo(nodeId) == 0 && node.Value.CompareTo(nodePriority) == 0, $"Failed to dequeue node where ID:[{nodeId}] from the queue.");

            // Create temp object to remove random node ID
            List<int> temp = new List<int>(medium_OutOfOrder_ExpectedOutput);
            // Remove randomly selected item
            temp.Remove(nodePriority);
            // Verify queue is still in order
            Assert.IsTrue(QueueDequeueHighestVerify(queue, temp.ToArray()));
        }

        /// <summary>
        /// Tests queue search
        /// </summary>
        [TestMethod]
        public void KeyExistsInQueue()
        {
            // Create new queue using medium out of order data
            PriorityQueue<AStarNode> queue = CreateQueue(medium_OutOfOrder_InputData);
            // Randomly choose a node ID, where node ID is the index of the array
            int nodeId = new Random().Next(0, medium_OutOfOrder_InputData.Length);
            // Get corresponding value
            int nodePriority = medium_OutOfOrder_InputData[nodeId];
            // Attempt to find node where key equals the randomly selected node ID
            AStarNode node = queue.Exists(nodeId);
            // Verify node was found
            Assert.IsNotNull(node);
            // Verify node ID and value (priority)
            Assert.IsTrue(node.Key.CompareTo(nodeId) == 0 && node.Value.CompareTo(nodePriority) == 0, $"Failed to dequeue node where ID:[{nodeId}] from the queue.");
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Enqueues the given data into the given priority queue.
        /// </summary>
        /// <param name="queue">A <see cref="PriorityQueue{T}"/>, the queue to insert the data into.</param>
        /// <param name="inputData">An <see cref="int[]"/>, the input data to insert into the queue.</param>
        private void QueueEnqueue(PriorityQueue<AStarNode> queue, int[] inputData)
        {
            // Enqueue all items into queue
            for (int i = 0; i < inputData.Length; i++)
            {
                queue.Enqueue(new AStarNode(i, inputData[i], 0, 0, null));
            }
        }

        /// <summary>
        /// Dequeues from the queue and verifies the dequeued value against the given expected ordered data.
        /// </summary>
        /// <param name="queue">A <see cref="PriorityQueue{T}"/>, the queue to dequeue from.</param>
        /// <param name="expectedOutput">An <see cref="int[]"/>, the ordered expected output.</param>
        /// <returns>A <see cref="bool"/>, true when all dequeues from the queue match the data given, false otherwise.</returns>
        private bool QueueDequeueHighestVerify(PriorityQueue<AStarNode> queue, int[] expectedOutput)
        {
            // Verify what is extracted against expected result
            for (int i = 0; i < expectedOutput.Length; i++)
            {
                if (queue.DequeueHighestPriority().Value != expectedOutput[i])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Enqueues the given input data into a new queue, dequeues from the queue and checks against the given ordered expected data.
        /// </summary>
        /// <param name="inputData">An <see cref="int[]"/>, the input data to insert into the queue.</param>
        /// <param name="expectedOutput">An <see cref="int[]"/>, the ordered expected output.</param>
        /// <returns>A <see cref="bool"/>, true when all dequeues from the queue match the data given, false otherwise.</returns>
        private bool QueueEnqueueDequeueHighestCheck(int[] inputData, int[] expectedOutput)
        {
            // Create new queue and add inputData
            PriorityQueue<AStarNode> queue = CreateQueue(inputData);
            // Verify what against expected result
            return (QueueDequeueHighestVerify(queue, expectedOutput));
        }

        /// <summary>
        /// Creates a new PriorityQueue and populates it with the given input data.
        /// </summary>
        /// <param name="inputData">An <see cref="int[]"/>, the input data to enqueue into the queue.</param>
        /// <returns>A <see cref="PriorityQueue{TNode}"/>, the newly created queue containing the given data.</returns>
        private PriorityQueue<AStarNode> CreateQueue(int[] inputData)
        {
            PriorityQueue<AStarNode> queue = new PriorityQueue<AStarNode>();
            // Insert all items into empty heap
            QueueEnqueue(queue, inputData);
            return queue;
        }

        #endregion
    }

}
