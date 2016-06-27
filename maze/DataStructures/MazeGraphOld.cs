using System.Collections.Generic;

namespace Common.DataStructures
{
    public class MazeGraphOld
    {
        public MazeGraphOld(Dictionary<int, List<int>> graphAsDictionary, int width, int startLocationX, int startLocationY, int finishLocationX, int finishLocationY)
        {
            // Initialize values
            Dictionary = graphAsDictionary;
            Width = width;
            StartLocationX = startLocationX;
            StartLocationY = startLocationY;
            FinishLocationX = finishLocationX;
            FinishLocationY = finishLocationY;
        }

        public List<int> GetNeighbors(int x, int y)
        {
            return GetNeighbors(LocationToId(x, y));
        }

        public List<int> GetNeighbors(int nodeId)
        {
            List<int> value;
            if (Dictionary.TryGetValue(nodeId, out value))
                return value;
            return null;
        }

        public bool NodeExists(int nodeId)
        {
            List<int> temp;
            if (Dictionary.TryGetValue(nodeId, out temp))
                return true;
            return false;
        }

        public int IdToX(int nodeId)
        {
            return nodeId - Width * (nodeId / Width);
        }

        public int IdToY(int nodeId)
        {
            return nodeId / Width;
        }

        public int LocationToId(int x, int y)
        {
            return x + (Width * y);
        }

        public int StartLocationX { get; set; }
        public int StartLocationY { get; set; }
        public int StartId { get { return LocationToId(StartLocationX, StartLocationY); } }
        public int FinishLocationX { get; set; }
        public int FinishLocationY { get; set; }
        public int FinishId { get { return LocationToId(FinishLocationX, FinishLocationY); } }
        public int Width { get; set; }
        public int Height { get; set; }

        private Dictionary<int, List<int>> Dictionary { get; set; }
    }
}