using UnityEngine;

namespace Game.Runtime.Utility
{
    /// <summary>
    /// A class that performs operations between the two color spaces. Not physically accurate.
    /// </summary>
    public static class RGBandCMYKUtility
    {
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
    }
}