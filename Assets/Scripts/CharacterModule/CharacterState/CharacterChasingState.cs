using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChasingState : CharacterBaseState
{

    float timer;
    public void OnEnter(CharacterStateManager manager)
    {
        manager.isChaseing = true;
        Debug.Log("进入追击状态");
    }

    public void OnExit(CharacterStateManager manager)
    {
        manager.isChaseing = false;
        Debug.Log("离开追击状态");
    }

    public void OnUpdate(CharacterStateManager manager)
    {
        manager.chaseScript.Chase();
        //如果范围内不在检测到敌方对象，重新回归到行走状态
        //如果有玩家控制脚本，则按下控制按键时也回到行走状态
        if (manager.enemySelected.Count == 0 || (manager.moveScript.ToString().Contains("Input") && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D))))
        {
            manager.SetAttackTarget(null);
            manager.ChangeState(new CharacterWalkingState());
            return;
        }
        //距离小于攻击距离时进入攻击状态
        if (manager.attackTarget && (Vector3.Distance(manager.transform.position, manager.attackTarget.transform.position) <= manager.entity.parameter.character.attackDistance))
        {
            manager.ChangeState(new CharacterAttackingState());
        }

    }


}
