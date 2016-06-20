using System.Collections.Generic;

namespace Maze
{
    public class Graph
    {
        public Graph(MazeImage mazeImage)
        {
            Width = mazeImage.Width;
            Height = mazeImage.Height;
            Dict = new Dictionary<int, List<int>>();
            GenerateGraph(mazeImage);
        }

        private void GenerateGraph(MazeImage mazeImage)
        {
            // Clear start and finish locations
            StartLocationX = -1;
            StartLocationY = -1;
            FinishLocationX = -1;
            FinishLocationY = -1;

            // ID = x + width(y) | x = ID - width(y) | y = ID/width (int div)
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    // Check current node type
                    MazeNodeType nodeType = mazeImage.GetPixel(x, y);
                    if (nodeType != MazeNodeType.Wall)
                    {
                        // Check for neighbors
                        List<int> neighbors = new List<int>();
                        for (int i = 0; i < 4; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    {
                                        if (y > 0 && mazeImage.GetPixel(x, y - 1) != MazeNodeType.Wall)
                                            neighbors.Add(x + (Width * (y - 1)));
                                        break;
                                    }
                                case 1:
                                    {
                                        if (y + 1 < Height && mazeImage.GetPixel(x, y + 1) != MazeNodeType.Wall)
                                            neighbors.Add(x + (Width * (y + 1)));
                                        break;
                                    }
                                case 2:
                                    {
                                        if (x > 0 && mazeImage.GetPixel(x - 1, y) != MazeNodeType.Wall)
                                            neighbors.Add((x - 1) + (Width * y));
                                        break;
                                    }
                                case 3:
                                    {
                                        if (x + 1 < Width && mazeImage.GetPixel(x + 1, y) != MazeNodeType.Wall)
                                            neighbors.Add((x + 1) + (Width * y));
                                        break;
                                    }
                            }
                        }
                        // Get current node id
                        int id = x + (Width * y);
                        // Add node
                        Dict.Add(id, neighbors);
                        // Update locations
                        if (nodeType == MazeNodeType.Start)
                        {
                            StartLocationX = x;
                            StartLocationY = y;
                        }
                        if (nodeType == MazeNodeType.Finish)
                        {
                            FinishLocationX = x;
                            FinishLocationY = y;
                        }
                    }
                } // End inner for loop (x)
            }
        }

        public List<int> GetNeighbors(int nodeId)
        {
            List<int> value;
            if (Dict.TryGetValue(nodeId, out value))
                return value;
            return null;
        }

        public bool NodeExists(int nodeId)
        {
            List<int> temp;
            if (Dict.TryGetValue(nodeId, out temp))
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

        private Dictionary<int, List<int>> Dict { get; set; }
    }
}