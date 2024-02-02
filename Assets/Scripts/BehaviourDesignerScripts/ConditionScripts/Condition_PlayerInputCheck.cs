using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine.InputSystem;
namespace DemonOverwhelming
{
    public class Condition_PlayerInputCheck : Conditional
    {

        [Header("默认状态下，有玩家输入时返回false，无输入时返回true，勾选这个之后返回相反")]
        public bool anti;
        [Header("检测按下技能")]
        public bool checkSkill;
        PlayerInput input;
        CharacterStateManager stateManager;
        public override void OnStart()
        {
           
            input = GetComponent<PlayerInput>();
            stateManager = GetComponent<CharacterStateManager>();
        }

        public override TaskStatus OnUpdate()
        {
            //检测技能键入的情况
            if (checkSkill)
            {
                if (stateManager.GetSkillInput())
                {
                    return TaskStatus.Success;
                }
                else
                    return TaskStatus.Failure;
            }


            //检测移动键入的情况
            if (stateManager.CheckIsInputMoving())
            {
                if (!anti)
                    return TaskStatus.Failure;
                else
                    return TaskStatus.Success;
            }
            else
            {
                if (!anti)
                    return TaskStatus.Success;
                else
                    return TaskStatus.Failure;

            }
        }
    }
}