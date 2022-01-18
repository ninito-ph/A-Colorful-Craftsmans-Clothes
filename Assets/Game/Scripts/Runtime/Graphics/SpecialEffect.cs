using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Runtime.Graphics
{
    /// <summary>
    /// A class that controls special effects
    /// </summary>
    [RequireComponent(typeof(AudioSource), typeof(ParticleSystem))]
    public sealed class SpecialEffect : MonoBehaviour
    {
        #region Private Fields

        [Header("Additional Parameters")]
        [SerializeField]
        [Tooltip("Whether the object should be randomly rotated on being spawned")]
        private bool rotateOnAppear = true;

        [SerializeField]
        [Tooltip("The variance in scale of the special effect being spawned")]
        private float scaleVariation = 0.25f;

        [SerializeField]
        [Tooltip("How hard the camera should be shaken")]
        private float cameraTrauma = 0f;
        
        [SerializeField]
        [Tooltip("The deviation in pitch of the audio being played")]
        private float pitchVariation = 0.2f;

        private AudioSource _audioSource;
        private float _lifetime;
        private ParticleSystem _visualParticleSystem;

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            _visualParticleSystem = GetComponent<ParticleSystem>();
            _audioSource = GetComponent<AudioSource>();

            _lifetime = CalculateLifetime();
        }

        private IEnumerator Start()
        {
            if (rotateOnAppear)
            {
                RotateByRandomAmount();
            }

            if (ScaleVariates())
            {
                VariateScale();
            }

            // Plays effects
            _visualParticleSystem.Play();
            PlayEffectSound();
            CameraShaker.Instance.InduceStress(cameraTrauma);

            yield return new WaitForSeconds(_lifetime);
            Destroy(gameObject);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Plays the effect's sound
        /// </summary>
        private void PlayEffectSound()
        {
            _audioSource.pitch = Random.Range(1f - pitchVariation, 1f + pitchVariation);
            _audioSource.Play();
        }
        
        /// <summary>
        /// Checks whether the effect's scale is variable
        /// </summary>
        /// <returns>Whether the effect's scale is variable</returns>
        private bool ScaleVariates()
        {
            return !Mathf.Approximately(0f, scaleVariation);
        }

        /// <summary>
        /// Returns the lifetime of this special effect object
        /// </summary>
        /// <returns>The longest duration out of the particle system and the audio clip lenght</returns>
        private float CalculateLifetime()
        {
            // Caches to avoid repeated marshalling
            ParticleSystem.MainModule main = _visualParticleSystem.main;
            AudioClip clip = _audioSource.clip;

            return clip == null ? main.duration : Mathf.Max(main.duration, clip.length);
        }

        /// <summary>
        /// Rotates the object randomly upon spawning
        /// </summary>
        private void RotateByRandomAmount()
        {
            transform.Rotate(new Vector3(0f, 0f, Random.Range(0f, 360f)));
        }

        /// <summary>
        /// Variates the scale of the object
        /// </summary>
        private void VariateScale()
        {
            float scaleDeviation = Random.Range(scaleVariation * -1f, scaleVariation);
            Transform transformCache = transform;
            Vector3 localScale = transformCache.localScale;

            Vector3 variantScale = new Vector3(localScale.x + scaleDeviation, localScale.y + scaleDeviation);
            transformCache.localScale = variantScale;
        }

        #endregion
    }
}