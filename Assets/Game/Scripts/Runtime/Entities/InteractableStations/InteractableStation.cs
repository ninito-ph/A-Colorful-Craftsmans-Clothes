using Ninito.UnityExtended.WindowManager;
using Ninito.UsualSuspects.Interactable;
using UnityEngine;

namespace Game.Runtime.Entities.InteractableStations
{
    /// <summary>
    /// A base class for all other interactable station types
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public abstract class InteractableStation : MonoBehaviour, IInteractable
    {
        #region Protected Fields

        [Header("Dependencies")]
        [SerializeField]
        private WindowManager _windowManager;

        [SerializeField]
        private string windowKey;

        private AudioSource _audioSource;
        
        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            TryGetComponent(out _audioSource);
        }

        #endregion

        #region IInteractable Implementation

        public abstract string InteractionToolTip { get; }

        public virtual void InteractWithAs(IInteractor interactor)
        {
            _audioSource.Play();
            _windowManager.SwitchToMenu(windowKey);
        }

        #endregion
    }
}