using Game.Runtime.Entities.Player;
using Game.Runtime.Systems.Clothing.Dyeing;
using Ninito.UsualSuspects.Interactable;
using UnityEngine;

namespace Game.Runtime.Entities.InteractableStations
{
    /// <summary>
    /// A class that controls the physical dyeing station
    /// </summary>
    public sealed class Dyer : InteractableStation
    {
        #region Private Fields

        [Header("Dependencies")]
        [SerializeField]
        private DyeingManager dyeingManager;
        
        #endregion
        
        #region IInteractable Implementation

        public override string InteractionToolTip => "Press E to open the dyeing menu";

        public override void InteractWithAs(IInteractor interactor)
        {
            dyeingManager.PlayerInventory = interactor.GameObject.GetComponent<PlayerInventory>().Contents;
            base.InteractWithAs(interactor);
        }

        #endregion
    }
}