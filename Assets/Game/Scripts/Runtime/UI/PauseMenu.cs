using Game.Runtime.Systems;
using Ninito.UnityExtended.WindowManager;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Runtime.UI
{
    /// <summary>
    /// A class that controls the in-game pause menu
    /// </summary>
    public sealed class PauseMenu : MonoBehaviour
    {
        #region Private Fields

        [Header("Configurations")]
        [SerializeField]
        private bool pauseOnOpen = true;

        [SerializeField]
        private bool resumeOnClose = true;

        [Header("Dependencies")]
        [SerializeField]
        private WindowManager windowManager;

        [FormerlySerializedAs("hudKey")]
        [Header("UI Keys")]
        [SerializeField]
        private string onCloseMenuKey;

        #endregion

        #region Unity Callbacks

        private void OnEnable()
        {
            if (pauseOnOpen)
            {
                TimeManager.Pause();
            }
        }

        private void OnDisable()
        {
            if (resumeOnClose)
            {
                TimeManager.Resume();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                windowManager.SwitchToMenu(onCloseMenuKey);
            }
        }

        #endregion
    }
}