using Common.DataStructures.Interfaces;

namespace Common.DataStructures
{
    public class AStarNode : IAStarNode
    {
        #region Constructor

        public AStarNode()
        {
        }

        public AStarNode(int key, int g, int h, AStarNode parent)
        {
            ID = key;
            G = g;
            //H = h;
            Parent = parent;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The node's ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The node's priority
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// The node's parent
        /// </summary>
        public INode Parent { get; set; }

        public int G { get; set; }

        //public int H { get; set; }

        #endregion
    }
}
