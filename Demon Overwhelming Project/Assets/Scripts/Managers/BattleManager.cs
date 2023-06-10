using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    /// <summary>
    /// ����ѡ���ʵ�壨�����ͣ�������棬����û�а���ѡ�����
    /// </summary>
    [Header("����ѡ���ʵ�壨�����ͣ�������棬����û�а���ѡ�����")]
    public GameObject nowChooseingTarget;
    /// <summary>
    /// ��ѡ���˵�ʵ��
    /// </summary>
    [Header("����ѡ�е�ʵ��")]
    public GameObject nowChoosedTarget;
    /// <summary>
    /// ����ĸ���ģʽ������UI�е�slider�����Ǹ���ѡ���ʵ�壩
    /// </summary>
    [HideInInspector]
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


        //���ԣ�������ѡ��Ķ���ʱ���Ҽ�ȷ��ѡ��
        if (nowChooseingTarget != null && Input.GetKeyDown(KeyCode.Mouse1))
        {
            EnshureChooseTarget();
        }
        //������ͷ�
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ReleaseChoosedEntity();
        }
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
    /// ȷ��ѡ�������ָ��ʵ��
    /// </summary>
    public void EnshureChooseTarget()
    {
        ChooseOneEntity(nowChooseingTarget);
    }


    /// <summary>
    /// ѡ��ʵ��
    /// 1.�ı�����ĸ���Ŀ�굽ʵ�����ڵ�X��λ�ã��л�UI��ʾģʽ
    /// 2.�ı���spriterenender�Ĳ�����ʾ
    /// </summary>
    /// <param name="Entity"></param>
    public void ChooseOneEntity(GameObject Entity)
    {
        if (nowChoosedTarget != null)
            ReleaseChoosedEntity();
        nowChoosedTarget = Entity;
        cameraControlMode = CameraControlMode.followTarget;
        nowChoosedTarget.GetComponent<SpriteRenderer>().material = SceneObjectsManager.instance.materialObject.onSelectedMaterial;
    }
    /// <summary>
    /// �ͷ�ѡ���ʵ��
    /// 1.�ı���������Ŀ��ΪĬ��״̬���л�UI��ʾģʽ
    /// 2.��ѡ���ʵ��Ĳ��ʱ��Ĭ��ģʽ
    /// </summary>
    public void ReleaseChoosedEntity()
    {
        if (nowChoosedTarget == null)
            return;
        nowChoosedTarget.GetComponent<SpriteRenderer>().material = SceneObjectsManager.instance.materialObject.defaultMaterial;
        nowChoosedTarget = null;
        cameraControlMode = CameraControlMode.followUi;
    }
}
