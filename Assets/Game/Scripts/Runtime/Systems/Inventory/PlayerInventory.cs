using System;
using System.Collections.Generic;
using System.Linq;
using Game.Runtime.Systems.Save;
using Ninito.UsualSuspects;
using UnityEngine;

namespace Game.Runtime.Systems.Inventory
{
    /// <summary>
    /// A class that manages the player's inventory.
    /// </summary>
    public sealed class PlayerInventory : MonoBehaviour
    {
        #region Private Fields

        [SerializeField]
        private ItemRegistry itemRegistry;
        
        private SerializedDictionary<IItem, int> _itemsByQuantity = new SerializedDictionary<IItem, int>();

        #endregion

        #region Properties

        /// <summary>
        /// IItem argument represents the last modified item, int represents the quantity of the item.
        /// </summary>
        public Action<IItem, int> OnInventoryModified { get; set; }

        public SerializedDictionary<IItem, int> ItemsByQuantity => _itemsByQuantity;

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            LoadInventory();
        }

        private void OnDestroy()
        {
            SaveInventory();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds an item to the inventory.
        /// </summary>
        /// <param name="item">The item to add</param>
        /// <param name="quantity">The quantity to add</param>
        /// <exception cref="ArgumentOutOfRangeException">Exception thrown if trying to add a negative quantity</exception>
        public void AddItem(IItem item, int quantity = 1)
        {
            if (quantity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), quantity,
                    "Quantity must be greater than or equal to zero.");
            }

            if (_itemsByQuantity.ContainsKey(item))
            {
                _itemsByQuantity[item] += quantity;
            }
            else
            {
                _itemsByQuantity.Add(item, quantity);
            }
            
            OnInventoryModified?.Invoke(item, quantity);
        }

        /// <summary>
        /// Attempts to remove a given quantity of an item from the inventory.
        /// </summary>
        /// <param name="item">The item to remove</param>
        /// <param name="quantity">The quantity to try to remove</param>
        /// <returns>Whether the specified quantity of the given item was removed</returns>
        public bool TryRemoveItem(IItem item, int quantity = 1)
        {
            if (!_itemsByQuantity.ContainsKey(item)) return false;
            if (_itemsByQuantity[item] < quantity) return false;

            _itemsByQuantity[item] -= quantity;

            if (_itemsByQuantity[item] == 0)
            {
                _itemsByQuantity.Remove(item);
            }

            OnInventoryModified?.Invoke(item, -quantity);
            return true;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Saves the inventory
        /// </summary>
        private void SaveInventory()
        {
            List<int> itemHashes = _itemsByQuantity.Select(pair => pair.Key.GetHashCode()).ToList();
            List<int> itemQuantities = _itemsByQuantity.Select(pair => pair.Value).ToList();

            // TODO: The inventory should only provide the inventory save class, refactor this later
            InventorySave save = new InventorySave(itemHashes, itemQuantities);
            PlayerPrefs.SetString("InventorySave", JsonUtility.ToJson(save));
        }

        /// <summary>
        /// Loads the inventory
        /// </summary>
        private void LoadInventory()
        {
            if (!PlayerPrefs.HasKey("InventorySave")) return;

            InventorySave save = JsonUtility.FromJson<InventorySave>(PlayerPrefs.GetString("InventorySave"));
            _itemsByQuantity = new SerializedDictionary<IItem, int>();

            for (int index = 0; index < save.InventoryItemQuantities.Count; index++)
            {
                if (itemRegistry.GetItemByHash(save.InventoryItemHashes[index]) == null)
                {
                    Debug.LogError("Could not restore an item! Item hash: " + save.InventoryItemHashes[index]);
                    continue;
                }
                
                _itemsByQuantity.Add(itemRegistry.GetItemByHash(save.InventoryItemHashes[index]),
                    save.InventoryItemQuantities[index]);
            }
        }

        #endregion
    }
}