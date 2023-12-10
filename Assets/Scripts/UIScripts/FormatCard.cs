using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
namespace DemonOverwhelming
{

    /// <summary>
    /// 布阵界面上的旗帜对象,有拖拽功能
    /// 也保存了创建新对象所需的数据，在玩家生成所有单位时，由战斗管理器来遍历所有的兵布阵卡来生成对应的单位
    /// </summary>
    public class FormatCard : MonoBehaviour
    {
        /// <summary>
        /// 需要的数据
        /// </summary>
        public SoldierCardParameter parentParameter;

        /// <summary>
        /// 幻影对象
        /// </summary>
        public GameObject shadowObject;


        /// <summary>
        /// 初始位置
        /// </summary>
        Vector3 startPos;

        /// <summary>
        /// 拖拽UI脚本
        /// </summary>
        UiDrag drag;
        Sprite flagSprite;

        public bool noOffset;
        public float x, y;
        private void Start()
        {

            drag = GetComponent<UiDrag>();

            //为避免布阵卡重叠，让自身随机位移
            x = Random.Range(-80, 80);
            y = Random.Range(-50, 50);
            transform.position += new Vector3(x, y, 0);
            startPos = transform.position;

            //计算幻影的初始位置
            Vector3 shadowPos = GetShadowPos();

            //生成幻影
            GameObject go = Instantiate(new GameObject());
            go.transform.position = shadowPos;
            shadowObject = go;
            go.name = "shadow_" + parentParameter.name;
            //根据数据中的阵型，生成各个具体的幻影对象并设置其相对位置
            foreach (Vector3 offset in parentParameter.formation.soldierOffsets)
            {
                GameObject shadow = Instantiate(new GameObject(), go.transform);
                shadow.name = "shadow_" + offset;
                shadow.transform.position = shadowObject.transform.position + offset;
                shadow.AddComponent<SpriteRenderer>().sprite = parentParameter.sprite;
            }

            //为战斗管理器的布阵卡集合添加自身
            BattleManager.instance.AddOneFormatCard(this);



        }
        private void Update()
        {
            shadowObject.transform.position = GetShadowPos();
        }

        /// <summary>
        /// 获取幻影应该在的位置
        /// </summary>
        public Vector3 GetShadowPos()
        {
            Vector3 startOffset = GetRelativeOffsetToCenter();
            //计算幻影的初始位置
            Vector3 shadowPos = SceneObjectsManager.instance.playerEntityGeneratePoint.position + new Vector3(startOffset.x * 0.1f, 0, startOffset.y * 0.35f);
            return shadowPos;
        }

        /// <summary>
        /// 获取自身到布阵卡中心的相对偏移量
        /// </summary>
        public Vector3 GetRelativeOffsetToCenter()
        {
            Vector3 v = SceneObjectsManager.instance.formationMakingAreaCenter.position - transform.position;
            return v;
        }

        /// <summary>
        /// 设置所需变量
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