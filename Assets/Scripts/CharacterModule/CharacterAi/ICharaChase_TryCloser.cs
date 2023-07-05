using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 在有敌人被检测到时，尝试向敌人移动
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
        transform.position = Vector3.MoveTowards(transform.position, manager.attackTarget.transform.position, speed * Time.deltaTime);

    }


    void Start()
    {
        entity = GetComponent<Entity>();
        manager = GetComponent<CharacterStateManager>();
        speed = entity.parameter.character.moveSpeed;
    }

}
