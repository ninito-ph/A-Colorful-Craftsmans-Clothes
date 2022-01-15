using Game.Editor;
using UnityEngine;

namespace Game.Core
{
    /// <summary>
    /// A class that allows for components to reference the GameManager even if it is not in the same scene at editor time.
    /// </summary>
    [CreateAssetMenu(fileName = CreateMenus.GAME_MANAGER_PROXY_FILENAME,
        menuName = CreateMenus.GAME_MANAGER_PROXY_MENUNAME, order = CreateMenus.GAME_MANAGER_PROXY_ORDER)]
    public sealed class GameManagerProxy : ScriptableObject
    {
        #region Public Methods

        public void LoadScene(string sceneName) => GameManager.Instance.LoadScene(sceneName);
        public void QuitApplication() => GameManager.Instance.QuitApplication();

        #endregion
    }
}