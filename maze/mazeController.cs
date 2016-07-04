using Common.DataTypes;
using Common.Imaging;
using Maze.DataTypes;

namespace Maze
{
    public class MazeController
    {
        #region Constructor

        /// <summary>
        /// Creates a new Maze Controller class.
        /// </summary>
        public MazeController()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Solves the given maze image.
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
            MazeSolution mazeSolution = MazeSolver.SolveMaze<Node>(graph);
            // Draw path on image
            mazeImage.DrawSolutionPath(mazeSolution);
            // Write solution to image
            ImageHelper.WriteToImage(mazeImage, solutionPath);
            // Determine if solution was found
            return mazeSolution.Result;
        }

        #endregion

        private MazeGraph ImageToGraph(string imagePath)
        {
            // Generate bitmap array from image
            BitmapArray bitmapArray = ImageHelper.ImageToBitmapArray(imagePath);
            // Create new maze image object
            MazeImage mazeImage = new MazeImage(bitmapArray);
            // Generate graph from image
            return GraphGenerator.CreateGraphFrom(mazeImage);
        }

        
    }
}
