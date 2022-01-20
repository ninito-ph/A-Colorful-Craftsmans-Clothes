using System;
using Game.Runtime.Core;
using Game.Runtime.Data.Attributes;
using Game.Runtime.Systems.Inventory;
using Game.Runtime.Systems.Wallet;
using Ninito.UnityExtended.WindowManager;
using UnityEngine;

namespace Game.Runtime.Systems.ItemShop
{
    /// <summary>
    /// A class that controls a very simple item shop for dyes
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public sealed class ItemShop : MonoBehaviour
    {
        #region Private Fields

        [Header("Configuration")]
        [SerializeField]
        private AudioClip sellClip;

        [SerializeField]
        private AudioClip failClip;

        [Header("Dependencies")]
        [SerializeField]
        private WalletView walletView;
        
        [SerializeField]
        private WindowManager windowManager;
        
        [SerializeField]
        private string onCloseWindowKey;
        
        private Wallet.Wallet _wallet;
        private InventoryProvider _inventoryProvider;
        private AudioSource _audioSource;
        
        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            TryGetComponent(out _audioSource);
        }

        private void OnEnable()
        {
            GameManager.Instance.SetPlayerInputEnabled(false);
        }
        
        private void OnDisable()
        {
            GameManager.Instance.SetPlayerInputEnabled(true);
        }

        #endregion
        
        #region Public Methods

        /// <summary>
        /// Begins selling items to a wallet and inventory provider
        /// </summary>
        /// <param name="wallet">The wallet to deduct from</param>
        /// <param name="provider">The provider to add items to</param>
        public void BeginSale(Wallet.Wallet wallet, InventoryProvider provider)
        {
            _wallet = wallet;
            _inventoryProvider = provider;
            walletView.BindWallet(wallet);
        }
        
        /// <summary>
        /// Sells an item to the player
        /// </summary>
        /// <param name="item">The item to sell</param>
        public void SellItem(ItemAttributes item)
        {
            if (!_wallet.CanAfford(item.Value))
            {
                PlayFailClip();
                return;
            }
            
            _wallet.Balance -= item.Value;
            _inventoryProvider.Contents.AddItem(item);
            PlaySellClip();
        }

        /// <summary>
        /// Ends the sale of items
        /// </summary>
        public void EndSale()
        {
            _wallet = null;
            _inventoryProvider = null;
            windowManager.SwitchToMenu(onCloseWindowKey);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Plays the sell clip to indicate an item was bought
        /// </summary>
        private void PlaySellClip()
        {
            _audioSource.clip = sellClip;
            _audioSource.Play();
        }
        
        /// <summary>
        /// Plays a failure clip to indicate an item was not bought
        /// </summary>
        private void PlayFailClip()
        {
            _audioSource.clip = failClip;
            _audioSource.Play();
        }

        #endregion
    }
}