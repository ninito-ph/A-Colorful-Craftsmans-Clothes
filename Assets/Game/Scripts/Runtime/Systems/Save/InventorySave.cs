using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime.Systems.Save
{
    /// <summary>
    /// A class that holds permanent data about the player's inventory
    /// </summary>
    [Serializable]
    public class InventorySave
    {
        [SerializeField]
        public List<int> InventoryItemHashes;

        [SerializeField]
        public List<int> InventoryItemQuantities;

        public InventorySave(List<int> inventoryItemHashes, List<int> inventoryItemQuantities)
        {
            InventoryItemHashes = inventoryItemHashes;
            InventoryItemQuantities = inventoryItemQuantities;
        }
    }
}