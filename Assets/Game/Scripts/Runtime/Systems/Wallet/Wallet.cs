using System;
using Game.Runtime.Systems.Save;
using Game.Runtime.Systems.Save.Saves;
using UnityEngine;

namespace Game.Runtime.Systems.Wallet
{
    /// <summary>
    /// A simple class that controls the player's wallet
    /// </summary>
    public sealed class Wallet : MonoBehaviour
    {
        #region Private Fields

        private float _balance = 0;

        #endregion

        #region Properties

        public float Balance
        {
            get => _balance;
            set
            {
                _balance = value;
                WalletUpdated?.Invoke();
            }
        }

        public Action WalletUpdated { get; set; }

        private string SaveKey => gameObject + ".wallet";

        #endregion

        #region UnityCallbacks

        private void Start()
        {
            if (DataLoader.TryLoad(SaveKey, out WalletSave save))
            {
                Balance = save.balance;
            }
        }

        private void OnDestroy()
        {
            WalletSave save = new WalletSave(Balance);
            DataSaver.SaveData(save, SaveKey);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Checks whether the wallet hsa enough money to buy an item
        /// </summary>
        /// <param name="amount">The amount to check for</param>
        /// <returns>Whether the wallet has enough money to buy a given item</returns>
        public bool CanAfford(float amount)
        {
            return Balance >= amount;
        }

        #endregion
    }
}