using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 普通攻击单位：执行一次攻击，攻击完毕之后回到行动状态
/// </summary>
public class ICharaAttack_Normal : MonoBehaviour,ICharacterAttack
{
    Entity entity;
    CharacterStateManager manager;
    float time;
    public void Attack()
    {
        //manager.Attack(manager.attackTarget);
        StartCoroutine(AttackIenumerator(manager.attackTarget));
        StartCoroutine(ReturnToNormal());
    }

   
    void Start()
    {
        entity = GetComponent<Entity>();
        manager = GetComponent<CharacterStateManager>();
        time = entity.parameter.character.attackTime + entity.parameter.character.attackWaitTime;
    }

    IEnumerator ReturnToNormal()
    {
        yield return new WaitForSeconds(time);
        manager.ChangeState(new CharacterWalkingState());
    }
   

    /// <summary>
    /// 攻击（协程）
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    IEnumerator AttackIenumerator(Entity e)
    {

        manager.canAttack = false;

        //动画
        entity.PlayAniamtion_Attack();
        yield return new WaitForSeconds(entity.parameter.character.attackTime);
        if (e)
            BattleManager.instance.GenerateOneMissle(entity, transform.position, entity.parameter.character.missileId, e);
        yield return new WaitForSeconds(entity.parameter.character.attackWaitTime);
        manager.canAttack = true;
    }
}
