using System;
using Cinemachine;
using Ninito.UsualSuspects;
using UnityEngine;

namespace Game.Runtime.Systems.CameraShake
{
    /// <summary>
    /// A class responsible for shaking the cmaera
    /// </summary>
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public sealed class CameraShaker : Singleton<CameraShaker>
    {
        #region Private Fields

        [Header("Camera Shake")]
        [Tooltip("The magnitude of the camera's shake")]
        [SerializeField]
        private float traumaMagnitude;

        [Tooltip("How fast the camera stops shaking (recovers from trauma)")]
        [SerializeField]
        private float recoverySpeed;

        private float _trauma = 0f;
        private CinemachineVirtualCamera _virtualCamera;
        private CinemachineBasicMultiChannelPerlin _perlinNoise;
        
        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
            _perlinNoise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        private void Update()
        {
            _trauma = Mathf.Clamp01(_trauma - recoverySpeed * Time.deltaTime);

            if (Math.Abs(_perlinNoise.m_AmplitudeGain - _trauma * traumaMagnitude) > Mathf.Epsilon)
            {
                _perlinNoise.m_AmplitudeGain = _trauma * traumaMagnitude;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Makes the camera shake
        /// </summary>
        /// <param name="stress">How shaky the camera should be</param>
        public void InduceStress(float stress)
        {
            _trauma += stress;
        }

        #endregion
    }
    
}
