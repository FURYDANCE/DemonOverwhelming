using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{
    /// <summary>
    /// 重写的投射物类
    /// </summary>
    public class UnitMissile : MonoBehaviour
    {
        /// <summary>
        /// 该投射物的创造者
        /// </summary>
        public Entity creater;
        /// <summary>
        /// 该投射物的目标对象（因为目标也许不是实体，所以设置为Transform）
        /// </summary>
        public Transform attackTarget;
        /// <summary>
        /// 用于获取各项数据的ID
        /// </summary>
        public string missileId;
        /// <summary>
        /// 从投射物目标对象上获取的单位组件
        /// </summary>
        Entity attackTargetEntity;

        public UnitParameter_Missile parameter;

        /// <summary>
        /// 当前位移的目标位置
        /// </summary>
        Vector3 nowTarget;

        private void Start()
        {
            SetParameter(creater, missileId, attackTarget);
        }
        private void Update()
        {
            Translate();
            MoveEndCheck();
            if (nowTarget != Vector3.zero)
                RotationCaculate(nowTarget);
        }
        /// <summary>
        /// 位移方法
        /// </summary>
        public void Translate()
        {
            if(attackTarget==null)
            {
                Debug.Log("目标不存在");
                MissileDie();
            }
            //位移目标存在单位组件时朝着其中心位置移动，否则直接朝着位移目标的坐标移动
            if (attackTargetEntity != null)
                nowTarget = attackTargetEntity.GetUnitCenter().position;
            else
                nowTarget = attackTarget.position;
            transform.position = Vector3.MoveTowards(transform.position, nowTarget, parameter.speed * Time.deltaTime);
        }
        /// <summary>
        /// 结束移动的检测
        /// </summary>
        public void MoveEndCheck()
        {
            if (Vector3.Distance(transform.position, nowTarget) < 0.25f)
            {
                Debug.Log("判定投射物移动到终点");
                if (attackTargetEntity)
                {
                    Debug.Log("伤害目标");
                    BattleManager.instance.CreateDamage(creater, parameter.damageData, attackTargetEntity);

                    MissileDie();
                }
            }
        }
        //调整角度
        public void RotationCaculate(Vector3 target)
        {
            if (parameter.moveType == MissileMoveType.parabola)
                return;
            Vector3 v = target - transform.position; //首先获得目标方向
            v.z = 0; //这里一定要将z设置为0
            float angle = Vector3.SignedAngle(Vector3.up, v, Vector3.forward); //得到围绕z轴旋转的角度
            Quaternion rotation = Quaternion.Euler(0, 0, angle); //将欧拉角转换为四元数
            transform.rotation = rotation;
        }
        public void SetParameter(Entity creater, string id, Transform moveTarget)
        {
            this.creater = creater;
            this.attackTarget = moveTarget;
            this.attackTargetEntity = moveTarget.GetComponent<Entity>();
            parameter = GameDataManager.instance.GetMissileDataById(id);
            parameter.damageData = GameDataManager.instance.GetDamageDataById(parameter.damageDataId);
            foreach (BuffInformation buff in creater.buffs)
            {
                buff.buff.OnAddbuff_Missile(this, buff.buffLevel);
            }
        }
        public void MissileDie()
        {
            Debug.Log("投射物死亡");
            Destroy(gameObject);
            return;
        }
    }
}