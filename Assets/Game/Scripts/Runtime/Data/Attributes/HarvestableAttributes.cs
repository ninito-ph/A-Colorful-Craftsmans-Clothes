using Game.Runtime.Systems.Inventory;
using UnityEngine;

namespace Game.Runtime.Data.Attributes
{
    /// <summary>
    /// A class that contains the attributes of a harvestable
    /// </summary>
    [CreateAssetMenu(fileName = CreateMenus.HARVESTABLE_ATTRIBUTES_FILENAME,
        menuName = CreateMenus.HARVESTABLE_ATTRIBUTES_MENUNAME, order = CreateMenus.HARVESTABLE_ATTRIBUTES_ORDER)]
    public sealed class HarvestableAttributes : Attributes, IItem
    {
        #region Private Fields

        [Header("Harvestable Properties")]
        [SerializeField]
        private string harvestableName;

        [SerializeField]
        private Sprite harvestableSprite;

        [SerializeField]
        private float respawnTime;

        [Header("Visual Effects")]
        [SerializeField]
        private GameObject harvestEffect;

        #endregion

        #region Properties

        public string HarvestableName => harvestableName;
        public Sprite HarvestableSprite => harvestableSprite;
        public float RespawnTime => respawnTime;
        public GameObject HarvestEffect => harvestEffect;

        #endregion

        #region IItem Implementation

        public string Name => harvestableName;
        public Sprite Icon => harvestableSprite;

        #endregion
    }
}