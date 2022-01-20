using Game.Runtime.Data.Attributes;
using Game.Runtime.Systems.Clothing;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Runtime.Systems.Orders
{
    /// <summary>
    /// A class that controls a customer; it sets the entity's clothing to a completed order
    /// </summary>
    [RequireComponent(typeof(DressableEntity))]
    public sealed class Customer : MonoBehaviour
    {
        #region Private Fields

        [Header("Dependencies")]
        [SerializeField]
        private OrderManager orderManager;

        private DressableEntity _dressableEntity;

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            TryGetComponent(out _dressableEntity);
            SetClothing();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets the dressable entity's clothing to a completed order
        /// </summary>
        private void SetClothing()
        {
            ClothingAttributes clothing = PickRandomClothing();
            if (clothing == null) return;

            if (clothing.Type == ClothingType.Body)
            {
                _dressableEntity.BodySlot.SetArticle(clothing);
            }
            else
            {
                _dressableEntity.AccessorySlot.SetArticle(clothing);
            }
        }

        /// <summary>
        /// Picks a random piece of clothing from the list of complete orders
        /// </summary>
        /// <returns>A random piece of clothing from the list of complete orders</returns>
        private ClothingAttributes PickRandomClothing()
        {
            return orderManager.CompleteOrders.Count == 0
                ? null
                : orderManager.CompleteOrders[Random.Range(0, orderManager.CompleteOrders.Count)].Item;
        }

        #endregion
    }
}