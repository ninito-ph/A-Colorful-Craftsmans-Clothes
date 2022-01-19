using System;
using Game.Runtime.Data.Registries;
using Ninito.UsualSuspects.Attributes;
using Ninito.UsualSuspects.CommonExtensions;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Runtime.Data.Attributes
{
    /// <summary>
    /// A class that contains attributes of an item.
    /// </summary>
    [CreateAssetMenu(fileName = CreateMenus.ITEM_ATTRIBUTES_FILENAME,
        menuName = CreateMenus.ITEM_ATTRIBUTES_MENUNAME, order = CreateMenus.ITEM_ATTRIBUTES_ORDER)]
    public class ItemAttributes : Attributes
    {
        #region Private Fields

        [Header("Item Properties")]
        [SerializeField]
        [FormerlySerializedAs("itemIcon")]
        private Sprite graphic;

        #endregion

        #region Public Fields

        public Color Color = Color.white;

        [Header("Internal Item Properties")]
        [ReadOnlyField]
        [SerializeField]
        public int ID = -1;

        #endregion

        #region Properties

        public Sprite Graphic => graphic;

        #endregion

        #region Public Methods

        /// <summary>
        /// Stores the item in a serializable class, <see cref="StoredItemAttributes"/>
        /// </summary>
        /// <returns>A JSON string of the ItemAttributes</returns>
        public string Store()
        {
            return JsonUtility.ToJson(new StoredItemAttributes(this));
        }

        /// <summary>
        /// Restores the item from a serialized string.
        /// </summary>
        /// <param name="data">The data to restore from</param>
        /// <param name="registry">The registry to use as a basis for restoring</param>
        /// <returns>The restored item</returns>
        public static ItemAttributes Restore(string data, ItemRegistry registry)
        {
            if (String.IsNullOrEmpty(data)) return null;
            
            StoredItemAttributes storedItemAttributes = JsonUtility.FromJson<StoredItemAttributes>(data);
            ItemAttributes itemAttributes = registry.GetItemByID(storedItemAttributes.ID);

            if (itemAttributes == null)
            {
                Debug.LogError(
                    $"Item with ID {storedItemAttributes.ID} not found in registry {registry.GetType()}. " +
                    $"Maybe you forgot to add it to the registry?");
                return null;
            }

            ItemAttributes itemAttributesInstance = itemAttributes.Clone();
            itemAttributesInstance.Color = storedItemAttributes.Color;

            return itemAttributesInstance;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Description.GetHashCode() ^ Color.GetHashCode();
        }

        public override bool Equals(object other)
        {
            if (other == null) return false;
            return GetHashCode() == other.GetHashCode();
        }

        #endregion
    }
}