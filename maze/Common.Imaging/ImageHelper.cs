using Common.DataTypes;
using Maze.DataTypes;
using Maze.Exceptions;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Common.Imaging
{
    /// <summary>
    /// Class used for reading and writing images.
    /// </summary>
    public static class ImageHelper
    {
        #region Public Methods

        /// <summary>
        /// Creates a <see cref="BitmapArray"/> using the given image path.
        /// </summary>
        /// <param name="imagePath">A <see cref="string"/>, the image path from which to generate a <see cref="BitmapArray"/>.</param>
        /// <returns>A <see cref="BitmapArray"/>, an array of bytes containing the given image.</returns>
        public static BitmapArray ImageToBitmapArray(string imagePath)
        {
            using (Bitmap bitmap = LoadImage(imagePath))
            {
                // Get the image's bits per pixel count
                int bpp = GetBpp(bitmap.PixelFormat);
                // Lock bitmap into system memory while we copy its data
                BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
                // Calculate total array size based on image height and image stride
                int imgByteCount = Math.Abs(bmpData.Stride) * bitmap.Height;
                // Initialize a new byte array to store the copied image byte values
                byte[] imgByteArr = new byte[imgByteCount];
                // Copy unmanaged bitmap data
                System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, imgByteArr, 0, imgByteCount);
                BitsPerColor bpc = GetBpc(bitmap.PixelFormat);
                // Create new BitmapArray to store our values
                BitmapArray bmpArr = new BitmapArray(imgByteArr, bmpData.Stride, bpp, bpc, bitmap.Width, bitmap.Height, bitmap.PixelFormat);
                // Release the bitmap lock
                bitmap.UnlockBits(bmpData);

                return bmpArr;
            }
        }

        /// <summary>
        /// Writes the given <see cref="MazeImage"/> to the given output path.
        /// </summary>
        /// <param name="mazeImage">A <see cref="MazeImage"/>, containing the image data.</param>
        /// <param name="outputPath">A <see cref="string"/>, the desired output path.</param>
        public static void WriteToImage(MazeImage mazeImage, string outputPath)
        {
            using (Bitmap bitmap = new Bitmap(mazeImage.Width, mazeImage.Height, mazeImage.PixelFormat))
            {
                // Lock bitmap into system memory while we copy its data
                BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, mazeImage.Width, mazeImage.Height), ImageLockMode.WriteOnly, mazeImage.PixelFormat);
                // Calculate total array size based on image height and image stride
                int imgByteCount = Math.Abs(mazeImage.Stride) * mazeImage.Height;
                // Copy managed bitmap data
                System.Runtime.InteropServices.Marshal.Copy(mazeImage.ToByteArray(), 0, bmpData.Scan0, imgByteCount);
                // Save to output path
                bitmap.Save(outputPath);
                // Release the bitmap lock
                bitmap.UnlockBits(bmpData);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates a new Bitmap using the given image path.
        /// </summary>
        /// <param name="imagePath">A <see cref="string"/>, the image path.</param>
        /// <returns>A <see cref="Bitmap"/> created from the given image path.</returns>
        private static Bitmap LoadImage(string imagePath)
        {
            try
            {
                // Check if exists
                if (!File.Exists(imagePath))
                    throw new FileNotFoundException(String.Format(ExceptionMessages.FileNotFoundExceptionMessage, imagePath));
                // Create bitmap
                Bitmap bitmap = new Bitmap(imagePath);
                // Check pixel format
                if (IsSupportedPixelFormat(bitmap.PixelFormat))
                    return bitmap;

                // Throw exception
                string pixelFormat = bitmap.PixelFormat.ToString();
                bitmap.Dispose();
                throw new UnsupportedImageFormatException(Path.GetFileName(imagePath), pixelFormat);
            }
            catch (ArgumentException)
            {
                throw new UnsupportedImageFormatException(Path.GetFileName(imagePath));
            }
        }

        /// <summary>
        /// Determines whether the given pixel format is supported.
        /// </summary>
        /// <param name="pixelFormat">A <see cref="PixelFormat"/>, used to determine whether conversion of
        /// image is possible.</param>
        /// <returns>A <see cref="bool"/>, true when pixel format is supported, false otherwise.</returns>
        private static bool IsSupportedPixelFormat(PixelFormat pixelFormat)
        {
            switch (pixelFormat)
            {
                case PixelFormat.Format24bppRgb:
                case PixelFormat.Format32bppArgb:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets the bits per pixel for the given pixel format.
        /// </summary>
        /// <param name="pixelFormat">A <see cref="PixelFormat"/>, the pixel format from which to obtain the
        /// number of bits per pixel.</param>
        /// <returns>An <see cref="int"/>, the number of bits per pixel.</returns>
        private static int GetBpp(PixelFormat pixelFormat)
        {
            int bpp = -1;
            switch (pixelFormat)
            {
                case PixelFormat.Format24bppRgb:
                    {
                        bpp = 24;
                        break;
                    }
                case PixelFormat.Format32bppArgb:
                    {
                        bpp = 32;
                        break;
                    }
            }

            return bpp;
        }

        /// <summary>
        /// Gets the bits per color for the given pixel format.
        /// </summary>
        /// <param name="pixelFormat">A <see cref="PixelFormat"/>, the pixel format for which to obtain the 
        /// bits per color.</param>
        /// <returns>A <see cref="BitsPerColor"/>, containing the bits per color.</returns>
        private static BitsPerColor GetBpc(PixelFormat pixelFormat)
        {
            int alpha = -1;
            int red = -1;
            int green = -1;
            int blue = -1;

            // Supported formats
            switch (pixelFormat)
            {
                case PixelFormat.Format24bppRgb:
                    {
                        alpha = 0;
                        red = 8;
                        green = 8;
                        blue = 8;
                        break;
                    }
                case PixelFormat.Format32bppArgb:
                    {
                        alpha = 8;
                        red = 8;
                        green = 8;
                        blue = 8;
                        break;
                    }
            }

            return new BitsPerColor(alpha, red, green, blue);
        }

        #endregion
    }
}
