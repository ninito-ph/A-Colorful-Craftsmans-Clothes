using System;
using UnityEngine;

namespace Game.Runtime.Systems.Orders
{
    /// <summary>
    /// A class that manages a list view of all orders
    /// </summary>
    public sealed class OrderListView : MonoBehaviour
    {
        #region Private Fields

        [Header("Dependencies")]
        [SerializeField]
        private OrderManager orderManager;
        [SerializeField]
        private GameObject orderListItemPrefab;
        [SerializeField]
        private Transform orderItemContainer;

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            orderManager.OnOrdersUpdated += UpdateOrderList;
            UpdateOrderList();
        }

        private void OnEnable()
        {
            UpdateOrderList();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Adds an order to the list
        /// </summary>
        /// <param name="order"></param>
        private void AddOrder(Order order)
        {
            GameObject orderListItem = Instantiate(orderListItemPrefab, orderItemContainer);
            orderListItem.GetComponent<OrderDisplay>().SetOrder(order);
        }

        /// <summary>
        /// Updates the order list by clearing old orders and adding new ones
        /// </summary>
        private void UpdateOrderList()
        {
            if (gameObject.activeSelf == false) return;
            ClearOrderList();
            FillOderList();
        }
        
        /// <summary>
        /// Clears the order list fo all items
        /// </summary>
        private void ClearOrderList()
        {
            foreach (Transform child in orderItemContainer)
            {
                Destroy(child.gameObject);
            }
        }

        /// <summary>
        /// Adds all orders from the order manager to the list
        /// </summary>
        private void FillOderList()
        {
            foreach (Order order in orderManager.Orders)
            {
                AddOrder(order);
            }
        }

        #endregion
    }
}