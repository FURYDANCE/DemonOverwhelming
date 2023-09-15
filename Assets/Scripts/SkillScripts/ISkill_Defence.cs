using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{


    public class ISkill_Defence : SkillBase
    {

        public int level;
        public void OnEnd()
        {

        }

        public void OnStart(Entity thisEntity)
        {
            Debug.Log("触发被动技能的设置");
            thisEntity.character_SpecialTagBases.Add(new Character_SpecialTag_Parry(25));
        }

        public void OnUse(Entity thisEntity, Skill thisSkill)
        {
            Debug.Log("该技能为非主动技能");
        }

        public void OnUsing()
        {

        }

        public void OnUpdate()
        {

        }
    }
}