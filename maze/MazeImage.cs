using Common.DataStructures;
using Common.Imaging;
using Maze.Enums;
using System.Drawing.Imaging;

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
        public MazeImage(BitmapArray bitmapArray)
        {
            CustomColor red = new CustomColor(255, 255, 0, 0);
            CustomColor blue = new CustomColor(255, 0, 0, 255);
            CustomColor green = new CustomColor(255, 0, 255, 0);
            CustomColor white = new CustomColor(255, 255, 255, 255);
            CustomColor black = new CustomColor(255, 0, 0, 0);
            Initialize(bitmapArray, red, blue, green, white, black);
        }

        /// <summary>
        /// Creates a new MazeImage structure using provided values.
        /// </summary>
        /// <param name="startColor"></param>
        /// <param name="finishColor"></param>
        /// <param name="pathColor"></param>
        /// <param name="floorColor"></param>
        /// <param name="wallColor"></param>
        public MazeImage(
            BitmapArray bitmapArray,
            PixelFormat pixelFormat,
            CustomColor startColor,
            CustomColor finishColor,
            CustomColor pathColor,
            CustomColor floorColor,
            CustomColor wallColor)
        {
            Initialize(bitmapArray, startColor, finishColor, pathColor, floorColor, wallColor);
        }

        #endregion

        private void Initialize(
            BitmapArray bitmapArray,
            CustomColor startColor,
            CustomColor finishColor,
            CustomColor pathColor,
            CustomColor floorColor,
            CustomColor wallColor)
        {
            BitmapArr = bitmapArray;
            StartColor = startColor;
            FinishColor = finishColor;
            SolutionPathColor = pathColor;
            FloorColor = floorColor;
            WallColor = wallColor;

            StartColorArgb = StartColor.ToArgb();
            FinishColorArgb = FinishColor.ToArgb();
            SolutionPathColorArgb = SolutionPathColor.ToArgb();
            FloorColorArgb = FloorColor.ToArgb();
            WallColorArgb = WallColor.ToArgb();
        }

        public MazeNodeType GetPixel(int x, int y)
        {
            int argbKey = BitmapArr.GetArgbAt(x, y);

            if (argbKey == StartColorArgb)
                return MazeNodeType.Start;
            if (argbKey == FinishColorArgb)
                return MazeNodeType.Finish;
            if (argbKey == FloorColorArgb)
                return MazeNodeType.Path;
            if (argbKey == WallColorArgb)
                return MazeNodeType.Wall;

            return MazeNodeType.Path; ;
        }

        public void DrawPath(AStarNode node)
        {
            AStarNode currentNode = node;
            while (currentNode != null)
            {
                int x = IndexToX(currentNode.Key);
                int y = IndexToY(currentNode.Key);
                BitmapArr.UpdateArgbAt(x, y, SolutionPathColor.A, SolutionPathColor.R, SolutionPathColor.G, SolutionPathColor.B);
                currentNode = (AStarNode)currentNode.Parent;
            }
        }

        public byte[] ToByteArray()
        {
            return BitmapArr.ByteArr;
        }

        public int IndexToX(int pixelIndex)
        {
            return pixelIndex - Width * (pixelIndex / Width);
        }

        public int IndexToY(int pixelIndex)
        {
            return pixelIndex / Width;
        }

        public int LocationToIndex(int x, int y)
        {
            return x + (Width * y);
        }

        #region Properties

        /// <summary>
        /// The starting color
        /// </summary>
        public CustomColor StartColor { get; set; }
        private int StartColorArgb;
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
        public CustomColor FloorColor { get; set; }
        public int FloorColorArgb { get; set; }
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
