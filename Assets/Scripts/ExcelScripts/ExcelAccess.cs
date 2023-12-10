using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Excel;
using UnityEditor;
using System.IO;
using System.Data;
using DemonOverwhelming;
using System;

public class ExcelAccess
{
    /// <summary>
    /// 对话表的文件名
    /// </summary>
    public static string PlotsExcel = "PlotsExcel";

    /// <summary>
    /// 实体表的文件名
    /// </summary>
    public static string EntityExcel = "EntityExcel";

    public enum LoadSpriteType
    {
        character, missile, flag, stand, icon
    }
#if UNITY_EDITOR
    public static Sprite LoadSprite(string path, LoadSpriteType loadSpriteType)
    {
        if (loadSpriteType == LoadSpriteType.character)
        {
            return AssetDatabase.LoadAssetAtPath<Sprite>("Assets/AddressableAssetsData/Sprites/ModelSprites/CharacterSprite/" + path + ".png");
        }
        if (loadSpriteType == LoadSpriteType.flag)
        {
            return AssetDatabase.LoadAssetAtPath<Sprite>("Assets/AddressableAssetsData/Sprites/ModelSprites/FlagSprite/" + path + ".png");
        }
        if (loadSpriteType == LoadSpriteType.missile)
        {
            return AssetDatabase.LoadAssetAtPath<Sprite>("Assets/AddressableAssetsData/Sprites/ModelSprites/MissileSprite/" + path + ".png");
        }
        if (loadSpriteType == LoadSpriteType.stand)
        {
            return AssetDatabase.LoadAssetAtPath<Sprite>("Assets/AddressableAssetsData/Sprites/DialogSprites/" + path + ".png");
        }
        if (loadSpriteType == LoadSpriteType.icon)
        {
            return AssetDatabase.LoadAssetAtPath<Sprite>("Assets/AddressableAssetsData/Sprites/ModelSprites/Icons/" + path + ".png");
        }
        else
            return null;

    }

    /// <summary>
    /// 读表，生成每句对话的对象，通过读表将对应的内容赋值给对象
    /// </summary>
    public static List<PlotsData> SelectMenuTable()
    {
        string excelName = PlotsExcel + ".xlsx";
        string sheetName = "sheet1";
        DataRowCollection collect = ReadExcel(excelName, sheetName);

        List<PlotsData> dataArray = new List<PlotsData>();
        for (int i = 3; i < collect.Count; i++)
        {
            if (collect[i][0].ToString() == "") continue; //行不是空的就开始执行
            PlotsData pd = new PlotsData
            {
                id = int.Parse(collect[i][0].ToString()),
                content_cn = collect[i][1].ToString(),
                speakerNmae = collect[i][2].ToString(),
                isLast = collect[i][3].ToString() == "True" ? true : false,

                left_stand = LoadSprite(collect[i][4].ToString(), LoadSpriteType.stand),
                right_stand = LoadSprite(collect[i][5].ToString(), LoadSpriteType.stand),

                events = collect[i][6].ToString().Split("/"),

                haveOption = collect[i][7].ToString() == "True" ? true : false,
                optionContents = collect[i][8].ToString().Split("/"),

                optionTargetIds = Array.ConvertAll<string, int>(collect[i][9].ToString().Split("/"), delegate (string s) { return int.Parse(s); }),
            };
            dataArray.Add(pd);
        }
        return dataArray;
    }
    /// <summary>
    /// 根据表内数值生成对应的对象，存在scriptableObject中
    /// </summary>
    /// <param name="entityData"></param>
    /// <param name="characterData"></param>
    /// <param name="buildingData"></param>
    /// <param name="cardDara"></param>
    /// <param name="missileData"></param>
    public static void SelectEntityTable(out List<UnitData> entityData, out List<UnitParameter_Character> characterData, out List<UnitParameter_Building> buildingData,
        out List<SoldierCardParameter> cardDara, out List<UnitParameter_Missile> missileData, out List<DamageData> damageData, out List<Skill> skillData)
    {
        string excelName = EntityExcel + ".xlsx";
        string sheetName = "Entity";
        //string sheetName_character = "Character";
        //string sheetName_building = "Building";
        string sheetName_SoldierGroup = "SoldierCard";
        string sheetName_Missile = "Missile";
        string sheetName_Damage = "Damage";
        string sheetName_Skill = "Skill";
        DataRowCollection collect = ReadExcel(excelName, sheetName);
        DataRowCollection collect_4 = ReadExcel(excelName, sheetName_SoldierGroup);
        DataRowCollection collect_5 = ReadExcel(excelName, sheetName_Missile);
        DataRowCollection collect_6 = ReadExcel(excelName, sheetName_Damage);
        DataRowCollection collect_7 = ReadExcel(excelName, sheetName_Skill);

        List<UnitData> dataArray = new List<UnitData>();
        List<UnitParameter_Character> dataArray_character = new List<UnitParameter_Character>();
        List<UnitParameter_Building> dataArray_building = new List<UnitParameter_Building>();
        List<SoldierCardParameter> dataArray_soldierCard = new List<SoldierCardParameter>();
        List<UnitParameter_Missile> dataArray_missile = new List<UnitParameter_Missile>();
        List<DamageData> dataArray_damage = new List<DamageData>();
        List<Skill> dataArray_skill = new List<Skill>();
        //实体信息变量
        for (int i = 3; i < collect.Count; i++)
        {
            if (collect[i][0].ToString() == "") continue; //行不是空的就开始执行
            UnitData up = new UnitData
            {

                ID = collect[i][0].ToString(),
                unitPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/AddressableAssetsData/Prefabs/Unit/new_unit.prefab"),
                type = (EntityType)int.Parse(collect[i][1].ToString()),
                name = collect[i][2].ToString(),
                sprite = LoadSprite(collect[i][3].ToString(), LoadSpriteType.character),
                hpBarSize = float.Parse(collect[i][4].ToString()),
                hpBarOffset = new Vector3(float.Parse(collect[i][5].ToString().Split(",")[0]), float.Parse(collect[i][5].ToString().Split(",")[1])),
                introduct = collect[i][6].ToString(),
                Hp = float.Parse(collect[i][7].ToString()),
                //hurtDamage = float.Parse(collect[i][8].ToString()),
                modleSize = float.Parse(collect[i][8].ToString()),
                shadowSize = new Vector3(float.Parse(collect[i][9].ToString().Split(",")[0]), float.Parse(collect[i][9].ToString().Split(",")[1])),
                shadowOffset = float.Parse(collect[i][9].ToString().Split(",")[2]),

                aiMode_Move = (Ai_MoveType)int.Parse(collect[i][10].ToString().Split("/")[0]),
                aiMode_Check = (Ai_CheckType)int.Parse(collect[i][10].ToString().Split("/")[1]),
                aiMode_Chase = (Ai_ChaseType)int.Parse(collect[i][10].ToString().Split("/")[2]),
                aiMode_Atk = (Ai_AttackType)int.Parse(collect[i][10].ToString().Split("/")[3]),


            };
            up.character = new UnitParameter_Character();
            up.character.id = up.ID;
            up.character.defence = float.Parse(collect[i][11].ToString());
            up.character.defence_far = float.Parse(collect[i][12].ToString());
            up.character.defence_magic = float.Parse(collect[i][13].ToString());
            up.character.attackTime = float.Parse(collect[i][14].ToString());
            up.character.attackWaitTime = float.Parse(collect[i][15].ToString());
            up.character.EnemyCheckArea = new Vector3(float.Parse(collect[i][16].ToString().Split(",")[0]), float.Parse(collect[i][16].ToString().Split(",")[1]));
            up.character.EnemyCheckOffset = new Vector3(float.Parse(collect[i][17].ToString().Split(",")[0]), float.Parse(collect[i][17].ToString().Split(",")[1]));
            up.character.attackDistance = float.Parse(collect[i][18].ToString());
            up.character.haveSkill = collect[i][19].ToString() == "True" ? true : false;
            up.character.moveSpeed = float.Parse(collect[i][20].ToString());
            up.character.aiType = (AiType)int.Parse(collect[i][21].ToString());
            up.character.missileId = collect[i][22].ToString();
            up.character.specialTags = collect[i][23].ToString().Split(",");
            up.character.bloodDrop = float.Parse(collect[i][24].ToString());

            up.character.skillIds = collect[i][25].ToString().Split("/");
            up.character.skillLevels = collect[i][26].ToString().Split("/");
            up.character.descriptionAndStory = collect[i][27].ToString().Split("|");
            dataArray.Add(up);
        }


        //兵种卡变量
        for (int i = 3; i < collect_4.Count; i++)
        {
            if (collect_4[i][0].ToString() == "") continue; //行不是空的就开始执行
            SoldierCardParameter psc = new SoldierCardParameter
            {
                id = collect_4[i][0].ToString(),
                name = collect_4[i][1].ToString(),
                sprite = LoadSprite(collect_4[i][2].ToString(), LoadSpriteType.character),
                flagSprite = LoadSprite(collect_4[i][3].ToString(), LoadSpriteType.flag),
                moneyCost = float.Parse(collect_4[i][4].ToString()),
                bloodCost = float.Parse(collect_4[i][5].ToString()),
                formationId = collect_4[i][6].ToString(),
                soldierId = collect_4[i][7].ToString(),
                soldierPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/AddressableAssetsData/Prefabs/Unit/new_unit.prefab")
            };
            dataArray_soldierCard.Add(psc);
        }
        //投射物变量
        for (int i = 3; i < collect_5.Count; i++)
        {
            if (collect_5[i][0].ToString() == "") continue; //行不是空的就开始执行
            UnitParameter_Missile pm = new UnitParameter_Missile
            {
                id = collect_5[i][0].ToString(),
                name = collect_5[i][1].ToString(),
                damageDataId = collect_5[i][2].ToString(),
                speed = float.Parse(collect_5[i][3].ToString()),
                moveType = (MissileMoveType)int.Parse(collect_5[i][4].ToString()),
                sprite = LoadSprite(collect_5[i][5].ToString(), LoadSpriteType.missile),
                useLifeTime = collect_5[i][6].ToString() == "True" ? true : false,
                lifeTime = float.Parse(collect_5[i][7].ToString()),
                useAoe = collect_5[i][8].ToString() == "True" ? true : false,
                aoeArea = new Vector3(float.Parse(collect_5[i][9].ToString().Split(",")[0]), float.Parse(collect_5[i][9].ToString().Split(",")[1]), 0),

                createNewObjectWhenDie = collect_5[i][10].ToString() == "True" ? true : false,
                objectCreatedWhenDieId = collect_5[i][11].ToString(),
                startObjectId = collect_5[i][12].ToString(),
                trailId = collect_5[i][13].ToString(),
                endObjectId = collect_5[i][14].ToString(),
                aoeWaitTime = float.Parse(collect_5[i][15].ToString()),
                aoeAmount = int.Parse(collect_5[i][16].ToString()),
                arcMoveTime = float.Parse(collect_5[i][17].ToString().Split(",")[0]),
                arcMoveHeight = float.Parse(collect_5[i][17].ToString().Split(",")[1]),
            };
            dataArray_missile.Add(pm);
        }
        //伤害变量
        for (int i = 3; i < collect_6.Count; i++)
        {
            if (collect_5[i][0].ToString() == "") continue; //行不是空的就开始执行
            DamageData dd = new DamageData
            {
                id = collect_6[i][0].ToString(),
                name = collect_6[i][1].ToString(),
                physicDamage = float.Parse(collect_6[i][2].ToString()),
                farDamage = float.Parse(collect_6[i][3].ToString()),
                magicDamage = float.Parse(collect_6[i][4].ToString()),
                buffs = collect_6[i][5].ToString().Split("/"),
                specialEffects = collect_6[i][6].ToString().Split("/"),
                vfxId = collect_6[i][7].ToString(),
                vfxSize = float.Parse(collect_6[i][8].ToString()),
                startDamageWiatTime = float.Parse(collect_6[i][9].ToString()),
            };
            dataArray_damage.Add(dd);
        }
        //技能变量
        for (int i = 3; i < collect_7.Count; i++)
        {
            if (collect_7[i][0].ToString() == "") continue; //行不是空的就开始执行
            Skill sl = new Skill
            {
                id = collect_7[i][0].ToString(),
                skillName = collect_7[i][1].ToString(),
                skillLevel = int.Parse(collect_7[i][2].ToString()),
                waitTime = float.Parse(collect_7[i][3].ToString()),
                bloodCost = float.Parse(collect_7[i][4].ToString()),
                moneyCost = float.Parse(collect_7[i][5].ToString()),
                skillIcon = LoadSprite(collect_7[i][6].ToString(), LoadSpriteType.icon),
                eachLevelDescriptions = collect_7[i][7].ToString().Split("|"),
            };
            dataArray_skill.Add(sl);
        }
        //弃用的：赋值单位的角色、建筑变量
        //foreach (UnitParameter up in dataArray)
        //{
        //    //获取角色数值
        //    if (up.type == EntityType.character)
        //    {
        //        up.character = dataArray_character.Find((UnitParameter_Character p) => { return p.id == up.ID; });
        //        Debug.Log(up.character);
        //    }
        //    if (up.type == EntityType.building)
        //    {
        //        up.character = dataArray_character.Find((UnitParameter_Character p) => { return p.id == up.ID; });
        //        up.building = dataArray_building.Find((UnitParameter_Building p) => { return p.id == up.ID; });
        //    }
        //}
        entityData = dataArray;
        characterData = dataArray_character;
        buildingData = dataArray_building;
        cardDara = dataArray_soldierCard;
        missileData = dataArray_missile;
        damageData = dataArray_damage;
        skillData = dataArray_skill;
    }


    static DataRowCollection ReadExcel(string excelName, string sheetName)
    {
        string path = Application.dataPath + "/Resources/DataTables/" + excelName;
        FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        IExcelDataReader wr = ExcelReaderFactory.CreateOpenXmlReader(stream);
        DataSet result = excelReader.AsDataSet();
        return result.Tables[sheetName].Rows;

    }
#endif
}