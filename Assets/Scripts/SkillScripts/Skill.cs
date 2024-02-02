using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{

    [System.Serializable]
    public class Skill
    {
        public string id;
        public string skillName;
        public int skillLevel;
        /// <summary>
        /// ����ʵ�ּ��ܵĽű���ͨ�������������жϻ�ȡ��
        /// </summary>
        public SkillBase skillBase;
        public float waitTime;
        public float waitTimer;
        public float bloodCost;
        public float moneyCost;
        public Sprite skillIcon;
        public string[] eachLevelDescriptions;
        public void SetValue(Skill skill)
        {
            id = skill.id;
            skillName = skill.skillName;
            skillLevel = skill.skillLevel;
            waitTime = skill.waitTime;
            bloodCost = skill.bloodCost;
            moneyCost = skill.moneyCost;
            skillIcon = skill.skillIcon;
            eachLevelDescriptions = skill.eachLevelDescriptions;
        }
        public void SkillUesd()
        {
            waitTimer = 0;
        }
    }
}