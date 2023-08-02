using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ������Ӣ����Ϣ�����ϵĽű���������ʾ Ui��Ӣ�ۼ�����ص���Ϣ
/// </summary>
public class HeroInformationUI : MonoBehaviour
{
    /// <summary>
    /// ��ǰ��Ӣ��
    /// </summary>
    public Entity hero;
    /// <summary>
    /// ��ǰ��Ӣ�۵ļ���
    /// </summary>
    public List<Skill> heroSkills;
    /// <summary>
    /// Ӣ�ۼ���ͼ��
    /// </summary>
    public Image[] skillImage;
    /// <summary>
    /// Ӣ�ۼ���ͼ������
    /// </summary>
    public Image[] skillImage_fill;

    /// <summary>
    /// ��ʼӢ����Ϣ���������
    /// </summary>
    public void SetHero(Entity hero)
    {
        //����Ӣ��
        this.hero = hero;
        //����Ӣ�ۼ���
        heroSkills = hero.parameter.character.skills;
        //����Ӣ�ۼ���ͼ��
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
        //���Ӣ�۵ļ����¼�
        skillImage_fill[0].GetComponent<Button>().onClick.AddListener(() => hero.UseSkill(0));
        skillImage_fill[1].GetComponent<Button>().onClick.AddListener(() => hero.UseSkill(1));
        skillImage_fill[2].GetComponent<Button>().onClick.AddListener(() => hero.UseSkill(2));
        skillImage_fill[3].GetComponent<Button>().onClick.AddListener(() => hero.UseSkill(3));
    }
    private void Update()
    {
        if (!hero)
            return;
        //����������ܵ����ֵ
        for (int i = 0; i < skillImage.Length; i++)
        {
            if (i < heroSkills.Count)
                skillImage_fill[i].fillAmount = heroSkills[i].waitTimer / heroSkills[i].waitTime;
        }
    }
}
