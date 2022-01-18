using System;
using Game.Runtime.Systems.Dialogue;
using Game.Runtime.Systems.Inventory;
using Game.Runtime.Systems.Orders;
using Ninito.UsualSuspects.Interactable;
using UnityEngine;

namespace Game.Runtime.Entities
{
    /// <summary>
    /// A class that controls the shopkeeper's interactability
    /// </summary>
    [RequireComponent(typeof(OrderReceiver))]
    public sealed class ShopKeep : NPCController
    {
        #region Private Fields

        [Header("Dialogue Options")]
        [SerializeField]
        private DialoguePassage[] onOrdersOngoing;

        [SerializeField]
        private DialoguePassage[] onOrderComplete;

        [SerializeField]
        private DialoguePassage[] onAllOrdersComplete;

        private OrderReceiver _orderReceiver;
        
        #endregion

        #region Unity Callbacks

        public void Awake()
        {
            TryGetComponent(out _orderReceiver);
        }

        #endregion
        
        #region IInteractable Implementation

        public override void InteractWithAs(IInteractor interactor)
        {
            Debug.Log("Interacting with shopkeep");
            _orderReceiver.TryCompleteOrders(interactor.GameObject.GetComponent<InventoryProvider>());
            //dialogueManager.StartDialogue(dialoguePassages, transform);
        }

        #endregion
    }
}