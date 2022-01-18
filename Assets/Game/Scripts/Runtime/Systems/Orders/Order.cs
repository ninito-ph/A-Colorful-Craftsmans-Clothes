using Game.Runtime.Data.Attributes;
using Game.Runtime.Utility;
using UnityEngine;

namespace Game.Runtime.Systems.Orders
{
    /// <summary>
    /// A class that represents a timed clothing item order
    /// </summary>
    public sealed class Order : ScriptableObject
    {
        #region Properties

        public ClothingAttributes Item { get; set; }

        public float ColorTolerance { get; set; }

        public float EndTime { get; set; }
        public float RemainingTime => EndTime - Time.time;
        
        public bool IsExpired => Time.time >= EndTime;
        public bool IsCompleted { get; set; } = false;

        #endregion

        #region Public Methods

        /// <summary>
        /// Starts the order's countdown
        /// </summary>
        /// <param name="orderItem">The item linked to the order</param>
        /// <param name="orderTime">The time the player has to complete the order</param>
        /// <param name="colorTolerance">The tolerance the player has to complete the order</param>
        public static Order CreateNew(ClothingAttributes orderItem, float orderTime, float colorTolerance)
        {
            Order newOrder = CreateInstance<Order>();

            if (orderItem == null)
            {
                Debug.LogError("Something tried to initialize an order with a null item!");
                return null;
            }

            newOrder.Item = orderItem;
            newOrder.EndTime = Time.time + orderTime;
            newOrder.ColorTolerance = colorTolerance;
            newOrder.IsCompleted = false;

            return newOrder;
        }

        /// <summary>
        /// Checks whether a given item meets the order's demand
        /// </summary>
        /// <param name="clothing">The item to check for a match</param>
        /// <returns>Whether the order matches</returns>
        public bool MeetsOrderDemand(ClothingAttributes clothing)
        {
            if (clothing == null) return false;
            
            return clothing.ID == Item.ID &&
                   RGBandCMYKUtility.AreColorsSimilar(clothing.Color, Item.Color, ColorTolerance);
        }

        #endregion
    }
}