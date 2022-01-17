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
        [Min(0.1f)]
        private float dyeCostMultiplier = 1;
        
        [SerializeField]
        private ClothingType type = ClothingType.Body;

        #endregion

        #region Properties

        public AnimatorOverrideController OverrideController => animatorOverrideController;

        public float DyeCostMultiplier => dyeCostMultiplier;
        
        public ClothingType Type => type;

        #endregion
    }

    public enum ClothingType
    {
        Body,
        Accessory
    }
}