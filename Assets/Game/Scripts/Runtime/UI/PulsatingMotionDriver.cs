using System;
using UnityEngine;

namespace Game.Runtime.UI
{
    /// <summary>
    /// A class that drives a simple pulsating motion for UI elements.
    /// </summary>
    public sealed class PulsatingMotionDriver : MonoBehaviour
    {
        #region Private Fields

        [Header("Configuration")]
        [SerializeField]
        private float amplitude = 0.5f;
        [SerializeField]
        private float frequency = 1.0f;
        [SerializeField]
        private float offset = 1f;

        [SerializeField]
        private bool randomize = true;

        private float _sinOffset = 0f;

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            if (randomize)
            {
                _sinOffset = UnityEngine.Random.Range(0f, 1f);
            }
        }

        private void Update()
        {
            float sin = Mathf.Sin((Time.time + _sinOffset) * frequency);
            float value = amplitude * sin + offset;
            transform.localScale = new Vector3(value, value, value);
        }

        #endregion
    }
}