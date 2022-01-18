using Cinemachine;
using Game.Runtime.Core;
using Ninito.UnityExtended.WindowManager;
using UnityEngine;

namespace Game.Runtime.Systems.Dialogue
{
    /// <summary>
    /// A class that manages simple dialogue. Only supports linear dialogue, but it should be fine for the project
    /// </summary>
    public sealed class DialogueManager : MonoBehaviour
    {
        #region Private Fields

        [Header("Dependencies")]
        [SerializeField]
        private WindowManager windowManager;

        [SerializeField]
        private string dialogueWindowKey = "DialogueMenu";

        [SerializeField]
        private string onCloseDialogueWindowKey = "HUD";

        [SerializeField]
        private DialogueDisplay dialogueDisplay;

        [SerializeField]
        private CinemachineVirtualCamera virtualCamera;

        private Transform _previousFollowTarget;
        private DialoguePassage[] _currentDialoguePassages;
        private int _currentDialoguePassageIndex = 0;

        #endregion

        #region Properties

        public DialoguePassage CurrentDialoguePassage => _currentDialoguePassages[_currentDialoguePassageIndex];

        #endregion

        #region Unity Callbacks

        public void Update()
        {
            if (!Input.GetKeyDown(KeyCode.E)) return;
            NextDialogue();
        }

        public void OnEnable()
        {
            GameManager.Instance.SetPlayerInputEnabled(false);
        }

        public void OnDisable()
        {
            GameManager.Instance.SetPlayerInputEnabled(true);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Starts a new dialogue sequence
        /// </summary>
        /// <param name="dialoguePassages">The passages that compose the sequence</param>
        /// <param name="speaker">The transform of the speaker</param>
        public void StartDialogue(DialoguePassage[] dialoguePassages, Transform speaker)
        {
            windowManager.SwitchToMenu(dialogueWindowKey);
            _currentDialoguePassages = dialoguePassages;
            FocusOnSpeaker(speaker);
            ShowDialogue();
        }

        /// <summary>
        /// Shows the next dialogue passage
        /// </summary>
        public void NextDialogue()
        {
            if (_currentDialoguePassageIndex >= _currentDialoguePassages.Length - 1)
            {
                EndDialogue();
            }
            else
            {
                _currentDialoguePassageIndex++;
                ShowDialogue();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Shows the current dialogue passage
        /// </summary>
        private void ShowDialogue()
        {
            dialogueDisplay.SetDisplayContents(CurrentDialoguePassage.SpeakerName, CurrentDialoguePassage.Text);
        }

        /// <summary>
        /// Focuses the virtual camera on the speaker
        /// </summary>
        /// <param name="speakerTransform">The transform of the speaker</param>
        private void FocusOnSpeaker(Transform speakerTransform)
        {
            CachePreviousFollowTarget();
            virtualCamera.Follow = speakerTransform;
        }

        /// <summary>
        /// Cache the previous follow target so we can restore it later
        /// </summary>
        private void CachePreviousFollowTarget()
        {
            _previousFollowTarget = virtualCamera.Follow;
        }

        /// <summary>
        /// Restores the virtual camera's follow target to the previous one
        /// </summary>
        private void RestorePreviousFollowTarget()
        {
            virtualCamera.Follow = _previousFollowTarget;
        }

        /// <summary>
        /// Ends the current dialogue sequence
        /// </summary>
        private void EndDialogue()
        {
            _currentDialoguePassages = null;
            _currentDialoguePassageIndex = 0;
            RestorePreviousFollowTarget();
            windowManager.SwitchToMenu(onCloseDialogueWindowKey);
        }

        #endregion
    }
}