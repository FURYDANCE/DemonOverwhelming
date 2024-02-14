using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
namespace DemonOverwhelming
{
    /// <summary>
    /// �ڹ���ǰ��Ҫ����س�ʼ��action
    /// </summary>
    public class Action_BeforeAttack : Action
    {
        public SharedTransform flipTarget;
        public SharedVector3 flipTargetV3;
        public SharedEntity thisEntity;
        Entity entity;
        public override void OnStart()
        {
            entity = thisEntity.Value;
            flipTargetV3.Value = flipTarget.Value.position;
        }

    }
}