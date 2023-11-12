using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace DemonOverwhelming
{

    public class ICharaMove_InputKey : MonoBehaviour, ICharacterMove
    {
        Entity entity;
        float speed;
        public float moveDir_X;
        public float moveDir_Y;
        public Vector3 moveDir;
        CharacterStateManager manager;
        bool m_Left;
        bool m_Right;
        bool m_Up;
        bool m_Down;
        public void Moving()
        {
            transform.position = Vector3.MoveTowards(transform.position, moveDir, speed * Time.deltaTime);

        }


        void Start()
        {
            manager = GetComponent<CharacterStateManager>();
            entity = GetComponent<Entity>();
            speed = entity.parameter.character.moveSpeed;
            manager.GetInterfaceScript();
        }
        //用于判断动画的变量
        bool moving = false;
        void Update()
        {
            if (m_Left)
                moveDir_X = -1;
            else if (m_Right)
                moveDir_X = 1;
            else
                moveDir_X = 0;
            if (m_Down)
                moveDir_Y = -1;
            else if (m_Up)
                moveDir_Y = 1;
            else
                moveDir_Y = 0;
            moveDir = transform.position + new Vector3(moveDir_X, moveDir_Y, 0).normalized;
            //当有输入的时候不能进入追击状态，仅当无输入的时候可以进入
            if (moveDir_X == 0 && moveDir_Y == 0)
                manager.SetAttackTarget(manager.enemyCheckScript.EnemyCheck());





            if (moveDir == transform.position && moving)
            {
                moving = false;
                Debug.Log("不动");
                entity.PlayAnimation_Idle();
            }
            if (moveDir != transform.position && !moving)
            {
                moving = true;
                Debug.Log("动");
                entity.PlayAniamtion_Walk();

            }
            entity.FlipTo(transform.position + new Vector3(moveDir_X, moveDir_Y));
        }
        public void OnMoveRight(InputAction.CallbackContext value)
        {

            if (value.phase == InputActionPhase.Performed)
                m_Right = true;
            //moveDir_X = 1;


            if (value.phase == InputActionPhase.Canceled)
                m_Right = false;


            //moveDir = transform.position + new Vector3(moveDir_X, moveDir_Y, 0).normalized;

        }
        public void OnMoveLeft(InputAction.CallbackContext value)
        {
            if (value.phase == InputActionPhase.Performed)
                m_Left = true;
            if (value.phase == InputActionPhase.Canceled)
                m_Left = false;

            //moveDir = transform.position + new Vector3(moveDir_X, moveDir_Y, 0).normalized;


        }
        public void OnMoveUp(InputAction.CallbackContext value)
        {
            if (value.phase == InputActionPhase.Performed)
                m_Up = true;
            if (value.phase == InputActionPhase.Canceled)
                m_Up = false;

            //moveDir = transform.position + new Vector3(moveDir_X, moveDir_Y, 0).normalized;


        }
        public void OnMoveDown(InputAction.CallbackContext value)
        {
            if (value.phase == InputActionPhase.Performed)
                m_Down = true;
            if (value.phase == InputActionPhase.Canceled)
                m_Down = false;
            //moveDir_Y = 0;
            //moveDir = transform.position + new Vector3(moveDir_X, moveDir_Y, 0).normalized;


        }
        public void OnSkill(InputAction.CallbackContext value)
        {
            if (value.phase == InputActionPhase.Performed)
                entity.UseSkill(1);

        }
    }
}