using Ninito.UnityExtended.WindowManager;
using Ninito.UsualSuspects.Interactable;
using UnityEngine;

namespace Game.Runtime.Entities.Player
{
    /// <summary>
    /// A class responsible for receiving player input and sending it to relevant classes.
    /// </summary>
    public sealed class PlayerInputHandler : MonoBehaviour
    {
        #region Private Fields

        [Header("Dependencies")]
        [SerializeField]
        private PlayerMovement movement;

        [SerializeField]
        private Simple2DInteractor interactor;
        
        [Header("UI Input")]
        [SerializeField]
        private WindowManager windowManager;

        [Header("Window Manager Keys")]
        [SerializeField]
        private string inventoryMenuKey;

        #endregion

        #region Unity Callbacks

        private void Update()
        {
            HandleMovementInput();
            HandleInteractionInput();
            HandleUIInput();
        }


        #endregion

        #region Private Methods

        /// <summary>
        /// Passes off interaction input to <see cref="Simple2DInteractor"/> class
        /// </summary>
        private void HandleInteractionInput()
        {
            if (!Input.GetKeyDown(KeyCode.E)) return;
            interactor.OnInteract();
        }

        /// <summary>
        /// Passes off movement input to the <see cref="PlayerMovement"/> class
        /// </summary>
        private void HandleMovementInput()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (horizontal == 0 && vertical == 0) return;

            movement.Move(horizontal, vertical);
        }

        /// <summary>
        /// Passes off UI input to the <see cref="WindowManager"/> class
        /// </summary>
        private void HandleUIInput()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                windowManager.ToggleMenu(inventoryMenuKey);
            }
        }
        
        #endregion
    }
}