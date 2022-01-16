using System.Linq;
using Game.Runtime.Data.Attributes;
using UnityEditor;
using UnityEngine;

namespace Game.Runtime.Systems
{
    /// <summary>
    /// A registry of items.
    /// </summary>
    [CreateAssetMenu(fileName = CreateMenus.ITEM_REGISTRY_FILENAME,
        menuName = CreateMenus.ITEM_REGISTRY_MENUNAME, order = CreateMenus.ITEM_REGISTRY_ORDER)]
    public sealed class ItemRegistry : Registry<ItemAttributes>
    {
        #region Public Methods

        public ItemAttributes GetItemByHash(int itemHash)
        {
            return Items.FirstOrDefault(item => item.GetHashCode() == itemHash);
        }

        #endregion

        #region Private Methods

        // Normally I wouldn't need to do this, but ContextMenu not showing up in inherited classes
        // is a regression bug present in this Unity version :(
        [ContextMenu("Auto fill")]
        private void FillSelf() => GetAllProjectItems();

        #endregion
    }
}