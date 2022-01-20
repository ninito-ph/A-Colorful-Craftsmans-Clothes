using Game.Runtime.Utility;
using UnityEngine;

namespace Game.Runtime.Data.Attributes
{
    /// <summary>
    /// A class that contains the attributes of a piece of clothing
    /// </summary>
    [CreateAssetMenu(fileName = CreateMenus.CLOTHING_ATTRIBUTES_FILENAME,
        menuName = CreateMenus.CLOTHING_ATTRIBUTES_MENUNAME, order = CreateMenus.CLOTHING_ATTRIBUTES_ORDER)]
    public sealed class ClothingAttributes : ItemAttributes
    {
        #region Private Fields

        [Header("Clothing Properties")]
        [SerializeField]
        private AnimatorOverrideController animatorOverrideController;

        [SerializeField]
        private ClothingType type = ClothingType.Body;

        #endregion

        #region Properties

        public AnimatorOverrideController OverrideController => animatorOverrideController;

        public ClothingType Type => type;

        public float ValueMultiplier => Value * RGBandCMYKUtility.GetSumOfColorComponentsInCMYK(Color);

        #endregion
    }

    public enum ClothingType
    {
        Body,
        Accessory
    }
}