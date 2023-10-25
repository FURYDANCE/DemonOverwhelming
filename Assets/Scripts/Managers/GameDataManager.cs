using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace DemonOverwhelming
{
    /// <summary>
    /// ´æ·ÅÏîÄ¿ÎÄ¼şÖĞĞèÒª±»µ÷ÓÃµÄ¶ÔÏó,°üÀ¨³õÊ¼»¯ÎÄ¼ş
    /// </summary>
    public class GameDataManager : MonoBehaviour
    {
        /// <summary>
        /// Í¨¹ıexcel±í±£´æµÄÊı¾İ×ÜºÍ
        /// </summary>
        [Header("Í¨¹ıexcel±í±£´æµÄÊı¾İ×ÜºÍ")]
        public ExcelDataManager excelDatas;
        [Header("¶¯»­Êı¾İ¼¯ºÏ")]
        public AnimationsObject animationData;
        [Header("¿ÉÄÜ±»µ÷ÓÃµÄ²ÄÖÊµÄÊı¾İ¼¯ºÏ")]
        public MaterialObject materialObject;
        [Header("±øÖÖ×éµÄ¼¯ºÏ")]
        public FormationObject formationObject;
        [Header("spineÔ¤ÖÆ¼şÊı¾İ¼¯ºÏ")]
        public SpinePrefabObject spinePrefabDatas;
        [Header("Ó¢ĞÛ¶ÔÏó¼¯ºÏ")]
        public HeroDataObject heroDatas;
        /// <summary>
        /// ½«Éú³ÉµÄ¿ÕÊµÌå
        /// </summary>
        [Header("½«Éú³ÉµÄ¿ÕÊµÌå")]
        public GameObject emptyEntity;
        [Header("½«Éú³ÉµÄ¿ÕspineÊµÌå")]
        public GameObject emptySpine;
        [Header("¿ÕµÄÑ¡Ôñ¿¨")]
        public GameObject emptySoldierCard;
        [Header("¿ÕµÄ²¼Õó¿¨")]
        public GameObject emptyFormat;
        [Header("Ç®°ü")]
        public GameObject moneyBag;
        [Header("³¡¾°ÄÚµÄÌáÊ¾ĞÅÏ¢")]
        public GameObject sceneInformation;
        public GameObject a;
        public Sprite bloodSprite;
        public static GameDataManager instance;

<<<<<<< HEAD
        private void Awake()
=======
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
    /// ³õÊ¼»¯
    /// </summary>
    void Initialization()
    {
        //Èç¹û´æµµÎÄ¼ş²»´æÔÚ£¬ÔòÏÈ´´½¨´æµµÎÄ¼ş
        if (!File.Exists(Application.persistentDataPath + "/" + JsonEditor.unitFileName + ".json"))
            SaveJson(Application.persistentDataPath, JsonEditor.unitFileName, excelDatas.unitDatas);
        if (!File.Exists(Application.persistentDataPath + "/" + JsonEditor.cardFileName + ".json"))
            SaveJson(Application.persistentDataPath, JsonEditor.cardFileName, excelDatas.cardDatas);
        if (!File.Exists(Application.persistentDataPath + "/" + JsonEditor.missileFileName + ".json"))
            SaveJson(Application.persistentDataPath, JsonEditor.missileFileName, excelDatas.missileDatas);
        //¶ÁÈ¡´æµµÎÄ¼ş
        ReadJson_Unit();
        ReadJson_Card();
        ReadJson_Missile();
    }
    #region Í¨¹ıid»ñÈ¡¸÷¸öÄÚÈİ
    /// <summary>
    /// Í¨¹ıid»ñÈ¡¶Ô»°ÄÚÈİ
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public PlotsData GetPoltById(int id)
    {
        PlotsData find = excelDatas.plotsData.Find((PlotsData p) => { return p.id == id; });
        return find;
    }
    /// <summary>
    /// Í¨¹ıid»ñÈ¡ÊµÌåĞÅÏ¢
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public UnitParameter GetEntityDataById(string id)
    {
        UnitParameter find = excelDatas.unitDatas.Find((UnitParameter p) => { return p.ID == id; });
        return find;
    }
    /// <summary>
    /// Í¨¹ıid»ñÈ¡±øÅÆĞÅÏ¢
    /// ²¢ÇÒÕÒµ½idËù¶ÔÓ¦µÄ±øÖÖ×é
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public SoldierCardParameter GetSoldierCardById(string id)
    {
        SoldierCardParameter find = excelDatas.cardDatas.Find((SoldierCardParameter p) => { return p.id == id; });
        find.content = formationObject.formations.Find((Formation p) => { return p.id == find.formationId; }).contentFormation.GetComponent<SoliderGroup>();
        find.content.finalSoldierId = find.soldierId;
        Debug.Log("FINDµ½µÄID==" + find.soldierId);
        return find;
    }
    /// <summary>
    /// Í¨¹ıid»ñÈ¡Í¶ÉäÎïĞÅÏ¢
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public UnitParameter_Missile GetMissileDataById(string id)
    {
        UnitParameter_Missile find = excelDatas.missileDatas.Find((UnitParameter_Missile p) => { return p.id == id; });
        return find;
    }
    /// <summary>
    /// Í¨¹ıid»ñÈ¡¶¯»­ĞÅÏ¢
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
    /// Í¨¹ıid»ñÈ¡ÉËº¦ĞÅÏ¢
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
    /// Í¨¹ıid»ñÈ¡ÕóĞÍĞÅÏ¢
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
    /// Í¨¹ıid»ñÈ¡¼¼ÄÜĞÅÏ¢
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

    #region Êı¾İµÄ¶Á´æ
    /// <summary>
    /// ±£´æÊı¾İÎÄ¼ş
    /// </summary>
    public void SaveJson(string path, string fileName, object o)
    {
        JsonEditor.WritingJson(path, fileName, o);
        Debug.Log("½«ÎÄ¼şĞ´ÈëjsonÍê³É");
    }
    /// <summary>
    /// ´ÓjsonÖĞ¶ÁÈ¡Êı¾İÎÄ¼ş
    /// </summary>
    public void ReadJson_Unit()
    {

        //ÏÈ»ñÈ¡¶Áµ½µÄjson
        List<UnitParameter> readData = JsonEditor.ReadingJson(Application.persistentDataPath, JsonEditor.unitFileName, excelDatas.unitDatas);
        //±éÀú±¾µØÊı¾İ²¢ÇÒ´æ½øÈ¥
        for (int i = 0; i < readData.Count; i++)
>>>>>>> c920aad3 (8.23 ä¿®æ”¹äº†æˆ˜æ–—ç•Œé¢ï¼ŒåŠ å…¥å‡ ä¸ªæ–°å…µç§å¡ï¼ˆç›®å‰å¯ä»¥åŒæ—¶å­˜åœ¨8å¼ å¡ï¼‰ï¼Œä¿®å¤äº†å…µç§ç”Ÿæˆç›¸å…³çš„bug)
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
        /// ³õÊ¼»¯
        /// </summary>
        void Initialization()
        {
            //Èç¹û´æµµÎÄ¼ş²»´æÔÚ£¬ÔòÏÈ´´½¨´æµµÎÄ¼ş
            if (!File.Exists(Application.persistentDataPath + "/" + JsonEditor.unitFileName + ".json"))
                SaveJson(Application.persistentDataPath, JsonEditor.unitFileName, excelDatas.unitDatas);
            if (!File.Exists(Application.persistentDataPath + "/" + JsonEditor.cardFileName + ".json"))
                SaveJson(Application.persistentDataPath, JsonEditor.cardFileName, excelDatas.cardDatas);
            if (!File.Exists(Application.persistentDataPath + "/" + JsonEditor.missileFileName + ".json"))
                SaveJson(Application.persistentDataPath, JsonEditor.missileFileName, excelDatas.missileDatas);
            //¶ÁÈ¡´æµµÎÄ¼ş
            ReadJson_Unit();
            ReadJson_Card();
            ReadJson_Missile();
        }
        #region Í¨¹ıid»ñÈ¡¸÷¸öÄÚÈİ
        /// <summary>
        /// Í¨¹ıid»ñÈ¡¶Ô»°ÄÚÈİ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PlotsData GetPoltById(int id)
        {
            PlotsData find = excelDatas.plotsData.Find((PlotsData p) => { return p.id == id; });
            return find;
        }
        /// <summary>
        /// Í¨¹ıid»ñÈ¡ÊµÌåĞÅÏ¢
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UnitData GetEntityDataById(string id)
        {
            UnitData find = excelDatas.unitDatas.Find((UnitData p) => { return p.ID == id; });
            return find;
        }
        /// <summary>
        /// Í¨¹ıid»ñÈ¡Ó¢ĞÛµÄÓÎÏ·¶ÔÏó
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GameObject GetHeroGameObjectById(string id)
        {
            GameObject go = heroDatas.heroDatas.Find((HeroData hd) => hd.id == id).hero;
            return go;
        }
        /// <summary>
        /// Í¨¹ıid»ñÈ¡±øÅÆĞÅÏ¢
        /// ²¢ÇÒÕÒµ½idËù¶ÔÓ¦µÄ±øÖÖ×é
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SoldierCardParameter GetSoldierCardById(string id)
        {
            SoldierCardParameter find = excelDatas.cardDatas.Find((SoldierCardParameter p) => { return p.id == id; });
            find.content = formationObject.formations.Find((Formation p) => { return p.id == find.formationId; }).contentFormation.GetComponent<SoliderGroup>();
            find.content.finalSoldierId = find.soldierId;
            Debug.Log("FINDµ½µÄID==" + find.soldierId);
            return find;
        }
        /// <summary>
        /// Í¨¹ıid»ñÈ¡Í¶ÉäÎïĞÅÏ¢
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UnitParameter_Missile GetMissileDataById(string id)
        {
            UnitParameter_Missile find = excelDatas.missileDatas.Find((UnitParameter_Missile p) => { return p.id == id; });
            return find;
        }
        /// <summary>
        /// Í¨¹ıid»ñÈ¡¶¯»­ĞÅÏ¢
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
        /// Í¨¹ıid»ñÈ¡ÉËº¦ĞÅÏ¢
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
        /// Í¨¹ıid»ñÈ¡ÕóĞÍĞÅÏ¢
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
        /// Í¨¹ıid»ñÈ¡¼¼ÄÜĞÅÏ¢
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Skill GetSkillById(string id)
        {
            Skill s = excelDatas.skillDatas.Find((Skill p) => { return p.id == id; });
            Skill skill = new Skill();
            if (s == null)
                return null;
            skill.SetValue(s);
            return skill;
        }
        #endregion

        #region Êı¾İµÄ¶Á´æ
        /// <summary>
        /// ±£´æÊı¾İÎÄ¼ş
        /// </summary>
        public void SaveJson(string path, string fileName, object o)
        {
            JsonEditor.WritingJson(path, fileName, o);
            Debug.Log("½«ÎÄ¼şĞ´ÈëjsonÍê³É");
        }
        /// <summary>
        /// ´ÓjsonÖĞ¶ÁÈ¡Êı¾İÎÄ¼ş
        /// </summary>
        public void ReadJson_Unit()
        {

            //ÏÈ»ñÈ¡¶Áµ½µÄjson
            List<UnitData> readData = JsonEditor.ReadingJson(Application.persistentDataPath, JsonEditor.unitFileName, excelDatas.unitDatas);
            //±éÀú±¾µØÊı¾İ²¢ÇÒ´æ½øÈ¥
            for (int i = 0; i < readData.Count; i++)
            {
                //Debug.Log(readData[i].ID);
                excelDatas.unitDatas[i].SetValue_noSprite(readData[i]);
            }
            Debug.Log("´Ójson¶ÁÈ¡ÎÄ¼şÍê³É");
        }
        public void ReadJson_Card()
        {
            //ÏÈ»ñÈ¡¶Áµ½µÄjson
            List<SoldierCardParameter> readData = JsonEditor.ReadingJson(Application.persistentDataPath, JsonEditor.cardFileName, excelDatas.cardDatas);
            //±éÀú±¾µØÊı¾İ²¢ÇÒ´æ½øÈ¥
            for (int i = 0; i < readData.Count; i++)
            {
                excelDatas.cardDatas[i].SetValue(readData[i]);
            }
            Debug.Log("´Ójson¶ÁÈ¡ÎÄ¼şÍê³É");
        }
        public void ReadJson_Missile()
        {
            //ÏÈ»ñÈ¡¶Áµ½µÄjson
            List<UnitParameter_Missile> readData = JsonEditor.ReadingJson(Application.persistentDataPath, JsonEditor.missileFileName, excelDatas.missileDatas);
            //±éÀú±¾µØÊı¾İ²¢ÇÒ´æ½øÈ¥
            for (int i = 0; i < readData.Count; i++)
            {
                excelDatas.missileDatas[i].SetValue(readData[i], false);
            }
            Debug.Log("´Ójson¶ÁÈ¡ÎÄ¼şÍê³É");
        }
        #endregion

    }
}