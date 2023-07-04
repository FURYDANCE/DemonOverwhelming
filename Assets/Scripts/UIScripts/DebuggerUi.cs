using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// �����ڵ���UI�ϵĽű�����ŵ��Է���
/// </summary>
public class DebuggerUi : MonoBehaviour
{
    public GameObject consolePanel;
    public GameObject debuggerPanel;

    public TMP_InputField mps_Text;
    public TMP_InputField mgs_Text;
    public TMP_InputField Blood_Text;




    public TMP_InputField searchIdField;
    public TMP_InputField searchIdField_Card;
    public TMP_InputField searchIdField_Missile;
    public UnitParameter searchedEntity;
    public SoldierCardParameter searchedCard;
    public UnitParameter_Missile searchedMissile;


    [Header("��λ")]
    public TMP_InputField nameField;
    public TMP_InputField hpField;
    public TMP_InputField damageField;
    public TMP_InputField sizeField;
    public TMP_InputField hpSizeField;
    public TMP_InputField hpOffsetField;
    public TMP_InputField defenceField;
    public TMP_InputField speedField;
    public TMP_InputField atkTimeField;
    public TMP_InputField atkWaitTimeField;
    public TMP_InputField atkDistanceField;
    public TMP_InputField checkAreaField;
    public TMP_InputField checkAreaOffsetField;
    public TMP_InputField aiTypeField;
    public TMP_InputField missileIdField;
    public TMP_InputField ShadowField;
    public Image entitySprite;

    [Header("����")]
    public TMP_InputField card_idField;
    public TMP_InputField card_nameField;
    public Image card_flagImage;
    public Image card_Image;
    public TMP_InputField card_moneyCostField;
    public TMP_InputField card_bloodCostField;
    [Header("Ͷ����")]
    public TMP_InputField missile_idField;
    public TMP_InputField missile_DamageField;
    public TMP_InputField missile_speedField;
    public TMP_InputField missile_nameField;
    public TMP_InputField missile_moveTypeField;
    public Image missile_sprite;

    public UI_SelectGroup selectGroup;

    public static DebuggerUi instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }

    private void Start()
    {
        ShowMoneyAndCost();
    }




    public void ShowMoneyAndCost()
    {
        mps_Text.text = BattleManager.instance.moneyPerSecond.ToString();
        mgs_Text.text = BattleManager.instance.moneyAddAmount.ToString();
        Blood_Text.text = BattleManager.instance.blood.ToString();
    }
    public void SetMonetAndCost()
    {
        BattleManager.instance.moneyPerSecond = float.Parse(mps_Text.text);
        BattleManager.instance.moneyAddAmount = float.Parse(mgs_Text.text);
        BattleManager.instance.blood = float.Parse(Blood_Text.text);
        //BattleManager.instance.StartAddMoney();
    }
    public void ShowConsole()
    {
        if (consolePanel.activeInHierarchy)
        {
            consolePanel.SetActive(false);
            return;
        }
        else
        {
            consolePanel.SetActive(true);
            return;
        }
    }
    public void ShowDebugger()
    {
        if (debuggerPanel.activeInHierarchy)
        {
            debuggerPanel.SetActive(false);
            return;
        }
        else
        {
            debuggerPanel.SetActive(true);
            return;
        }
    }
    public void SearchEntityParameterById()
    {
        searchedEntity = GameDataManager.instance.GetEntityDataById(searchIdField.text);
        if (searchedEntity == null)
        {
            Debug.Log("û��������ʵ��");
            return;
        }
        nameField.text = searchedEntity.name;
        hpField.text = searchedEntity.Hp.ToString();
        damageField.text = searchedEntity.hurtDamage.ToString();
        sizeField.text = searchedEntity.modleSize.ToString();
        hpSizeField.text = searchedEntity.hpBarSize.ToString();
        hpOffsetField.text = searchedEntity.hpBarOffset.ToString().Replace(")", "").Replace("(", "");
        defenceField.text = searchedEntity.character.defence.ToString();
        speedField.text = searchedEntity.character.moveSpeed.ToString();
        atkTimeField.text = searchedEntity.character.attackTime.ToString();
        atkWaitTimeField.text = searchedEntity.character.attackWaitTime.ToString();
        atkDistanceField.text = searchedEntity.character.attackDistance.ToString();
        checkAreaField.text = searchedEntity.character.EnemyCheckArea.ToString().Replace(")", "").Replace("(", "");
        checkAreaOffsetField.text = searchedEntity.character.EnemyCheckOffset.ToString().Replace(")", "").Replace("(", "");
        aiTypeField.text = searchedEntity.character.aiType.ToString();
        missileIdField.text = searchedEntity.character.missileId.ToString();
        entitySprite.sprite = searchedEntity.sprite;
        //ȥ��Ӱ�Ӵ�Сv3����λ���vector2
        string newText = searchedEntity.shadowSize.ToString().Replace(searchedEntity.shadowSize.ToString().Split(",")[2], "");
        //��ȥ�����Ķ��ţ�֮�����y��ƫ�ƣ��õ�Ӧ�еĽ��
        ShadowField.text = (newText.Remove(newText.Length - 1)
            + "," + searchedEntity.shadowOffset.ToString()).Replace(")", "").Replace("(", "");
    }
    public void SaveEntityParameter()
    {
        if (searchedEntity == null || searchedEntity.name == "")
        {
            Debug.Log("��û����������λ");
            return;
        }
        searchedEntity.name = nameField.text;
        searchedEntity.Hp = float.Parse(hpField.text);
        searchedEntity.hurtDamage = float.Parse(damageField.text);
        searchedEntity.modleSize = float.Parse(sizeField.text);
        searchedEntity.hpBarSize = float.Parse(hpSizeField.text);
        searchedEntity.hpBarOffset = new Vector3(float.Parse(hpOffsetField.text.Split(",")[0]), float.Parse(hpOffsetField.text.Split(",")[1]), 0);
        searchedEntity.character.defence = float.Parse(defenceField.text);
        searchedEntity.character.moveSpeed = float.Parse(speedField.text);
        searchedEntity.character.attackTime = float.Parse(atkTimeField.text);
        searchedEntity.character.attackWaitTime = float.Parse(atkWaitTimeField.text);
        searchedEntity.character.attackDistance = float.Parse(atkDistanceField.text);
        searchedEntity.character.EnemyCheckArea = new Vector3(float.Parse(checkAreaField.text.Split(",")[0]), float.Parse(checkAreaField.text.Split(",")[1]));
        searchedEntity.character.EnemyCheckOffset = new Vector3(float.Parse(checkAreaOffsetField.text.Split(",")[0]), float.Parse(checkAreaOffsetField.text.Split(",")[1]));
        searchedEntity.character.aiType = (AiType)System.Enum.Parse(typeof(AiType), aiTypeField.text);
        searchedEntity.character.missileId = missileIdField.text;
        searchedEntity.shadowSize = new Vector3(float.Parse(ShadowField.text.Split(",")[0]), float.Parse(ShadowField.text.Split(",")[1]), 0);
        searchedEntity.shadowOffset = float.Parse(ShadowField.text.Split(",")[2]);
        ////�޸�������֮�󣬱��������ϵ�ʿ�����޸�������
        //foreach(Entity e in BattleManager.instance.allSoldiers)
        //{
        //    if (e.parameter.ID == searchIdField.text)
        //    {
        //        e.SetEntityParameter();
        //    }
        //    Debug.Log("������ʿ���������޸�");
        //}
      
        Debug.Log("��λ�������");
        SaveChanges();
    }

    public void SearchSoldierCardById()
    {
        searchedCard = GameDataManager.instance.GetSoldierCardById(card_idField.text);
        if (searchedCard == null)
        {
            Debug.Log("û������������");
            return;
        }
        card_nameField.text = searchedCard.name;
        //card_costField.text = searchedCard.cost.ToString();
        card_moneyCostField.text = searchedCard.moneyCost.ToString();
        card_bloodCostField.text = searchedCard.bloodCost.ToString();
        card_Image.sprite = searchedCard.sprite;
        card_flagImage.sprite = searchedCard.flagSprite;
    }
    public void SaveSoldierCardParameter()
    {
        if (searchedCard == null || searchedCard.name == "")
        {
            Debug.Log("��û������������");
            return;
        }
        searchedCard.name = card_nameField.text;
        //searchedCard.cost = float.Parse(card_costField.text);
        searchedCard.moneyCost = float.Parse(card_moneyCostField.text);
        searchedCard.bloodCost = float.Parse(card_bloodCostField.text);
        BattleManager.instance.ClearSoldierCard();
        BattleManager.instance.CreateOneSoldierCard("1000001");
        BattleManager.instance.CreateOneSoldierCard("1000002");
        Debug.Log("���Ƹ������");
        SaveChanges();

    }

    public void SearchMissileById()
    {
        searchedMissile = GameDataManager.instance.GetMissileDataById(missile_idField.text);
        if (searchedMissile == null)
        {
            Debug.Log("û��������Ͷ����");
            return;
        }
        //missile_DamageField.text = searchedMissile.damage.ToString();
        missile_speedField.text = searchedMissile.speed.ToString();
        missile_nameField.text = searchedMissile.name.ToString();
        missile_moveTypeField.text = searchedMissile.moveType.ToString();
        missile_sprite.sprite = searchedMissile.sprite;
      

    }
    public void SaveMissileParameter()
    {
        if (searchedMissile == null || searchedMissile.name == "")
        {
            Debug.Log("��û��������Ͷ����");
            return;
        }
        //searchedMissile.damage = float.Parse(missile_DamageField.text);
        searchedMissile.speed = float.Parse(missile_speedField.text);
        searchedMissile.name = missile_nameField.text;
        searchedMissile.moveType = (MissileMoveType)System.Enum.Parse(typeof(MissileMoveType), missile_moveTypeField.text);
        Debug.Log("Ͷ����������");
        SaveChanges();
    }
    public void SaveChanges()
    {
        GameDataManager.instance.SaveJson(Application.persistentDataPath, JsonEditor.unitFileName, GameDataManager.instance.excelDatas.unitDatas);
        GameDataManager.instance.SaveJson(Application.persistentDataPath, JsonEditor.cardFileName, GameDataManager.instance.excelDatas.cardDatas);
        GameDataManager.instance.SaveJson(Application.persistentDataPath, JsonEditor.missileFileName, GameDataManager.instance.excelDatas.missileDatas);
        Debug.Log("�����ļ��������");
    }

}
