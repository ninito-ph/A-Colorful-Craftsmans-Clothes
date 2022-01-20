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
        private float _time;

        #endregion

        #region Properties

        public GameObject Player { get; private set; }

        public float LastSavedTime => _time;

        #endregion

        #region Unity Callbacks

        protected override void Awake()
        {
            base.Awake();
            UpdateReferences();
            SceneManager.sceneLoaded += UpdateReferences;
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (Time.time == 0) return;
            _time = Time.time;
        }

        protected override void OnDestroy()
        {
            SceneManager.sceneLoaded -= UpdateReferences;
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
            if (_inputHandler == null)
            {
                Debug.LogError(
                    "Something went wrong. A script is trying to alter the player input handler, but it is null.");
                return;
            }

            _inputHandler.InputEnabled = inputEnabled;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Disposes of existing references. Not strictly necessary but used anyway for safety
        /// </summary>
        private void DisposeOfReferences()
        {
            Player = null;
            _inputHandler = null;
        }

        /// <summary>
        /// Caches all references
        /// </summary>
        private void CacheReferences()
        {
            CachePlayerReferences();
        }

        /// <summary>
        /// Disposes of old references and checks again for references. Used to cache references
        /// again when loading a new scene
        /// </summary>
        /// <param name="scene">The scene loaded. Ignored.</param>
        /// <param name="mode">The mode through which the scene was loaded. Ignored.</param>
        private void UpdateReferences(Scene scene, LoadSceneMode mode)
        {
            DisposeOfReferences();
            CacheReferences();
        }
        
        /// <summary>
        /// Disposes of old references and checks again for references. Used to cache references
        /// again when loading a new scene
        /// </summary>
        private void UpdateReferences()
        {
            DisposeOfReferences();
            CacheReferences();
        }

        /// <summary>
        /// Caches all player references
        /// </summary>
        private void CachePlayerReferences()
        {
            _inputHandler = FindObjectOfType<PlayerInputHandler>();

            if (_inputHandler == null)
            {
                return;
            }

            Player = _inputHandler.gameObject;
        }

        #endregion
    }
}