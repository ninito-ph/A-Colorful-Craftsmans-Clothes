using System.Linq;
using Game.Runtime.Systems.Inventory;
using Ninito.UsualSuspects.Attributes;
using UnityEngine;

namespace Game.Runtime.Systems
{
    /// <summary>
    /// A registry of items.
    /// </summary>
    [CreateAssetMenu(fileName = CreateMenus.ITEM_REGISTRY_FILENAME,
        menuName = CreateMenus.ITEM_REGISTRY_MENUNAME, order = CreateMenus.ITEM_REGISTRY_ORDER)]
    public sealed class ItemRegistry : Registry<IItem>
    {
        #region Private Fields

        [ReadOnlyField]
        [SerializeField]
        private int totalItemCount;

        #endregion
        
        #region Public Methods

        public IItem GetItemByHash(int itemHash)
        {
            return Items.FirstOrDefault(item => item.GetHashCode() == itemHash);
        }

        #endregion
        
        #region Private Methods

        [ContextMenu("Get All Items In Project")]
        private void GetAllProjectItems()
        {
            Items = FindObjectsOfType<ScriptableObject>().OfType<IItem>().ToList();
            totalItemCount = Items.Count;
        }

        #endregion
    }
}