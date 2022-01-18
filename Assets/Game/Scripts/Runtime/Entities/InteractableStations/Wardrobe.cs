using Game.Runtime.Systems.Clothing.Wardrobe;
using Ninito.UnityExtended.WindowManager;
using Ninito.UsualSuspects.Interactable;
using UnityEngine;

namespace Game.Runtime.Entities.InteractableStations
{
    /// <summary>
    /// A simple class that represents the physical wardrobe. 
    /// </summary>
    public sealed class Wardrobe : InteractableStation
    {
        #region Private Fields

        [Header("Dependencies")]
        [SerializeField]
        private WardrobeManager wardrobeManager;

        #endregion
        
        #region Private Fields

        public override string InteractionToolTip => "Press E to open the wardrobe";
        
        public override void InteractWithAs(IInteractor interactor)
        {
            wardrobeManager.ReceivePlayer(interactor.GameObject);
            base.InteractWithAs(interactor);
        }

        #endregion
    }
}