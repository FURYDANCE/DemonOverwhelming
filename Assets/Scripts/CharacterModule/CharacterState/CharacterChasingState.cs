using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChasingState : CharacterBaseState
{

    float timer;
    public void OnEnter(CharacterStateManager manager)
    {
        manager.isChaseing = true;
        Debug.Log("����׷��״̬");
    }

    public void OnExit(CharacterStateManager manager)
    {
        manager.isChaseing = false;
        Debug.Log("�뿪׷��״̬");
    }

    public void OnUpdate(CharacterStateManager manager)
    {
        manager.chaseScript.Chase();
        //�����Χ�ڲ��ڼ�⵽�з��������»ع鵽����״̬
        //�������ҿ��ƽű������¿��ư���ʱҲ�ص�����״̬
        if (manager.enemySelected.Count == 0 || (manager.moveScript.ToString().Contains("Input") && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D))))
        {
            manager.SetAttackTarget(null);
            manager.ChangeState(new CharacterWalkingState());
            return;
        }
        //����С�ڹ�������ʱ���빥��״̬
        if (manager.attackTarget && (Vector3.Distance(manager.transform.position, manager.attackTarget.transform.position) <= manager.entity.parameter.character.attackDistance))
        {
            manager.ChangeState(new CharacterAttackingState());
        }

    }


}
