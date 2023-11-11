using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DemonOverwhelming;
using BJSYGameCore;
using BJSYGameCore.UI;

namespace DemonOverwhelming
{

    /// <summary>
    /// ��ʾ�ؿ�������Ϣ��UI��ӵ�н����Ӧ�ؿ��ĵ������
    /// </summary>
    public class LevelInformationUI : UIPanel
    {
        public SceneManager_MapScene mapSceneManager;
        [Header("�ؿ��������ֶ���")]
        public TextMeshProUGUI levelNameText;
        [Header("�ؿ�����")]
        public string sceneName;
        [Header("�ؿ���ͼImage����")]
        public Image levelSpriteImage;
        [Header("�ؿ��������ֶ���")]
        public TextMeshProUGUI levelDescriptionText;
        [Header("�ؿ�Ŀ�����ֶ���")]
        public TextMeshProUGUI levelTargetText;
        [SerializeField]
        private Button btn_IntoGameScene;
        [SerializeField]
        private Button btn_ReSelect;
        [SerializeField]
        private string _gameScenePath;
        private void Awake()
        {
            btn_IntoGameScene.onClick.AddListener(delegate
            {
                mapSceneManager.gameManager.OnClickIntoGameSceneButton(_gameScenePath);
            });
            btn_ReSelect.onClick.AddListener(delegate
            {
                Debug.Log("����ѡ��");

                ReSelect();
            });
        }
        public void SetInformation(LevelBtn information)
        {

            levelNameText.text = information.levelName;
            sceneName = information.targetSceneName;
            levelSpriteImage.sprite = information.levelSprite;
            levelDescriptionText.text = information.levelDescription;
            levelTargetText.text = information.leveltargetDescription;
            _gameScenePath = information._gameScenePath;
        }

        public void IntoLevel()
        {
          

        }
        public void ReSelect()
        {
            SceneManager_MapScene.instance.ReSelectLevel();
        }

    }
}