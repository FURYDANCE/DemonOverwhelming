using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace DemonOverwhelming
{
    /// <summary>
    /// Õ½¶·¹ÜÀíÆ÷
    /// </summary>
<<<<<<< HEAD
    public class BattleManager : MonoBehaviour
=======
    [Header("½ğÇ®")]
    public float money;
    public float moneyPerSecond;
    public float moneyAddAmount;
    public float nowMoneyCost;
    /// <summary>
    /// ÑªÒº
    /// </summary>
    [Header("ÑªÒº")]
    public float blood;
    public float nowBloodCost;
    [Header("Ñ¡ÔñµÄ±øÅÆ×éºÏ")]
    public List<SoldierCard> soldierCards;
    [Header("²¼ÕóÖĞµÄ±øÖÖ×éºÏs")]
    public List<SoliderGroup> soliderFormatGroups;
    /// <summary>
    /// ´ı¶¨Ñ¡ÔñµÄÊµÌå£¨Êó±êĞüÍ£ÔÚÁËÉÏÃæ£¬µ«»¹Ã»ÓĞ°´ÏÂÑ¡Ôñ¼ü£©
    /// </summary>
    [Header("´ı¶¨Ñ¡ÔñµÄÊµÌå£¨Êó±êĞüÍ£ÔÚÁËÉÏÃæ£¬µ«»¹Ã»ÓĞ°´ÏÂÑ¡Ôñ¼ü£©")]
    public GameObject nowChooseingTarget;
    /// <summary>
    /// ±»Ñ¡ÔñÁËµÄÊµÌå
    /// </summary>
    [Header("ÏÖÔÚÑ¡ÖĞµÄÊµÌå")]
    public GameObject nowChoosedTarget;
    /// <summary>
    /// Õ½¶·½çÃæUI
    /// </summary>
    //[Header("Õ½¶·½çÃæUI")]
    //public GameObject BattleUI;
    [Header("¶ÔÏóĞÅÏ¢ÌáÊ¾¿ò(Ô¤ÖÆ¼ş£©")]
    public UnitInformationUi unitInformationUI;
    //[Header("Ñ¡¿¨½çÃæ")]
    //public Image cardSelectUI;
    //[Header("²¼Õó½çÃæ")]
    //public Image formationMakingUI;
    //[Header("Ó¢ĞÛĞÅÏ¢½çÃæ")]
    //public HeroInformationUI heroInfoUI;
    /// <summary>
    /// µ±Ç°²¼Õó½çÃæÉÏµÄËùÓĞÆìÖÄ
    /// </summary>
    [Header("µ±Ç°²¼Õó½çÃæÉÏµÄËùÓĞÆìÖÄ")]
    public List<FormatCard> formatCards;
    FormatCard lastSelectCard;
    //public SoldierCard lastSelectCard_card;
    [Header("±»´´Ôì³öµÄËùÓĞÊ¿±ø")]
    public List<Entity> allSoldiers;
    UnitInformationUi nowUnitInformationUI;
    int genrateAmount;
    /// <summary>
    /// Ïà»úµÄ¸úËæÄ£Ê½£¨¸úËæUIÖĞµÄsliderÌõ»¹ÊÇ¸úËæÑ¡ÔñµÄÊµÌå£©
    /// </summary>
    [HideInInspector]
    public CameraControlMode cameraControlMode = CameraControlMode.followUi;

    /// <summary>
    /// ´Ó¶ÔÏó¹ÜÀíÆ÷ÖĞÈ¡µÃµÄÏà»ú¸úËæÖá
    /// </summary>
    Transform cameraFollowTarget;
    /// <summary>
    /// Êó±êÊÇ·ñÎ»ÓÚĞÅÏ¢uiÉÏ£¬ÓÃÓÚ¿ØÖÆ°´ÏÂ×ó¼üÊ±ÊÇ·ñÈ¡ÏûĞÅÏ¢uiÏÔÊ¾
    /// </summary>
    public bool mouseOveringInfoUI;
    /// <summary>
    /// Õ½¶·¹ÜÀíÆ÷µ¥Àı
    /// </summary>
    public static BattleManager instance;
    SceneObjectsManager objectManager;
    Coroutine generateMoneyIenumerator;
    /// <summary>
    /// Ìî³äÌõµÄÆğÊ¼ÑÕÉ«
    /// </summary>
    Color fillStartColor;
    //¼ÆËãÌî³äÌõsmoothDampµÄÁ½¸öËÙ¶È±äÁ¿
    float v1;
    float v2;

    public List<string> historySelectedCard;
    public List<Vector3> historyCardPos;
    public List<Vector3> historyShadowPos;
    public List<float> historyX, historyY;
    private void Awake()
>>>>>>> c920aad3 (8.23 ä¿®æ”¹äº†æˆ˜æ–—ç•Œé¢ï¼ŒåŠ å…¥å‡ ä¸ªæ–°å…µç§å¡ï¼ˆç›®å‰å¯ä»¥åŒæ—¶å­˜åœ¨8å¼ å¡ï¼‰ï¼Œä¿®å¤äº†å…µç§ç”Ÿæˆç›¸å…³çš„bug)
    {
        [Header("Ó¢ĞÛ")]
        public Entity hero;
        /// <summary>
        /// ½ğÇ®
        /// </summary>
        [Header("½ğÇ®")]
        public float money;
        public float moneyPerSecond;
        public float moneyAddAmount;
        public float nowMoneyCost;
        /// <summary>
        /// ÑªÒº
        /// </summary>
        [Header("ÑªÒº")]
        public float blood;
        public float nowBloodCost;
        [Header("Ñ¡ÔñµÄ±øÅÆ×éºÏ")]
        public List<SoldierCard> soldierCards;
        [Header("²¼ÕóÖĞµÄ±øÖÖ×éºÏs")]
        public List<SoliderGroup> soliderFormatGroups;
        /// <summary>
        /// ´ı¶¨Ñ¡ÔñµÄÊµÌå£¨Êó±êĞüÍ£ÔÚÁËÉÏÃæ£¬µ«»¹Ã»ÓĞ°´ÏÂÑ¡Ôñ¼ü£©
        /// </summary>
        [Header("´ı¶¨Ñ¡ÔñµÄÊµÌå£¨Êó±êĞüÍ£ÔÚÁËÉÏÃæ£¬µ«»¹Ã»ÓĞ°´ÏÂÑ¡Ôñ¼ü£©")]
        public GameObject nowChooseingTarget;
        /// <summary>
        /// ±»Ñ¡ÔñÁËµÄÊµÌå
        /// </summary>
        [Header("ÏÖÔÚÑ¡ÖĞµÄÊµÌå")]
        public GameObject nowChoosedTarget;
        /// <summary>
        /// Õ½¶·½çÃæUI
        /// </summary>
        //[Header("Õ½¶·½çÃæUI")]
        //public GameObject BattleUI;
        [Header("¶ÔÏóĞÅÏ¢ÌáÊ¾¿ò(Ô¤ÖÆ¼ş£©")]
        public UnitInformationUi unitInformationUI;
        //[Header("Ñ¡¿¨½çÃæ")]
        //public Image cardSelectUI;
        //[Header("²¼Õó½çÃæ")]
        //public Image formationMakingUI;
        //[Header("Ó¢ĞÛĞÅÏ¢½çÃæ")]
        //public HeroInformationUI heroInfoUI;
        /// <summary>
        /// µ±Ç°²¼Õó½çÃæÉÏµÄËùÓĞÆìÖÄ
        /// </summary>
        [Header("µ±Ç°²¼Õó½çÃæÉÏµÄËùÓĞÆìÖÄ")]
        public List<FormatCard> formatCards;
        FormatCard lastSelectCard;
        //public SoldierCard lastSelectCard_card;
        [Header("±»´´Ôì³öµÄËùÓĞÊ¿±ø")]
        public List<Entity> allSoldiers;
        UnitInformationUi nowUnitInformationUI;
        int genrateAmount;
        /// <summary>
        /// Ïà»úµÄ¸úËæÄ£Ê½£¨¸úËæUIÖĞµÄsliderÌõ»¹ÊÇ¸úËæÑ¡ÔñµÄÊµÌå£©
        /// </summary>
        [HideInInspector]
        public CameraControlMode cameraControlMode = CameraControlMode.followUi;

<<<<<<< HEAD
        /// <summary>
        /// ´Ó¶ÔÏó¹ÜÀíÆ÷ÖĞÈ¡µÃµÄÏà»ú¸úËæÖá
        /// </summary>
        Transform cameraFollowTarget;
        /// <summary>
        /// Êó±êÊÇ·ñÎ»ÓÚĞÅÏ¢uiÉÏ£¬ÓÃÓÚ¿ØÖÆ°´ÏÂ×ó¼üÊ±ÊÇ·ñÈ¡ÏûĞÅÏ¢uiÏÔÊ¾
        /// </summary>
        public bool mouseOveringInfoUI;
        /// <summary>
        /// Õ½¶·¹ÜÀíÆ÷µ¥Àı
        /// </summary>
        public static BattleManager instance;
        SceneObjectsManager objectManager;
        Coroutine generateMoneyIenumerator;
        /// <summary>
        /// Ìî³äÌõµÄÆğÊ¼ÑÕÉ«
        /// </summary>
        Color fillStartColor;
        //¼ÆËãÌî³äÌõsmoothDampµÄÁ½¸öËÙ¶È±äÁ¿
        float v1;
        float v2;

        public List<string> historySelectedCard;
        public List<Vector3> historyCardPos;
        public List<Vector3> historyShadowPos;
        public List<float> historyX, historyY;
        private void Awake()
=======
    void Start()
    {
        objectManager = SceneObjectsManager.instance;
        cameraFollowTarget = SceneObjectsManager.instance.GetCameraFollowTarget();
        StartAddMoney();
        //Éú³ÉÓÒÏÂ½ÇµÄ±øÖÖ¿¨
        CreateOneSoldierCard("21000001");
        CreateOneSoldierCard("21000002");
        CreateOneSoldierCard("21000003");
        CreateOneSoldierCard("21000004");
        CreateOneSoldierCard("21000005");
        CreateOneSoldierCard("21000006");
        CreateOneSoldierCard("21000007");
        CreateOneSoldierCard("21000008");
        fillStartColor = SceneObjectsManager.instance.costFill.color;

        //cardSelectUI = GameObject.Find("CardSelectArea").transform.GetComponent<Image>();
        //formationMakingUI = GameObject.Find("FormationMakingArea").transform.GetComponent<Image>();
        //heroInfoUI = GameObject.Find("HeroInfoArea").transform.GetComponent<HeroInformationUI>();


    }


    void Update()
    {
        Money_BoloodShowing();

        //µ±ÓĞÑ¡ÖĞµÄÊµÌåÊ±£¬Ïà»ú¸úËæËùÑ¡ÔñµÄÊµÌå
        if (nowChoosedTarget != null)
            CameraFollow_ByChoosedTarget();


        //²âÊÔ£ºÓĞÕıÔÚÑ¡ÔñµÄ¶ÔÏóÊ±°´ÓÒ¼üÈ·ÈÏÑ¡Ôñ
        if (nowChooseingTarget != null && Input.GetKeyDown(KeyCode.Mouse1))
>>>>>>> c920aad3 (8.23 ä¿®æ”¹äº†æˆ˜æ–—ç•Œé¢ï¼ŒåŠ å…¥å‡ ä¸ªæ–°å…µç§å¡ï¼ˆç›®å‰å¯ä»¥åŒæ—¶å­˜åœ¨8å¼ å¡ï¼‰ï¼Œä¿®å¤äº†å…µç§ç”Ÿæˆç›¸å…³çš„bug)
        {
            if (instance != null)
                Destroy(instance);
            instance = this;
        }

        void Start()
        {
            objectManager = SceneObjectsManager.instance;
            cameraFollowTarget = SceneObjectsManager.instance.GetCameraFollowTarget();
            StartAddMoney();
            //Éú³ÉÓÒÏÂ½ÇµÄ±øÖÖ¿¨
            CreateOneSoldierCard("21000001");
            CreateOneSoldierCard("21000002");
            CreateOneSoldierCard("21000003");
            CreateOneSoldierCard("21000004");
            CreateOneSoldierCard("21000005");
            CreateOneSoldierCard("21000006");
            CreateOneSoldierCard("21000007");
            CreateOneSoldierCard("21000008");
            fillStartColor = SceneObjectsManager.instance.costFill.color;

            //cardSelectUI = GameObject.Find("CardSelectArea").transform.GetComponent<Image>();
            //formationMakingUI = GameObject.Find("FormationMakingArea").transform.GetComponent<Image>();
            //heroInfoUI = GameObject.Find("HeroInfoArea").transform.GetComponent<HeroInformationUI>();


        }


        void Update()
        {
            Money_BoloodShowing();

<<<<<<< HEAD
            //µ±ÓĞÑ¡ÖĞµÄÊµÌåÊ±£¬Ïà»ú¸úËæËùÑ¡ÔñµÄÊµÌå
            if (nowChoosedTarget != null)
                CameraFollow_ByChoosedTarget();
=======
    #region Ó¢ĞÛÏà¹Ø

    /// <summary>
    /// ÉèÖÃÓ¢ĞÛ
    /// </summary>
    /// <param name="hero"></param>
    public void SetHero(Entity hero)
    {
        this.hero = hero;
        SceneObjectsManager.instance.heroInfoUI.SetHero(hero);
    }

    #endregion
>>>>>>> c920aad3 (8.23 ä¿®æ”¹äº†æˆ˜æ–—ç•Œé¢ï¼ŒåŠ å…¥å‡ ä¸ªæ–°å…µç§å¡ï¼ˆç›®å‰å¯ä»¥åŒæ—¶å­˜åœ¨8å¼ å¡ï¼‰ï¼Œä¿®å¤äº†å…µç§ç”Ÿæˆç›¸å…³çš„bug)


            //²âÊÔ£ºÓĞÕıÔÚÑ¡ÔñµÄ¶ÔÏóÊ±°´ÓÒ¼üÈ·ÈÏÑ¡Ôñ
            if (nowChooseingTarget != null && Input.GetKeyDown(KeyCode.Mouse1))
            {
                EnshureChooseTarget();
            }
            //°´×ó¼üÊÍ·Å,Èç¹ûÊó±êÔÚĞÅÏ¢uiÉÏÔò²»ÊÍ·Å
            if (Input.GetKeyDown(KeyCode.Mouse0) && !mouseOveringInfoUI)
            {
                ReleaseChoosedEntity();
                DestoryNowUnitInformation();
            }
            ////Éú³ÉÊµÌå²âÊÔ
            if (Input.GetKeyDown(KeyCode.B))
                GenerateOneHero(Camp.demon, SoldierIds.hero1, SceneObjectsManager.instance.playerEntityGeneratePoint.position);
            //if (Input.GetKeyDown(KeyCode.B))
            //{
            //    CreateSoldierWithGroup(Camp.demon, SoldierIds.lmp, FormationIds.Formation_4Soldiers, true);
            //}
            ////Ó¢ĞÛ¼¼ÄÜ²âÊÔ
            //if (hero && Input.GetKeyDown(KeyCode.Alpha1))
            //{
            //    hero.UseSkill(0);
            //}
            //if (hero && Input.GetKeyDown(KeyCode.Alpha2))
            //{
            //    hero.UseSkill(1);
            //}
        }

        #region Ó¢ĞÛÏà¹Ø

        /// <summary>
        /// ÉèÖÃÓ¢ĞÛ
        /// </summary>
        /// <param name="hero"></param>
        public void SetHero(Entity hero)
        {
            this.hero = hero;
            SceneObjectsManager.instance.heroInfoUI.SetHero(hero);
        }

        #endregion



        #region ½çÃæÏÔÊ¾Ïà¹Ø
        /// <summary>
        /// ÏÔÊ¾½çÃæÉÏµÄ½ğÇ®ÓëÑªÒº£¬ÏÔÊ¾Ìî³äÌõµÄÌî³äĞ§¹û
        /// </summary>
        public void Money_BoloodShowing()
        {
            //ÏÔÊ¾½ğÇ®UI
            objectManager.moneyText.text = nowMoneyCost + "/" + money;
            //ÏÔÊ¾ÑªÒºUI
            objectManager.bloodText.text = nowBloodCost + "/" + blood;
            //ÏÔÊ¾½ğÇ®ºÍUIµÄÌî³ä
            objectManager.costFill.fillAmount = Mathf.SmoothDamp(objectManager.costFill.fillAmount, nowBloodCost / blood, ref v1, 0.25f);
            objectManager.moneyFill.fillAmount = Mathf.SmoothDamp(objectManager.moneyFill.fillAmount, nowMoneyCost / money, ref v2, 0.25f);
            //³¬¹ı×î´óÖµÊ±½«Ìî³äÌõµÄÑÕÉ«±äÎªºìÉ«
            if (nowBloodCost > blood)
                objectManager.costFill.color = Color.red;
            else
                objectManager.costFill.color = fillStartColor;
            if (nowMoneyCost > money)
                objectManager.moneyFill.color = Color.red;
            else
                objectManager.moneyFill.color = fillStartColor;


        }
        #endregion

        #region ÉËº¦Ïà¹Ø
        /// <summary>
        /// ´´½¨Ò»¸öaoeÉËº¦ÇøÓò£¬²ÎÊıÎªÖĞĞÄ×ø±ê£¬¼ì²â·¶Î§£¬´´ÔìÕß£¬ÕóÓª£¬ÉËº¦£¬ÈôÒª´«Èëbuff£¬ÆäbuffµÄid
        /// </summary>
        /// <param name="ÖĞĞÄ×ø±ê"></param>
        /// <param name="¼ì²â·¶Î§"></param>
        /// <param name="´´ÔìÕß"></param>
        /// <param name="ÕóÓª"></param>
        /// <param name="ÉËº¦"></param>
        /// <param name="ÆäbuffµÄid"></param>
        public void CreateAoeHurtArea(Vector3 center, Vector2 checkArea, Entity creater, Camp camp, DamageData damageData)
        {
            //Éú³ÉÌØĞ§
            //GameObject vfx = VfxManager.instance.CreateVfx(VfxManager.instance.vfx_Aoe_2, center, new Vector3(5, 5, 5), 5); //ÌØĞ§´óĞ¡µÈÓĞÁË¸ü¶àÊı¾İºóÔÙ¸ú×ÅĞŞ¸Ä
            //vfx.transform.rotation = Quaternion.Euler(0, 0, 0);
            Collider[] colliders = Physics.OverlapBox(center, new Vector3(checkArea.x, checkArea.y, 50));
            foreach (Collider c in colliders)
            {
                Entity e = c.GetComponent<Entity>();
                if (e)
                {
                    CreateDamage(creater, damageData, e);

                }
            }
        }
        /// <summary>
        /// ÉËº¦·½·¨,Í¬Ê±¸ù¾İÉËº¦ĞÅÏ¢Éú³ÉbuffºÍÌØÊâĞ§¹û£¬ÉËº¦Ö´ĞĞÊ±Ò²»á´¥·¢±»¹¥»÷ÕßµÄÌØÊâ´ÊÌõĞ§¹û£¬²ÎÊıÎªÉËº¦µÄÖÆÔìÕß£¬ÉËº¦ĞÅÏ¢£¬ÉËº¦Ä¿±ê
        /// ·µ»ØÖµÎªÔì³ÉµÄ×îÖÕÉËº¦
        /// </summary>
        /// <param name="ÉËº¦Õß"></param>
        /// <param name="ÉËº¦ĞÅÏ¢"></param>
        /// <param name="±»ÉËº¦Õß"></param>
        public float CreateDamage(Entity creater, DamageData damageData, Entity target)
        {
            if (creater.camp == target.camp)
                return 0;
            float finalDamage;
            //Ê¹ÆäÊÜÉË
            if (target && creater)
            {
                //ÉËº¦Êä³öÕßµÄ¹¥»÷Ç°´ÊÌõÊÂ¼ş
                creater.TagEvent_BeforeAttack(creater, target, damageData, damageData);
                //ÉËº¦³ĞÊÜÕßµÄÊÜÉËÇ°´ÊÌõÊÂ¼ş
                target.TagEvent_BeforeHurt(target, creater, damageData, damageData);

                //Ôì³ÉÉËº¦
                target.TakeDamage(damageData, creater, out finalDamage);

                //ÉËº¦Êä³öÕßµÄ¹¥»÷ºó´ÊÌõÊÂ¼ş
                creater.TagEvent_AfterAttack(creater, finalDamage, target, damageData);
                //ÉËº¦³ĞÊÜÕßµÄÊÜÉËºó´ÊÌõÊÂ¼ş
                target.TagEvent_AfterHurt(target, finalDamage, creater, damageData);
                //buff¼ì²â
                if (damageData.buffs != null)
                    for (int i = 0; i < damageData.buffs.Length; i++)
                    {
                        BuffManager.instance.EntityAddBuff(target, damageData.buffs[i]);
                    }
                return finalDamage;
            }
            return 0;
        }

        #endregion

        #region Í¶ÉäÎïÏà¹Ø

        /// <summary>
        /// ´´½¨Ò»¸öÍ¶ÉäÎï£¬²ÎÊıÎªid£¬×ø±ê,Ä¿±ê
        /// </summary>
        /// <returns></returns>
        public Missile GenerateOneMissle(Entity creater, Vector3 pos, string id, Entity target)
        {
            GameObject go = Instantiate(new GameObject(), GameObject.Find("Missiles").transform);

            if (creater)
            {
                go.transform.position = new Vector3(pos.x, pos.y + creater.GetComponent<BoxCollider>().size.y, SceneObjectsManager.instance.playerEntityGeneratePoint.position.z);

            }
            SpriteRenderer spriteRenderer = go.AddComponent<SpriteRenderer>();

            spriteRenderer.sortingLayerName = "Layer1";
            //spriteRenderer.sortingLayerID = 1;
            go.AddComponent<FaceToCamera>();
            Missile m = go.AddComponent<Missile>();
            m.id = id;
            m.camp = creater.camp;
            m.creater = creater;
            //m.SetParameter(id);
            m.SetTarget(target.transform);
            return m;
        }
        /// <summary>
        /// Éú³ÉÍ¶ÉäÎï£¬²ÎÊıÎªÄ¿±ê¾ßÌå×ø±ê£¬id¡¢Éú³ÉÕóÓªºÍ´´ÔìÕß
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="id"></param>
        /// <param name="camp"></param>
        /// <returns></returns>
        public Missile GenerateOneMissle(Vector3 pos, string id, Camp camp, Entity creater)
        {
            GameObject go = Instantiate(new GameObject(), GameObject.Find("Missiles").transform);
            go.transform.position = pos;
            go.transform.SetParent(GameObject.Find("Missiles").transform);
            SpriteRenderer spriteRenderer = go.AddComponent<SpriteRenderer>();

            spriteRenderer.sortingLayerName = "Layer1";
            //spriteRenderer.sortingLayerID = 1;
            go.AddComponent<FaceToCamera>();
            Missile m = go.AddComponent<Missile>();
            m.creater = creater;
            m.id = id;
            m.camp = camp;
            //m.SetParameter(id);
            m.SetTarget(pos);
            return m;
        }

        #endregion

        #region ÊµÌåÏà¹Ø ´´½¨ÊµÌå£¬Ñ¡ÔñÊµÌå£¬Ïà»ú¸úËæ£¬´´½¨ĞÅÏ¢£¬ÊÍ·ÅÑ¡ÔñµÄÊµÌå

        /// <summary>
        /// ´´½¨Ò»¸öÊµÌå£¬²ÎÊıÎªÕóÓªºÍID
        /// </summary>
        /// <param name="ÕóÓª"></param>
        /// <param name="id"></param>
        public Entity GenerateOneEntity(Camp camp, string id)
        {
            genrateAmount++;
            GameObject go;
            if (GameDataManager.instance.GetEntityDataById(id).name.Split("/").Length == 1)
                go = Instantiate(GameDataManager.instance.emptyEntity, SceneObjectsManager.instance.allUnitParent);
            else
                go = Instantiate(GameDataManager.instance.GetSpinePrefabDataById(id), SceneObjectsManager.instance.allUnitParent);
            Entity e;

            //»ñÈ¡ÊµÌå×é¼ş
            if (!go.GetComponent<Entity>())
                e = go.AddComponent<Entity>();
            else
                e = go.GetComponent<Entity>();
            //e.camp = camp;
            e.parameter = new UnitData();
            e.parameter.SetValue(GameDataManager.instance.GetEntityDataById(id));
            e.GenerateEntity();
            e.name = e.parameter.name + "_" + genrateAmount;
            if (camp == Camp.demon)
            {
                e.camp = Camp.demon;
                e.transform.position = SceneObjectsManager.instance.playerEntityGeneratePoint.transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
            }
            if (camp == Camp.human)
            {
                e.camp = Camp.human;
                e.transform.position = SceneObjectsManager.instance.enemyEntityGeneratePoint.transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
            }
            allSoldiers.Add(e);

            return e;
        }
        /// <summary>
        /// ´´½¨Ò»¸öÊµÌå£¬²ÎÊıÎªÕóÓª£¬IDºÍÉú³É×ø±ê
        /// </summary>
        /// <param name="ÕóÓª"></param>
        /// <param name="id"></param>
        /// <param name="×ø±ê"></param>
        public Entity GenerateOneEntity(Camp camp, string id, Vector3 pos)
        {
            if (GameDataManager.instance.GetEntityDataById(id) == null)
                return null;
            //Éú³É´ÎÊı+1
            genrateAmount++;
            //Éú³É¿Õ¶ÔÏó£¬½«ÆäÄÉÈëFaceToCameraÖĞ
            GameObject go;/* = Instantiate(GameDataManager.instance.emptyEntity, SceneObjectsManager.instance.allUnitParent);*/
            if (GameDataManager.instance.GetEntityDataById(id).name.Split("/").Length == 1)
                go = Instantiate(GameDataManager.instance.emptyEntity, SceneObjectsManager.instance.allUnitParent);
            else
                go = Instantiate(GameDataManager.instance.GetSpinePrefabDataById(id), SceneObjectsManager.instance.allUnitParent);
            //ÉèÖÃ×ø±ê
            go.transform.position = pos;
            //Ìí¼Ó»ò»ñÈ¡ÊµÌå½Å±¾
            Entity e;
            if (!go.GetComponent<Entity>())
                e = go.AddComponent<Entity>();
            else
                e = go.GetComponent<Entity>();
            e.camp = camp;
            //¶ÁÈ¡±äÁ¿
            e.parameter = new UnitData();
            e.parameter.SetValue(GameDataManager.instance.GetEntityDataById(id));
            //Éú³ÉÊµÌå
            e.GenerateEntity();
            //ÉèÖÃÃû³Æ
            e.name = e.parameter.name + "_" + genrateAmount;
            //ÉèÖÃÕóÓª
            if (camp == Camp.demon)
            {
                e.camp = Camp.demon;
            }
            if (camp == Camp.human)
            {
                e.camp = Camp.human;
            }
            allSoldiers.Add(e);

            return e;
        }
        /// <summary>
        /// ´´½¨Ò»¸öÓ¢ĞÛ£¬²ÎÊıÎªÕóÓª£¬IDºÍÉú³É×ø±ê
        /// </summary>
        /// <param name="camp"></param>
        /// <param name="id"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public Entity GenerateOneHero(Camp camp, string id, Vector3 pos)
        {
            if (GameDataManager.instance.GetHeroGameObjectById(id) == null)
                return null;
            //Éú³É´ÎÊı+1
            genrateAmount++;
            //Éú³ÉÓ¢ĞÛ¶ÔÏó£¬½«ÆäÄÉÈëFaceToCameraÖĞ
            GameObject go = Instantiate(GameDataManager.instance.GetHeroGameObjectById(id), SceneObjectsManager.instance.allUnitParent);/* = Instantiate(GameDataManager.instance.emptyEntity, SceneObjectsManager.instance.allUnitParent);*/

            //ÉèÖÃ×ø±ê
            go.transform.position = pos;
            return go.GetComponent<Entity>();
        }
        /// <summary>
        /// µ±ÓĞÑ¡ÖĞµÄÊµÌåÊ±£¬Ïà»ú¿ªÊ¼¸úËæÕâ¸öÊµÌå
        /// </summary>
        public void CameraFollow_ByChoosedTarget()
        {
            //Ö»¸Ä±äÏà»ú¸úËæÄ¿±êµÄXÖá£¬²»¸Ä±äYZ
            cameraFollowTarget.transform.position = new Vector3(nowChoosedTarget.transform.position.x, cameraFollowTarget.transform.position.y, 3);
        }

        /// <summary>
        /// ÒÔ×éÎªµ¥Î»Éú³ÉÊ¿±ø£¬²ÎÊıÎªÕóÓª£¬Ê¿±øIDºÍÕóĞÍID
        /// </summary>
        public void CreateSoldierWithGroup(Camp camp, string soldierId, string formationId, bool destoryShadow)
        {
            //Éú³É´ÎÊı++
            genrateAmount++;
            //¿Õ¶ÔÏó
            GameObject go;
            //¸ù¾İÕóÓªÅĞ¶ÏÉú³ÉÔÚÄÄÒ»±ß
            Vector3 pos = camp == Camp.demon ? SceneObjectsManager.instance.playerEntityGeneratePoint.position : SceneObjectsManager.instance.enemyEntityGeneratePoint.position;
            //Éú³ÉÕóĞÍºÍËæ»úÎ»ÖÃÆ«ÒÆ
            go = Instantiate(GameDataManager.instance.GetFormationById(formationId), SceneObjectsManager.instance.allUnitParent);
            go.transform.position = pos + new Vector3(Random.Range(-5f, 5f), Random.Range(-7f, 7f), 0);
            //»ñÈ¡×é¼ş£¬¸³Öµ×é¼ş
            SoliderGroup sg = go.GetComponent<SoliderGroup>();
            sg.camp = camp;
            Debug.Log("Éú³ÉÊ±µÄÊ¿±øID£º" + soldierId);
            sg.finalSoldierId = soldierId;
            sg.Initialize();
            sg.Generate(destoryShadow);
        }
        public void CreateSoldierWithGroup(Camp camp, string soldierId, string formationId, bool destoryShadow, Vector3 Offset)
        {
            genrateAmount++;
            GameObject go;
            Vector3 pos = camp == Camp.demon ? SceneObjectsManager.instance.playerEntityGeneratePoint.position : SceneObjectsManager.instance.enemyEntityGeneratePoint.position;
            go = Instantiate(GameDataManager.instance.GetFormationById(formationId), SceneObjectsManager.instance.allUnitParent);
            go.transform.position = pos/* + new Vector3(Random.Range(-5f, 5f), Random.Range(-7f, 7f), 0)*/;
            SoliderGroup sg = go.GetComponent<SoliderGroup>();
            sg.camp = camp;
            Debug.Log("Éú³ÉÊ±µÄÊ¿±øID£º" + soldierId);

            sg.finalSoldierId = soldierId;
            sg.transform.position += Offset;
            sg.Generate(destoryShadow);
        }


        #region Êó±êÑ¡ÔñÊµÌåµÄÏà¹Ø·½·¨s

        /// <summary>
        /// È·¶¨Ñ¡ÔñÊó±êËùÖ¸µÄÊµÌå
        /// </summary>
        public void EnshureChooseTarget()
        {
            ChooseOneEntity(nowChooseingTarget);
            CreateInforMationUi();
        }
        /// <summary>
        /// ´´½¨ÊµÌåĞÅÏ¢UI
        /// </summary>
        public void CreateInforMationUi()
        {
            DestoryNowUnitInformation();
            nowUnitInformationUI = GameObject.Instantiate(unitInformationUI, SceneObjectsManager.instance.BattleUI.transform);
            nowUnitInformationUI.SetInformation(nowChoosedTarget.GetComponent<Entity>().parameter, nowChoosedTarget);
        }
        /// <summary>
        /// È¡Ïûµ±Ç°µÄÊµÌåĞÅÏ¢UI
        /// </summary>
        public void DestoryNowUnitInformation()
        {
            if (nowUnitInformationUI != null)
            {
                nowUnitInformationUI.gameObject.SetActive(false);
                Destroy(nowUnitInformationUI);
                nowUnitInformationUI = null;
            }
        }

        /// <summary>
        /// Ñ¡ÔñÊµÌå
        /// 1.¸Ä±äÏà»úµÄ¸úËæÄ¿±êµ½ÊµÌåËùÔÚµÄXÖáÎ»ÖÃ£¬ÇĞ»»UIÏÔÊ¾Ä£Ê½
        /// 2.¸Ä±äÆäspriterenenderµÄ²ÄÖÊÏÔÊ¾
        /// </summary>
        /// <param name="Entity"></param>
        public void ChooseOneEntity(GameObject Entity)
        {
            if (nowChoosedTarget != null)
                ReleaseChoosedEntity();
            nowChoosedTarget = Entity;
            cameraControlMode = CameraControlMode.followTarget;
            if (nowChoosedTarget.GetComponent<SpriteRenderer>())
                nowChoosedTarget.GetComponent<SpriteRenderer>().material = GameDataManager.instance.materialObject.onSelectedMaterial;
        }
        /// <summary>
        /// ÊÍ·ÅÑ¡ÔñµÄÊµÌå
        /// 1.¸Ä±äÏà»ú¸úËæµÄÄ¿±êÎªÄ¬ÈÏ×´Ì¬£¬ÇĞ»»UIÏÔÊ¾Ä£Ê½
        /// 2.½«Ñ¡ÔñµÄÊµÌåµÄ²ÄÖÊ±ä»ØÄ¬ÈÏÄ£Ê½
        /// </summary>
        public void ReleaseChoosedEntity()
        {
            if (nowChoosedTarget == null)
                return;

            nowChoosedTarget.GetComponent<Entity>().SetDefaultMaterial();
            nowChoosedTarget = null;
            cameraControlMode = CameraControlMode.followUi;
        }
        #endregion

        #endregion

        #region ±øÖÖ¿¨Éú³É¡¢Ñ¡Ôñ±øÅÆ¡¢Éú³ÉÊ¿±ø

        /// <summary>
        /// ÔÚUIÓÒÏÂ½Ç´´½¨Ò»¸ö±øÖÖ¿¨£¬²ÎÊıÎªÆäid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SoldierCard CreateOneSoldierCard(string id)
        {
            SoldierCard card = Instantiate(GameDataManager.instance.emptySoldierCard, SceneObjectsManager.instance.BattleUI.transform.Find("BattleUI_Down_Image").transform.Find("CardSelectArea").transform).GetComponent<SoldierCard>();

            card.parameter.id = id;
            card.name = "SoldierCard:" + id;
            soldierCards.Add(card);

            return card;
        }
        /// <summary>
        /// Í¨¹ıid»ñÈ¡µ½´´½¨ÁËµÄ±øÖÖ¿¨
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SoldierCard GetOneSoldierCard(string id)
        {
            foreach (SoldierCard c in soldierCards)
            {
                if (c.parameter.id == id)
                    return c;
            }
            return null;
        }
        /// <summary>
        /// Çå¿ÕÓÒÏÂ½ÇµÄ±øÅÆ
        /// </summary>
        public void ClearSoldierCard()
        {
            foreach (SoldierCard card in soldierCards)
            {
                Destroy(card.gameObject);
            }
            soldierCards.Clear();
        }
<<<<<<< HEAD
        /// <summary>
        /// Ñ¡Ôñ±øÅÆ£¬µ±µã»÷ÓÒÏÂ½ÇµÄ±øÅÆÖ®ºó£¬ÔÚ²¼ÕóÇøÉú³ÉÆìÖÄ£¬´«Èë±øÅÆ£¬ÔÚ±øÅÆÖĞÓĞ¶ÔÓ¦±ø×éµÄÒıÓÃ£¬½«´«¸øÆìÖÄ¶ÔÓ¦µÄ±ø×é
        /// </summary>
        /// <param name="card"></param>
        public FormatCard SelectSoldierCard(SoldierCard card)
=======
        allSoldiers.Add(e);

        return e;
    }

    /// <summary>
    /// µ±ÓĞÑ¡ÖĞµÄÊµÌåÊ±£¬Ïà»ú¿ªÊ¼¸úËæÕâ¸öÊµÌå
    /// </summary>
    public void CameraFollow_ByChoosedTarget()
    {
        //Ö»¸Ä±äÏà»ú¸úËæÄ¿±êµÄXÖá£¬²»¸Ä±äYZ
        cameraFollowTarget.transform.position = new Vector3(nowChoosedTarget.transform.position.x, cameraFollowTarget.transform.position.y, 3);
    }

    /// <summary>
    /// ÒÔ×éÎªµ¥Î»Éú³ÉÊ¿±ø£¬²ÎÊıÎªÕóÓª£¬Ê¿±øIDºÍÕóĞÍID
    /// </summary>
    public void CreateSoldierWithGroup(Camp camp, string soldierId, string formationId, bool destoryShadow)
    {
        //Éú³É´ÎÊı++
        genrateAmount++;
        //¿Õ¶ÔÏó
        GameObject go;
        //¸ù¾İÕóÓªÅĞ¶ÏÉú³ÉÔÚÄÄÒ»±ß
        Vector3 pos = camp == Camp.demon ? SceneObjectsManager.instance.playerEntityGeneratePoint.position : SceneObjectsManager.instance.enemyEntityGeneratePoint.position;
        //Éú³ÉÕóĞÍºÍËæ»úÎ»ÖÃÆ«ÒÆ
        go = Instantiate(GameDataManager.instance.GetFormationById(formationId), GameObject.Find("FaceToCamera").transform);
        go.transform.position = pos + new Vector3(Random.Range(-5f, 5f), Random.Range(-7f, 7f), 0);
        //»ñÈ¡×é¼ş£¬¸³Öµ×é¼ş
        SoliderGroup sg = go.GetComponent<SoliderGroup>();
        sg.camp = camp;
        Debug.Log("Éú³ÉÊ±µÄÊ¿±øID£º" + soldierId);
        sg.finalSoldierId = soldierId;
        sg.Initialize();
        sg.Generate(destoryShadow);
    }
    public void CreateSoldierWithGroup(Camp camp, string soldierId, string formationId, bool destoryShadow, Vector3 Offset)
    {
        genrateAmount++;
        GameObject go;
        Vector3 pos = camp == Camp.demon ? SceneObjectsManager.instance.playerEntityGeneratePoint.position : SceneObjectsManager.instance.enemyEntityGeneratePoint.position;
        go = Instantiate(GameDataManager.instance.GetFormationById(formationId), GameObject.Find("FaceToCamera").transform);
        go.transform.position = pos/* + new Vector3(Random.Range(-5f, 5f), Random.Range(-7f, 7f), 0)*/;
        SoliderGroup sg = go.GetComponent<SoliderGroup>();
        sg.camp = camp;
        Debug.Log("Éú³ÉÊ±µÄÊ¿±øID£º" + soldierId);

        sg.finalSoldierId = soldierId;
        sg.transform.position += Offset;
        sg.Generate(destoryShadow);
    }


    #region Êó±êÑ¡ÔñÊµÌåµÄÏà¹Ø·½·¨s

    /// <summary>
    /// È·¶¨Ñ¡ÔñÊó±êËùÖ¸µÄÊµÌå
    /// </summary>
    public void EnshureChooseTarget()
    {
        ChooseOneEntity(nowChooseingTarget);
        CreateInforMationUi();
    }
    /// <summary>
    /// ´´½¨ÊµÌåĞÅÏ¢UI
    /// </summary>
    public void CreateInforMationUi()
    {
        DestoryNowUnitInformation();
        nowUnitInformationUI = GameObject.Instantiate(unitInformationUI, SceneObjectsManager.instance.BattleUI.transform);
        nowUnitInformationUI.SetInformation(nowChoosedTarget.GetComponent<Entity>().parameter, nowChoosedTarget);
    }
    /// <summary>
    /// È¡Ïûµ±Ç°µÄÊµÌåĞÅÏ¢UI
    /// </summary>
    public void DestoryNowUnitInformation()
    {
        if (nowUnitInformationUI != null)
>>>>>>> c920aad3 (8.23 ä¿®æ”¹äº†æˆ˜æ–—ç•Œé¢ï¼ŒåŠ å…¥å‡ ä¸ªæ–°å…µç§å¡ï¼ˆç›®å‰å¯ä»¥åŒæ—¶å­˜åœ¨8å¼ å¡ï¼‰ï¼Œä¿®å¤äº†å…µç§ç”Ÿæˆç›¸å…³çš„bug)
        {
            Debug.Log("Cost:" + nowBloodCost + "  cardCost:" + card.parameter.bloodCost + "money:" + money + "  cardMoney:" + card.parameter.moneyCost);

            //Ìí¼Ó½ğÇ®ÓëcostÍ³¼Æ
            nowMoneyCost += card.parameter.moneyCost;
            nowBloodCost += card.parameter.bloodCost;
            //ÔÚ²¼ÕóÇøÉú³ÉÆìÖÄ
            FormatCard formatCard = Instantiate(GameDataManager.instance.emptyFormat, SceneObjectsManager.instance.formationMakingUI.transform).GetComponent<FormatCard>();

            formatCard.SetParentParameter(card.parameter);
            //½«±øÅÆ¶ÔÓ¦µÄ±ø×é´«¸øÆìÖÄ
            formatCard.connectedSoldierGroup = card.parameter.content;
            //ÉèÖÃÆìÖÄÌùÍ¼
            formatCard.SetFlagSprite(card.parameter.flagSprite);

            //ÉèÖÃÆìÖÄµÄÍÏ×§·¶Î§
            formatCard.GetComponent<UiDrag>().container = formatCard.transform.parent.gameObject.GetComponent<RectTransform>();
            //½«ÆìÖÄ¼ÓÈëÍ³¼Æ
            formatCards.Add(formatCard);
            //ÉèÖÃµã»÷Éú³ÉµÄ¿¨Îª×îºóµã»÷µÄ¿¨
            SetLastSelectCard(formatCard);

<<<<<<< HEAD
            return formatCard;
=======
    #region ±øÖÖ¿¨Éú³É¡¢Ñ¡Ôñ±øÅÆ¡¢Éú³ÉÊ¿±ø

    /// <summary>
    /// ÔÚUIÓÒÏÂ½Ç´´½¨Ò»¸ö±øÖÖ¿¨£¬²ÎÊıÎªÆäid
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public SoldierCard CreateOneSoldierCard(string id)
    {
        SoldierCard card = Instantiate(GameDataManager.instance.emptySoldierCard, SceneObjectsManager.instance.BattleUI.transform.Find("BattleUI_Down_Image").transform.Find("CardSelectArea").transform).GetComponent<SoldierCard>();

        card.parameter.id = id;
        card.name = "SoldierCard:" + id;
        soldierCards.Add(card);

        return card;
    }
    /// <summary>
    /// Í¨¹ıid»ñÈ¡µ½´´½¨ÁËµÄ±øÖÖ¿¨
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public SoldierCard GetOneSoldierCard(string id)
    {
        foreach (SoldierCard c in soldierCards)
        {
            if (c.parameter.id == id)
                return c;
        }
        return null;
    }
    /// <summary>
    /// Çå¿ÕÓÒÏÂ½ÇµÄ±øÅÆ
    /// </summary>
    public void ClearSoldierCard()
    {
        foreach (SoldierCard card in soldierCards)
        {
            Destroy(card.gameObject);
        }
        soldierCards.Clear();
    }
    /// <summary>
    /// Ñ¡Ôñ±øÅÆ£¬µ±µã»÷ÓÒÏÂ½ÇµÄ±øÅÆÖ®ºó£¬ÔÚ²¼ÕóÇøÉú³ÉÆìÖÄ£¬´«Èë±øÅÆ£¬ÔÚ±øÅÆÖĞÓĞ¶ÔÓ¦±ø×éµÄÒıÓÃ£¬½«´«¸øÆìÖÄ¶ÔÓ¦µÄ±ø×é
    /// </summary>
    /// <param name="card"></param>
    public FormatCard SelectSoldierCard(SoldierCard card)
    {
        Debug.Log("Cost:" + nowBloodCost + "  cardCost:" + card.parameter.bloodCost + "money:" + money + "  cardMoney:" + card.parameter.moneyCost);

        //Ìí¼Ó½ğÇ®ÓëcostÍ³¼Æ
        nowMoneyCost += card.parameter.moneyCost;
        nowBloodCost += card.parameter.bloodCost;
        //ÔÚ²¼ÕóÇøÉú³ÉÆìÖÄ
        FormatCard formatCard = Instantiate(GameDataManager.instance.emptyFormat, SceneObjectsManager.instance.formationMakingUI.transform).GetComponent<FormatCard>();

        formatCard.SetParentParameter(card.parameter);
        //½«±øÅÆ¶ÔÓ¦µÄ±ø×é´«¸øÆìÖÄ
        formatCard.connectedSoldierGroup = card.parameter.content;
        //ÉèÖÃÆìÖÄÌùÍ¼
        formatCard.SetFlagSprite(card.parameter.flagSprite);

        //ÉèÖÃÆìÖÄµÄÍÏ×§·¶Î§
        formatCard.GetComponent<UiDrag>().container = formatCard.transform.parent.gameObject.GetComponent<RectTransform>();
        //½«ÆìÖÄ¼ÓÈëÍ³¼Æ
        formatCards.Add(formatCard);
        //ÉèÖÃµã»÷Éú³ÉµÄ¿¨Îª×îºóµã»÷µÄ¿¨
        SetLastSelectCard(formatCard);

        return formatCard;
    }

    #region ³·Ïú£¬Çå¿ÕÑ¡Ôñ¼ÇÂ¼µÄ·½·¨s

    /// <summary>
    /// Çå¿ÕÆìÖÄ£¬Ç®Ñª¼ÆËã
    /// </summary>
    public void ClearCardSelect()
    {
        //Ñ¡Ôñ¼ÇÂ¼ÎªÁãÖ±½Ó·µ»Ø
        if (formatCards.Count == 0)
            return;
        //±éÀúËùÓĞµÄÑ¡ÔñµÄ²¼Õó¿¨£¬Çå³ıÏÔÊ¾
        foreach (FormatCard c in formatCards)
        {
            c.ClearThis();
            Destroy(c.gameObject);
        }
        //Çå³ı²¼Õó¿¨Ñ¡Ôñ¼ÇÂ¼ºÍ±øÅÆÑ¡Ôñ¼ÇÂ¼
        ClearSelectRecord();
        soliderFormatGroups.Clear();

        //½ğÇ®ÑªÒº¼ÆËã£¬¼ÆËãÍê³Éºó¹éÁã
        //money -= nowMoneyCost;
        //blood -= nowBloodCost;
        //nowMoneyCost = 0;
        ////cost»Ø¹éÎª0
        //nowBloodCost = 0;
        //½«ÉÏ´ÎÑ¡ÔñµÄ¼ÇÂ¼Çå¿Õ
        SetLastSelectCard(null);
    }
    /// <summary>
    /// µã»÷Ë®¾§Çòºó£¬±éÀúµ±Ç°µÄÆìÖÄÉú³É¶ÔÓ¦µÄ±øÖÖ£¬Ö®ºóÈ¡ÏûÆìÖÄ
    /// ¸ü¸Ä£º³¬¹ı×î´óÖµÖ®ºóÈÔÈ»¿ÉÒÔÉú³ÉÆìÖÄ£¬µ«ÊÇ²»ÄÜÉú³É¾ßÌå±ø×é
    /// </summary>
    public void GenerateSoldiers()
    {
        List<SoliderGroup> newGroupList = new List<SoliderGroup>();
        //¼ÆËã½ğÇ®ºÍcostÍ³¼Æ£¬Èô³¬¹ı×î´óÖµÔò²»Éú³É
        if (nowBloodCost > blood || nowMoneyCost > money)
        {
            Debug.Log("³¬¹ı×î´óÖµ");
            return;
        }
        //Éú³É±øÖÖ
        foreach (SoliderGroup sg in soliderFormatGroups)
        {
            //¼ÇÂ¼µ±Ç°½çÃæÉÏµÄ±øÖÖÎ»ÖÃºÍĞÅÏ¢
            SoliderGroup NG = Instantiate(sg.gameObject, sg.transform.parent).GetComponent<SoliderGroup>();
            sg.parentFormatCard.SetConnectGroup(NG);
            newGroupList.Add(NG);


            sg.Generate(false);

        }
        money -= nowMoneyCost;
        blood -= nowBloodCost;
        //Éú³É±øÖÖÖ®ºó£¬³¡ÉÏµÄ²ĞÓ°¾Í»áÏûÊ§£¬¸úËæÉú³ÉÁËµÄ±øÖÖ£¬ĞèÒªÔÙÔ­µØÉú³ÉÒ»ÑùµÄ²ĞÓ°£¬Í¬Ê±ÉèÖÃ¹ØÁª²ĞÓ°µÄ²¼Õó¿¨
        soliderFormatGroups.Clear();
        foreach (SoliderGroup s in newGroupList)
        {
            soliderFormatGroups.Add(s);
        }
        //Debug.Log("µ½ÕâÀïÁË");
        //ReBuildSoldierGroups();

        //Çå¿ÕÆìÖÄ£¬¼ÆËã»¨·Ñ½ğÇ®
        //ClearCardSelect();

        //ReBuildSoldierGroups();
    }
    ///// <summary>
    ///// Éú³É±øÅÆÖ®ºó£¬ÖØĞÂÉèÖÃ³¡ÉÏµÄ±øÖÖ¿¨
    ///// ÓÉÓÚ±øÖÖÉú³Éºó£¬ÒÑ¾­Éú³É¹ıµÄ±ø×é¸¸¶ÔÏó²»ÄÜÔÙ×÷ÓÃµ½ÏÂÒ»²¨Éú³ÉµÄ±ø¶Ó
    ///// ¹ÊĞèÒªÇå³ıÄ¿Ç°µÄ¼ÇÂ¼£¬ÔÙÔÚÏàÍ¬Î»ÖÃÉú³ÉÒ»ÑùµÄĞÂ±øÖÖ¸¸¶ÔÏó£¬ÔÙÍ³¼ÆÕâĞ©ĞÂµÄ¶ÔÏó
    ///// </summary>
    //public void ReBuildSoldierGroups()
    //{
    //    foreach (FormatCard card in formatCards)
    //    {
    //        historySelectedCard.Add(card.parentParameter.id);
    //        historyCardPos.Add(card.transform.position);
    //        historyShadowPos.Add(card.connectedSoldierGroup.transform.position);
    //        historyX.Add(card.x);
    //        historyY.Add(card.y);
    //    }
    //    ClearCardSelect();
    //    for (int i = 0; i < historySelectedCard.Count; i++)
    //    {
    //        string id = historySelectedCard[i];

    //        FormatCard c = SelectSoldierCard(GetOneSoldierCard(id));
    //        c.noOffset = true;
    //        c.transform.position = historyCardPos[i];
    //        c.connectedSoldierGroup.transform.position = historyShadowPos[i];
    //        c.x = historyX[i];
    //        c.y = historyY[i];
    //        c.transform.position -= new Vector3(c.x, c.y);
    //        //c.connectedSoldierGroup.transform.position -= new Vector3(c.x, c.y);
    //    }
    //    historyCardPos.Clear();
    //    historySelectedCard.Clear();
    //    historyShadowPos.Clear();
    //}
    /// <summary>
    /// ÉèÖÃ×îºóµã»÷µÄ±øÅÆ¼ÇÂ¼£¨ÓÃÓÚ³·Ïú£©
    /// </summary>
    /// <param name="card"></param>
    public void SetLastSelectCard(FormatCard card)
    {
        lastSelectCard = card;
    }

    /// <summary>
    /// ³·Ïú×îºóÒ»´ÎÆìÖÄµÄÑ¡Ôñ
    /// </summary>
    public void RevokeCardSelect()
    {
        //Ñ¡Ôñ¼ÇÂ¼ÎªÁãÖ±½Ó·µ»Ø
        if (formatCards.Count == 0)
            return;

        //½ğÇ®ÑªÒº¼ÆËãÍ³¼Æ¼õÈ¥ÉÏ´Îµã»÷µÄ¿¨µÄÊıÖµ
        nowMoneyCost -= lastSelectCard.parentParameter.moneyCost;
        nowBloodCost -= lastSelectCard.parentParameter.bloodCost;


        //²¼Õó¿¨Í³¼ÆÖĞÒÆ³ıÉÏÒ»´Îµã»÷±øÅÆ³öÏÖµÄ²¼Õó¿¨
        formatCards.Remove(lastSelectCard);


        //È¡Ïû¸Ã²¼Õó¿¨ÔÚ³¡¾°ÖĞ´ú±íµÄ¶ÔÏóµÄÏÔÊ¾
        lastSelectCard.ClearThis();


        //´İ»Ù¸Ã²¼Õó¿¨
        Destroy(lastSelectCard.gameObject);


        //µ±count´óÓÚÁãÊ±£¬¼ÌĞø½«¼¯ºÏÖĞÏÂ±ê×î¸ßµÄ¶ÔÏó×÷ÎªÉÏ´Îµã»÷µÄ¶ÔÏó
        if (formatCards.Count > 0)
            SetLastSelectCard(formatCards[formatCards.Count - 1]);
    }


    /// <summary>
    /// ³·ÏúËùÓĞµÄÑ¡Ôñ£¨µ±count²»ÎªÁãÊ±Ñ­»·Ö´ĞĞ³·Ïú·½·¨£©
    /// </summary>
    public void RevokeAllCardSelect()
    {
        while (formatCards.Count > 0)
        {
            RevokeCardSelect();
        }
    }


    /// <summary>
    /// Çå¿ÕÑ¡Ôñ¼ÇÂ¼
    /// </summary>
    void ClearSelectRecord()
    {
        formatCards.Clear();

    }

    #endregion


    #endregion

    #region ½ğÇ®ÑªÒºÏà¹Ø
    /// <summary>
    /// ¿ªÊ¼Ôö¼Ó½ğÇ®£¨ÔÚĞŞ¸Ä½ğÇ®ÊıÖµÖ®ºó»áÖØĞÂ¿ªÆôÒ»¸ö¼ÓÇ®Ğ­³Ì)
    /// </summary>
    public void StartAddMoney()
    {
        //if (generateMoneyIenumerator != null)
        //    StopCoroutine(generateMoneyIenumerator);
        generateMoneyIenumerator = StartCoroutine(addMoney());
    }

    /// <summary>
    /// Ã¿ÃëÔö¼Ó½ğÇ®µÄĞ­³Ì
    /// </summary>
    /// <returns></returns>
    IEnumerator addMoney()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(moneyPerSecond);
            money += moneyAddAmount;
        }
    }
    /// <summary>
    /// Ôö¼ÓÑªÒº
    /// </summary>
    /// <param name="amount"></param>
    public void AddBlood(float amount)
    {
        blood += amount;
    }
    /// <summary>
    /// ÒÔÒ»¶¨ÊıÁ¿Ôö¼Ó½ğÇ®
    /// </summary>
    /// <param name="moneyAmount"></param>
    public void AddMoney(float moneyAmount)
    {
        money += moneyAmount;
        Debug.Log("¼ñÆğÁËÇ®°ü");
    }
    /// <summary>
    /// ´´ÔìÒ»¸öÇ®°ü
    /// </summary>
    /// <returns></returns>
    public GameObject CreateMoneyBag(Vector3 startPos, float moneyAmount)
    {
        GameObject go = Instantiate(GameDataManager.instance.moneyBag, GameObject.Find("FaceToCamera").transform);
        go.name = "Ç®°ü";

        go.transform.position = startPos;
        go.GetComponent<MoneyBag>().moneyAmount = moneyAmount;
        go.GetComponent<ArcMovement>().targetV3 = startPos + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
        return go;
    }
    #endregion


    /// <summary>
    /// ÓÎÏ·½áÊø
    /// </summary>
    /// <param name="playerWin"></param>
    public void EndBattle(bool playerWin)
    {
        if (!SceneObjectsManager.instance.gameOverPanel)
            return;
        if (playerWin)
        {
            SceneObjectsManager.instance.gameOverPanel.gameObject.SetActive(true);
            SceneObjectsManager.instance.gameOverPanel.endText.text = "½©Ê¬³ÔµôÁËÄãµÄÄÔ×Ó \n (²»ÊÇ)";
            return;
        }
        else
        {
            SceneObjectsManager.instance.gameOverPanel.gameObject.SetActive(true);
            SceneObjectsManager.instance.gameOverPanel.endText.text = "Äã³ÔµôÁË½©Ê¬µÄÄÔ×Ó \n (Îí)";
            return;
>>>>>>> c920aad3 (8.23 ä¿®æ”¹äº†æˆ˜æ–—ç•Œé¢ï¼ŒåŠ å…¥å‡ ä¸ªæ–°å…µç§å¡ï¼ˆç›®å‰å¯ä»¥åŒæ—¶å­˜åœ¨8å¼ å¡ï¼‰ï¼Œä¿®å¤äº†å…µç§ç”Ÿæˆç›¸å…³çš„bug)
        }

        #region ³·Ïú£¬Çå¿ÕÑ¡Ôñ¼ÇÂ¼µÄ·½·¨s

        /// <summary>
        /// Çå¿ÕÆìÖÄ£¬Ç®Ñª¼ÆËã
        /// </summary>
        public void ClearCardSelect()
        {
            //Ñ¡Ôñ¼ÇÂ¼ÎªÁãÖ±½Ó·µ»Ø
            if (formatCards.Count == 0)
                return;
            //±éÀúËùÓĞµÄÑ¡ÔñµÄ²¼Õó¿¨£¬Çå³ıÏÔÊ¾
            foreach (FormatCard c in formatCards)
            {
                c.ClearThis();
                Destroy(c.gameObject);
            }
            //Çå³ı²¼Õó¿¨Ñ¡Ôñ¼ÇÂ¼ºÍ±øÅÆÑ¡Ôñ¼ÇÂ¼
            ClearSelectRecord();
            soliderFormatGroups.Clear();

            //½ğÇ®ÑªÒº¼ÆËã£¬¼ÆËãÍê³Éºó¹éÁã
            //money -= nowMoneyCost;
            //blood -= nowBloodCost;
            //nowMoneyCost = 0;
            ////cost»Ø¹éÎª0
            //nowBloodCost = 0;
            //½«ÉÏ´ÎÑ¡ÔñµÄ¼ÇÂ¼Çå¿Õ
            SetLastSelectCard(null);
        }
        /// <summary>
        /// µã»÷Ë®¾§Çòºó£¬±éÀúµ±Ç°µÄÆìÖÄÉú³É¶ÔÓ¦µÄ±øÖÖ£¬Ö®ºóÈ¡ÏûÆìÖÄ
        /// ¸ü¸Ä£º³¬¹ı×î´óÖµÖ®ºóÈÔÈ»¿ÉÒÔÉú³ÉÆìÖÄ£¬µ«ÊÇ²»ÄÜÉú³É¾ßÌå±ø×é
        /// </summary>
        public void GenerateSoldiers()
        {
            List<SoliderGroup> newGroupList = new List<SoliderGroup>();
            //¼ÆËã½ğÇ®ºÍcostÍ³¼Æ£¬Èô³¬¹ı×î´óÖµÔò²»Éú³É
            if (nowBloodCost > blood || nowMoneyCost > money)
            {
                Debug.Log("³¬¹ı×î´óÖµ");
                return;
            }
            //Éú³É±øÖÖ
            foreach (SoliderGroup sg in soliderFormatGroups)
            {
                //¼ÇÂ¼µ±Ç°½çÃæÉÏµÄ±øÖÖÎ»ÖÃºÍĞÅÏ¢
                SoliderGroup NG = Instantiate(sg.gameObject, sg.transform.parent).GetComponent<SoliderGroup>();
                sg.parentFormatCard.SetConnectGroup(NG);
                newGroupList.Add(NG);


                sg.Generate(false);

            }
            money -= nowMoneyCost;
            blood -= nowBloodCost;
            //Éú³É±øÖÖÖ®ºó£¬³¡ÉÏµÄ²ĞÓ°¾Í»áÏûÊ§£¬¸úËæÉú³ÉÁËµÄ±øÖÖ£¬ĞèÒªÔÙÔ­µØÉú³ÉÒ»ÑùµÄ²ĞÓ°£¬Í¬Ê±ÉèÖÃ¹ØÁª²ĞÓ°µÄ²¼Õó¿¨
            soliderFormatGroups.Clear();
            foreach (SoliderGroup s in newGroupList)
            {
                soliderFormatGroups.Add(s);
            }
            //Debug.Log("µ½ÕâÀïÁË");
            //ReBuildSoldierGroups();

            //Çå¿ÕÆìÖÄ£¬¼ÆËã»¨·Ñ½ğÇ®
            //ClearCardSelect();

            //ReBuildSoldierGroups();
        }
        ///// <summary>
        ///// Éú³É±øÅÆÖ®ºó£¬ÖØĞÂÉèÖÃ³¡ÉÏµÄ±øÖÖ¿¨
        ///// ÓÉÓÚ±øÖÖÉú³Éºó£¬ÒÑ¾­Éú³É¹ıµÄ±ø×é¸¸¶ÔÏó²»ÄÜÔÙ×÷ÓÃµ½ÏÂÒ»²¨Éú³ÉµÄ±ø¶Ó
        ///// ¹ÊĞèÒªÇå³ıÄ¿Ç°µÄ¼ÇÂ¼£¬ÔÙÔÚÏàÍ¬Î»ÖÃÉú³ÉÒ»ÑùµÄĞÂ±øÖÖ¸¸¶ÔÏó£¬ÔÙÍ³¼ÆÕâĞ©ĞÂµÄ¶ÔÏó
        ///// </summary>
        //public void ReBuildSoldierGroups()
        //{
        //    foreach (FormatCard card in formatCards)
        //    {
        //        historySelectedCard.Add(card.parentParameter.id);
        //        historyCardPos.Add(card.transform.position);
        //        historyShadowPos.Add(card.connectedSoldierGroup.transform.position);
        //        historyX.Add(card.x);
        //        historyY.Add(card.y);
        //    }
        //    ClearCardSelect();
        //    for (int i = 0; i < historySelectedCard.Count; i++)
        //    {
        //        string id = historySelectedCard[i];

        //        FormatCard c = SelectSoldierCard(GetOneSoldierCard(id));
        //        c.noOffset = true;
        //        c.transform.position = historyCardPos[i];
        //        c.connectedSoldierGroup.transform.position = historyShadowPos[i];
        //        c.x = historyX[i];
        //        c.y = historyY[i];
        //        c.transform.position -= new Vector3(c.x, c.y);
        //        //c.connectedSoldierGroup.transform.position -= new Vector3(c.x, c.y);
        //    }
        //    historyCardPos.Clear();
        //    historySelectedCard.Clear();
        //    historyShadowPos.Clear();
        //}
        /// <summary>
        /// ÉèÖÃ×îºóµã»÷µÄ±øÅÆ¼ÇÂ¼£¨ÓÃÓÚ³·Ïú£©
        /// </summary>
        /// <param name="card"></param>
        public void SetLastSelectCard(FormatCard card)
        {
            lastSelectCard = card;
        }

        /// <summary>
        /// ³·Ïú×îºóÒ»´ÎÆìÖÄµÄÑ¡Ôñ
        /// </summary>
        public void RevokeCardSelect()
        {
            //Ñ¡Ôñ¼ÇÂ¼ÎªÁãÖ±½Ó·µ»Ø
            if (formatCards.Count == 0)
                return;

            //½ğÇ®ÑªÒº¼ÆËãÍ³¼Æ¼õÈ¥ÉÏ´Îµã»÷µÄ¿¨µÄÊıÖµ
            nowMoneyCost -= lastSelectCard.parentParameter.moneyCost;
            nowBloodCost -= lastSelectCard.parentParameter.bloodCost;


            //²¼Õó¿¨Í³¼ÆÖĞÒÆ³ıÉÏÒ»´Îµã»÷±øÅÆ³öÏÖµÄ²¼Õó¿¨
            formatCards.Remove(lastSelectCard);


            //È¡Ïû¸Ã²¼Õó¿¨ÔÚ³¡¾°ÖĞ´ú±íµÄ¶ÔÏóµÄÏÔÊ¾
            lastSelectCard.ClearThis();


            //´İ»Ù¸Ã²¼Õó¿¨
            Destroy(lastSelectCard.gameObject);


            //µ±count´óÓÚÁãÊ±£¬¼ÌĞø½«¼¯ºÏÖĞÏÂ±ê×î¸ßµÄ¶ÔÏó×÷ÎªÉÏ´Îµã»÷µÄ¶ÔÏó
            if (formatCards.Count > 0)
                SetLastSelectCard(formatCards[formatCards.Count - 1]);
        }


        /// <summary>
        /// ³·ÏúËùÓĞµÄÑ¡Ôñ£¨µ±count²»ÎªÁãÊ±Ñ­»·Ö´ĞĞ³·Ïú·½·¨£©
        /// </summary>
        public void RevokeAllCardSelect()
        {
            while (formatCards.Count > 0)
            {
                RevokeCardSelect();
            }
        }


        /// <summary>
        /// Çå¿ÕÑ¡Ôñ¼ÇÂ¼
        /// </summary>
        void ClearSelectRecord()
        {
            formatCards.Clear();

        }

        #endregion


        #endregion

        #region ½ğÇ®ÑªÒºÏà¹Ø
        /// <summary>
        /// ¿ªÊ¼Ôö¼Ó½ğÇ®£¨ÔÚĞŞ¸Ä½ğÇ®ÊıÖµÖ®ºó»áÖØĞÂ¿ªÆôÒ»¸ö¼ÓÇ®Ğ­³Ì)
        /// </summary>
        public void StartAddMoney()
        {
            //if (generateMoneyIenumerator != null)
            //    StopCoroutine(generateMoneyIenumerator);
            generateMoneyIenumerator = StartCoroutine(addMoney());
        }

        /// <summary>
        /// Ã¿ÃëÔö¼Ó½ğÇ®µÄĞ­³Ì
        /// </summary>
        /// <returns></returns>
        IEnumerator addMoney()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(moneyPerSecond);
                money += moneyAddAmount;
            }
        }
        /// <summary>
        /// Ôö¼ÓÑªÒº
        /// </summary>
        /// <param name="amount"></param>
        public void AddBlood(float amount)
        {
            blood += amount;
        }
        /// <summary>
        /// ÒÔÒ»¶¨ÊıÁ¿Ôö¼Ó½ğÇ®
        /// </summary>
        /// <param name="moneyAmount"></param>
        public void AddMoney(float moneyAmount)
        {
            money += moneyAmount;
            Debug.Log("¼ñÆğÁËÇ®°ü");
        }
        /// <summary>
        /// ´´ÔìÒ»¸öÇ®°ü
        /// </summary>
        /// <returns></returns>
        public GameObject CreateMoneyBag(Vector3 startPos, float moneyAmount)
        {
            GameObject go = Instantiate(GameDataManager.instance.moneyBag, SceneObjectsManager.instance.allUnitParent);
            go.name = "Ç®°ü";

            go.transform.position = startPos;
            go.GetComponent<MoneyBag>().moneyAmount = moneyAmount;
            go.GetComponent<ArcMovement>().targetV3 = startPos + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
            return go;
        }
        #endregion


        /// <summary>
        /// ÓÎÏ·½áÊø
        /// </summary>
        /// <param name="playerWin"></param>
        public void EndBattle(bool playerWin)
        {
            if (!SceneObjectsManager.instance.gameOverPanel)
                return;
            if (playerWin)
            {
                SceneObjectsManager.instance.gameOverPanel.gameObject.SetActive(true);
                SceneObjectsManager.instance.gameOverPanel.endText.text = "½©Ê¬³ÔµôÁËÄãµÄÄÔ×Ó \n (²»ÊÇ)";
                return;
            }
            else
            {
                SceneObjectsManager.instance.gameOverPanel.gameObject.SetActive(true);
                SceneObjectsManager.instance.gameOverPanel.endText.text = "Äã³ÔµôÁË½©Ê¬µÄÄÔ×Ó \n (Îí)";
                return;
            }

        }


        #region ÏÔÊ¾ĞÅÏ¢Ïà¹Ø
        public GameObject CreateSceneInformation(GameObject creater, Sprite sprite, string info)
        {
            GameObject go = Instantiate(GameDataManager.instance.sceneInformation);
            go.transform.position = creater.transform.position + new Vector3(-4, 0.5f, 0);
            go.GetComponent<SceneInformation>().SetInformation(sprite, info);
            return go;
        }
        public GameObject CreateSceneInformation(GameObject creater, Sprite sprite, string info, bool seePanel)
        {
            GameObject go = Instantiate(GameDataManager.instance.sceneInformation);
            go.transform.position = creater.transform.position + new Vector3(-4, 0.5f, 0);
            SceneInformation s = go.GetComponent<SceneInformation>();
            s.SetInformation(sprite, info);
            if (!seePanel)
                s.panel.color = new Color(1, 1, 1, 0);
            return go;
        }
        public GameObject CreateSceneInformation(GameObject creater, Sprite sprite, string info, bool seePanel, Color textColor)
        {
            GameObject go = Instantiate(GameDataManager.instance.sceneInformation);
            go.transform.position = creater.transform.position + new Vector3(-4, 0.5f, 0);
            SceneInformation s = go.GetComponent<SceneInformation>();
            s.SetInformation(sprite, info);
            if (!seePanel)
                s.panel.color = new Color(1, 1, 1, 0);
            s.text.color = textColor;
            return go;
        }




        #endregion
    }
<<<<<<< HEAD
}
=======


    #region ÏÔÊ¾ĞÅÏ¢Ïà¹Ø
    public GameObject CreateSceneInformation(GameObject creater, Sprite sprite, string info)
    {
        GameObject go = Instantiate(GameDataManager.instance.sceneInformation);
        go.transform.position = creater.transform.position + new Vector3(-4, 0.5f, 0);
        go.GetComponent<SceneInformation>().SetInformation(sprite, info);
        return go;
    }
    public GameObject CreateSceneInformation(GameObject creater, Sprite sprite, string info, bool seePanel)
    {
        GameObject go = Instantiate(GameDataManager.instance.sceneInformation);
        go.transform.position = creater.transform.position + new Vector3(-4, 0.5f, 0);
        SceneInformation s = go.GetComponent<SceneInformation>();
        s.SetInformation(sprite, info);
        if (!seePanel)
            s.panel.color = new Color(1, 1, 1, 0);
        return go;
    }
    public GameObject CreateSceneInformation(GameObject creater, Sprite sprite, string info, bool seePanel, Color textColor)
    {
        GameObject go = Instantiate(GameDataManager.instance.sceneInformation);
        go.transform.position = creater.transform.position + new Vector3(-4, 0.5f, 0);
        SceneInformation s = go.GetComponent<SceneInformation>();
        s.SetInformation(sprite, info);
        if (!seePanel)
            s.panel.color = new Color(1, 1, 1, 0);
        s.text.color = textColor;
        return go;
    }




    #endregion
}
>>>>>>> c920aad3 (8.23 ä¿®æ”¹äº†æˆ˜æ–—ç•Œé¢ï¼ŒåŠ å…¥å‡ ä¸ªæ–°å…µç§å¡ï¼ˆç›®å‰å¯ä»¥åŒæ—¶å­˜åœ¨8å¼ å¡ï¼‰ï¼Œä¿®å¤äº†å…µç§ç”Ÿæˆç›¸å…³çš„bug)
