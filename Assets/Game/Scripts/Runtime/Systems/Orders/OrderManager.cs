using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime.Systems.Orders
{
    /// <summary>
    /// A class that manages clothing orders
    /// </summary>
    public sealed class OrderManager : MonoBehaviour
    {
        #region Private Fields

        [Header("Configurations")]
        [SerializeField]
        private int maxOrdersAtATime = 3;

        [Header("Dependencies")]
        [SerializeField]
        private OrderGenerator orderGenerator;

        [SerializeField]
        private List<Order> _orders;

        private Coroutine _checkExpirationRoutine;

        #endregion

        #region Properties

        public List<Order> Orders
        {
            get => _orders;
            private set => _orders = value;
        }

        public Action OnOrdersUpdated { get; set; }
        public Action OnOrderCompleted { get; set; }
        public Action OnOrderExpired { get; set; }

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            GenerateNewOrders();
            _checkExpirationRoutine = StartCoroutine(MonitorOrders());
            OnOrdersUpdated += HandleOrderProgress;
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Generates new orders
        /// </summary>
        private void GenerateNewOrders()
        {
            Orders = new List<Order>(maxOrdersAtATime);

            for (int index = 0; index < maxOrdersAtATime; index++)
            {
                Orders.Add(orderGenerator.GenerateOrder());
            }

            OnOrdersUpdated?.Invoke();
        }

        /// <summary>
        /// Adds new orders if there are no orders left
        /// </summary>
        private void HandleOrderProgress()
        {
            if (Orders == null || Orders.Count > 0) return;
            GenerateNewOrders();
        }

        /// <summary>
        /// Checks for expired and completed orders every second
        /// </summary>
        private IEnumerator MonitorOrders()
        {
            WaitForSeconds wait = new WaitForSeconds(1f);

            // Since there are no allocations here, I don't think this will cause any issues
            while (true)
            {
                yield return wait;

                for (int index = Orders.Count - 1; index >= 0; index--)
                {
                    HandleExpiredOrder(index);
                    HandleCompleteOrder(index);
                }
            }
        }

        /// <summary>
        /// Handles the order if it is expired
        /// </summary>
        /// <param name="index">The index of the order to check</param>
        /// <returns>True if the order was expired, false if not</returns>
        private void HandleExpiredOrder(int index)
        {
            if (index < 0 || index >= Orders.Count) return;
            if (!Orders[index].IsExpired) return;
            Orders.RemoveAt(index);
            OnOrdersUpdated?.Invoke();
            OnOrderExpired?.Invoke();
        }

        /// <summary>
        /// Handles the order if it is completed
        /// </summary>
        /// <param name="index">The index of the order to check</param>
        private void HandleCompleteOrder(int index)
        {
            if (index < 0 || index >= Orders.Count) return;
            if (!Orders[index].IsCompleted) return;
            Orders.RemoveAt(index);
            OnOrdersUpdated?.Invoke();
            OnOrderCompleted?.Invoke();
        }

        #endregion
    }
}