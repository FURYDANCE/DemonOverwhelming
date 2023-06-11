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
    /// �����
    /// </summary>
    public GameObject AllPanel;
    /// <summary>
    /// Ŀǰ�����ĶԻ�����
    /// </summary>
    public PlotsData nowData;
    /// <summary>
    /// ��ʼid
    /// </summary>
    public int startId;
    /// <summary>
    /// ��ǰid
    /// </summary>
    public int nowId;
    /// <summary>
    /// ��ʷ�Ի�ͳ��
    /// </summary>
    public HistoryDialogPanel historyDialogPanel;
    /// <summary>
    /// ��������ʷ�Ի����ֶ���
    /// </summary>
    public GameObject historyDialogSlot;
    /// <summary>
    /// ������
    /// </summary>
    public Image leftStand;
    /// <summary>
    /// ������
    /// </summary>
    public Image rightStand;
    /// <summary>
    /// ��ʷ�Ի���ť
    /// </summary>
    public Button historyBtn;
    /// <summary>
    /// �����Ի���ť
    /// </summary>
    public Button skipBtn;
    /// <summary>
    /// �Ի����ݰ�ť
    /// </summary>
    public TextMeshProUGUI contentText;
    /// <summary>
    /// speaker����
    /// </summary>
    public TextMeshProUGUI nameText;
    /// <summary>
    /// ѡ�����
    /// </summary>
    public GameObject opinionPanel;
    /// <summary>
    /// ��ť
    /// </summary>
    public Button opinionBtn_1;
    public TextMeshProUGUI opinionBtn_1_text;
    public Button opinionBtn_2;
    public TextMeshProUGUI opinionBtn_2_text;
    /// <summary>
    /// ������ʾ��ʷ�Ի���ѡ��ı���
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
    /// ������ʱ�����ļ����Ի����ж�
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
    /// ���ģ���ȡ�Ի���Ϣ
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
    /// ��ȡ������Ϣ
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
    /// ��ʼ�Ի�
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
    /// ���ݻ�ȡ���ĶԻ���Ϣˢ������ϵ���ʾ
    /// </summary>
    public void RefreshText()
    {

        opinionPanel.SetActive(false);
        leftStand.color = new Color(1, 1, 1, 1);
        rightStand.color = new Color(1, 1, 1, 1);
        leftStand.sprite = null;
        rightStand.sprite = null;
        nowData = GetRoleById(nowId)[0];
        //���ֻ�Э��
        typingCoroutine = StartCoroutine(typeText(nowData.content_cn));
        //��ȡ����
        nameText.text = nowData.speakerNmae;
        //��ȡ��ͼ
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
        //��ȡѡ��
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
        //�Ի��¼�
        {
            DialogEvent(nowData.event_1);
            DialogEvent(nowData.event_2);
            DialogEvent(nowData.event_3);
            DialogEvent(nowData.event_4);
        }
       
    }
   

    /// <summary>
    /// ����Э��
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
    /// �����ڴ���ʱ������������ٽ�������
    /// </summary>
    public void EndTypingText()
    {
        contentText.text = nowData.content_cn;
        isTyping = false;

    }
    /// <summary>
    /// ��һ��Ի�
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
    /// �����Ի�
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
    /// �Ի��¼�
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
    //ѡ����ط���
    public void ChooseOpinion(int targetId)
    {
        Debug.Log("Ŀ��id"+targetId);

        nowId = targetId;
        isOpinioning = false;
        Debug.Log("ѡ����ѡ��");

        RefreshText();

    }
    public void ChooseOpinion_1()
    {
        Debug.Log("ѡ����ѡ��1");
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
    /// չʾ��ʷ�Ի�
    /// ������ʷ�Ի���弴�ɣ������еĽű��У�������ʱ��ᴴ����ʷ�Ի�
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
