using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
namespace DemonOverwhelming
{
    /// <summary>
    /// ��ħ�����ļ�����Ϊ
    /// </summary>
    public class Action_Skill_1 : Action
    {
        public SharedEntity thisEntity;
        Entity entity;
        public override void OnStart()
        {
            //��ʼ��
            entity = thisEntity.Value;
            entity.GetComponent<CharacterStateManager>().ResetMovingDir();
            Skill();
            entity.skills[0].SkillUesd();
        }
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
        /// <summary>
        /// ���ܷ���
        /// </summary>
        public void Skill()
        {
            Debug.Log("ִ���˼���");

            Collider[] colliders = Physics.OverlapSphere(entity.transform.position + (entity.transform.rotation.y == 0 ? 3 * Vector3.right : 3 * Vector3.left), 3);
            DamageData damage = new DamageData();
            //�����˺�
            damage.SetDamageData(15, 0, 0);
            //������Ч
            VfxManager.instance.CreateVfx(VfxManager.instance.GetVfxByIdOrName("7").vfx, entity.transform.position + (entity.transform.rotation.y == 0 ? 3 * Vector3.right : 3 * Vector3.left) + 5 * Vector3.up, new Vector3(10, 10, 10), 3);
            foreach (Collider c in colliders)
            {
                //�Լ�ⷶΧ�ڵ�ÿ����������˺���ÿ�λظ��˺���10%��Ѫ��
                Entity e = c.GetComponent<Entity>();
                if (e && e.camp != entity.camp)
                {
                    entity.Heal(0.1f * BattleManager.instance.CreateDamage(entity, damage, e));
                }
            }


        }
    }
}