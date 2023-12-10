using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
namespace DemonOverwhelming
{

    /// <summary>
    /// ��������ϵ����Ķ���,����ק����
    /// Ҳ�����˴����¶�����������ݣ�������������е�λʱ����ս�����������������еı����������ɶ�Ӧ�ĵ�λ
    /// </summary>
    public class FormatCard : MonoBehaviour
    {
        /// <summary>
        /// ��Ҫ������
        /// </summary>
        public SoldierCardParameter parentParameter;

        /// <summary>
        /// ��Ӱ����
        /// </summary>
        public GameObject shadowObject;


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

            //Ϊ���Ⲽ���ص������������λ��
            x = Random.Range(-80, 80);
            y = Random.Range(-50, 50);
            transform.position += new Vector3(x, y, 0);
            startPos = transform.position;

            //�����Ӱ�ĳ�ʼλ��
            Vector3 shadowPos = GetShadowPos();

            //���ɻ�Ӱ
            GameObject go = Instantiate(new GameObject());
            go.transform.position = shadowPos;
            shadowObject = go;
            go.name = "shadow_" + parentParameter.name;
            //���������е����ͣ����ɸ�������Ļ�Ӱ�������������λ��
            foreach (Vector3 offset in parentParameter.formation.soldierOffsets)
            {
                GameObject shadow = Instantiate(new GameObject(), go.transform);
                shadow.name = "shadow_" + offset;
                shadow.transform.position = shadowObject.transform.position + offset;
                shadow.AddComponent<SpriteRenderer>().sprite = parentParameter.sprite;
            }

            //Ϊս���������Ĳ��󿨼����������
            BattleManager.instance.AddOneFormatCard(this);



        }
        private void Update()
        {
            shadowObject.transform.position = GetShadowPos();
        }

        /// <summary>
        /// ��ȡ��ӰӦ���ڵ�λ��
        /// </summary>
        public Vector3 GetShadowPos()
        {
            Vector3 startOffset = GetRelativeOffsetToCenter();
            //�����Ӱ�ĳ�ʼλ��
            Vector3 shadowPos = SceneObjectsManager.instance.playerEntityGeneratePoint.position + new Vector3(startOffset.x * 0.1f, 0, startOffset.y * 0.35f);
            return shadowPos;
        }

        /// <summary>
        /// ��ȡ�����������ĵ����ƫ����
        /// </summary>
        public Vector3 GetRelativeOffsetToCenter()
        {
            Vector3 v = SceneObjectsManager.instance.formationMakingAreaCenter.position - transform.position;
            return v;
        }

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="parameter"></param>
        public void SetParentParameter(SoldierCardParameter parameter)
        {
            parentParameter = parameter;
            if (parameter.flagSprite != null)
                GetComponent<Image>().sprite = parameter.flagSprite;
        }
        public void ClearThis()
        {
            BattleManager.instance.allSelectedCards.Remove(this);
            Destroy(shadowObject.gameObject);
            Destroy(gameObject);
            //BattleManager.instance.soliderFormatGroups.Remove(connectedSoldierGroup_inScene);
            //Destroy(connectedSoldierGroup_inScene.gameObject);
        }
        public void SetFlagSprite(Sprite sprite)
        {
            this.flagSprite = sprite;
        }
    }
}