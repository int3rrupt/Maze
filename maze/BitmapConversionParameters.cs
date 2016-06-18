using System.Collections.Generic;
using System.Drawing;

namespace maze
{
    /// <summary>
    /// A collection of conversion parameters used to convert a bitmap into characters
    /// </summary>
    public class BitmapConversionParameters
    {
        #region Constructor

        /// <summary>
        /// Creates a new BitmapConversionParameters class.
        /// </summary>
        public BitmapConversionParameters()
        {
            // Initialize
            this.ConversionParametersDictionary = new Dictionary<int, char>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds conversion parameters to the collection.
        /// </summary>
        /// <param name="color">A <see cref="Color"/>, the pixel color from the bitmap to be converted.</param>
        /// <param name="converToChar">A <see cref="char"/> to convert the given color to. </param>
        public void AddParameter(Color color, char converToChar)
        {
            this.ConversionParametersDictionary.Add(color.ToArgb(), converToChar);
        }

        /// <summary>
        /// Adds conversion parameters to the collection.
        /// </summary>
        /// <param name="alpha">A <see cref="byte"/> representing alpha.</param>
        /// <param name="red">A <see cref="byte"/> representing red.</param>
        /// <param name="green">A <see cref="byte"/> representing green.</param>
        /// <param name="blue">A <see cref="byte"/> representing blue.</param>
        /// <param name="converToChar"></param>
        public void AddParameter(byte alpha, byte red, byte green, byte blue, char converToChar)
        {
            this.ConversionParametersDictionary.Add((int)alpha << 32 | (int)red << 16 | (int)green << 8 | (int)blue, converToChar);
        }

        #endregion

        #region Properties

        /// <summary>
        /// A <see cref="Dictionary{TKey, TValue}"/> collection of all conversion parameters where the Key represents the ARBG value of a color.
        /// </summary>
        public Dictionary<int, char> ConversionParametersDictionary { get; private set; }

        /// <summary>
        /// The character to use when a pixel color value is not defined in the conversion parameter collection.
        /// </summary>
        public char UndefinedConversionChar { get; set; }

        #endregion
    }
}
