using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// 调试界面，输出控制台的面板
/// </summary>
public class ConsoleTextPanel : MonoBehaviour
{
    public static ConsoleTextPanel Instance;

    public TextMeshProUGUI logText;
    public RectTransform content;

    private int count = 0;
    private Vector2 contentVe2 = new Vector2();
    StringBuilder MyStrBulder;
    private bool isUpdate = false;

    private bool isShow = false;

    private ConsoleTextPanel()
    {
        if (Instance == null)
            Instance = this;
    }

    private string strDebg = string.Empty;

    public void AddText(string str)
    {
        isUpdate = true;
        MyStrBulder.AppendFormat("{0}:{1}\n", count, str);
        count++;
        isUpdate = false;

    }
    // Use this for initialization
    void Awake()
    {
        MyStrBulder = new StringBuilder();

#if UNITY_5
            Application.logMessageReceived += HandleLog;  
#else
        Application.logMessageReceived += HandleLog;
#endif

    }

    void HandleLog(string message, string stackTrace, LogType type)
    {
        switch (type)
        {
            case LogType.Error:
                message = "<color=#FF0000>" + message + "</color>";
                break;
            case LogType.Assert:
                message = "<color=#0000ff>" + message + "</color>";
                break;
            case LogType.Warning:
                return;
            case LogType.Log:
                message = "<color=#000000>" + message + "</color>";
                break;
            case LogType.Exception:
                break;
            default:
                break;
        }

        AddText(message);
    }

    public void ShowHide()
    {
        isShow = !isShow;
        if (isShow)
        {
            transform.GetChild(0).localPosition = new Vector3(-9990, 0, 0);
        }
        else
        {
            transform.GetChild(0).localPosition = new Vector3(0, 0, 0);
        }
    }

    private bool isAdd = false;
    // Update is called once per frame
    void Update()
    {
        logText.text = MyStrBulder.ToString();
        LayoutRebuilder.ForceRebuildLayoutImmediate(logText.GetComponent<RectTransform>());
        //logText.text += MyStrBulder;
        //contentVe2.Set(0, 16f * count); 
        //content.sizeDelta = contentVe2;
    }

    //IEnumerator UpdateLayout(RectTransform rect)
    //{
    //    yield return new WaitForEndOfFrame();

    //    LayoutRebuilder.ForceRebuildLayoutImmediate(rect);

    //}
    public void clear()
    {
        MyStrBulder.Clear();
    }
}

