using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Buff���ݼ���", menuName = "ScriptableObject/Buff���ݼ���", order = 0)]

public class BuffObject : ScriptableObject
{
    public List<BuffInformation> buffs;
}