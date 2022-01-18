using UnityEngine;

namespace Game.Runtime.Systems.Inventory
{
    /// <summary>
    /// A simple base class for all other classes that provide an inventory
    /// </summary>
    public abstract class InventoryProvider : MonoBehaviour
    {
        #region Properties

        public abstract ItemInventory Contents { get; }

        #endregion
    }
}