using System;

/// <summary>
/// Defines properties common to all nodes.
/// </summary>
namespace DataStructures.Interfaces
{
    public interface INode<T> where T : IComparable
    {
        /// <summary>
        /// The node key. An ID.
        /// </summary>
        T Key { get; set; }
        /// <summary>
        /// The value of the node.
        /// </summary>
        T Value { get; set; }
        /// <summary>
        /// The parent to the node.
        /// </summary>
        INode<T> Parent { get; set; }
    }
}
