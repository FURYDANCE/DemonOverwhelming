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
            Debug.Log("�����������ܵ�����");
            thisEntity.character_SpecialTagBases.Add(new Character_SpecialTag_Parry(25));
        }

        public void OnUse(Entity thisEntity, Skill thisSkill)
        {
            Debug.Log("�ü���Ϊ����������");
        }

        public void OnUsing()
        {

        }

        public void OnUpdate()
        {

        }
    }
}