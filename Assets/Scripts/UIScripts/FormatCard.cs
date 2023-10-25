using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
namespace DemonOverwhelming
{
<<<<<<< HEAD
=======
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
>>>>>>> c920aad3 (8.23 修改了战斗界面，加入几个新兵种卡（目前可以同时存在8张卡），修复了兵种生成相关的bug)

    /// <summary>
    /// ��������ϵ����Ķ���,����ק����
    /// </summary>
<<<<<<< HEAD
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
=======
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
>>>>>>> c920aad3 (8.23 修改了战斗界面，加入几个新兵种卡（目前可以同时存在8张卡），修复了兵种生成相关的bug)
        {

            drag = GetComponent<UiDrag>();
            //���ɲ�Ӱ
            connectedSoldierGroup_inScene = Instantiate(connectedSoldierGroup, SceneObjectsManager.instance.allUnitParent);
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
<<<<<<< HEAD
}
=======
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
>>>>>>> c920aad3 (8.23 修改了战斗界面，加入几个新兵种卡（目前可以同时存在8张卡），修复了兵种生成相关的bug)
