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
        public SharedEntity thisEntity;
        public SharedTransform attackTargetTransform;
        Entity entity;
        public override void OnStart()
        {
            entity = thisEntity.Value;
            //���ﻹ���ȵ�������ǰд���˺�����������֮��������ع�һ�£�
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