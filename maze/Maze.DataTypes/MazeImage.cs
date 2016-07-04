using Common.DataTypes;
using Common.DataTypes.Interfaces;
using Common.Imaging;
using Maze.Enums;
using System.Drawing.Imaging;

namespace Maze.DataTypes
{
    /// <summary>
    /// A collection of properties used to represent a maze
    /// </summary>
    public class MazeImage
    {
        #region Constructors

        /// <summary>
        /// Creates a new MazeImage class with default values.
        /// </summary>
        /// <param name="bitmapArray">A <see cref="BitmapArray"/>, containing the image information.</param>
        public MazeImage(BitmapArray bitmapArray)
        {
            // Default values
            CustomColor red = new CustomColor(255, 255, 0, 0);
            CustomColor blue = new CustomColor(255, 0, 0, 255);
            CustomColor green = new CustomColor(255, 0, 255, 0);
            CustomColor white = new CustomColor(255, 255, 255, 255);
            CustomColor black = new CustomColor(255, 0, 0, 0);

            Initialize(bitmapArray, 90, red, blue, green, white, black);
        }

        /// <summary>
        /// Creates a new MazeImage structure using the provided values.
        /// </summary>
        /// <param name="bitmapArray">A <see cref="BitmapArray"/>, containing the image information.</param>
        /// <param name="deviationAmount">An <see cref="int"/>, the amount of deviation allowed for any given color.</param>
        /// <param name="startColor">A <see cref="CustomColor"/>, the color indicating the start on the image.</param>
        /// <param name="finishColor">A <see cref="CustomColor"/>, the color indicating the finish on the image.</param>
        /// <param name="solutionPathColor">A <see cref="CustomColor"/>, the color indicating the solution on the image.</param>
        /// <param name="pathColor">A <see cref="CustomColor"/>, the color indicating a path on the image.</param>
        /// <param name="wallColor">A <see cref="CustomColor"/>, the color indicating a wall on the image.</param>
        public MazeImage(
            BitmapArray bitmapArray,
            int deviationAmount,
            CustomColor startColor,
            CustomColor finishColor,
            CustomColor solutionPathColor,
            CustomColor pathColor,
            CustomColor wallColor)
        {
            Initialize(bitmapArray, deviationAmount, startColor, finishColor, solutionPathColor, pathColor, wallColor);
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes the maze image class using the provided values.
        /// </summary>
        /// <param name="bitmapArray">A <see cref="BitmapArray"/>, containing the image information.</param>
        /// <param name="deviationAmount">An <see cref="int"/>, the amount of deviation allowed for any given color.</param>
        /// <param name="startColor">A <see cref="CustomColor"/>, the color indicating the start on the image.</param>
        /// <param name="finishColor">A <see cref="CustomColor"/>, the color indicating the finish on the image.</param>
        /// <param name="solutionPathColor">A <see cref="CustomColor"/>, the color indicating the solution on the image.</param>
        /// <param name="pathColor">A <see cref="CustomColor"/>, the color indicating a path on the image.</param>
        /// <param name="wallColor">A <see cref="CustomColor"/>, the color indicating a wall on the image.</param>
        private void Initialize(
            BitmapArray bitmapArray,
            int deviationAmount,
            CustomColor startColor,
            CustomColor finishColor,
            CustomColor solutionPathColor,
            CustomColor pathColor,
            CustomColor wallColor)
        {
            BitmapArr = bitmapArray;
            DeviationAmount = deviationAmount;
            StartColor = startColor;
            FinishColor = finishColor;
            SolutionPathColor = solutionPathColor;
            PathColor = pathColor;
            WallColor = wallColor;
            // Store ARGB values
            StartColorArgb = StartColor.ToArgb();
            FinishColorArgb = FinishColor.ToArgb();
            SolutionPathColorArgb = SolutionPathColor.ToArgb();
            PathColorArgb = PathColor.ToArgb();
            WallColorArgb = WallColor.ToArgb();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the <see cref="MazeNodeType"/> at the given x-y location.
        /// </summary>
        /// <param name="x">An <see cref="int"/>, the x location.</param>
        /// <param name="y">An <see cref="int"/>, the y location.</param>
        /// <returns>A <see cref="MazeNodeType"/>, indicating the maze node type of the given location.</returns>
        public MazeNodeType GetPixelType(int x, int y)
        {
            // Get ARGB value at given point for comparison
            int argbKey = BitmapArr.GetArgbAt(x, y);
            // Compare to known ARGB values
            if (argbKey == StartColorArgb)
                return MazeNodeType.Start;
            if (argbKey == FinishColorArgb)
                return MazeNodeType.Finish;
            if (argbKey == PathColorArgb)
                return MazeNodeType.Path;
            if (argbKey == WallColorArgb)
                return MazeNodeType.Wall;
            // If no known value is found, check against known values
            // taking deviation amount into account
            return GetPixelTypeWithDeviation(x, y);
        }

        /// <summary>
        /// Draws the solution path given a <see cref="MazeSolution"/>
        /// </summary>
        /// <param name="solution">A <see cref="MazeSolution"/>, the solution to the <see cref="MazeImage"/>.</param>
        public void DrawSolutionPath(MazeSolution solution)
        {
            INode currentNode = solution.LastNode;
            // Iterate through the linked list
            while (currentNode != null)
            {
                // Get x-y
                int x = IndexToX(currentNode.ID);
                int y = IndexToY(currentNode.ID);
                // Print the solution path onto the image
                BitmapArr.UpdateArgbAt(x, y, SolutionPathColor.A, SolutionPathColor.R, SolutionPathColor.G, SolutionPathColor.B);
                // Update current node
                currentNode = currentNode.Parent;
            }
        }

        /// <summary>
        /// Gets an array of <see cref="byte"/> representing the maze image.
        /// </summary>
        /// <returns>An array of <see cref="byte"/> representing the maze image.</returns>
        public byte[] ToByteArray()
        {
            return BitmapArr.ByteArr;
        }

        /// <summary>
        /// Gets the x location of the given pixel using the given pixel index or ID.
        /// </summary>
        /// <param name="pixelIndex">An <see cref="int"/>, the pixel index or ID.</param>
        /// <returns>An <see cref="int"/>, the x location of the pixel.</returns>
        public int IndexToX(int pixelIndex)
        {
            return pixelIndex - Width * (pixelIndex / Width);
        }

        /// <summary>
        /// Gets the y location of the given pixel using the given pixel index or ID.
        /// </summary>
        /// <param name="pixelIndex">An <see cref="int"/>, the pixel index or ID.</param>
        /// <returns>An <see cref="int"/>, the y location of the pixel.</returns>
        public int IndexToY(int pixelIndex)
        {
            return pixelIndex / Width;
        }

        /// <summary>
        /// Gets a pixel's index or ID using the given x-y location.
        /// </summary>
        /// <param name="x">An <see cref="int"/>, the x location.</param>
        /// <param name="y">An <see cref="int"/>, the y location.</param>
        /// <returns>An <see cref="int"/>, the pixel index or ID.</returns>
        public int LocationToIndex(int x, int y)
        {
            return x + (Width * y);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the <see cref="MazeNodeType"/> at the given x-y location taking deviation amount
        /// into account.
        /// </summary>
        /// <param name="x">An <see cref="int"/>, the x location.</param>
        /// <param name="y">An <see cref="int"/>, the y location.</param>
        /// <returns>A <see cref="MazeNodeType"/>, indicating the maze node type of the given location.</returns>
        private MazeNodeType GetPixelTypeWithDeviation(int x, int y)
        {
            // Get the color at the given point for comparison
            CustomColor color = BitmapArr.GetColorAt(x, y);
            // Compare to known values taking deviation into account
            if (StartColor.R - DeviationAmount <= color.R && color.R <= StartColor.R + DeviationAmount &&
                StartColor.G - DeviationAmount <= color.G && color.G <= StartColor.G + DeviationAmount &&
                StartColor.B - DeviationAmount <= color.B && color.B <= StartColor.B + DeviationAmount)
                return MazeNodeType.Start;
            if (FinishColor.R - DeviationAmount <= color.R && color.R <= FinishColor.R + DeviationAmount &&
                FinishColor.G - DeviationAmount <= color.G && color.G <= FinishColor.G + DeviationAmount &&
                FinishColor.B - DeviationAmount <= color.B && color.B <= FinishColor.B + DeviationAmount)
                return MazeNodeType.Finish;
            if (PathColor.R - DeviationAmount <= color.R && color.R <= PathColor.R + DeviationAmount &&
                PathColor.G - DeviationAmount <= color.G && color.G <= PathColor.G + DeviationAmount &&
                PathColor.B - DeviationAmount <= color.B && color.B <= PathColor.B + DeviationAmount)
                return MazeNodeType.Path;
            if (WallColor.R - DeviationAmount <= color.R && color.R <= WallColor.R + DeviationAmount &&
                WallColor.G - DeviationAmount <= color.G && color.G <= WallColor.G + DeviationAmount &&
                WallColor.B - DeviationAmount <= color.B && color.B <= WallColor.B + DeviationAmount)
                return MazeNodeType.Wall;
            // If no match is found, return default
            return MazeNodeType.Path;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The starting color
        /// </summary>
        public CustomColor StartColor { get; set; }
        public int StartColorArgb { get; set; }
        /// <summary>
        /// The finishing color
        /// </summary>
        public CustomColor FinishColor { get; set; }
        public int FinishColorArgb { get; set; }
        /// <summary>
        /// The path color
        /// </summary>
        public CustomColor SolutionPathColor{ get; set; }
        public int SolutionPathColorArgb { get; set; }
        /// <summary>
        /// The floor color
        /// </summary>
        public CustomColor PathColor { get; set; }
        public int PathColorArgb { get; set; }
        /// <summary>
        /// The wall color
        /// </summary>
        public CustomColor WallColor { get; set; }
        public int WallColorArgb { get; set; }

        public int Width { get { return BitmapArr.Width; } }
        public int Height { get { return BitmapArr.Height; } }
        public int DeviationAmount { get; set; }
        public PixelFormat PixelFormat { get { return BitmapArr.PixelFormat; } }
        public int Stride { get { return BitmapArr.Stride; } }

        private BitmapArray BitmapArr { get; set; }
        

        #endregion
    }
}
