using Common.DataTypes;
using Common.DataTypes.Interfaces;
using Maze.DataTypes;
using System;
using System.Collections.Generic;

namespace Common.Algorithms
{
    /// <summary>
    /// A collection of search algorithms.
    /// </summary>
    public static class SearchAlgorithms
    {
        /// <summary>
        ///  The default movement cost.
        /// </summary>
        private static int MovementCost = 5;

        /// <summary>
        /// The A* pathfinding algorithm
        /// </summary>
        /// <param name="graph">A <see cref="MazeGraph"/>, representing a maze to be solved.</param>
        /// <returns>A <see cref="MazeSolution"/>, containing the solution to the maze.</returns>
        public static MazeSolution AStar<T>(MazeGraph graph) where T : IAStarNode, new()
        {
            // Initialize queues
            PriorityQueue<T> open = new PriorityQueue<T>();
            Dictionary<int, T> closed = new Dictionary<int, T>();

            // Create start node
            T newNode = new T();
            // Set values
            newNode.ID = graph.StartLocationID;
            newNode.G = 0;
            int priority = H(graph.StartLocationID, graph);
            // Enqueue start node
            open.Enqueue(priority, newNode);

            T currentNode = default(T);
            // Check for finish
            while (open.Peek != null && (currentNode = open.DequeueHighestPriority()).ID != graph.FinishLocationID)
            {
                // Add current node to closed
                closed.Add(currentNode.ID, currentNode);
                // Iterate through all of current node's neighbors
                foreach (int id in graph.GetNeighbors(currentNode.ID))
                {
                    T neighborInOpen = default(T);
                    T neighborInClosed = default(T);

                    int newCost = currentNode.G + MovementCost;

                    // Try to find neighbor in open or closed and compare newCost against neighbor's G value.
                    // Add to open if neighbor not found in open or closed
                    if ((neighborInOpen = open.Exists(id)) != null && newCost < neighborInOpen.G)
                    {
                        // Dequeue from open
                        neighborInOpen = open.Dequeue(neighborInOpen.ID);
                        // Update values
                        neighborInOpen.G = newCost;
                        neighborInOpen.Parent = currentNode;
                        priority = newCost + H(neighborInOpen.ID, graph);
                        // Requeue
                        open.Enqueue(priority, neighborInOpen);
                    }
                    else if (closed.TryGetValue(id, out neighborInClosed) && newCost < neighborInClosed.G)
                    {
                        // Dequeue from closed
                        closed.Remove(id);
                        // Update values
                        neighborInClosed.G = newCost;
                        neighborInClosed.Parent = currentNode;
                        priority = newCost + H(id, graph);
                        // Requeue in open queue
                        open.Enqueue(priority, neighborInClosed);
                    }
                    else if (neighborInOpen == null && neighborInClosed == null)
                    {
                        // Create new node
                        newNode = new T();
                        // Set values
                        newNode.ID = id;
                        newNode.G = newCost;
                        newNode.Parent = currentNode;
                        priority = newCost + H(id, graph);
                        // Enqueue into priority queue
                        open.Enqueue(priority, newNode);
                    }
                }
            }
            // Determine result
            bool result = currentNode.ID == graph.FinishLocationID;
            return new MazeSolution(result, currentNode);
        }

        /// <summary>
        /// Calculates the heuristic for the given node ID.
        /// </summary>
        /// <param name="nodeID">An <see cref="int"/>, the node ID for which to calculate the heuristic.</param>
        /// <param name="graph">A <see cref="MazeGraph"/>, the graph containing the maze.</param>
        /// <returns>An <see cref="int"/>, the heuristic calculated for the given node ID.</returns>
        private static int H(int nodeID, MazeGraph graph)
        {
            Tuple<int, int> xy = graph.GetXYFor(nodeID);
            // Check if exists
            if (xy != null)
                return (Math.Abs(xy.Item1 - graph.FinishLocationX) + Math.Abs(xy.Item2 - graph.FinishLocationY)) * MovementCost;

            return -1;
        }
    }
}
