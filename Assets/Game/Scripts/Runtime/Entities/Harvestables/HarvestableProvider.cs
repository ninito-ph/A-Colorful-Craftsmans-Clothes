using Game.Runtime.Data.Attributes;
using UnityEngine;

namespace Game.Runtime.Entities.Harvestables
{
    /// <summary>
    /// A class that provides a HarvestableAttributes randomly
    /// </summary>
    [CreateAssetMenu(fileName = CreateMenus.HARVESTABLE_PROVIDER_FILENAME,
        menuName = CreateMenus.HARVESTABLE_PROVIDER_MENUNAME, order = CreateMenus.HARVESTABLE_PROVIDER_ORDER)]
    public sealed class HarvestableProvider : ScriptableObject
    {
        #region Private Fields

        [Header("Configurations")]
        [SerializeField]
        private HarvestableAttributes[] existingHarvestables;

        #endregion

        #region Public Methods

        /// <summary>
        /// Get a random HarvestableAttributes
        /// </summary>
        /// <returns>A random HarvestableAttributes</returns>
        public HarvestableAttributes GetHarvestable()
        {
            return existingHarvestables[Random.Range(0, existingHarvestables.Length)];
        }

        #endregion
    }
}