using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 实现技能具体表现效果的基类
/// </summary>
public interface SkillBase 
{
    public void OnStart(Entity thisEntity);

    public void OnUse(Entity thisEntity,Skill thisSkill);

    public void OnUsing();

    public void OnEnd();

    public void OnUpdate();
}
