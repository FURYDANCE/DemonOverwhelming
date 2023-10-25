using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DemonOverwhelming;
/// <summary>
/// 历史对话界面的脚本
/// </summary>
public class HistoryDialogPanel : MonoBehaviour
{
    public List<int> ids;
    public DialogManager manager;
    public GameObject content;
    public HistoryDialogSlot slotObject;
 /// <summary>
 /// 在面板中创建历史对话信息
 /// </summary>
 /// <param name="name"></param>
 /// <param name="text"></param>
    public void CreateHistorySlot(string name, string text)
    {

        HistoryDialogSlot g = Instantiate(slotObject.gameObject).GetComponent<HistoryDialogSlot>();
        g.SetText(name, text);
        g.transform.SetParent(content.transform);

    }
    /// <summary>
    /// 在面板中创建历史选项信息
    /// </summary>
    /// <param name="text"></param>
    public void CreateOpinionSlot(string text)
    {
        HistoryDialogSlot g = Instantiate(slotObject.gameObject).GetComponent<HistoryDialogSlot>();
        g.SetSelection(text);
        g.transform.SetParent(content.transform);

    }
    /// <summary>
    /// 每次激活时重新生成
    /// </summary>
    private void OnEnable()
    {
        CerateAllHistory();
    }
    private void OnDisable()
    {
        ClearAllHistory();

    }
    public void CerateAllHistory()
    {
        for (int i = 0; i < ids.Count; i++)
        {
            //这里代表是做出的选项，不是对话
            if (ids[i] < 100)
            {
                CreateOpinionSlot(manager.selections[ids[i]]);
                continue;
            }
        
            PlotsData data = manager.GetRoleById(ids[i])[0];
            CreateHistorySlot(data.speakerNmae, data.content_cn);
        }
    }

    public void ClearAllHistory()
    {
        Debug.Log("clear");
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
   
      
    }
}
