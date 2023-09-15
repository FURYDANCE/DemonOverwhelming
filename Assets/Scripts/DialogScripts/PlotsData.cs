using UnityEngine;
using DemonOverwhelming;
[System.Serializable]
public class PlotsData
{
    public int id;                          //对话id
    public string content_cn;               //对话内容
    public string speakerNmae;              //说话者名字
    public bool isLast;                     //是否为结束
    public Sprite left_stand;               //左立绘
    public Sprite right_stand;              //右立绘
    public string event_1;                  //对话事件
    public string event_2;
    public string event_3;
    public string event_4;
    public bool haveOption;                 //是否有选项
    public string optionContent_1;          //选项1内容
    public string optionContent_2;          //选项2内容
    public int option_1_targetId;           //选项1跳转id
    public int option_2_targetId;           //选项2跳转id
}
