using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("现在选中的实体")]
    public GameObject nowChoosedTarget;
    /// <summary>
    /// 相机的跟随模式（跟随UI中的slider条还是跟随选择的实体）
    /// </summary>
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



        //测试：按A获取到场景中的测试对象，按S释放（相机跟随测试功能）
        if (Input.GetKeyDown(KeyCode.A))
            ChooseOneEntity(GameObject.Find("TestA"));
        if (Input.GetKeyDown(KeyCode.S))
            ReleaseChoosedEntity();
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
    /// 选择实体
    /// 1.改变相机的跟随目标到实体所在的X轴位置，切换UI显示模式
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
    /// 释放选择的实体
    /// 1.改变相机跟随的目标为默认状态，切换UI显示模式
    /// </summary>
    public void ReleaseChoosedEntity()
    {
        nowChoosedTarget = null;
        cameraControlMode = CameraControlMode.followUi;
    }
}
