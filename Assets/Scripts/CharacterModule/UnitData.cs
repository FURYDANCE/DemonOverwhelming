using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{

    /// <summary>
    /// ���ʵ��������࣬����Ҳ������ɫ��������Ͷ�������
    /// </summary>
    [System.Serializable]
    public class UnitData
    {
        public string ID;
        [Header("����ʹ��RtsEngine�¸ĵģ��ڵ�λ����������Ի�ȡ����λԤ�Ƽ�")]
        public GameObject unitPrefab;
        /// <summary>
        /// ʵ������
        /// </summary>
        public EntityType type;
        public string name;
        [JsonIgnore]
        public Sprite sprite;
        public float Hp;
        public float nowHp;
        [Header("�˺�")]
        public float hurtDamage;
        [Header("��ʾ��Ѫ���ĳߴ��ƫ��")]
        public float hpBarSize;
        public Vector3 hpBarOffset;
        [Header("��λ���")]
        [Multiline(5)]
        public string introduct;
        [Header("��ɫģ�ͳߴ�")]
        public float modleSize;
        [Header("Ӱ�ӳߴ磬ƫ��")]
        public Vector3 shadowSize;
        public float shadowOffset;
        [Header("AI��Ϊģʽ")]
        public Ai_MoveType aiMode_Move;
        public Ai_CheckType aiMode_Check;
        public Ai_ChaseType aiMode_Chase;
        public Ai_AttackType aiMode_Atk;
        [Header("��ɫ����")]
        public UnitParameter_Character character;
        [Header("���������ȫ��Ϊ�ٷֱȣ�")]
        public UnitParameter_Buff buffParameter;

        public void SetValue(UnitData data)
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
            aiMode_Move = data.aiMode_Move;
            aiMode_Check = data.aiMode_Check;
            aiMode_Chase = data.aiMode_Chase;
            aiMode_Atk = data.aiMode_Atk;
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
       
            //if (data.character.missileId != "")
            //{
            //    //missile = data.missile;
            //    missile = data.missile.SetValue(data.missile, true);

            //}
            character.defence_far = data.character.defence_far;
            character.defence_magic = data.character.defence_magic;
            character.missileId = data.character.missileId;
            //building.building_canAttack = data.building.building_canAttack;
            character.skillIds = data.character.skillIds;
            character.skillLevels = data.character.skillLevels;
            character.descriptionAndStory = data.character.descriptionAndStory;
            buffParameter = new UnitParameter_Buff();
        }
        
    }
    [System.Serializable]
    public class UnitParameter_Character
    {
        public string id;
        public int cost;
        /// <summary>
        /// ai����
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
        public string[] skillIds;
        public List<Skill> skills;
        public string[] skillLevels;
        public string[] descriptionAndStory;
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
        public float lifeTime; //�������ڣ��������
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
}
[System.Serializable]
public class UnitParameter_Buff
{
    public float speedBuff;
}