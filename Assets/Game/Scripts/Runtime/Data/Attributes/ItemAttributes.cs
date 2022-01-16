using UnityEngine;

namespace Game.Runtime.Data.Attributes
{
    /// <summary>
    /// A class that contains attributes of an item.
    /// </summary>
    [CreateAssetMenu(fileName = CreateMenus.ITEM_ATTRIBUTES_FILENAME,
        menuName = CreateMenus.ITEM_ATTRIBUTES_MENUNAME, order = CreateMenus.ITEM_ATTRIBUTES_ORDER)]
    public sealed class ItemAttributes : ScriptableObject
    {
        #region Private Fields

        [Header("Visual")]
        [SerializeField]
        private string itemName;

        [SerializeField]
        private Sprite itemIcon;

        [Header("Properties")]
        [SerializeField]
        private bool usable = false;

        [SerializeField]
        private int value = 1;

        #endregion

        #region Properties

        public string Name => itemName;
        public Sprite Icon => itemIcon;
        public bool Usable => usable;
        public int Value => value;

        #endregion
    }
}