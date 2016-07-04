using Common.Imaging;
using System.Drawing.Imaging;

namespace Common.DataTypes
{
    /// <summary>
    /// Class containing array of bytes representing a bitmap and methods for interacting with the bitmap.
    /// </summary>
    public class BitmapArray
    {
        #region Constructors

        /// <summary>
        /// Creates a new BitmapArray.
        /// </summary>
        /// <param name="byteArr">An array of <see cref="byte"/>, containing the bitmap.</param>
        /// <param name="stride">An <see cref="int"/>, the stride of the image array.</param>
        /// <param name="bpp">An <see cref="int"/>, the bits per pixel count.</param>
        /// <param name="bpc">A <see cref="BitsPerColor"/>, the number of bits per color.</param>
        /// <param name="width">An <see cref="int"/>, the image width.</param>
        /// <param name="height">An <see cref="int"/>, the image height.</param>
        /// <param name="pixelFormat">A <see cref="PixelFormat"/>, the image's pixel format.</param>
        public BitmapArray(byte[] byteArr, int stride, int bpp, BitsPerColor bpc, int width, int height, PixelFormat pixelFormat)
        {
            ByteArr = byteArr;
            Stride = stride;
            Bpp = bpp;
            Bpc = bpc;
            Width = width;
            Height = height;
            PixelFormat = pixelFormat;
            // Calculate bitmap padding
            // Bpp / 8 = Number of bytes
            // Stride % Number of Bytes = Padding in Bytes
            //
            Padding = Stride % (Bpp / 8);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the ARGB value at the given point cast to an <see cref="int"/> in the following
        /// format: AARRGGBB.
        /// </summary>
        /// <param name="x">An <see cref="int"/>, the x location.</param>
        /// <param name="y">An <see cref="int"/>, the y location.</param>
        /// <returns>An <see cref="int"/>, the ARGB value.</returns>
        public int GetArgbAt(int x, int y)
        {
            // Get index to first byte of ARGB color
            int byteIndex = GetByteIndexFor(x, y);
            int aRGB = -1;
            switch (PixelFormat)
            {
                case PixelFormat.Format32bppArgb:
                    {
                        // Place all into single int in form: AARRGGBB
                        aRGB = (ByteArr[byteIndex] |                // B
                                    ByteArr[byteIndex + 1] << 8 |   // G
                                    ByteArr[byteIndex + 2] << 16 |  // R
                                    ByteArr[byteIndex + 3] << 24);  // A
                        break;
                    }
                case PixelFormat.Format24bppRgb:
                    {
                        // Place all into single int in form: AARRGGBB
                        aRGB = (ByteArr[byteIndex] |                // B
                                    ByteArr[byteIndex + 1] << 8 |   // G
                                    ByteArr[byteIndex + 2] << 16 |  // R
                                    255 << 24);                     // A
                        break;
                    }
            }
            return aRGB;
        }

        /// <summary>
        /// Gets the <see cref="CustomColor"/> at the given x-y location.
        /// </summary>
        /// <param name="x">An <see cref="int"/>, the x location.</param>
        /// <param name="y">An <see cref="int"/>, the y location.</param>
        /// <returns>A <see cref="CustomColor"/>, containing the ARGB values at the given x-y location.</returns>
        public CustomColor GetColorAt(int x, int y)
        {
            // Get index to first byte of ARGB color
            int byteIndex = GetByteIndexFor(x, y);
            // Default alpha to 255
            byte alpha = 255;
            switch (PixelFormat)
            {
                case PixelFormat.Format32bppArgb:
                    {
                        alpha = ByteArr[byteIndex + 3];
                        break;
                    }
                case PixelFormat.Format24bppRgb:
                    {
                        // Keep alpha as 255;
                        break;
                    }
            }
            return new CustomColor(alpha, ByteArr[byteIndex + 2], ByteArr[byteIndex + 1], ByteArr[byteIndex]);
        }

        /// <summary>
        /// Updates the image's ARGB values with the ones given at the given x-y location.
        /// </summary>
        /// <param name="x">An <see cref="int"/>, the x location.</param>
        /// <param name="y">An <see cref="int"/>, the y location.</param>
        /// <param name="A">A <see cref="byte"/>, the alpha value.</param>
        /// <param name="R">A <see cref="byte"/>, the red value.</param>
        /// <param name="G">A <see cref="byte"/>, the green value.</param>
        /// <param name="B">A <see cref="byte"/>, the blue value.</param>
        public void UpdateArgbAt(int x, int y, byte A, byte R, byte G, byte B)
        {
            // Get index to first byte of ARGB color
            int byteIndex = GetByteIndexFor(x, y);
            ByteArr[byteIndex] = B;
            ByteArr[byteIndex + 1] = G;
            ByteArr[byteIndex + 2] = R;
            ByteArr[byteIndex + 3] = A;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the index to the first color value in the byte array for the
        /// given pixel location.
        /// </summary>
        /// <param name="x">An <see cref="int"/>, the x location.</param>
        /// <param name="y">An <see cref="int"/>, the y location.</param>
        /// <returns>An <see cref="int"/>, the index to the fist color value.</returns>
        private int GetByteIndexFor(int x, int y)
        {
            // Provides index to first color value for current pixel
            //    current column      Bits in row * current row
            // x(Bytes Per Pixel)     +     (y * stride) 
            return (x * Bpp / 8) + (y * (Stride));
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The byte array of the converted bitmap.
        /// </summary>
        public byte[] ByteArr { get; private set; }
        /// <summary>
        /// The stride (scan line) of the bitmap.
        /// </summary>
        public int Stride { get; private set; }
        /// <summary>
        /// The bits per pixel of the the bitmap.
        /// </summary>
        public int Bpp { get; private set; }
        /// <summary>
        /// The number of bits per color.
        /// </summary>
        public BitsPerColor Bpc { get; private set; }
        /// <summary>
        /// The amount of padding per row of pixels in bytes.
        /// </summary>
        public int Padding { get; private set; }
        /// <summary>
        /// The image width.
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// The image height.
        /// </summary>
        public int Height { get; private set; }
        /// <summary>
        /// The pixel format of the image.
        /// </summary>
        public PixelFormat PixelFormat { get; private set; }

        #endregion
    }
}
