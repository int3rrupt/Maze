namespace Common.DataTypes.Interfaces
{
    /// <summary>
    /// The interface for a Node. Defines properties common to all Nodes.
    /// </summary>
    public interface INode
    {
        /// <summary>
        /// The node ID.
        /// </summary>
        int ID { get; set; }
        /// <summary>
        /// The ID of the parent to the node.
        /// </summary>
        INode Parent { get; set; }
    }
}
