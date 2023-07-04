using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    UnitParameter parameter;
    /// <summary>
    /// ʹ�õ�ai
    /// </summary>
    public AiBase nowAi;
    public Transform moveTarget;
    [HideInInspector]
    public Transform left;
    [HideInInspector]
    public Transform right;
    [Header("��ǰ����Ŀ��")]
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
    /// <summary>
    /// λ��
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
    /// �ı�λ��Ŀ��
    /// </summary>
    /// <param name="target"></param>
    public void SetMoveTarget(Transform target)
    {
        moveTarget = target;
    }
    /// <summary>
    /// ��鹥����Χ��û�е���
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
    /// ���ù���Ŀ��
    /// </summary>
    /// <param name="e"></param>
    public void SetEnemy(Entity e)
    {

        moveTarget = e.transform;
        attackTarget = e.transform;
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
        //if (entity.camp == Camp.human)
        //    Debug.Log("attackDistance:" + parameter.character.attackDistance + "      nowDistance:" + attackDistance);
        if (attackDistance <= parameter.character.attackDistance)
        {
            ChangeState(new CharacterAttackingState());
        }
    }
    ///// <summary>
    ///// ����Ƿ����׷��״̬
    ///// </summary>
    //public void ChaseingStateCheck()
    //{
    //    if (attackTarget == null)
    //        return;
    //    float attackDistance = Vector3.Distance(transform.position, attackTarget.transform.position);
    //    //Debug.Log("���룺" + attackDistance);
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
    /// ����
    /// </summary>
    /// <param name="e"></param>
    public void Attack(Entity e)
    {
        if (!canAttack)
            return;
        nowAi.Attack(e);

    }

}
