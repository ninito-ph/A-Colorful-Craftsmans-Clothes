using System;
using System.Collections.Generic;
using Game.Runtime.Systems.Orders;
using UnityEngine;

namespace Game.Runtime.Systems.Save.Saves
{
    /// <summary>
    /// A class that contains permanent data for the <see cref="OrderManager"/>
    /// </summary>
    [Serializable]
    public sealed class OrderManagerSave
    {
        #region Public Fields

        [SerializeField]
        public List<string> Orders;

        [SerializeField]
        public int OrdersComplete;

        #endregion

        #region Constructors

        public OrderManagerSave(List<string> orders, int ordersComplete)
        {
            Orders = orders;
            OrdersComplete = ordersComplete;
        }

        #endregion
    }
}