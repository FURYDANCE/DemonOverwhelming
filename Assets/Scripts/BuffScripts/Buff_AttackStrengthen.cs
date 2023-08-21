/// <summary>
/// 使单位攻击力增强的buff（战意高昂）
/// </summary>
public class Buff_AttackStrengthen : BuffBase
{

    public void OnAddBuff(Entity e, float buffLevel)
    {
        float newDamage = e.parameter.hurtDamage * buffLevel;
        e.parameter.hurtDamage = newDamage;
    }

    public void OnRemoveBuff(Entity e, float buffLevel)
    {
        float newDamage = e.parameter.hurtDamage / buffLevel;
        e.parameter.hurtDamage = newDamage;
    }

    public void OnUpdateBuff(Entity e, float buffLevel)
    {

    }
    public void OnAddbuff_Missile(Missile m,float buffLevel)
    {
        float newDamage = m.parameter.damageData.physicDamage * buffLevel;
        m.parameter.damageData.physicDamage = newDamage;
    }
}
