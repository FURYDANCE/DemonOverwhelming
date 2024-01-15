using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
namespace DemonOverwhelming
{
    /// <summary>
    /// ͨ�������������ƣ����ŵ�λ�Ĺ��������Ľű�
    /// </summary>
    public class Action_PlayAnimation : Action
    {
        [Header("Ҫ���ŵĶ�������")]
        public string animationName;
        [Header("�Ƿ�ѭ������")]
        public bool loop;
        public SharedEntity thisEntity;
        Entity entity;
        bool played;
        public override void OnStart()
        {

            entity = thisEntity.Value;
            entity.PlayAnimation(animationName, loop);
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }
}