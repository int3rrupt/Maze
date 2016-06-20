﻿using Maze.Imaging;
using System.Drawing;

namespace Maze
{
    /// <summary>
    /// A collection of properties used to represent a maze
    /// </summary>
    public class MazeImage
    {
        #region Constructor

        /// <summary>
        /// Creates a new MazeImage structure with default values.
        /// </summary>
        /// <param name="imagePath"></param>
        public MazeImage(string imagePath)
        {
            Initialize(imagePath, Color.Red, Color.Blue, Color.Green, Color.White, Color.Black);
        }

        /// <summary>
        /// Creates a new MazeImage structure using provided values.
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="startColor"></param>
        /// <param name="finishColor"></param>
        /// <param name="pathColor"></param>
        /// <param name="floorColor"></param>
        /// <param name="wallColor"></param>
        public MazeImage(string imagePath, Color startColor, Color finishColor, Color pathColor, Color floorColor, Color wallColor)
        {
            Initialize(imagePath, startColor, finishColor, pathColor, floorColor, wallColor);
        }

        #endregion

        public void Initialize(string imagePath, Color startColor, Color finishColor, Color pathColor, Color floorColor, Color wallColor)
        {
            ImagePath = imagePath;
            StartColor = startColor;
            FinishColor = finishColor;
            PathColor = pathColor;
            FloorColor = floorColor;
            WallColor = wallColor;

            StartColorArgb = StartColor.ToArgb();
            FinishColorArgb = FinishColor.ToArgb();
            PathColorArgb = PathColor.ToArgb();
            FloorColorArgb = FloorColor.ToArgb();
            WallColorArgb = WallColor.ToArgb();

            // Generate bitmap array from image
            BitmapArr = ImageHelper.ImageToBitmapArray(ImagePath);
        }

        public MazeNodeType GetPixel(int x, int y)
        {
            int argbKey = BitmapArr.GetArgbAt(x, y);

            if (argbKey == StartColorArgb)
                return MazeNodeType.Start;
            if (argbKey == FinishColorArgb)
                return MazeNodeType.Finish;
            if (argbKey == FloorColorArgb)
                return MazeNodeType.Floor;
            if (argbKey == WallColorArgb)
                return MazeNodeType.Wall;

            return MazeNodeType.Floor; ;
        }

        #region Properties

        public string ImagePath { get; set; }
        /// <summary>
        /// The starting color
        /// </summary>
        public Color StartColor { get; set; }
        private int StartColorArgb;
        /// <summary>
        /// The finishing color
        /// </summary>
        public Color FinishColor { get; set; }
        public int FinishColorArgb { get; set; }
        /// <summary>
        /// The path color
        /// </summary>
        public Color PathColor { get; set; }
        public int PathColorArgb { get; set; }
        /// <summary>
        /// The floor color
        /// </summary>
        public Color FloorColor { get; set; }
        public int FloorColorArgb { get; set; }
        /// <summary>
        /// The wall color
        /// </summary>
        public Color WallColor { get; set; }
        public int WallColorArgb { get; set; }

        public int Width { get { return BitmapArr.Width; } }
        public int Height { get { return BitmapArr.Height; } }
        public int DeviationAmount { get; set; }


        private BitmapArray BitmapArr { get; set; }

        #endregion
    }
}
