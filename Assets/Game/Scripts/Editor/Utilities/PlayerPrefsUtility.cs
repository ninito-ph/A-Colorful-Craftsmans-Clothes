using UnityEditor;
using UnityEngine;

namespace Game.Editor.Utilities
{
    /// <summary>
    /// A class that provides simple utilities for working with PlayerPrefs.
    /// </summary>
    public static class PlayerPrefsUtility
    {
        /// <summary>
        /// Clears out all PlayerPrefs keys
        /// </summary>
        [MenuItem("Tools/PlayerPrefs/Delete All")]
        public static void ClearAll()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}