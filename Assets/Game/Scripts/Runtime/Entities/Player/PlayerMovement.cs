using Ninito.UsualSuspects.Attributes;
using UnityEngine;

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

        [SerializeField]
        private Animator[] animators;

#if UNITY_EDITOR
        [Header("Debugging Values")]
        [SerializeField]
        [ReadOnlyField]
        private float hAxisInput;

        [SerializeField]
        [ReadOnlyField]
        private float vAxisInput;

        [SerializeField]
        [ReadOnlyField]
        private Vector3 movementPreview;
#endif

        private static readonly int _horizontalAnimProperty = Animator.StringToHash("Horizontal");
        private static readonly int _verticalAnimProperty = Animator.StringToHash("Vertical");
        private static readonly int _speedAnimProperty = Animator.StringToHash("Speed");

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

#if UNITY_EDITOR
            hAxisInput = horizontal;
            vAxisInput = vertical;
            movementPreview = movement * speed * Time.fixedDeltaTime;
#endif

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