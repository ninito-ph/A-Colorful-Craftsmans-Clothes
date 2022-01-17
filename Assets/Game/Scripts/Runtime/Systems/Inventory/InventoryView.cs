using System.Collections.Generic;
using Game.Runtime.Data.Attributes;
using Game.Runtime.Entities.Player;
using UnityEngine;

namespace Game.Runtime.Systems.Inventory
{
    /// <summary>
    /// A class that manages the view of the player's inventory.
    /// </summary>
    public sealed class InventoryView : MonoBehaviour
    {
        #region Private Fields

        [Header("Dependencies")]
        [SerializeField]
        private PlayerInventory playerInventory;

        [SerializeField]
        private GameObject itemEntryViewPrefab;
        
        [SerializeField]
        private GameObject itemEntryViewContainer;

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            playerInventory.Contents.OnInventoryModified += UpdateInventoryView;
            PaintInventoryView();
        }

        private void OnDestroy()
        {
            playerInventory.Contents.OnInventoryModified -= UpdateInventoryView;
        }

        #endregion
        
        #region Public Methods

        /// <summary>
        /// Updates the inventory view by clearing and painting it again
        /// </summary>
        private void UpdateInventoryView(ItemAttributes item, int quantity)
        {
            ClearInventoryView();
            PaintInventoryView();
        }

        /// <summary>
        /// Paints the inventory view
        /// </summary>
        private void PaintInventoryView()
        {
            foreach (KeyValuePair<ItemAttributes, int> itemAndQuantity in playerInventory.Contents.ItemsByQuantity)
            {
                PaintItemEntryView(itemAndQuantity.Key, itemAndQuantity.Value);
            }
        }

        /// <summary>
        /// Paints a single item's entry
        /// </summary>
        /// <param name="item">The item to paint the entry with</param>
        /// <param name="quantity">The quantity of the item to paint</param>
        private void PaintItemEntryView(ItemAttributes item, int quantity)
        {
            GameObject itemEntryView = Instantiate(itemEntryViewPrefab, itemEntryViewContainer.transform);
            itemEntryView.GetComponent<InventoryItemEntryView>().SetViewItem(item, quantity);
        }

        /// <summary>
        /// Clears all item views from the inventory.
        /// </summary>
        private void ClearInventoryView()
        {
            foreach (Transform child in itemEntryViewContainer.transform)
            {
                Destroy(child.gameObject);
            }
        }
        
        #endregion
    }
}