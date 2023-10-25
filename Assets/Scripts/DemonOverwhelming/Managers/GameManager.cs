using BJSYGameCore;
using BJSYGameCore.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DemonOverwhelming
{
    public class GameManager : GlobalManager
    {
        protected override void Initialize()
        {
            base.Initialize();
            (uiManager as IManager<GameManager>).Initialize(this);

            //加载加载界面UI
            LoadingPanel loadingUI = uiManager.ShowLoadingUI();
            //读取配置文件

            //加载主菜单UI
            LoadResourceOperationBase loadOperation = resourceManager.loadAsync<GameObject>(uiManager.mainMenuPrefabPath);
            loadOperation.onCompleted += OnMainMenuPanelLoaded;
            //绑定加载界面
            loadingUI.Display(loadOperation);
        }
        private void OnMainMenuPanelLoaded(object obj)
        {
            MainMenuPanel mainMenuPanel = (obj as GameObject).GetComponent<MainMenuPanel>();

            //加载完成后，隐藏加载界面
            uiManager.getPanel<LoadingPanel>().Hide();
            mainMenuPanel = uiManager.createPanel(mainMenuPanel);
            mainMenuPanel.OnClickStartButton += OnClickStartButtonMainMenu;
            mainMenuPanel.OnClickExitButton += OnClickExitButtonMainMenu;
        }
        private void OnClickStartButtonMainMenu()
        {
            //隐藏主菜单UI
            MainMenuPanel mainMenuPanel = uiManager.getPanel<MainMenuPanel>();
            mainMenuPanel.Hide();
            //显示加载界面
            LoadingPanel loadingUI = uiManager.ShowLoadingUI();
            //加载大地图场景
            LoadSceneOperationBase loadOperation = resourceManager.loadSceneAsync(_mapScenePath, LoadSceneMode.Additive);
            loadOperation.onComplete += OnMapSceneLoaded;
            //绑定加载界面
            loadingUI.Display(loadOperation);
        }
        private void OnMapSceneLoaded(object obj)
        {
            //隐藏加载界面
            uiManager.getPanel<LoadingPanel>().Hide();
        }
        private void OnClickExitButtonMainMenu()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public ResourceManager resourceManager;
        public UIManager uiManager;
        [SerializeField]
        private string _mapScenePath;
        [SerializeField]
        private string _gameScenePath;
    }
}
