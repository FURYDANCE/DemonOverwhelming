using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色行进状态
/// </summary>
public class CharacterWalkingState : CharacterBaseState
{
    public void OnEnter(CharacterStateManager manager)
    {
        manager.entity.PlayAniamtion_Walk();
        //Debug.Log("进入行动状态");
        manager.isWalking = true;
        manager.intoWalking = true;
        manager.GetInterfaceScript();
    }

    public void OnExit(CharacterStateManager manager)
    {
        manager.isWalking = false;
    }
    public void OnUpdate(CharacterStateManager manager)
    {
      
        //当攻击目标不为空时进入追击状态
        if (manager.attackTarget != null)
        {
            manager.ChangeState(new CharacterChasingState());
            return;
        }
        //执行行为模块
        if (manager.moveScript != null)
        {
            manager.moveScript.Moving();
        }
        
    }


}
