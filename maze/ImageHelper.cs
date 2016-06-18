using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace maze
{
    public static class ImageHelper
    {
        #region Public Methods

        public static void BitmapToMaze(Bitmap image)
        {

        }

        #endregion
        
        #region Private Methods

        private static BitmapArray BitmapToBitmapArray(Bitmap image)
        {
            try
            {
                // Lock bitmap into system memory while we copy its data
                BitmapData bmpData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);
                // Calculate total array size based on image height and image stride
                int imgByteCount = Math.Abs(bmpData.Stride) * image.Height;
                // Initialize a new byte array to store the copied image byte values
                byte[] imgByteArr = new byte[imgByteCount];
                // Copy unmanaged bitmap data
                System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, imgByteArr, 0, imgByteCount);
                // Create new BitmapArray to store our values
                BitmapArray bmpArr = new BitmapArray(imgByteArr, image.);
                // Release the bitmap lock
                image.UnlockBits(bmpData);

                return bmpArr;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        public static char[][] BitmapToCharArray(Bitmap image, BitmapConversionParameters bmpConvParams)
        {
            try
            {
                char[][] imgCharArr = new char[image.Height][];
                // Get byte array from bitmap
                BitmapArray bitmapArray = BitmapToBitmapArray(image);

                // Determine byte to color mapping
                //image.pi

                // Iterate through byte array converting to char array
                for (int imgY = 0; imgY < image.Height; imgY++)
                {
                    for (int imgX = 0; imgX < image.Width; imgX++)
                    {
                        // x(stride) + y(stride*x) provides index to first ARGB value.
                        // *Note: The order of ARGB may vary depending on PixelFormat of
                        // bitmap, because of this argb[x] was chosen as the variable name.
                        int argb0 = bitmapArray.ByteArr[imgY + (imgX * bitmapArray.Stride) + (imgY * (bitmapArray.Stride * imgX))];
                        int argb1 = bitmapArray.ByteArr[imgY + (imgX * bitmapArray.Stride) + (imgY * (bitmapArray.Stride * imgX))];
                        int argb2 = bitmapArray.ByteArr[imgY + (imgX * bitmapArray.Stride) + (imgY * (bitmapArray.Stride * imgX))];
                    }
                }

                return imgCharArr;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
