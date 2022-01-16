using System;
using UnityEngine;

namespace Game.Runtime.Systems.Clothing
{
    /// <summary>
    /// A component that can be used to dress an entity
    /// </summary>
    public sealed class DressableEntity : MonoBehaviour
    {
        #region Private Fields

        [Header("Slots")]
        [SerializeField]
        private ClothingSlot accessorySlot = new ClothingSlot();

        [SerializeField]
        private ClothingSlot bodySlot = new ClothingSlot();

        #endregion
    }
}