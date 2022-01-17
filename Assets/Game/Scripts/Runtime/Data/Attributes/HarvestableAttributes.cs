using UnityEngine;

namespace Game.Runtime.Data.Attributes
{
    /// <summary>
    /// A class that contains the attributes of a harvestable
    /// </summary>
    [CreateAssetMenu(fileName = CreateMenus.HARVESTABLE_ATTRIBUTES_FILENAME,
        menuName = CreateMenus.HARVESTABLE_ATTRIBUTES_MENUNAME, order = CreateMenus.HARVESTABLE_ATTRIBUTES_ORDER)]
    public sealed class HarvestableAttributes : ItemAttributes
    {
        #region Private Fields

        [Header("Harvestable Properties")]
        [SerializeField]
        private float respawnTime;

        [SerializeField]
        private GameObject harvestEffect;

        #endregion

        #region Properties

        public float RespawnTime => respawnTime;
        public GameObject HarvestEffect => harvestEffect;

        #endregion
    }
}