using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

namespace DemonOverwhelming
{
    /// <summary>
    /// 设置直线的移动目标的task
    /// 当全局变量的移动目标（transform）不为空时，将单位的移动目标设置为其全局变量的坐标
    /// 否则，仅根据阵营决定设置左方或右方的坐标为目标
    /// </summary>
    public class Action_SetUnitMoveTarget_Direct : Action
    {
        public SharedEntity sharedEntity;
        public SharedTransform sharedTransform;
        Entity entity;
        public override void OnStart()
        {
            entity = sharedEntity.Value;

            if (sharedTransform.Value != null)
            {
                entity.SetMoveTarget(sharedTransform.Value.position);
            }
            else
            {
                if (entity.camp == Camp.demon)
                {
                    entity.SetMoveTarget(new Vector3(SceneObjectsManager.instance.cameraBound_Right.position.x, entity.transform.position.y, entity.transform.position.z));
                }
                else
                    entity.SetMoveTarget(new Vector3(SceneObjectsManager.instance.cameraBound_Left.position.x, entity.transform.position.y, entity.transform.position.z));

            }
        }
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }
}