namespace Common.Imaging
{
    /// <summary>
    /// The number of bits for each of the colors.
    /// </summary>
    public struct BitsPerColor
    {
        /// <summary>
        /// The number of bits used for Alpha
        /// </summary>
        public int Alpha { get; private set; }
        /// <summary>
        /// The number of bits used for Red
        /// </summary>
        public int Red { get; private set; }
        /// <summary>
        /// The number of bits used for Green
        /// </summary>
        public int Green { get; private set; }
        /// <summary>
        /// The number of bits used for Blue
        /// </summary>
        public int Blue { get; private set; }

        /// <summary>
        /// Creates a new BitsPerColor structure using the given values
        /// </summary>
        /// <param name="alpha">An <see cref="int"/>, the number of bits used for Alpha.</param>
        /// <param name="red">An <see cref="int"/>, the number of bits used for Red.</param>
        /// <param name="green">An <see cref="int"/>, the number of bits used for Green.</param>
        /// <param name="blue">An <see cref="int"/>, the number of bits used for Blue.</param>
        public BitsPerColor(int alpha, int red, int green, int blue)
        {
            Alpha = alpha;
            Red = red;
            Green = green;
            Blue = blue;
        }
    }
}
