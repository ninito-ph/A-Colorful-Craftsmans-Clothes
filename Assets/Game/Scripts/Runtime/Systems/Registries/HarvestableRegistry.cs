using Game.Runtime.Data.Attributes;
using UnityEngine;

namespace Game.Runtime.Systems
{
    /// <summary>
    /// A registry of harvestable objects.
    /// </summary>
    [CreateAssetMenu(fileName = CreateMenus.HARVESTABLE_REGISTRY_FILENAME,
        menuName = CreateMenus.HARVESTABLE_REGISTRY_MENUNAME, order = CreateMenus.HARVESTABLE_REGISTRY_ORDER)]
    public sealed class HarvestableRegistry : Registry<HarvestableAttributes>
    {
    }
}