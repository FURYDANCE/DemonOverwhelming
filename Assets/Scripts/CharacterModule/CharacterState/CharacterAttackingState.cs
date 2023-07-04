using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色攻击目标的状态
/// </summary>
public class CharacterAttackingState : CharacterBaseState
{
    public void OnEnter(CharacterStateManager manager)
    {
        manager.isAttacking = true;
    }
    public void OnExit(CharacterStateManager manager)
    {
        manager.isAttacking = false;
    }
    public void OnUpdate(CharacterStateManager manager)
    {
        //if (Vector3.Distance(manager.transform.position, manager.attackTarget.transform.position) > manager.entity.parameter.character.attackDistance + 0.2f)
        //{
        //    manager.attackTarget = null;
        //    manager.ChangeState(new CharacterWalkingState());
        //}
        //manager.ChaseingStateCheck();
        if (manager.attackTarget == null)
        {
            manager.ChangeState(new CharacterWalkingState());
            return;
        }
        manager.Attack(manager.attackTarget.GetComponent<Entity>());
    }
}
