using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{
    /// <summary>
    /// ���е��˱���⵽ʱ������������ƶ�
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
            //��׷��Ŀ�궪ʧʱ��������״̬
            if (!manager.attackTarget)
            {
                manager.ChangeState(new CharacterWalkingState());
                return;
            }
            //��׷����ͬʱ��Ȼ���׷��Ŀ�꣨�л�Ŀ�꣩
            manager.CheckEnemy();
            manager.SetAttackTarget(manager.enemyCheckScript.EnemyCheck());
            //��Ŀ��׷��
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