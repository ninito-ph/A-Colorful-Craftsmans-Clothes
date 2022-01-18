using System.Linq;
using Game.Runtime.Data.Attributes;
using UnityEngine;

namespace Game.Runtime.Data.Registries
{
    /// <summary>
    /// A registry of items.
    /// </summary>
    [CreateAssetMenu(fileName = CreateMenus.ITEM_REGISTRY_FILENAME,
        menuName = CreateMenus.ITEM_REGISTRY_MENUNAME, order = CreateMenus.ITEM_REGISTRY_ORDER)]
    public sealed class ItemRegistry : Registry<ItemAttributes>
    {
        #region Private Methods

#if UNITY_EDITOR
        // Normally I wouldn't need to do this, but ContextMenu not showing up in inherited classes
        // is a regression bug present in this Unity version :(
        [ContextMenu("Auto fill")]
        private void FillSelf() => GetAllProjectItems();
#endif

        #endregion

        #region Public Methods

        /// <summary>
        /// Finds an item by its ID
        /// </summary>
        /// <param name="itemID">The ID of the searched item</param>
        /// <returns>An item with a specified ID</returns>
        public ItemAttributes GetItemByID(int itemID)
        {
            return Items.FirstOrDefault(item => item.ID == itemID);
        }

        /// <summary>
        /// Adds an item to the registry and sets its ID.
        /// </summary>
        /// <param name="item">The item to add</param>
        public override void Add(ItemAttributes item)
        {
            if (Items.Contains(item)) return;
            Items.Add(item);
            item.ID = Items.Count - 1;
        }

        #endregion
    }
}