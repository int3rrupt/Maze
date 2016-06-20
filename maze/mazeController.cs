using Maze.Imaging;
using System.Drawing;

namespace Maze
{
    public class MazeController
    {
        #region Constructor

        /// <summary>
        /// Creates a new Maze Controller class using default values.
        /// </summary>
        public MazeController()
        {
        }

        public MazeController(string imagePath, Color startColor, Color finishColor, Color path, Color floor, Color wall)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Solves the given maze image using default maze properties.
        /// Outputs a copy of the image with the computed solution.
        /// </summary>
        /// <param name="imagePath">A <see cref="string"/>, the path to the maze image.</param>
        /// <param name="solutionPath">A <see cref="string"/>, the path to the solution image.</param>
        public bool SolveMaze(string imagePath, string solutionPath)
        {
            // Create new maze image structure
            MazeImage mazeImage = new MazeImage(imagePath);

            Graph graph = new Graph(mazeImage);
            MazeSolver.SolveMaze(graph);
            // Set conversion paramters
            //BitmapConversionParams<char> bmpConvParams = new BitmapConversionParams<char>();
            //bmpConvParams.AddParameter(mazeImage.StartColor, '@');
            //bmpConvParams.AddParameter(mazeImage.FinishColor, 'X');
            //bmpConvParams.AddParameter(mazeImage.FloorColor, ' ');
            //bmpConvParams.AddParameter(mazeImage.WallColor, '*');
            //// 
            //BitmapArray bmpArr = ImageHelper<char>.ImageToMaze(imagePath);
            //Maze maze = new Maze(bmpArr, )
            // Convert bitmap to Maze
            //charArr = ImageHelper.BitmapToCharArray(mazeImage, bmpConvParams);


            //using (Bitmap solutionImage = MazeSolver.SolveMaze(mazeImage, this.StartColor, this.FinishColor, this.WallColor))
            //{



            //    FileHelper.WriteImage(solutionImage, solutionPath);
            //}

            return false;
        }

        public void BitmapToGraph(Bitmap bitmap)
        {
            //
        }

        #endregion

        
    }
}
