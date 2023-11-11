using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{
    public class UnitObjectTrigger_BattleIntensity : UnitObjectTriggerBase
    {
        [Header("！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！")]

        [Header("倉業奐紗議吉雫")]
        public float intensityAddAmount;

        public override void OnTrigger()
        {
            base.OnTrigger();
            WaveManager.instance.AddBattleIntensity(intensityAddAmount);
        }

    }
}