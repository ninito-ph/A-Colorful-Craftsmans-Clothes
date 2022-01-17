using Game.Runtime.Systems.Inventory;
using UnityEngine;

namespace Game.Runtime.Entities.Player
{
    /// <summary>
    /// A class that controls the player's inventory
    /// </summary>
    public sealed class PlayerInventory : MonoBehaviour
    {
        #region Private Fields

        [Header("Configuration")]
        [SerializeField]
        private bool saveAndLoad = true;

        [SerializeField]
        private ItemInventory _contents = new ItemInventory();

        #endregion

        #region Properties

        public ItemInventory Contents => _contents;

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            if (!saveAndLoad) return;
            Contents.LoadInventory();
        }

        private void OnDestroy()
        {
            if (!saveAndLoad) return;
            Contents.SaveInventory();
        }

        #endregion
    }
}