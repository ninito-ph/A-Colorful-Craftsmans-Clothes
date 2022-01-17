using UnityEngine;

namespace Game.Runtime.Data.Attributes
{
    [CreateAssetMenu(fileName = CreateMenus.CLOTHING_ATTRIBUTES_FILENAME,
        menuName = CreateMenus.CLOTHING_ATTRIBUTES_MENUNAME, order = CreateMenus.CLOTHING_ATTRIBUTES_ORDER)]
    public sealed class ClothingAttributes : ItemAttributes
    {
        #region Private Fields

        [Header("Clothing Properties")]
        [SerializeField]
        public AnimatorOverrideController AnimatorOverrideController;

        [SerializeField]
        [Min(1)]
        public float DyeCostMultiplier = 1;

        #endregion

        #region Properties

        public Color Color { get; set; }

        #endregion
    }
}