using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime.Systems.Inventory
{
    /// <summary>
    /// A class that manages the view of the player's inventory.
    /// </summary>
    public sealed class PlayerInventoryView : MonoBehaviour
    {
        #region Private Fields

        [Header("Dependencies")]
        [SerializeField]
        private PlayerInventory inventory;

        [SerializeField]
        private GameObject itemEntryViewPrefab;

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            inventory.OnInventoryModified += UpdateInventoryView;
        }

        private void OnDestroy()
        {
            inventory.OnInventoryModified -= UpdateInventoryView;
        }

        #endregion
        
        #region Public Methods

        /// <summary>
        /// Updates the inventory view by clearing and painting it again
        /// </summary>
        private void UpdateInventoryView(IItem item, int quantity)
        {
            ClearInventoryView();
            PaintInventoryView();
        }

        /// <summary>
        /// Paints the inventory view
        /// </summary>
        private void PaintInventoryView()
        {
            foreach (KeyValuePair<IItem, int> itemAndQuantity in inventory.ItemsByQuantity)
            {
                PaintItemEntryView(itemAndQuantity.Key, itemAndQuantity.Value);
            }
        }

        /// <summary>
        /// Paints a single item's entry
        /// </summary>
        /// <param name="item">The item to paint the entry with</param>
        /// <param name="quantity">The quantity of the item to paint</param>
        private void PaintItemEntryView(IItem item, int quantity)
        {
            GameObject itemEntryView = Instantiate(itemEntryViewPrefab, transform);
            itemEntryView.GetComponent<InventoryItemEntryView>().SetViewItem(item, quantity);
        }

        /// <summary>
        /// Clears all item views from the inventory.
        /// </summary>
        private void ClearInventoryView()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
        
        #endregion
    }
}