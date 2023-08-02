using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 挂载在英雄信息界面上的脚本，用于显示 Ui中英雄技能相关的信息
/// </summary>
public class HeroInformationUI : MonoBehaviour
{
    /// <summary>
    /// 当前的英雄
    /// </summary>
    public Entity hero;
    /// <summary>
    /// 当前的英雄的技能
    /// </summary>
    public List<Skill> heroSkills;
    /// <summary>
    /// 英雄技能图标
    /// </summary>
    public Image[] skillImage;
    /// <summary>
    /// 英雄技能图标的填充
    /// </summary>
    public Image[] skillImage_fill;

    /// <summary>
    /// 开始英雄信息界面的运作
    /// </summary>
    public void SetHero(Entity hero)
    {
        //设置英雄
        this.hero = hero;
        //设置英雄技能
        heroSkills = hero.parameter.character.skills;
        //设置英雄技能图标
        for (int i = 0; i < skillImage.Length; i++)
        {
            if (i < heroSkills.Count)
            {
                skillImage[i].sprite = heroSkills[i].skillIcon;
                skillImage_fill[i].sprite = heroSkills[i].skillIcon;
                skillImage_fill[i].GetComponent<MouseHover_Skill>().skill = heroSkills[i];


            }
            else
            {
                skillImage[i].color = new Color(1, 1, 1, 0);
                skillImage_fill[i].color = new Color(1, 1, 1, 0);
            }
            skillImage_fill[i].GetComponent<Button>().onClick.AddListener(() => hero.UseSkill(i));

        }
        //添加英雄的技能事件
        skillImage_fill[0].GetComponent<Button>().onClick.AddListener(() => hero.UseSkill(0));
        skillImage_fill[1].GetComponent<Button>().onClick.AddListener(() => hero.UseSkill(1));
        skillImage_fill[2].GetComponent<Button>().onClick.AddListener(() => hero.UseSkill(2));
        skillImage_fill[3].GetComponent<Button>().onClick.AddListener(() => hero.UseSkill(3));
    }
    private void Update()
    {
        if (!hero)
            return;
        //计算各个技能的填充值
        for (int i = 0; i < skillImage.Length; i++)
        {
            if (i < heroSkills.Count)
                skillImage_fill[i].fillAmount = heroSkills[i].waitTimer / heroSkills[i].waitTime;
        }
    }
}
