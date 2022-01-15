using UnityEngine;

namespace Game.Runtime.Data.Attributes
{
    /// <summary>
    /// A base class that serves as a base for any attribute collection of any entity 
    /// </summary>
    public abstract class Attributes : ScriptableObject
    {
        #region Private Fields

        [SerializeField]
        [TextArea]
        private string description;

        #endregion

        #region Properties

        public string Description => description;

        #endregion
    }
}