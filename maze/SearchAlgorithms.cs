using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    public static class SearchAlgorithms
    {
        public static void AStar(Graph graph)
        {
            List<int> open = new List<int>();
            List<int> closed = new List<int>();

            open.Add(graph.StartId);

            // Check for finish
            while (open.Count != 0 && open[0] != graph.FinishId)
            {
                //currentNode = open.Re
            }


            //while lowest rank in OPEN is not the GOAL:
            //current = remove lowest rank item from OPEN
            //add current to CLOSED
            //for neighbors of current:
            //  cost = g(current) + movementcost(current, neighbor)
            //  if neighbor in OPEN and cost less than g(neighbor):
            //    remove neighbor from OPEN, because new path is better
            //  if neighbor in CLOSED and cost less than g(neighbor): ⁽²⁾
            //    remove neighbor from CLOSED
            //  if neighbor not in OPEN and neighbor not in CLOSED:
            //          set g(neighbor)to cost
            //          add neighbor to OPEN
            //          set priority queue rank to g(neighbor) + h(neighbor)
            //          set neighbor's parent to current

        }

        private static int Heuristic(int nodeId, Graph graph)
        {
            // Check if exists
            if (graph.GetNeighbors(nodeId) != null)
            {
                return Math.Abs(graph.IdToX(nodeId) - graph.FinishLocationX) + Math.Abs(graph.IdToY(nodeId) - graph.FinishLocationY);
            }
            return -1;
        }

        private static int Cost(int nodeId, Graph graph)
        {
            // Check if exists
            if (graph.GetNeighbors(nodeId) != null)
            {
                if (nodeId == graph.StartId)
                    return 0;
                //return Math.Abs(graph.IdToX(nodeId) - graph.FinishLocationX) + Math.Abs(graph.IdToY(nodeId) - graph.FinishLocationY);
                return Cost(nodeId, graph);
            }
            return -1;
        }

        private static int F (int nodeId, Graph graph)
        {
            return Cost(nodeId, graph) + Heuristic(nodeId, graph);
        }
    }
}
