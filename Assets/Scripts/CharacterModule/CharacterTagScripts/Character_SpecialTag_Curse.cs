using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{


    public class Character_SpecialTag_Curse : Character_SpecialTagBase
    {
        public void AfterAttack(Entity thisEntity, float damage, Entity damageTarget, DamageData damageData)
        {

        }

        public void AfterHurt(Entity thisEntity, float damage, Entity damageCreater, DamageData damageData)
        {

        }

        public void BeforeAttack(Entity thisEntity, Entity damageTarget, DamageData damageData, out DamageData data)
        {
            if (damageTarget.tag == Tags.Hero)
            {
                damageData.farDamage = 50;
                damageData.physicDamage = 50;
                damageData.magicDamage = 50;
            }
            else
            {
                damageData.farDamage = 999999;
                damageData.physicDamage = 999999;
                damageData.magicDamage = 999999;
            }
            data = damageData;
        }

        public void BeforeHurt(Entity thisEntity, Entity damageCreater, DamageData damageData, out DamageData data)
        {
            data = damageData;
        }


    }

}