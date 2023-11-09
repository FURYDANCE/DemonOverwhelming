using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace DemonOverwhelming
{


    /// <summary>
    /// ���ֿ�����UI�����½���ʾ������ʱ��BattleManager��ͨ������id����
    /// </summary>
    public class SoldierCard : MonoBehaviour
    {
        public SoldierCardParameter parameter;
        public Image image;
        public TextMeshProUGUI costText;
        public TextMeshProUGUI hpText;
        MouseHover_SoldierCard hoverScript;
        private void Start()
        {
            Generate();
            hoverScript = GetComponent<MouseHover_SoldierCard>();
            if (hoverScript)
                hoverScript.descriptionAndStory = GameDataManager.instance.GetEntityDataById(parameter.soldierId).character.descriptionAndStory;
        }
        /// <summary>
        /// ��ʼ����ͨ��id��ʾ��Ӧ����ֵ
        /// </summary>
        public void Generate()
        {
            parameter = GameDataManager.instance.GetSoldierCardById(parameter.id);
            image.sprite = parameter.sprite;
            costText.text = "Money   " + parameter.moneyCost.ToString();
            hpText.text = "Blood   " + parameter.bloodCost.ToString();
        }

        /// <summary>
        /// ����¼�
        /// </summary>
        public void SelectThis()
        {
            BattleManager.instance.SelectSoldierCard(this);
        }

    }
}