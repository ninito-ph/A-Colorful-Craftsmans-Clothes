using UnityEngine;

namespace Game.Editor.Data.Attributes
{
    [CreateAssetMenu(fileName = CreateMenus.HARVESTABLE_ATTRIBUTES_FILENAME,
        menuName = CreateMenus.GAME_MANAGER_PROXY_MENUNAME, order = CreateMenus.HARVESTABLE_ATTRIBUTES_ORDER)]
    public class HarvestableAttributes : ScriptableObject
    {
    }
}