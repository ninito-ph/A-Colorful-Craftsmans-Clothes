using TMPro;
using UnityEngine;

namespace Game.Runtime.Systems.Dialogue
{
    /// <summary>
    /// A simple class that controls a dialogue display
    /// </summary>
    public sealed class DialogueDisplay : MonoBehaviour
    {
        #region Private Fields

        [Header("Dependencies")]
        [SerializeField]
        private TMP_Text speakerName;
        [SerializeField]
        private TMP_Text bodyText;

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the display's contents
        /// </summary>
        /// <param name="speaker">The name of the speaker</param>
        /// <param name="body">What the speaker is saying</param>
        public void SetDisplayContents(string speaker, string body)
        {
            speakerName.text = speaker;
            bodyText.text = body;
        }

        #endregion
    }
}