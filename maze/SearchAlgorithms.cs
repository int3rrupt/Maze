using Common.DataStructures;
using Common.DataStructures.Interfaces;
using System;
using System.Collections.Generic;

namespace Common.Algorithms
{
    public static class SearchAlgorithms
    {
        public static INode AStar(MazeGraph graph)
        {
            int movementCost = 1;
            PriorityQueue<AStarNode> open = new PriorityQueue<AStarNode>();
            List<AStarNode> closed = new List<AStarNode>();

            // Enqueue start node
            open.Enqueue(NewAStarNode(graph.StartId, graph));

            AStarNode currentNode = null;
            // Check for finish
            while (open.Peek != null && (currentNode = open.DequeueHighestPriority()).Key != graph.FinishId)
            {
                closed.Add(currentNode);

                foreach (int id in graph.GetNeighbors(currentNode.Key))
                {
                    int cost = currentNode.G + movementCost;
                    AStarNode neighbor;
                    if ((neighbor = open.Exists(id)) != null && cost < neighbor.G)
                    {
                        neighbor = open.Dequeue(id);
                        closed.Add(neighbor);
                    }
                    else if ((neighbor = closed.Find(n => n.Key == id)) != null && cost < neighbor.G)
                    {
                        closed.Remove(neighbor);
                        open.Enqueue(neighbor);
                    }
                    else if (open.Exists(id) == null && !closed.Exists(n => n.Key == id))
                    {
                        AStarNode newNode = NewAStarNode(id, graph);
                        newNode.G = cost;
                        newNode.Value = newNode.G + newNode.H;
                        newNode.Parent = currentNode;
                        open.Enqueue(newNode);
                    }
                }
            }

            return currentNode;


            //OPEN = priority queue containing START
            //CLOSED = empty set
            //while lowest rank in OPEN is not the GOAL:
            //  current = remove lowest rank item from OPEN
            //  add current to CLOSED
            //  for neighbors of current:
            //    cost = g(current) + movementcost(current, neighbor)
            //    if neighbor in OPEN and cost less than g(neighbor):
            //      remove neighbor from OPEN, because new path is better
            //    if neighbor in CLOSED and cost less than g(neighbor): ⁽²⁾
            //      remove neighbor from CLOSED
            //    if neighbor not in OPEN and neighbor not in CLOSED:
            //            set g(neighbor)to cost
            //            add neighbor to OPEN
            //            set priority queue rank to g(neighbor) + h(neighbor)
            //            set neighbor's parent to current

        }

        private static int H(int nodeId, MazeGraph graph)
        {
            // Check if exists
            if (graph.GetNeighbors(nodeId) != null)
            {
                return Math.Abs(graph.IdToX(nodeId) - graph.FinishLocationX) + Math.Abs(graph.IdToY(nodeId) - graph.FinishLocationY);
            }
            return -1;
        }

        /// <summary>
        /// Cost of parent plus movement cost to node
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="graph"></param>
        /// <returns></returns>
        private static int G(int nodeId, MazeGraph graph)
        {
            // Check if exists
            if (graph.GetNeighbors(nodeId) != null)
            {
                if (nodeId == graph.StartId)
                    return 0;
                //return Math.Abs(graph.IdToX(nodeId) - graph.FinishLocationX) + Math.Abs(graph.IdToY(nodeId) - graph.FinishLocationY);
                return G(nodeId, graph);
            }
            return -1;
        }

        private static int F (int nodeId, MazeGraph graph)
        {
            return G(nodeId, graph) + H(nodeId, graph);
        }

        private static AStarNode NewAStarNode(int id, MazeGraph graph)
        {
            int g = 0;
            int h = H(id, graph);
            int f = g + h;

            return new AStarNode(id, f, g, h, null);
        }
    }
}
