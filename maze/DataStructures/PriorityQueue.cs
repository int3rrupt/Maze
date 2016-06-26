using Common.DataStructures.Interfaces;

namespace Common.DataStructures
{
    public class PriorityQueue<TNode> where TNode : IAStarNode
    {
        #region Constructors

        /// <summary>
        /// Initializes a new PriorityQueue class.
        /// </summary>
        public PriorityQueue()
        {
            Heap = new BinaryHeap<TNode>();
        }

        #endregion

        #region Public Methods

        #region Standard Operations

        /// <summary>
        /// Enqueues the given node based on the given priority.
        /// </summary>
        /// <param name="node">An <see cref="AStarNode{T}"/>, the item to enqueue.</param>
        public void Enqueue(AStarNode node)
        {
            Heap.Insert(node);
        }

        /// <summary>
        /// Dequeues the node with the highest priority (lowest value).
        /// </summary>
        /// <returns>An <see cref="AStarNode{T}"/>, the node with the highest priority.</returns>
        public AStarNode DequeueHighestPriority()
        {
            return (AStarNode)Heap.ExtractRoot();
        }

        #endregion

        #region Additional Operations

        /// <summary>
        /// Dequeues the node with the given key.
        /// </summary>
        /// <param name="key">A <see cref="T"/>, the item to dequeue.</param>
        /// <returns>A <see cref="T"/>, the item requested.</returns>
        public AStarNode Dequeue(int key)
        {
            int index = FindNodeIndexWithKey(key);
            return DequeueAt(index);
        }

        public AStarNode DequeueAt(int index)
        {
            if (index < 0 || index >= Heap.NodeList.Count)
                return null;
            //int index = Heap.NodeList.FindIndex(n => n.Key.CompareTo(key) == 0);
            AStarNode node = (AStarNode)Heap.NodeList[index];
            Heap.Replace(index);
            Heap.PercolateDown(index);
            // Determine whether value exists
            //AStarNode<T> node = Exists(key);
            // Remove from heap
            // heap.Remove(node);
            return node;
        }

        /// <summary>
        /// Determines whether the given key exists in the queue and returns it.
        /// </summary>
        /// <param name="key">A <see cref="typeof(T)"/>, the key to search the queue for.</param>
        /// <returns>An <see cref="AStarNode{T}"/>, the node containing the given key.</returns>
        public AStarNode Exists(int key)
        {
            int index = FindNodeIndexWithKey(key);
            if (index > -1)
                return (AStarNode)Heap.NodeList[index];
            return null;
            //return (AStarNode)Heap.NodeList.Find(n => n.Key.CompareTo(key) == 0);
        }

        public AStarNode PeekAt(int index)
        {
            return (AStarNode)Heap.NodeList[index];
        }

        #endregion

        #endregion

        #region Private Methods

        public int FindNodeIndexWithKey(int key)
        {
            for (int i = 0; i < Heap.NodeList.Count; i++)
            {
                if (Heap.NodeList[i].Key == key)
                    return i;
            }

            return -1;
        }

        private AStarNode CopyNode(AStarNode node)
        {
            // TODO: figure out how to prevent modifying the parent
            return new AStarNode(node.Key, node.Value, node.G, node.H, (AStarNode)node.Parent);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Returns the <see cref="IAStarNode"/> with the highest priority but does not dequeue it.
        /// </summary>
        public AStarNode Peek
        {
            get
            {
                return (AStarNode)Heap.Peek;
            }
        }

        #endregion

        #region Private Properties

        private BinaryHeap<TNode> Heap { get; set; }

        #endregion
    }
}
