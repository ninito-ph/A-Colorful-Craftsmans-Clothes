using UnityEngine;

namespace Game.Runtime.Systems.Save
{
    /// <summary>
    /// A class responsible for loading data
    /// </summary>
    public static class DataLoader
    {
        #region Public Methods 

        /// <summary>
        /// Loads data of the given type from the given key
        /// </summary>
        /// <param name="saveKey">The key the data was saved to</param>
        /// <typeparam name="T">The type of the data to save</typeparam>
        /// <returns>The loaded data from the specified key</returns>
        public static T Load<T>(string saveKey)
        {
            return HasData(saveKey) ? JsonUtility.FromJson<T>(PlayerPrefs.GetString(saveKey)) : default;
        }

        /// <summary>
        /// Checks whether the given key has data saved to it
        /// </summary>
        /// <param name="saveKey">The key to check</param>
        /// <returns>Whether there is data at the specified key</returns>
        public static bool HasData(string saveKey)
        {
            return PlayerPrefs.HasKey(saveKey);
        }
        
        /// <summary>
        /// Attempts to load data from the specified key
        /// </summary>
        /// <param name="saveKey">The key to load from</param>
        /// <param name="data">The data loaded from the key, if the key exists</param>
        /// <typeparam name="T">The type of data to load</typeparam>
        /// <returns>Whether any data could be loaded from the key</returns>
        public static bool TryLoad<T>(string saveKey, out T data)
        {
            data = default;
            if (!HasData(saveKey))
            {
                return false;
            }

            data = Load<T>(saveKey);
            return true;
        }

        #endregion
    }
}