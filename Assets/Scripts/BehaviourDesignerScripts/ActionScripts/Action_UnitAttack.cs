using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
namespace DemonOverwhelming
{
    /// <summary>
    /// 单位的攻击行为
    /// 只是一对一的单次攻击，给出攻击目标即可，会直接调用伤害方法伤害对方，然后返回success
    /// </summary>
    public class Action_UnitAttack : Action
    {
        public int attackIndex;
        public SharedEntity thisEntity;
        public SharedTransform attackTargetTransform;
        Entity entity;
        public override void OnStart()
        {
            entity = thisEntity.Value;
            if (attackTargetTransform.Value != null)
                BattleManager.instance.CreateAttack(entity, entity.parameter.character.attackIds[attackIndex], attackTargetTransform.Value.GetComponent<Entity>());
            attackTargetTransform.SetValue(null);
        }
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }

    }
}