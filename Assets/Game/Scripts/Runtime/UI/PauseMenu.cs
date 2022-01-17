using Game.Runtime.Systems;
using Ninito.UnityExtended.WindowManager;
using UnityEngine;

namespace Game.Runtime.UI
{
    /// <summary>
    /// A class that controls the in-game pause menu
    /// </summary>
    public sealed class PauseMenu : MonoBehaviour
    {
        #region Private Fields

        [Header("Dependencies")]
        [SerializeField]
        private WindowManager windowManager;

        [Header("UI Keys")]
        [SerializeField]
        private string hudKey = "HUD";

        #endregion
        
        #region Unity Callbacks

        private void OnEnable()
        {
            TimeManager.Pause();
        }

        private void OnDisable()
        {
            TimeManager.Resume();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                windowManager.SwitchToMenu(hudKey);
            }
        }

        #endregion
    }
}