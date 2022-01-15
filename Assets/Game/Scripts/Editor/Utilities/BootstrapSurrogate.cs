using Game.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Editor.Utilities
{
    /// <summary>
    /// The BootstrapSurrogate automatically creates key classes, like the GameManager, even if the game
    /// is started from outside the Bootstrap scene. Speeds up iteration time.
    /// </summary>
    public static class BootstrapSurrogate
    {
        #region Public Methods

        /// <summary>
        /// Executes all actions that would normally be executed in the bootstrap scene
        /// </summary>
        [RuntimeInitializeOnLoadMethod]
        public static void ExecuteBootstrapOperations()
        {
            // Scene 0 should ALWAYS be Bootstrap
            if (SceneManager.GetActiveScene().buildIndex == 0) return;
            Debug.Log("<b>BootstrapSurrogate:</b> initializing...");
            InstantiateGameManager();
            Debug.Log("<b>BootstrapSurrogate:</b> done!");
        }
        
        #endregion

        #region Private Methods
        
        /// <summary>
        /// Instantiates a GameObject containing a GameManager component
        /// </summary>
        private static void InstantiateGameManager()
        {
            if (Object.FindObjectOfType<GameManager>() != null) return;
            GameObject gameManager = new GameObject("Game Manager");
            gameManager.AddComponent<GameManager>();
            Debug.Log("<b>BootstrapSurrogate:</b> GameManager created");
        }

        #endregion
    }
}