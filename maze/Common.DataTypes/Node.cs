using Common.DataTypes.Interfaces;

namespace Common.DataTypes
{
    public class Node : IAStarNode
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
