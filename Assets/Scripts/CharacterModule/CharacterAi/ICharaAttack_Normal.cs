using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ͨ������λ��ִ��һ�ι������������֮��ص��ж�״̬
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
    /// ������Э�̣�
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    IEnumerator AttackIenumerator(Entity e)
    {

        manager.canAttack = false;

        //����
        entity.PlayAniamtion_Attack();
        yield return new WaitForSeconds(entity.parameter.character.attackTime);
        if (e)
            BattleManager.instance.GenerateOneMissle(entity, transform.position, entity.parameter.character.missileId, e);
        yield return new WaitForSeconds(entity.parameter.character.attackWaitTime);
        manager.canAttack = true;
    }
}
