using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime.Systems.Save.Saves
{
    /// <summary>
    /// A class that holds permanent data about the player's inventory
    /// </summary>
    [Serializable]
    public class InventorySave
    {
        #region Public Fields

        [SerializeField]
        public List<string> StoredInventoryItems;

        [SerializeField]
        public List<int> InventoryItemQuantities;

        #endregion

        #region Constructors

        public InventorySave(List<string> storedInventoryItems, List<int> inventoryItemQuantities)
        {
            StoredInventoryItems = storedInventoryItems;
            InventoryItemQuantities = inventoryItemQuantities;
        }

        #endregion
    }
}