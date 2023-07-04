using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ɫ����ʱ����ĳ�ʼ״̬�����ڲ�������ʱ�Ķ�����
/// </summary>
public class CharacterGeneratingState : CharacterBaseState
{
    float timer;
    public void OnEnter(CharacterStateManager manager)
    {
        //Debug.Log("��������");
        manager.entity.PlayAnimation_Idle();
        timer = 1;
        manager.isGenerating = true;
    }

    public void OnExit(CharacterStateManager manager)
    {
        //Debug.Log("�˳�����״̬");
        manager.isGenerating = false;

    }

    public void OnUpdate(CharacterStateManager manager)
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            manager.ChangeState(new CharacterWalkingState());
        }
    }


}
