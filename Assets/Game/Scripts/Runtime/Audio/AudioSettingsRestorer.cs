using UnityEngine;
using UnityEngine.Audio;

namespace Game.Runtime.Audio
{
    /// <summary>
    /// A class that restores audio settings at startup
    /// </summary>
    public sealed class AudioSettingsRestorer : MonoBehaviour
    {
        #region Private Fields

        [Header("Dependencies")]
        [SerializeField]
        private AudioMixer audioMixer;

        [SerializeField]
        private string[] audioGroupNames;

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            foreach (string groupName in audioGroupNames)
            {
                float volume = PlayerPrefs.GetFloat(groupName + "Volume", 1f);
                audioMixer.SetFloat(groupName, Mathf.Log10(volume) * 20);
            }
        }

        #endregion
    }
}