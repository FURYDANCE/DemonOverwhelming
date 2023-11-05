using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{
    public class Character_SpecialTag_AttackCastle : Character_SpecialTagBase
    {
        public float level;

        public Character_SpecialTag_AttackCastle(float level)
        {
            this.level = level;
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
            //伤害承受者为建筑时，造成的伤害*1+level的百分比
            if (damageTarget.parameter.type == EntityType.building)
            {
                damageData.physicDamage *= (1 + (level / 100));
                damageData.farDamage *= (1 + (level / 100));
                damageData.magicDamage *= (1 + (level / 100));
            }
        }

        public void BeforeHurt(Entity thisEntity, Entity damageCreater, DamageData damageData, out DamageData data)
        {
            //伤害创造者为建筑时，受到的伤害*=1+level的百分比
            if (damageCreater.parameter.type == EntityType.building)
            {
                damageData.physicDamage *= (1 + (level / 100));
                damageData.farDamage *= (1 + (level / 100));
                damageData.magicDamage *= (1 + (level / 100));
            }
            data = damageData;
        }


    }
}