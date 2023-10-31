using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{
    /// <summary>
    /// 兵组的父对象，有旗帜的spriteRenender，作为预制体在生成兵组时被调用
    /// </summary>
    public class SoliderGroup : MonoBehaviour
    {
        /// <summary>
        /// 展示生成位置的虚影
        /// </summary>
        public Transform[] soldiers;
        public Vector3[] unitOffsetsToCaptain;
        /// <summary>
        /// 所有在场内生成且存活的，作为子对象的士兵s
        /// </summary>
        public List<Entity> createdSoldiers;
        /// <summary>
        /// 阵营
        /// </summary>
        [HideInInspector]
        public Camp camp;

        public FormatCard parentFormatCard;
        public string finalSoldierId;
        public SpriteRenderer flagSprite;
        /// <summary>
        /// 当前旗帜跟随的单位，也就是一队的队长
        /// </summary>
        public Entity flagFollowingSoldier;
        /// <summary>
        /// 完成重整阵型的队员的数量，用于判断整个队伍的重整阵型是否全部完成
        /// </summary>
        int settledUnitAmount;
        /// <summary>
        /// 是否正在重整阵型
        /// </summary>
        bool isReorganizing;
        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            CheckUnitBackingToRelativePos();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            unitOffsetsToCaptain = new Vector3[soldiers.Length];
            Debug.Log("soldier数量：" + soldiers.Length);

            //给残影对象加上面向相机的效果
            foreach (Transform t in soldiers)
            {
                t.gameObject.AddComponent<FaceToCamera>();
            }
            for (int i = 0; i < soldiers.Length; i++)
            {
                unitOffsetsToCaptain[i] = soldiers[0].transform.position - soldiers[i].transform.position;
            }
        }
        /// <summary>
        /// 设置旗帜贴图
        /// </summary>
        /// <param name="sprite"></param>
        public void SetFlagSprite(Sprite sprite)
        {
            flagSprite.sprite = sprite;
        }
        /// <summary>
        /// 设置残影具体贴图
        /// </summary>
        /// <param name="sprite"></param>
        public void SetSoldierShadowSprite(Sprite sprite)
        {
            foreach (Transform t in soldiers)
            {

                UnitData p = GameDataManager.instance.GetEntityDataById(finalSoldierId/*parentFormatCard.parentParameter.soldierId*/);
                t.GetComponent<SpriteRenderer>().sprite = sprite;
                t.transform.localScale = new Vector3(p.modleSize, p.modleSize, 1);
            }
        }
        /// <summary>
        /// 生成具体的士兵
        /// </summary>
        /// <param name="destoryShadow">是否摧毁场上的虚影</param>
        public void Generate(bool destoryShadow)
        {
            //SoliderGroup newGroup = flagSprite.gameObject.AddComponent<SoliderGroup>();
            //newGroup = Instantiate(this);
            //Debug.Log(newGroup.gameObject.name);
            //SoliderGroup NG = Instantiate(this.gameObject, this.transform.parent).GetComponent<SoliderGroup>();
            //parentFormatCard.SetConnectGroup(NG);
            //根据坐标和id生成实体  
            for (int i = 0; i < soldiers.Length; i++)
            {

                Entity e = BattleManager.instance.GenerateOneEntity(camp, parentFormatCard != null ? parentFormatCard.parentParameter.soldierId : finalSoldierId, soldiers[i].position);
                if (!e)
                {
                    Debug.Log("实体没有正确生成！（也许是生成的id出错或其他问题）");
                    foreach (Transform t in soldiers)
                        Destroy(t.gameObject);
                    Destroy(flagSprite.gameObject);
                    return;
                }
                e.transform.localPosition = new Vector3(e.transform.localPosition.x, e.transform.localPosition.y, 0);
                e.SetParentSoldierGroup(this);
                createdSoldiers.Add(e);

            }

            //生成之后摧毁虚影
            foreach (Transform t in soldiers)
            {
                //if (destoryShadow)
                Destroy(t.gameObject);
            }
            //跟随
            //gameObject.AddComponent<FaceToCamera>();
            SetFlagFollowSoldier();
            //设置具体士兵和队长的相对位置
            SetUnitOffset();
            //transform.SetParent(createdSoldiers[0].transform);
            //BattleManager.instance.soliderFormatGroups.Remove(this);
            //BattleManager.instance.soliderFormatGroups.Add(NG);

        }
        //当组内士兵死亡时调用的方法
        public void OnSoldierDie(Entity diedSoldier)
        {
            flagSprite.GetComponent<Animator>().Play("HurtEffect");
            createdSoldiers.Remove(diedSoldier);
            //士兵全部死亡时摧毁相关对象
            if (createdSoldiers.Count == 0)
            {
                Destroy(flagSprite.gameObject);
                Destroy(gameObject);
            }
            if (diedSoldier == flagFollowingSoldier)
            {
                //重新设置旗帜跟随
                SetFlagFollowSoldier();
            }
        }
        //设置旗帜跟随的士兵
        void SetFlagFollowSoldier()
        {
            if (createdSoldiers.Count == 0)
                return;
            flagSprite.gameObject.transform.SetParent(createdSoldiers[0].transform);
            flagFollowingSoldier = createdSoldiers[0];
            flagSprite.gameObject.transform.localPosition = new Vector3(0, 8 / createdSoldiers[0].transform.localScale.x, flagSprite.transform.localPosition.z);
        }
        /// <summary>
        /// 设置各个对象的相对阵型位置
        /// </summary>
        public void SetUnitOffset()
        {
            if (unitOffsetsToCaptain.Length != 0)
                for (int i = 0; i < createdSoldiers.Count; i++)
                {
                    Debug.Log(unitOffsetsToCaptain.Length);
                    if (unitOffsetsToCaptain[i] != null)
                        createdSoldiers[i].OffsetToCaptain = unitOffsetsToCaptain[i];
                }
        }
        /// <summary>
        /// 遍历所有生成了的士兵，让其处于正在回到相对位置的状态之中（原先有给队长上减速buff，但是后面改成了队长在原地等待其他士兵汇合
        /// </summary>
        public void SetUnitBackingToRelativePos()
        {
            //BuffManager.instance.EntityAddBuff(createdSoldiers[0], "2000001");
            for (int i = 0; i < createdSoldiers.Count; i++)
            {
                createdSoldiers[i].SetBackingToRelativePos(true);

            }
            settledUnitAmount = 0;
            isReorganizing = true;
        }
        /// <summary>
        /// 检测士兵的回到相对位置是否完成
        /// </summary>
        public void CheckUnitBackingToRelativePos()
        {
            if (isReorganizing)
            {
                //遍历所有生成了的士兵，如果其位置和阵型中的相对位置非常接近，则视为它完成了自己的重整阵型，这时这个士兵在原地待命，完成重整的士兵数量+1
                //当完成重整的士兵数量等于存活的士兵总数量时，视为整个队伍完成重整，队伍再一起移动
                foreach (Entity e in createdSoldiers)
                {
                    if (Vector3.Distance(e.transform.position, e.parentSoldierGroup.createdSoldiers[0].transform.position - e.OffsetToCaptain) < 0.1f)
                    {
                        if (e.GetBackingToRelativePos() == true)
                        {

                            //此时已经回到相对位置，在所有士兵到齐之前原地待命
                            //if (e != createdSoldiers[0])
                            //{
                            e.SetSpeedBuff(-100);
                            //}

                            //完成重整的士兵数量+1
                            settledUnitAmount++;

                            //Debug.Log("数量增加！当前：" + settledUnitAmount + "目标：" + createdSoldiers.Count);
                        }
                        //这名士兵判定为完成了自己的重整阵型
                        e.SetBackingToRelativePos(false);

                        //Debug.Log("回到了相对位置" + name);


                    }
                    //当所有士兵到齐，解除士兵的原地待命
                    if (settledUnitAmount == createdSoldiers.Count)
                    {
                        foreach (Entity e2 in createdSoldiers)
                        {
                            //if (e2 != createdSoldiers[0])
                            //{
                            Debug.Log("解除待命：" + e2.name);
                            e2.SetSpeedBuff(100);
                            //}
                        }
                        //BuffManager.instance.EntityRemoveBuff(createdSoldiers[0], "2000001");
                        //Debug.Log("解除了队长的减速buff");

                        //初始化相关变量
                        isReorganizing = false;
                        settledUnitAmount = 0;
                    }
                }
            }
        }

        public void SetParentFormatCard(FormatCard card)
        {
            this.parentFormatCard = card;
        }
        public FormatCard GetParentFormatCard()
        {
            return parentFormatCard;
        }
    }
}