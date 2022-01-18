using UnityEngine;

namespace Game.Runtime.Utility
{
    /// <summary>
    /// A class that performs operations between the two color spaces. Not physically accurate.
    /// </summary>
    public static class RGBandCMYKUtility
    {
        #region Public Methods

        /// <summary>
        /// Converts from RGB color space to CMYK color space
        /// </summary>
        /// <param name="rgbColor">The RGB color to convert</param>
        /// <returns>The RGB color in CMYK space</returns>
        public static Vector4 ConvertRGBtoCMYK(Color rgbColor)
        {
            float r = rgbColor.r;
            float g = rgbColor.g;
            float b = rgbColor.b;

            float k = 1 - Mathf.Max(r, Mathf.Max(g, b));
            float c = (1 - r - k) / (1 - k);
            float m = (1 - g - k) / (1 - k);
            float y = (1 - b - k) / (1 - k);

            return new Vector4(c, m, y, k);
        }

        /// <summary>
        /// Converts from CMYK color space to RGB color space
        /// </summary>
        /// <param name="cmykColor">The CMYK color to convert</param>
        /// <returns>The CMYK color in RGB space</returns>
        public static Color ConvertCMYKtoRGB(Vector4 cmykColor)
        {
            float c = cmykColor.x;
            float m = cmykColor.y;
            float y = cmykColor.z;
            float k = cmykColor.w;

            float r = (1 - c) * (1 - k);
            float g = (1 - m) * (1 - k);
            float b = (1 - y) * (1 - k);

            return new Color(r, g, b, 1);
        }

        /// <summary>
        /// Adds colors in CMYK color space
        /// </summary>
        /// <param name="color">The first color to add</param>
        /// <param name="secondColor">The second color to add</param>
        /// <returns>The colors added in CMYK space, converted back to RGB</returns>
        public static Color AddColorsInCMYK(Color color, Color secondColor)
        {
            return ConvertCMYKtoRGB(ConvertRGBtoCMYK(color) + ConvertRGBtoCMYK(secondColor));
        }
        
        /// <summary>
        /// Adds a CMYK color to an RGB color
        /// </summary>
        /// <param name="rgbColor">The rgb color to add to</param>
        /// <param name="cmykColor">The cmyk color to add</param>
        /// <returns>The added colors in RGB space</returns>
        public static Color AddCMYKtoRBG(Color rgbColor, Vector4 cmykColor)
        {
            return ConvertCMYKtoRGB(ConvertRGBtoCMYK(rgbColor) + cmykColor);
        }

        /// <summary>
        /// Generates a random RGB color
        /// </summary>
        /// <param name="minValue">The minimum value of the color</param>
        /// <param name="maxValue">The maximum value of the color</param>
        /// <returns>A random RGB color</returns>
        public static Color GenerateRandomRGBColor(float minValue = 0f, float maxValue = 1f)
        {
            float r = Random.Range(0f, 1f);
            float g = Random.Range(0f, 1f);
            float b = Random.Range(0f, 1f);

            return new Color(r, g, b, 1f);
        }

        /// <summary>
        /// Checks whether two colors are equal in RGB space within a certain tolerance
        /// </summary>
        /// <param name="colorA">The first color</param>
        /// <param name="colorB">The second color</param>
        /// <param name="tolerance">The tolerance to compare with</param>
        /// <returns>Whether the colors are similar</returns>
        public static bool AreColorsSimilar(Color colorA, Color colorB, float tolerance)
        {
            return IsNumberSimilar(colorA.r, colorB.r, tolerance) &&
                   IsNumberSimilar(colorA.g, colorB.g, tolerance) &&
                   IsNumberSimilar(colorA.b, colorB.b, tolerance);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Checks whether two numbers are similar enough
        /// </summary>
        /// <param name="a">The first number</param>
        /// <param name="b">The second number</param>
        /// <param name="tolerance">The tolerance between the two numbers</param>
        /// <returns></returns>
        private static bool IsNumberSimilar(float a, float b, float tolerance)
        {
            // Checks wheter the difference between the two numbers is less than the tolerance
            return Mathf.Abs(a - b) <= tolerance;
        }

        #endregion
    }
}