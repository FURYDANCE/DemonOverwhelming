using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{

    /// <summary>
    /// ��ɫ�н�״̬
    /// </summary>
    public class CharacterWalkingState : CharacterBaseState
    {
        public void OnEnter(CharacterStateManager manager)
        {
            //Debug.Log("�����ж�״̬");
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

            //������Ŀ�겻Ϊ��ʱ����׷��״̬
            if (manager.attackTarget != null)
            {
                manager.ChangeState(new CharacterChasingState());
                return;
            }
            
        }


    }
}
