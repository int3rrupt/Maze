using Common.DataStructures;
using Common.Imaging;
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
            // Generate bitmap array from image
            BitmapArray bitmapArray = ImageHelper.ImageToBitmapArray(imagePath);
            // Create new maze image object
            MazeImage mazeImage = new MazeImage(bitmapArray);
            // Generate graph from image
            MazeGraph graph = GraphGenerator.CreateGraphFrom(mazeImage);
            // Attempt to solve the maze
            AStarNode mazeSolution = MazeSolver.SolveMaze<AStarNode>(graph);
            // Draw path on image
            mazeImage.DrawPath(mazeSolution);
            // Write solution to image
            ImageHelper.WriteToImage(mazeImage, solutionPath);
            // Determine if solution was found
            if (mazeSolution.Key == graph.FinishId)
                return true;

            return false;
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
        }

        public void BitmapToGraph(Bitmap bitmap)
        {
            //
        }

        #endregion

        
    }
}
