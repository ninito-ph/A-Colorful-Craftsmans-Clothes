using Game.Runtime.Data.Attributes;
using UnityEngine;

namespace Game.Runtime.Systems.Clothing
{
    /// <summary>
    /// A class that controls a specific clothing article's appearance.
    /// </summary>
    public sealed class ClothingArticle : ScriptableObject
    {
        #region Properties

        [field: SerializeField]
        public ClothingAttributes Attributes { get; private set; }
        [field: SerializeField]
        public Color Color { get; private set; } = Color.white;

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the clothing's appearance and color
        /// </summary>
        /// <param name="attributes">The attributes to set the article to</param>
        /// <param name="color">The color to set the article to</param>
        public void SetClothingAppearance(ClothingAttributes attributes, Color color)
        {
            Attributes = attributes;
            Color = color;
        }

        #endregion
    }
}