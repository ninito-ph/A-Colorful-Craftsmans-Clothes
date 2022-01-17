using System;
using UnityEngine;

namespace Game.Runtime.Data.Attributes
{
    /// <summary>
    /// The stored (serialized) form of an ItemAttributes class
    /// </summary>
    [Serializable]
    public sealed class StoredItemAttributes
    {
        #region Public Fields

        [SerializeField]
        public int ID;

        [SerializeField]
        private float ItemColorR;
        [SerializeField]
        private float ItemColorG;
        [SerializeField]
        private float ItemColorB;
        [SerializeField]
        private float ItemColorA;

        #endregion

        #region Properties

        public Color Color
        {
            get => new Color(ItemColorR, ItemColorG, ItemColorB, ItemColorA);
            set
            {
                ItemColorR = value.r;
                ItemColorG = value.g;
                ItemColorB = value.b;
                ItemColorA = value.a;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the StoredItemAttributes class
        /// </summary>
        /// <param name="attributes">The attributes of the item to store</param>
        public StoredItemAttributes(ItemAttributes attributes)
        {
            ID = attributes.ID;
            Color = attributes.Color;
        }

        #endregion
    }
}