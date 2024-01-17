using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
namespace DemonOverwhelming
{
    /// <summary>
    /// ׷��Ŀ���task
    /// navmesh��ֻ������Ŀ��v3�ķ�����û������Ŀ��transform�ķ�������û�л�����û�ҵ�����
    /// ����Ҫ׷��Ŀ��Ļ�����Ҫ��׷�������ÿһ֡����Ŀ������ΪĿ�������
    /// ������׷��״̬���жϺ�Ŀ��֮��ľ����Ƿ�С�ڹ�������
    /// �Լ���Ӧ��ÿ֡��ⷶΧ�ڵ����е��ˣ�Ȼ��ѡ������ĵ�����ΪĿ��
    /// ����Χ��û�е���ʱ��Ҳһ�������task
    /// </summary>
    public class Action_ChaseTarget : Action
    {
        public SharedEntity thisEntity;
        public SharedTransform moveTarget;
        public SharedVector3 moveTargetVector;
        Entity entity;
        public override void OnStart()
        {
            entity = thisEntity.Value;
        }
        public override TaskStatus OnUpdate()
        {
            //����Χ�ڲ����е���ʱ������failed�������н������Ĺ����ж����Ƿ����ƶ���Ϊ��
            if (entity.enemiesInCheckArea.Count == 0)
            {
                return TaskStatus.Failure;
            }
            //����Χ���е���ʱ���������е��˵õ�������˵����꣬���ƶ�Ŀ������Ϊ��Ŀ��
            else
            {
                Transform nearestTransform = null;
                float nearestDistance = float.MaxValue;
                foreach (Entity t in entity.enemiesInCheckArea)
                {
                    if (t)
                    {
                        float distance = Vector3.Distance(transform.position, t.transform.position);
                        if (distance < nearestDistance)
                        {
                            nearestTransform = t.transform;
                            nearestDistance = distance;
                        }
                    }
                }
                //����λ����Ŀ��ص�ľ���С�ڵ�λ�Ĺ�������ʱ������success�����н������Ĺ����ж���
                if (nearestTransform != null && Vector3.Distance(transform.position, nearestTransform.position) < entity.parameter.character.attackDistance)
                {
                    //����sharedTransform�������ڹ���task�����ȡ��
                    moveTarget.SetValue(nearestTransform);
                    return TaskStatus.Success;
                }
                if (nearestTransform != null)
                {
                    entity.SetMoveTarget(nearestTransform.position);
                    moveTargetVector.SetValue(nearestTransform.position);
                }
                //�������������Ϊ����׷��Ŀ�꣬����running
                return TaskStatus.Running;
            }

        }
    }
}