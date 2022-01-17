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

        [SerializeField]
        private bool usable = false;

        [SerializeField]
        private int value = 1;

        #endregion

        #region Properties

        public Sprite Graphic => graphic;
        public bool Usable => usable;
        public int Value => value;

        #endregion
    }
}