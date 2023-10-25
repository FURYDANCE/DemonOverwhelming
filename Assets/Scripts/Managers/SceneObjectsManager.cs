using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;
namespace DemonOverwhelming
{
    /// <summary>
    /// ��ų�������Ҫ�����õĶ���
    /// �������п��ܱ���������õĶ���Ĺ���������Ҫ����ʱ�������е�get����
    /// ˼·�ǣ������������Ҫ��������õĶ��󣬱������UI������Ҫ�����صı���������������л�ȡ������Ķ��������Դ��
    /// </summary>

    public class SceneObjectsManager : MonoBehaviour
    {

        [Header("��Ǯtext")]
        public TextMeshProUGUI moneyText;
        [Header("costText")]
        public TextMeshProUGUI bloodText;
        [Header("����ʵ��ĸ�����")]
        public Transform allUnitParent;
        [Header("ʵ�����ɵ�λ")]
        public Transform playerEntityGeneratePoint;
        public Transform enemyEntityGeneratePoint;
        [Header("Ѫ��UI")]
        public GameObject hpBarObject;
        [Header("���")]
        [SerializeField]
        private CinemachineVirtualCamera camera;
        /// <summary>
        /// ������������Ŀ�꣬���Կ���������ƶ��ᣬ��battlemanager�У���ѡ����ʵ��֮��������Ķ�Ӧ��λ�ã�������y��z����˶���ֻ�ı�x��
        /// </summary>
        [Header("����ĸ����λ")]
        [SerializeField]
        private Transform cameraFollowTarget;
        [Header("��������ұ߽�")]
        [SerializeField]
        public Transform cameraBound_Left;
        [SerializeField]
        public Transform cameraBound_Right;
        [Header("��Ǯ��cost�����")]
        public Image moneyFill;
        public Image costFill;
        [Header("�����ͣʱ�Ķ�����Ϣ��ʾUI")]
        public ObjectInfoUI objectInfoUI;
        [Header("������վݵ�")]
        public GameObject playerFinalStrongHold;
        [Header("GAMEOVER����")]
        public GameOverUI gameOverPanel;
        [Header("ս������UI")]
        [Header("UI���s")]
        public GameObject BattleUI;
        [Header("ѡ������")]
        public Image cardSelectUI;
        [Header("�������")]
        public Image formationMakingUI;
        [Header("Ӣ����Ϣ����")]
        public HeroInformationUI heroInfoUI;
        public static SceneObjectsManager instance;
        private void Awake()
        {
            if (instance != null)
                Destroy(instance);
            instance = this;
            Initialize();

        }
        public void Initialize()
        {
            if (camera == null)
                camera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
            if (cameraBound_Left == null)
                cameraBound_Left = GameObject.Find("LeftCameraBound").transform;
            if (cameraBound_Right == null)
                cameraBound_Right = GameObject.Find("RightCameraBound").transform;
            if (playerEntityGeneratePoint == null)
                playerEntityGeneratePoint = GameObject.Find("EntityGenerateArea_Left").transform;
            if (enemyEntityGeneratePoint == null)
                enemyEntityGeneratePoint = GameObject.Find("EntityGenerateArea_Right").transform;
            if (moneyFill == null)
                moneyFill = GameObject.Find("MoneyFill").GetComponent<Image>();
            if (costFill == null)
                costFill = GameObject.Find("BolldFill").GetComponent<Image>();
            if (objectInfoUI == null)
                objectInfoUI = GameObject.Find("ObjectInfoUI").GetComponent<ObjectInfoUI>();
            if (playerFinalStrongHold == null)
                playerFinalStrongHold = GameObject.Find("Player's Stronghold");
            if (gameOverPanel == null)
                gameOverPanel = GameObject.Find("GameOverUi").GetComponent<GameOverUI>();
            if (!BattleUI)
                BattleUI = GameObject.Find("BattleUI");
            if (!cardSelectUI)
                cardSelectUI = GameObject.Find("CardSelectArea").GetComponent<Image>();
            if (!formationMakingUI)
                formationMakingUI = GameObject.Find("FormationMakingArea").GetComponent<Image>();
            if (!heroInfoUI)
                heroInfoUI = GameObject.Find("HeroInfoArea").GetComponent<HeroInformationUI>();
        }

        /// <summary>
        /// ��ȡ�������
        /// </summary>
        /// <returns></returns>
        public Transform GetCamera()
        {
            return camera.transform;
        }
        public CinemachineVirtualCamera GetCameraScript()
        {
            return camera;
        }
        public Transform GetCameraFollowTarget()
        {
            return cameraFollowTarget;
        }
        /// <summary>
        /// ��ȡ��������ұ߽�
        /// </summary>
        /// <param name="L"></param>
        /// <param name="R"></param>
        public void GetCameraBound(out Transform L, out Transform R)
        {
            L = cameraBound_Left;
            R = cameraBound_Right;
        }

        public void ShowObjectInfoUI(bool isTrue)
        {
            objectInfoUI.gameObject.SetActive(isTrue);
        }
    }
}
