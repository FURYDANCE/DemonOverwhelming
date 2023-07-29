using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// ѡ��ʵ��֮�����ɵ�ʵ����Ϣҳ��
/// </summary>
public class UnitInformationUi : MonoBehaviour
{
    UnitParameter parameter;
    GameObject nowTargetObject;
    public HpShowUi hpInformation;
    public GameObject characterInformation;
    public GameObject buildingInformation;

    public TextMeshProUGUI nameText;
    public Image sprite;
    public TextMeshProUGUI basicInfoText;
    //public Image hpBar;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI skillText;
    public TextMeshProUGUI introductText;
    public TextMeshProUGUI info4Text;

    public TextMeshProUGUI damageText_Building;
    public TextMeshProUGUI defenceText_Building;
    string nowid;
    float startSpeed;
    [Header("��������")]
    bool hpLocked;
    bool speedLocked;
    public TextMeshProUGUI lockHpText;
    public TextMeshProUGUI lockPosText;

    private void Update()
    {
        UpdateInformation();

        //�����ͣ��ȥ��ʱ����Ĺ������еı���������Ϊ��ʱ��������������������UI
        if (EventSystem.current.IsPointerOverGameObject())
        {
            BattleManager.instance.mouseOveringInfoUI = true;
        }
        else
            BattleManager.instance.mouseOveringInfoUI = false;

    }
    /// <summary>
    /// ��ȡ���������Ϣ����ʾ��Ui��
    /// </summary>
    /// <param name="parameter"></param>
    /// <param name="infoGameObject"></param>
    public void SetInformation(UnitParameter parameter, GameObject infoGameObject)
    {
        nowTargetObject = infoGameObject;
        nowid = parameter.ID;

        this.parameter = parameter;
        nameText.text = "����:" + parameter.name;
        //hpBar.fillAmount = parameter.nowHp / parameter.Hp;
        sprite.sprite = parameter.sprite;
        introductText.text = parameter.introduct;
        if (parameter.type == EntityType.character)
        {
            characterInformation.SetActive(true);
            buildingInformation.SetActive(false);
            basicInfoText.text = "cost:" + parameter.character.cost + "\n" + "maxHp:" + parameter.Hp;
            speedText.text = "�ٶ�:" + parameter.character.moveSpeed;
            damageText.text = "�˺�:" + parameter.hurtDamage;
            skillText.text = "����:" + parameter.character.skillName;
            info4Text.text = "״̬��" + infoGameObject.GetComponent<CharacterStateManager>().currentState.ToString();
        }
        if (parameter.type == EntityType.building)
        {
            characterInformation.SetActive(false);
            buildingInformation.SetActive(true);
            basicInfoText.text = parameter.building.building_canAttack ? "�����Խ���" + "\n" + "maxHp:" + parameter.Hp : "�ݵ�" + "\n" + "maxHp:" + parameter.Hp;
            damageText.gameObject.SetActive(false);
            skillText.gameObject.SetActive(false);
            defenceText_Building.gameObject.SetActive(true);
            defenceText_Building.text = "������" + parameter.character.defence;
        }
        hpInformation.bufferBar.fillAmount = parameter.nowHp / parameter.Hp;
        startSpeed = parameter.character.moveSpeed;
        Debug_ShowLock();

    }
    public void UpdateInformation()
    {
        hpInformation.Refresh(parameter.nowHp, parameter.Hp);
        //hpBar.fillAmount = parameter.nowHp / parameter.Hp;
    }



    #region ���Թ���

    public void Debug_HpRecover()
    {
        parameter.nowHp = parameter.Hp;
    }
    public void Debug_ShowLock()
    {
        if (nowTargetObject.GetComponent<Entity>().hpLock == true)
        {
            hpLocked = true;
            lockHpText.text = "������������";
            return;
        }
        if (nowTargetObject.GetComponent<Entity>().hpLock == false)
        {
            hpLocked = false;
            lockHpText.text = "������������";

            return;
        }

        if (parameter.character.moveSpeed == 0)
        {
            lockPosText.text = "λ����������";
            speedLocked = true;
            return;
        }
        if (parameter.character.moveSpeed != 0)
        {
            lockPosText.text = "λ����������";
            speedLocked = false;
            return;
        }
    }
    public void Debug_HpLock()
    {
        if (!hpLocked)
        {
            hpLocked = true;
            lockHpText.text = "������������";
            nowTargetObject.GetComponent<Entity>().hpLock = true;
            return;
        }
        else
        {
            hpLocked = false;
            lockHpText.text = "������������";
            nowTargetObject.GetComponent<Entity>().hpLock = false;
            return;
        }
    }
    public void Debug_MoveLeft()
    {
        nowTargetObject.transform.position += Vector3.left * 10;
    }
    public void Debug_MoveRight()
    {
        nowTargetObject.transform.position += Vector3.right * 10;
    }
    public void KillTarget()
    {
        nowTargetObject.GetComponent<Entity>().Die();
    }
    public void Debug_SpeedLock()
    {
        if (!speedLocked)
        {
            speedLocked = true;

            lockPosText.text = "λ����������";
            parameter.character.moveSpeed = 0;
            return;
        }
        else
        {
            speedLocked = false;
            lockPosText.text = "λ����������";
            parameter.character.moveSpeed = startSpeed;
            return;
        }
    }
    /// <summary>
    /// �ӵ�λ��Ϣ�����Ӧ�������޸�
    /// </summary>
    public void Debug_IntoDebugger()
    {
        if (!DebuggerUi.instance.debuggerPanel.activeInHierarchy)
            DebuggerUi.instance.ShowDebugger();
        DebuggerUi.instance.selectGroup.SelectBtn(0);
        DebuggerUi.instance.searchIdField.text = parameter.ID;
        DebuggerUi.instance.SearchEntityParameterById();
        BattleManager.instance.ReleaseChoosedEntity();
        BattleManager.instance.DestoryNowUnitInformation();
    }
    #endregion
}
