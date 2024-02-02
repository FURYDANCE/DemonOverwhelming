using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace DemonOverwhelming
{
    /// <summary>
    /// 状态管理器脚本，用行为树的话，之后这个脚本要弃用了
    /// </summary>
    public class CharacterStateManager : MonoBehaviour
    {
        /// <summary>
        /// 当前状态
        /// </summary>
        public CharacterBaseState currentState;
        /// <summary>
        /// 当前实体
        /// </summary>
        public Entity entity;
        /// <summary>
        /// 实体变量
        /// </summary>
        UnitData parameter;
        public Transform moveTarget;

        [Header("当前攻击目标")]
        public Entity attackTarget;
        public Collider[] enemyChecked;
        public List<Collider> enemySelected;
        public bool canAttack;

        public bool isGenerating;
        public bool isWalking;
        public bool isChaseing;
        public bool isAttacking;
        public bool isSkilling;
        public bool isDying;
        [HideInInspector]
        public bool intoWalking;
        public float start_Y;

        public NavMeshAgent agent;

        SpriteRenderer spriteRenderer;
        void Start()
        {
            start_Y = transform.position.y;
            canAttack = true;
            entity = GetComponent<Entity>();
            parameter = GetComponent<Entity>().parameter;
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            //ChangeState(new CharacterGeneratingState());
            //enemySelected = new List<Collider>();
            //spriteRenderer = GetComponent<SpriteRenderer>();
            //if (spriteRenderer)
            //{
            //    if (entity.camp == Camp.demon)
            //        spriteRenderer.flipX = false;
            //    else
            //        spriteRenderer.flipX = true;
            //}


        }

        void Update()
        {

            InputMove();
            //currentState.OnUpdate(this);
            //CheckEnemy();
        }


        //inputsystem输入的各个方法
        bool mvLeft, mvRight, mvUp, mvDown;
        /// <summary>
        /// 重置键入的各个方向
        /// </summary>
        public void ResetMovingDir()
        {
            mvLeft = false;
            mvRight = false;
            mvUp = false;
            mvDown = false;
        }
        /// <summary>
        /// 检测是否在输入移动中
        /// </summary>
        /// <returns></returns>
        public bool CheckIsInputMoving()
        {
            if (!mvLeft && !mvRight && !mvUp && !mvDown)
                return false;
            else
                return true;
        }
        public void OnMoveLeft(InputAction.CallbackContext context)
        {
            if (cantMove)
            {
                Debug.Log("当前不能移动");
                return;
            }
            if (context.started)
            {

                entity.PlayAniamtion_Walk();
                mvLeft = true;
            }

            if (context.canceled)
            {
                mvLeft = false;
                if (!CheckIsInputMoving())
                    entity.PlayAnimation_Idle();
            }
        }
        public void OnMoveRight(InputAction.CallbackContext context)
        {
            if (cantMove)
            {
                Debug.Log("当前不能移动");
                return;
            }
            if (context.started)
            {
                entity.PlayAniamtion_Walk();
                mvRight = true;
            }

            if (context.canceled)
            {
                mvRight = false;
                if (!CheckIsInputMoving())
                    entity.PlayAnimation_Idle();
            }
        }
        public void OnMoveUp(InputAction.CallbackContext context)
        {
            if (cantMove)
            {
                Debug.Log("当前不能移动");
                return;
            }
            if (context.phase == InputActionPhase.Started)
            {
                entity.PlayAniamtion_Walk();
                mvUp = true;
            }

            if (context.phase == InputActionPhase.Canceled)
            {
                mvUp = false;
                if (!CheckIsInputMoving())
                    entity.PlayAnimation_Idle();
            }
        }
        public void OnMoveDown(InputAction.CallbackContext context)
        {
            if (cantMove)
            {
                Debug.Log("当前不能移动");
                return;
            }
            if (context.started)
            {
                entity.PlayAniamtion_Walk();
                mvDown = true;
            }

            if (context.canceled)
            {
                mvDown = false;
                if (!CheckIsInputMoving())
                    entity.PlayAnimation_Idle();
            }
        }
        //使用技能的输入判定
        bool skillInput;
        /// <summary>
        /// 获取是否键入技能按键
        /// </summary>
        /// <returns></returns>
        public bool GetSkillInput() => skillInput;
        /// <summary>
        /// 行为树读取到可以使用技能之后便会执行使用技能的行为
        /// </summary>
        /// <param name="isTrue"></param>
        public void SetSkillInput(bool isTrue) => skillInput = isTrue;
        public void OnSkill(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                if (entity.CheckSkillCanUse(1))
                {
                    SetSkillInput(true);
                }
                //entity.UseSkill(1);
            }

        }
        bool cantMove;
        public void SetCantMove(bool isTrue) => cantMove = isTrue;
        public void InputMove()
        {
            if (cantMove)
            {
                Debug.Log("当前不能移动");
                return;
            }
            float speedX = 0, speedZ = 0;
            if (mvLeft)
                speedX = -parameter.character.moveSpeed;
            if (mvRight)
                speedX = parameter.character.moveSpeed;
            if (mvUp)
                speedZ = parameter.character.moveSpeed;
            if (mvDown)
                speedZ = -parameter.character.moveSpeed;
            Vector3 speed = new Vector3(speedX, 0, speedZ);
            agent.velocity = speed;
            entity.FlipTo(transform.position + speed);




        }




        /// <summary>
        /// 改变状态
        /// </summary>
        /// <param name="newState"></param>
        public void ChangeState(CharacterBaseState newState)
        {
            if (currentState != null)
                currentState.OnExit(this);
            currentState = newState;
            currentState.OnEnter(this);
        }


        public void SetAttackTarget(Entity e)
        {
            if (e == null)
            {
                //Debug.Log("将攻击目标设为空");
                attackTarget = null;
                return;
            }
            //Debug.Log("设置了攻击目标");
            attackTarget = e;
        }
        /// <summary>
        /// 检查攻击范围有没有敌人
        /// </summary>
        public void CheckEnemy()
        {
            //enemySelected.Clear();

            //enemyChecked = Physics.OverlapBox(entity.camp == Camp.demon ? new Vector3(transform.position.x + parameter.character.EnemyCheckOffset.x, transform.position.y + parameter.character.EnemyCheckOffset.y, transform.position.z)
            //    : new Vector3(transform.position.x - parameter.character.EnemyCheckOffset.x, transform.position.y + parameter.character.EnemyCheckOffset.y, transform.position.z)
            //    , parameter.character.EnemyCheckArea);
            //foreach (Collider c in enemyChecked)
            //{
            //    if (!c)
            //        continue;
            //    Entity e = c.GetComponent<Entity>();
            //    if (e && c && e.camp != entity.camp)
            //    {
            //        enemySelected.Add(c);
            //    }
            //}
        }
        /// <summary>
        /// 检查是否进入攻击状态
        /// </summary>
        public void AttackStateCheck()
        {
            if (attackTarget == null)
                return;
            //获取距离碰撞体的最近距离
            Vector3 s = attackTarget.gameObject.GetComponent<Collider>().ClosestPoint(transform.position);
            float attackDistance = Vector3.Distance(transform.position, s);
            if (attackDistance <= parameter.character.attackDistance)
            {
                ChangeState(new CharacterAttackingState());
            }
        }
        private void OnDrawGizmos()
        {
            //if (entity != null && parameter != null)
            //    //Gizmos.DrawWireCube(entity.camp == Camp.demon ? transform.position + parameter.character.EnemyCheckOffset : transform.position - parameter.character.EnemyCheckOffset, parameter.character.EnemyCheckArea);
            //    Gizmos.DrawWireCube(entity.camp == Camp.demon ? new Vector3(transform.position.x + parameter.character.EnemyCheckOffset.x, transform.position.y + parameter.character.EnemyCheckOffset.y, transform.position.z)
            //    : new Vector3(transform.position.x - parameter.character.EnemyCheckOffset.x, transform.position.y + parameter.character.EnemyCheckOffset.y, transform.position.z)
            //    , parameter.character.EnemyCheckArea);
        }

    }
}