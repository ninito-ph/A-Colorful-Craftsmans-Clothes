using Game.Runtime.Data.Attributes;
using Game.Runtime.Systems.Inventory;
using Ninito.UnityExtended.WindowManager;
using UnityEngine;
using UnityEngine.UI;
using GameManager = Game.Runtime.Core.GameManager;

namespace Game.Runtime.Systems.Clothing.Dyeing
{
    /// <summary>
    /// A class that manages the dyeing process at a high level
    /// </summary>
    public sealed class DyeingManager : MonoBehaviour
    {
        #region Private Fields

        [Header("Dependencies")]
        [SerializeField]
        private DyeingItemSelector itemSelector;

        [SerializeField]
        private DyeButton[] dyeButtons;

        [SerializeField]
        private Button craftButton;

        [SerializeField]
        private WindowManager windowManager;

        [SerializeField]
        private string onCloseMenuKey;

        [Header("Configurations")]
        [SerializeField]
        [Range(0f, 1f)]
        private float defaultDyeAmount = 0.1f;

        private DyeingOperation _currentOperation;

        #endregion

        #region Properties

        public ItemInventory PlayerInventory { get; set; }

        #endregion

        #region Unity Callbacks

        private void OnEnable()
        {
            InjectPlayerInventoryIntoButtons();
            GameManager.Instance.SetPlayerInputEnabled(false);
        }

        private void OnDisable()
        {
            PlayerInventory = null;
            CancelDyeingOperation();
            GameManager.Instance.SetPlayerInputEnabled(true);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds cyan dye to the ongoing operation
        /// </summary>
        /// <param name="dye">The item to consume in the dyeing process</param>
        public void AddCyan(ItemAttributes dye)
        {
            EnsureDyeingOperationExists();

            if (PlayerInventory.TryRemoveItem(dye))
            {
                _currentOperation.DyeWith(dye, new Vector4(defaultDyeAmount, 0f, 0f, 0f));
            }
        }

        /// <summary>
        /// Adds magenta dye to the ongoing operation
        /// </summary>
        /// <param name="dye">The item to consume in the dyeing process</param>
        public void AddMagenta(ItemAttributes dye)
        {
            EnsureDyeingOperationExists();

            if (PlayerInventory.TryRemoveItem(dye))
            {
                _currentOperation.DyeWith(dye, new Vector4(0f, defaultDyeAmount, 0f, 0f));
            }
        }

        /// <summary>
        /// Adds yellow dye to the ongoing operation
        /// </summary>
        /// <param name="dye">The item to consume in the dyeing process</param>
        public void AddYellow(ItemAttributes dye)
        {
            EnsureDyeingOperationExists();

            if (PlayerInventory.TryRemoveItem(dye))
            {
                _currentOperation.DyeWith(dye, new Vector4(0f, 0f, defaultDyeAmount, 0f));
            }
        }

        /// <summary>
        /// Adds key dye to the ongoing operation
        /// </summary>
        /// <param name="dye">The item to consume in the dyeing process</param>
        public void AddKey(ItemAttributes dye)
        {
            EnsureDyeingOperationExists();

            if (PlayerInventory.TryRemoveItem(dye))
            {
                _currentOperation.DyeWith(dye, new Vector4(0f, 0f, 0f, defaultDyeAmount));
            }
        }

        /// <summary>
        /// Cancels the existing dyeing operation
        /// </summary>
        public void CancelDyeingOperation()
        {
            if (_currentOperation == null) return;

            PlayerInventory.AddEverythingFrom(_currentOperation.CancelOperation());
            _currentOperation = null;
            craftButton.interactable = false;
        }

        /// <summary>
        /// Concludes the existing dyeing operation
        /// </summary>
        public void ConcludeDyeingOperation()
        {
            if (_currentOperation == null) return;

            PlayerInventory.AddItem(_currentOperation.ConcludeOperation());
            _currentOperation = null;
            craftButton.interactable = false;

            windowManager.SwitchToMenu(onCloseMenuKey);
        }

        /// <summary>
        /// Closes the dyeing menu
        /// </summary>
        public void CloseMenu()
        {
            windowManager.SwitchToMenu(onCloseMenuKey);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates a dyeing operation if one does not exist
        /// </summary>
        private void EnsureDyeingOperationExists()
        {
            if (_currentOperation == null)
            {
                StartDyeingOperation();
            }
        }

        /// <summary>
        /// Starts a new dyeing operation
        /// </summary>
        private void StartDyeingOperation()
        {
            _currentOperation = new DyeingOperation(itemSelector.CurrentItem);
            itemSelector.HandleOperation(_currentOperation);
            craftButton.interactable = true;
        }

        /// <summary>
        /// Injects the player inventory into dye buttons
        /// </summary>
        private void InjectPlayerInventoryIntoButtons()
        {
            foreach (DyeButton button in dyeButtons)
            {
                button.PlayerInventory = PlayerInventory;
            }
        }

        #endregion
    }
}