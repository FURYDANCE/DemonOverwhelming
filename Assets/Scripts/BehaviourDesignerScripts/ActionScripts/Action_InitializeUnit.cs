using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;

namespace DemonOverwhelming
{

    /// <summary>
    ///  单位通用的行为树初始化脚本，用于赋值各项行为树所需的变量
    /// </summary>
    public class Action_InitializeUnit : Action
    {
        [Header("本单位")]
        public SharedEntity thisEntity;
        [Header("攻击前摇时间")]
        public SharedFloat attackStartWaitTime;
        [Header("攻击后摇时间")]
        public SharedFloat attackAfterWaitTime;



        bool success;
        public override void OnStart()
        {
            Entity e = GetComponent<Entity>();
            thisEntity.SetValue(e);
            //获取angent组件（若不存在则生成）
            e.angent = e.GetComponent<NavMeshAgent>() ?? e.gameObject.AddComponent<NavMeshAgent>();
            e.angent.updateRotation = false;
            e.angent.speed = e.GetSpeed();
            attackStartWaitTime.SetValue(e.parameter.character.attackTime);
            attackAfterWaitTime.SetValue(e.parameter.character.attackWaitTime);
            e.FlipTo(e.camp == Camp.demon ? e.transform.position + Vector3.right : e.transform.position - Vector3.right);
            success = true;
        }

        public override TaskStatus OnUpdate()
        {
            if (success)
                return TaskStatus.Success;
            else
            {
                Debug.LogError("单位初始化失败");
                return TaskStatus.Failure;
            }
        }
    }
}