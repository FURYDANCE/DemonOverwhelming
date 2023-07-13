using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager_MapScene : MonoBehaviour
{
    public GameObject levelInformationUI;
    [Header("创建关卡信息Ui的父对象")]
    public Transform levelInfoParent;
    [Header("是否有打开的关卡信息UI")]
    public bool isInformationing;
    [Header("现在打开的关卡信息UI")]
    public GameObject nowLevelInformation;
    [Header("地图拖拽脚本")]
    public MapDrag mapDrag;

    public static SceneManager_MapScene instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }
    /// <summary>
    /// 当点击关卡图标之后执行的方法，创造关卡详细信息界面
    /// </summary>
    /// <param name="information"></param>
    public void CreateLevelInformation(LevelBtn information)
    {
        if (isInformationing)
            return;
        mapDrag.SetCameraScale(15);
        mapDrag.RefreshPos(information.transform.position, new Vector2(10, -8));
        isInformationing = true;
        GameObject go = Instantiate(levelInformationUI, levelInfoParent);
        nowLevelInformation = go;
        go.name = "LEVEL INFO UI";
        Debug.Log("生成了UI");
        nowLevelInformation.GetComponent<LevelInformationUI>().SetInformation(information);
    }
    /// <summary>
    /// 当点击取消按钮后执行的方法，消除关卡信息界面
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
