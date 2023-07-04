using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "特效数据集合", menuName = "ScriptableObject/特效数据集合", order = 0)]
public class VfxDataObject : ScriptableObject
{
    public List<VfxData> vfxDatas;
}
