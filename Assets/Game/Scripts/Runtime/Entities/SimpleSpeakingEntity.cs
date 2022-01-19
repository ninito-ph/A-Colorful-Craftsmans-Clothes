using Game.Runtime.Systems.Dialogue;
using Ninito.UsualSuspects.Interactable;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Runtime.Entities
{
    /// <summary>
    /// A class that represents an entity with simple dialogue
    /// </summary>
    public sealed class SimpleSpeakingEntity : MonoBehaviour, IInteractable
    {
        #region Private Fields

        [FormerlySerializedAs("_dialogueManager")]
        [Header("Dependencies")]
        [SerializeField]
        private DialogueManager dialogueManager;
        
        [Header("Configurations")]
        [SerializeField]
        [TextArea]
        private string dialogue;

        [SerializeField]
        private string speakerName;

        [SerializeField]
        private string interactionTooltip;

        private DialoguePassage[] _dialoguePassage;

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            GenerateDialogue();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Generates dialogue based on the simple text in the dialogue field.
        /// </summary>
        private void GenerateDialogue()
        {
            _dialoguePassage = new[] { ScriptableObject.CreateInstance<DialoguePassage>() };
            _dialoguePassage[0].InternalText = dialogue;
            _dialoguePassage[0].SpeakerName = speakerName;
        }

        #endregion

        #region IInteractable Implementation

        public string InteractionToolTip => interactionTooltip;

        public void InteractWithAs(IInteractor interactor)
        {
            dialogueManager.StartDialogue(_dialoguePassage, transform);
        }

        #endregion
    }
}