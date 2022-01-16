﻿using UnityEngine;

namespace Game.Runtime.Entities.Player
{
    /// <summary>
    /// A class responsible for controlling player movement.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public sealed class PlayerMovement : MonoBehaviour
    {
        #region Private Fields

        [SerializeField]
        private float speed = 5f;
        
        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private static readonly int _horizontalAnimProperty = Animator.StringToHash("Horizontal");
        private static readonly int _verticalAnimProperty = Animator.StringToHash("Vertical");
        private static readonly int _speedAnimProperty = Animator.StringToHash("Speed");

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            TryGetComponent(out _animator);
            TryGetComponent(out _rigidbody);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Moves the player using the given axis inputs
        /// </summary>
        /// <param name="horizontal">The horizontal axis input</param>
        /// <param name="vertical">The vertical axis input</param>
        public void Move(float horizontal, float vertical)
        {
            Vector3 movement = new Vector3(horizontal, vertical);

            if (movement.sqrMagnitude > 1f)
            {
                movement.Normalize();
            }
            
            transform.Translate(movement * speed * Time.fixedDeltaTime);
            UpdateAnimatorValues(movement);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates animator values
        /// </summary>
        /// <param name="movement">The movement vector to update values with</param>
        private void UpdateAnimatorValues(Vector2 movement)
        {
            _animator.SetFloat(_horizontalAnimProperty, movement.x);
            _animator.SetFloat(_verticalAnimProperty, movement.y);
            _animator.SetFloat(_speedAnimProperty, movement.sqrMagnitude);
        }

        #endregion
    }
}