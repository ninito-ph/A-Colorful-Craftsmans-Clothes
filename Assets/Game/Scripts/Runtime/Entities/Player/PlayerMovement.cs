using UnityEngine;

namespace Game.Runtime.Entities.Player
{
    /// <summary>
    /// A class responsible for controlling player movement.
    /// </summary>
    public sealed class PlayerMovement : MonoBehaviour
    {
        #region Private Fields

        [SerializeField]
        private float speed = 5f;

        #endregion

        #region Public Methods

        /// <summary>
        /// Moves the player using the given axis inputs
        /// </summary>
        /// <param name="horizontal">The horizonal axis input</param>
        /// <param name="vertical">The vertical axis input</param>
        public void Move(float horizontal, float vertical)
        {
            Vector3 movement = new Vector3(horizontal, vertical, 0);
            transform.Translate(movement * speed * Time.deltaTime);
        }

        #endregion
    }
}