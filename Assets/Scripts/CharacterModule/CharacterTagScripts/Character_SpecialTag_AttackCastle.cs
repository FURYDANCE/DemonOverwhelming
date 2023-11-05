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
            //�˺�������Ϊ����ʱ����ɵ��˺�*1+level�İٷֱ�
            if (damageTarget.parameter.type == EntityType.building)
            {
                damageData.physicDamage *= (1 + (level / 100));
                damageData.farDamage *= (1 + (level / 100));
                damageData.magicDamage *= (1 + (level / 100));
            }
        }

        public void BeforeHurt(Entity thisEntity, Entity damageCreater, DamageData damageData, out DamageData data)
        {
            //�˺�������Ϊ����ʱ���ܵ����˺�*=1+level�İٷֱ�
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