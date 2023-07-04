using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Spine预制件数据集合", menuName = "ScriptableObject/Spine预制件数据集合", order = 0)]
public class SpinePrefabObject : ScriptableObject
{
    public List<SpinePrefab> spinePrefabs;
}
[System.Serializable]
public class SpinePrefab
{
    [Header("id")]
     public string id;
    [Header("名称")]
    public string m_name;
    [Header("对应的预制件")]
    public GameObject spinePrefab;
}