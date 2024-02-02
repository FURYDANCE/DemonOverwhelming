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

        [Header("Ĭ��״̬�£����������ʱ����false��������ʱ����true����ѡ���֮�󷵻��෴")]
        public bool anti;
        [Header("��ⰴ�¼���")]
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
            //��⼼�ܼ�������
            if (checkSkill)
            {
                if (stateManager.GetSkillInput())
                {
                    return TaskStatus.Success;
                }
                else
                    return TaskStatus.Failure;
            }


            //����ƶ���������
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