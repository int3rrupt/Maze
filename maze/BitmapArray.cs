namespace maze
{
    /// <summary>
    /// An array of bytes containing a bitmap.
    /// </summary>
    public struct BitmapArray
    {
        /// <summary>
        /// The bits per pixel of the the bitmap
        /// </summary>
        public int BPP { get; set; }
        /// <summary>
        /// The byte array of the converted bitmap
        /// </summary>
        public byte[] ByteArr { get; set; }

        /// <summary>
        /// Creates a new BitmapArray.
        /// </summary>
        /// <param name="byteArr">A <see cref="byte[]"/>, containing the bitmap.</param>
        /// <param name="bpp">An <see cref="int"/>, the bits per pixel of the bitmap.</param>
        public BitmapArray(byte[] byteArr, int bpp)
        {
            this.ByteArr = byteArr;
            this.BPP = bpp;
        }
    }
}
