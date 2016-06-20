using System.Collections.Generic;
using System.Drawing;

namespace Maze.Imaging
{
    /// <summary>
    /// A collection of conversion parameters used for converting a bitmap into a collection of type T
    /// </summary>
    public class BitmapConversionParams<T>
    {
        #region Constructor

        /// <summary>
        /// Creates a new BitmapConversionParameters class.
        /// </summary>
        public BitmapConversionParams()
        {
            // Initialize
            Collection = new Dictionary<int, T>();
            DeviationAmount = 0;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds conversion parameters to the collection.
        /// </summary>
        /// <param name="color">A <see cref="Color"/>, the pixel color from the bitmap to be converted.</param>
        /// <param name="convertToValue">A <see cref="T"/>, the value to convert the given color to.</param>
        public void AddParameter(Color color, T convertToValue)
        {
            Collection.Add(color.ToArgb(), convertToValue);
        }

        /// <summary>
        /// Adds conversion parameters to the collection.
        /// </summary>
        /// <param name="alpha">A <see cref="byte"/> representing alpha.</param>
        /// <param name="red">A <see cref="byte"/> representing red.</param>
        /// <param name="green">A <see cref="byte"/> representing green.</param>
        /// <param name="blue">A <see cref="byte"/> representing blue.</param>
        /// <param name="convertToValue">A <see cref="T"/>, the value to convert the given color to.</param>
        public void AddParameter(byte alpha, byte red, byte green, byte blue, T convertToValue)
        {
            this.Collection.Add(alpha << 24 | red << 16 | green << 8 | blue, convertToValue);
        }

        /// <summary>
        /// Finds the value closest to the given key. Returns the undefined default value when
        /// no sufficiently close match is found.
        /// </summary>
        /// <param name="key">An <see cref="int"/>, the ARGB value to search for.</param>
        /// <returns>A <see cref="T"/>, the value found.<</returns>
        public T GetClosestMatch(int key)
        {
            T value = UndefinedDefaultValue;
            // Check if key is defined
            if (this.Collection.TryGetValue(key, out value))
                return value;

            // Check all definitions for closest value
            if (DeviationAmount > 0)
            {
                foreach (KeyValuePair<int, T> keyValPair in this.Collection)
                {
                    if (keyValPair.Key - DeviationAmount < key && key < keyValPair.Key + DeviationAmount)
                        break;
                }
            }

            return value;
        }    

        #endregion

        #region Properties

        /// <summary>
        /// A <see cref="Dictionary{TKey, TValue}"/> collection of all conversion parameters where the Key represents the ARBG value of a color.
        /// </summary>
        public Dictionary<int, T> Collection { get; private set; }
        /// <summary>
        /// The character to use when a pixel color value is not defined in the conversion parameter collection.
        /// </summary>
        public T UndefinedDefaultValue { get; set; }
        /// <summary>
        /// The amount of deviation allowed from the defined values. The absolute value.
        /// </summary>
        public byte DeviationAmount { get; set; }

        #endregion
    }
}
