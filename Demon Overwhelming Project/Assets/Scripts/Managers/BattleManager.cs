using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    /// <summary>
    /// 待定选择的实体（鼠标悬停在了上面，但还没有按下选择键）
    /// </summary>
    [Header("待定选择的实体（鼠标悬停在了上面，但还没有按下选择键）")]
    public GameObject nowChooseingTarget;
    /// <summary>
    /// 被选择了的实体
    /// </summary>
    [Header("现在选中的实体")]
    public GameObject nowChoosedTarget;
    /// <summary>
    /// 相机的跟随模式（跟随UI中的slider条还是跟随选择的实体）
    /// </summary>
    [HideInInspector]
    public CameraControlMode cameraControlMode = CameraControlMode.followUi;

    /// <summary>
    /// 从对象管理器中取得的相机跟随轴
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
        //当有选中的实体时，相机跟随所选择的实体
        if (nowChoosedTarget != null)
            CameraFollow_ByChoosedTarget();


        //测试：有正在选择的对象时按右键确认选择
        if (nowChooseingTarget != null && Input.GetKeyDown(KeyCode.Mouse1))
        {
            EnshureChooseTarget();
        }
        //按左键释放
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ReleaseChoosedEntity();
        }
    }
    /// <summary>
    /// 当有选中的实体时，相机开始跟随这个实体
    /// </summary>
    public void CameraFollow_ByChoosedTarget()
    {
        //只改变相机跟随目标的X轴，不改变YZ
        cameraFollowTarget.transform.position = new Vector3(nowChoosedTarget.transform.position.x, cameraFollowTarget.transform.position.y, 3);
    }

    /// <summary>
    /// 确定选择鼠标所指的实体
    /// </summary>
    public void EnshureChooseTarget()
    {
        ChooseOneEntity(nowChooseingTarget);
    }


    /// <summary>
    /// 选择实体
    /// 1.改变相机的跟随目标到实体所在的X轴位置，切换UI显示模式
    /// 2.改变其spriterenender的材质显示
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
    /// 释放选择的实体
    /// 1.改变相机跟随的目标为默认状态，切换UI显示模式
    /// 2.将选择的实体的材质变回默认模式
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
