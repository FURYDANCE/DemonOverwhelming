using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[System.Serializable]
public class StandParameter
{
    public string id;
    public Sprite sprite;
}


public class DialogManager : MonoBehaviour
{
    public List<StandParameter> standDatas;
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
    /// <summary>
    /// 历史对话统计
    /// </summary>
    public HistoryDialogPanel historyDialogPanel;
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
    /// <summary>
    /// 选项面板
    /// </summary>
    public GameObject opinionPanel;
    /// <summary>
    /// 按钮
    /// </summary>
    public Button opinionBtn_1;
    public TextMeshProUGUI opinionBtn_1_text;
    public Button opinionBtn_2;
    public TextMeshProUGUI opinionBtn_2_text;
    /// <summary>
    /// 用于显示历史对话中选项的变量
    /// </summary>
    [HideInInspector]
    public int selectionIndex;
    public List<string> selections;
    Coroutine typingCoroutine;
    bool isDuringDilaog;
    bool isTyping;
    bool isOpinioning;
    bool isShowingHistory;
    string assetName = "DataTables/PlotsDatas";
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            startDialog(1000001);
        }


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
            if (!isOpinioning )
            {
                NextDialog();
            }
        }

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
    /// <summary>
    /// 获取立绘信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Sprite GetStandById(string id)
    {

        //Debug.Log(asset.name);
        StandParameter find = standDatas.Find((StandParameter p) => { return p.id == id; });
        return find.sprite;
    }
    /// <summary>
    /// 开始对话
    /// </summary>
    /// <param name="startId"></param>
    public void startDialog(int startId)
    {
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

        opinionPanel.SetActive(false);
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
            if (nowData.left_stand == "0")
                leftStand.color = new Color(0, 0, 0, 0);
            else
            {
                leftStand.sprite = GetStandById(nowData.left_stand);
            }
            if (nowData.right_stand == "0")
                rightStand.color = new Color(0, 0, 0, 0);
            else
                rightStand.sprite = GetStandById(nowData.right_stand);
        }
        //获取选项
        if (nowData.haveOption)
        {
            opinionBtn_1.onClick.RemoveAllListeners();
            opinionBtn_2.onClick.RemoveAllListeners();
            isOpinioning = true;
            opinionPanel.SetActive(true);
            opinionBtn_1_text.text = nowData.optionContent_1;
            opinionBtn_1.onClick.AddListener(ChooseOpinion_1);
            opinionBtn_2_text.text = nowData.optionContent_2;
            opinionBtn_2.onClick.AddListener(ChooseOpinion_2);

        }
        historyDialogPanel.ids.Add(nowId);
        //对话事件
        {
            DialogEvent(nowData.event_1);
            DialogEvent(nowData.event_2);
            DialogEvent(nowData.event_3);
            DialogEvent(nowData.event_4);
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
        AllPanel.SetActive(false);
        isDuringDilaog = false;
        nowData = null;
        nowId = 0;
        selections.Clear();
        selectionIndex = 0;
        historyDialogPanel.ids.Clear();
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
    public void ChooseOpinion(int targetId)
    {
        Debug.Log("目标id"+targetId);

        nowId = targetId;
        isOpinioning = false;
        Debug.Log("选择了选项");

        RefreshText();

    }
    public void ChooseOpinion_1()
    {
        Debug.Log("选择了选项1");
        historyDialogPanel.ids.Add(selectionIndex);
        selections.Add(nowData.optionContent_1);
        selectionIndex++;
        ChooseOpinion(nowData.option_1_targetId);
       
    }
    public void ChooseOpinion_2()
    {
        historyDialogPanel.ids.Add(selectionIndex);
      
        selections.Add(nowData.optionContent_2);
        selectionIndex++;
        ChooseOpinion(nowData.option_2_targetId);
        
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
            historyDialogPanel.gameObject.SetActive(true);
            return;
        }
        else
        {
            isShowingHistory = false;
            historyDialogPanel.gameObject.SetActive(false);
            return;
        }
    }
}
