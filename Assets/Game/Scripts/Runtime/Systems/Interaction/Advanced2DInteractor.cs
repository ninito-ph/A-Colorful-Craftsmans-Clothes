using System;
using Ninito.UsualSuspects.Interactable;
using UnityEngine;

namespace Game.Runtime.Systems
{
    public sealed class Advanced2DInteractor : Simple2DInteractor
    {
        #region Private Fields

        [Header("Dependencies")]
        [SerializeField]
        private TooltipDisplay tooltipDisplay;

        #endregion

        #region Protected Methods

        protected override void UpdateInteractionTooltip()
        {
            IInteractable interactable = GetInteractableInVolume();
            tooltipDisplay.SetTooltip(interactable == null ? String.Empty : interactable.InteractionToolTip);
        }

        #endregion
    }
}