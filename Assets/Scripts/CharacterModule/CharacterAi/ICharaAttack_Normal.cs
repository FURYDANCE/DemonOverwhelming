using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
namespace DemonOverwhelming
{
=======
/// <summary>
/// ÆÕÍ¨¹¥»÷µ¥Î»£ºÖ´ĞĞÒ»´Î¹¥»÷£¬¹¥»÷Íê±ÏÖ®ºó»Øµ½ĞĞ¶¯×´Ì¬
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

>>>>>>> c920aad3 (8.23 ä¿®æ”¹äº†æˆ˜æ–—ç•Œé¢ï¼ŒåŠ å…¥å‡ ä¸ªæ–°å…µç§å¡ï¼ˆç›®å‰å¯ä»¥åŒæ—¶å­˜åœ¨8å¼ å¡ï¼‰ï¼Œä¿®å¤äº†å…µç§ç”Ÿæˆç›¸å…³çš„bug)

    /// <summary>
    /// ÆÕÍ¨¹¥»÷µ¥Î»£ºÖ´ĞĞÒ»´Î¹¥»÷£¬¹¥»÷Íê±ÏÖ®ºó»Øµ½ĞĞ¶¯×´Ì¬
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
        /// ¹¥»÷£¨Ğ­³Ì£©
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        IEnumerator AttackIenumerator(Entity e)
        {

            manager.canAttack = false;

            //¶¯»­
            entity.PlayAniamtion_Attack();
            yield return new WaitForSeconds(entity.parameter.character.attackTime);
            if (e)
                BattleManager.instance.GenerateOneMissle(entity, transform.position, entity.parameter.character.missileId, e);
            yield return new WaitForSeconds(entity.parameter.character.attackWaitTime);
            manager.canAttack = true;
        }
    }
}