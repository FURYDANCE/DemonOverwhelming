using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ʵ�ּ��ܾ������Ч���Ļ���
/// </summary>
public interface SkillBase 
{
    public void OnStart(Entity thisEntity);

    public void OnUse(Entity thisEntity,Skill thisSkill);

    public void OnUsing();

    public void OnEnd();

    public void OnUpdate();
}
