using System;
using Game.Runtime.Data.Attributes;
using Game.Runtime.Systems.Inventory;
using Game.Runtime.Utility;
using Ninito.UsualSuspects.CommonExtensions;
using UnityEngine;

namespace Game.Runtime.Systems.Clothing.Dyeing
{
    /// <summary>
    /// A class that contains information of an ongoing dye operation
    /// </summary>
    [Serializable]
    public sealed class DyeingOperation
    {
        #region Public Fields

        [SerializeField]
        public ItemInventory ItemInventory;

        [SerializeField]
        public ClothingAttributes Craftee;

        public event Action<ClothingAttributes> OnCrafteeModified;
        
        public event Action OnOperationCanceled;
        public event Action<ClothingAttributes> OnOperationConcluded;

        #endregion

        #region Constructors

        public DyeingOperation(ItemInventory itemInventory, ClothingAttributes craftee)
        {
            ItemInventory = itemInventory;
            Craftee = craftee.Clone();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Dyes the craftee with the specified item
        /// </summary>
        /// <param name="item">The item to dye with</param>
        /// <param name="color">The color to add</param>
        public void DyeWith(ItemAttributes item, Color color)
        {
            ItemInventory.AddItem(item);
            Craftee.Color += color / Craftee.DyeCostMultiplier;
            OnCrafteeModified?.Invoke(Craftee);
        }

        /// <summary>
        /// Concludes the dyeing operation and returns the dyed item
        /// </summary>
        /// <returns>The dyed item</returns>
        public ClothingAttributes ConcludeOperation()
        {
            OnOperationConcluded?.Invoke(Craftee);
            return Craftee;
        }
        
        /// <summary>
        /// Cancels the operation
        /// </summary>
        /// <returns>The items that were in the operation</returns>
        public ItemInventory CancelOperation()
        {
            OnOperationCanceled?.Invoke();
            return ItemInventory;
        }

        #endregion
    }
}