using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色生成时进入的初始状态（用于播放生成时的动画）
/// </summary>
public class CharacterGeneratingState : CharacterBaseState
{
    float timer;
    public void OnEnter(CharacterStateManager manager)
    {
        //Debug.Log("我生成了");
        manager.entity.PlayAnimation_Idle();
        timer = 1;
        manager.isGenerating = true;
    }

    public void OnExit(CharacterStateManager manager)
    {
        //Debug.Log("退出生成状态");
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
