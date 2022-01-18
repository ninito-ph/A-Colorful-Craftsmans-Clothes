using Game.Runtime.Systems.Dialogue;
using Ninito.UsualSuspects.Interactable;
using UnityEngine;

namespace Game.Runtime.Entities
{
    /// <summary>
    /// A base class for all Non Player Characters to inherit from.
    /// </summary>
    public abstract class NPCController : MonoBehaviour, IInteractable
    {
        #region Private Fields

        [Header("Configuration")]
        [SerializeField]
        protected string npcName;

        [Header("Dependencies")]
        [SerializeField]
        protected DialogueManager dialogueManager;
        
        #endregion
        
        #region IInteractable Implementation

        public string InteractionToolTip => "Press E to talk to " + npcName;

        public abstract void InteractWithAs(IInteractor interactor);

        #endregion
    }
}