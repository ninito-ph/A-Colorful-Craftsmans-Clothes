using System;
using Game.Runtime.Data.Attributes;
using Game.Runtime.Systems.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime.Systems.Clothing.Dyeing
{
    /// <summary>
    /// A class that controls a single button relating to a single dye
    /// </summary>
    [RequireComponent(typeof(Button))]
    public sealed class DyeButton : MonoBehaviour
    {
        #region Private Fields

        [Header("Configuration")]
        [SerializeField]
        private ItemAttributes dye;
        [SerializeField]
        private Button button;

        private ItemInventory _playerInventory;
        
        #endregion
        
        #region Properties

        public ItemInventory PlayerInventory
        {
            get => _playerInventory;
            set
            {
                _playerInventory = value;
                UpdateButtonInteractability();
            } 
        }

        #endregion

        #region Unity Callbacks

        private void OnEnable()
        {
            button ??= GetComponent<Button>();
            button.onClick.AddListener(UpdateButtonInteractability);
        }

        #endregion
        
        #region Private Methods

        /// <summary>
        /// Updates the button's interactability based on the player's inventory
        /// </summary>
        private void UpdateButtonInteractability()
        {
            button.interactable = CheckIfDyeIsAvailable();
        }
        
        /// <summary>
        /// Checks if the player has the dye in their inventory
        /// </summary>
        /// <returns>Whether the player has the dye in their inventory</returns>
        private bool CheckIfDyeIsAvailable()
        {
            return _playerInventory.HasItem(dye);
        }

        #endregion
    }
}