using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Character_SpecialTagBase
{
    public void BeforeAttack(Entity thisEntity, Entity damageTarget, DamageData damageData,out DamageData data);

    public void AfterAttack(Entity thisEntity, float damage, Entity damageTarget, DamageData damageData);

    public void BeforeHurt(Entity thisEntity, Entity damageCreater, DamageData damageData,out DamageData data);

    public void AfterHurt(Entity thisEntity, float damage, Entity damageCreater, DamageData damageData);
}