using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{
    /// <summary>
    /// 一波单位，每一波中有多组单位
    /// </summary>
    [System.Serializable]
    public class Wave
    {
        public string name;
        public List<OneGroup> groups;
        /// <summary>
        /// 根据波次内多组的单位信息生成这一波单位
        /// </summary>
        public void GenerateOneWave()
        {
            foreach (OneGroup group in groups)
            {
                Vector3 randomOffset = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
                Debug.Log("随机偏移：" + randomOffset);
                UnitCreateManager.instance.CreateOneTeamUnit(Camp.human, group.soldierId, group.formationId, group.offset + randomOffset);
                //BattleManager.instance.CreateSoldierWithGroup(Camp.human, group.soldierId, group.formationId, true, group.offset);
            }
        }

    }
}
/// <summary>
/// 波次类中的每一波中的每一组单位
/// </summary>
[System.Serializable]
public class OneGroup
{
    public string name;
    public string soldierId;
    public string formationId;
    public Vector3 offset;
}