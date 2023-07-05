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
        Debug.Log("进入攻击状态");
        manager.isAttacking = true;
        //执行攻击模块
        manager.attackScript.Attack();
    }
    public void OnExit(CharacterStateManager manager)
    {
        manager.isAttacking = false;
    }
    public void OnUpdate(CharacterStateManager manager)
    {
       
    }
}
