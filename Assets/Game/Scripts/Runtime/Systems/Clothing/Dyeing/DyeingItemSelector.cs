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

        [Header("Registry")]
        [SerializeField]
        private ItemRegistry itemRegistry;
        
        private ClothingAttributes[] _clothingAttributes;
        private int _currentItemIndex = 0;
        
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

        #endregion
    }
}