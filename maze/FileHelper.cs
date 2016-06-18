using System;
using System.Drawing;
using System.IO;

namespace maze
{
    public static class FileHelper
    {
        /// <summary>
        /// Reads image ...
        /// </summary>
        /// <param name="imagePath"></param>
        public static Bitmap ReadImage(string imagePath)
        {
            try
            {
                // Create image object first 
                Image image = Image.FromFile(imagePath);
                return new Bitmap(image);
            }
            catch (FileNotFoundException)
            {
                throw new NotImplementedException();
            }
        }

        public static bool WriteImage(Bitmap image, string imagePath)
        {
            return false;
        }
    }
}