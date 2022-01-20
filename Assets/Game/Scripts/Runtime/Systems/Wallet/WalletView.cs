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
            if (wallet == null) return;
            HandleWalletChange();
        }

        private void OnDisable()
        {
            wallet.WalletUpdated -= UpdateBalanceDisplay;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Binds the view to a wallet
        /// </summary>
        /// <param name="walletToBind">The wallet to bind the view to</param>
        public void BindWallet(Wallet walletToBind)
        {
            wallet = walletToBind;
            HandleWalletChange();
        }

        #endregion
        
        #region Private Methods

        /// <summary>
        /// Handles a wallet change by subscribing to the wallet's event and updating the display
        /// </summary>
        private void HandleWalletChange()
        {
            wallet.WalletUpdated += UpdateBalanceDisplay;
            UpdateBalanceDisplay();
        }

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