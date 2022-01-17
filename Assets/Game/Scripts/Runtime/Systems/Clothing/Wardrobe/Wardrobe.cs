using Ninito.UnityExtended.WindowManager;
using Ninito.UsualSuspects.Interactable;
using UnityEngine;

namespace Game.Runtime.Systems.Clothing.Wardrobe
{
    /// <summary>
    /// A simple class that represents the physical wardrobe. 
    /// </summary>
    public sealed class Wardrobe : MonoBehaviour, IInteractable
    {
        #region Private Fields

        [Header("Dependencies")]
        [SerializeField]
        private WindowManager windowManager;
        
        [SerializeField]
        private string wardrobeWindowKey;
        
        [SerializeField]
        private WardrobeManager wardrobeManager;

        #endregion
        
        #region Private Fields

        public string InteractionToolTip => "Press E to open the wardrobe";
        
        public void InteractWithAs(IInteractor interactor)
        {
            wardrobeManager.ReceivePlayer(interactor.GameObject);
            windowManager.SwitchToMenu(wardrobeWindowKey);
        }

        #endregion
    }
}