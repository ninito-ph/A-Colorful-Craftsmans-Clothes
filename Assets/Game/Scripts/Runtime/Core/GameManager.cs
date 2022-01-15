using UnityEngine;

namespace Game.Core
{
    /// <summary>
    /// A class that manages
    /// </summary>
    public sealed class GameManager : Ninito.UsualSuspects.Common.Singleton.GameManager
    {
        public override void LoadScene(string sceneName)
        {
            LoadingScreenManager.LoadScene(sceneName);
            base.LoadScene("SA_LoadingScreen");
        }

        public override void QuitApplication()
        {
            base.QuitApplication();
        }
    }
}
