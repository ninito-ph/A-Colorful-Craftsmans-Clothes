using Game.Runtime.Data.Attributes;
using UnityEngine;

namespace Game.Runtime.Data.Registries
{
    /// <summary>
    /// A registry of harvestable objects.
    /// </summary>
    [CreateAssetMenu(fileName = CreateMenus.HARVESTABLE_REGISTRY_FILENAME,
        menuName = CreateMenus.HARVESTABLE_REGISTRY_MENUNAME, order = CreateMenus.HARVESTABLE_REGISTRY_ORDER)]
    public sealed class HarvestableRegistry : Registry<HarvestableAttributes>
    {
        #region Private Methods

#if UNITY_EDITOR
        // Normally I wouldn't need to do this, but ContextMenu not showing up in inherited classes
        // is a regression bug present in this Unity version :(
        [ContextMenu("Auto fill")]
        private void FillSelf() => GetAllProjectItems();
#endif

        #endregion
    }
}