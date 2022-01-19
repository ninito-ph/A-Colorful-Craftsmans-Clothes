using System.Collections.Generic;
using System.Linq;
using Ninito.UsualSuspects;
using UnityEngine;

namespace Game.Runtime.Systems.Dialogue
{
    /// <summary>
    /// A class contains a single passage of dialogue
    /// </summary>
    [CreateAssetMenu(fileName = CreateMenus.DIALOGUE_FILENAME,
        menuName = CreateMenus.DIALOGUE_MENUNAME, order = CreateMenus.DIALOGUE_ORDER)]
    public sealed class DialoguePassage : ScriptableObject
    {
        #region Private Fields

        [Header("Configurations")]
        [SerializeField]
        [Tooltip("A dictionary containing keys that can be replaced in the text")]
        private SerializedDictionary<string, string> keysAndReplacements = new SerializedDictionary<string, string>();

        #endregion
        
        #region Public Fields
        
        [SerializeField]
        public string SpeakerName = "John Doe";
        
        [SerializeField]
        [TextArea]
        public string InternalText = "Hey there! My name is John Doe. I'm a dummy character. I'm here to talk to you.";

        #endregion

        #region Properties

        public SerializedDictionary<string, string> KeysAndReplacements => keysAndReplacements;

        public string Text => ReplaceKeys();

        #endregion

        #region Private Methods

        /// <summary>
        /// Replaces all keys by their corresponding values
        /// </summary>
        /// <returns>The original text with keys replaced by values</returns>
        private string ReplaceKeys()
        {
            return keysAndReplacements.Aggregate(InternalText,
                (current, keyAndReplacement) => current.Replace(keyAndReplacement.Key, keyAndReplacement.Value));
        }

        #endregion
    }
}