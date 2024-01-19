using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
namespace DemonOverwhelming
{
    /// <summary>
    /// 检测附近的敌人的task
    /// 检测附近范围内的敌人是否在
    /// 若检测到敌人，则将全局变量的移动目标设置为下标为0的敌人，然后返回success，否则返回running
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