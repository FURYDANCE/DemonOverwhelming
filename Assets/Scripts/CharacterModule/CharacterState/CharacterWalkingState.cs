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
        manager.GetInterfaceScript();
    }

    public void OnExit(CharacterStateManager manager)
    {
        manager.isWalking = false;
    }
    public void OnUpdate(CharacterStateManager manager)
    {
      
        //������Ŀ�겻Ϊ��ʱ����׷��״̬
        if (manager.attackTarget != null)
        {
            manager.ChangeState(new CharacterChasingState());
            return;
        }
        //ִ����Ϊģ��
        if (manager.moveScript != null)
        {
            manager.moveScript.Moving();
        }
        
    }


}
