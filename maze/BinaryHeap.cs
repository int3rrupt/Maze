using System;

namespace Maze
{
    public class BinaryHeap<KeyT, ValueT> where KeyT: IComparable
    {
        #region Constructor

        public BinaryHeap()
        {
            Count = 0;
        }

        #endregion

        #region Public Methods

        public void Insert(KeyT key, ValueT value)
        {
            Node<KeyT, ValueT> node = new Node<KeyT, ValueT>(key, value);
            Insert(node);
        }

        public void Insert(Node<KeyT, ValueT> node)
        {
            // Otherwise Add to bottom of heap
            AddToBottom(node);
            // Re-order heap
            PercolateUp();
        }

        public Node<KeyT, ValueT> Extract()
        {
            Node<KeyT, ValueT> head = Head;
            ReplaceHeadWithTail();
            PercolateDown();
            return head;
        }

        public Node<KeyT, ValueT> Extract(ValueT value)
        {
            Node<KeyT, ValueT> head = Head;
            ReplaceHeadWithTail();
            PercolateDown();
            return head;
        }

        #endregion

        #region PrivateMethods

        private void AddToBottom(Node<KeyT, ValueT> node)
        {
            // *NOTES:
            // TODO: Add explanation

            if (Count == 0)
                Head = node;
            else
            {
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
                        Node<KeyT, ValueT> leftChildParent = GetClosestParentAs(NodeType.LeftChild, Tail);
                        // If parent is the top of heap set node to leftmost child
                        if (leftChildParent == Head)
                            node.Parent = GetLeftMostChild(leftChildParent);
                        else // Parent is not top of heap
                            node.Parent = GetLeftMostChild(leftChildParent.Parent.RightChild);
                        node.Parent.LeftChild = node;
                    }
                }
            }
            // Update Tail
            Tail = node;
            // Update Count
            Count++;
        }

        private NodeType GetNodeParentRelation(Node<KeyT, ValueT> node)
        {
            if (node == Head)
                return NodeType.Head;
            if (node.Parent != null && node.Parent.LeftChild == node)
                return NodeType.LeftChild;
            return NodeType.RightChild;
        }

        private Node<KeyT, ValueT> GetClosestParentAs(NodeType nodeType, Node<KeyT, ValueT> node)
        {
            Node<KeyT, ValueT> currentNode = node.Parent;
            // Search for next parent
            while (currentNode != Head)
            {
                // Check if current node parent is of desired node type
                if (GetNodeParentRelation(currentNode) == nodeType)
                    // TODO: Clean up logic if possible
                    return currentNode;
                // Update current node to current node's parent and continue search
                currentNode = currentNode.Parent;
            }
            return currentNode;
        }

        private Node<KeyT, ValueT> GetLeftMostChild(Node<KeyT, ValueT> node)
        {
            Node<KeyT, ValueT> currentNode = node;
            // Search for leftmost child
            while (currentNode.LeftChild != null)
            {
                currentNode = currentNode.LeftChild;
            }
            return currentNode;
        }

        private Node<KeyT, ValueT> GetRightMostChild(Node<KeyT, ValueT> node)
        {
            Node<KeyT, ValueT> currentNode = node;
            // Search for leftmost child
            while (currentNode.RightChild != null)
            {
                currentNode = currentNode.RightChild;
            }
            return currentNode;
        }

        private void PercolateUp()
        {
            Node<KeyT, ValueT> node = Tail;
            // Compare node priorities
            while (node != Head && node.Key.CompareTo(node.Parent.Key) < 0)
            {
                // Switch x and y
                Node<KeyT, ValueT> x = node;
                Node<KeyT, ValueT> y = node.Parent;
                Node<KeyT, ValueT> xLeftChild = x.LeftChild;
                Node<KeyT, ValueT> xRightChild = x.RightChild;
                Node<KeyT, ValueT> yLeftChild = y.LeftChild;
                Node<KeyT, ValueT> yRightChild = y.RightChild;
                Node<KeyT, ValueT> yParent = y.Parent;
                
                // Update y
                y.LeftChild = xLeftChild;
                y.RightChild = xRightChild;
                y.Parent = x;
                // Update y children
                if (x != yLeftChild)
                    yLeftChild.Parent = x;
                else if (yRightChild != null)
                    yRightChild.Parent = x;
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
            if (Count > 0)
            {
                Node<KeyT, ValueT> node = Head;
                // Compare node priorities
                // While current node key is greater
                while ((node.LeftChild != null && node.Key.CompareTo(node.LeftChild.Key) > 0 || (node.RightChild != null && node.Key.CompareTo(node.RightChild.Key) > 0)))
                {
                    // Switch x and y
                    Node<KeyT, ValueT> x = node;
                    Node<KeyT, ValueT> y;
                    // Determine which child node to switch with, use smaller of the two
                    if (node.RightChild == null || (node.LeftChild != null && node.LeftChild.Key.CompareTo(node.RightChild.Key) < 0))
                        y = node.LeftChild;
                    else
                        y = node.RightChild;
                    Node<KeyT, ValueT> xLeftChild = x.LeftChild;
                    Node<KeyT, ValueT> xRightChild = x.RightChild;
                    Node<KeyT, ValueT> xParent = x.Parent;
                    Node<KeyT, ValueT> yLeftChild = y.LeftChild;
                    Node<KeyT, ValueT> yRightChild = y.RightChild;

                    // Update y children
                    if (yLeftChild != null)
                        yLeftChild.Parent = x;
                    if (yRightChild != null)
                        yRightChild.Parent = x;
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
                    // Update x
                    x.LeftChild = yLeftChild;
                    x.RightChild = yRightChild;
                    x.Parent = y;
                    // Update x children
                    if (xLeftChild != y)
                        xLeftChild.Parent = y;
                    else if(xRightChild != null)
                        xRightChild.Parent = y;
                    
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
        }

        private void ReplaceHeadWithTail()
        {
            // Determine whether Tail is the head, a left child, or right child
            NodeType tailNodeType = GetNodeParentRelation(Tail);
            if (tailNodeType == NodeType.Head)
            {
                Head = null;
                Tail = null;
            }
            else
            {
                // Determine new Tail
                Node<KeyT, ValueT> newTail = GetNodeNextToTail();
                // Have Tail Inherit Head's children
                if (Head.LeftChild != Tail)
                    Tail.LeftChild = Head.LeftChild;
                if (Head.RightChild != Tail)
                    Tail.RightChild = Head.RightChild;
                // Update inherited children's parent
                if (Tail.LeftChild != null)
                    Tail.LeftChild.Parent = Tail;
                if (Tail.RightChild != null)
                    Tail.RightChild.Parent = Tail;
                // Remove Tail from parent
                if (tailNodeType == NodeType.LeftChild)
                    Tail.Parent.LeftChild = null;
                else // Tail is right child
                    Tail.Parent.RightChild = null;
                // Done with current Head, make current Tail the new Head
                Head = Tail;
                // Update Tail
                Tail = newTail;
                // Remove new Head's parent
                Head.Parent = null;
            }
            // Update Count
            Count--;
        }

        private Node<KeyT, ValueT> GetNodeNextToTail()
        {
            Node<KeyT, ValueT> node = null;
            // Get Tail node type
            NodeType currentNodeType = GetNodeParentRelation(Tail);
            if (currentNodeType != NodeType.Head)
            {
                if (currentNodeType == NodeType.RightChild)
                    node = Tail.Parent.LeftChild;
                else // Tail is left child
                {
                    // Determine node type of Tail's Parent
                    currentNodeType = GetNodeParentRelation(Tail.Parent);
                    if (currentNodeType == NodeType.Head)
                        node = Tail;
                    else if (currentNodeType == NodeType.RightChild)
                        node = Tail.Parent.Parent.LeftChild.RightChild;
                    else // Tail.Parent is left child
                    {
                        Node<KeyT, ValueT> rightChildParent = GetClosestParentAs(NodeType.RightChild, Tail);
                        // If parent is the top of heap set node to rightmost child
                        if (rightChildParent == Head)
                            node = GetRightMostChild(rightChildParent);
                        else // Parent is not top of heap
                            node = GetRightMostChild(rightChildParent.Parent.LeftChild);
                    }
                }
            }

            return node;
        }

        //private Node<KeyT, ValueT> FindValue(ValueT value)
        //{
        //    head
        //}

        #endregion

        #region Private Properties

        private Node<KeyT, ValueT> Head { get; set; }
        private Node<KeyT, ValueT> Tail { get; set; }

        #endregion

        #region Public Properties

        public int Count { get; set; }

        #endregion
    }
}
