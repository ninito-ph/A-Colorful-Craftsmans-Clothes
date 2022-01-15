using Ninito.UsualSuspects.Interactable;
using UnityEngine;

namespace Game.Runtime.Entities
{
    /// <summary>
    /// A class that controls a harvestable object
    /// </summary>
    public sealed class Harvestable : MonoBehaviour, IInteractable
    {
        #region IInteractable Implementation

        public string InteractionToolTip => "Flower";
        
        public void InteractWithAs(IInteractor interactor)
        {
            Destroy(gameObject);
        }

        #endregion
    }
}