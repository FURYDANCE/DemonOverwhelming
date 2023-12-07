using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SoldierFormation
{
    [Header("阵型内的各个兵种对于中心位置的相对位置")]
    public List<Vector3> soldierOffsets;
}
