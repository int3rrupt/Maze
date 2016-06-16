using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace maze
{
    public class MazeController
    {
        #region Constructor

        /// <summary>
        /// The default constructor for the MazeController class
        /// </summary>
        public MazeController()
        {
            //
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
                using (Bitmap solutionImage = MazeSolver.SolveMaze(mazeImage))
                {
                    FileHelper.WriteImage(solutionImage, solutionPath);
                }
            }
                return false;
        }

        #endregion
    }
}
