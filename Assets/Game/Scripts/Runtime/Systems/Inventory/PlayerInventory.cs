using System;
using System.Collections.Generic;
using System.Linq;
using Game.Runtime.Data.Attributes;
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

        #endregion

        #region Properties

        /// <summary>
        /// ItemAttributes argument represents the last modified item, int represents the quantity of the item.
        /// </summary>
        public Action<ItemAttributes, int> OnInventoryModified { get; set; }

        public SerializedDictionary<ItemAttributes, int> ItemsByQuantity { get; private set; } =
            new SerializedDictionary<ItemAttributes, int>();

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
        public void AddItem(ItemAttributes item, int quantity = 1)
        {
            if (quantity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), quantity,
                    "Quantity must be greater than or equal to zero.");
            }

            if (ItemsByQuantity.ContainsKey(item))
            {
                ItemsByQuantity[item] += quantity;
            }
            else
            {
                ItemsByQuantity.Add(item, quantity);
            }

            OnInventoryModified?.Invoke(item, quantity);
        }

        /// <summary>
        /// Attempts to remove a given quantity of an item from the inventory.
        /// </summary>
        /// <param name="item">The item to remove</param>
        /// <param name="quantity">The quantity to try to remove</param>
        /// <returns>Whether the specified quantity of the given item was removed</returns>
        public bool TryRemoveItem(ItemAttributes item, int quantity = 1)
        {
            if (!ItemsByQuantity.ContainsKey(item)) return false;
            if (ItemsByQuantity[item] < quantity) return false;

            ItemsByQuantity[item] -= quantity;

            if (ItemsByQuantity[item] == 0)
            {
                ItemsByQuantity.Remove(item);
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
            List<int> itemHashes = ItemsByQuantity.Select(pair => pair.Key.GetHashCode()).ToList();
            List<int> itemQuantities = ItemsByQuantity.Select(pair => pair.Value).ToList();

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
            ItemsByQuantity = new SerializedDictionary<ItemAttributes, int>();

            for (int index = 0; index < save.InventoryItemQuantities.Count; index++)
            {
                if (itemRegistry.GetItemByHash(save.InventoryItemHashes[index]) == null)
                {
                    Debug.LogError("Could not restore an item! Item hash: " + save.InventoryItemHashes[index]);
                    continue;
                }

                ItemsByQuantity.Add(itemRegistry.GetItemByHash(save.InventoryItemHashes[index]),
                    save.InventoryItemQuantities[index]);
            }

            if (ItemsByQuantity.Count <= 0) return;
            OnInventoryModified?.Invoke(null, 0);
        }

        #endregion
    }
}