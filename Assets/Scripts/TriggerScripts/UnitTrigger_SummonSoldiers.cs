using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{
    public class UnitTrigger_SummonSoldiers : UnitTriggerBase
    {
        [Header("————————————————————————————————————————————————————————")]

        [Header("触发器：召唤新的单位s")]
        [Header("召唤的单位的阵营")]
        public Camp targetCamp;
        [Header("召唤的目标位置")]
        public Transform summonTargetTransform;
        [Header("召唤的兵种id，阵型id，和目标位置之间的相对偏移（数量需保持一致）")]
        public string[] summonIds;
        public string[] summonFormations;
        public Vector3[] summonOffsets;
        public override void OnTrigger(Collider collision)
        {
            base.OnTrigger(collision);
            //遍历并生成对应的士兵s
            for (int i = 0; i < summonIds.Length; i++)
            {
                //BattleManager.instance.CreateSoldierWithGroup(targetCamp, summonIds[i], summonFormations[i], true, summonTargetTransform.position, summonOffsets[i]);
            }
        }

    }
}