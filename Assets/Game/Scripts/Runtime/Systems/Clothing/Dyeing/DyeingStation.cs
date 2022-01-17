using Game.Runtime.Entities.Player;
using Game.Runtime.Systems.Inventory;
using Ninito.UnityExtended.WindowManager;
using Ninito.UsualSuspects.Interactable;
using UnityEngine;

namespace Game.Runtime.Systems.Clothing.Dyeing
{
    /// <summary>
    /// A class that controls the physical dyeing station
    /// </summary>
    public sealed class DyeingStation : MonoBehaviour, IInteractable
    {
        #region Private Fields

        [Header("Dependencies")]
        [SerializeField]
        private DyeingManager dyeingManager;
        
        [SerializeField]
        private WindowManager windowManager;

        [SerializeField]
        private string dyeingMenuKey = "DyeingMenu";

        #endregion
        
        #region IInteractable Implementation

        public string InteractionToolTip => "Press E to open the dyeing menu";

        public void InteractWithAs(IInteractor interactor)
        {
            dyeingManager.PlayerInventory = interactor.GameObject.GetComponent<PlayerInventory>().Contents;
            windowManager.SwitchToMenu(dyeingMenuKey);
        }

        #endregion
    }
}