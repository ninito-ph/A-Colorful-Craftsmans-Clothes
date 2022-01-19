using System;
using UnityEngine;

namespace Game.Runtime.Systems.Save.Saves
{
    /// <summary>
    /// A class that holds permanent data regarding the DressableEntity component.
    /// </summary>
    [Serializable]
    public sealed class DressableEntitySave
    {
        #region Public Fields

        [SerializeField]
        public string AccessorySlotItem;

        [SerializeField]
        public string BodySlotItem;

        #endregion

        #region Constructors

        public DressableEntitySave(string accessorySlotItem, string bodySlotItem)
        {
            AccessorySlotItem = accessorySlotItem;
            BodySlotItem = bodySlotItem;
        }

        #endregion
    }
}