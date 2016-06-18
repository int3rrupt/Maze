using System.Drawing;

namespace maze
{
    public class MazeController
    {
        #region Constructor

        /// <summary>
        /// Creates a new Maze Controller class
        /// </summary>
        public MazeController()
        {
            // This purpose of this constructor is to provide the XML summary
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Solves the given maze image. Outputs a copy of the image with the computed solution.
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="solutionPath"></param>
        public bool SolveMaze(string imagePath, string solutionPath)
        {
            using (Bitmap mazeImage = FileHelper.ReadImage(imagePath))
            {
                BitmapConversionParameters bmpConvParams = new BitmapConversionParameters();
                bmpConvParams.AddParameter(Color.Red, '@');
                bmpConvParams.AddParameter(Color.Blue, 'X');
                bmpConvParams.AddParameter(Color.Black, '*');
                // Convert bitmap to Maze
                ImageHelper.BitmapToCharArray(mazeImage, bmpConvParams);





                using (Bitmap solutionImage = MazeSolver.SolveMaze(mazeImage, this.StartColor, this.FinishColor, this.WallColor))
                {
                    


                    FileHelper.WriteImage(solutionImage, solutionPath);
                }
            }
            return false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The starting color
        /// </summary>
        public Color StartColor { get; set; }

        /// <summary>
        /// The finishing color
        /// </summary>
        public Color FinishColor { get; set; }

        /// <summary>
        /// The wall color
        /// </summary>
        public Color WallColor { get; set; }

        #endregion
    }
}
