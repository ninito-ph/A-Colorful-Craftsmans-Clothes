using UnityEngine;

namespace Game.Runtime.Data.Attributes
{
    [CreateAssetMenu(fileName = CreateMenus.CLOTHING_ATTRIBUTES_FILENAME,
        menuName = CreateMenus.CLOTHING_ATTRIBUTES_MENUNAME, order = CreateMenus.CLOTHING_ATTRIBUTES_ORDER)]
    public sealed class ClothingAttributes : ScriptableObject
    {
        #region Private Fields

        [Header("Animations")]
        [SerializeField]
        public AnimatorOverrideController AnimatorOverrideController;

        [Header("Crafting")]
        [SerializeField]
        [Min(1)]
        public float DyeCostMultiplier = 1;

        #endregion
    }
}