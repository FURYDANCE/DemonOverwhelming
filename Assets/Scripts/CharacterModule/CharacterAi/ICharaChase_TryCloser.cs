using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{
    /// <summary>
    /// 在有敌人被检测到时，尝试向敌人移动
    /// </summary>
    public class ICharaChase_TryCloser : MonoBehaviour, ICharacterChase
    {
        Entity entity;
        CharacterStateManager manager;
        float speed;
        float timer;
        public void Chase()
        {
            if (!manager)
            {
                manager = GetComponent<CharacterStateManager>();
            }
            //当追击目标丢失时返回行走状态
            if (!manager.attackTarget)
            {
                manager.ChangeState(new CharacterWalkingState());
                return;
            }
            //在追击的同时仍然检测追击目标（切换目标）
            manager.CheckEnemy();
            manager.SetAttackTarget(manager.enemyCheckScript.EnemyCheck());
            //向目标追击
            //if (Vector3.Distance(transform.position, manager.attackTarget.transform.position) > manager.entity.parameter.character.attackDistance)
            if (timer < 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, manager.attackTarget.transform.position, speed * Time.deltaTime);

            }
            timer -= Time.deltaTime;
        }

        public void OnStartChase()
        {
            timer = 0.5f;
        }

        void Start()
        {
            entity = GetComponent<Entity>();
            manager = GetComponent<CharacterStateManager>();
            speed = entity.parameter.character.moveSpeed;
            timer = 0.5f;
        }

    }
}