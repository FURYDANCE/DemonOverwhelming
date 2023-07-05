using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���е��˱���⵽ʱ������������ƶ�
/// </summary>
public class ICharaChase_TryCloser : MonoBehaviour, ICharacterChase
{
    Entity entity;
    CharacterStateManager manager;
    float speed;
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
        transform.position = Vector3.MoveTowards(transform.position, manager.attackTarget.transform.position, speed * Time.deltaTime);

    }


    void Start()
    {
        entity = GetComponent<Entity>();
        manager = GetComponent<CharacterStateManager>();
        speed = entity.parameter.character.moveSpeed;
    }

}
