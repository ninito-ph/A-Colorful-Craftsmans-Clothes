using UnityEngine;

namespace Game.Runtime.Systems.Save
{
    /// <summary>
    /// A class responsible for saving data
    /// </summary>
    public static class DataSaver
    {
        #region Public Methods

        /// <summary>
        /// Converts an object to a string using <see cref="JsonUtility"/> and saves it to the specified key
        /// </summary>
        /// <param name="data">The data to save</param>
        /// <param name="saveKey">The key to save to</param>
        public static void SaveData(object data, string saveKey)
        {
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(saveKey, json);
            PlayerPrefs.Save();
        }
        
        #endregion
    }
}