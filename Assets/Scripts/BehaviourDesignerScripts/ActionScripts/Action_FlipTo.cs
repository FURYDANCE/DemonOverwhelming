using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
namespace DemonOverwhelming
{
    /// <summary>
    /// 控制单位转向的action
    /// </summary>
    public class Action_FlipTo : Action
    {
        public SharedEntity thisEntity;
        Entity entity;
        public SharedVector3 flipTarget;

        public override void OnStart()
        {
            entity = thisEntity.Value;
        }
        public override TaskStatus OnUpdate()
        {
            if (flipTarget.Value != Vector3.zero)
                entity.FlipTo(flipTarget.Value);
            return TaskStatus.Running;
        }
    }
}