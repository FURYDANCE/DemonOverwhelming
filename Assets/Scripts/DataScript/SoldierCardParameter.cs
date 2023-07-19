using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
/// <summary>
/// 兵种卡的变量类
/// </summary>
[System.Serializable]
public class SoldierCardParameter 
{
    public string id;
    public string name;
    [JsonIgnore]
    public Sprite sprite;
    [JsonIgnore]
    public Sprite flagSprite;
    /// <summary>
    /// 金钱花费
    /// </summary>
    public float moneyCost;
    /// <summary>
    /// 血液花费
    /// </summary>
    public float bloodCost;
    /// <summary>
    /// 引用的阵型id
    /// </summary>
    public string formationId;
    /// <summary>
    /// 引用的士兵id
    /// </summary>
    public string soldierId;

    /// <summary>
    /// 存放兵种内容的预制件，通过id找到
    /// </summary>
    [JsonIgnore]
    public SoliderGroup content;

    public void SetValue(SoldierCardParameter data)
    {
        this.id = data.id;
        name = data.name;
        moneyCost = data.moneyCost;
        bloodCost = data.bloodCost;
        formationId = data.formationId;
        soldierId = data.soldierId;
    }
}
