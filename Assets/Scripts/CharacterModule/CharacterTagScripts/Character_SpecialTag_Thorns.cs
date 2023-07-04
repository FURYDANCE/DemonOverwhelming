using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_SpecialTag_Thorns : Character_SpecialTagBase
{
    public float level;
    public Character_SpecialTag_Thorns(float level)
    {
        this.level = level;
    }

    public void AfterAttack(Entity thisEntity, float damage, Entity damageTarget, DamageData damageData)
    {

    }
    /// <summary>
    /// …À∫¶∑¥…‰
    /// </summary>
    /// <param name="thisEntity"></param>
    /// <param name="damage"></param>
    /// <param name="damageCreater"></param>
    /// <param name="damageData"></param>
    public void AfterHurt(Entity thisEntity, float damage, Entity damageCreater, DamageData damageData)
    {
        float finalDamage;
        DamageData newdamage = damageData;
        newdamage.physicDamage = Mathf.Clamp(damageData.physicDamage - thisEntity.parameter.character.defence, 0, 999999999) * (level / 100);
        damageCreater.TakeDamage(newdamage, thisEntity, out finalDamage);
        Debug.Log("…À∫¶∑µªπ£∫" + newdamage.physicDamage);
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
