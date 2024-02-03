using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
namespace DemonOverwhelming
{
    /// <summary>
    /// 恶魔领主的技能行为
    /// </summary>
    public class Action_Skill_1 : Action
    {
        public SharedEntity thisEntity;
        Entity entity;
        public override void OnStart()
        {
            //初始化
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
        /// 技能方法
        /// </summary>
        public void Skill()
        {
            Debug.Log("执行了技能");

            Collider[] colliders = Physics.OverlapSphere(entity.transform.position + (entity.transform.rotation.y == 0 ? 3 * Vector3.right : 3 * Vector3.left), 3);
            DamageData damage = new DamageData();
            //设置伤害
            damage.SetDamageData(15, 0, 0);
            //生成特效
            VfxManager.instance.CreateVfx(VfxManager.instance.GetVfxByIdOrName("7").vfx, entity.transform.position + (entity.transform.rotation.y == 0 ? 3 * Vector3.right : 3 * Vector3.left) + 5 * Vector3.up, new Vector3(10, 10, 10), 3);
            foreach (Collider c in colliders)
            {
                //对监测范围内的每个敌人造成伤害，每次回复伤害的10%的血量
                Entity e = c.GetComponent<Entity>();
                if (e && e.camp != entity.camp)
                {
                    entity.Heal(0.1f * BattleManager.instance.CreateDamage(entity, damage, e));
                }
            }


        }
    }
}