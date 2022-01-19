using System;
using Game.Runtime.Systems.Orders;
using UnityEngine;

namespace Game.Runtime.Systems.Save.Saves
{
    /// <summary>
    /// A class that contains permanent data for a single order <see cref="Order"/>
    /// </summary>
    [Serializable]
    public sealed class OrderSave
    {
        #region Public Fields

        [SerializeField]
        public string StoredItem;
        [SerializeField]
        public float RemainingTime;

        [SerializeField]
        public float ColorTolerance;

        #endregion

        #region Constructors

        public OrderSave(string storedItem, float remainingTime, float colorTolerance)
        {
            StoredItem = storedItem;
            RemainingTime = remainingTime;
            ColorTolerance = colorTolerance;
        }

        #endregion
    }
}