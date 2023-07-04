using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAi_Archer : MonoBehaviour, AiBase
{
    //public UnitParameter parameter;
    public CharacterStateManager character;
    public Entity entity;
    public float targetChangeTime;
    public float targetChangeTimer;

    public Vector3 x;
    public bool changed;
    // Start is called before the first frame update
    void Start()
    {
        entity = GetComponent<Entity>();
        character = GetComponent<CharacterStateManager>();
        character.nowAi = this;
        targetChangeTimer = targetChangeTime;
    }

    // Update is called once per frame
    void Update()
    {
        SetWalkDir();
        if (character.isWalking)
        {
            character.CheckEnemy();
            if (character.enemySelected.Count != 0)
            {
                SelectEnemy();
            }
        }
    }
    /// <summary>
    /// ����û�е���ʱ���ƶ�����
    /// </summary>
    public void SetWalkDir()
    {
        if (character.intoWalking)
        {
            character.intoWalking = false;

            if (entity.camp == Camp.demon)
                character.moveTarget = character.right;
            else
                character.moveTarget = character.left;
        }

    }
    /// <summary>
    /// ��ͨ��սai����������ľ���ѡ��Ŀ��
    /// </summary>
    public void SelectEnemy()
    {
        Entity selected = null;
        float minDistance = Vector3.Distance(transform.position, character.enemySelected[0].transform.position);
        foreach (Collider c in character.enemySelected)
        {
            if (Vector3.Distance(transform.position, c.transform.position) <= minDistance)
            {
                selected = c.GetComponent<Entity>();
            }
        }
        //Debug.Log("ѡ����Ŀ��");
        character.SetEnemy(selected);
    }

    public void Attack(Entity e)
    {
        StartCoroutine(AttackIenumerator(e));
    }

    /// <summary>
    /// ������Э�̣�
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    IEnumerator AttackIenumerator(Entity e)
    {
        //Debug.Log("��������");
        character.canAttack = false;
        //����
        yield return new WaitForSeconds(entity.parameter.character.attackTime);
        //����Ͷ����
        if (e)
            BattleManager.instance.GenerateOneMissle(this.entity, transform.position, entity.parameter.character.missileId, e);
        yield return new WaitForSeconds(entity.parameter.character.attackWaitTime);
        character.canAttack = true;
    }
}

