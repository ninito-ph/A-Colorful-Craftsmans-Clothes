using System.Collections.Generic;
using System.Linq;
using Game.Runtime.Data.Attributes;
using Game.Runtime.Systems.Inventory;
using UnityEngine;

namespace Game.Runtime.Systems.Orders
{
    /// <summary>
    /// A class that receives items and attempts to complete orders with them
    /// </summary>
    public sealed class OrderReceiver : MonoBehaviour
    {
        #region Private Fields

        [Header("Dependencies")]
        [SerializeField]
        private OrderManager orderManager;

        #endregion

        #region Public Methods

        /// <summary>
        /// Attempts to complete as many orders as possible using the given inventory
        /// </summary>
        /// <param name="inventoryProvider">The inventory provider to check for order-completing items</param>
        /// <returns>The amount of complete orders</returns>
        public int TryCompleteOrders(InventoryProvider inventoryProvider)
        {
            List<Order> activeOrders = orderManager.Orders;
            int completedOrders = 0;
            int currentlyActiveOrders = activeOrders.Count;
            
            for (int index = currentlyActiveOrders - 1; index >= 0; index--)
            {
                ClothingAttributes adequateItem = GetMatchingClothingAttributes(inventoryProvider, activeOrders[index]);

                if (!inventoryProvider.Contents.TryRemoveItem(adequateItem)) continue;
                completedOrders++;
                activeOrders[index].IsCompleted = true;
            }

            return completedOrders;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an item that matches the order's demand
        /// </summary>
        /// <param name="inventoryProvider">The inventory provider to check</param>
        /// <param name="order">The order to compare to</param>
        /// <returns>The matching clothing item</returns>
        private static ClothingAttributes GetMatchingClothingAttributes(InventoryProvider inventoryProvider,
            Order order)
        {
            return inventoryProvider.Contents.ItemsByQuantity.Keys
                .Where(item => item as ClothingAttributes != null)
                .Cast<ClothingAttributes>().FirstOrDefault(order.MeetsOrderDemand);
        }

        #endregion
    }
}