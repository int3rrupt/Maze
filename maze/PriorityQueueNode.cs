namespace Maze
{
    public class PriorityQueueNode
    {
        public PriorityQueueNode(int nodeId, int priority)
        {
            NodeId = nodeId;
            Priority = priority;
        }

        public PriorityQueueNode Parent { get; set; }
        public PriorityQueueNode LeftChild { get; set; }
        public PriorityQueueNode RightChild { get; set; }
        public int NodeId { get; set; }
        public int Priority { get; set; }
    }
}
