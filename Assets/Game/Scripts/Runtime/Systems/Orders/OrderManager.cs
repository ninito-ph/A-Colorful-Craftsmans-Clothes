using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Runtime.Systems.Save;
using Game.Runtime.Systems.Save.Saves;
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

        public List<Order> CompleteOrders { get; set; } = new List<Order>();

        public Action OnOrdersUpdated { get; set; }
        public Action OnOrderCompleted { get; set; }
        public Action OnOrderExpired { get; set; }

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            if (!TryLoad() || Orders == null || Orders.Count == 0)
            {
                GenerateNewOrders();
            }

            _checkExpirationRoutine = StartCoroutine(MonitorOrders());
            OnOrdersUpdated += HandleOrderProgress;
        }

        private void OnDestroy()
        {
            OnOrdersUpdated -= HandleOrderProgress;
            Save();
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
            if (index < 0 || index >= Orders.Count || !Orders[index].IsExpired) return;
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
            if (index < 0 || index >= Orders.Count || !Orders[index].IsCompleted) return;
            CompleteOrders.Add(Orders[index]);
            Orders.RemoveAt(index);
            OnOrdersUpdated?.Invoke();
            OnOrderCompleted?.Invoke();
        }

        /// <summary>
        /// Saves all orders to an <see cref="OrderManagerSave"/> in a key with the same name as the class
        /// </summary>
        private void Save()
        {
            List<string> orderStrings = _orders.Select(order => order.Store()).ToList();
            List<string> completeOrders = CompleteOrders.Select(order => order.Store()).ToList();
            OrderManagerSave save = new OrderManagerSave(orderStrings, completeOrders);
            DataSaver.SaveData(save, nameof(OrderManager));
        }

        /// <summary>
        /// Loads all orders from the proper Save Key.
        /// </summary>
        /// <returns>Whether anything was loaded from the Save Key</returns>
        private bool TryLoad()
        {
            if (!DataLoader.TryLoad(nameof(OrderManager), out OrderManagerSave save)) return false;

            Orders = RestoreOngoingOrders(save);
            CompleteOrders = RestoreLastFiveCompleteOrders(save);

            if (Orders.All(order => order == null))
            {
                Debug.LogError("All loaded orders were null!");
            }

            OnOrdersUpdated?.Invoke();
            return true;
        }

        /// <summary>
        /// Restores the last five complete orders through the given save
        /// </summary>
        /// <param name="save">The save to read from</param>
        /// <returns>The last five or less complete orders</returns>
        private List<Order> RestoreLastFiveCompleteOrders(OrderManagerSave save)
        {
            return save.LastFiveCompleteOrders
                .Select(orderString => Order.Restore(orderString, orderGenerator.ItemRegistry))
                .Where(order => order != null).ToList();
        }

        /// <summary>
        /// Restores ongoing orders from the given save
        /// </summary>
        /// <param name="save">The save the read from</param>
        /// <returns>The ongoing orders when the game was last saved</returns>
        private List<Order> RestoreOngoingOrders(OrderManagerSave save)
        {
            return save.Orders
                .Select(orderString => Order.Restore(orderString, orderGenerator.ItemRegistry))
                .Where(order => order != null).ToList();
        }

        #endregion
    }
}