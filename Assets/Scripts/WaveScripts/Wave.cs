using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{
    /// <summary>
    /// һ����λ��ÿһ�����ж��鵥λ
    /// </summary>
    [System.Serializable]
    public class Wave
    {
        public string name;
        public List<OneGroup> groups;
        /// <summary>
        /// ���ݲ����ڶ���ĵ�λ��Ϣ������һ����λ
        /// </summary>
        public void GenerateOneWave()
        {
            foreach (OneGroup group in groups)
            {
                UnitCreateManager.instance.CreateOneTeamUnit(Camp.human, group.soldierId, group.formationId, group.offset);
                //BattleManager.instance.CreateSoldierWithGroup(Camp.human, group.soldierId, group.formationId, true, group.offset);
            }
        }

    }
}
/// <summary>
/// �������е�ÿһ���е�ÿһ�鵥λ
/// </summary>
[System.Serializable]
public class OneGroup
{
    public string name;
    public string soldierId;
    public string formationId;
    public Vector2 offset;
}