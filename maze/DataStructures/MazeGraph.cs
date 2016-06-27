using System;
using System.Collections.Generic;

namespace Common.DataStructures
{
    public class MazeGraph
    {
        public MazeGraph(byte[,] graphArray, int startLocationX, int startLocationY, int finishLocationX, int finishLocationY)
        {
            // Initialize values
            Graph = graphArray;
            StartLocationX = startLocationX;
            StartLocationY = startLocationY;
            StartLocationID = ID(StartLocationX, StartLocationY);
            FinishLocationX = finishLocationX;
            FinishLocationY = finishLocationY;
            FinishLocationID = ID(FinishLocationX, FinishLocationY);
            Width = Graph.GetLength(1);
            Height = Graph.GetLength(0);
            MaxID = ID(Width - 1, Height - 1);
        }

        public List<int> GetNeighbors(int nodeID)
        {
            int y = Y(nodeID);
            if (IsValidID(nodeID))
                return GetNeighbors(X(y, nodeID), y);
            return null;
        }

        public List<int> GetNeighbors(int x, int y)
        {
            List<int> neighbors = new List<int>();
            // Verify point is within boundaries and not a wall
            if (IsValidPoint(x, y))
            {
                // North
                if (y - 1 >= 0 && Graph[y - 1, x] != 4)
                    neighbors.Add(ID(x, y - 1));
                // East
                if (x + 1 < Width && Graph[y, x + 1] != 4)
                    neighbors.Add(ID(x + 1, y));
                // South
                if (y + 1 < Height && Graph[y + 1, x] != 4)
                    neighbors.Add(ID(x, y + 1));
                // West
                if (x - 1 >= 0 && Graph[y, x - 1] != 4)
                    neighbors.Add(ID(x - 1, y));

                return neighbors;
            }
            return null;
        }

        public int GetIDFor(int x, int y)
        {
            if (IsValidPoint(x, y))
            {
                return ID(x, y);
            }
            return -1;
        }

        public Tuple<int, int> GetXYFor(int nodeID)
        {
            if (IsValidID(nodeID))
            {
                int y = Y(nodeID);
                return new Tuple<int, int>(X(y, nodeID), y);
            }
            return null;
        }

        private bool IsValidPoint(int x, int y)
        {
            // Verify point is within boundaries and not a wall
            if ((x >= 0 && x < Graph.GetLength(1) && y >= 0 && y < Graph.GetLength(0)) &&
                Graph[y, x] != 4)
                return true;

            return false;
        }

        private bool IsValidID(int nodeID)
        {
            if (nodeID >= 0 && nodeID <= MaxID)
                return true;

            return false;
        }

        private int ID(int x, int y)
        {
            return x + (y * Graph.GetLength(1));
        }

        private int Y(int nodeID)
        {
            return nodeID / Width;
        }

        private int X(int y, int nodeID)
        {
            return nodeID - (y * Width);
        }

        public int StartLocationX { get; set; }
        public int StartLocationY { get; set; }
        public int StartLocationID { get; set; }
        public int FinishLocationX { get; set; }
        public int FinishLocationY { get; set; }
        public int FinishLocationID { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        private byte[,] Graph { get; set; }
        private int MaxID { get; set; }
    }
}