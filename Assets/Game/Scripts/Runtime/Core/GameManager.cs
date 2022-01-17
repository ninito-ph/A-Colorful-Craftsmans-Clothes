using Game.Runtime.Entities.Player;
using Ninito.UsualSuspects;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Runtime.Core
{
    /// <summary>
    /// A class that manages
    /// </summary>
    public sealed class GameManager : Singleton<GameManager>
    {
        #region Private Fields

        private PlayerInputHandler _inputHandler;

        #endregion
        
        #region Properties

        public GameObject Player { get; private set; }

        #endregion

        #region Unity Callbacks

        protected override void Awake()
        {
            base.Awake();
            CachePlayerReferences();
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        #endregion
        
        #region Public Methods

        /// <summary>
        ///     Loads a given scene
        /// </summary>
        /// <param name="sceneName">The build name of the scene</param>
        public void LoadScene(string sceneName)
        {
            LoadingScreenManager.LoadScene(sceneName);
            SceneManager.LoadScene("SA_LoadingScreen");
        }

        /// <summary>
        ///     Quits the game application
        /// </summary>
        public void QuitApplication()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        /// <summary>
        /// Enables or disables the player input handler
        /// </summary>
        /// <param name="inputEnabled">Whether the input handler should be enabled or disabled</param>
        public void SetPlayerInputEnabled(bool inputEnabled)
        {
            _inputHandler.enabled = inputEnabled;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Caches all player references
        /// </summary>
        private void CachePlayerReferences()
        {
            _inputHandler = FindObjectOfType<PlayerInputHandler>();
            Player = _inputHandler.gameObject;
        }

        #endregion
    }
}
