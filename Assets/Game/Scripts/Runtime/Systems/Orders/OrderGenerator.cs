using System.Linq;
using Game.Runtime.Data.Attributes;
using Game.Runtime.Data.Registries;
using Game.Runtime.Utility;
using Ninito.TheUsualSuspects.Attributes;
using Ninito.UsualSuspects.CommonExtensions;
using UnityEngine;

namespace Game.Runtime.Systems.Orders
{
    /// <summary>
    /// A class that randomly generates orders
    /// </summary>
    [CreateAssetMenu(fileName = CreateMenus.ORDER_GENERATOR_FILENAME,
        menuName = CreateMenus.ORDER_GENERATOR_MENUNAME, order = CreateMenus.ORDER_GENERATOR_ORDER)]
    public sealed class OrderGenerator : ScriptableObject
    {
        #region Private Fields

        [Header("Configurations")]
        [SerializeField]
        [MinMaxSlider(1, 10)]
        [Tooltip("The minimum and maximum amount of time in minutes the play may have to finish an order")]
        private Vector2 orderTimeRange = new Vector2(1, 3);

        [SerializeField]
        [MinMaxSlider(0f, 1f)]
        [Tooltip("The darkest value a generated order can have, and the lightest value a generated order can have")]
        private Vector2 minMaxColorValues = new Vector2(0.3f, 0.9f);

        [SerializeField]
        [Range(0f, 1f)]
        [Tooltip("How close to the order's color a received item must be to be considered a match")]
        private float colorTolerance;

        [Header("Dependencies")]
        [SerializeField]
        private ItemRegistry itemRegistry;

        #endregion

        #region Public Methods

        /// <summary>
        /// Generates a random order
        /// </summary>
        /// <returns>A random order</returns>
        public Order GenerateOrder()
        {
            return Order.CreateNew(DrawRandomClothingItem(),
                Random.Range(orderTimeRange.x * 60, orderTimeRange.y * 60), colorTolerance);
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Draws a random clothing item from the item registry with a random color
        /// </summary>
        /// <returns>A random clothing item with a random color</returns>
        private ClothingAttributes DrawRandomClothingItem()
        {
            ClothingAttributes[] clothingAttributes = itemRegistry.GetAllItemsOfType<ClothingAttributes>();
            ClothingAttributes item = clothingAttributes[Random.Range(0, clothingAttributes.Length)].Clone();
            item.Color = RGBandCMYKUtility.GenerateRandomRGBColor(minMaxColorValues.x, minMaxColorValues.y);
            return item;
        }

        #endregion
    }
}