using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// 单个的历史对话保存对象
/// </summary>
public class HistoryDialogSlot : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI contentText;
    public void SetText(string name, string content)
    {
        nameText.text = name;
        contentText.text = content;
    }
    public void SetSelection(string content)
    {
        nameText.text = "Select";
        contentText.text = content;
        nameText.color = Color.red;
        contentText.color = Color.red;
    }
}
