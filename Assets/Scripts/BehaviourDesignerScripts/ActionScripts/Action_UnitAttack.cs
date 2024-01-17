using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
namespace DemonOverwhelming
{
    /// <summary>
    /// ��λ�Ĺ�����Ϊ
    /// ֻ��һ��һ�ĵ��ι�������������Ŀ�꼴�ɣ���ֱ�ӵ����˺������˺��Է���Ȼ�󷵻�success
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