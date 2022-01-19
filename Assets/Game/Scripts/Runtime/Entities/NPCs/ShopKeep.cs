using System.Collections;
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

        [Header("Dependencies")]
        [SerializeField]
        private GameObject orderCompleteEffect;

        [Header("Configurations")]
        [SerializeField]
        [Min(0.1f)]
        private float orderCompleteEffectInterval = 0.75f;

        [SerializeField]
        private float pitchIncreasePerOrderComplete = 0.15f;

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
            dialogueManager.StartDialogue(ReceiveOrdersFrom(interactor) ? onOrderComplete : onOrdersOngoing, transform);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Receives items from the given interactor
        /// </summary>
        /// <param name="interactor"></param>
        /// <returns>Whether any order was complete</returns>
        private bool ReceiveOrdersFrom(IInteractor interactor)
        {
            int completedOrders =
                _orderReceiver.TryCompleteOrders(interactor.GameObject.GetComponent<InventoryProvider>());

            if (completedOrders > 0)
            {
                StartCoroutine(InstantiateCompleteOrderEffects(interactor.GameObject.transform, completedOrders));
            }

            return completedOrders > 0;
        }

        /// <summary>
        /// Instantiates a number of order complete effects at the target's position
        /// </summary>
        /// <param name="targetTransform">The target's transform</param>
        /// <param name="amount">The amount of effects to instantiate</param>
        private IEnumerator InstantiateCompleteOrderEffects(Transform targetTransform, int amount)
        {
            WaitForSeconds wait = new WaitForSeconds(orderCompleteEffectInterval);

            for (int index = 0; index < amount; index++)
            {
                GameObject completeEffect =
                    Instantiate(orderCompleteEffect, targetTransform.position, Quaternion.identity);
                completeEffect.GetComponent<AudioSource>().pitch += index * pitchIncreasePerOrderComplete;

                yield return wait;
            }
        }

        #endregion
    }
}