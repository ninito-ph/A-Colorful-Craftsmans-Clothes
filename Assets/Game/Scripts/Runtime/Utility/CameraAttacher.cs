using Game.Runtime.Core;
using Ninito.TheUsualSuspects.Utilities;
using UnityEngine;

namespace Game.Runtime.Utility
{
    /// <summary>
    /// A script that attaches its parent transform to the player
    /// </summary>
    public sealed class CameraAttacher : MonoBehaviour
    {
        #region Private Fields

        [Header("Configurations")]
        [SerializeField]
        private bool preserveScale = true;

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            Vector3 scale = transform.localScale;
            
            transform.SetParent(CameraUtilities.MainCamera.transform);

            if (preserveScale)
            {
                transform.localScale = scale;
            }
        }

        #endregion
    }
}