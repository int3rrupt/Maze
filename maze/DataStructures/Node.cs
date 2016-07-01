using Common.DataStructures.Interfaces;

namespace Common.DataStructures
{
    public class Node : INode
    {
        public Node()
        {
        }

        public Node(int id, INode parent, int g)
        {
            ID = id;
            Parent = parent;
            G = g;
        }

        public int ID { get; set; }

        public INode Parent { get; set; }

        public int G { get; set; }
    }
}
