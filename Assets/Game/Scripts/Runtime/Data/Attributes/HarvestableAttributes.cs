using UnityEngine;

namespace Game.Editor.Data.Attributes
{
    /// <summary>
    /// A class that contains the attributes of a harvestable
    /// </summary>
    [CreateAssetMenu(fileName = CreateMenus.HARVESTABLE_ATTRIBUTES_FILENAME,
        menuName = CreateMenus.HARVESTABLE_ATTRIBUTES_MENUNAME, order = CreateMenus.HARVESTABLE_ATTRIBUTES_ORDER)]
    public class HarvestableAttributes : Attributes
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
    }
}