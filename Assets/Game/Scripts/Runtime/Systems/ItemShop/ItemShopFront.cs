using System;
using Game.Runtime.Systems.Inventory;
using Ninito.UnityExtended.WindowManager;
using Ninito.UsualSuspects.Interactable;
using UnityEngine;

namespace Game.Runtime.Systems.ItemShop
{
    /// <summary>
    /// A simple class that opens a window to buy items.
    /// </summary>
    public sealed class ItemShopFront : MonoBehaviour, IInteractable
    {
        #region Private Fields

        [Header("Dependencies")]
        [SerializeField]
        private WindowManager windowManager;

        [SerializeField]
        private string shopMenuKey;

        [SerializeField]
        private string interactionText = "Press E to open the Dye shop";

        [SerializeField]
        private ItemShop itemShop;

        #endregion

        #region Properties

        public string InteractionToolTip => interactionText;

        public void InteractWithAs(IInteractor interactor)
        {
            itemShop.BeginSale(interactor.GameObject.GetComponent<Wallet.Wallet>(),
                interactor.GameObject.GetComponent<InventoryProvider>());
            windowManager.SwitchToMenu(shopMenuKey);
        }

        #endregion
    }
}