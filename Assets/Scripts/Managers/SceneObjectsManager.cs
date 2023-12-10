using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;
namespace DemonOverwhelming
{
    /// <summary>
    /// 存放场景中需要被调用的对象
    /// 管理场景中可能被其他类调用的对象的管理器，需要调用时调用其中的get方法
    /// 思路是，难免会遇到需要其他类调用的对象，比如计算UI可能需要相机相关的变量，从这个单例中获取到所需的对象更好溯源？
    /// </summary>

    public class SceneObjectsManager : MonoBehaviour
    {

        [Header("金钱text")]
        public TextMeshProUGUI moneyText;
        public Image moneyOverImage;
        [Header("costText")]
        public TextMeshProUGUI bloodText;
        public Image bloodOverImage;

        [Header("所有实体的父对象")]
        public Transform allUnitParent;
        [Header("实体生成点位")]
        public Transform playerEntityGeneratePoint;
        public Transform enemyEntityGeneratePoint;
        [Header("血条UI")]
        public GameObject hpBarObject;
        [Header("相机")]
        [SerializeField]
        private CinemachineVirtualCamera camera;
        /// <summary>
        /// 这是相机跟随的目标，可以看做相机的移动轴，在battlemanager中，当选择了实体之后计算它的对应的位置，不会有y和z轴的运动，只改变x轴
        /// </summary>
        [Header("相机的跟随点位")]
        [SerializeField]
        private Transform cameraFollowTarget;
        [Header("相机的左右边界")]
        [SerializeField]
        public Transform cameraBound_Left;
        [SerializeField]
        public Transform cameraBound_Right;
        [Header("金钱和cost的填充")]
        public Image moneyFill;
        public Image costFill;
        [Header("鼠标悬停时的对象信息显示UI")]
        public ObjectInfoUI objectInfoUI;
        [Header("玩家最终据点")]
        public GameObject playerFinalStrongHold;
        [Header("GAMEOVER界面")]
        public GameOverUI gameOverPanel;
        [Header("战斗界面UI")]
        [Header("UI相关s")]
        public GameObject BattleUI;
        [Header("选卡界面")]
        public GridLayoutGroup cardSelectUI;
        [Header("布阵界面")]
        public Image formationMakingUI;
        [Header("布阵界面中心位置")]
        public RectTransform formationMakingAreaCenter;
        [Header("英雄信息界面")]
        public HeroInformationUI heroInfoUI;
        [Header("按钮：创造波次，撤销，重选（用于添加点击事件）")]
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
        /// 获取相机对象
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
        /// 获取相机的左右边界
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