using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

/// <summary>
/// 布阵界面上的旗帜对象,有拖拽功能
/// </summary>
public class FormatCard : MonoBehaviour
{
    public SoldierCardParameter parentParameter;
    /// <summary>
    /// 兵组
    /// </summary>
    [HideInInspector]
    public SoliderGroup connectedSoldierGroup;
    /// <summary>
    /// 兵组（在场景中生成后的）
    /// </summary>
    [HideInInspector]
    public SoliderGroup connectedSoldierGroup_inScene;
    /// <summary>
    /// 初始位置
    /// </summary>
    Vector3 startPos;
    float posX;
    float posY;
    /// <summary>
    /// 拖拽UI脚本
    /// </summary>
    UiDrag drag;
    Sprite flagSprite;
    private void Start()
    {
        float x = Random.Range(-80, 80);
        float y = Random.Range(-50, 50);
        drag = GetComponent<UiDrag>();
        //生成残影
        connectedSoldierGroup_inScene = Instantiate(connectedSoldierGroup, GameObject.Find("FaceToCamera").transform);
        connectedSoldierGroup_inScene.transform.position = SceneObjectsManager.instance.playerEntityGeneratePoint.transform.position;
        //设置残影在生成实体士兵时的旗帜贴图
        connectedSoldierGroup_inScene.SetFlagSprite(flagSprite);
        //将该旗帜对应的兵组位置传给战斗管理器
        BattleManager.instance.soliderFormatGroups.Add(connectedSoldierGroup_inScene);
        startPos = connectedSoldierGroup_inScene.transform.position;
        //设置旗帜的位置偏移
        transform.position += new Vector3(x, y, 0);
        drag.startPos = transform.position - new Vector3(x, y, 0);
    }
    private void Update()
    {
        ///计算ui拖拽的相对位置，改变残影的位置
        if (connectedSoldierGroup_inScene)
        {
            connectedSoldierGroup_inScene.transform.position = startPos + new Vector3(drag.relativeGap.x * 0.15f, drag.relativeGap.y * 0.35f + 4, 0);
        }
    }
    public void SetParentParameter(SoldierCardParameter parameter)
    {
        parentParameter = parameter;
    }
    public void ClearThis()
    {
        BattleManager.instance.soliderFormatGroups.Remove(connectedSoldierGroup_inScene);
        Destroy(connectedSoldierGroup_inScene.gameObject);
    }
    public void SetFlagSprite(Sprite sprite)
    {
        this.flagSprite = sprite;
    }
}
