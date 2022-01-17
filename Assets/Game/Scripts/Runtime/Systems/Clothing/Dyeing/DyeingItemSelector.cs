using System;
using Game.Runtime.Data.Attributes;
using Game.Runtime.Data.Registries;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime.Systems.Clothing.Dyeing
{
    /// <summary>
    /// A class that controls a selection view for clothing dyeing
    /// </summary>
    public sealed class DyeingItemSelector : MonoBehaviour
    {
        #region Private Fields

        [Header("Visual Dependencies")]
        [SerializeField]
        private Image itemPreviewRenderer;

        [SerializeField]
        private TMP_Text itemPreviewName;

        [SerializeField]
        private Button[] itemSelectionButtons;

        [Header("Registry")]
        [SerializeField]
        private ItemRegistry itemRegistry;
        
        private ClothingAttributes[] _clothingAttributes;
        private int _currentItemIndex = 0;
        private DyeingOperation _dyeingOperation;
        
        #endregion

        #region Properties

        public ClothingAttributes CurrentItem => _clothingAttributes[_currentItemIndex];
        public Action<ClothingAttributes> OnItemSelected { get; set; }

        #endregion
        
        #region Unity Callbacks

        private void Start()
        {
            _clothingAttributes = itemRegistry.GetAllItemsOfType<ClothingAttributes>();
            UpdatePreview();
        }
        
        #endregion

        #region Public Methods

        /// <summary>
        /// Receives an ongoing dyeing operation
        /// </summary>
        /// <param name="operation">The operation to handle</param>
        public void HandleOperation(DyeingOperation operation)
        {
            _dyeingOperation = operation;
            
            _dyeingOperation.OnCrafteeModified += UpdatePreview;
            _dyeingOperation.OnOperationConcluded += ReleaseOperation;
            _dyeingOperation.OnOperationCanceled += ReleaseOperation;
            
            SetSelectionButtonsInteractable(false);
            UpdatePreview(operation.Craftee);
        }
        
        /// <summary>
        /// Selects the next clothing item. Loops if there is no next item.
        /// </summary>
        public void SelectNext()
        {
            if (_currentItemIndex + 1 >= _clothingAttributes.Length)
            {
                _currentItemIndex = 0;
            }
            else
            {
                _currentItemIndex++;
            }
            
            OnItemSelected?.Invoke(CurrentItem);
            UpdatePreview();
        }

        /// <summary>
        /// Selects the previous clothing item. Loops if there is no previous item.
        /// </summary>
        public void SelectPrevious()
        {
            if (_currentItemIndex - 1 < 0)
            {
                _currentItemIndex = _clothingAttributes.Length - 1;
            }
            else
            {
                _currentItemIndex--;
            }
            
            OnItemSelected?.Invoke(CurrentItem);
            UpdatePreview();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the preview of the selected item
        /// </summary>
        private void UpdatePreview()
        {
            itemPreviewName.text = CurrentItem.Name;
            itemPreviewRenderer.sprite = CurrentItem.Graphic;
            itemPreviewRenderer.color = CurrentItem.Color;
        }
        
        /// <summary>
        /// Updates the preview to a specific item
        /// </summary>
        /// <param name="item">The item to preview</param>
        private void UpdatePreview(ClothingAttributes item)
        {
            itemPreviewName.text = item.Name;
            itemPreviewRenderer.sprite = item.Graphic;
            itemPreviewRenderer.color = item.Color;
        }

        /// <summary>
        /// Sets all buttons to the specified state
        /// </summary>
        /// <param name="interactable">The state to set to; false is non-clickable, true is clickable</param>
        private void SetSelectionButtonsInteractable(bool interactable)
        {
            foreach (Button button in itemSelectionButtons)
            {
                button.interactable = interactable;
            }
        }

        /// <summary>
        /// Unsubscribes from the ongoing dyeing operation and restores normal functionality
        /// </summary>
        /// <param name="item">Argument ignored</param>
        private void ReleaseOperation(ClothingAttributes item)
        {
            ReleaseOperation();
        }

        /// <summary>
        /// Unsubscribes from the ongoing dyeing operation and restores normal functionality
        /// </summary>
        private void ReleaseOperation()
        {
            SetSelectionButtonsInteractable(true);
            UpdatePreview();
            
            _dyeingOperation.OnCrafteeModified -= UpdatePreview;
            _dyeingOperation.OnOperationConcluded -= ReleaseOperation;
            _dyeingOperation.OnOperationCanceled -= ReleaseOperation;
            
            _dyeingOperation = null;
        }
        
        #endregion
    }
}