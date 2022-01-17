using Game.Runtime.Data.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime.Systems.Inventory
{
    /// <summary>
    /// A class that manages a view of an item in the inventory
    /// </summary>
    public sealed class InventoryItemEntryView : MonoBehaviour
    {
        #region Private Fields

        [Header("Dependencies")]
        [SerializeField]
        private Image icon;

        [SerializeField]
        private TMP_Text nameText;

        [SerializeField]
        private TMP_Text countText;

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the view's item
        /// </summary>
        /// <param name="item">The item to display on the view</param>
        /// <param name="quantity">The quantity of the item to display</param>
        public void SetViewItem(ItemAttributes item, int quantity)
        {
            icon.sprite = item.Graphic;
            icon.color = item.Color;
            nameText.text = item.Name;
            countText.text = quantity.ToString();
        }

        #endregion
    }
}