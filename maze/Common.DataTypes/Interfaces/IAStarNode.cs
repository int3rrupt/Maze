namespace Common.DataTypes.Interfaces
{
    /// <summary>
    /// The interface for an A* Node. Defines properties common to all A* Nodes.
    /// </summary>
    public interface IAStarNode : INode
    {
        /// <summary>
        /// The cost to this node from the start location.
        /// </summary>
        int G { get; set; }

        /// <summary>
        /// The heuristic value from this node to the finish location.
        /// </summary>
        //int H { get; set; }
    }
}
