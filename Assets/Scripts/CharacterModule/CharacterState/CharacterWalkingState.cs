using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{

    /// <summary>
    /// 角色行进状态
    /// </summary>
    public class CharacterWalkingState : CharacterBaseState
    {
        public void OnEnter(CharacterStateManager manager)
        {
            //Debug.Log("进入行动状态");
            manager.isWalking = true;
            manager.intoWalking = true;

            if (manager.entity.isHero == false)
                manager.entity.PlayAniamtion_Walk();

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
            
        }


    }
}
