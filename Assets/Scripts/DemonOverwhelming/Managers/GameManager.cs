using BJSYGameCore;
using BJSYGameCore.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemonOverwhelming
{
    public class GameManager : GlobalManager
    {
        protected override void Initialize()
        {
            base.Initialize();
            (uiManager as IManager<GameManager>).Initialize(this);

            //���ؼ��ؽ���UI
            LoadingPanel loadingUI = uiManager.ShowLoadingUI();
            //��ȡ�����ļ�

            //�������˵�UI
            LoadResourceOperationBase loadOperation = resourceManager.loadAsync<GameObject>(uiManager.mainMenuPrefabPath);
            loadOperation.onCompleted += OnMainMenuPanelLoaded;
            //�󶨼��ؽ���
            loadingUI.Display(loadOperation);
        }
        private void OnMainMenuPanelLoaded(object obj)
        {
            MainMenuPanel mainMenuPanel = (obj as GameObject).GetComponent<MainMenuPanel>();

            //������ɺ����ؼ��ؽ���
            uiManager.getPanel<LoadingPanel>().Hide();
            mainMenuPanel = uiManager.createPanel(mainMenuPanel);
            mainMenuPanel.OnClickStartButton += OnClickStartButtonMainMenu;
            mainMenuPanel.OnClickExitButton += OnClickExitButtonMainMenu;
        }
        private void OnClickStartButtonMainMenu()
        {

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
    }
}
