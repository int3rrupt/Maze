using Common.DataStructures.Interfaces;

namespace Common.DataStructures
{
    public class AStarNode : IAStarNode
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

        /// <summary>
        /// The node's ID
        /// </summary>
        public int Key { get; set; }
        /// <summary>
        /// The node's priority
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// The node's parent
        /// </summary>
        public INode Parent { get; set; }

        public int G { get; set; }

        public int H { get; set; }

        #endregion
    }
}
