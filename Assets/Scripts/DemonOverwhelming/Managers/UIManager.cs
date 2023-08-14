using BJSYGameCore;
using BJSYGameCore.UI;
using UnityEngine;

namespace DemonOverwhelming
{
    public class UIManager : BJSYGameCore.UI.UIManager, IManager<GameManager>
    {
        public LoadingPanel ShowLoadingUI()
        {
            //实例化加载界面
            LoadingPanel loadingUI = createPanel(loadingUIPrefab);
            return loadingUI;
        }
        public T GetPanel<T>() where T : UIObject
        {
            return GetComponentInChildren<T>(true);
        }
        public LoadingPanel loadingUIPrefab;
        public string mainMenuPrefabPath;
        public GameManager gameManager { get; set; }
    }
}
