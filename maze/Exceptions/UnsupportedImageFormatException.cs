using System;

namespace Maze.Exceptions
{
    /// <summary>
    /// The exception that is thrown when an attempt is made to use an unsupported image format.
    /// </summary>
    public class UnsupportedImageFormatException : Exception
    {
        /// <summary>
        /// The path to the image that caused the exception.
        /// </summary>
        public string ImagePath { get; private set; }
        /// <summary>
        /// The PixelFormat of the image.
        /// </summary>
        public string PixelFormat { get; private set; }

        /// <summary>
        /// Creates a new UnsupportedImageFormatException class.
        /// </summary>
        /// <param name="imageName">A <see cref="string"/>, the name of the image that caused the exception</param>
        /// <param name="pixelFormat">A <see cref="string"/>, the PixelFormat of the image.</param>
        public UnsupportedImageFormatException(string imageName, string pixelFormat)
            : base(ExceptionMessages.UnsupportedImageFormatExceptionMessage)
        {
            ImagePath = imageName;
            PixelFormat = pixelFormat;
        }
    }
}
