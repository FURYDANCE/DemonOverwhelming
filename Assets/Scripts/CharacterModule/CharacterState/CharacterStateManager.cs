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

    /// <summary>
    /// ʵ�ֽ�ɫ�ƶ��ӿڵĽű�
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
    //��ȡ�ж�ģ��
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
            Debug.Log("������Ŀ����Ϊ��");
            attackTarget = null;
            return;
        }
        Debug.Log("�����˹���Ŀ��");
        attackTarget = e;
    }
    /// <summary>
    /// ��鹥����Χ��û�е���
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
        if (entity)
            //Gizmos.DrawWireCube(entity.camp == Camp.demon ? transform.position + parameter.character.EnemyCheckOffset : transform.position - parameter.character.EnemyCheckOffset, parameter.character.EnemyCheckArea);
            Gizmos.DrawWireCube(entity.camp == Camp.demon ? new Vector3(transform.position.x + parameter.character.EnemyCheckOffset.x, transform.position.y + parameter.character.EnemyCheckOffset.y, transform.position.z)
            : new Vector3(transform.position.x - parameter.character.EnemyCheckOffset.x, transform.position.y + parameter.character.EnemyCheckOffset.y, transform.position.z)
            , parameter.character.EnemyCheckArea);
    }

}
