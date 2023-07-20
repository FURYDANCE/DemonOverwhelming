using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
/// <summary>
/// ���ֿ��ı�����
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
    /// ��Ǯ����
    /// </summary>
    public float moneyCost;
    /// <summary>
    /// ѪҺ����
    /// </summary>
    public float bloodCost;
    /// <summary>
    /// ���õ�����id
    /// </summary>
    public string formationId;
    /// <summary>
    /// ���õ�ʿ��id
    /// </summary>
    public string soldierId;

    /// <summary>
    /// ��ű������ݵ�Ԥ�Ƽ���ͨ��id�ҵ�
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
