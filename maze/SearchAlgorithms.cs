using Common.DataStructures;
using Common.DataStructures.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Common.Algorithms
{
    public static class SearchAlgorithms
    {
        public static INode AStar(MazeGraph graph)
        {
            int nodesChecked = 0;
            int movementCost = 10;
            PriorityQueue<AStarNode> open = new PriorityQueue<AStarNode>();
            Dictionary<int, AStarNode> closed = new Dictionary<int, AStarNode>();

            // Enqueue start node
            open.Enqueue(NewAStarNode(graph.GetIDFor(graph.StartLocationX, graph.StartLocationY), movementCost, graph));

            AStarNode currentNode = null;
            // Check for finish
            while (open.Peek != null && (currentNode = open.DequeueHighestPriority()).Key != graph.FinishLocationID)
            {
                // Add current node to closed
                closed.Add(currentNode.Key, currentNode);
                // Iterate through all of current node's neighbors
                foreach (int id in graph.GetNeighbors(currentNode.Key))
                {
                    int neighborOpenIndex = -1;
                    AStarNode neighbor;
                    int cost = currentNode.G + movementCost;

                    if ((neighborOpenIndex = open.FindNodeIndexWithKey(id)) > -1 && cost < open.PeekAt(neighborOpenIndex).G)
                    {
                        neighbor = open.DequeueAt(neighborOpenIndex);
                        neighbor.G = cost;
                        neighbor.Value = neighbor.G + neighbor.H;
                        neighbor.Parent = currentNode;
                        open.Enqueue(neighbor);
                    }
                    else if ((closed.TryGetValue(id, out neighbor)) && cost < neighbor.G)
                    //else if ((neighbor = closed.Find(n => n.Key == id)) != null && cost < neighbor.G)
                    {
                        closed.Remove(neighbor.Key);
                        //closed.Remove(neighbor);
                        neighbor.G = cost;
                        neighbor.Value = neighbor.G + neighbor.H;
                        neighbor.Parent = currentNode;
                        open.Enqueue(neighbor);
                    }
                    else if (neighborOpenIndex == -1 && neighbor == null)
                    {
                        AStarNode newNode = NewAStarNode(id, movementCost, graph);
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

        private static int H(int nodeID, int movementCost, MazeGraph graph)
        {
            // Check if exists
            if (graph.GetNeighbors(nodeID) != null)
            {
                Tuple<int, int> xy = graph.GetXYFor(nodeID);
                return (Math.Abs(xy.Item1 - graph.FinishLocationX) + Math.Abs(xy.Item2 - graph.FinishLocationY)) * movementCost;
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
            if (nodeId == graph.StartLocationID)
                return 0;
            //return Math.Abs(graph.IdToX(nodeId) - graph.FinishLocationX) + Math.Abs(graph.IdToY(nodeId) - graph.FinishLocationY);
            return G(nodeId, graph);
        }

        private static int F (int nodeId, int movementCost, MazeGraph graph)
        {
            return G(nodeId, graph) + H(nodeId, movementCost, graph);
        }

        private static AStarNode NewAStarNode(int id, int movementCost, MazeGraph graph)
        {
            int g = 0;
            int h = H(id, movementCost, graph);
            int f = g + h;

            return new AStarNode(id, f, g, h, null);
        }

        private static int FindNodeIndexWithKey(int key, List<AStarNode> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Key == key)
                    return i;
            }

            return -1;
        }
    }
}
