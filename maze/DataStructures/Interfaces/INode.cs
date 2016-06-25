/// <summary>
/// Defines properties common to all nodes.
/// </summary>
namespace Common.DataStructures.Interfaces
{
    public interface INode
    {
        /// <summary>
        /// The node key. An ID.
        /// </summary>
        int Key { get; set; }
        /// <summary>
        /// The value of the node.
        /// </summary>
        int Value { get; set; }
        /// <summary>
        /// The parent to the node.
        /// </summary>
        INode Parent { get; set; }
    }
}
