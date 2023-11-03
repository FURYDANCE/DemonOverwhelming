using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{
    /// <summary>
    /// Ãÿ ‚¥ Ãı£∫∂‹«Ω£∫∂‘‘∂≥Ã…À∫¶100%√‚“ﬂ
    /// </summary>
    public class Character_SpecialTag_ShieldWall : Character_SpecialTagBase
    {
        public float level;
        public Character_SpecialTag_ShieldWall()
        {

        }

        public void AfterAttack(Entity thisEntity, float damage, Entity damageTarget, DamageData damageData)
        {

        }

        public void AfterHurt(Entity thisEntity, float damage, Entity damageCreater, DamageData damageData)
        {

        }

        public void BeforeAttack(Entity thisEntity, Entity damageTarget, DamageData damageData, out DamageData data)
        {
            data = damageData;
        }

        public void BeforeHurt(Entity thisEntity, Entity damageCreater, DamageData damageData, out DamageData data)
        {
            damageData.farDamage *= 0;
            data = damageData;
        }


    }
}
