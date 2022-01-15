using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime.Systems.Save
{
    /// <summary>
    /// A class that represents a save file.
    /// </summary>
    [Serializable]
    public class GameSave
    {
        // TODO: make these two lists a single class
        [SerializeField]
        public List<int> InventoryItemHashes;
        [SerializeField]
        public List<int> InventoryItemQuantities;

        public GameSave(List<int> inventoryItemHashes, List<int> inventoryItemQuantities)
        {
            InventoryItemHashes = inventoryItemHashes;
            InventoryItemQuantities = inventoryItemQuantities;
        }
    }
}