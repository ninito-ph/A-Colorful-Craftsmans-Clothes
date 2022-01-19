using System;
using Game.Runtime.Core;
using Game.Runtime.Data.Attributes;
using Game.Runtime.Data.Registries;
using Game.Runtime.Systems.Save.Saves;
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
        
        // Look at the Store method for why I'm using LastSavedTime instead of Time.time
        public float RemainingTime => EndTime - GameManager.Instance.LastSavedTime;

        public bool IsExpired => GameManager.Instance.LastSavedTime >= EndTime;
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

        /// <summary>
        /// Stores the order in a serialized JSON string
        /// </summary>
        /// <returns>The serialized JSON string of the order</returns>
        public string Store()
        {
            /*
             * You have got to be fucking kidding me. Time.time returns 0 when called at the frame where the game is
             * closed, and the best part is that its NOWHERE to be found in the Unity documentation. This is beyond
             * human comprehension. I spent hours figuring out why RemainingTime was not accurate here.
             */
            OrderSave save = new OrderSave(Item.Store(), RemainingTime, ColorTolerance);
            return JsonUtility.ToJson(save);
        }

        /// <summary>
        /// Restores the Order from its serialized JSON form
        /// </summary>
        /// <param name="data">The data to restore</param>
        /// <param name="registry">The registry to restore with</param>
        /// <returns>The restored Order object</returns>
        public static Order Restore(string data, ItemRegistry registry)
        {
            if (String.IsNullOrEmpty(data)) return null;

            OrderSave save = JsonUtility.FromJson<OrderSave>(data);

            Order order = CreateNew(ItemAttributes.Restore(save.StoredItem, registry) as ClothingAttributes,
                save.RemainingTime,
                save.ColorTolerance);
            order.EndTime = Time.time + save.RemainingTime;

            return order;
        }

        #endregion
    }
}