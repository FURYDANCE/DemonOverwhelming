using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace DemonOverwhelming
{
    /// <summary>
    /// ս��������
    /// </summary>
    public class BattleManager : MonoBehaviour
    {
        [Header("Ϊdemo��ʱ��ӵģ��ؿ����")]
        public int levelId;
        [Header("Ӣ��")]
        public Entity hero;
        /// <summary>
        /// ��Ǯ
        /// </summary>
        [Header("��Ǯ")]
        public float money;
        public float moneyPerSecond;
        public float moneyAddAmount;
        public float nowMoneyCost;
        /// <summary>
        /// ѪҺ
        /// </summary>
        [Header("ѪҺ")]
        public float blood;
        public float nowBloodCost;
        [Header("ѡ��ı������")]
        public List<SoldierCard> soldierCards;

        [Header("Ϊdemo��ʱ��ӣ�ȡ����λѡ��")]
        public bool debug_DontChosseEntity;
        /// <summary>
        /// ����ѡ���ʵ�壨�����ͣ�������棬����û�а���ѡ�����
        /// </summary>
        [Header("����ѡ���ʵ�壨�����ͣ�������棬����û�а���ѡ�����")]
        public GameObject nowChooseingTarget;
        /// <summary>
        /// ��ѡ���˵�ʵ��
        /// </summary>
        [Header("����ѡ�е�ʵ��")]
        public GameObject nowChoosedTarget;
        /// <summary>
        /// ս������UI
        /// </summary>
        //[Header("ս������UI")]
        //public GameObject BattleUI;
        [Header("������Ϣ��ʾ��(Ԥ�Ƽ���")]
        public UnitInformationUi unitInformationUI;
        //[Header("ѡ������")]
        //public Image cardSelectUI;
        //[Header("�������")]
        //public Image formationMakingUI;
        //[Header("Ӣ����Ϣ����")]
        //public HeroInformationUI heroInfoUI;
        /// <summary>
        /// ��ǰ��������ϵ���������
        /// </summary>
        [Header("��ǰ��������ϵ���������")]
        public List<FormatCard> formatCards;
        FormatCard lastSelectCard;
        //public SoldierCard lastSelectCard_card;
        [Header("�������������ʿ��")]
        public List<Entity> allSoldiers;
        UnitInformationUi nowUnitInformationUI;
        int genrateAmount;
        /// <summary>
        /// ����ĸ���ģʽ������UI�е�slider�����Ǹ���ѡ���ʵ�壩
        /// </summary>
        [HideInInspector]
        public CameraControlMode cameraControlMode = CameraControlMode.followUi;

        /// <summary>
        /// �Ӷ����������ȡ�õ����������
        /// </summary>
        Transform cameraFollowTarget;
        /// <summary>
        /// ����Ƿ�λ����Ϣui�ϣ����ڿ��ư������ʱ�Ƿ�ȡ����Ϣui��ʾ
        /// </summary>
        public bool mouseOveringInfoUI;
        /// <summary>
        /// ս������������
        /// </summary>
        public static BattleManager instance;
        SceneObjectsManager objectManager;
        Coroutine generateMoneyIenumerator;
        /// <summary>
        /// ���������ʼ��ɫ
        /// </summary>
        Color fillStartColor;
        //���������smoothDamp�������ٶȱ���
        float v1;
        float v2;

        public List<string> historySelectedCard;
        public List<Vector3> historyCardPos;
        public List<Vector3> historyShadowPos;
        public List<float> historyX, historyY;

        /// <summary>
        /// ����ѡ���˵ı�������
        /// </summary>
        public List<FormatCard> allSelectedCards;

        private void Awake()
        {
            if (instance != null)
                Destroy(instance);
            instance = this;
        }

        void Start()
        {
            //objectManager = SceneObjectsManager.instance;
            //cameraFollowTarget = SceneObjectsManager.instance.GetCameraFollowTarget();
            //StartAddMoney();
            //�������½ǵı��ֿ�
            if (levelId == 1)
            {
                CreateOneSoldierCard("21000001");
                CreateOneSoldierCard("21000002");
                CreateOneSoldierCard("21000007");
                CreateOneSoldierCard("21000004");
                CreateOneSoldierCard("21000008");
            }
            if (levelId == 2)
            {
                CreateOneSoldierCard("21000001");
                CreateOneSoldierCard("21000007");
                CreateOneSoldierCard("21000009");
                CreateOneSoldierCard("21000013");
                CreateOneSoldierCard("21000021");


            }
            //CreateOneSoldierCard("21000005");
            //CreateOneSoldierCard("21000009");
            //CreateOneSoldierCard("21000007");
            //CreateOneSoldierCard("21000008");



            //fillStartColor = SceneObjectsManager.instance.costFill.color;

            //cardSelectUI = GameObject.Find("CardSelectArea").transform.GetComponent<Image>();
            //formationMakingUI = GameObject.Find("FormationMakingArea").transform.GetComponent<Image>();
            //heroInfoUI = GameObject.Find("HeroInfoArea").transform.GetComponent<HeroInformationUI>();


        }


        void Update()
        {
            //Money_BoloodShowing();

            ////����ѡ�е�ʵ��ʱ�����������ѡ���ʵ��
            //if (nowChoosedTarget != null)
            //    CameraFollow_ByChoosedTarget();


            ////���ԣ�������ѡ��Ķ���ʱ���Ҽ�ȷ��ѡ��
            //if (!debug_DontChosseEntity && nowChooseingTarget != null && Input.GetKeyDown(KeyCode.Mouse1))
            //{

            //    EnshureChooseTarget();
            //}
            ////������ͷ�,����������Ϣui�����ͷ�
            //if (Input.GetKeyDown(KeyCode.Mouse0) && !mouseOveringInfoUI)
            //{
            //    ReleaseChoosedEntity();
            //    DestoryNowUnitInformation();
            //}
            //////����ʵ�����
            //if (Input.GetKeyDown(KeyCode.B))
            //    GenerateOneHero(Camp.demon, SoldierIds.hero1, SceneObjectsManager.instance.playerEntityGeneratePoint.position);
            ////if (Input.GetKeyDown(KeyCode.B))
            ////{
            ////    CreateSoldierWithGroup(Camp.demon, SoldierIds.lmp, FormationIds.Formation_4Soldiers, true);
            ////}
            //////Ӣ�ۼ��ܲ���
            ////if (hero && Input.GetKeyDown(KeyCode.Alpha1))
            ////{
            ////    hero.UseSkill(0);
            ////}
            ////if (hero && Input.GetKeyDown(KeyCode.Alpha2))
            ////{
            ////    hero.UseSkill(1);
            ////}
        }

        #region Ӣ�����

        /// <summary>
        /// ����Ӣ��
        /// </summary>
        /// <param name="hero"></param>
        public void SetHero(Entity hero)
        {
            this.hero = hero;
            SceneObjectsManager.instance.heroInfoUI.SetHero(hero);
        }

        #endregion

        #region ������ʾ���
        /// <summary>
        /// ��ʾ�����ϵĽ�Ǯ��ѪҺ����ʾ����������Ч��
        /// </summary>
        public void Money_BoloodShowing()
        {
            //��ʾ��ǮUI
            objectManager.moneyText.text = nowMoneyCost + "/" + money;
            //��ʾѪҺUI
            objectManager.bloodText.text = nowBloodCost + "/" + blood;
            //��ʾ��Ǯ��UI�����
            objectManager.costFill.fillAmount = Mathf.SmoothDamp(objectManager.costFill.fillAmount, nowBloodCost / blood, ref v1, 0.25f);
            objectManager.moneyFill.fillAmount = Mathf.SmoothDamp(objectManager.moneyFill.fillAmount, nowMoneyCost / money, ref v2, 0.25f);
            //�������ֵʱ�����������ɫ��Ϊ��ɫ
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

        #region �˺����
        /// <summary>
        /// ����һ��aoe�˺����򣬲���Ϊ�������꣬��ⷶΧ�������ߣ���Ӫ���˺�����Ҫ����buff����buff��id
        /// </summary>
        /// <param name="��������"></param>
        /// <param name="��ⷶΧ"></param>
        /// <param name="������"></param>
        /// <param name="��Ӫ"></param>
        /// <param name="�˺�"></param>
        /// <param name="��buff��id"></param>
        public void CreateAoeHurtArea(Vector3 center, Vector2 checkArea, Entity creater, Camp camp, DamageData damageData)
        {
            //������Ч
            //GameObject vfx = VfxManager.instance.CreateVfx(VfxManager.instance.vfx_Aoe_2, center, new Vector3(5, 5, 5), 5); //��Ч��С�����˸������ݺ��ٸ����޸�
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
        /// �˺�����,ͬʱ�����˺���Ϣ����buff������Ч�����˺�ִ��ʱҲ�ᴥ���������ߵ��������Ч��������Ϊ�˺��������ߣ��˺���Ϣ���˺�Ŀ��
        /// ����ֵΪ��ɵ������˺�
        /// </summary>
        /// <param name="�˺���"></param>
        /// <param name="�˺���Ϣ"></param>
        /// <param name="���˺���"></param>
        public float CreateDamage(Entity creater, DamageData damageData, Entity target)
        {
            if (creater.camp == target.camp)
                return 0;
            float finalDamage;
            //ʹ������
            if (target && creater)
            {
                //�˺�����ߵĹ���ǰ�����¼�
                creater.TagEvent_BeforeAttack(creater, target, damageData, damageData);
                //�˺������ߵ�����ǰ�����¼�
                target.TagEvent_BeforeHurt(target, creater, damageData, damageData);

                //����˺�
                target.TakeDamage(damageData, creater, out finalDamage);

                //�˺�����ߵĹ���������¼�
                creater.TagEvent_AfterAttack(creater, finalDamage, target, damageData);
                //�˺������ߵ����˺�����¼�
                target.TagEvent_AfterHurt(target, finalDamage, creater, damageData);
                //buff���
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

        #region Ͷ�������

        /// <summary>
        /// ����һ��Ͷ�������Ϊid������,Ŀ��
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
        /// ����Ͷ�������ΪĿ��������꣬id��������Ӫ�ʹ�����
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
        /// <summary>
        /// ����Ͷ�������ΪĿ��������꣬id��������Ӫ,������
        /// ���ı����ǲ���ѡȡ��Χ�ڵ�Ŀ��
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="id"></param>
        /// <param name="camp"></param>
        /// <returns></returns>
        public Missile GenerateOneMissle(Vector3 pos, string id, Camp camp, Entity creater, Entity entityNotAttack)
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
            //m.SetTarget(pos);
            m.entitiesNotInAttackTarget = new List<Entity>();
            m.entitiesNotInAttackTarget.Add(entityNotAttack);
            return m;
        }
        #endregion

        #region ʵ����� ����ʵ�壬ѡ��ʵ�壬������棬������Ϣ���ͷ�ѡ���ʵ��

        /// <summary>
        /// ����һ��ʵ�壬����Ϊ��Ӫ��ID
        /// </summary>
        /// <param name="��Ӫ"></param>
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

            //��ȡʵ�����
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
        /// ����һ��ʵ�壬����Ϊ��Ӫ��ID����������
        /// </summary>
        /// <param name="��Ӫ"></param>
        /// <param name="id"></param>
        /// <param name="����"></param>
        public Entity GenerateOneEntity(Camp camp, string id, Vector3 pos)
        {
            if (GameDataManager.instance.GetEntityDataById(id) == null)
                return null;
            //���ɴ���+1
            genrateAmount++;
            //���ɿն��󣬽�������FaceToCamera��
            GameObject go;/* = Instantiate(GameDataManager.instance.emptyEntity, SceneObjectsManager.instance.allUnitParent);*/
            if (GameDataManager.instance.GetEntityDataById(id).name.Split("/").Length == 1)
                go = Instantiate(GameDataManager.instance.emptyEntity, SceneObjectsManager.instance.allUnitParent);
            else
                go = Instantiate(GameDataManager.instance.GetSpinePrefabDataById(id), SceneObjectsManager.instance.allUnitParent);
            //��������
            go.transform.position = pos;
            //��ӻ��ȡʵ��ű�
            Entity e;
            if (!go.GetComponent<Entity>())
                e = go.AddComponent<Entity>();
            else
                e = go.GetComponent<Entity>();
            e.camp = camp;
            //��ȡ����
            e.parameter = new UnitData();
            e.parameter.SetValue(GameDataManager.instance.GetEntityDataById(id));
            //����ʵ��
            e.GenerateEntity();
            //��������
            e.name = e.parameter.name + "_" + genrateAmount;
            //������Ӫ
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
        /// ����һ��Ӣ�ۣ�����Ϊ��Ӫ��ID����������
        /// </summary>
        /// <param name="camp"></param>
        /// <param name="id"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public Entity GenerateOneHero(Camp camp, string id, Vector3 pos)
        {
            if (GameDataManager.instance.GetHeroGameObjectById(id) == null)
                return null;
            //���ɴ���+1
            genrateAmount++;
            //����Ӣ�۶��󣬽�������FaceToCamera��
            GameObject go = Instantiate(GameDataManager.instance.GetHeroGameObjectById(id), SceneObjectsManager.instance.allUnitParent);/* = Instantiate(GameDataManager.instance.emptyEntity, SceneObjectsManager.instance.allUnitParent);*/

            //��������
            go.transform.position = pos;
            return go.GetComponent<Entity>();
        }
        /// <summary>
        /// ����ѡ�е�ʵ��ʱ�������ʼ�������ʵ��
        /// </summary>
        public void CameraFollow_ByChoosedTarget()
        {
            //ֻ�ı��������Ŀ���X�ᣬ���ı�YZ
            cameraFollowTarget.transform.position = new Vector3(nowChoosedTarget.transform.position.x, cameraFollowTarget.transform.position.y, 3);
        }

        ///// <summary>
        ///// ����Ϊ��λ����ʿ��������Ϊ��Ӫ��ʿ��ID������ID
        ///// </summary>
        //public void CreateSoldierWithGroup(Camp camp, string soldierId, string formationId, bool destoryShadow)
        //{
        //    //���ɴ���++
        //    genrateAmount++;
        //    //�ն���
        //    GameObject go;
        //    //������Ӫ�ж���������һ��
        //    Vector3 pos = camp == Camp.demon ? SceneObjectsManager.instance.playerEntityGeneratePoint.position : SceneObjectsManager.instance.enemyEntityGeneratePoint.position;
        //    //�������ͺ����λ��ƫ��
        //    go = Instantiate(GameDataManager.instance.GetFormationById(formationId), SceneObjectsManager.instance.allUnitParent);
        //    go.transform.position = pos + new Vector3(Random.Range(-5f, 5f), Random.Range(-7f, 7f), 0);
        //    //��ȡ�������ֵ���
        //    SoliderGroup sg = go.GetComponent<SoliderGroup>();
        //    sg.camp = camp;
        //    Debug.Log("����ʱ��ʿ��ID��" + soldierId);
        //    sg.finalSoldierId = soldierId;
        //    sg.Initialize();
        //    sg.Generate(destoryShadow);
        //}
        ///// <summary>
        ///// ����Ϊ��λ����ʿ��������Ϊ��Ӫ��ʿ��ID,����ID,�Ƿ�ݻ���Ӱ����������Ӧ�ٻ���λ�����ƫ��λ��
        ///// </summary>
        //public void CreateSoldierWithGroup(Camp camp, string soldierId, string formationId, bool destoryShadow, Vector3 Offset)
        //{
        //    genrateAmount++;
        //    GameObject go;
        //    Vector3 pos = camp == Camp.demon ? SceneObjectsManager.instance.playerEntityGeneratePoint.position : SceneObjectsManager.instance.enemyEntityGeneratePoint.position;
        //    go = Instantiate(GameDataManager.instance.GetFormationById(formationId), SceneObjectsManager.instance.allUnitParent);
        //    go.transform.position = pos/* + new Vector3(Random.Range(-5f, 5f), Random.Range(-7f, 7f), 0)*/;
        //    SoliderGroup sg = go.GetComponent<SoliderGroup>();
        //    sg.camp = camp;
        //    Debug.Log("����ʱ��ʿ��ID��" + soldierId);

        //    sg.finalSoldierId = soldierId;
        //    sg.transform.position += Offset;
        //    sg.Generate(destoryShadow);
        //}
        ///// <summary>
        ///// ����Ϊ��λ����ʿ��������Ϊ��Ӫ��ʿ��ID,����ID,�Ƿ�ݻ���Ӱ������λ�ã����ƫ��λ��
        ///// </summary>
        //public void CreateSoldierWithGroup(Camp camp, string soldierId, string formationId, bool destoryShadow, Vector3 StartPos, Vector3 Offset)
        //{
        //    genrateAmount++;
        //    GameObject go;
        //    Vector3 pos = StartPos;/* = camp == Camp.demon ? SceneObjectsManager.instance.playerEntityGeneratePoint.position : SceneObjectsManager.instance.enemyEntityGeneratePoint.position;*/
        //    go = Instantiate(GameDataManager.instance.GetFormationById(formationId), SceneObjectsManager.instance.allUnitParent);
        //    go.transform.position = pos/* + new Vector3(Random.Range(-5f, 5f), Random.Range(-7f, 7f), 0)*/;
        //    SoliderGroup sg = go.GetComponent<SoliderGroup>();
        //    sg.camp = camp;
        //    Debug.Log("����ʱ��ʿ��ID��" + soldierId);

        //    sg.finalSoldierId = soldierId;
        //    sg.transform.position += Offset;
        //    sg.Generate(destoryShadow);
        //}


        #region ���ѡ��ʵ�����ط���s

        /// <summary>
        /// ȷ��ѡ�������ָ��ʵ��
        /// </summary>
        public void EnshureChooseTarget()
        {
            ChooseOneEntity(nowChooseingTarget);
            CreateInforMationUi();
        }
        /// <summary>
        /// ����ʵ����ϢUI
        /// </summary>
        public void CreateInforMationUi()
        {
            DestoryNowUnitInformation();
            nowUnitInformationUI = GameObject.Instantiate(unitInformationUI, SceneObjectsManager.instance.BattleUI.transform);
            nowUnitInformationUI.SetInformation(nowChoosedTarget.GetComponent<Entity>().parameter, nowChoosedTarget);
        }
        /// <summary>
        /// ȡ����ǰ��ʵ����ϢUI
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
        /// ѡ��ʵ��
        /// 1.�ı�����ĸ���Ŀ�굽ʵ�����ڵ�X��λ�ã��л�UI��ʾģʽ
        /// 2.�ı���spriterenender�Ĳ�����ʾ
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
        /// �ͷ�ѡ���ʵ��
        /// 1.�ı���������Ŀ��ΪĬ��״̬���л�UI��ʾģʽ
        /// 2.��ѡ���ʵ��Ĳ��ʱ��Ĭ��ģʽ
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

        #region ���ֿ����ɡ�ѡ����ơ�����ʿ��

        /// <summary>
        /// Ϊ���󿨼������һ������
        /// </summary>
        public void AddOneFormatCard(FormatCard formatCard)
        {
            allSelectedCards.Add(formatCard);
        }
        /// <summary>
        /// Ϊ���󿨼����Ƴ�һ������
        /// </summary>
        public void RemoveOneFormatCard(FormatCard formatCard)
        {
            allSelectedCards.Remove(formatCard);
        }
        /// <summary>
        /// Ϊ���󿨼����Ƴ������ӵĲ���
        /// </summary>
        public void RemoveLastFormatCard()
        {
            allSelectedCards.RemoveAt(allSelectedCards.Count - 1);
        }


        /// <summary>
        /// ��UI���½Ǵ���һ�����ֿ�������Ϊ��id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SoldierCard CreateOneSoldierCard(string id)
        {
            SoldierCard card = Instantiate(GameDataManager.instance.emptySoldierCard, SceneObjectsManager.instance.cardSelectUI.transform).GetComponent<SoldierCard>();

            card.parameter.id = id;
            card.name = "SoldierCard:" + id;
            soldierCards.Add(card);

            return card;
        }
        /// <summary>
        /// ͨ��id��ȡ�������˵ı��ֿ�
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
        /// ������½ǵı���
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
        /// ѡ����ƣ���������½ǵı���֮���ڲ������������ģ�������ƣ��ڱ������ж�Ӧ��������ã����������Ķ�Ӧ�ı���
        /// </summary>
        /// <param name="card"></param>
        public FormatCard SelectSoldierCard(SoldierCard card)
        {
            Debug.Log("Cost:" + nowBloodCost + "  cardCost:" + card.parameter.bloodCost + "money:" + money + "  cardMoney:" + card.parameter.moneyCost);

            //��ӽ�Ǯ��costͳ��
            nowMoneyCost += card.parameter.moneyCost;
            nowBloodCost += card.parameter.bloodCost;
            //�ڲ��������ɲ���
            FormatCard formatCard = Instantiate(GameDataManager.instance.emptyFormat, SceneObjectsManager.instance.formationMakingUI.transform).GetComponent<FormatCard>();
            //���貼����Ҫ������
            formatCard.SetParentParameter(card.parameter);
            //�����ƶ�Ӧ�ı��鴫������
            //formatCard.connectedSoldierGroup = card.parameter.content;
            //����������ͼ
            formatCard.SetFlagSprite(card.parameter.flagSprite);

            //�������ĵ���ק��Χ
            formatCard.GetComponent<UiDrag>().container = formatCard.transform.parent.gameObject.GetComponent<RectTransform>();
            //�����ļ���ͳ��
            formatCards.Add(formatCard);
            //���õ�����ɵĿ�Ϊ������Ŀ�
            SetLastSelectCard(formatCard);

            return formatCard;
        }

        #region ���������ѡ���¼�ķ���s

        /// <summary>
        /// ������ģ�ǮѪ����
        /// </summary>
        public void ClearCardSelect()
        {
            //ѡ���¼Ϊ��ֱ�ӷ���
            if (formatCards.Count == 0)
                return;
            //�������е�ѡ��Ĳ��󿨣������ʾ
            foreach (FormatCard c in formatCards)
            {
                c.ClearThis();
                Destroy(c.gameObject);
            }
            //�������ѡ���¼�ͱ���ѡ���¼
            ClearSelectRecord();
           

            //��ǮѪҺ���㣬������ɺ����
            //money -= nowMoneyCost;
            //blood -= nowBloodCost;
            //nowMoneyCost = 0;
            ////cost�ع�Ϊ0
            //nowBloodCost = 0;
            //���ϴ�ѡ��ļ�¼���
            SetLastSelectCard(null);
        }
        /// <summary>
        /// ���ˮ����󣬱�����ǰ���������ɶ�Ӧ�ı��֣�֮��ȡ������
        /// ���ģ��������ֵ֮����Ȼ�����������ģ����ǲ������ɾ������
        /// </summary>
        public void GenerateSoldiers()
        {
            //List<SoliderGroup> newGroupList = new List<SoliderGroup>();
            ////�����Ǯ��costͳ�ƣ����������ֵ������
            //if (nowBloodCost > blood || nowMoneyCost > money)
            //{
            //    Debug.Log("�������ֵ");
            //    return;
            //}
            ////���ɱ���
            //foreach (SoliderGroup sg in soliderFormatGroups)
            //{
            //    //��¼��ǰ�����ϵı���λ�ú���Ϣ
            //    SoliderGroup NG = Instantiate(sg.gameObject, sg.transform.parent).GetComponent<SoliderGroup>();
            //    sg.parentFormatCard.SetConnectGroup(NG);
            //    newGroupList.Add(NG);


            //    sg.Generate(false);

            //}
            //money -= nowMoneyCost;
            //blood -= nowBloodCost;
            ////���ɱ���֮�󣬳��ϵĲ�Ӱ�ͻ���ʧ�����������˵ı��֣���Ҫ��ԭ������һ���Ĳ�Ӱ��ͬʱ���ù�����Ӱ�Ĳ���
            //soliderFormatGroups.Clear();
            //foreach (SoliderGroup s in newGroupList)
            //{
            //    soliderFormatGroups.Add(s);
            //}
            //Debug.Log("��������");
            //ReBuildSoldierGroups();

            //������ģ����㻨�ѽ�Ǯ
            //ClearCardSelect();

            //ReBuildSoldierGroups();
        }
        ///// <summary>
        ///// ���ɱ���֮���������ó��ϵı��ֿ�
        ///// ���ڱ������ɺ��Ѿ����ɹ��ı��鸸�����������õ���һ�����ɵı���
        ///// ����Ҫ���Ŀǰ�ļ�¼��������ͬλ������һ�����±��ָ�������ͳ����Щ�µĶ���
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
        /// ����������ı��Ƽ�¼�����ڳ�����
        /// </summary>
        /// <param name="card"></param>
        public void SetLastSelectCard(FormatCard card)
        {
            lastSelectCard = card;
        }

        /// <summary>
        /// �������һ�����ĵ�ѡ��
        /// </summary>
        public void RevokeCardSelect()
        {
            //ѡ���¼Ϊ��ֱ�ӷ���
            if (formatCards.Count == 0)
                return;

            //��ǮѪҺ����ͳ�Ƽ�ȥ�ϴε���Ŀ�����ֵ
            nowMoneyCost -= lastSelectCard.parentParameter.moneyCost;
            nowBloodCost -= lastSelectCard.parentParameter.bloodCost;


            //����ͳ�����Ƴ���һ�ε�����Ƴ��ֵĲ���
            formatCards.Remove(lastSelectCard);


            //ȡ���ò����ڳ����д���Ķ������ʾ
            lastSelectCard.ClearThis();


            //�ݻٸò���
            if (lastSelectCard)
                Destroy(lastSelectCard.gameObject);


            //��count������ʱ���������������±���ߵĶ�����Ϊ�ϴε���Ķ���
            if (formatCards.Count > 0)
                SetLastSelectCard(formatCards[formatCards.Count - 1]);
        }


        /// <summary>
        /// �������е�ѡ�񣨵�count��Ϊ��ʱѭ��ִ�г���������
        /// </summary>
        public void RevokeAllCardSelect()
        {
            while (formatCards.Count > 0)
            {
                RevokeCardSelect();
            }
        }


        /// <summary>
        /// ���ѡ���¼
        /// </summary>
        void ClearSelectRecord()
        {
            formatCards.Clear();
        }

        #endregion


        #endregion

        #region ��ǮѪҺ���
        /// <summary>
        /// ��ʼ���ӽ�Ǯ�����޸Ľ�Ǯ��ֵ֮������¿���һ����ǮЭ��)
        /// </summary>
        public void StartAddMoney()
        {
            //if (generateMoneyIenumerator != null)
            //    StopCoroutine(generateMoneyIenumerator);
            generateMoneyIenumerator = StartCoroutine(addMoney());
        }

        /// <summary>
        /// ÿ�����ӽ�Ǯ��Э��
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
        /// ����ѪҺ
        /// </summary>
        /// <param name="amount"></param>
        public void AddBlood(float amount)
        {
            blood += amount;
        }
        /// <summary>
        /// ��һ���������ӽ�Ǯ
        /// </summary>
        /// <param name="moneyAmount"></param>
        public void AddMoney(float moneyAmount)
        {
            money += moneyAmount;
            Debug.Log("������Ǯ��");
        }
        /// <summary>
        /// ����һ��Ǯ��
        /// </summary>
        /// <returns></returns>
        public GameObject CreateMoneyBag(Vector3 startPos, float moneyAmount)
        {
            GameObject go = Instantiate(GameDataManager.instance.moneyBag, SceneObjectsManager.instance.allUnitParent);
            go.name = "Ǯ��";

            go.transform.position = startPos;
            go.GetComponent<MoneyBag>().moneyAmount = moneyAmount;
            go.GetComponent<ArcMovement>().targetV3 = startPos + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
            return go;
        }
        #endregion


        /// <summary>
        /// ��Ϸ����
        /// </summary>
        /// <param name="playerWin"></param>
        public void EndBattle(bool playerWin)
        {
            if (!SceneObjectsManager.instance.gameOverPanel)
                return;
            if (playerWin)
            {
                SceneObjectsManager.instance.gameOverPanel.gameObject.SetActive(true);
                SceneObjectsManager.instance.gameOverPanel.endText.text = "��ʬ�Ե���������� \n (����)";
                return;
            }
            else
            {
                SceneObjectsManager.instance.gameOverPanel.gameObject.SetActive(true);
                SceneObjectsManager.instance.gameOverPanel.endText.text = "��Ե��˽�ʬ������ \n (��)";
                return;
            }

        }


        #region ��ʾ��Ϣ���
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


        #region ս���������


        /// <summary>
        /// ���ݾݵ�ķ��������볡���еľݵ㣬����������������Ҫ���ִ��
        /// </summary>
        /// <param name="sceneStrongHold"></param>
        public void CaptureStrongHold(SceneStrongHold sceneStrongHold)
        {
            if (sceneStrongHold.gameObjectSetActiveWhenDestory != null)
                sceneStrongHold.gameObjectSetActiveWhenDestory.SetActive(true);
            //˫�����ɵ�λ���л�

            ChangePlayerUnitSpawnPoint(sceneStrongHold.connectedUnitSpawnPoint_Player);
            ChangeEnemyUnitSpawnPoint(sceneStrongHold.connectedUnitSpawnPoint_Enemy);

            RevokeAllCardSelect();
            sceneStrongHold.gameObject.SetActive(false);
        }
        /// <summary>
        /// �ı���ҵĵ�λ���ɵ�λ
        /// </summary>
        /// <param name="newPoint"></param>
        public void ChangePlayerUnitSpawnPoint(Transform newPoint) => SceneObjectsManager.instance.playerEntityGeneratePoint = newPoint;
        /// <summary>
        /// �ı���˵ĵ�λ���ɵ�λ
        /// </summary>
        /// <param name="newPoint"></param>
        public void ChangeEnemyUnitSpawnPoint(Transform newPoint) => SceneObjectsManager.instance.enemyEntityGeneratePoint = newPoint;


        #endregion
    }
}