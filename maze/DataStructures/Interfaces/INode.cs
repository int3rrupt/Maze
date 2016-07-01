/// <summary>
/// Defines properties common to all nodes.
/// </summary>
namespace Common.DataStructures.Interfaces
{
    public interface INode
    {
        /// <summary>
        /// The node ID.
        /// </summary>
        int ID { get; set; }
        /// <summary>
        /// The parent to the node.
        /// </summary>
        INode Parent { get; set; }
    }
}
