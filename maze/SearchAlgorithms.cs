using Common.DataStructures;
using Common.DataStructures.Interfaces;
using System;
using System.Collections.Generic;

namespace Common.Algorithms
{
    public static class SearchAlgorithms
    {
        private static int MovementCost = 10;

        public static INode AStar(MazeGraph graph)
        {
            PriorityQueue<Node> open = new PriorityQueue<Node>();
            Dictionary<int, Tuple<Node, int>> closed = new Dictionary<int, Tuple<Node, int>>();

            // Enqueue start node
            Node newNode = new Node();
            int priority = H(graph.StartLocationID, graph);
            newNode.ID = graph.StartLocationID;
            newNode.G = 0;
            open.Enqueue(priority, newNode);

            Node currentNode = null;

            int reopenCount = 0;
            // Check for finish
            while (open.Peek != null && (currentNode = open.DequeueHighestPriority()).ID != graph.FinishLocationID)
            {
                // Add current node to closed
                closed.Add(currentNode.ID, new Tuple<Node, int>((Node)currentNode.Parent, currentNode.G));
                // Iterate through all of current node's neighbors
                foreach (int id in graph.GetNeighbors(currentNode.ID))
                {
                    Node neighborInOpen;
                    Tuple<Node, int> neighborInClosed;
                    int newCost = currentNode.G + MovementCost;

                    if ((neighborInOpen = open.Exists(id)) != null && newCost < neighborInOpen.G)
                    {
                        neighborInOpen = open.Dequeue(neighborInOpen.ID);
                        neighborInOpen.G = newCost;
                        priority = newCost + H(neighborInOpen.ID, graph);
                        neighborInOpen.Parent = currentNode;
                        open.Enqueue(priority, neighborInOpen);
                    }
                    else if ((closed.TryGetValue(id, out neighborInClosed)) && newCost < neighborInClosed.Item2)
                    {
                        reopenCount++;
                        closed.Remove(id);
                        priority = newCost + H(id, graph);
                        open.Enqueue(priority, new Node(id, neighborInClosed.Item1, newCost));
                    }
                    else if (neighborInOpen == null && neighborInClosed == null)
                    {
                        newNode = new Node();
                        newNode.ID = id;
                        newNode.G = newCost;
                        priority = newCost + H(newNode.ID, graph);
                        newNode.Parent = currentNode;
                        open.Enqueue(priority, newNode);
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

        private static int H(int nodeID, MazeGraph graph)
        {
            Tuple<int, int> xy = graph.GetXYFor(nodeID);
            // Check if exists
            if (xy != null)
                return (Math.Abs(xy.Item1 - graph.FinishLocationX) + Math.Abs(xy.Item2 - graph.FinishLocationY)) * MovementCost;

            return -1;
        }

        /// <summary>
        /// Cost of parent plus movement cost to node
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="graph"></param>
        /// <returns></returns>
        //private static int G(INode node)
        //{
        //    if (node != null)
        //    {
        //        if (node.Parent == null)
        //            return 0;
        //        //return Math.Abs(graph.IdToX(nodeId) - graph.FinishLocationX) + Math.Abs(graph.IdToY(nodeId) - graph.FinishLocationY);
        //        return G(node.Parent) + MovementCost;
        //    }
        //    return -1;
        //}

        //private static int F (int nodeId, int movementCost, MazeGraph graph)
        //{
        //    return G(nodeId, graph) + H(nodeId, movementCost, graph);
        //}
    }
}
