using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{
    public class UnitTrigger_SummonSoldiers : UnitTriggerBase
    {
        [Header("����������������������������������������������������������������������������������������������������������������")]

        [Header("���������ٻ��µĵ�λs")]
        [Header("�ٻ��ĵ�λ����Ӫ")]
        public Camp targetCamp;
        [Header("�ٻ���Ŀ��λ��")]
        public Transform summonTargetTransform;
        [Header("�ٻ��ı���id������id����Ŀ��λ��֮������ƫ�ƣ������豣��һ�£�")]
        public string[] summonIds;
        public string[] summonFormations;
        public Vector3[] summonOffsets;
        public override void OnTrigger(Collider collision)
        {
            base.OnTrigger(collision);
            //���������ɶ�Ӧ��ʿ��s
            for (int i = 0; i < summonIds.Length; i++)
            {
                //BattleManager.instance.CreateSoldierWithGroup(targetCamp, summonIds[i], summonFormations[i], true, summonTargetTransform.position, summonOffsets[i]);
            }
        }

    }
}