using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DemonOverwhelming;
/// <summary>
/// 对话管理器
/// 通过startDialog方法，传入对话开始的id来执行对话
/// </summary>
public class DialogManager : MonoBehaviour
{
    /// <summary>
    /// 总面板
    /// </summary>
    public GameObject AllPanel;
    /// <summary>
    /// 目前读到的对话内容
    /// </summary>
    public PlotsData nowData;
    /// <summary>
    /// 开始id
    /// </summary>
    public int startId;
    /// <summary>
    /// 当前id
    /// </summary>
    public int nowId;
    ///// <summary>
    ///// 历史对话统计
    ///// </summary>
    //public HistoryDialogPanel historyDialogPanel;
    /// <summary>
    /// 单个的历史对话表现对象
    /// </summary>
    public GameObject historyDialogSlot;
    /// <summary>
    /// 左立绘
    /// </summary>
    public Image leftStand;
    /// <summary>
    /// 右立绘
    /// </summary>
    public Image rightStand;
    /// <summary>
    /// 历史对话按钮
    /// </summary>
    public Button historyBtn;
    /// <summary>
    /// 跳过对话按钮
    /// </summary>
    public Button skipBtn;
    /// <summary>
    /// 对话内容按钮
    /// </summary>
    public TextMeshProUGUI contentText;
    /// <summary>
    /// speaker名称
    /// </summary>
    public TextMeshProUGUI nameText;
    ///// <summary>
    ///// 选项面板
    ///// </summary>
    //public GameObject opinionPanel;

    /// <summary>
    /// 按钮
    /// </summary>
    [Header("按钮对象")]
    public GameObject optionBtn;
    [Header("按钮的父对象")]
    public Transform optionParent;
    //public TextMeshProUGUI opinionBtn_1_text;
    //public Button opinionBtn_2;
    //public TextMeshProUGUI opinionBtn_2_text;
    ///// <summary>
    ///// 用于显示历史对话中选项的变量
    ///// </summary>
    //[HideInInspector]
    //public int selectionIndex;
    //public List<string> selections;
    [Header("历史对话面板")]
    public GameObject HistoryDialogPanel;
    /// <summary>
    /// 当前所有收集了的历史对话信息
    /// </summary>
    [Header("当前所有收集了的历史对话信息")]
    public List<HistoryDialogInformatioin> historyDialogs;
    /// <summary>
    /// 历史对话信息的父对象
    /// </summary>
    [Header("历史对话信息的父对象")]
    public Transform historyDialogParent;

    Coroutine typingCoroutine;
    bool isDuringDilaog;
    bool isTyping;
    bool isOpinioning;
    bool isShowingHistory;
    string assetName = "DataTables/PlotsDatas";

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            startDialog(1000001);
    }
    /// <summary>
    /// 核心：获取对话信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public List<PlotsData> GetRoleById(int id)
    {
        ExcelDataManager asset = Resources.Load<ExcelDataManager>(assetName);
        //Debug.Log(asset.name);
        List<PlotsData> find = asset.plotsData.FindAll((PlotsData p) => { return p.id == id; });
        return find;
    }
    #region 对话方法


    /// <summary>
    /// 开始对话
    /// </summary>
    /// <param name="startId"></param>
    public void startDialog(int startId)
    {
        //每次开始对话时清空历史对话记录
        historyDialogs.Clear();
        DestoryHistoryDialogs();

        //隐藏战斗UI
        SceneObjectsManager.instance.BattleUI.SetActive(false);
        isDuringDilaog = true;
        AllPanel.SetActive(true);
        this.startId = startId;
        nowId = startId;
        RefreshText();
    }
    /// <summary>
    /// 根据获取到的对话信息刷新面板上的显示
    /// </summary>
    public void RefreshText()
    {
     
        //如果按钮父对象的子物体数量不为空则摧毁所有按钮
        if (optionParent.childCount != 0)
        {
            foreach (Transform go in optionParent.transform)
                Destroy(go.gameObject);
        }



        leftStand.color = new Color(1, 1, 1, 1);
        rightStand.color = new Color(1, 1, 1, 1);
        leftStand.sprite = null;
        rightStand.sprite = null;
        nowData = GetRoleById(nowId)[0];
        //打字机协程
        typingCoroutine = StartCoroutine(typeText(nowData.content_cn));
        //获取名字
        nameText.text = nowData.speakerNmae;
        //获取贴图
        {
            if (!nowData.left_stand)
                leftStand.color = new Color(0, 0, 0, 0);
            else
            {
                leftStand.sprite = nowData.left_stand;
            }
            if (!nowData.right_stand)
                rightStand.color = new Color(0, 0, 0, 0);
            else
                rightStand.sprite = nowData.right_stand;
        }
        //获取选项
        if (nowData.haveOption)
        {
            //遍历按钮选项内容并且生成按钮，设置其文字与点击方法
            for (int i = 0; i < nowData.optionContents.Length; i++)
            {
                string btnContent = nowData.optionContents[i];
                GameObject go = Instantiate(optionBtn, optionParent);
                go.name = "Option_" + nowData.optionTargetIds[i];
                go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = btnContent;
                Button btn = go.GetComponent<Button>();

                btn.onClick.AddListener(delegate
                {
                    ChooseOption(int.Parse(btn.name.Replace("Option_", "")));
                });
                btn.onClick.AddListener(delegate { AddHistoryTextByOption(btn); });
            }



            isOpinioning = true;


        }

        //对话事件
        {
            foreach (string s in nowData.events)
                DialogEvent(s);
        }
        //添加历史对话记录
        AddHistoryTextByNowId();
    }

    /// <summary>
    /// 点击面板时触发的继续对话的判断
    /// </summary>
    public void NextDialogCheck()
    {
        if (isDuringDilaog)
        {
            if (isShowingHistory)
                return;
            if (isTyping)
            {
                EndTypingText();
                return;
            }
            if (!isOpinioning)
            {
                NextDialog();
            }
        }
    }
    /// <summary>
    /// 打字协程
    /// </summary>
    /// <param name="fullText"></param>
    /// <returns></returns>
    IEnumerator typeText(string fullText)
    {
        isTyping = true;
        fullText = nowData.content_cn;
        string nowText = "";
        for (int i = 0; i < fullText.Length + 1; i++)
        {
            nowText = fullText.Substring(0, i);
            contentText.text = nowText;
            yield return new WaitForSeconds(0.02f);

        }

        isTyping = false;

    }
    /// <summary>
    /// 当正在打字时按下左键，快速结束打字
    /// </summary>
    public void EndTypingText()
    {
        StopCoroutine(typingCoroutine);
        contentText.text = nowData.content_cn;
        isTyping = false;

    }
    /// <summary>
    /// 下一句对话
    /// </summary>
    public void NextDialog()
    {
        if (nowData.isLast)
        {
            EndDialog();
            return;
        }
        nowId++;
        RefreshText();
    }
    /// <summary>
    /// 结束对话
    /// </summary>
    public void EndDialog()
    {
        SceneObjectsManager.instance.BattleUI.SetActive(true);

        AllPanel.SetActive(false);
        isDuringDilaog = false;
        nowData = null;
        nowId = 0;
        //selections.Clear();
        //selectionIndex = 0;

    }
    /// <summary>
    /// 对话事件
    /// </summary>
    /// <param name="eventId"></param>
    public void DialogEvent(string eventId)
    {
        if (eventId == "1")
        {
            leftStand.color = new Color(1, 1, 1, 1);
            rightStand.color = new Color(1, 1, 1, 0.5f);
        }
        if (eventId == "2")
        {
            leftStand.color = new Color(1, 1, 1, 0.5f);
            rightStand.color = new Color(1, 1, 1, 1);
        }
        if (eventId == "3")
        {
            leftStand.color = new Color(1, 1, 1, 0.5f);
            rightStand.color = new Color(1, 1, 1, 0.5f);
        }
    }
    //选项相关方法
    public void ChooseOption(int targetId)
    {
        Debug.Log("目标id" + targetId);

        nowId = targetId;
        isOpinioning = false;
        Debug.Log("选择了选项");

        RefreshText();

    }

    /// <summary>
    /// 展示历史对话
    /// 激活历史对话面板即可，在其中的脚本中，当激活时便会创造历史对话
    /// </summary>
    public void ShowHistoryDialogs()
    {
        if (!isShowingHistory)
        {
            isShowingHistory = true;
            HistoryDialogPanel.SetActive(true);
            CreateHistoryDialogs();
            return;
        }
        else
        {
            isShowingHistory = false;
            HistoryDialogPanel.SetActive(false);
            DestoryHistoryDialogs();
            return;
        }
    }
    #endregion

    /// <summary>
    /// 根据现在的对话id创建新的历史对话记录
    /// </summary>
    public void AddHistoryTextByNowId()
    {
        HistoryDialogInformatioin hi = new HistoryDialogInformatioin(nowId, nowData.speakerNmae, nowData.content_cn, Color.white);
        historyDialogs.Add(hi);
    }
    /// <summary>
    /// 根据选项创建新的历史对话记录
    /// </summary>
    /// <param name="btn"></param>
    public void AddHistoryTextByOption(Button btn)
    {
        HistoryDialogInformatioin hi = new HistoryDialogInformatioin(nowId, "选项", btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text, Color.red);
        historyDialogs.Add(hi);
    }
    /// <summary>
    /// 遍历收集了的历史对话信息，生成所有的历史对话对象
    /// </summary>
    public void CreateHistoryDialogs()
    {
        if (historyDialogs.Count != 0)
            foreach (HistoryDialogInformatioin hi in historyDialogs)
            {
                //在正确的父物体下生成单个的历史对话表现对象
                HistoryDialogSlot slot = GameObject.Instantiate(historyDialogSlot, historyDialogParent).GetComponent<HistoryDialogSlot>();
                //根据数据赋值其各个文字和颜色
                slot.nameText.text = hi.speaker;
                slot.contentText.text = hi.contentText;
                slot.nameText.color = hi.textColor;
                slot.contentText.color = hi.textColor;
            }
    }
    public void DestoryHistoryDialogs()
    {
        if (historyDialogParent.childCount != 0)
            foreach (Transform t in historyDialogParent.transform)
                Destroy(t.gameObject);
    }
}

/// <summary>
/// 每句历史对话的信息
/// </summary>
[System.Serializable]
public class HistoryDialogInformatioin
{
    public int dialogId;
    public string speaker;
    public string contentText;
    public Color textColor;
    public HistoryDialogInformatioin(int id, string speaker, string content, Color color)
    {
        dialogId = id;
        this.speaker = speaker;
        contentText = content;
        textColor = color;
    }
}
