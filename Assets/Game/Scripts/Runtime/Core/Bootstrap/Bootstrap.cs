using System;
using UnityEngine;

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
        
        private GameManager _gameManager;

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void Start()
        {
            _gameManager.LoadScene(postInitializeSceneName);
        }

        #endregion
    }
}
