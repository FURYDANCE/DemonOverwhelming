using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DemonOverwhelming;

public class ExcelDataManager : ScriptableObject
{
    public List<PlotsData> plotsData;

    public List<UnitData> unitDatas;
    public List<UnitParameter_Character> characterDatas;
    public List<UnitParameter_Building> buildingDatas;
    public List<SoldierCardParameter> cardDatas;
    public List<UnitParameter_Missile> missileDatas;
    public List<DamageData> damageDatas;
    public List<Skill> skillDatas;
}
