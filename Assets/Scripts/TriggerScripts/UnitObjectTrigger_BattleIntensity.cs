using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{
    public class UnitObjectTrigger_BattleIntensity : UnitObjectTriggerBase
    {
        [Header("����������������������������������������������������������������������������������������������������������������������������������������������������")]

        [Header("�Ҷ����ӵĵȼ�")]
        public float intensityAddAmount;

        public override void OnTrigger()
        {
            base.OnTrigger();
            WaveManager.instance.AddBattleIntensity(intensityAddAmount);
        }

    }
}