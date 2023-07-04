using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

/// <summary>
/// ��������ϵ����Ķ���,����ק����
/// </summary>
public class FormatCard : MonoBehaviour
{
    public SoldierCardParameter parentParameter;
    /// <summary>
    /// ����
    /// </summary>
    [HideInInspector]
    public SoliderGroup connectedSoldierGroup;
    /// <summary>
    /// ���飨�ڳ��������ɺ�ģ�
    /// </summary>
    [HideInInspector]
    public SoliderGroup connectedSoldierGroup_inScene;
    /// <summary>
    /// ��ʼλ��
    /// </summary>
    Vector3 startPos;
    float posX;
    float posY;
    /// <summary>
    /// ��קUI�ű�
    /// </summary>
    UiDrag drag;
    Sprite flagSprite;
    private void Start()
    {
        float x = Random.Range(-80, 80);
        float y = Random.Range(-50, 50);
        drag = GetComponent<UiDrag>();
        //���ɲ�Ӱ
        connectedSoldierGroup_inScene = Instantiate(connectedSoldierGroup, GameObject.Find("FaceToCamera").transform);
        connectedSoldierGroup_inScene.transform.position = SceneObjectsManager.instance.playerEntityGeneratePoint.transform.position;
        //���ò�Ӱ������ʵ��ʿ��ʱ��������ͼ
        connectedSoldierGroup_inScene.SetFlagSprite(flagSprite);
        //�������Ķ�Ӧ�ı���λ�ô���ս��������
        BattleManager.instance.soliderFormatGroups.Add(connectedSoldierGroup_inScene);
        startPos = connectedSoldierGroup_inScene.transform.position;
        //�������ĵ�λ��ƫ��
        transform.position += new Vector3(x, y, 0);
        drag.startPos = transform.position - new Vector3(x, y, 0);
    }
    private void Update()
    {
        ///����ui��ק�����λ�ã��ı��Ӱ��λ��
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
