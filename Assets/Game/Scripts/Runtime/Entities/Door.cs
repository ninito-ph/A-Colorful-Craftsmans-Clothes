using Game.Runtime.Core;
using Ninito.UsualSuspects.Interactable;
using UnityEngine;

namespace Game.Runtime.Entities
{
    /// <summary>
    /// A diegetic object which allows the player to change scenes
    /// </summary>
    public sealed class Door : MonoBehaviour, IInteractable
    {
        #region Public Fields

        [Header("Configurations")]
        [SerializeField]
        private string sceneName;

        [SerializeField]
        private string interactionText;
        
        #endregion
        
        #region IInteractable Implementation

        public string InteractionToolTip => interactionText;
        
        public void InteractWithAs(IInteractor interactor)
        {
            GameManager.Instance.LoadScene(sceneName);
        }

        #endregion
    }
}