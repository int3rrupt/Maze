using System;

namespace Maze
{
    public class BinaryHeap
    {
        #region Constructor

        public BinaryHeap()
        {
            Count = 0;
        }

        #endregion

        #region Public Methods

        public void Insert(int nodeId, int priority)
        {
            PriorityQueueNode newNode = new PriorityQueueNode(nodeId, priority);
            // If tree is empty, add to head
            if (Count == 0)
            {
                Head = newNode;
                Tail = Head;
                Count++;
                return;
            }
            // Otherwise Add to bottom of heap
            AddToBottom(newNode);
            // Re-order heap
            PercolateUp();
            Count++;
        }

        public PriorityQueueNode Extract()
        {
            PriorityQueueNode head = Head;
            ReplaceHeadWithTail();
            PercolateDown();

            return head;
        }

        #endregion

        #region PrivateMethods

        private void AddToBottom(PriorityQueueNode node)
        {
            // *NOTES:
            // TODO: Add explanation

            // Determine whether Tail is a left or right child
            NodeType currentNodeType = GetNodeParentRelation(Tail);
            if (currentNodeType == NodeType.Head)
            {
                node.Parent = Head;
                node.Parent.LeftChild = node;
            }
            else if (currentNodeType == NodeType.LeftChild)
            {
                node.Parent = Tail.Parent;
                node.Parent.RightChild = node;
            }
            else // Tail is Right Child
            {
                // Determine whether Tail.Parent is a left or right child
                currentNodeType = GetNodeParentRelation(Tail.Parent);
                if (currentNodeType == NodeType.Head)
                {
                    node.Parent = Head.LeftChild;
                    node.Parent.LeftChild = node;
                }
                else if (currentNodeType == NodeType.LeftChild)
                {
                    node.Parent = Tail.Parent.Parent.RightChild;
                    node.Parent.LeftChild = node;
                    
                }
                else // Tail.Parent is Right Child
                {
                    // Find the closest parent of node where parent is a left child or root
                    PriorityQueueNode leftChildParent = GetClosestParentAsLeftChild(Tail);
                    // If parent is the top of heap set node to leftmost child
                    if (leftChildParent == Head)
                    {
                        node.Parent = GetLeftMostChild(leftChildParent);
                        node.Parent.LeftChild = node;
                    }
                    else // Parent is not top of heap
                    {
                        node.Parent = GetLeftMostChild(leftChildParent.Parent.RightChild);
                        node.Parent.LeftChild = node;
                    }
                }
            }
            // Update Tail
            Tail = node;
        }

        private NodeType GetNodeParentRelation(PriorityQueueNode node)
        {
            if (node == Head)
                return NodeType.Head;
            if (node.Parent.LeftChild == node)
                return NodeType.LeftChild;
            return NodeType.RightChild;
        }

        private PriorityQueueNode GetClosestParentAsLeftChild(PriorityQueueNode node)
        {
            PriorityQueueNode currentNode = node.Parent;
            // Search for next parent
            while (currentNode != Head)
            {
                // Check if current node parent is a left child
                if (GetNodeParentRelation(currentNode) == NodeType.LeftChild)
                    return currentNode;
                // Update current node to current node's parent and continue search
                currentNode = currentNode.Parent;
            }
            return currentNode;
        }

        private PriorityQueueNode GetLeftMostChild(PriorityQueueNode node)
        {
            PriorityQueueNode currentNode = node;
            // Search for leftmost child
            while (currentNode.LeftChild != null)
            {
                currentNode = currentNode.LeftChild;
            }
            return currentNode;
        }

        private void PercolateUp()
        {
            PriorityQueueNode node = Tail;
            // Compare node priorities
            while (node != Head && node.Priority < node.Parent.Priority)
            {
                // Switch x and y
                PriorityQueueNode x = node;
                PriorityQueueNode y = node.Parent;
                PriorityQueueNode xLeftChild = x.LeftChild;
                PriorityQueueNode xRightChild = x.RightChild;
                PriorityQueueNode yLeftChild = y.LeftChild;
                PriorityQueueNode yRightChild = y.RightChild;
                PriorityQueueNode yParent = y.Parent;
                
                // Update y
                y.LeftChild = xLeftChild;
                y.RightChild = xRightChild;
                y.Parent = x;
                // Update x children
                if (xLeftChild != null)
                    xLeftChild.Parent = y;
                if (xRightChild != null)
                    xRightChild.Parent = y;
                // Update x
                if (x == yLeftChild)
                {
                    x.LeftChild = y;
                    x.RightChild = yRightChild;
                }
                else
                {
                    x.LeftChild = yLeftChild;
                    x.RightChild = y;
                }
                x.Parent = yParent;
                // Update original y parent
                if (yParent != null)
                {
                    if (y == yParent.LeftChild)
                        yParent.LeftChild = x;
                    else
                        yParent.RightChild = x;
                }

                // Check if switching tail
                if (x == Tail)
                    Tail = y;
                if (y == Head)
                    Head = x;
            }
        }

        private void PercolateDown()
        {
            
            PriorityQueueNode node = Head;
            // Compare node priorities
            while ((node.LeftChild != null && node.Priority > node.LeftChild.Priority) || (node.RightChild != null && node.Priority > node.RightChild.Priority))
            {
                // Switch x and y
                PriorityQueueNode x = node;
                PriorityQueueNode y;
                // Determine which child node to switch with, use smaller of the two
                if (node.RightChild == null || (node.LeftChild != null && node.LeftChild.Priority < node.RightChild.Priority))
                    y = node.LeftChild;
                else
                    y = node.RightChild;
                PriorityQueueNode xLeftChild = x.LeftChild;
                PriorityQueueNode xRightChild = x.RightChild;
                PriorityQueueNode yLeftChild = y.LeftChild;
                PriorityQueueNode yRightChild = y.RightChild;
                PriorityQueueNode xParent = x.Parent;

                // Update y children
                if (yLeftChild != null)
                    yLeftChild.Parent = x;
                if (yRightChild != null)
                    yRightChild.Parent = x;
                // Update x
                x.LeftChild = yLeftChild;
                x.RightChild = yRightChild;
                x.Parent = y;
                // Update y
                if (y == xLeftChild)
                {
                    y.LeftChild = x;
                    y.RightChild = xRightChild;
                }
                else
                {
                    y.LeftChild = xLeftChild;
                    y.RightChild = x;
                }
                y.Parent = xParent;
                // Update original x parent
                if (xParent != null)
                {
                    if (x == xParent.LeftChild)
                        xParent.LeftChild = y;
                    else
                        xParent.RightChild = y;
                }

                // Check if switching tail
                if (y == Tail)
                    Tail = x;
                if (x == Head)
                    Head = y;
            }
        }

        private void ReplaceHeadWithTail()
        {
            // Determine whether Tail is a left or right child
            NodeType currentNodeType = GetNodeParentRelation(Tail);
            if (currentNodeType == NodeType.Head)
            {
                Head = null;
                Tail = null;
                return;
            }
            // Update current Tail
            Tail.LeftChild = Head.LeftChild;
            Tail.RightChild = Head.RightChild;
            // Update current Head.Children
            Tail.LeftChild.Parent = Tail;
            Tail.RightChild.Parent = Tail;
            // Done with Head replace Head with Tail
            Head = Tail;
            // Set new Tail
            if (currentNodeType == NodeType.RightChild)
            {
                Tail = Head.Parent.LeftChild;
                Tail.Parent.RightChild = null;
            }
            else // Tail is left child
            {
                Head.Parent.LeftChild = null;
                Tail = GetNextRightMostNode(Head.Parent);
            }
            // Update Head.Parent
            Head.Parent = null;
        }

        private PriorityQueueNode GetNextRightMostNode(PriorityQueueNode node)
        {
            bool foundParent = false;
            PriorityQueueNode currentNode = node.Parent;
            // Search for next parent where parent.LeftChild is not current branch
            while (currentNode != Head && !foundParent)
            {
                // Check if current Node's parent's left child is current node
                if (currentNode.Parent.LeftChild != currentNode)
                    foundParent = true;
                // Update current node to current node's parent
                currentNode = currentNode.Parent;
            }
            // Jump to the left branch
            currentNode = currentNode.LeftChild;
            // Get rightmost node starting at current node
            while (currentNode.RightChild != null)
            {
                currentNode = currentNode.RightChild;
            }

            return currentNode;
        }

        #endregion

        #region Private Properties

        private PriorityQueueNode Head { get; set; }
        private PriorityQueueNode Tail { get; set; }
        private int Count { get; set; }

        #endregion
    }
}
