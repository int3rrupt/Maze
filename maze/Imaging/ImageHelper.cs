using Common.DataStructures;
using Maze;
using Maze.Exceptions;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Common.Imaging
{
    public static class ImageHelper
    {
        #region Public Methods

        /// <summary>
        /// Creates a <see cref="BitmapArray"/> from the given <see cref="Bitmap"/>.
        /// </summary>
        /// <param name="image">A <see cref="Bitmap"/>, the image from which to generate a <see cref="BitmapArray"/>.</param>
        /// <returns>A <see cref="BitmapArray"/>, an array of bytes containing the given <see cref="Bitmap"/>.</returns>
        public static BitmapArray ImageToBitmapArray(string imagePath)
        {
            try
            {
                using (Bitmap bitmap = LoadImage(imagePath))
                {
                    // Try to get the image's bits per pixel
                    int bpp = GetBpp(bitmap.PixelFormat);
                    if (bpp == -1)
                        throw new NotSupportedException();

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
                    BitmapArray bmpArr = new BitmapArray(imgByteArr, bmpData.Stride, bpp, bpc, bitmap.Width, bitmap.Height);
                    // Release the bitmap lock
                    bitmap.UnlockBits(bmpData);

                    return bmpArr;
                }
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        public static void WriteToImage(MazeImage mazeImage, string outputPath)
        {
            using (MemoryStream memStream = new MemoryStream(mazeImage.ToByteArray()))
            {
                Image image = Bitmap.FromStream(memStream);
                image.Save(outputPath);
            }
        }

        //public static Graph ImageToGraph(MazeImage mazeImage, BitmapConversionParams<T> bmpConvParams)
        //{
        //    try
        //    {
        //        // Get byte array from bitmap
        //        BitmapArray bitmapArray = ImageToBitmapArray(mazeImage.ImagePath);
        //        PixelFormat pxFormat = image.PixelFormat;
        //        byte[] array = bitmapArray.ByteArr;
        //        // Determine byte to color mapping
        //        //image.pi

        //        // Iterate through byte array converting to multidimesional char array
        //        for (int imgY = 0; imgY < image.Height; imgY++)
        //        {
        //            for (int imgX = 0; imgX < image.Width; imgX++)
        //            {
        //                char charVal;
        //                //// x(stride) + y(stride*x) provides index to first ARGB value.
        //                //int byteIndex = (imgX * 4) + (imgY * (4 * image.Width));
        //                //int aRGB = (int)(bitmapArray.ByteArr[byteIndex + 3] << 24 |
        //                //           bitmapArray.ByteArr[byteIndex + 2] << 16 |
        //                //           bitmapArray.ByteArr[byteIndex + 1] << 8 |
        //                //           bitmapArray.ByteArr[byteIndex]);
        //                int aRGB = bitmapArray.GetArgbAt(imgX, imgY);
        //                int trueArgb = image.GetPixel(imgX, imgY).ToArgb();
        //                if (aRGB != trueArgb)
        //                    throw new Exception();
        //                charArr[imgY, imgX] = bmpConvParams.Collection.TryGetValue(aRGB, out charVal) ? charVal : bmpConvParams.UndefDefChar;
        //            }
        //        }

        //        return charArr;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates a new Bitmap using the given image path.
        /// </summary>
        /// <param name="imagePath">A <see cref="Bitmap"/> created from the given image path.</param>
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
                throw new UnsupportedImageFormatException(imagePath, pixelFormat);
            }
            catch (ArgumentException)
            {
                throw new NotImplementedException();
            }
        }

        private static bool IsSupportedPixelFormat(PixelFormat pixelFormat)
        {
            return true;
        }

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
