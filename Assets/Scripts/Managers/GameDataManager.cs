using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
/// <summary>
/// 存放项目文件中需要被调用的对象,包括初始化文件
/// </summary>
public class GameDataManager : MonoBehaviour
{
    /// <summary>
    /// 通过excel表保存的数据总和
    /// </summary>
    [Header("通过excel表保存的数据总和")]
    public ExcelDataManager excelDatas;
    [Header("动画数据集合")]
    public AnimationsObject animationData;
    [Header("可能被调用的材质的数据集合")]
    public MaterialObject materialObject;
    [Header("兵种组的集合")]
    public SoldierGroupObject soldierGroupObject;
    [Header("spine预制件数据集合")]
    public SpinePrefabObject spinePrefabDatas;
    /// <summary>
    /// 将生成的空实体
    /// </summary>
    [Header("将生成的空实体")]
    public GameObject emptyEntity;
    [Header("将生成的空spine实体")]
    public GameObject emptySpine;
    [Header("空的选择卡")]
    public GameObject emptySoldierCard;
    [Header("空的布阵卡")]
    public GameObject emptyFormat;
    public GameObject a;
    public static GameDataManager instance;
    
    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
        excelDatas = (ExcelDataManager)Resources.Load("DataTables/PlotsDatas");
    }

    private void Start()
    {
        Initialization();   
    }
    /// <summary>
    /// 初始化
    /// </summary>
    void Initialization()
    {
        //如果存档文件不存在，则先创建存档文件
        if (!File.Exists(Application.persistentDataPath + "/" + JsonEditor.unitFileName + ".json"))
            SaveJson(Application.persistentDataPath, JsonEditor.unitFileName, excelDatas.unitDatas);
        if (!File.Exists(Application.persistentDataPath + "/" + JsonEditor.cardFileName + ".json"))
            SaveJson(Application.persistentDataPath, JsonEditor.cardFileName, excelDatas.cardDatas);
        if (!File.Exists(Application.persistentDataPath + "/" + JsonEditor.missileFileName + ".json"))
            SaveJson(Application.persistentDataPath, JsonEditor.missileFileName, excelDatas.missileDatas);
        //读取存档文件
        ReadJson_Unit();
        ReadJson_Card();
        ReadJson_Missile();
    }
    #region 通过id获取各个内容
    /// <summary>
    /// 通过id获取对话内容
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public PlotsData GetPoltById(int id)
    {
        PlotsData find = excelDatas.plotsData.Find((PlotsData p) => { return p.id == id; });
        return find;
    }
    /// <summary>
    /// 通过id获取实体信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public UnitParameter GetEntityDataById(string id)
    {
        UnitParameter find = excelDatas.unitDatas.Find((UnitParameter p) => { return p.ID == id; });
        return find;
    }
    /// <summary>
    /// 通过id获取兵牌信息
    /// 并且找到id所对应的兵种组
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public SoldierCardParameter GetSoldierCardById(string id)
    {
        SoldierCardParameter find = excelDatas.cardDatas.Find((SoldierCardParameter p) => { return p.id == id; });
        find.content = soldierGroupObject.group.Find((SoldierGroup_ p) => { return p.id == find.id; }).contentObject.GetComponent<SoliderGroup>();
        return find;
    }
    /// <summary>
    /// 通过id获取投射物信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public UnitParameter_Missile GetMissileDataById(string id)
    {
        UnitParameter_Missile find = excelDatas.missileDatas.Find((UnitParameter_Missile p) => { return p.id == id; });
        return find;
    }
    /// <summary>
    /// 通过id获取动画信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public AnimationsList GetAnimatinosDataById(string id)
    {
        AnimationsList animations= animationData.animations.Find((AnimationsList p) => { return p.id == id; });
        return animations;
    }
    public GameObject GetSpinePrefabDataById(string id)
    {
        SpinePrefab spinePrefab = spinePrefabDatas.spinePrefabs.Find((SpinePrefab p) => { return p.id == id; });
        return spinePrefab.spinePrefab;
    }
    /// <summary>
    /// 通过id获取伤害信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public DamageData GetDamageDataById(string id)
    {
        DamageData find= excelDatas.damageDatas.Find((DamageData p) => { return p.id == id; });
        DamageData d = new DamageData();
        d.id = find.id;
        d.name = find.name;
        d.physicDamage = find.physicDamage;
        d.farDamage = find.farDamage;
        d.magicDamage = find.magicDamage;
        d.buffs = find.buffs;
        d.specialEffects = find.specialEffects;
        d.vfxId = find.vfxId;
        d.vfxSize = find.vfxSize;
        d.startDamageWiatTime = find.startDamageWiatTime;
        return d;
    }
    #endregion

    #region 数据的读存
    /// <summary>
    /// 保存数据文件
    /// </summary>
    public void SaveJson(string path, string fileName, object o)
    {
        JsonEditor.WritingJson(path, fileName, o);
        Debug.Log("将文件写入json完成");
    }
    /// <summary>
    /// 从json中读取数据文件
    /// </summary>
    public void ReadJson_Unit()
    {

        //先获取读到的json
        List<UnitParameter> readData = JsonEditor.ReadingJson(Application.persistentDataPath, JsonEditor.unitFileName, excelDatas.unitDatas);
        //遍历本地数据并且存进去
        for (int i = 0; i < readData.Count; i++)
        {
            //Debug.Log(readData[i].ID);
            excelDatas.unitDatas[i].SetValue_noSprite(readData[i]);
        }
        Debug.Log("从json读取文件完成");
    }
    public void ReadJson_Card()
    {
        //先获取读到的json
        List<SoldierCardParameter> readData = JsonEditor.ReadingJson(Application.persistentDataPath, JsonEditor.cardFileName, excelDatas.cardDatas);
        //遍历本地数据并且存进去
        for (int i = 0; i < readData.Count; i++)
        {
            excelDatas.cardDatas[i].SetValue(readData[i]);
        }
        Debug.Log("从json读取文件完成");
    }
    public void ReadJson_Missile()
    {
        //先获取读到的json
        List<UnitParameter_Missile> readData = JsonEditor.ReadingJson(Application.persistentDataPath, JsonEditor.missileFileName, excelDatas.missileDatas);
        //遍历本地数据并且存进去
        for (int i = 0; i < readData.Count; i++)
        {
            excelDatas.missileDatas[i].SetValue(readData[i], false);
        }
        Debug.Log("从json读取文件完成");
    }
    #endregion

}
