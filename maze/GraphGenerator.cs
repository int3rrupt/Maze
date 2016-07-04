using Maze.DataTypes;
using Maze.Enums;

namespace Maze
{
    public static class GraphGenerator
    {
        #region Public Methods

        public static MazeGraph CreateGraphFrom(MazeImage mazeImage)
        {
            // TODO move these somewhere more appropriate
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
                    MazeNodeType nodeType = mazeImage.GetPixelType(x, y);
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

        #endregion
    }
}
