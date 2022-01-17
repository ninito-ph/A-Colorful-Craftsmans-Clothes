using System;
using Game.Runtime.Core;
using Game.Runtime.Data.Attributes;
using Game.Runtime.Entities.Player;
using Game.Runtime.Systems.Inventory;
using Ninito.UnityExtended.WindowManager;
using UnityEngine;

namespace Game.Runtime.Systems.Clothing.Wardrobe
{
    /// <summary>
    /// A class that manages the wardrobe and switching of clothing.
    /// </summary>
    public sealed class WardrobeManager : MonoBehaviour
    {
        #region Private Fields

        [Header("Dependencies")]
        [SerializeField]
        private WardrobeItemSelector accessorySelector;

        [SerializeField]
        private WardrobeItemSelector bodySelector;

        [SerializeField]
        private WindowManager windowManager;
        
        [SerializeField]
        private string onCloseMenuKey = "HUD";

        #endregion

        #region Properties

        public ItemInventory ClothingSource { get; set; }
        public DressableEntity DressableEntity { get; set; }

        #endregion

        #region Unity Callbacks

        private void OnEnable()
        {
            GameManager.Instance.SetPlayerInputEnabled(false);
        }

        private void OnDisable()
        {
            ClothingSource = null;
            DressableEntity = null;
            GameManager.Instance.SetPlayerInputEnabled(true);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Receives a player and extracts necessary dependencies
        /// </summary>
        /// <param name="player">The player to draw dependencies from</param>
        public void ReceivePlayer(GameObject player)
        {
            ClothingSource = player.GetComponent<PlayerInventory>().Contents;
            DressableEntity = player.GetComponent<DressableEntity>();
            
            InjectDependenciesInto(accessorySelector);
            InjectDependenciesInto(bodySelector);
        }

        /// <summary>
        /// Closes the wardrobe
        /// </summary>
        public void CloseWardrobe()
        {
            windowManager.SwitchToMenu(onCloseMenuKey);
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Injects dependencies into the item selector
        /// </summary>
        /// <param name="selector">The selector to inject dependencies into</param>
        private void InjectDependenciesInto(WardrobeItemSelector selector)
        {
            selector.BoundSlot = selector.SelectorType == ClothingType.Accessory
                ? DressableEntity.AccessorySlot
                : DressableEntity.BodySlot;
            
            selector.SetClothingOptions(ClothingSource.ItemsByQuantity.Keys);
        }

        #endregion
    }
}