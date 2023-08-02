using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkillingState : CharacterBaseState
{
    public void OnEnter(CharacterStateManager manager)
    {
        Debug.Log("进入技能释放状态");
    }

    public void OnExit(CharacterStateManager manager)
    {
        
    }

    public void OnUpdate(CharacterStateManager manager)
    {
        
    }
}
