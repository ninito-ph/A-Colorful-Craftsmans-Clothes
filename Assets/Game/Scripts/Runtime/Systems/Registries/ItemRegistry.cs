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

#if UNITY_EDITOR
        [ContextMenu("Get All Items In Project")]
        private void GetAllProjectItems()
        {
            Items.Clear();
            
            foreach (Object foundObject in Resources.FindObjectsOfTypeAll(typeof(ItemAttributes)))
            {
                ItemAttributes itemAttributes = (ItemAttributes) foundObject;
                if (!EditorUtility.IsPersistent(itemAttributes)) continue;
                Items.Add(itemAttributes);
            }
        }
#endif

        #endregion
    }
}