using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 直线移动脚本，当阵营为恶魔时向右直线移动，反之向左
/// </summary>
public class ICharaMove_Direct : MonoBehaviour, ICharacterMove
{
    Entity entity;
    float speed;
    Vector3 moveDir;
    CharacterStateManager manager;

    void Start()
    {
        entity = GetComponent<Entity>();
        speed = entity.parameter.character.moveSpeed;
        manager = GetComponent<CharacterStateManager>();
    }


    public void Moving()
    {
        if (entity.camp == Camp.demon)
            moveDir = Vector3.right.normalized;
        else
            moveDir = Vector3.left.normalized;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDir, speed * Time.deltaTime);

        //在直线移动的同时，检测周围的敌人，是否进入追击敌人的状态
        //在这里执行的原因：当玩家操控输入时，当范围内有敌人也不应该进入追击状态，而是在不按下任何行动按钮的时候并且范围内有敌人才进入追击状态，所以应该在具体的脚本中执行
        manager.CheckEnemy();
        manager.SetAttackTarget(manager.enemyCheckScript.EnemyCheck());

    }



}
