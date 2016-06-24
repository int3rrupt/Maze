using System;

namespace Maze
{
    public class OldNode<KeyT, ValueT> where KeyT: IComparable
    {
        public OldNode(KeyT key, ValueT value)
        {
            Key = key;
            Value = value;
        }

        public OldNode<KeyT, ValueT> Parent { get; set; }
        public OldNode<KeyT, ValueT> LeftChild { get; set; }
        public OldNode<KeyT, ValueT> RightChild { get; set; }
        public KeyT Key { get; set; }
        public ValueT Value { get; set; }
    }
}
