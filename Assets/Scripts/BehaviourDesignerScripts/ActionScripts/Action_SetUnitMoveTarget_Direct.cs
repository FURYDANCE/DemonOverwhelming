using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

namespace DemonOverwhelming
{
    /// <summary>
    /// ����ֱ�ߵ��ƶ�Ŀ���task
    /// ��ȫ�ֱ������ƶ�Ŀ�꣨transform����Ϊ��ʱ������λ���ƶ�Ŀ������Ϊ��ȫ�ֱ���������
    /// ���򣬽�������Ӫ���������󷽻��ҷ�������ΪĿ��
    /// </summary>
    public class Action_SetUnitMoveTarget_Direct : Action
    {
        public SharedEntity sharedEntity;
        public SharedTransform sharedTransform;
        public SharedVector3 moveTatgetVector;
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
                    Vector3 v = new Vector3(SceneObjectsManager.instance.cameraBound_Right.position.x, entity.transform.position.y, entity.transform.position.z);
                    entity.SetMoveTarget(v);
                    moveTatgetVector.Value = v;
                }
                else
                {
                    Vector3 v = new Vector3(SceneObjectsManager.instance.cameraBound_Left.position.x, entity.transform.position.y, entity.transform.position.z);
                    entity.SetMoveTarget(v);
                    moveTatgetVector.Value = v;
                }
            }
        }
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }
}