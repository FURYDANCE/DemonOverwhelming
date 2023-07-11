using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ɫ����Ŀ���״̬
/// </summary>
public class CharacterAttackingState : CharacterBaseState
{
    public void OnEnter(CharacterStateManager manager)
    {
        Debug.Log("���빥��״̬");
        manager.isAttacking = true;
        //ִ�й���ģ��
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
