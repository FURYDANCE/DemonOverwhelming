using BJSYGameCore;
using BJSYGameCore.UI;
using UnityEngine;

namespace DemonOverwhelming
{
    public class UIManager : BJSYGameCore.UI.UIManager, IManager<GameManager>
    {
        public LoadingPanel ShowLoadingUI()
        {
            if (getPanel<LoadingPanel>() is LoadingPanel loadingUI)
            {
                loadingUI.Show();
                return loadingUI;
            }
            //实例化加载界面
            loadingUI = createPanel(loadingUIPrefab);
            return loadingUI;
        }
        public LoadingPanel loadingUIPrefab;
        public string mainMenuPrefabPath;
        public GameManager gameManager { get; set; }
    }
}
