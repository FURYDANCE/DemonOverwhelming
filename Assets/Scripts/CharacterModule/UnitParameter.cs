using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 存放实体变量的类，其中也包括角色，建筑，投射物变量
/// </summary>
[System.Serializable]
public class UnitParameter
{
    public string ID;

    /// <summary>
    /// 实体类型
    /// </summary>
    public EntityType type;
    public string name;
    [JsonIgnore]
    public Sprite sprite;
    public float Hp;
    public float nowHp;
    [Header("伤害")]
    public float hurtDamage;
    [Header("显示的血条的尺寸和偏移")]
    public float hpBarSize;
    public Vector3 hpBarOffset;
    [Header("单位简介")]
    [Multiline(5)]
    public string introduct;
    [Header("角色模型尺寸")]
    public float modleSize;
    [Header("影子尺寸，偏移")]
    public Vector3 shadowSize;
    public float shadowOffset;

    [Header("角色变量")]
    public UnitParameter_Character character;
    [Header("建筑变量")]
    public UnitParameter_Building building;
    //[Header("投射物变量")]
    //public UnitParameter_Missile missile;


    public void SetValue(UnitParameter data)
    {
        ID = data.ID;
        type = data.type;
        name = data.name;
        sprite = data.sprite;
        Hp = data.Hp;
        nowHp = data.nowHp;
        hurtDamage = data.hurtDamage;
        hpBarSize = data.hpBarSize;
        hpBarOffset = data.hpBarOffset;
        introduct = data.introduct;
        shadowSize = data.shadowSize;
        shadowOffset = data.shadowOffset;
        character = new UnitParameter_Character();
        character.aiType = data.character.aiType;
        character.attackDistance = data.character.attackDistance;
        character.attackTime = data.character.attackTime;
        character.attackWaitTime = data.character.attackWaitTime;
        character.chaseTime = data.character.chaseTime;
        character.cost = data.character.cost;
        character.defence = data.character.defence;
        character.EnemyCheckArea = new Vector3(data.character.EnemyCheckArea.x, data.character.EnemyCheckArea.y, 100);
        character.EnemyCheckOffset = data.character.EnemyCheckOffset;
        character.haveSkill = data.character.haveSkill;
        character.moveSpeed = data.character.moveSpeed;
        character.id = data.character.id;
        character.specialTags = data.character.specialTags;
        character.bloodDrop = data.character.bloodDrop;
        modleSize = data.modleSize;
        building = data.building;
        //if (data.character.missileId != "")
        //{
        //    //missile = data.missile;
        //    missile = data.missile.SetValue(data.missile, true);

        //}
        character.defence_far = data.character.defence_far;
        character.defence_magic = data.character.defence_magic;
        character.missileId = data.character.missileId;
        building.building_canAttack = data.building.building_canAttack;
        
    }
    public void SetValue_noSprite(UnitParameter data)
    {
        ID = data.ID;
        type = data.type;
        name = data.name;
        Hp = data.Hp;
        nowHp = data.nowHp;
        hurtDamage = data.hurtDamage;
        hpBarSize = data.hpBarSize;
        hpBarOffset = data.hpBarOffset;
        introduct = data.introduct;
        shadowSize = data.shadowSize;
        shadowOffset = data.shadowOffset;
        character = new UnitParameter_Character();
        character.aiType = data.character.aiType;
        character.attackDistance = data.character.attackDistance;
        character.attackTime = data.character.attackTime;
        character.attackWaitTime = data.character.attackWaitTime;
        character.chaseTime = data.character.chaseTime;
        character.cost = data.character.cost;
        character.defence = data.character.defence;
        character.EnemyCheckArea = new Vector3(data.character.EnemyCheckArea.x, data.character.EnemyCheckArea.y, 100);
        character.EnemyCheckOffset = data.character.EnemyCheckOffset;
        character.haveSkill = data.character.haveSkill;
        character.moveSpeed = data.character.moveSpeed;
        character.id = data.character.id;
        character.specialTags = data.character.specialTags;
        character.bloodDrop = data.character.bloodDrop;
        modleSize = data.modleSize;
        building = data.building;
        //if (data.character.missileId != "0")
        //{
        //    //missile = data.missile;
        //    missile = data.missile.SetValue(data.missile, false);
        //    //missile = data.missile.SetValue(data.missile, false);
        //}
        character.missileId = data.character.missileId;
        character.defence_far = data.character.defence_far;
        character.defence_magic = data.character.defence_magic;
        building.building_canAttack = data.building.building_canAttack;

    }
}
[System.Serializable]
public class UnitParameter_Character
{
    public string id;
    public int cost;
    /// <summary>
    /// ai类型
    /// </summary>
    public AiType aiType;
    public float moveSpeed;
    public float defence;
    public float defence_far;
    public float defence_magic;
    public bool haveSkill;
    public int skillId;
    public string skillName;

    public Vector3 EnemyCheckArea;

    public Vector3 EnemyCheckOffset;
    public float attackDistance;
    public float chaseTime;
    public float attackWaitTime;
    public float attackTime;
    public string missileId;
    public string[] specialTags;
    public float bloodDrop;
}
[System.Serializable]
public class UnitParameter_Building
{
    public string id;
    public bool building_canAttack;

}
[System.Serializable]
public class UnitParameter_Missile
{
    public string id;
    public string name;
    public string damageDataId;
    public DamageData damageData;
    public float speed;
    public MissileMoveType moveType;
    [JsonIgnore]
    public Sprite sprite;
    public bool useLifeTime;
    public float lifeTime; //生命周期，测试完成
    public bool useAoe;
    public Vector3 aoeArea;
    public float aoeWaitTime;
    public bool createNewObjectWhenDie;
    public string objectCreatedWhenDieId;
    public string startObjectId;
    public string endObjectId;
    public string trailId;
    public int aoeAmount;
    public float arcMoveTime;
    public float arcMoveHeight;
    public UnitParameter_Missile SetValue(UnitParameter_Missile data, bool useSprite)
    {
        id = data.id;
        name = data.name;
        damageData = data.damageData;
        speed = data.speed;
        moveType = data.moveType;
        if (useSprite)
            sprite = data.sprite;
        useLifeTime = data.useLifeTime;
        lifeTime = data.lifeTime;
        useAoe = data.useAoe;
        aoeArea = new Vector3(data.aoeArea.x, data.aoeArea.y, 50);
        createNewObjectWhenDie = data.createNewObjectWhenDie;
        objectCreatedWhenDieId = data.objectCreatedWhenDieId;
        startObjectId = data.startObjectId;
        trailId = data.trailId;
        endObjectId = data.endObjectId;
        aoeWaitTime = data.aoeWaitTime;
        arcMoveTime = data.arcMoveTime;
        arcMoveHeight = data.arcMoveHeight;
        
        return this;
    }
}

