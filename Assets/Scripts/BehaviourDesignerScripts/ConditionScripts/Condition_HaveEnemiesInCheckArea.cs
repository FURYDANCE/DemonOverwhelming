using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
namespace DemonOverwhelming
{
    /// <summary>
    /// 条件task：当本单位的索敌范围内没有敌人时返回running，否则返回success
    /// </summary>
    public class Condition_HaveEnemiesInCheckArea : Conditional
    {
        public SharedEntity thisEntity;
        Entity entity;
        public override void OnStart()
        {
            entity = thisEntity.Value;
        }
        public override TaskStatus OnUpdate()
        {
            if (entity.targetableEntities.Count == 0)
                return TaskStatus.Running;
            else
                return TaskStatus.Success;
        }
    }
}