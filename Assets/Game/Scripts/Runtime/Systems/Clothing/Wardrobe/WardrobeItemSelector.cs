using System;
using System.Collections.Generic;
using System.Linq;
using Game.Runtime.Data.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime.Systems.Clothing.Wardrobe
{
    /// <summary>
    /// A component that allows the user to select a wardrobe item.
    /// </summary>
    public sealed class WardrobeItemSelector : MonoBehaviour
    {
        #region Private Fields

        [Header("Configurations")]
        [SerializeField]
        private ClothingType selectorType;

        [Header("Dependencies")]
        [SerializeField]
        private Image previewImage;

        [SerializeField]
        private TMP_Text previewName;

        [SerializeField]
        private Button[] cyclingButtons;

        private int _currentItemIndex = 0;
        private ClothingAttributes[] _options;

        #endregion

        #region Properties

        private ClothingAttributes CurrentItem => _options[_currentItemIndex];

        public ClothingSlot BoundSlot { get; set; }

        public ClothingType SelectorType => selectorType;

        #endregion

        #region Unity Callbacks

        private void OnDisable()
        {
            BoundSlot = null;
            _options = null;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gives the wardrobe selector its options
        /// </summary>
        /// <param name="options"></param>
        public void SetClothingOptions(IEnumerable<ItemAttributes> options)
        {
            _options = options.Where(item => item is ClothingAttributes).Cast<ClothingAttributes>()
                .Where(article => article.Type == SelectorType).ToArray();
            UpdatePreview();
        }

        /// <summary>
        /// Selects the next clothing item. Loops if there is no next item.
        /// </summary>
        public void SelectNext()
        {
            if (_currentItemIndex + 1 >= _options.Length)
            {
                _currentItemIndex = 0;
            }
            else
            {
                _currentItemIndex++;
            }

            BoundSlot.SetArticle(CurrentItem);
            UpdatePreview();
        }

        /// <summary>
        /// Selects the previous clothing item. Loops if there is no previous item.
        /// </summary>
        public void SelectPrevious()
        {
            if (_currentItemIndex - 1 < 0)
            {
                _currentItemIndex = _options.Length - 1;
            }
            else
            {
                _currentItemIndex--;
            }

            BoundSlot.SetArticle(CurrentItem);
            UpdatePreview();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the selection preview
        /// </summary>
        private void UpdatePreview()
        {
            if (_options == null || _options.Length == 0)
            {
                SetCyclingButtonsInteractable(false);
                previewName.text = String.Empty;
                previewImage.color = Color.clear;
                return;
            }
            
            previewImage.sprite = CurrentItem.Graphic;
            previewImage.color = CurrentItem.Color;
            previewName.text = CurrentItem.Name;
            SetCyclingButtonsInteractable(true);
        }
        
        /// <summary>
        /// Disables all cycling buttons
        /// </summary>
        private void SetCyclingButtonsInteractable(bool interactable)
        {
            foreach (Button cyclingButton in cyclingButtons)
            {
                cyclingButton.interactable = interactable;
            }
        }

        #endregion
    }
}