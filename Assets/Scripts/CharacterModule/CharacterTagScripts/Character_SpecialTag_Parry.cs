using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_SpecialTag_Parry : Character_SpecialTagBase
{
    public float level;
    public Character_SpecialTag_Parry(float level)
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
    }

    public void BeforeHurt(Entity thisEntity, Entity damageCreater, DamageData damageData, out DamageData data)
    {
        damageData.physicDamage *= (1 - (level / 100));
        data = damageData;
    }


}
