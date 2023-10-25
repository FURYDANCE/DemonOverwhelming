using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DemonOverwhelming;
[CreateAssetMenu(fileName = "Buff数据集合", menuName = "ScriptableObject/Buff数据集合", order = 0)]

public class BuffObject : ScriptableObject
{
    public List<BuffInformation> buffs;
}
