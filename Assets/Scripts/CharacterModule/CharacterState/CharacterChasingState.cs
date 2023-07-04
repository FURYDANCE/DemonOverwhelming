using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChasingState : CharacterBaseState
{

    float timer;
    public void OnEnter(CharacterStateManager manager)
    {
        manager.isChaseing = true;
        //manager.SetMoveTarget(manager.attackTarget);
    }

    public void OnExit(CharacterStateManager manager)
    {
        manager.isChaseing = false;

    }

    public void OnUpdate(CharacterStateManager manager)
    {
        manager.AttackStateCheck();

        timer -= Time.deltaTime;
        Debug.Log("×·»÷Ê±¼ä»¹Ê££º" + timer);
        if (timer <= 0)
        {
            manager.attackTarget = null;
            manager.ChangeState(new CharacterWalkingState());

        }
    }


}
