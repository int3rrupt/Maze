using Common.DataTypes;
using Common.Imaging;
using Maze;
using Maze.DataTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazeUnitTest
{
    [TestClass]
    public class GraphGeneratorTests
    {
        [TestMethod]
        public void GenerateGraph()
        {
            
            string imagePath = @"C:\Users\reyes\Downloads\smallmaze3.png";
            // Generate bitmap array from image
            BitmapArray bitmapArray = ImageHelper.ImageToBitmapArray(imagePath);
            // Create new maze image object
            MazeImage mazeImage = new MazeImage(bitmapArray);
            // Generate graph from image
            MazeGraph graph = GraphGenerator.CreateGraphFrom(mazeImage);
        }
    }
}
