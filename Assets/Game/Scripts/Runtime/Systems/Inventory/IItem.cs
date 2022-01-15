using UnityEngine;

namespace Game.Runtime.Systems.Inventory
{
    /// <summary>
    /// An interface that describes something that can be owned as an item
    /// </summary>
    public interface IItem
    {
        #region Public Fields

        public string Name { get; }
        public string Description { get; }
        public Sprite Icon { get; }

        #endregion
    }
}