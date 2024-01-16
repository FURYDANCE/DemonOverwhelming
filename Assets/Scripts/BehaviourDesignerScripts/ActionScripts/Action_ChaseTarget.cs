using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
namespace DemonOverwhelming
{
    /// <summary>
    /// 追逐目标的task
    /// navmesh中只有设置目标v3的方法，没有设置目标transform的方法（真没有还是我没找到？）
    /// 所以要追逐目标的话，需要在追逐过程中每一帧都将目标设置为目标的坐标
    /// 而脱离追逐状态则判断和目标之间的距离是否小于攻击距离
    /// 以及，应该每帧检测范围内的所有敌人，然后选择最近的敌人作为目标
    /// 当范围内没有敌人时，也一样脱离该task
    /// </summary>
    public class Action_ChaseTarget : Action
    {
        public SharedEntity thisEntity;
        public SharedTransform moveTarget;
        public SharedVector3 moveTargetVector;
        Entity entity;
        public override void OnStart()
        {
            entity = thisEntity.Value;
        }
        public override TaskStatus OnUpdate()
        {
            //当范围内不再有敌人时，返回failed（不进行接下来的攻击判定而是返回移动行为）
            if (entity.enemiesInCheckArea.Count == 0)
            {
                return TaskStatus.Failure;
            }
            //当范围内有敌人时，遍历所有敌人得到最近敌人的坐标，将移动目标设置为其目标
            else
            {
                Transform nearestTransform = null;
                float nearestDistance = float.MaxValue;
                foreach (Entity t in entity.enemiesInCheckArea)
                {
                    if (t)
                    {
                        float distance = Vector3.Distance(transform.position, t.transform.position);
                        if (distance < nearestDistance)
                        {
                            nearestTransform = t.transform;
                            nearestDistance = distance;
                        }
                    }
                }
                //当单位距离目标地点的距离小于单位的攻击距离时，返回success（进行接下来的攻击判定）
                if (nearestTransform != null && Vector3.Distance(transform.position, nearestTransform.position) < entity.parameter.character.attackDistance)
                {
                    //设置sharedTransform，用于在攻击task里面读取到
                    moveTarget.SetValue(nearestTransform);
                    return TaskStatus.Success;
                }
                if (nearestTransform != null)
                {
                    entity.SetMoveTarget(nearestTransform.position);
                    moveTargetVector.SetValue(nearestTransform.position);
                }
                //其他的情况，视为正在追逐目标，返回running
                return TaskStatus.Running;
            }

        }
    }
}