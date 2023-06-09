using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 兵种卡，在UI的右下角显示，生成时在BattleManager中通过传入id生成
/// </summary>
public class SoldierCard : MonoBehaviour
{
    public SoldierCardParameter parameter;
    public Image image;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI hpText;

    private void Start()
    {
        Generate();
    }
    /// <summary>
    /// 初始化，通过id显示对应的数值
    /// </summary>
    public void Generate()
    {
        parameter = GameDataManager.instance.GetSoldierCardById(parameter.id);
        image.sprite = parameter.sprite;
        costText.text = "Money   " + parameter.moneyCost.ToString();
        hpText.text = "Blood   " + parameter.bloodCost.ToString();
    }
   
    /// <summary>
    /// 点击事件
    /// </summary>
    public void SelectThis()
    {
        BattleManager.instance.SelectSoldierCard(this);
    }

}
