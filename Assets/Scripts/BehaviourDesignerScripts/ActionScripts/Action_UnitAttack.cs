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
        public SharedEntity thisEntity;
        public SharedTransform attackTargetTransform;
        Entity entity;
        public override void OnStart()
        {
            entity = thisEntity.Value;
            //这里还是先调用了以前写的伤害方法，但是之后或许还会重构一下？
            Debug.Log(thisEntity.Value + attackTargetTransform.Value.name + entity.parameter.character.missileId);
            Debug.Log( entity.parameter.character.missileId);
            Debug.Log(attackTargetTransform.Value.name );
            BattleManager.instance.GenerateOneMissle(entity, transform.position, entity.parameter.character.missileId, attackTargetTransform.Value.gameObject.GetComponent<Entity>());
            attackTargetTransform.SetValue(null);
        }
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }

    }
}