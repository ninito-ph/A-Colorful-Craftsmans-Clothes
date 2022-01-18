using System;
using System.Collections.Generic;
using System.Linq;
using Game.Runtime.Data.Attributes;
using Game.Runtime.Data.Registries;
using Game.Runtime.Systems.Save;
using UnityEngine;

namespace Game.Runtime.Systems.Inventory
{
    /// <summary>
    /// A class that manages an inventory of items.
    /// </summary>
    [Serializable]
    public sealed class ItemInventory
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

        public string SaveKey { get; set; }

        public Dictionary<ItemAttributes, int> ItemsByQuantity { get; private set; } =
            new Dictionary<ItemAttributes, int>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds everything from another inventory
        /// </summary>
        /// <param name="inventory">The inventory to add from</param>
        public void AddEverythingFrom(ItemInventory inventory)
        {
            foreach (KeyValuePair<ItemAttributes, int> item in inventory.ItemsByQuantity)
            {
                AddItem(item.Key, item.Value);
            }
        }

        /// <summary>
        /// Adds an item to the inventory.
        /// </summary>
        /// <param name="item">The item to add</param>
        /// <param name="quantity">The quantity to add</param>
        /// <exception cref="ArgumentOutOfRangeException">Exception thrown if trying to add a negative quantity</exception>
        public void AddItem(ItemAttributes item, int quantity = 1)
        {
            if (item == null) return;
            
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
            if (item == null || !ItemsByQuantity.ContainsKey(item)) return false;
            if (ItemsByQuantity[item] < quantity) return false;

            ItemsByQuantity[item] -= quantity;

            if (ItemsByQuantity[item] == 0)
            {
                ItemsByQuantity.Remove(item);
            }

            OnInventoryModified?.Invoke(item, -quantity);
            return true;
        }

        /// <summary>
        /// Checks whether the player has the given amount of the given item
        /// </summary>
        /// <param name="item">The item to check the quantity of</param>
        /// <param name="quantity">The minimum quantity</param>
        /// <returns>Whether the player has the specified quantity (default 1) of the given item</returns>
        public bool HasItem(ItemAttributes item, int quantity = 1)
        {
            if (!ItemsByQuantity.ContainsKey(item)) return false;
            return ItemsByQuantity[item] >= quantity;
        }

        /// <summary>
        /// Saves the inventory
        /// </summary>
        public void SaveInventory()
        {
            List<string> storedItems = ItemsByQuantity.Select(pair => pair.Key.Store()).ToList();
            List<int> itemQuantities = ItemsByQuantity.Select(pair => pair.Value).ToList();

            InventorySave save = new InventorySave(storedItems, itemQuantities);
            DataSaver.SaveData(save, SaveKey);
        }

        /// <summary>
        /// Loads the inventory
        /// </summary>
        public void LoadInventory()
        {
            if (!DataLoader.TryLoad(SaveKey, out InventorySave save)) return;

            ItemsByQuantity = new Dictionary<ItemAttributes, int>();

            for (int index = 0; index < save.InventoryItemQuantities.Count; index++)
            {
                ItemAttributes item = ItemAttributes.Restore(save.StoredInventoryItems[index], itemRegistry);
                AddItem(item, save.InventoryItemQuantities[index]);
            }
        }

        #endregion
    }
}