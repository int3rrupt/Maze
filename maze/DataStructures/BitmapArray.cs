﻿using Common.Imaging;
using System.Drawing.Imaging;

namespace Common.DataStructures
{
    /// <summary>
    /// An array of bytes containing a bitmap.
    /// </summary>
    public class BitmapArray
    {
        #region Declarations

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
        public int Padding { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
        public PixelFormat PixelFormat { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new BitmapArray.
        /// </summary>
        /// <param name="byteArr">A <see cref="int"/>, containing the bitmap.</param>
        /// <param name="bpp">A <see cref="int"/>, containing the bitmap.</param>
        /// <param name="stride">An <see cref="ByteArr[]"/>, the bits per pixel of the bitmap.</param>
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
        /// Gets the ARGB value at the given point cast to an int in the following
        /// format: AARRGGBB.
        /// </summary>
        /// <param name="x">An <see cref="int"/>, the x location.</param>
        /// <param name="y">An <see cref="int"/>, the y location.</param>
        /// <returns>An <see cref="int"/>, the ARGB value.</returns>
        public int GetArgbAt(int x, int y)
        {
            int aRGB = -1;
            switch (PixelFormat)
            {
                case PixelFormat.Format32bppArgb:
                    {
                        // Provides index to first color value for current pixel
                        //    current column      Bits in row * current row
                        // x(Bytes Per Pixel)     +     (y * stride) 
                        int byteIndex = (x * Bpp / 8) + (y * (Stride));
                        // Place all into single int in form: AARRGGBB
                        aRGB = (ByteArr[byteIndex] |           // B
                                    ByteArr[byteIndex + 1] << 8 |  // G
                                    ByteArr[byteIndex + 2] << 16 | // R
                                    ByteArr[byteIndex + 3] << 24); // A}
                        break;
                    }
                case PixelFormat.Format24bppRgb:
                    {
                        // Provides index to first color value for current pixel
                        //    current column      Bits in row * current row
                        // x(Bytes Per Pixel)     +     (y * stride) 
                        int byteIndex = (x * Bpp / 8) + (y * (Stride));
                        // Place all into single int in form: AARRGGBB
                        aRGB = (ByteArr[byteIndex] |           // B
                                    ByteArr[byteIndex + 1] << 8 |  // G
                                    ByteArr[byteIndex + 2] << 16 | // R
                                    255 << 24); // A
                        break;
                    }
            }
            

            return aRGB;
        }

        public void UpdateArgbAt(int x, int y, byte A, byte R, byte G, byte B)
        {
            int byteIndex = (x * Bpp / 8) + (y * (Stride));
            ByteArr[byteIndex] = B;
            ByteArr[byteIndex + 1] = G;
            ByteArr[byteIndex + 2] = R;
            ByteArr[byteIndex + 3] = A;
        }

        #endregion
    }
}