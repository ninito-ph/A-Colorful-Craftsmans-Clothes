using System;
using UnityEngine;

namespace Game.Runtime.Systems
{
    /// <summary>
    /// A class that is responsible for pausing and resuming the game.
    /// </summary>
    public static class TimeManager
    {
        #region Public Fields

        /// <summary>
        /// An event invoked when the game is paused. The bool argument is true if the game is paused, false otherwise.
        /// </summary>
        public static event Action<bool> OnPause;

        #endregion

        #region Properties

        public static bool IsPaused { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the game's pause state
        /// </summary>
        /// <param name="pause"></param>
        public static void SetPause(bool pause)
        {
            Time.timeScale = pause ? 0f : 1f;
            IsPaused = pause;
            OnPause?.Invoke(pause);
        }

        /// <summary>
        /// Sets the state of the game to paused
        /// </summary>
        public static void Pause()
        {
            SetPause(true);
        }

        /// <summary>
        /// Sets the state of the game to resumed
        /// </summary>
        public static void Resume()
        {
            SetPause(false);
        }

        #endregion
    }
}