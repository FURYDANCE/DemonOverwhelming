using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    UnitParameter parameter;
    /// <summary>
    /// 使用的ai
    /// </summary>
    public AiBase nowAi;
    public Transform moveTarget;
    [HideInInspector]
    public Transform left;
    [HideInInspector]
    public Transform right;
    [Header("当前攻击目标")]
    public Transform attackTarget;
    Collider[] enemyChecked;
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

    SpriteRenderer spriteRenderer;
    void Start()
    {
        start_Y = transform.position.y;
        canAttack = true;
        entity = GetComponent<Entity>();
        parameter = GetComponent<Entity>().parameter;
        ChangeState(new CharacterGeneratingState());
        left = Instantiate(new GameObject(), transform).transform;
        right = Instantiate(new GameObject(), transform).transform;
        enemySelected = new List<Collider>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer)
        {
            if (entity.camp == Camp.demon)
                spriteRenderer.flipX = false;
            else
                spriteRenderer.flipX = true;
        }
        if (entity.spineObject)
        {
            if (entity.camp == Camp.demon)
                transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }


    void Update()
    {
        if (parameter.nowHp <= 0)
            return;
        currentState.OnUpdate(this);
        //if (parameter.nowHp <= 0)
        //    Destroy(gameObject);
        right.transform.position = new Vector3(transform.position.x + 1, start_Y, transform.position.z);
        left.transform.position = new Vector3(transform.position.x - 1, start_Y, transform.position.z);

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
    /// <summary>
    /// 位移
    /// </summary>
    public void Translate()
    {
        if (moveTarget)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(moveTarget.position.x, moveTarget.position.y, transform.position.z), parameter.character.moveSpeed * Time.deltaTime);
        if (!entity.spineObject)
        {
            if (moveTarget && transform.position.x < moveTarget.transform.position.x)
                spriteRenderer.flipX = false;
            else
                spriteRenderer.flipX = true;
        }
        //else
        //{
        //    if (moveTarget && transform.position.x < moveTarget.transform.position.x)
        //        transform.rotation = Quaternion.Euler(0, 180, 0);
        //    else
        //        transform.rotation = Quaternion.Euler(0, 0, 0);
        //}
    }
    /// <summary>
    /// 改变位移目标
    /// </summary>
    /// <param name="target"></param>
    public void SetMoveTarget(Transform target)
    {
        moveTarget = target;
    }
    /// <summary>
    /// 检查攻击范围有没有敌人
    /// </summary>
    public void CheckEnemy()
    {
        enemySelected.Clear();
        //enemyChecked = Physics.OverlapBox(entity.camp == Camp.demon ? transform.position + parameter.character.EnemyCheckOffset : transform.position - parameter.character.EnemyCheckOffset, parameter.character.EnemyCheckArea);
        enemyChecked = Physics.OverlapBox(entity.camp == Camp.demon ? new Vector3(transform.position.x + parameter.character.EnemyCheckOffset.x, transform.position.y + parameter.character.EnemyCheckOffset.y, transform.position.z)
            : new Vector3(transform.position.x - parameter.character.EnemyCheckOffset.x, transform.position.y + parameter.character.EnemyCheckOffset.y, transform.position.z)
            , parameter.character.EnemyCheckArea);
        foreach (Collider c in enemyChecked)
        {
            //Entity p = c.transform.parent.GetComponent<Entity>();
            //if (p && p.camp != entity.camp)
            //{
            //    enemySelected.Add(c);
            //}
            Entity e = c.GetComponent<Entity>();
            if (e && e.camp != entity.camp)
            {
                enemySelected.Add(c);
            }
        }
    }
    /// <summary>
    /// 设置攻击目标
    /// </summary>
    /// <param name="e"></param>
    public void SetEnemy(Entity e)
    {

        moveTarget = e.transform;
        attackTarget = e.transform;
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
        //if (entity.camp == Camp.human)
        //    Debug.Log("attackDistance:" + parameter.character.attackDistance + "      nowDistance:" + attackDistance);
        if (attackDistance <= parameter.character.attackDistance)
        {
            ChangeState(new CharacterAttackingState());
        }
    }
    ///// <summary>
    ///// 检查是否进入追击状态
    ///// </summary>
    //public void ChaseingStateCheck()
    //{
    //    if (attackTarget == null)
    //        return;
    //    float attackDistance = Vector3.Distance(transform.position, attackTarget.transform.position);
    //    //Debug.Log("距离：" + attackDistance);
    //    if (attackDistance > parameter.character.attackDistance + 0.1f)
    //    {
    //        ChangeState(new CharacterChasingState());
    //    }
    //}
    private void OnDrawGizmos()
    {
        if (entity)
            //Gizmos.DrawWireCube(entity.camp == Camp.demon ? transform.position + parameter.character.EnemyCheckOffset : transform.position - parameter.character.EnemyCheckOffset, parameter.character.EnemyCheckArea);
            Gizmos.DrawWireCube(entity.camp == Camp.demon ? new Vector3(transform.position.x + parameter.character.EnemyCheckOffset.x, transform.position.y + parameter.character.EnemyCheckOffset.y, transform.position.z)
            : new Vector3(transform.position.x - parameter.character.EnemyCheckOffset.x, transform.position.y + parameter.character.EnemyCheckOffset.y, transform.position.z)
            , parameter.character.EnemyCheckArea);
    }
    /// <summary>
    /// 攻击
    /// </summary>
    /// <param name="e"></param>
    public void Attack(Entity e)
    {
        if (!canAttack)
            return;
        nowAi.Attack(e);

    }

}
