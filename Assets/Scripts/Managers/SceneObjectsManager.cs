using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;
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
    [Header("������վݵ�")]
    public GameObject playerFinalStrongHold;
    [Header("GAMEOVER����")]
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
  
}
