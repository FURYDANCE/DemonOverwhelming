using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 战士ai：默认朝对应方向移动，检测最近的敌人攻击
/// </summary>
public class CharacterAi_Warrior : MonoBehaviour, AiBase
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
            //if (entity.camp == camp.demon)
            //    character.moveTarget = new Vector3(character.right.transform.position.x, start_Y, transform.position.z);
            //else
            //    character.moveTarget = new Vector3(character.left.transform.position.x, start_Y, transform.position.z);


            character.CheckEnemy();
            if (character.enemySelected.Count != 0)
            {
                SelectEnemy();
            }
        }

    }
    /// <summary>
    /// 设置没有敌人时的移动方向
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
    /// 普通近战ai：根据最近的距离选择目标
    /// </summary>
    public void SelectEnemy()
    {
        Entity selected = null;
        float minDistance = Vector3.Distance(transform.position, character.enemySelected[0].transform.position);
        foreach (Collider c in character.enemySelected)
        {
            //Debug.Log("MINDISTANCE：" + minDistance + "\nC's Distance:" + c.name + "   " + Vector3.Distance(transform.position, c.transform.position));
            if (Vector3.Distance(transform.position, c.transform.position) <= minDistance)
            {
                minDistance = Vector3.Distance(transform.position, c.transform.position);
                selected = c.GetComponent<Entity>();
            }
        }
        //Debug.Log("选择了目标");
        character.SetEnemy(selected);
    }

    public void Attack(Entity e)
    {
        StartCoroutine(AttackIenumerator(e));
    }

    /// <summary>
    /// 攻击（协程）
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    IEnumerator AttackIenumerator(Entity e)
    {

        //Debug.Log("攻击！！");
        character.canAttack = false;

        //动画
        //StartCoroutine(AttackAniIenu());
        entity.PlayAniamtion_Attack();
        yield return new WaitForSeconds(entity.parameter.character.attackTime);
        if (e)
            BattleManager.instance.GenerateOneMissle(entity, transform.position, entity.parameter.character.missileId, e);
        yield return new WaitForSeconds(entity.parameter.character.attackWaitTime);
        character.canAttack = true;
    }
    //IEnumerator AttackAniIenu()
    //{
    //    if (entity.parameter.nowHp > 0 && entity.skAni != null)
    //    {
    //        entity.PlayAniamtion_Attack();
    //        yield return new WaitForSeconds(entity.skAni.skeletonDataAsset.duration[0]);
    //        entity.PlayAnimation_Idle();
    //    }
    //}
}
