using Common.DataStructures.Interfaces;
using System.Collections.Generic;

namespace Common.DataStructures
{
    public class PriorityQueue<TNode> : BinaryHeap where TNode : INode
    {
        #region Constructors

        /// <summary>
        /// Initializes a new PriorityQueue class.
        /// </summary>
        public PriorityQueue()
            : base()
        {
            NodeList = new List<TNode>();
            IndexLookup = new Dictionary<int, int>();

        }

        #endregion

        #region Public Methods

        #region Standard Operations

        /// <summary>
        /// Enqueues the given node based on the given priority.
        /// </summary>
        /// <param name="node">An <see cref="Node{T}"/>, the item to enqueue.</param>
        public void Enqueue(int priority, TNode node)
        {
            // Append to end of heap
            NodeList.Add(node);
            IndexLookup.Add(node.ID, IndexLookup.Count);
            Insert(priority);
        }

        /// <summary>
        /// Dequeues the node with the highest priority (lowest value).
        /// </summary>
        /// <returns>An <see cref="Node{T}"/>, the node with the highest priority.</returns>
        public TNode DequeueHighestPriority()
        {
            IndexLookup.Remove(Peek.ID);
            TNode node = default(TNode);
            if (NodeList.Count > 0)
            {
                node = NodeList[0];
                ExtractRoot();
            }

            return node;
        }

        #endregion

        #region Overridden Methods

        protected override void Swap(int index1, int index2)
        {
            // Swap priorities
            base.Swap(index1, index2);

            // Swap contents of index1 with index2 and vice versa
            TNode currentItem = NodeList[index1];
            NodeList[index1] = NodeList[index2];
            NodeList[index2] = currentItem;
            // Update index lookup
            IndexLookup[NodeList[index1].ID] = index1;
            IndexLookup[NodeList[index2].ID] = index2;
        }

        protected override void Replace(int index)
        {
            // Replace priorities
            base.Replace(index);

            // Get key of item being replaced
            int replacedKey = NodeList[index].ID;
            // Get last item
            TNode lastItem = NodeList[NodeList.Count - 1];
            // Write last item to given index
            NodeList[index] = lastItem;
            IndexLookup[NodeList[index].ID] = index;
            // Remove last item
            NodeList.RemoveAt(NodeList.Count - 1);
            // Remove lookup to item that was removed
            IndexLookup.Remove(replacedKey);
        }

        #endregion

        #region Additional Operations

        /// <summary>
        /// Dequeues the node with the given key.
        /// </summary>
        /// <param name="key">A <see cref="T"/>, the item to dequeue.</param>
        /// <returns>A <see cref="T"/>, the item requested.</returns>
        public TNode Dequeue(int key)
        {
            int index;
            if (IndexLookup.TryGetValue(key, out index))
                return DequeueAt(index);
            return default(TNode);
        }

        public TNode DequeueAt(int index)
        {
            if (index < 0 || index >= NodeList.Count)
                return default(TNode);
            TNode node = (TNode)NodeList[index];
            Replace(index);
            PercolateDown(index);

            return node;
        }

        /// <summary>
        /// Determines whether the given key exists in the queue and returns it.
        /// </summary>
        /// <param name="key">A <see cref="typeof(T)"/>, the key to search the queue for.</param>
        /// <returns>An <see cref="Node{T}"/>, the node containing the given key.</returns>
        public TNode Exists(int key)
        {
            int index;
            if (IndexLookup.TryGetValue(key, out index))
                return NodeList[index];
            return default(TNode);
            //return (Node)Heap.NodeList.Find(n => n.Key.CompareTo(key) == 0);
        }

        public TNode PeekAt(int index)
        {
            return (TNode)NodeList[index];
        }

        #endregion

        #endregion

        #region Private Methods

        //public int FindNodeIndexWithKey(int key)
        //{
        //    for (int i = 0; i < NodeList.Count; i++)
        //    {
        //        if (NodeList[i].Key == key)
        //            return i;
        //    }

        //    return -1;
        //}

        //private Node CopyNode(Node node)
        //{
        //    // TODO: figure out how to prevent modifying the parent
        //    return new Node(node.Key, node.Value, node.G, node.H, (Node)node.Parent);
        //}

        #endregion

        #region Public Properties

        /// <summary>
        /// Returns the <see cref="INode"/> with the highest priority but does not dequeue it.
        /// </summary>
        public TNode Peek
        {
            get
            {
                // TODO: Fix this, user could potentially update node's values
                if (NodeList.Count > 0)
                    return (TNode)NodeList[0];
                return default(TNode);
            }
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// A <see cref="List{T}"/> of <see cref="INode{T}"/> used to represent the heap.
        /// </summary>
        internal List<TNode> NodeList { get; set; }

        internal Dictionary<int, int> IndexLookup { get; set; }


        #endregion
    }
}
