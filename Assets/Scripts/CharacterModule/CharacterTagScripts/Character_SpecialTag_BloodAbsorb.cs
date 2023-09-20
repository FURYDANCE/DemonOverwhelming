using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{


    public class Character_SpecialTag_BloodAbsorb : Character_SpecialTagBase
    {
        public float level;
        public Character_SpecialTag_BloodAbsorb(float level)
        {
            this.level = level;
        }
        /// <summary>
        /// ��Ѫ������������
        /// </summary>
        /// <param name="thisEntity"></param>
        /// <param name="damage"></param>
        /// <param name="damageCreater"></param>
        /// <param name="damageData"></param>
        public void AfterAttack(Entity thisEntity, float damage, Entity damageCreater, DamageData damageData)
        {
            thisEntity.parameter.nowHp += damage * (level / 100);
            thisEntity.parameter.nowHp = Mathf.Clamp(thisEntity.parameter.nowHp, 0, thisEntity.parameter.Hp);
            //����������Ч
            //����
            thisEntity.RefreshHp();
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
            data = damageData;
        }



    }
}