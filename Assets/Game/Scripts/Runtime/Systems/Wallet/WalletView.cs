using TMPro;
using UnityEngine;

namespace Game.Runtime.Systems.Wallet
{
    /// <summary>
    /// A simple class that displays a wallet's contents
    /// </summary>
    public sealed class WalletView : MonoBehaviour
    {
        #region Private Fields

        [Header("Dependencies")]
        [SerializeField]
        private Wallet wallet;

        [SerializeField]
        private TMP_Text balanceText;

        [SerializeField]
        private string appendToCurrency = "$";
        
        #endregion

        #region Unity Callbacks

        private void OnEnable()
        {
            wallet.WalletUpdated += UpdateBalanceDisplay;
            UpdateBalanceDisplay();
        }

        private void OnDisable()
        {
            wallet.WalletUpdated -= UpdateBalanceDisplay;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the wallet's display with the current amount
        /// </summary>
        private void UpdateBalanceDisplay()
        {
            balanceText.text = Mathf.RoundToInt(wallet.Balance) + appendToCurrency;
        }

        #endregion
    }
}