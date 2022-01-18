using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Runtime.Core
{
    /// <summary>
    /// A class responsible for initializing the game.
    /// </summary>
    public sealed class Bootstrap : MonoBehaviour
    {
        #region Private Fields

        [Header("Configurations")]
        [SerializeField]
        private string postInitializeSceneName = "SA_MainMenu";
        
        #endregion

        #region Unity Callbacks

        private void Start()
        {
            SceneManager.LoadSceneAsync(postInitializeSceneName);
        }

        #endregion
    }
}