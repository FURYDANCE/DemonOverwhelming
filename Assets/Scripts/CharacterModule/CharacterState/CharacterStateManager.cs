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

    /// <summary>
    /// 实现角色移动接口的脚本
    /// </summary>
    public ICharacterMove moveScript;
    public ICharacterEnemyCheck enemyCheckScript;
    public ICharacterChase chaseScript;
    public ICharacterAttack attackScript;


    SpriteRenderer spriteRenderer;
    void Start()
    {
        start_Y = transform.position.y;
        canAttack = true;
        entity = GetComponent<Entity>();
        parameter = GetComponent<Entity>().parameter;
        ChangeState(new CharacterGeneratingState());
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
        GetInterfaceScript();
    }
    //获取行动模块
    public void GetInterfaceScript()
    {
        moveScript = GetComponent<ICharacterMove>();
        enemyCheckScript = GetComponent<ICharacterEnemyCheck>();
        chaseScript = GetComponent<ICharacterChase>();
        attackScript = GetComponent<ICharacterAttack>();
    }
    void Update()
    {
        currentState.OnUpdate(this);
        CheckEnemy();
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
            Debug.Log("将攻击目标设为空");
            attackTarget = null;
            return;
        }
        Debug.Log("设置了攻击目标");
        attackTarget = e;
    }
    /// <summary>
    /// 检查攻击范围有没有敌人
    /// </summary>
    public void CheckEnemy()
    {
        enemySelected.Clear();
        
        enemyChecked = Physics.OverlapBox(entity.camp == Camp.demon ? new Vector3(transform.position.x + parameter.character.EnemyCheckOffset.x, transform.position.y + parameter.character.EnemyCheckOffset.y, transform.position.z)
            : new Vector3(transform.position.x - parameter.character.EnemyCheckOffset.x, transform.position.y + parameter.character.EnemyCheckOffset.y, transform.position.z)
            , parameter.character.EnemyCheckArea);
        foreach (Collider c in enemyChecked)
        {
            if (!c)
                continue;
            Entity e = c.GetComponent<Entity>();
            if (e  &&c&& e.camp != entity.camp)
            {
                enemySelected.Add(c);
            }
        }
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
        if (entity)
            //Gizmos.DrawWireCube(entity.camp == Camp.demon ? transform.position + parameter.character.EnemyCheckOffset : transform.position - parameter.character.EnemyCheckOffset, parameter.character.EnemyCheckArea);
            Gizmos.DrawWireCube(entity.camp == Camp.demon ? new Vector3(transform.position.x + parameter.character.EnemyCheckOffset.x, transform.position.y + parameter.character.EnemyCheckOffset.y, transform.position.z)
            : new Vector3(transform.position.x - parameter.character.EnemyCheckOffset.x, transform.position.y + parameter.character.EnemyCheckOffset.y, transform.position.z)
            , parameter.character.EnemyCheckArea);
    }

}
