using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("����ѡ�е�ʵ��")]
    public GameObject nowChoosedTarget;
    /// <summary>
    /// ����ĸ���ģʽ������UI�е�slider�����Ǹ���ѡ���ʵ�壩
    /// </summary>
    public CameraControlMode cameraControlMode = CameraControlMode.followUi;

    /// <summary>
    /// �Ӷ����������ȡ�õ����������
    /// </summary>
    Transform cameraFollowTarget;

    public static BattleManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }

    void Start()
    {
        cameraFollowTarget = SceneObjectsManager.instance.GetCameraFollowTarget();
    }


    void Update()
    {
        //����ѡ�е�ʵ��ʱ�����������ѡ���ʵ��
        if (nowChoosedTarget != null)
            CameraFollow_ByChoosedTarget();



        //���ԣ���A��ȡ�������еĲ��Զ��󣬰�S�ͷţ����������Թ��ܣ�
        if (Input.GetKeyDown(KeyCode.A))
            ChooseOneEntity(GameObject.Find("TestA"));
        if (Input.GetKeyDown(KeyCode.S))
            ReleaseChoosedEntity();
    }
    /// <summary>
    /// ����ѡ�е�ʵ��ʱ�������ʼ�������ʵ��
    /// </summary>
    public void CameraFollow_ByChoosedTarget()
    {
        //ֻ�ı��������Ŀ���X�ᣬ���ı�YZ
        cameraFollowTarget.transform.position = new Vector3(nowChoosedTarget.transform.position.x, cameraFollowTarget.transform.position.y, 3);
    }





    /// <summary>
    /// ѡ��ʵ��
    /// 1.�ı�����ĸ���Ŀ�굽ʵ�����ڵ�X��λ�ã��л�UI��ʾģʽ
    /// </summary>
    /// <param name="Entity"></param>
    public void ChooseOneEntity(GameObject Entity)
    {
        if (nowChoosedTarget != null)
            ReleaseChoosedEntity();
        nowChoosedTarget = Entity;
        cameraControlMode = CameraControlMode.followTarget;
    }
    /// <summary>
    /// �ͷ�ѡ���ʵ��
    /// 1.�ı���������Ŀ��ΪĬ��״̬���л�UI��ʾģʽ
    /// </summary>
    public void ReleaseChoosedEntity()
    {
        nowChoosedTarget = null;
        cameraControlMode = CameraControlMode.followUi;
    }
}
