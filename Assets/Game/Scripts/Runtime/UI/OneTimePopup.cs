using System;
using Game.Runtime.Core;
using UnityEngine;

namespace Game.Runtime.UI
{
    /// <summary>
    /// A class that shows a pop up that should only appear once
    /// </summary>
    public sealed class OneTimePopup : MonoBehaviour
    {
        #region Private Fields

        [Header("Configurations")]
        [SerializeField]
        private string popUpKey = "";

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            GameManager.Instance.SetPlayerInputEnabled(false);
            
            if (PlayerPrefs.GetInt(popUpKey, 0) == 1)
            {
                Destroy(gameObject);
            }
            
            PlayerPrefs.SetInt(popUpKey, 1);
        }

        private void OnDestroy()
        {
            GameManager.Instance.SetPlayerInputEnabled(true);
        }

        #endregion

        #region Public Methods

        public void Close()
        {
            Destroy(gameObject);
        }

        #endregion
    }
}