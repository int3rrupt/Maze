using System;

namespace Maze
{
    public class Node<KeyT, ValueT> where KeyT: IComparable
    {
        public Node(KeyT key, ValueT value)
        {
            Key = key;
            Value = value;
        }

        public Node<KeyT, ValueT> Parent { get; set; }
        public Node<KeyT, ValueT> LeftChild { get; set; }
        public Node<KeyT, ValueT> RightChild { get; set; }
        public KeyT Key { get; set; }
        public ValueT Value { get; set; }
    }
}
