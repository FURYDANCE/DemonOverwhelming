using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// 选择实体之后生成的实体信息页面
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
    [Header("调试区域")]
    bool hpLocked;
    bool speedLocked;
    public TextMeshProUGUI lockHpText;
    public TextMeshProUGUI lockPosText;

    private void Update()
    {
        UpdateInformation();

        //鼠标悬停上去的时候更改管理器中的变量，作用为这时候按下鼠标左键不会消除该UI
        if (EventSystem.current.IsPointerOverGameObject())
        {
            BattleManager.instance.mouseOveringInfoUI = true;
        }
        else
            BattleManager.instance.mouseOveringInfoUI = false;

    }
    /// <summary>
    /// 获取到对象的信息，显示在Ui中
    /// </summary>
    /// <param name="parameter"></param>
    /// <param name="infoGameObject"></param>
    public void SetInformation(UnitParameter parameter, GameObject infoGameObject)
    {
        nowTargetObject = infoGameObject;
        nowid = parameter.ID;

        this.parameter = parameter;
        nameText.text = "名称:" + parameter.name;
        //hpBar.fillAmount = parameter.nowHp / parameter.Hp;
        sprite.sprite = parameter.sprite;
        introductText.text = parameter.introduct;
        if (parameter.type == EntityType.character)
        {
            characterInformation.SetActive(true);
            buildingInformation.SetActive(false);
            basicInfoText.text = "cost:" + parameter.character.cost + "\n" + "maxHp:" + parameter.Hp;
            speedText.text = "速度:" + parameter.character.moveSpeed;
            damageText.text = "伤害:" + parameter.hurtDamage;
            skillText.text = "技能:" + parameter.character.skillName;
            info4Text.text = "状态：" + infoGameObject.GetComponent<CharacterStateManager>().currentState.ToString();
        }
        if (parameter.type == EntityType.building)
        {
            characterInformation.SetActive(false);
            buildingInformation.SetActive(true);
            basicInfoText.text = parameter.building.building_canAttack ? "防御性建筑" + "\n" + "maxHp:" + parameter.Hp : "据点" + "\n" + "maxHp:" + parameter.Hp;
            damageText.gameObject.SetActive(false);
            skillText.gameObject.SetActive(false);
            defenceText_Building.gameObject.SetActive(true);
            defenceText_Building.text = "防御：" + parameter.character.defence;
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



    #region 调试功能

    public void Debug_HpRecover()
    {
        parameter.nowHp = parameter.Hp;
    }
    public void Debug_ShowLock()
    {
        if (nowTargetObject.GetComponent<Entity>().hpLock == true)
        {
            hpLocked = true;
            lockHpText.text = "生命锁定：开";
            return;
        }
        if (nowTargetObject.GetComponent<Entity>().hpLock == false)
        {
            hpLocked = false;
            lockHpText.text = "生命锁定：关";

            return;
        }

        if (parameter.character.moveSpeed == 0)
        {
            lockPosText.text = "位置锁定：开";
            speedLocked = true;
            return;
        }
        if (parameter.character.moveSpeed != 0)
        {
            lockPosText.text = "位置锁定：关";
            speedLocked = false;
            return;
        }
    }
    public void Debug_HpLock()
    {
        if (!hpLocked)
        {
            hpLocked = true;
            lockHpText.text = "生命锁定：开";
            nowTargetObject.GetComponent<Entity>().hpLock = true;
            return;
        }
        else
        {
            hpLocked = false;
            lockHpText.text = "生命锁定：关";
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

            lockPosText.text = "位置锁定：开";
            parameter.character.moveSpeed = 0;
            return;
        }
        else
        {
            speedLocked = false;
            lockPosText.text = "位置锁定：关";
            parameter.character.moveSpeed = startSpeed;
            return;
        }
    }
    /// <summary>
    /// 从单位信息进入对应的数据修改
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
