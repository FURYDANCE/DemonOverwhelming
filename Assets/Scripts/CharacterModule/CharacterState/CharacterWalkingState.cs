using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ɫ�н�״̬
/// </summary>
public class CharacterWalkingState : CharacterBaseState
{
    public void OnEnter(CharacterStateManager manager)
    {
        manager.entity.PlayAniamtion_Walk();
        //Debug.Log("�����ж�״̬");
        manager.isWalking = true;
        manager.intoWalking = true;
    }

    public void OnExit(CharacterStateManager manager)
    {
        manager.isWalking = false;

    }

    public void OnUpdate(CharacterStateManager manager)
    {
        if (manager.moveTarget == null)
            manager.intoWalking = true;
        manager.AttackStateCheck();
        manager.Translate();
    }


}
