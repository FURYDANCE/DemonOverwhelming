using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DemonOverwhelming;
/// <summary>
/// �Ի�������
/// ͨ��startDialog����������Ի���ʼ��id��ִ�жԻ�
/// </summary>
public class DialogManager : MonoBehaviour
{
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
    ///// <summary>
    ///// ��ʷ�Ի�ͳ��
    ///// </summary>
    //public HistoryDialogPanel historyDialogPanel;
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
    ///// <summary>
    ///// ѡ�����
    ///// </summary>
    //public GameObject opinionPanel;

    /// <summary>
    /// ��ť
    /// </summary>
    [Header("��ť����")]
    public GameObject optionBtn;
    [Header("��ť�ĸ�����")]
    public Transform optionParent;
    //public TextMeshProUGUI opinionBtn_1_text;
    //public Button opinionBtn_2;
    //public TextMeshProUGUI opinionBtn_2_text;
    ///// <summary>
    ///// ������ʾ��ʷ�Ի���ѡ��ı���
    ///// </summary>
    //[HideInInspector]
    //public int selectionIndex;
    //public List<string> selections;
    [Header("��ʷ�Ի����")]
    public GameObject HistoryDialogPanel;
    /// <summary>
    /// ��ǰ�����ռ��˵���ʷ�Ի���Ϣ
    /// </summary>
    [Header("��ǰ�����ռ��˵���ʷ�Ի���Ϣ")]
    public List<HistoryDialogInformatioin> historyDialogs;
    /// <summary>
    /// ��ʷ�Ի���Ϣ�ĸ�����
    /// </summary>
    [Header("��ʷ�Ի���Ϣ�ĸ�����")]
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
    #region �Ի�����


    /// <summary>
    /// ��ʼ�Ի�
    /// </summary>
    /// <param name="startId"></param>
    public void startDialog(int startId)
    {
        //ÿ�ο�ʼ�Ի�ʱ�����ʷ�Ի���¼
        historyDialogs.Clear();
        DestoryHistoryDialogs();

        //����ս��UI
        SceneObjectsManager.instance.BattleUI.SetActive(false);
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
     
        //�����ť�������������������Ϊ����ݻ����а�ť
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
        //���ֻ�Э��
        typingCoroutine = StartCoroutine(typeText(nowData.content_cn));
        //��ȡ����
        nameText.text = nowData.speakerNmae;
        //��ȡ��ͼ
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
        //��ȡѡ��
        if (nowData.haveOption)
        {
            //������ťѡ�����ݲ������ɰ�ť��������������������
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

        //�Ի��¼�
        {
            foreach (string s in nowData.events)
                DialogEvent(s);
        }
        //�����ʷ�Ի���¼
        AddHistoryTextByNowId();
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
            if (!isOpinioning)
            {
                NextDialog();
            }
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
        StopCoroutine(typingCoroutine);
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
        SceneObjectsManager.instance.BattleUI.SetActive(true);

        AllPanel.SetActive(false);
        isDuringDilaog = false;
        nowData = null;
        nowId = 0;
        //selections.Clear();
        //selectionIndex = 0;

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
    public void ChooseOption(int targetId)
    {
        Debug.Log("Ŀ��id" + targetId);

        nowId = targetId;
        isOpinioning = false;
        Debug.Log("ѡ����ѡ��");

        RefreshText();

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
    /// �������ڵĶԻ�id�����µ���ʷ�Ի���¼
    /// </summary>
    public void AddHistoryTextByNowId()
    {
        HistoryDialogInformatioin hi = new HistoryDialogInformatioin(nowId, nowData.speakerNmae, nowData.content_cn, Color.white);
        historyDialogs.Add(hi);
    }
    /// <summary>
    /// ����ѡ����µ���ʷ�Ի���¼
    /// </summary>
    /// <param name="btn"></param>
    public void AddHistoryTextByOption(Button btn)
    {
        HistoryDialogInformatioin hi = new HistoryDialogInformatioin(nowId, "ѡ��", btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text, Color.red);
        historyDialogs.Add(hi);
    }
    /// <summary>
    /// �����ռ��˵���ʷ�Ի���Ϣ���������е���ʷ�Ի�����
    /// </summary>
    public void CreateHistoryDialogs()
    {
        if (historyDialogs.Count != 0)
            foreach (HistoryDialogInformatioin hi in historyDialogs)
            {
                //����ȷ�ĸ����������ɵ�������ʷ�Ի����ֶ���
                HistoryDialogSlot slot = GameObject.Instantiate(historyDialogSlot, historyDialogParent).GetComponent<HistoryDialogSlot>();
                //�������ݸ�ֵ��������ֺ���ɫ
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
/// ÿ����ʷ�Ի�����Ϣ
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
