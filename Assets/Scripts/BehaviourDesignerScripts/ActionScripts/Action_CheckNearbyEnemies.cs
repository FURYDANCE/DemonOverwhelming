using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
namespace DemonOverwhelming
{
    /// <summary>
    /// ��⸽���ĵ��˵�task
    /// ��⸽����Χ�ڵĵ����Ƿ���
    /// ����⵽���ˣ���ȫ�ֱ������ƶ�Ŀ������Ϊ�±�Ϊ0�ĵ��ˣ�Ȼ�󷵻�success�����򷵻�running
    /// </summary>
    public class Action_CheckNearbyEnemies : Action
    {
        public SharedEntity sharedEntity;
        public SharedTransformList checkedTargets;
        float radius;
        Entity entity;
        public override void OnStart()
        {
            entity = sharedEntity.Value;
           
        }

        public override TaskStatus OnUpdate()
        {

            Collider[] colliders = Physics.OverlapSphere(transform.position, 10);
            for (int i = 0; i < colliders.Length; i++)
            {
                Collider c = colliders[i];
                
            }
          
            return TaskStatus.Running;
        }
    }
}