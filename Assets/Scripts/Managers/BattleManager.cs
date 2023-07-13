using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ս��������
/// </summary>
public class BattleManager : MonoBehaviour
{
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
    [Header("�����еı������s")]
    public List<SoliderGroup> soliderFormatGroups;
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
    [Header("ս������UI")]
    public GameObject BattleUI;
    [Header("������Ϣ��ʾ��(Ԥ�Ƽ���")]
    public UnitInformationUi unitInformationUI;
    [Header("ѡ������")]
    public Image cardSelectUI;
    [Header("�������")]
    public Image formationMakingUI;
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
    private void Awake()
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
        //�������½ǵı��ֿ�
        CreateOneSoldierCard("21000001");
        CreateOneSoldierCard("21000002");
        //string json1 = JsonConvert.SerializeObject("asd");
        //Debug.Log(json1);
    }


    void Update()
    {
        //��ʾ��ǮUI
        objectManager.moneyText.text = nowMoneyCost + "/" + money;
        //��ʾѪҺUI
        objectManager.bloodText.text = nowBloodCost + "/" + blood;
        //��ʾ��Ǯ��UI�����
        objectManager.costFill.fillAmount = nowBloodCost / blood;
        objectManager.moneyFill.fillAmount = nowMoneyCost / money;
        //����ѡ�е�ʵ��ʱ�����������ѡ���ʵ��
        if (nowChoosedTarget != null)
            CameraFollow_ByChoosedTarget();


        //���ԣ�������ѡ��Ķ���ʱ���Ҽ�ȷ��ѡ��
        if (nowChooseingTarget != null && Input.GetKeyDown(KeyCode.Mouse1))
        {
            EnshureChooseTarget();
        }
        //������ͷ�,����������Ϣui�����ͷ�
        if (Input.GetKeyDown(KeyCode.Mouse0) && !mouseOveringInfoUI)
        {
            ReleaseChoosedEntity();
            DestoryNowUnitInformation();
        }
        //����ʵ�����
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GenerateOneEntity(Camp.demon, "1000004");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            GenerateOneEntity(Camp.demon, "4000001");
        }
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    GenerateOneEntity(Camp.demon, "1000005");
        //}
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    GenerateOneEntity(Camp.demon, "1000004");
        //}
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug_CreateEnemy();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug_CreateEnemy_2();
        }
    }

    public void Debug_CreateEnemy()
    {
        GenerateOneEntity(Camp.human, "1000002");

    }
    public void Debug_CreateEnemy_2()
    {
        GenerateOneEntity(Camp.human, "1000006");

    }
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
    /// </summary>
    /// <param name="�˺���"></param>
    /// <param name="�˺���Ϣ"></param>
    /// <param name="���˺���"></param>
    public void CreateDamage(Entity creater, DamageData damageData, Entity target)
    {
        if (creater.camp == target.camp)
            return;
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
            for (int i = 0; i < damageData.buffs.Length; i++)
            {
                BuffManager.instance.EntityAddBuff(target, damageData.buffs[i]);
            }
        }
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
            go = Instantiate(GameDataManager.instance.emptyEntity, GameObject.Find("FaceToCamera").transform);
        else
            go = Instantiate(GameDataManager.instance.GetSpinePrefabDataById(id), GameObject.Find("FaceToCamera").transform);
        Entity e;

        //��ȡʵ�����
        if (!go.GetComponent<Entity>())
            e = go.AddComponent<Entity>();
        else
            e = go.GetComponent<Entity>();
        //e.camp = camp;
        e.parameter = new UnitParameter();
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
        //���ɴ���+1
        genrateAmount++;
        //���ɿն��󣬽�������FaceToCamera��
        GameObject go = Instantiate(GameDataManager.instance.emptyEntity, GameObject.Find("FaceToCamera").transform);
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
        e.parameter = new UnitParameter();
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
    /// ����ѡ�е�ʵ��ʱ�������ʼ�������ʵ��
    /// </summary>
    public void CameraFollow_ByChoosedTarget()
    {
        //ֻ�ı��������Ŀ���X�ᣬ���ı�YZ
        cameraFollowTarget.transform.position = new Vector3(nowChoosedTarget.transform.position.x, cameraFollowTarget.transform.position.y, 3);
    }

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
        nowUnitInformationUI = GameObject.Instantiate(unitInformationUI, BattleUI.transform);
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
    /// ��UI���½Ǵ���һ�����ֿ�������Ϊ��id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public SoldierCard CreateOneSoldierCard(string id)
    {
        SoldierCard card = Instantiate(GameDataManager.instance.emptySoldierCard, BattleUI.transform.Find("BattleUI_Down_Image").transform.Find("CardSelectArea").transform).GetComponent<SoldierCard>();

        card.parameter.id = id;
        card.name = "SoldierCard:" + id;
        soldierCards.Add(card);

        return card;
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
    public void SelectSoldierCard(SoldierCard card)
    {
        Debug.Log("Cost:" + nowBloodCost + "  cardCost:" + card.parameter.bloodCost + "money:" + money + "  cardMoney:" + card.parameter.moneyCost);
        //�����Ǯ��costͳ�ƣ����������ֵ������
        if (nowBloodCost + card.parameter.bloodCost > blood || nowMoneyCost + card.parameter.moneyCost > money)
        {
            Debug.Log("�������ֵ");
            return;
        }
        //��ӽ�Ǯ��costͳ��
        nowMoneyCost += card.parameter.moneyCost;
        nowBloodCost += card.parameter.bloodCost;
        //�ڲ�������������
        FormatCard formatCard = Instantiate(GameDataManager.instance.emptyFormat, formationMakingUI.transform).GetComponent<FormatCard>();
        formatCard.SetParentParameter(card.parameter);
        //�����ƶ�Ӧ�ı��鴫������
        formatCard.connectedSoldierGroup = card.parameter.content;
        //����������ͼ
        formatCard.SetFlagSprite(card.parameter.flagSprite);
        //�������ĵ���ק��Χ
        formatCard.GetComponent<UiDrag>().container = formatCard.transform.parent.gameObject.GetComponent<RectTransform>();
        //�����ļ���ͳ��
        formatCards.Add(formatCard);
        //���õ�����ɵĿ�Ϊ������Ŀ�
        SetLastSelectCard(formatCard);
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
        soliderFormatGroups.Clear();

        //��ǮѪҺ���㣬������ɺ����
        money -= nowMoneyCost;
        blood -= nowBloodCost;
        nowMoneyCost = 0;

        //cost�ع�Ϊ0
        nowBloodCost = 0;
        //���ϴ�ѡ��ļ�¼���
        SetLastSelectCard(null);
    }
    /// <summary>
    /// ���ˮ����󣬱�����ǰ���������ɶ�Ӧ�ı��֣�֮��ȡ������
    /// </summary>
    public void GenerateSoldiers()
    {
        //���ɱ���
        foreach (SoliderGroup sg in soliderFormatGroups)
        {
            sg.Generate();
        }
        //������ģ����㻨�ѽ�Ǯ
        ClearCardSelect();
    }

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
        GameObject go = Instantiate(GameDataManager.instance.moneyBag, GameObject.Find("FaceToCamera").transform);
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
}
