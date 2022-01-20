using Ninito.TheUsualSuspects.Attributes;
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
        [MinMaxSlider(1f, 600f)]
        [SerializeField]
        private Vector2 respawnTime = new Vector2(30f, 60f);

        [SerializeField]
        private GameObject harvestEffect;

        #endregion

        #region Properties

        public float RespawnTime => Random.Range(respawnTime.x, respawnTime.y);
        public GameObject HarvestEffect => harvestEffect;

        #endregion
    }
}