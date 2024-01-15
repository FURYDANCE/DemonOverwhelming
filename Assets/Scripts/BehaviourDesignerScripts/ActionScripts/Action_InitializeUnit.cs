using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;

namespace DemonOverwhelming
{

    /// <summary>
    ///  ��λͨ�õ���Ϊ����ʼ���ű������ڸ�ֵ������Ϊ������ı���
    /// </summary>
    public class Action_InitializeUnit : Action
    {
        [Header("����λ")]
        public SharedEntity thisEntity;
        [Header("����ǰҡʱ��")]
        public SharedFloat attackStartWaitTime;
        [Header("������ҡʱ��")]
        public SharedFloat attackAfterWaitTime;



        bool success;
        public override void OnStart()
        {
            Entity e = GetComponent<Entity>();
            thisEntity.SetValue(e);
            //��ȡangent������������������ɣ�
            e.angent = e.GetComponent<NavMeshAgent>() ?? e.gameObject.AddComponent<NavMeshAgent>();
            e.angent.updateRotation = false;
            attackStartWaitTime.SetValue(e.parameter.character.attackTime);
            attackAfterWaitTime.SetValue(e.parameter.character.attackWaitTime);
            success = true;
        }

        public override TaskStatus OnUpdate()
        {
            if (success)
                return TaskStatus.Success;
            else
            {
                Debug.LogError("��λ��ʼ��ʧ��");
                return TaskStatus.Failure;
            }
        }
    }
}