using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace DemonOverwhelming
{
    /// <summary>
    /// ״̬�������ű�������Ϊ���Ļ���֮������ű�Ҫ������
    /// </summary>
    public class CharacterStateManager : MonoBehaviour
    {
        /// <summary>
        /// ��ǰ״̬
        /// </summary>
        public CharacterBaseState currentState;
        /// <summary>
        /// ��ǰʵ��
        /// </summary>
        public Entity entity;
        /// <summary>
        /// ʵ�����
        /// </summary>
        UnitData parameter;
        public Transform moveTarget;

        [Header("��ǰ����Ŀ��")]
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


        //inputsystem����ĸ�������
        bool mvLeft, mvRight, mvUp, mvDown;
        /// <summary>
        /// ���ü���ĸ�������
        /// </summary>
        public void ResetMovingDir()
        {
            mvLeft = false;
            mvRight = false;
            mvUp = false;
            mvDown = false;
        }
        /// <summary>
        /// ����Ƿ��������ƶ���
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
                Debug.Log("��ǰ�����ƶ�");
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
                Debug.Log("��ǰ�����ƶ�");
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
                Debug.Log("��ǰ�����ƶ�");
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
                Debug.Log("��ǰ�����ƶ�");
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
        //ʹ�ü��ܵ������ж�
        bool skillInput;
        /// <summary>
        /// ��ȡ�Ƿ���뼼�ܰ���
        /// </summary>
        /// <returns></returns>
        public bool GetSkillInput() => skillInput;
        /// <summary>
        /// ��Ϊ����ȡ������ʹ�ü���֮����ִ��ʹ�ü��ܵ���Ϊ
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
                Debug.Log("��ǰ�����ƶ�");
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
        /// �ı�״̬
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
                //Debug.Log("������Ŀ����Ϊ��");
                attackTarget = null;
                return;
            }
            //Debug.Log("�����˹���Ŀ��");
            attackTarget = e;
        }
        /// <summary>
        /// ��鹥����Χ��û�е���
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
        /// ����Ƿ���빥��״̬
        /// </summary>
        public void AttackStateCheck()
        {
            if (attackTarget == null)
                return;
            //��ȡ������ײ����������
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