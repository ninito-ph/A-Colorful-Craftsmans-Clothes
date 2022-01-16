using System;
using TMPro;
using UnityEngine;

namespace Game.Runtime.Systems
{
    /// <summary>
    /// A class that controls a display for tooltips
    /// </summary>
    public sealed class TooltipDisplay : MonoBehaviour
    {
        #region Private Fields

        [Header("Dependencies")]
        [SerializeField]
        private GameObject displayParent;

        [SerializeField]
        private TMP_Text tooltipText;

        #endregion

        #region Unity Callbacks

        private void Reset()
        {
            displayParent = gameObject;
        }

        #endregion
        
        #region Public Methods

        /// <summary>
        /// Sets the text of the tooltip, and automatically disables the display if no tooltip is set
        /// </summary>
        /// <param name="tooltip">The tooltip to display</param>
        public void SetTooltip(string tooltip)
        {
            if (String.IsNullOrEmpty(tooltip))
            {
                displayParent.SetActive(false);
                tooltipText.text = String.Empty;
            }
            else
            {
                displayParent.SetActive(true);
                tooltipText.text = tooltip;
            }
        }

        #endregion
    }
}