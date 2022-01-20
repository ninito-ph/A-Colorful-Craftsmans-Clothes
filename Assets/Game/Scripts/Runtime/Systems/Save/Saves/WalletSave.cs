using System;
using UnityEngine;

namespace Game.Runtime.Systems.Save.Saves
{
    /// <summary>
    /// A simple class that contains permanent data for the wallet
    /// </summary>
    [Serializable]
    public class WalletSave
    {
        #region Public Fields

        [SerializeField]
        public float balance;

        #endregion

        #region Constructors

        public WalletSave(float saveBalance)
        {
            balance = saveBalance;
        }

        #endregion
    }
}