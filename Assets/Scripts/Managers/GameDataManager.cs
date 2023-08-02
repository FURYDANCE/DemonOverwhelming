using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
/// <summary>
/// �����Ŀ�ļ�����Ҫ�����õĶ���,������ʼ���ļ�
/// </summary>
public class GameDataManager : MonoBehaviour
{
    /// <summary>
    /// ͨ��excel����������ܺ�
    /// </summary>
    [Header("ͨ��excel����������ܺ�")]
    public ExcelDataManager excelDatas;
    [Header("�������ݼ���")]
    public AnimationsObject animationData;
    [Header("���ܱ����õĲ��ʵ����ݼ���")]
    public MaterialObject materialObject;
    [Header("������ļ���")]
    public FormationObject formationObject;
    [Header("spineԤ�Ƽ����ݼ���")]
    public SpinePrefabObject spinePrefabDatas;
    /// <summary>
    /// �����ɵĿ�ʵ��
    /// </summary>
    [Header("�����ɵĿ�ʵ��")]
    public GameObject emptyEntity;
    [Header("�����ɵĿ�spineʵ��")]
    public GameObject emptySpine;
    [Header("�յ�ѡ��")]
    public GameObject emptySoldierCard;
    [Header("�յĲ���")]
    public GameObject emptyFormat;
    [Header("Ǯ��")]
    public GameObject moneyBag;
    [Header("�����ڵ���ʾ��Ϣ")]
    public GameObject sceneInformation;
    public GameObject a;
    public Sprite bloodSprite;
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
    /// ��ʼ��
    /// </summary>
    void Initialization()
    {
        //����浵�ļ������ڣ����ȴ����浵�ļ�
        if (!File.Exists(Application.persistentDataPath + "/" + JsonEditor.unitFileName + ".json"))
            SaveJson(Application.persistentDataPath, JsonEditor.unitFileName, excelDatas.unitDatas);
        if (!File.Exists(Application.persistentDataPath + "/" + JsonEditor.cardFileName + ".json"))
            SaveJson(Application.persistentDataPath, JsonEditor.cardFileName, excelDatas.cardDatas);
        if (!File.Exists(Application.persistentDataPath + "/" + JsonEditor.missileFileName + ".json"))
            SaveJson(Application.persistentDataPath, JsonEditor.missileFileName, excelDatas.missileDatas);
        //��ȡ�浵�ļ�
        ReadJson_Unit();
        ReadJson_Card();
        ReadJson_Missile();
    }
    #region ͨ��id��ȡ��������
    /// <summary>
    /// ͨ��id��ȡ�Ի�����
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public PlotsData GetPoltById(int id)
    {
        PlotsData find = excelDatas.plotsData.Find((PlotsData p) => { return p.id == id; });
        return find;
    }
    /// <summary>
    /// ͨ��id��ȡʵ����Ϣ
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public UnitParameter GetEntityDataById(string id)
    {
        UnitParameter find = excelDatas.unitDatas.Find((UnitParameter p) => { return p.ID == id; });
        return find;
    }
    /// <summary>
    /// ͨ��id��ȡ������Ϣ
    /// �����ҵ�id����Ӧ�ı�����
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public SoldierCardParameter GetSoldierCardById(string id)
    {
        SoldierCardParameter find = excelDatas.cardDatas.Find((SoldierCardParameter p) => { return p.id == id; });
        find.content = formationObject.formations.Find((Formation p) => { return p.id == find.formationId; }).contentFormation.GetComponent<SoliderGroup>();
        find.content.Id = find.soldierId;
        return find;
    }
    /// <summary>
    /// ͨ��id��ȡͶ������Ϣ
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public UnitParameter_Missile GetMissileDataById(string id)
    {
        UnitParameter_Missile find = excelDatas.missileDatas.Find((UnitParameter_Missile p) => { return p.id == id; });
        return find;
    }
    /// <summary>
    /// ͨ��id��ȡ������Ϣ
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public AnimationsList GetAnimatinosDataById(string id)
    {
        AnimationsList animations = animationData.animations.Find((AnimationsList p) => { return p.id == id; });
        return animations;
    }
    public GameObject GetSpinePrefabDataById(string id)
    {
        SpinePrefab spinePrefab = spinePrefabDatas.spinePrefabs.Find((SpinePrefab p) => { return p.id == id; });
        return spinePrefab.spinePrefab;
    }
    /// <summary>
    /// ͨ��id��ȡ�˺���Ϣ
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public DamageData GetDamageDataById(string id)
    {
        DamageData find = excelDatas.damageDatas.Find((DamageData p) => { return p.id == id; });
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
    /// <summary>
    /// ͨ��id��ȡ������Ϣ
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public GameObject GetFormationById(string id)
    {
        GameObject go = formationObject.formations.Find((Formation f) => { return f.id == id; }).contentFormation.gameObject;
        if (!go)
            return null;
        return go;
    }
    /// <summary>
    /// ͨ��id��ȡ������Ϣ
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Skill GetSkillById(string id)
    {
        Skill s = excelDatas.skillDatas.Find((Skill p) => { return p.id == id; });
        Skill skill = new Skill();
        if (s==null)
            return null;
        skill.SetValue(s);
        return skill;
    }
    #endregion

    #region ���ݵĶ���
    /// <summary>
    /// ���������ļ�
    /// </summary>
    public void SaveJson(string path, string fileName, object o)
    {
        JsonEditor.WritingJson(path, fileName, o);
        Debug.Log("���ļ�д��json���");
    }
    /// <summary>
    /// ��json�ж�ȡ�����ļ�
    /// </summary>
    public void ReadJson_Unit()
    {

        //�Ȼ�ȡ������json
        List<UnitParameter> readData = JsonEditor.ReadingJson(Application.persistentDataPath, JsonEditor.unitFileName, excelDatas.unitDatas);
        //�����������ݲ��Ҵ��ȥ
        for (int i = 0; i < readData.Count; i++)
        {
            //Debug.Log(readData[i].ID);
            excelDatas.unitDatas[i].SetValue_noSprite(readData[i]);
        }
        Debug.Log("��json��ȡ�ļ����");
    }
    public void ReadJson_Card()
    {
        //�Ȼ�ȡ������json
        List<SoldierCardParameter> readData = JsonEditor.ReadingJson(Application.persistentDataPath, JsonEditor.cardFileName, excelDatas.cardDatas);
        //�����������ݲ��Ҵ��ȥ
        for (int i = 0; i < readData.Count; i++)
        {
            excelDatas.cardDatas[i].SetValue(readData[i]);
        }
        Debug.Log("��json��ȡ�ļ����");
    }
    public void ReadJson_Missile()
    {
        //�Ȼ�ȡ������json
        List<UnitParameter_Missile> readData = JsonEditor.ReadingJson(Application.persistentDataPath, JsonEditor.missileFileName, excelDatas.missileDatas);
        //�����������ݲ��Ҵ��ȥ
        for (int i = 0; i < readData.Count; i++)
        {
            excelDatas.missileDatas[i].SetValue(readData[i], false);
        }
        Debug.Log("��json��ȡ�ļ����");
    }
    #endregion

}
