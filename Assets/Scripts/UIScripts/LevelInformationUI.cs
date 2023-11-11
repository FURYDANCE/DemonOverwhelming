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
    /// 显示关卡内容信息的UI，拥有进入对应关卡的点击方法
    /// </summary>
    public class LevelInformationUI : UIPanel
    {
        public SceneManager_MapScene mapSceneManager;
        [Header("关卡名称文字对象")]
        public TextMeshProUGUI levelNameText;
        [Header("关卡名称")]
        public string sceneName;
        [Header("关卡贴图Image对象")]
        public Image levelSpriteImage;
        [Header("关卡描述文字对象")]
        public TextMeshProUGUI levelDescriptionText;
        [Header("关卡目标文字对象")]
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
                Debug.Log("重新选择");

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