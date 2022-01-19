using System;
using Ninito.UsualSuspects.Attributes;
using UnityEngine;

namespace Game.Runtime.Entities.Player
{
    /// <summary>
    /// A class responsible for controlling player movement.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerMovement : MonoBehaviour
    {
        #region Private Fields

        [SerializeField]
        private float speed = 5f;

        [SerializeField]
        private float deadZoneThreshold = 0.1f;

        [SerializeField]
        private Animator[] animators;

        private Vector2 _inputs;
        private Rigidbody2D _rigidbody;

        private static readonly int _horizontalAnimProperty = Animator.StringToHash("Horizontal");
        private static readonly int _verticalAnimProperty = Animator.StringToHash("Vertical");
        private static readonly int _speedAnimProperty = Animator.StringToHash("Speed");

        #endregion

        #region Properties

        private Vector2 PositionDelta => _inputs * speed * Time.fixedDeltaTime;

        private bool InputIsAboveDeadZone =>
            Mathf.Abs(_inputs.x) > deadZoneThreshold || Mathf.Abs(_inputs.y) > deadZoneThreshold;

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            TryGetComponent(out _rigidbody);
        }

        private void FixedUpdate()
        {
            if (!InputIsAboveDeadZone) return;
            Move();
        }

        private void Update()
        {
            UpdateAnimatorValues(_inputs);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Moves the player using the given axis inputs
        /// </summary>
        /// <param name="horizontal">The horizontal axis input</param>
        /// <param name="vertical">The vertical axis input</param>
        public void ProcessInput(float horizontal, float vertical)
        {
            Vector2 inputs = new Vector2(horizontal, vertical);

            if (inputs.sqrMagnitude > 1f)
            {
                inputs.Normalize();
            }

            _inputs = inputs;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Moves the player using the given axis inputs
        /// </summary>
        private void Move()
        {
            _rigidbody.MovePosition(_rigidbody.position + PositionDelta);
        }

        /// <summary>
        /// Updates animator values
        /// </summary>
        /// <param name="movement">The movement vector to update values with</param>
        private void UpdateAnimatorValues(Vector2 movement)
        {
            // ReSharper disable once ForCanBeConvertedToForeach
            for (int index = 0; index < animators.Length; index++)
            {
                if (animators[index].runtimeAnimatorController == null) continue;

                animators[index].SetFloat(_horizontalAnimProperty, movement.x);
                animators[index].SetFloat(_verticalAnimProperty, movement.y);
                animators[index].SetFloat(_speedAnimProperty, movement.sqrMagnitude);
            }
        }

        #endregion
    }
}