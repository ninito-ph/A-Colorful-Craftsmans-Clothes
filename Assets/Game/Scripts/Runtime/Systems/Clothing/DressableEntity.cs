using System;
using Game.Runtime.Data.Attributes;
using Game.Runtime.Data.Registries;
using Game.Runtime.Systems.Save;
using Game.Runtime.Systems.Save.Saves;
using Ninito.UsualSuspects.Attributes;
using UnityEngine;

namespace Game.Runtime.Systems.Clothing
{
    /// <summary>
    /// A component that can be used to dress an entity
    /// </summary>
    public sealed class DressableEntity : MonoBehaviour
    {
        #region Private Fields

        [Header("Configuration")]
        [SerializeField]
        private bool saveAndLoad = false;

        [ShowIf(nameof(saveAndLoad))]
        [SerializeField]
        private ItemRegistry itemRegistry;

        [Header("Slots")]
        [SerializeField]
        private ClothingSlot accessorySlot = new ClothingSlot();

        [SerializeField]
        private ClothingSlot bodySlot = new ClothingSlot();

        #endregion

        #region Properties

        public ClothingSlot BodySlot => bodySlot;
        public ClothingSlot AccessorySlot => accessorySlot;
        public string SaveKey => gameObject.name + ".DressableEntity";

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            if (saveAndLoad)
            {
                Load();
            }
        }

        private void OnDestroy()
        {
            if (saveAndLoad)
            {
                Save();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads the saved accessory and body slot and equips them
        /// </summary>
        private void Load()
        {
            if (!DataLoader.TryLoad(SaveKey, out DressableEntitySave save)) return;
            RestoreBodySlot(save);
            RestoreAccessorySlot(save);
        }

        
        /// <summary>
        /// Restores the saved accessory slot
        /// </summary>
        /// <param name="save">The save to read from</param>
        private void RestoreAccessorySlot(DressableEntitySave save)
        {
            if (String.IsNullOrEmpty(save.AccessorySlotItem)) return;
            ItemAttributes clothing = ItemAttributes.Restore(save.AccessorySlotItem, itemRegistry);
            accessorySlot.SetArticle(clothing as ClothingAttributes);
        }

        /// <summary>
        /// Restores the saved body slot
        /// </summary>
        /// <param name="save">The save to read from</param>
        private void RestoreBodySlot(DressableEntitySave save)
        {
            if (String.IsNullOrEmpty(save.BodySlotItem)) return;
            ItemAttributes clothing = ItemAttributes.Restore(save.BodySlotItem, itemRegistry);
            bodySlot.SetArticle(clothing as ClothingAttributes);
        }

        /// <summary>
        /// Saves the accessory and body slot
        /// </summary>
        private void Save()
        {
            string accessoryData = accessorySlot.Article != null ? accessorySlot.Article.Store() : String.Empty;
            string bodyData = bodySlot.Article != null ? bodySlot.Article.Store() : String.Empty;

            DressableEntitySave save = new DressableEntitySave(accessoryData, bodyData);
            DataSaver.SaveData(save, SaveKey);
        }

        #endregion
    }
}