using Common.DataStructures;
using Maze.Enums;
using System;
using System.Collections.Generic;

namespace Maze
{
    public static class GraphGenerator
    {
        #region Public Methods

        public static MazeGraph CreateGraphFrom(MazeImage mazeImage)
        {
            byte start = 0;
            byte finish = 1;
            byte solutionPath = 2;
            byte path = 3;
            byte wall = 4;

            int startLocationX = -1;
            int startLocationY = -1;
            int finishLocationX = -1;
            int finishLocationY = -1;


            byte[,] graphArray = new byte[mazeImage.Height, mazeImage.Width];

            for (int y = 0; y < mazeImage.Height; y++)
            {
                for (int x = 0; x < mazeImage.Width; x++)
                {
                    // Update this to some default
                    byte nodeTypeByte = 4;
                    // Check current node type
                    MazeNodeType nodeType = mazeImage.GetPixel(x, y);
                    switch (nodeType)
                    {
                        case MazeNodeType.Start:
                            {
                                nodeTypeByte = start;
                                startLocationX = x;
                                startLocationY = y;
                                break;
                            }
                        case MazeNodeType.Finish:
                            {
                                nodeTypeByte = finish;
                                finishLocationX = x;
                                finishLocationY = y;
                                break;
                            }
                        case MazeNodeType.Path:
                            {
                                nodeTypeByte = path;
                                break;
                            }
                        case MazeNodeType.Wall:
                            {
                                nodeTypeByte = wall;
                                break;
                            }
                    }

                    graphArray[y, x] = nodeTypeByte;
                } // End inner for loop (x)
            }

            return new MazeGraph(graphArray, startLocationX, startLocationY, finishLocationX, finishLocationY);
        }

        //public static MazeGraph CreateGraphFrom(MazeImage mazeImage)
        //{
        //    // Create new dictionary to represent graph
        //    Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
        //    // Set initial start and finish locations
        //    int startLocationX = -1;
        //    int startLocationY = -1;
        //    int finishLocationX = -1;
        //    int finishLocationY = -1;


        //    // NOTES
        //    // ID of pixel = x + width(y)
        //    // x coordinate of pixel = ID - width(y)
        //    // y coordinate of pixel = ID/width (int div)

        //    // Iterate through entire image
        //    for (int y = 0; y < mazeImage.Height; y++)
        //    {
        //        for (int x = 0; x < mazeImage.Width; x++)
        //        {
        //            // Check current node type
        //            MazeNodeType nodeType = mazeImage.GetPixel(x, y);
        //            if (nodeType != MazeNodeType.Wall)
        //            {
        //                // If this is a start node and no start location has been set
        //                if (nodeType == MazeNodeType.Start && startLocationX != -1)
        //                {
        //                    // Check whether this node has access to a path

        //                }
        //                // Check for neighbors
        //                List<int> neighbors = new List<int>();
        //                for (int i = 0; i < 4; i++)
        //                {
        //                    switch (i)
        //                    {
        //                        case 0:
        //                            {
        //                                if (y > 0 && mazeImage.GetPixel(x, y - 1) != MazeNodeType.Wall)
        //                                    neighbors.Add(x + (mazeImage.Width * (y - 1)));
        //                                break;
        //                            }
        //                        case 1:
        //                            {
        //                                if (y + 1 < mazeImage.Height && mazeImage.GetPixel(x, y + 1) != MazeNodeType.Wall)
        //                                    neighbors.Add(x + (mazeImage.Width * (y + 1)));
        //                                break;
        //                            }
        //                        case 2:
        //                            {
        //                                if (x > 0 && mazeImage.GetPixel(x - 1, y) != MazeNodeType.Wall)
        //                                    neighbors.Add((x - 1) + (mazeImage.Width * y));
        //                                break;
        //                            }
        //                        case 3:
        //                            {
        //                                if (x + 1 < mazeImage.Width && mazeImage.GetPixel(x + 1, y) != MazeNodeType.Wall)
        //                                    neighbors.Add((x + 1) + (mazeImage.Width * y));
        //                                break;
        //                            }
        //                    }
        //                }
        //                // Get current node id
        //                int id = x + (mazeImage.Width * y);
        //                // Add node
        //                dictionary.Add(id, neighbors);
        //                // Update locations
        //                if (nodeType == MazeNodeType.Start)
        //                {
        //                    startLocationX = x;
        //                    startLocationY = y;
        //                }
        //                if (nodeType == MazeNodeType.Finish)
        //                {
        //                    finishLocationX = x;
        //                    finishLocationY = y;
        //                }
        //            }
        //        } // End inner for loop (x)
        //    }

        //    // Create new graph
        //    return new MazeGraph(dictionary, mazeImage.Width, startLocationX, startLocationY, finishLocationX, finishLocationY);
        //}

        //private bool NodeCanAccessPath(int x, int y, MazeGraph graph)
        //{
        //    foreach (int neighborId in graph.GetNeighbors(x, y))
        //    {
        //        graph.
        //    }
        //}

        private static void GetBorder(int startX, int startY)
        {
            /* current = start
             * whil something
             * // Check neighbors
             * if current.top != null && current.top include
             *      add to collection
             *      current = current.top
             *      continue
             * if current.right != null && current.top include
             *      add to collection
             *      current = current.right
             *      continue
             * if current.bottom != null && current.bottom include
             *      add to collection
             *      current = current.bottom
             *      continue
             * if current.left != null && current.left include
             *      add to collection
             *      current = current.left
             *      continue
            */
        }

        #endregion
    }
}
