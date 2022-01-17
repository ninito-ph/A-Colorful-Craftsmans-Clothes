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
        private AnimatorOverrideController animatorOverrideController;

        [SerializeField]
        [Min(1)]
        private float dyeCostMultiplier = 1;

        #endregion

        #region Properties

        public AnimatorOverrideController OverrideController => animatorOverrideController;

        public float DyeCostMultiplier => dyeCostMultiplier;

        #endregion

    }
}