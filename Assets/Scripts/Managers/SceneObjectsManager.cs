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
        public Image moneyOverImage;
        [Header("costText")]
        public TextMeshProUGUI bloodText;
        public Image bloodOverImage;

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
        public GridLayoutGroup cardSelectUI;
        [Header("�������")]
        public Image formationMakingUI;
        [Header("�����������λ��")]
        public RectTransform formationMakingAreaCenter;
        [Header("Ӣ����Ϣ����")]
        public HeroInformationUI heroInfoUI;
        [Header("��ť�����첨�Σ���������ѡ��������ӵ���¼���")]
        public Button btn_CreateSolider;
        public Button btn_Revoke;
        public Button btn_RevokeAll;

        public static SceneObjectsManager instance;
        private void Awake()
        {
            if (instance != null)
                Destroy(instance);
            instance = this;

        }
        private void Start()
        {
            Initialize();

        }
        public void Initialize()
        {
            btn_CreateSolider.onClick.AddListener(BattleManager.instance.GenerateSoldiers);
            btn_Revoke.onClick.AddListener(BattleManager.instance.RevokeCardSelect);
            btn_RevokeAll.onClick.AddListener(BattleManager.instance.RevokeAllCardSelect);
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
            if (objectInfoUI)
                objectInfoUI.gameObject.SetActive(isTrue);
        }
    }
}