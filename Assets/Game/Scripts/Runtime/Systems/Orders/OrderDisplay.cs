using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime.Systems.Orders
{
    /// <summary>
    /// A class that controls a single order's display
    /// </summary>
    public sealed class OrderDisplay : MonoBehaviour
    {
        #region Private Fields

        [Header("Dependencies")]
        [SerializeField]
        private TMP_Text itemName;

        [SerializeField]
        private TMP_Text orderTime;

        [SerializeField]
        private Image itemIcon;

        private Coroutine _updateTimerDisplayRoutine;

        #endregion

        #region Properties

        public Order Order { get; set; }

        #endregion

        #region Unity Callbacks

        private void OnEnable()
        {
            _updateTimerDisplayRoutine = StartCoroutine(UpdateTimerDisplay());
        }

        private void OnDisable()
        {
            if (_updateTimerDisplayRoutine != null)
            {
                StopCoroutine(_updateTimerDisplayRoutine);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the display's order
        /// </summary>
        /// <param name="order">The order to display</param>
        public void SetOrder(Order order)
        {
            Order = order;
            itemName.text = Order.Item.Name;
            itemIcon.sprite = Order.Item.Graphic;
            itemIcon.color = Order.Item.Color;
            // ReSharper disable once SpecifyACultureInStringConversionExplicitly
            orderTime.text = GetFormattedOrderTime();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the order's time display
        /// </summary>
        private IEnumerator UpdateTimerDisplay()
        {
            WaitForSeconds wait = new WaitForSeconds(1);

            while (true)
            {
                if (Order == null)
                {
                    yield return wait;
                    continue;
                }

                orderTime.text = GetFormattedOrderTime();
                yield return wait;
            }
        }

        /// <summary>
        /// Format's the order's time to a pretty m:ss format
        /// </summary>
        /// <returns>A string of the order's time in m:ss format</returns>
        private string GetFormattedOrderTime()
        {
            int minutes = (int)Order.RemainingTime / 60;
            int seconds = (int)Order.RemainingTime - minutes * 60;
            string timeLeft = $"{minutes:0}:{seconds:00}";
            return timeLeft;
        }

        #endregion
    }
}