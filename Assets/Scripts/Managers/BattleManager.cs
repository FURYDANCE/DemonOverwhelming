using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 战斗管理器
/// </summary>
public class BattleManager : MonoBehaviour
{
    /// <summary>
    /// 金钱
    /// </summary>
    [Header("金钱")]
    public float money;
    public float moneyPerSecond;
    public float moneyAddAmount;
    public float nowMoneyCost;
    /// <summary>
    /// 血液
    /// </summary>
    [Header("血液")]
    public float blood;
    public float nowBloodCost;
    [Header("选择的兵牌组合")]
    public List<SoldierCard> soldierCards;
    [Header("布阵中的兵种组合s")]
    public List<SoliderGroup> soliderFormatGroups;
    /// <summary>
    /// 待定选择的实体（鼠标悬停在了上面，但还没有按下选择键）
    /// </summary>
    [Header("待定选择的实体（鼠标悬停在了上面，但还没有按下选择键）")]
    public GameObject nowChooseingTarget;
    /// <summary>
    /// 被选择了的实体
    /// </summary>
    [Header("现在选中的实体")]
    public GameObject nowChoosedTarget;
    /// <summary>
    /// 战斗界面UI
    /// </summary>
    [Header("战斗界面UI")]
    public GameObject BattleUI;
    [Header("对象信息提示框(预制件）")]
    public UnitInformationUi unitInformationUI;
    [Header("选卡界面")]
    public Image cardSelectUI;
    [Header("布阵界面")]
    public Image formationMakingUI;
    /// <summary>
    /// 当前布阵界面上的所有旗帜
    /// </summary>
    [Header("当前布阵界面上的所有旗帜")]
    public List<FormatCard> formatCards;
    FormatCard lastSelectCard;
    //public SoldierCard lastSelectCard_card;
    [Header("被创造出的所有士兵")]
    public List<Entity> allSoldiers;
    UnitInformationUi nowUnitInformationUI;
    int genrateAmount;
    /// <summary>
    /// 相机的跟随模式（跟随UI中的slider条还是跟随选择的实体）
    /// </summary>
    [HideInInspector]
    public CameraControlMode cameraControlMode = CameraControlMode.followUi;

    /// <summary>
    /// 从对象管理器中取得的相机跟随轴
    /// </summary>
    Transform cameraFollowTarget;
    /// <summary>
    /// 鼠标是否位于信息ui上，用于控制按下左键时是否取消信息ui显示
    /// </summary>
    public bool mouseOveringInfoUI;
    /// <summary>
    /// 战斗管理器单例
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
        //生成右下角的兵种卡
        CreateOneSoldierCard("21000001");
        CreateOneSoldierCard("21000002");
        //string json1 = JsonConvert.SerializeObject("asd");
        //Debug.Log(json1);
    }


    void Update()
    {
        //显示金钱UI
        objectManager.moneyText.text = nowMoneyCost + "/" + money;
        //显示血液UI
        objectManager.bloodText.text = nowBloodCost + "/" + blood;
        //显示金钱和UI的填充
        objectManager.costFill.fillAmount = nowBloodCost / blood;
        objectManager.moneyFill.fillAmount = nowMoneyCost / money;
        //当有选中的实体时，相机跟随所选择的实体
        if (nowChoosedTarget != null)
            CameraFollow_ByChoosedTarget();


        //测试：有正在选择的对象时按右键确认选择
        if (nowChooseingTarget != null && Input.GetKeyDown(KeyCode.Mouse1))
        {
            EnshureChooseTarget();
        }
        //按左键释放,如果鼠标在信息ui上则不释放
        if (Input.GetKeyDown(KeyCode.Mouse0) && !mouseOveringInfoUI)
        {
            ReleaseChoosedEntity();
            DestoryNowUnitInformation();
        }
        //生成实体测试
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
    #region 伤害相关
    /// <summary>
    /// 创建一个aoe伤害区域，参数为中心坐标，检测范围，创造者，阵营，伤害，若要传入buff，其buff的id
    /// </summary>
    /// <param name="中心坐标"></param>
    /// <param name="检测范围"></param>
    /// <param name="创造者"></param>
    /// <param name="阵营"></param>
    /// <param name="伤害"></param>
    /// <param name="其buff的id"></param>
    public void CreateAoeHurtArea(Vector3 center, Vector2 checkArea, Entity creater, Camp camp, DamageData damageData)
    {
        //生成特效
        //GameObject vfx = VfxManager.instance.CreateVfx(VfxManager.instance.vfx_Aoe_2, center, new Vector3(5, 5, 5), 5); //特效大小等有了更多数据后再跟着修改
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
    /// 伤害方法,同时根据伤害信息生成buff和特殊效果，伤害执行时也会触发被攻击者的特殊词条效果，参数为伤害的制造者，伤害信息，伤害目标
    /// </summary>
    /// <param name="伤害者"></param>
    /// <param name="伤害信息"></param>
    /// <param name="被伤害者"></param>
    public void CreateDamage(Entity creater, DamageData damageData, Entity target)
    {
        if (creater.camp == target.camp)
            return;
        float finalDamage;
        //使其受伤
        if (target && creater)
        {
            //伤害输出者的攻击前词条事件
            creater.TagEvent_BeforeAttack(creater, target, damageData, damageData);
            //伤害承受者的受伤前词条事件
            target.TagEvent_BeforeHurt(target, creater, damageData, damageData);

            //造成伤害
            target.TakeDamage(damageData, creater, out finalDamage);

            //伤害输出者的攻击后词条事件
            creater.TagEvent_AfterAttack(creater, finalDamage, target, damageData);
            //伤害承受者的受伤后词条事件
            target.TagEvent_AfterHurt(target, finalDamage, creater, damageData);
            //buff检测
            for (int i = 0; i < damageData.buffs.Length; i++)
            {
                BuffManager.instance.EntityAddBuff(target, damageData.buffs[i]);
            }
        }
    }

    #endregion


    #region 投射物相关

    /// <summary>
    /// 创建一个投射物，参数为id，坐标,目标
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
    /// 生成投射物，参数为目标具体坐标，id、生成阵营和创造者
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

    #region 实体相关 创建实体，选择实体，相机跟随，创建信息，释放选择的实体

    /// <summary>
    /// 创建一个实体，参数为阵营和ID
    /// </summary>
    /// <param name="阵营"></param>
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

        //获取实体组件
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
    /// 创建一个实体，参数为阵营，ID和生成坐标
    /// </summary>
    /// <param name="阵营"></param>
    /// <param name="id"></param>
    /// <param name="坐标"></param>
    public Entity GenerateOneEntity(Camp camp, string id, Vector3 pos)
    {
        //生成次数+1
        genrateAmount++;
        //生成空对象，将其纳入FaceToCamera中
        GameObject go = Instantiate(GameDataManager.instance.emptyEntity, GameObject.Find("FaceToCamera").transform);
        //设置坐标
        go.transform.position = pos;
        //添加或获取实体脚本
        Entity e;
        if (!go.GetComponent<Entity>())
            e = go.AddComponent<Entity>();
        else
            e = go.GetComponent<Entity>();
        e.camp = camp;
        //读取变量
        e.parameter = new UnitParameter();
        e.parameter.SetValue(GameDataManager.instance.GetEntityDataById(id));
        //生成实体
        e.GenerateEntity();
        //设置名称
        e.name = e.parameter.name + "_" + genrateAmount;
        //设置阵营
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
    /// 当有选中的实体时，相机开始跟随这个实体
    /// </summary>
    public void CameraFollow_ByChoosedTarget()
    {
        //只改变相机跟随目标的X轴，不改变YZ
        cameraFollowTarget.transform.position = new Vector3(nowChoosedTarget.transform.position.x, cameraFollowTarget.transform.position.y, 3);
    }

    #region 鼠标选择实体的相关方法s

    /// <summary>
    /// 确定选择鼠标所指的实体
    /// </summary>
    public void EnshureChooseTarget()
    {
        ChooseOneEntity(nowChooseingTarget);
        CreateInforMationUi();
    }
    /// <summary>
    /// 创建实体信息UI
    /// </summary>
    public void CreateInforMationUi()
    {
        DestoryNowUnitInformation();
        nowUnitInformationUI = GameObject.Instantiate(unitInformationUI, BattleUI.transform);
        nowUnitInformationUI.SetInformation(nowChoosedTarget.GetComponent<Entity>().parameter, nowChoosedTarget);
    }
    /// <summary>
    /// 取消当前的实体信息UI
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
    /// 选择实体
    /// 1.改变相机的跟随目标到实体所在的X轴位置，切换UI显示模式
    /// 2.改变其spriterenender的材质显示
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
    /// 释放选择的实体
    /// 1.改变相机跟随的目标为默认状态，切换UI显示模式
    /// 2.将选择的实体的材质变回默认模式
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

    #region 兵种卡生成、选择兵牌、生成士兵

    /// <summary>
    /// 在UI右下角创建一个兵种卡，参数为其id
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
    /// 清空右下角的兵牌
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
    /// 选择兵牌，当点击右下角的兵牌之后，在布阵区生成旗帜，传入兵牌，在兵牌中有对应兵组的引用，将传给旗帜对应的兵组
    /// </summary>
    /// <param name="card"></param>
    public void SelectSoldierCard(SoldierCard card)
    {
        Debug.Log("Cost:" + nowBloodCost + "  cardCost:" + card.parameter.bloodCost + "money:" + money + "  cardMoney:" + card.parameter.moneyCost);
        //计算金钱和cost统计，若超过最大值则不生成
        if (nowBloodCost + card.parameter.bloodCost > blood || nowMoneyCost + card.parameter.moneyCost > money)
        {
            Debug.Log("超过最大值");
            return;
        }
        //添加金钱与cost统计
        nowMoneyCost += card.parameter.moneyCost;
        nowBloodCost += card.parameter.bloodCost;
        //在布阵区生成旗帜
        FormatCard formatCard = Instantiate(GameDataManager.instance.emptyFormat, formationMakingUI.transform).GetComponent<FormatCard>();
        formatCard.SetParentParameter(card.parameter);
        //将兵牌对应的兵组传给旗帜
        formatCard.connectedSoldierGroup = card.parameter.content;
        //设置旗帜贴图
        formatCard.SetFlagSprite(card.parameter.flagSprite);
        //设置旗帜的拖拽范围
        formatCard.GetComponent<UiDrag>().container = formatCard.transform.parent.gameObject.GetComponent<RectTransform>();
        //将旗帜加入统计
        formatCards.Add(formatCard);
        //设置点击生成的卡为最后点击的卡
        SetLastSelectCard(formatCard);
    }

    #region 撤销，清空选择记录的方法s

    /// <summary>
    /// 清空旗帜，钱血计算
    /// </summary>
    public void ClearCardSelect()
    {
        //选择记录为零直接返回
        if (formatCards.Count == 0)
            return;
        //遍历所有的选择的布阵卡，清除显示
        foreach (FormatCard c in formatCards)
        {
            c.ClearThis();
            Destroy(c.gameObject);
        }
        //清除布阵卡选择记录和兵牌选择记录
        ClearSelectRecord();
        soliderFormatGroups.Clear();

        //金钱血液计算，计算完成后归零
        money -= nowMoneyCost;
        blood -= nowBloodCost;
        nowMoneyCost = 0;

        //cost回归为0
        nowBloodCost = 0;
        //将上次选择的记录清空
        SetLastSelectCard(null);
    }
    /// <summary>
    /// 点击水晶球后，遍历当前的旗帜生成对应的兵种，之后取消旗帜
    /// </summary>
    public void GenerateSoldiers()
    {
        //生成兵种
        foreach (SoliderGroup sg in soliderFormatGroups)
        {
            sg.Generate();
        }
        //清空旗帜，计算花费金钱
        ClearCardSelect();
    }

    /// <summary>
    /// 设置最后点击的兵牌记录（用于撤销）
    /// </summary>
    /// <param name="card"></param>
    public void SetLastSelectCard(FormatCard card)
    {
        lastSelectCard = card;
    }

    /// <summary>
    /// 撤销最后一次旗帜的选择
    /// </summary>
    public void RevokeCardSelect()
    {
        //选择记录为零直接返回
        if (formatCards.Count == 0)
            return;

        //金钱血液计算统计减去上次点击的卡的数值
        nowMoneyCost -= lastSelectCard.parentParameter.moneyCost;
        nowBloodCost -= lastSelectCard.parentParameter.bloodCost;


        //布阵卡统计中移除上一次点击兵牌出现的布阵卡
        formatCards.Remove(lastSelectCard);


        //取消该布阵卡在场景中代表的对象的显示
        lastSelectCard.ClearThis();


        //摧毁该布阵卡
        Destroy(lastSelectCard.gameObject);


        //当count大于零时，继续将集合中下标最高的对象作为上次点击的对象
        if (formatCards.Count > 0)
            SetLastSelectCard(formatCards[formatCards.Count - 1]);
    }


    /// <summary>
    /// 撤销所有的选择（当count不为零时循环执行撤销方法）
    /// </summary>
    public void RevokeAllCardSelect()
    {
        while (formatCards.Count > 0)
        {
            RevokeCardSelect();
        }
    }


    /// <summary>
    /// 清空选择记录
    /// </summary>
    void ClearSelectRecord()
    {
        formatCards.Clear();

    }

    #endregion


    #endregion

    #region 金钱血液相关
    /// <summary>
    /// 开始增加金钱（在修改金钱数值之后会重新开启一个加钱协程)
    /// </summary>
    public void StartAddMoney()
    {
        //if (generateMoneyIenumerator != null)
        //    StopCoroutine(generateMoneyIenumerator);
        generateMoneyIenumerator = StartCoroutine(addMoney());
    }

    /// <summary>
    /// 每秒增加金钱的协程
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
    /// 增加血液
    /// </summary>
    /// <param name="amount"></param>
    public void AddBlood(float amount)
    {
        blood += amount;
    }
    /// <summary>
    /// 以一定数量增加金钱
    /// </summary>
    /// <param name="moneyAmount"></param>
    public void AddMoney(float moneyAmount)
    {
        money += moneyAmount;
        Debug.Log("捡起了钱包");
    }
    /// <summary>
    /// 创造一个钱包
    /// </summary>
    /// <returns></returns>
    public GameObject CreateMoneyBag(Vector3 startPos, float moneyAmount)
    {
        GameObject go = Instantiate(GameDataManager.instance.moneyBag, GameObject.Find("FaceToCamera").transform);
        go.name = "钱包";

        go.transform.position = startPos;
        go.GetComponent<MoneyBag>().moneyAmount = moneyAmount;
        go.GetComponent<ArcMovement>().targetV3 = startPos + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
        return go;
    }
    #endregion


    /// <summary>
    /// 游戏结束
    /// </summary>
    /// <param name="playerWin"></param>
    public void EndBattle(bool playerWin)
    {
        if (!SceneObjectsManager.instance.gameOverPanel)
            return;
        if (playerWin)
        {
            SceneObjectsManager.instance.gameOverPanel.gameObject.SetActive(true);
            SceneObjectsManager.instance.gameOverPanel.endText.text = "僵尸吃掉了你的脑子 \n (不是)";
            return;
        }
        else
        {
            SceneObjectsManager.instance.gameOverPanel.gameObject.SetActive(true);
            SceneObjectsManager.instance.gameOverPanel.endText.text = "你吃掉了僵尸的脑子 \n (雾)";
            return;
        }

    }


    #region 显示信息相关
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
