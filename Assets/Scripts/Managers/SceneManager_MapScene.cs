using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BJSYGameCore.UI;
namespace DemonOverwhelming
{
    public class SceneManager_MapScene : MonoBehaviour
    {
        public GameManager gameManager;
        public LevelInformationUI levelInformationUI;
        [Header("�����ؿ���ϢUi�ĸ�����")]
        public Transform levelInfoParent;
        [Header("�Ƿ��д򿪵Ĺؿ���ϢUI")]
        public bool isInformationing;
        [Header("���ڴ򿪵Ĺؿ���ϢUI")]
        public GameObject nowLevelInformation;
        [Header("��ͼ��ק�ű�")]
        public MapDrag mapDrag;

        public static SceneManager_MapScene instance;
        private void Awake()
        {
            if (instance != null)
                Destroy(instance);
            instance = this;
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        /// <summary>
        /// ������ؿ�ͼ��֮��ִ�еķ���������ؿ���ϸ��Ϣ����
        /// </summary>
        /// <param name="information"></param>
        public void CreateLevelInformation(LevelBtn information)
        {
            if (isInformationing)
                return;
            mapDrag.SetCameraScale(15);
            mapDrag.RefreshPos(information.transform.position, new Vector2(10, -8));
            isInformationing = true;
            //LevelInformationUI createdLevelInformationUI = gameManager.uiManager.createPanel(levelInformationUI);
            LevelInformationUI createdLevelInformationUI = Instantiate(levelInformationUI, levelInfoParent);
            nowLevelInformation = createdLevelInformationUI.gameObject;
            createdLevelInformationUI.name = "LEVEL INFO UI";
            createdLevelInformationUI.mapSceneManager = this;
            createdLevelInformationUI.SetInformation(information);

        }

        /// <summary>
        /// �����ȡ����ť��ִ�еķ����������ؿ���Ϣ����
        /// </summary>
        public void ReSelectLevel()
        {
            isInformationing = false;
            mapDrag.SetCameraScale(0);
            nowLevelInformation.GetComponent<Animator>().Play("LevelInfo_out");
            nowLevelInformation.AddComponent<LifeTime>().lifeTime = 3;
            return;
        }

    }
}
