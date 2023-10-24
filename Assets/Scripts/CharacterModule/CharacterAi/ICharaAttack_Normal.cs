using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
namespace DemonOverwhelming
{
=======
/// <summary>
/// ��ͨ������λ��ִ��һ�ι������������֮��ص��ж�״̬
/// </summary>
public class ICharaAttack_Normal : MonoBehaviour, ICharacterAttack
{
    Entity entity;
    CharacterStateManager manager;
    float time;
    public void Attack()
    {
        //manager.Attack(manager.attackTarget);
        StartCoroutine(AttackIenumerator(manager.attackTarget));
        StartCoroutine(ReturnToNormal());
        manager.entity.FlipTo(manager.attackTarget.transform.position);
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

>>>>>>> c920aad3 (8.23 修改了战斗界面，加入几个新兵种卡（目前可以同时存在8张卡），修复了兵种生成相关的bug)

    /// <summary>
    /// ��ͨ������λ��ִ��һ�ι������������֮��ص��ж�״̬
    /// </summary>
    public class ICharaAttack_Normal : MonoBehaviour, ICharacterAttack
    {
        Entity entity;
        CharacterStateManager manager;
        float time;
        public void Attack()
        {
            //manager.Attack(manager.attackTarget);
            StartCoroutine(AttackIenumerator(manager.attackTarget));
            StartCoroutine(ReturnToNormal());
            manager.entity.FlipTo(manager.attackTarget.transform.position);
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
}