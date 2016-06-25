using Common.DataStructures.Interfaces;

namespace Common.DataStructures
{
    public class AStarNode : INode
    {
        #region Constructor

        public AStarNode(int key, int value, int g, int h, AStarNode parent)
        {
            Key = key;
            Value = value;
            G = g;
            H = h;
            Parent = parent;
        }

        #endregion

        #region Public Properties

        public int Key { get; set; }

        public int Value { get; set; }

        public INode Parent { get; set; }

        public int G { get; set; }

        public int H { get; set; }

        #endregion
    }
}
