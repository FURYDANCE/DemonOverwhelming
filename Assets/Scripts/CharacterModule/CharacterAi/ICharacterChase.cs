using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 范围内发现敌人之后的进一步行为脚本：如：追击敌人、原地待命
/// 一般情况下当敌人的距离小于等于攻击距离后，就该进入攻击状态中，若敌人的距离大于检测距离，将检测到的该敌人设为空，当范围内没有任何敌人时，重新回到行动状态中
/// </summary>
public interface ICharacterChase 
{
    public void Chase();
}