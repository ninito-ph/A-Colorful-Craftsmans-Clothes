using System;
using System.Collections;
using Ninito.UsualSuspects;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Core
{
    public class LoadingScreenManager : Singleton<LoadingScreenManager>
    {
        #region Private Fields

        [Header("Visual Settings")]
        [SerializeField]
        [Tooltip("The loading graphic to be filled as the game loads.")]
        private Image fillGraphic;

        [SerializeField]
        private TMP_Text loadText;

        [SerializeField]
        [Tooltip("The time waited before moving to the loaded scene, after it has finished loading")]
        private float loadingScreenDelay = 0.5f;

        [Header("Loading Settings")]
        [SerializeField]
        private bool loadAdditively = true;
        
        private AsyncOperation _loadOperation;
        private static string _sceneToLoad;

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            if (loadAdditively)
            {
                StartCoroutine(LoadSceneAdditive(_sceneToLoad));
            }
            else
            {
                LoadSceneNormal(_sceneToLoad);
            }
        }

        private void Update()
        {
            if (fillGraphic != null && _loadOperation != null)
            {
                fillGraphic.fillAmount = _loadOperation.progress;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the loading screen's text
        /// </summary>
        /// <param name="text">The text to display</param>
        public virtual void SetLoadText(string text)
        {
            loadText.text = text;
        }

        /// <summary>
        /// Loads a scene
        /// </summary>
        /// <param name="sceneName">The name of the scene to load</param>
        public static void LoadScene(string sceneName)
        {
            _sceneToLoad = sceneName;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Loads a scene normally
        /// </summary>
        /// <param name="sceneName">The name of the scene to load</param>
        protected virtual void LoadSceneNormal(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        /// <summary>
        /// Loads a scene additively
        /// </summary>
        /// <param name="sceneName">The name of the scene to load</param>
        protected virtual IEnumerator LoadSceneAdditive(string sceneName)
        {
            yield return new WaitForSeconds(loadingScreenDelay);
            _loadOperation = SceneManager.LoadSceneAsync(sceneName);
        }

        #endregion
    }
}