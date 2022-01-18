using Ninito.UsualSuspects.AudioCue;
using UnityEngine;

namespace Game.Runtime.Audio
{
    /// <summary>
    /// A class that plays AudioCues instead of AudioClips directly
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public sealed class AudioCuePlayer : MonoBehaviour
    {
        #region Private Fields

        [Header("AudioCue")]
        [SerializeField]
        private AudioCue audioCue;

        private AudioSource _audioSource;
        
        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            TryGetComponent(out _audioSource);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Plays the current AudioCue.
        /// </summary>
        public void Play()
        {
            if (audioCue == null)
            {
                Debug.LogWarning("Something tried to play an AudioCuePlayer without an AudioCue!");
                return;
            }

            _audioSource.clip = audioCue.GetClip();
            _audioSource.pitch = audioCue.GetPitch();
            _audioSource.Play();
        }

        #endregion
    }
}