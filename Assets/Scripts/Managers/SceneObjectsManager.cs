using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;
/// <summary>
/// 存放场景中需要被调用的对象
/// 管理场景中可能被其他类调用的对象的管理器，需要调用时调用其中的get方法
/// 思路是，难免会遇到需要其他类调用的对象，比如计算UI可能需要相机相关的变量，从这个单例中获取到所需的对象更好溯源？
/// </summary>
public class SceneObjectsManager : MonoBehaviour
{
    [Header("金钱text")]
    public TextMeshProUGUI moneyText;
    [Header("costText")]
    public TextMeshProUGUI bloodText;
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
    [Header("玩家最终据点")]
    public GameObject playerFinalStrongHold;
    [Header("GAMEOVER界面")]
    public GameOverUI gameOverPanel;
    public static SceneObjectsManager instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
        
        if (camera == null)
            camera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        if (cameraBound_Left == null)
            cameraBound_Left = GameObject.Find("LeftCameraBound").transform;
        if (cameraBound_Right == null)
            cameraBound_Right = GameObject.Find("RightCameraBound").transform;
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
  
}
