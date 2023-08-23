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
 
    public SoliderGroup connectedSoldierGroup;
    public Shadow connectedShadow;
    /// <summary>
    /// ���飨�ڳ��������ɺ�ģ�
    /// </summary>

    public SoliderGroup connectedSoldierGroup_inScene;
    /// <summary>
    /// ��ʼλ��
    /// </summary>
    Vector3 startPos;

    /// <summary>
    /// ��קUI�ű�
    /// </summary>
    UiDrag drag;
    Sprite flagSprite;

    public bool noOffset;
    public float x, y;
    private void Start()
    {
       
        drag = GetComponent<UiDrag>();
        //���ɲ�Ӱ
        connectedSoldierGroup_inScene = Instantiate(connectedSoldierGroup, GameObject.Find("FaceToCamera").transform);
        connectedSoldierGroup_inScene.transform.position = SceneObjectsManager.instance.playerEntityGeneratePoint.transform.position;
        //���ñ���
        connectedSoldierGroup_inScene.SetParentFormatCard(this);
        //���ò�Ӱ������ʵ��ʿ��ʱ��������ͼ
        connectedSoldierGroup_inScene.SetFlagSprite(flagSprite);
        //���ò�Ӱ�ľ�����ͼ
        connectedSoldierGroup_inScene.SetSoldierShadowSprite(parentParameter.sprite);

        //�������Ķ�Ӧ�ı���λ�ô���ս��������
        BattleManager.instance.soliderFormatGroups.Add(connectedSoldierGroup_inScene);
        startPos = connectedSoldierGroup_inScene.transform.position;
        if (!noOffset)
        {
            Debug.LogError("��OFFSET��");
            //�������ĵ�λ��ƫ��
            x = Random.Range(-80, 80);
            y = Random.Range(-50, 50);

          

        }
        transform.position += new Vector3(x, y, 0);
        drag.startPos = transform.position - new Vector3(x, y, 0);
    }
    private void Update()
    {
        //����ui��ק�����λ�ã��ı��Ӱ��λ��
        if (connectedSoldierGroup_inScene)
        {
            connectedSoldierGroup_inScene.transform.position = startPos + new Vector3(drag.relativeGap.x * 0.15f, drag.relativeGap.y * 0.35f + 4, 0);
        }
    }
    public void SetParentParameter(SoldierCardParameter parameter)
    {
        parentParameter = parameter;
        if (parameter.flagSprite != null)
            GetComponent<Image>().sprite = parameter.flagSprite;
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
    public void SetConnectGroup(SoliderGroup soliderGroup)
    {
        connectedSoldierGroup_inScene = soliderGroup;
    }
}
