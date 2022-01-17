using System;
using Game.Runtime.Data.Attributes;
using UnityEngine;

namespace Game.Runtime.Systems.Clothing
{
    /// <summary>
    /// A class that describes a slot for a clothing item
    /// </summary>
    [Serializable]
    public sealed class ClothingSlot
    {
        #region Public Fields

        [Header("Equipment")]
        [SerializeField]
        public ClothingAttributes Article;
        
        [Header("Dependencies")]
        [SerializeField]
        public SpriteRenderer ClothesRenderer;
        [SerializeField]
        public Animator ClothesAnimator;

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the clothing article to the given one
        /// </summary>
        /// <param name="article">The given clothing article</param>
        public void SetArticle(ClothingAttributes article)
        {
            Article = article;
            UpdateRendererAndAnimator();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the renderer and animator of the clothing slot
        /// </summary>
        private void UpdateRendererAndAnimator()
        {
            ClothesRenderer.color = Article.Color;
            ClothesAnimator.runtimeAnimatorController = Article.OverrideController;
        }

        #endregion
    }
}