using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace maze
{
    /// <summary>
    /// A maze solving class.
    /// </summary>
    public static class MazeSolver
    {
        private struct MazeEndPoints
        {
            public Point StartLocation { get; set; }
            public Point FinishLocation { get; set; }

            public MazeEndPoints(Point startLocation, Point finishLocation)
            {
                this.StartLocation = startLocation;
                this.FinishLocation = finishLocation;
            }
        }

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="startColor"></param>
        /// <param name="finishColor"></param>
        /// <returns></returns>
        public static Bitmap SolveMaze(Bitmap image, Color startColor, Color finishColor, Color wallColor)
        {
            long start = DateTime.Now.Ticks;
            // Find the start and finish endpoints
            MazeEndPoints endPoints = FindEndPoints(image, startColor, finishColor);
            long elapsedTime = DateTime.Now.Ticks - start;
            TimeSpan time = new DateTime(elapsedTime).TimeOfDay;
            return image;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Searches the given image for the start and finish points using the given colors as a reference.
        /// </summary>
        /// <param name="image">A <see cref="Bitmap"/>, the image containing the start point we wish to locate.</param>
        /// <param name="startColor">A <see cref="Color"/>, the color used for the start point.</param>
        /// <param name="finishColor">A <see cref="Color"/>, the color used for the finish point.</param>
        /// <returns>A <see cref="MazeEndPoints"/>, containing two points representing the start and finish locations.</returns>
        static MazeEndPoints FindEndPoints(Bitmap image, Color startColor, Color finishColor)
        {
            MazeEndPoints mazeEndPoints = new MazeEndPoints(Point.Empty, Point.Empty);
            // Loop through entire image, pixel by pixel, searching for the given colors
            for (int imageY = 0; imageY < image.Height; imageY++)
            {
                for (int imageX = 0; imageX < image.Width; imageX++)
                {
                    // Get current pixel color.
                    // **Note, apparently GetPixel can be very slow due to locking and unlocking. TODO: Look into alternatives.
                    int currentPixelArgbValue = image.GetPixel(imageX, imageY).ToArgb();
                    // Add points
                    if (currentPixelArgbValue == startColor.ToArgb())
                        mazeEndPoints.StartLocation = new Point(imageX, imageY);
                    else if (currentPixelArgbValue == finishColor.ToArgb())
                        mazeEndPoints.FinishLocation = new Point(imageX, imageY);
                    // Check if done
                    if (mazeEndPoints.StartLocation != Point.Empty && mazeEndPoints.FinishLocation != Point.Empty)
                        return mazeEndPoints;
                }
            }
            return mazeEndPoints;
        }

        #endregion
    }
}
